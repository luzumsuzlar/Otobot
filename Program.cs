using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Velopack;

namespace Otobot
{
    internal static class Program
    {
        [DllImport("user32.dll")] static extern bool SetProcessDpiAwarenessContext(IntPtr value);
        static readonly IntPtr DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2 = new IntPtr(-4);

        [STAThread]
        static void Main()
        {
            // Velopack yalnızca dağıtım derlemelerinde çalışır. Böylece kaynak
            // klasörden başlatılan Debug sürümü, bilgisayardaki eski kurulu
            // sürüme yönlenmeden doğrudan test edilebilir.
#if !DEBUG
            VelopackApp.Build().Run();
#endif

            SetProcessDpiAwarenessContext(DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2);
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}
