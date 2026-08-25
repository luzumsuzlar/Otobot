using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Otobot;

internal sealed class TelegramService
{
    private static readonly HttpClient Http = new()
    {
        Timeout = TimeSpan.FromSeconds(20)
    };

    private readonly string settingsPath = AppDataPaths.GetDataFilePath("telegram_settings.json");

    public TelegramSettings LoadSettings()
    {
        try
        {
            if (!File.Exists(settingsPath)) return new();

            StoredTelegramSettings? stored = JsonSerializer.Deserialize<StoredTelegramSettings>(
                File.ReadAllText(settingsPath));
            if (stored == null) return new();

            return new TelegramSettings
            {
                Token = Unprotect(stored.ProtectedToken),
                ChatId = stored.ChatId ?? string.Empty,
                ReportWindowNumber = Math.Max(1, stored.ReportWindowNumber),
                ReportIntervalMinutes = Math.Clamp(stored.ReportIntervalMinutes, 1, 1440),
                ReportsEnabled = stored.ReportsEnabled
            };
        }
        catch
        {
            return new();
        }
    }

    public void SaveSettings(string token, string chatId)
    {
        ValidateToken(token);

        TelegramSettings current = LoadSettings();
        current.Token = token.Trim();
        current.ChatId = chatId.Trim();
        SaveSettings(current);
    }

    public void SaveSettings(TelegramSettings settings)
    {
        ValidateToken(settings.Token);

        var stored = new StoredTelegramSettings
        {
            ProtectedToken = Protect(settings.Token.Trim()),
            ChatId = settings.ChatId.Trim(),
            ReportWindowNumber = Math.Max(1, settings.ReportWindowNumber),
            ReportIntervalMinutes = Math.Clamp(settings.ReportIntervalMinutes, 1, 1440),
            ReportsEnabled = settings.ReportsEnabled
        };
        File.WriteAllText(settingsPath, JsonSerializer.Serialize(stored));
    }

    public async Task<TelegramChat> FindLatestChatAsync(string token, CancellationToken cancellationToken = default)
    {
        ValidateToken(token);

        using JsonDocument response = await GetApiResponseAsync(token, "getUpdates", cancellationToken);
        JsonElement result = response.RootElement.GetProperty("result");
        for (int index = result.GetArrayLength() - 1; index >= 0; index--)
        {
            JsonElement update = result[index];
            if (!TryGetChat(update, out JsonElement chat)) continue;

            string id = chat.GetProperty("id").GetInt64().ToString();
            string displayName = GetDisplayName(chat);
            return new TelegramChat(id, displayName);
        }

        throw new InvalidOperationException(
            "Botla henüz bir sohbet bulunamadı. Telegram'da bota /start gönderip tekrar deneyin.");
    }

    public async Task SendMessageAsync(
        string token,
        string chatId,
        string message,
        CancellationToken cancellationToken = default)
    {
        ValidateToken(token);
        if (string.IsNullOrWhiteSpace(chatId))
            throw new InvalidOperationException("Önce SOHBETİ BUL VE KAYDET düğmesini kullanın.");

        string endpoint = BuildEndpoint(token, "sendMessage");
        using HttpResponseMessage httpResponse = await Http.PostAsJsonAsync(
            endpoint,
            new { chat_id = chatId.Trim(), text = message },
            cancellationToken);
        await EnsureTelegramSuccessAsync(httpResponse, cancellationToken);
    }

    public async Task SendPhotoAsync(
        string token,
        string chatId,
        Stream image,
        string fileName,
        string caption,
        CancellationToken cancellationToken = default)
    {
        ValidateToken(token);
        if (string.IsNullOrWhiteSpace(chatId))
            throw new InvalidOperationException("Önce SOHBETİ BUL VE KAYDET düğmesini kullanın.");

        using var content = new MultipartFormDataContent();
        content.Add(new StringContent(chatId.Trim()), "chat_id");
        content.Add(new StringContent(caption), "caption");
        using var imageContent = new StreamContent(image);
        imageContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
        content.Add(imageContent, "photo", fileName);

        using HttpResponseMessage response = await Http.PostAsync(
            BuildEndpoint(token, "sendPhoto"), content, cancellationToken);
        await EnsureTelegramSuccessAsync(response, cancellationToken);
    }

    public async Task SendDocumentAsync(
        string token,
        string chatId,
        Stream document,
        string fileName,
        string caption,
        CancellationToken cancellationToken = default)
    {
        ValidateToken(token);
        if (string.IsNullOrWhiteSpace(chatId))
            throw new InvalidOperationException("Önce SOHBETİ BUL VE KAYDET düğmesini kullanın.");

        using var content = new MultipartFormDataContent();
        content.Add(new StringContent(chatId.Trim()), "chat_id");
        content.Add(new StringContent(caption), "caption");
        using var documentContent = new StreamContent(document);
        documentContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
        content.Add(documentContent, "document", fileName);

        using HttpResponseMessage response = await Http.PostAsync(
            BuildEndpoint(token, "sendDocument"), content, cancellationToken);
        await EnsureTelegramSuccessAsync(response, cancellationToken);
    }

    private static async Task<JsonDocument> GetApiResponseAsync(
        string token,
        string method,
        CancellationToken cancellationToken)
    {
        using HttpResponseMessage httpResponse = await Http.GetAsync(
            BuildEndpoint(token, method), cancellationToken);
        string body = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        if (!httpResponse.IsSuccessStatusCode)
            throw CreateSafeApiException(httpResponse.StatusCode, body);

        JsonDocument document = JsonDocument.Parse(body);
        if (!document.RootElement.TryGetProperty("ok", out JsonElement ok) || !ok.GetBoolean())
        {
            document.Dispose();
            throw new InvalidOperationException(GetSafeDescription(body));
        }

        return document;
    }

    private static async Task EnsureTelegramSuccessAsync(
        HttpResponseMessage response,
        CancellationToken cancellationToken)
    {
        string body = await response.Content.ReadAsStringAsync(cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw CreateSafeApiException(response.StatusCode, body);

        using JsonDocument document = JsonDocument.Parse(body);
        if (!document.RootElement.TryGetProperty("ok", out JsonElement ok) || !ok.GetBoolean())
            throw new InvalidOperationException(GetSafeDescription(body));
    }

    private static Exception CreateSafeApiException(System.Net.HttpStatusCode statusCode, string body)
    {
        string description = GetSafeDescription(body);
        return new InvalidOperationException($"Telegram bağlantısı başarısız ({(int)statusCode}): {description}");
    }

    private static string GetSafeDescription(string body)
    {
        try
        {
            using JsonDocument document = JsonDocument.Parse(body);
            if (document.RootElement.TryGetProperty("description", out JsonElement description))
                return description.GetString() ?? "Bilinmeyen Telegram hatası.";
        }
        catch { }

        return "Telegram yanıtı alınamadı. İnternet bağlantısını ve tokeni kontrol edin.";
    }

    private static bool TryGetChat(JsonElement update, out JsonElement chat)
    {
        foreach (string messageType in new[] { "message", "edited_message", "channel_post" })
        {
            if (update.TryGetProperty(messageType, out JsonElement message) &&
                message.TryGetProperty("chat", out chat))
                return true;
        }

        chat = default;
        return false;
    }

    private static string GetDisplayName(JsonElement chat)
    {
        if (chat.TryGetProperty("title", out JsonElement title))
            return title.GetString() ?? "Telegram sohbeti";

        string firstName = chat.TryGetProperty("first_name", out JsonElement first)
            ? first.GetString() ?? string.Empty
            : string.Empty;
        string lastName = chat.TryGetProperty("last_name", out JsonElement last)
            ? last.GetString() ?? string.Empty
            : string.Empty;
        string name = $"{firstName} {lastName}".Trim();
        return string.IsNullOrWhiteSpace(name) ? "Telegram sohbeti" : name;
    }

    private static string BuildEndpoint(string token, string method) =>
        $"https://api.telegram.org/bot{token.Trim()}/{method}";

    private static void ValidateToken(string token)
    {
        string value = token.Trim();
        int separator = value.IndexOf(':');
        if (separator <= 0 || separator == value.Length - 1 ||
            !value[..separator].All(char.IsDigit))
            throw new InvalidOperationException("Telegram bot tokeni geçersiz görünüyor.");
    }

    private static string Protect(string value)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(value);
        byte[] protectedBytes = ProtectedData.Protect(bytes, null, DataProtectionScope.CurrentUser);
        return Convert.ToBase64String(protectedBytes);
    }

    private static string Unprotect(string? value)
    {
        if (string.IsNullOrWhiteSpace(value)) return string.Empty;
        byte[] protectedBytes = Convert.FromBase64String(value);
        byte[] bytes = ProtectedData.Unprotect(protectedBytes, null, DataProtectionScope.CurrentUser);
        return Encoding.UTF8.GetString(bytes);
    }

    private sealed class StoredTelegramSettings
    {
        public string ProtectedToken { get; set; } = string.Empty;
        public string ChatId { get; set; } = string.Empty;
        public int ReportWindowNumber { get; set; } = 3;
        public int ReportIntervalMinutes { get; set; } = 60;
        public bool ReportsEnabled { get; set; }
    }
}

internal sealed class TelegramSettings
{
    public string Token { get; set; } = string.Empty;
    public string ChatId { get; set; } = string.Empty;
    public int ReportWindowNumber { get; set; } = 3;
    public int ReportIntervalMinutes { get; set; } = 60;
    public bool ReportsEnabled { get; set; }
}

internal sealed record TelegramChat(string Id, string DisplayName);
