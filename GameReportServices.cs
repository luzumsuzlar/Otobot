using System.Drawing;
using System.Globalization;
using System.Text.Json;
using Tesseract;

namespace Otobot;

internal sealed class GameReportOcrService
{
    private readonly string dataPath = Path.Combine(AppContext.BaseDirectory, "tessdata");

    public decimal ReadBalance(Bitmap fullscreenGame)
    {
        string value = ReadNumber(
            fullscreenGame,
            RelativeArea(fullscreenGame, .07, .775, .10, .06),
            "0123456789.,");

        value = value.Replace(',', '.');
        if (!decimal.TryParse(value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal balance))
            throw new InvalidOperationException("Bakiye sayısal olarak okunamadı.");
        return balance;
    }

    public IReadOnlyList<long> ReadTopTen(Bitmap fullscreenRankings)
    {
        var scores = new List<long>(10);
        for (int row = 0; row < 10; row++)
        {
            string text = ReadNumber(
                fullscreenRankings,
                RelativeArea(fullscreenRankings, .58, .385 + row * .03, .09, .03),
                "0123456789");
            string digits = new(text.Where(char.IsDigit).ToArray());
            if (!long.TryParse(digits, NumberStyles.None, CultureInfo.InvariantCulture, out long score) || score <= 0)
                throw new InvalidOperationException($"Sıralamadaki {row + 1}. puan okunamadı.");
            scores.Add(score);
        }
        return scores;
    }

    string ReadNumber(Bitmap source, Rectangle area, string whitelist)
    {
        Rectangle clipped = Rectangle.Intersect(new Rectangle(0, 0, source.Width, source.Height), area);
        if (clipped.Width <= 0 || clipped.Height <= 0)
            throw new InvalidOperationException("Okunacak görüntü alanı hesaplanamadı.");

        string imagePath = Path.Combine(Path.GetTempPath(), $"otobot-ocr-{Guid.NewGuid():N}.png");
        try
        {
            using Bitmap crop = source.Clone(clipped, source.PixelFormat);
            crop.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);
            using var engine = new TesseractEngine(dataPath, "eng", EngineMode.LstmOnly);
            engine.SetVariable("tessedit_char_whitelist", whitelist);
            using Pix pix = Pix.LoadFromFile(imagePath);
            using Page page = engine.Process(pix, PageSegMode.SingleLine);
            return page.GetText().Trim();
        }
        finally
        {
            try { File.Delete(imagePath); } catch { }
        }
    }

    static Rectangle RelativeArea(Bitmap source, double x, double y, double width, double height) => new(
        (int)Math.Round(source.Width * x),
        (int)Math.Round(source.Height * y),
        (int)Math.Round(source.Width * width),
        (int)Math.Round(source.Height * height));
}

internal sealed class GameReportHistoryService
{
    private readonly string historyPath = AppDataPaths.GetDataFilePath("telegram_ranking_history.json");

    public GameReportSnapshot? Load()
    {
        try
        {
            if (!File.Exists(historyPath)) return null;
            return JsonSerializer.Deserialize<GameReportSnapshot>(File.ReadAllText(historyPath));
        }
        catch { return null; }
    }

    public void Save(GameReportSnapshot snapshot) =>
        File.WriteAllText(historyPath, JsonSerializer.Serialize(snapshot));
}

internal sealed class GameReportSnapshot
{
    public DateTime CapturedAt { get; set; }
    public decimal Balance { get; set; }
    public List<long> Scores { get; set; } = [];
}
