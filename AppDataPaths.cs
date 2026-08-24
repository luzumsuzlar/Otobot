namespace Otobot;

internal static class AppDataPaths
{
    private static readonly string DataDirectory = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "Otobot");

    private static readonly string[] LegacyDataDirectories =
    [
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Chrome11Bot"),
        AppContext.BaseDirectory
    ];

    public static string GetDataFilePath(string fileName)
    {
        Directory.CreateDirectory(DataDirectory);

        string targetPath = Path.Combine(DataDirectory, fileName);
        // Carry settings from Chrome11Bot and older portable/debug builds into
        // Otobot's update-safe per-user directory on first launch.
        foreach (string legacyDirectory in LegacyDataDirectories)
        {
            if (File.Exists(targetPath)) break;

            string legacyPath = Path.Combine(legacyDirectory, fileName);
            if (!File.Exists(legacyPath)) continue;

            try
            {
                File.Copy(legacyPath, targetPath, overwrite: false);
            }
            catch
            {
                // The application can continue with defaults if migration is
                // blocked; normal save operations will report their own errors.
            }
        }

        return targetPath;
    }
}
