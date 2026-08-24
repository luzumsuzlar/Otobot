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
            // Velopack must run before WinForms initialization so it can finish
            // install/update lifecycle operations without opening the main UI.
            VelopackApp.Build().Run();

            SetProcessDpiAwarenessContext(DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2);
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}
