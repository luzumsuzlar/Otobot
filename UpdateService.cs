using Velopack;
using Velopack.Sources;

namespace Otobot;

internal static class UpdateService
{
    private const string RepositoryUrl = "https://github.com/luzumsuzlar/Otobot";
    private static readonly SemaphoreSlim CheckGate = new(1, 1);

    public static async Task CheckForUpdatesAsync(
        IWin32Window owner,
        bool showNoUpdateMessage,
        Action<string>? reportStatus = null,
        Action<string>? reportWarning = null)
    {
        if (!await CheckGate.WaitAsync(0))
        {
            if (showNoUpdateMessage)
                reportWarning?.Invoke("Bir güncelleme denetimi zaten çalışıyor.");
            return;
        }

        try
        {
            var source = new GithubSource(RepositoryUrl, accessToken: null, prerelease: false);
            var manager = new UpdateManager(source);

            if (!manager.IsInstalled)
            {
                if (showNoUpdateMessage)
                    reportWarning?.Invoke(
                        "Otomatik güncelleme yalnızca Otobot Setup ile kurulan sürümde çalışır.");
                return;
            }

            reportStatus?.Invoke("Güncelleme denetleniyor...");
            var update = await manager.CheckForUpdatesAsync();

            if (update == null)
            {
                reportStatus?.Invoke("Otobot güncel.");
                return;
            }

            string targetVersion = update.TargetFullRelease.Version.ToString();
            var answer = MessageBox.Show(
                owner,
                $"Otobot {targetVersion} sürümü hazır. Şimdi indirip kurmak ister misiniz?\n\n" +
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
                reportWarning?.Invoke("Güncelleme denetlenemedi:\n" + ex.Message);
        }
        finally
        {
            CheckGate.Release();
        }
    }
}
