using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using MimeKit;

namespace Otobot;

internal sealed class GmailCodeService
{
    private readonly string settingsPath = AppDataPaths.GetDataFilePath("gmail_code_settings.json");

    public GmailCodeSettings LoadSettings()
    {
        try
        {
            if (!File.Exists(settingsPath)) return new();
            StoredGmailCodeSettings? stored = JsonSerializer.Deserialize<StoredGmailCodeSettings>(File.ReadAllText(settingsPath));
            if (stored == null) return new();
            return new GmailCodeSettings
            {
                Address = stored.Address ?? string.Empty,
                AppPassword = Unprotect(stored.ProtectedAppPassword),
                ExpectedSender = stored.ExpectedSender ?? string.Empty
            };
        }
        catch { return new(); }
    }

    public void SaveSettings(GmailCodeSettings settings)
    {
        Validate(settings);
        var stored = new StoredGmailCodeSettings
        {
            Address = settings.Address.Trim(),
            ProtectedAppPassword = Protect(settings.AppPassword.Replace(" ", string.Empty)),
            ExpectedSender = settings.ExpectedSender.Trim()
        };
        File.WriteAllText(settingsPath, JsonSerializer.Serialize(stored));
    }

    public async Task<GmailVerificationCode> FindRecentCodeAsync(
        GmailCodeSettings settings,
        DateTime notBefore,
        CancellationToken cancellationToken = default)
    {
        Validate(settings);
        using var client = new ImapClient();
        await client.ConnectAsync("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect, cancellationToken);
        await client.AuthenticateAsync(settings.Address.Trim(), settings.AppPassword.Replace(" ", string.Empty), cancellationToken);
        // Gmail'de bazı bildirimler Gelen Kutusu etiketini taşımayabilir.
        // Bu nedenle tüm postalar klasöründe ara.
        IMailFolder inbox = client.GetFolder(SpecialFolder.All) ?? client.Inbox;
        await inbox.OpenAsync(FolderAccess.ReadOnly, cancellationToken);

        IList<UniqueId> ids = await inbox.SearchAsync(
            SearchQuery.DeliveredAfter(notBefore.AddMinutes(-1)), cancellationToken);
        foreach (UniqueId id in ids.Reverse().Take(25))
        {
            MimeMessage message = await inbox.GetMessageAsync(id, cancellationToken);
            string sender = message.From.Mailboxes.FirstOrDefault()?.Address ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(settings.ExpectedSender) &&
                !sender.Contains(settings.ExpectedSender, StringComparison.OrdinalIgnoreCase))
                continue;

            string plainText = message.GetTextBody(MimeKit.Text.TextFormat.Plain) ?? string.Empty;
            string htmlText = message.GetTextBody(MimeKit.Text.TextFormat.Html) ?? string.Empty;
            string content = $"{message.Subject}\n{plainText}\n{htmlText}";
            string? code = FindVerificationCode(content);
            if (code != null)
                return new GmailVerificationCode(code, sender, message.Subject ?? string.Empty);
        }

        throw new InvalidOperationException(
            "Belirtilen zaman aralığında doğrulama kodu bulunamadı. Gönderici filtresini kontrol edin.");
    }

    static void Validate(GmailCodeSettings settings)
    {
        if (string.IsNullOrWhiteSpace(settings.Address) || !settings.Address.Contains('@'))
            throw new InvalidOperationException("Geçerli Gmail adresini girin.");
        if (settings.AppPassword.Replace(" ", string.Empty).Length < 16)
            throw new InvalidOperationException("Gmail uygulama şifresi eksik görünüyor.");
    }

    static string? FindVerificationCode(string content)
    {
        // Site adresi, tarih ve IP gibi sayıları kod sanma. Önce e-postadaki
        // "aşağıdaki kodu kullanın" türü ifadeden hemen sonra gelen sayıyı ara.
        Match explicitCode = Regex.Match(
            content,
            @"(?:aşağıdaki\s+)?(?:doğrulama\s+|güvenlik\s+|oturum\s+açma\s+)?kodu\s+(?:kullan(?:ın|in)|gir(?:in|meniz))\s*[:\-]?\s*(?:<[^>]+>\s*){0,4}[^0-9]{0,50}(?<!\d)(\d{4,8})(?!\d)",
            RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        if (explicitCode.Success) return explicitCode.Groups[1].Value;

        foreach (Match candidate in Regex.Matches(content, @"(?<!\d)(\d{6})(?!\d)"))
        {
            int contextStart = Math.Max(0, candidate.Index - 120);
            string context = content[contextStart..candidate.Index];
            if (context.Contains("http", StringComparison.OrdinalIgnoreCase) &&
                context.LastIndexOf("http", StringComparison.OrdinalIgnoreCase) >
                context.LastIndexOfAny(['\n', ' ', '\t']))
                continue;
            return candidate.Groups[1].Value;
        }

        return null;
    }

    static string Protect(string value) => Convert.ToBase64String(
        ProtectedData.Protect(Encoding.UTF8.GetBytes(value), null, DataProtectionScope.CurrentUser));

    static string Unprotect(string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) return string.Empty;
        return Encoding.UTF8.GetString(ProtectedData.Unprotect(
            Convert.FromBase64String(value), null, DataProtectionScope.CurrentUser));
    }

    private sealed class StoredGmailCodeSettings
    {
        public string? Address { get; set; }
        public string? ProtectedAppPassword { get; set; }
        public string? ExpectedSender { get; set; }
    }
}

internal sealed class GmailCodeSettings
{
    public string Address { get; set; } = string.Empty;
    public string AppPassword { get; set; } = string.Empty;
    public string ExpectedSender { get; set; } = string.Empty;
}

internal sealed record GmailVerificationCode(string Code, string Sender, string Subject);
