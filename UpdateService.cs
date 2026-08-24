using Velopack;
using Velopack.Sources;

namespace Chrome11Bot;

internal static class UpdateService
{
    private const string RepositoryUrl = "https://github.com/luzumsuzlar/Chrome11Bot";
    private static readonly SemaphoreSlim CheckGate = new(1, 1);

    public static async Task CheckForUpdatesAsync(
        IWin32Window owner,
        bool showNoUpdateMessage,
        Action<string>? reportStatus = null)
    {
        if (!await CheckGate.WaitAsync(0))
        {
            if (showNoUpdateMessage)
                MessageBox.Show(owner, "Bir güncelleme denetimi zaten çalışıyor.", "Chrome11Bot");
            return;
        }

        try
        {
            var source = new GithubSource(RepositoryUrl, accessToken: null, prerelease: false);
            var manager = new UpdateManager(source);

            if (!manager.IsInstalled)
            {
                if (showNoUpdateMessage)
                {
                    MessageBox.Show(
                        owner,
                        "Otomatik güncelleme yalnızca Chrome11Bot Setup ile kurulan sürümde çalışır.",
                        "Chrome11Bot");
                }
                return;
            }

            reportStatus?.Invoke("Güncelleme denetleniyor...");
            var update = await manager.CheckForUpdatesAsync();

            if (update == null)
            {
                reportStatus?.Invoke("Chrome11Bot güncel.");
                if (showNoUpdateMessage)
                    MessageBox.Show(owner, "En güncel sürümü kullanıyorsunuz.", "Chrome11Bot");
                return;
            }

            string targetVersion = update.TargetFullRelease.Version.ToString();
            var answer = MessageBox.Show(
                owner,
                $"Chrome11Bot {targetVersion} sürümü hazır. Şimdi indirip kurmak ister misiniz?\n\n" +
                "Güncelleme tamamlandığında uygulama yeniden başlatılacak.",
                "Güncelleme hazır",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);

            if (answer != DialogResult.Yes)
            {
                reportStatus?.Invoke($"{targetVersion} güncellemesi daha sonra kurulacak.");
                return;
            }

            reportStatus?.Invoke($"{targetVersion} güncellemesi indiriliyor...");
            await manager.DownloadUpdatesAsync(update);
            reportStatus?.Invoke("Güncelleme kuruluyor; uygulama yeniden başlatılacak...");
            manager.ApplyUpdatesAndRestart(update);
        }
        catch (Exception ex)
        {
            reportStatus?.Invoke("Güncelleme denetlenemedi.");
            if (showNoUpdateMessage)
            {
                MessageBox.Show(
                    owner,
                    "Güncelleme denetlenemedi:\n" + ex.Message,
                    "Chrome11Bot",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }
        finally
        {
            CheckGate.Release();
        }
    }
}
