using System.Text.Json;

namespace Otobot;

internal sealed class UrlListService
{
    private readonly string settingsPath = AppDataPaths.GetDataFilePath("url_list.json");

    public UrlListSettings Load()
    {
        try
        {
            if (!File.Exists(settingsPath)) return Normalize(CreateDefault());
            UrlListSettings? settings = JsonSerializer.Deserialize<UrlListSettings>(File.ReadAllText(settingsPath));
            return Normalize(settings ?? CreateDefault());
        }
        catch { return Normalize(CreateDefault()); }
    }

    public void Save(UrlListSettings settings)
    {
        UrlListSettings normalized = Normalize(settings);
        if (!Uri.TryCreate(normalized.BaseAddress, UriKind.Absolute, out Uri? baseUri) ||
            (baseUri.Scheme != Uri.UriSchemeHttp && baseUri.Scheme != Uri.UriSchemeHttps))
            throw new InvalidOperationException("Ana adres http:// veya https:// ile başlayan geçerli bir adres olmalı.");
        if (normalized.Remainders.Any(value => !string.IsNullOrWhiteSpace(value) && !value.TrimStart().StartsWith('/')))
            throw new InvalidOperationException("Kalan URL alanları / ile başlamalıdır.");
        File.WriteAllText(settingsPath, JsonSerializer.Serialize(normalized));
    }

    static UrlListSettings CreateDefault() => new()
    {
        BaseAddress = "https://www.2011marsbahis.com",
        Remainders = ["/tr/casino?game_player=23063&gv_type=r"]
    };

    static UrlListSettings Normalize(UrlListSettings settings)
    {
        settings.BaseAddress = settings.BaseAddress?.Trim().TrimEnd('/') ?? string.Empty;
        settings.Remainders ??= [];
        settings.Remainders = settings.Remainders
            .Select(value => value?.Trim() ?? string.Empty)
            .ToList();
        return settings;
    }
}

internal sealed class UrlListSettings
{
    public string BaseAddress { get; set; } = string.Empty;
    public List<string> Remainders { get; set; } = [];
}
