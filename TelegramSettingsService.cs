using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Otobot;

internal sealed class TelegramSettingsService
{
    readonly string path = AppDataPaths.GetDataFilePath("telegram_settings.json");

    public TelegramSettings Load()
    {
        try
        {
            StoredTelegramSettings? stored = JsonSerializer.Deserialize<StoredTelegramSettings>(File.ReadAllText(path));
            return stored == null ? new() : new TelegramSettings
            {
                BotToken = Unprotect(stored.BotToken),
                ChatId = stored.ChatId ?? string.Empty
            };
        }
        catch { return new(); }
    }

    public void Save(TelegramSettings settings) => File.WriteAllText(path,
        JsonSerializer.Serialize(new StoredTelegramSettings
        {
            BotToken = Protect(settings.BotToken),
            ChatId = settings.ChatId?.Trim() ?? string.Empty
        }));

    static string Protect(string? value) => Convert.ToBase64String(
        ProtectedData.Protect(Encoding.UTF8.GetBytes(value ?? string.Empty), null, DataProtectionScope.CurrentUser));

    static string Unprotect(string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) return string.Empty;
        return Encoding.UTF8.GetString(ProtectedData.Unprotect(Convert.FromBase64String(value), null, DataProtectionScope.CurrentUser));
    }

    sealed class StoredTelegramSettings
    {
        public string BotToken { get; set; } = string.Empty;
        public string ChatId { get; set; } = string.Empty;
    }
}

internal sealed class TelegramSettings
{
    public string BotToken { get; set; } = string.Empty;
    public string ChatId { get; set; } = string.Empty;
}

internal sealed class TelegramService
{
    static readonly HttpClient Client = new() { Timeout = TimeSpan.FromSeconds(20) };

    public async Task SendMessageAsync(string botToken, string chatId, string message, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(botToken) || string.IsNullOrWhiteSpace(chatId))
            throw new InvalidOperationException("Telegram bot tokeni ve sohbet kimliği girilmeli.");

        using var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            ["chat_id"] = chatId.Trim(),
            ["text"] = message
        });
        using HttpResponseMessage response = await Client.PostAsync(
            $"https://api.telegram.org/bot{botToken.Trim()}/sendMessage", content, cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new InvalidOperationException($"Telegram bildirimi gönderilemedi (HTTP {(int)response.StatusCode}).");
    }
}
