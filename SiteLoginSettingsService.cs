using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Otobot;

// Site parolası düz metin olarak saklanmaz. Windows DPAPI ile yalnızca bu
// Windows kullanıcısının aynı bilgisayarında çözülebilir.
internal sealed class SiteLoginSettingsService
{
    readonly string path = AppDataPaths.GetDataFilePath("site_login_settings.json");

    public SiteLoginSettings Load()
    {
        try
        {
            if (!File.Exists(path)) return new();
            var saved = JsonSerializer.Deserialize<StoredSiteLoginSettings>(File.ReadAllText(path));
            return saved == null ? new() : new SiteLoginSettings
            {
                UserName = saved.UserName ?? string.Empty,
                Password = Unprotect(saved.ProtectedPassword)
            };
        }
        catch { return new(); }
    }

    public void Save(SiteLoginSettings settings)
    {
        if (string.IsNullOrWhiteSpace(settings.UserName))
            throw new InvalidOperationException("Site kullanıcı adını girin.");
        if (string.IsNullOrWhiteSpace(settings.Password))
            throw new InvalidOperationException("Site şifresini girin.");
        File.WriteAllText(path, JsonSerializer.Serialize(new StoredSiteLoginSettings
        {
            UserName = settings.UserName.Trim(),
            ProtectedPassword = Protect(settings.Password)
        }));
    }

    static string Protect(string value) => Convert.ToBase64String(
        ProtectedData.Protect(Encoding.UTF8.GetBytes(value), null, DataProtectionScope.CurrentUser));
    static string Unprotect(string? value) => string.IsNullOrWhiteSpace(value) ? string.Empty :
        Encoding.UTF8.GetString(ProtectedData.Unprotect(Convert.FromBase64String(value), null, DataProtectionScope.CurrentUser));

    sealed class StoredSiteLoginSettings
    {
        public string? UserName { get; set; }
        public string? ProtectedPassword { get; set; }
    }
}

internal sealed class SiteLoginSettings
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
