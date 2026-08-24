namespace Chrome11Bot;

internal static class AppDataPaths
{
    private static readonly string DataDirectory = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "Chrome11Bot");

    public static string GetDataFilePath(string fileName)
    {
        Directory.CreateDirectory(DataDirectory);

        string targetPath = Path.Combine(DataDirectory, fileName);
        string legacyPath = Path.Combine(AppContext.BaseDirectory, fileName);

        // Carry settings created by older portable/debug builds into the
        // update-safe per-user data directory on first launch.
        if (!File.Exists(targetPath) && File.Exists(legacyPath))
        {
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
