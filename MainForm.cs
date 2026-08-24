using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;

namespace Chrome11Bot;

static class EmbeddedTemplates
{
    // User-provided common KAPAT button template: 102x26 PNG.
    public static readonly byte[] CloseButton = new byte[] { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82, 0, 0, 0, 102, 0, 0, 0, 26, 8, 6, 0, 0, 0, 69, 63, 41, 216, 0, 0, 0, 1, 115, 82, 71, 66, 0, 174, 206, 28, 233, 0, 0, 0, 4, 103, 65, 77, 65, 0, 0, 177, 143, 11, 252, 97, 5, 0, 0, 0, 9, 112, 72, 89, 115, 0, 0, 14, 195, 0, 0, 14, 195, 1, 199, 111, 168, 100, 0, 0, 2, 172, 73, 68, 65, 84, 104, 67, 237, 214, 95, 72, 83, 81, 0, 199, 241, 239, 166, 173, 37, 148, 154, 46, 210, 76, 251, 135, 138, 172, 63, 12, 234, 41, 35, 145, 40, 34, 53, 180, 32, 10, 130, 178, 66, 36, 138, 200, 7, 31, 42, 179, 132, 44, 178, 135, 44, 168, 64, 35, 140, 94, 132, 200, 10, 234, 33, 43, 12, 137, 148, 204, 80, 44, 203, 12, 117, 102, 211, 150, 127, 82, 215, 166, 185, 30, 46, 66, 158, 199, 237, 140, 46, 238, 124, 224, 62, 220, 223, 57, 156, 11, 247, 199, 185, 247, 24, 172, 86, 171, 23, 69, 119, 140, 98, 160, 232, 131, 42, 70, 167, 84, 49, 58, 165, 138, 209, 41, 85, 140, 78, 169, 98, 116, 202, 16, 200, 227, 242, 18, 139, 7, 179, 41, 96, 203, 255, 87, 83, 94, 232, 237, 55, 225, 153, 52, 136, 67, 82, 72, 47, 102, 78, 168, 151, 163, 187, 251, 201, 218, 52, 72, 84, 248, 164, 56, 60, 235, 60, 107, 88, 64, 229, 35, 11, 45, 95, 230, 137, 67, 126, 145, 90, 76, 108, 180, 135, 242, 130, 110, 18, 227, 127, 139, 67, 179, 222, 169, 27, 113, 212, 212, 69, 136, 177, 207, 164, 254, 99, 46, 228, 247, 6, 101, 41, 0, 37, 121, 118, 86, 175, 114, 137, 177, 207, 164, 21, 147, 145, 58, 132, 45, 121, 76, 140, 253, 118, 250, 26, 244, 13, 64, 215, 55, 200, 59, 15, 63, 135, 181, 252, 102, 53, 28, 58, 11, 147, 127, 180, 251, 252, 18, 216, 87, 8, 185, 69, 112, 240, 12, 12, 143, 130, 195, 9, 91, 243, 224, 245, 123, 104, 254, 8, 71, 138, 97, 77, 142, 54, 175, 226, 254, 140, 199, 72, 145, 155, 49, 32, 70, 62, 147, 86, 76, 154, 109, 68, 140, 164, 233, 232, 129, 210, 74, 184, 120, 2, 22, 134, 195, 232, 56, 12, 12, 66, 98, 2, 244, 124, 215, 230, 68, 69, 192, 229, 147, 80, 81, 12, 27, 172, 208, 209, 13, 77, 31, 32, 115, 51, 52, 183, 195, 186, 100, 184, 85, 4, 89, 105, 218, 188, 220, 108, 241, 41, 254, 75, 95, 63, 194, 92, 73, 135, 29, 105, 197, 36, 196, 120, 196, 72, 10, 135, 19, 142, 151, 66, 152, 89, 187, 0, 90, 62, 195, 178, 88, 72, 181, 193, 203, 70, 45, 115, 14, 65, 65, 153, 182, 91, 218, 58, 97, 229, 82, 104, 104, 129, 236, 116, 109, 13, 135, 115, 198, 178, 1, 19, 103, 145, 243, 30, 164, 21, 227, 158, 8, 204, 177, 209, 18, 9, 15, 203, 33, 101, 5, 60, 120, 14, 94, 47, 60, 173, 7, 151, 27, 186, 250, 160, 177, 85, 251, 108, 77, 239, 152, 202, 115, 112, 181, 16, 236, 14, 248, 49, 4, 245, 205, 16, 26, 2, 175, 154, 196, 149, 3, 67, 214, 241, 89, 90, 49, 109, 95, 229, 30, 23, 167, 25, 141, 16, 98, 132, 253, 153, 80, 251, 6, 106, 27, 192, 96, 128, 3, 59, 97, 207, 54, 88, 155, 164, 253, 67, 68, 141, 173, 176, 119, 59, 236, 218, 2, 135, 115, 180, 98, 92, 110, 113, 150, 92, 131, 191, 66, 233, 113, 152, 196, 216, 39, 210, 142, 203, 182, 164, 113, 238, 20, 117, 138, 113, 80, 185, 253, 56, 154, 43, 247, 22, 139, 177, 79, 164, 237, 152, 166, 246, 48, 170, 158, 68, 137, 113, 208, 232, 176, 155, 185, 94, 189, 72, 140, 125, 38, 173, 24, 128, 75, 85, 49, 220, 13, 194, 114, 222, 125, 10, 227, 88, 89, 60, 238, 9, 121, 175, 83, 218, 167, 236, 95, 182, 164, 49, 118, 108, 28, 38, 101, 185, 11, 179, 105, 74, 28, 158, 21, 188, 24, 232, 234, 51, 241, 226, 237, 124, 106, 234, 34, 197, 97, 191, 5, 164, 24, 197, 127, 242, 246, 158, 34, 149, 42, 70, 167, 84, 49, 58, 165, 138, 209, 41, 85, 140, 78, 253, 5, 48, 189, 197, 35, 124, 38, 191, 245, 0, 0, 0, 0, 73, 69, 78, 68, 174, 66, 96, 130 };
}

public class MainForm : Form
{
    [DllImport("user32.dll")] static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);
    [DllImport("user32.dll", CharSet = CharSet.Unicode)] static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
    [DllImport("user32.dll")] static extern bool IsWindowVisible(IntPtr hWnd);
    [DllImport("user32.dll")] static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
    [DllImport("user32.dll")] static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint pid);
    [DllImport("user32.dll")] static extern bool SetForegroundWindow(IntPtr hWnd);
    [DllImport("user32.dll")] static extern IntPtr GetForegroundWindow();
    [DllImport("user32.dll")] static extern bool BringWindowToTop(IntPtr hWnd);
    [DllImport("user32.dll")] static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);
    [DllImport("kernel32.dll")] static extern uint GetCurrentThreadId();
    [DllImport("user32.dll")] static extern bool IsWindow(IntPtr hWnd);
    [DllImport("user32.dll", SetLastError = true)] static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
    [DllImport("user32.dll")] static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    [DllImport("user32.dll")] static extern bool SetCursorPos(int X, int Y);
    [DllImport("user32.dll")] static extern void mouse_event(uint flags, uint dx, uint dy, uint data, UIntPtr extraInfo);
    [DllImport("user32.dll")] static extern bool GetCursorPos(out POINT point);
    [DllImport("user32.dll")] static extern bool RegisterHotKey(IntPtr hWnd, int id, uint modifiers, uint vk);
    [DllImport("user32.dll")] static extern bool UnregisterHotKey(IntPtr hWnd, int id);
    [DllImport("user32.dll", SetLastError = true)] static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);
    [DllImport("user32.dll", SetLastError = true)] static extern bool UnhookWindowsHookEx(IntPtr hhk);
    [DllImport("user32.dll")] static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)] static extern IntPtr GetModuleHandle(string? lpModuleName);
    [DllImport("user32.dll", SetLastError = true)] static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

    delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);
    const int WH_MOUSE_LL = 14;
    const int WM_LBUTTONDOWN = 0x0201;
    const int WM_LBUTTONUP = 0x0202;

    delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
    const int WH_KEYBOARD_LL = 13;
    const int WM_KEYDOWN = 0x0100;
    const int WM_SYSKEYDOWN = 0x0104;
    const int VK_F8 = 0x77;
    const int VK_F9 = 0x78;
    const int VK_F11 = 0x7A;
    const int VK_F12 = 0x7B;

    delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
    [StructLayout(LayoutKind.Sequential)] struct RECT { public int Left, Top, Right, Bottom; }
    [StructLayout(LayoutKind.Sequential)] struct POINT { public int X, Y; }

    const int SW_RESTORE = 9;
    const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
    const uint MOUSEEVENTF_LEFTUP = 0x0004;
    const uint MOD_NOREPEAT = 0x4000;
    const int HOTKEY_F8 = 5001, HOTKEY_F12 = 5002, HOTKEY_F11 = 5003, HOTKEY_F9 = 5004;

    readonly DataGridView grid = new();
    readonly Label status = new();
    readonly Label hotkeyStatus = new();
    readonly Button scanButton = new();
    readonly NumericUpDown reloadWaitBox = new();
    readonly NumericUpDown scanIntervalBox = new();
    readonly NumericUpDown actionClickDelayBox = new();

    readonly Button detectButton = new();
    readonly Button errorRefreshButton = new();
    readonly Button refreshAllButton = new();
    readonly Button saveCoordButton = new();
    readonly Button captureClickButton = new();
    readonly Button captureCoordButton = new();
    readonly Button startButton = new();
    readonly Button stopButton = new();
    readonly Button saveSessionButton = new();
    readonly Button restoreSelectedButton = new();
    readonly Button restoreAllButton = new();
    readonly NumericUpDown thresholdBox = new();
    readonly TextBox oldDomainBox = new();
    readonly TextBox newDomainBox = new();
    readonly Button replaceDomainButton = new();
    readonly Button checkUpdateButton = new();

    readonly List<ChromeWindow> windows = new();
    readonly string coordFile = AppDataPaths.GetDataFilePath("refresh_coordinates.json");
    readonly string sessionFile = AppDataPaths.GetDataFilePath("chrome_session.json");
    Mat? closeButtonTemplate;
    CancellationTokenSource? scanCts;
    int pageReloadWaitSeconds = 30;
    int scanIntervalSeconds = 60;
    int actionClickDelayMs = 500;

    int selectedClickNumber = 1;
    IntPtr keyboardHook = IntPtr.Zero;
    LowLevelKeyboardProc? keyboardProc;
    IntPtr mouseHook = IntPtr.Zero;
    LowLevelMouseProc? mouseProc;
    bool autoCoordinateCapture = false;
    bool suppressFirstAutoClick = false;
    bool suppressFirstAutoMouseUp = false;

    int autoCoordinateRow = 0;
    int autoCaptureStep = 0; // 0=yenileme, 1=işlem1, 2=işlem2, 3=işlem3
    int pendingSpecificClickNumber = 0;
    bool suppressSpecificMouseDown = false;

    readonly Button autoCoordinateButton = new();
    readonly Button captureClick1Button = new();
    readonly Button captureClick2Button = new();
    readonly Button captureClick3Button = new();

    public MainForm()
    {
        Text = "Chrome 11 Bot - Hata Ekranı Algılama";
        Width = 1250; Height = 700; StartPosition = FormStartPosition.CenterScreen;
        var applicationIcon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);
        if (applicationIcon != null) Icon = applicationIcon;

        scanButton.Text = "Chrome Pencerelerini Tara"; scanButton.AutoSize = true; scanButton.Click += (_, _) => ScanWindows();
        detectButton.Text = "Hata Ekranlarını Tara"; detectButton.AutoSize = true; detectButton.Click += (_, _) => DetectErrors();
        errorRefreshButton.Text = "Hatalı Ekranları Yenile"; errorRefreshButton.AutoSize = true; errorRefreshButton.Click += async (_, _) => await RefreshDetectedErrorsAsync();
        refreshAllButton.Text = "Tüm Sayfaları Yenile"; refreshAllButton.AutoSize = true; refreshAllButton.Click += async (_, _) => await RefreshAllPagesAsync();
        startButton.Text = "BAŞLAT (F12)";
        startButton.AutoSize = true;
        startButton.Click += (_, _) => StartContinuousScan();

        stopButton.Text = "DURDUR (F11)";
        stopButton.AutoSize = true;
        stopButton.Click += (_, _) => StopContinuousScan();

        captureCoordButton.Text = "Mouse Konumunu Ata (F8)"; captureCoordButton.AutoSize = true; captureCoordButton.Click += (_, _) => CaptureCurrentMousePosition();
        saveCoordButton.Text = "Koordinatları Kaydet"; saveCoordButton.AutoSize = true; saveCoordButton.Click += (_, _) => SaveCoordinates();

        captureClickButton.Text = "İşlem Koordinatı Kaydet (F9)";
        captureClickButton.AutoSize = true;
        captureClickButton.Click += (_, _) => CaptureClickCoordinate();

        saveSessionButton.Text = "PENCERELERİ KAYDET";
        saveSessionButton.AutoSize = true;
        saveSessionButton.Click += async (_, _) => await SaveSessionAsync();

        restoreSelectedButton.Text = "SEÇİLİ PENCEREYİ GERİ YÜKLE";
        restoreSelectedButton.AutoSize = true;
        restoreSelectedButton.Click += async (_, _) => await RestoreSelectedSessionAsync();

        restoreAllButton.Text = "KAYITLI PENCERELERİ GERİ YÜKLE";
        restoreAllButton.AutoSize = true;
        restoreAllButton.Click += async (_, _) => await RestoreMissingSessionsAsync();

        thresholdBox.DecimalPlaces = 2;
        thresholdBox.Minimum = .50m;
        thresholdBox.Maximum = .99m;
        thresholdBox.Increment = .01m;
        thresholdBox.Value = .80m;
        thresholdBox.Width = 75;

        hotkeyStatus.Text = "F12: Başlat | F11: Durdur | F8: Yenileme | F9: İşlem";
        hotkeyStatus.AutoSize = true;
        hotkeyStatus.Padding = new Padding(8, 8, 0, 0);

        // ---------------- PENCERELER SEKME ----------------
        var windowsPage = new TabPage("🪟 Pencereler");

        var windowTop = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 122,
            Padding = new Padding(8),
            WrapContents = true,
            AutoScroll = true
        };

        scanButton.Text = "Chrome Pencerelerini Tara";
        scanButton.AutoSize = true;
        detectButton.Text = "Hata Ekranlarını Tara";
        detectButton.AutoSize = true;
        errorRefreshButton.Text = "Hatalı Ekranları Yenile";
        errorRefreshButton.AutoSize = true;
        refreshAllButton.Text = "Tüm Sayfaları Yenile";
        refreshAllButton.AutoSize = true;

        startButton.Text = "BAŞLAT (F12)";
        startButton.AutoSize = true;
        startButton.Font = new Font(Font.FontFamily, 9, FontStyle.Bold);
        stopButton.Text = "DURDUR (F11)";
        stopButton.AutoSize = true;

        captureCoordButton.Text = "Yenileme Koordinatı (F8)";
        captureCoordButton.AutoSize = true;
        captureClickButton.Text = "İşlem Koordinatı (F9)";
        captureClickButton.AutoSize = true;
        saveCoordButton.Text = "Koordinatları Kaydet";
        saveCoordButton.AutoSize = true;

        autoCoordinateButton.Text = "⚡ OTOMATİK KOORDİNAT TOPLA";
        autoCoordinateButton.AutoSize = true;
        autoCoordinateButton.Font = new Font(Font.FontFamily, 9, FontStyle.Bold);
        autoCoordinateButton.Click += (_, _) => StartAutoCoordinateCapture();

        captureClick1Button.Text = "İşlem 1 Koordinatı";
        captureClick1Button.AutoSize = true;
        captureClick1Button.Click += (_, _) => CaptureSpecificClickCoordinate(1);

        captureClick2Button.Text = "İşlem 2 Koordinatı";
        captureClick2Button.AutoSize = true;
        captureClick2Button.Click += (_, _) => CaptureSpecificClickCoordinate(2);

        captureClick3Button.Text = "İşlem 3 Koordinatı";
        captureClick3Button.AutoSize = true;
        captureClick3Button.Click += (_, _) => CaptureSpecificClickCoordinate(3);

        saveSessionButton.Text = "PENCERELERİ KAYDET";
        saveSessionButton.AutoSize = true;
        restoreSelectedButton.Text = "SEÇİLİ PENCEREYİ GERİ YÜKLE";
        restoreSelectedButton.AutoSize = true;
        restoreAllButton.Text = "KAYITLI PENCERELERİ GERİ YÜKLE";
        restoreAllButton.AutoSize = true;


        // Bot kontrolleri Pencereler sekmesinde.
        foreach (Control c in new Control[] {
            startButton, stopButton, scanButton, detectButton, errorRefreshButton,
            refreshAllButton, autoCoordinateButton, captureCoordButton, captureClickButton,
            captureClick1Button, captureClick2Button, captureClick3Button,
            saveCoordButton, saveSessionButton, restoreSelectedButton, restoreAllButton
        })
        {
            windowTop.Controls.Add(c);
        }

        hotkeyStatus.Text = "F12: Başlat | F11: Durdur | F8: Yenileme | F9: İşlem";
        hotkeyStatus.AutoSize = true;
        hotkeyStatus.Padding = new Padding(8, 8, 0, 0);
        windowTop.Controls.Add(hotkeyStatus);

        status.AutoSize = true;
        status.Text = "Hazır.";
        status.Padding = new Padding(8, 8, 0, 0);
        windowTop.Controls.Add(status);

        grid.Dock = DockStyle.Fill;
        grid.ReadOnly = true;
        grid.AllowUserToAddRows = false;
        grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        grid.MultiSelect = false;
        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

        // RebuildGrid sütun isimleriyle aynı sırada.
        foreach (var c in new[] {
            "No", "Handle", "Başlık", "URL", "X", "Y", "Genişlik", "Yükseklik",
            "Yenile RX", "Yenile RY", "İşlem 1 RX", "İşlem 1 RY",
            "İşlem 2 RX", "İşlem 2 RY", "İşlem 3 RX", "İşlem 3 RY",
            "Hata Durumu", "Eşleşme"
        }) grid.Columns.Add(c, c);

        windowsPage.Controls.Add(grid);
        windowsPage.Controls.Add(windowTop);

        // ---------------- AYARLAR SEKME ----------------
        var settingsPage = new TabPage("⚙ Ayarlar");
        var settingsPanel = new TableLayoutPanel
        {
            Dock = DockStyle.Top,
            Padding = new Padding(20),
            AutoSize = true,
            ColumnCount = 2,
            RowCount = 8
        };
        settingsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 260));
        settingsPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

        settingsPanel.Controls.Add(new Label { Text = "Hata eşik değeri:", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 0);
        settingsPanel.Controls.Add(thresholdBox, 1, 0);
        reloadWaitBox.Minimum = 1;
        reloadWaitBox.Maximum = 600;
        reloadWaitBox.Value = pageReloadWaitSeconds;
        reloadWaitBox.Width = 90;
        reloadWaitBox.ValueChanged += (_, _) =>
        {
            pageReloadWaitSeconds = (int)reloadWaitBox.Value;
            SaveTimingSettings();
        };

        scanIntervalBox.Minimum = 1;
        scanIntervalBox.Maximum = 600;
        scanIntervalBox.Value = scanIntervalSeconds;
        scanIntervalBox.Width = 90;
        scanIntervalBox.ValueChanged += (_, _) =>
        {
            scanIntervalSeconds = (int)scanIntervalBox.Value;
            SaveTimingSettings();
        };

        actionClickDelayBox.Minimum = 50;
        actionClickDelayBox.Maximum = 10000;
        actionClickDelayBox.Increment = 50;
        actionClickDelayBox.Value = actionClickDelayMs;
        actionClickDelayBox.Width = 90;
        actionClickDelayBox.ValueChanged += (_, _) =>
        {
            actionClickDelayMs = (int)actionClickDelayBox.Value;
            SaveTimingSettings();
        };

        settingsPanel.Controls.Add(new Label { Text = "Sayfa yenileme sonrası bekleme (sn):", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 1);
        settingsPanel.Controls.Add(reloadWaitBox, 1, 1);
        settingsPanel.Controls.Add(new Label { Text = "Tarama döngüleri arasındaki bekleme (sn):", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 2);
        settingsPanel.Controls.Add(scanIntervalBox, 1, 2);
        settingsPanel.Controls.Add(new Label { Text = "İşlem tıklamaları arasındaki bekleme (ms):", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 3);
        settingsPanel.Controls.Add(actionClickDelayBox, 1, 3);

        var urlGroup = new GroupBox { Text = "Toplu URL / Domain Değiştirme", Dock = DockStyle.Top, Padding = new Padding(12), Height = 145 };
        var urlPanel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 3 };
        urlPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160));
        urlPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        oldDomainBox.Dock = DockStyle.Fill; newDomainBox.Dock = DockStyle.Fill;
        urlPanel.Controls.Add(new Label { Text = "Eski domain:", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 0);
        urlPanel.Controls.Add(oldDomainBox, 1, 0);
        urlPanel.Controls.Add(new Label { Text = "Yeni domain:", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 1);
        urlPanel.Controls.Add(newDomainBox, 1, 1);
        replaceDomainButton.Text = "KAYITLI URL'LERİ GÜNCELLE";
        replaceDomainButton.AutoSize = true;
        replaceDomainButton.Click += async (_, _) => await ReplaceSavedUrlDomainAsync();
        urlPanel.Controls.Add(replaceDomainButton, 1, 2);
        urlGroup.Controls.Add(urlPanel);
        settingsPanel.Controls.Add(urlGroup, 0, 4);
        settingsPanel.SetColumnSpan(urlGroup, 2);

        var settingsNote = new Label
        {
            Text = "Not: URL değişikliğinde yalnızca eski domain değiştirilir; URL'nin geri kalan yolu ve parametreleri korunur.",
            AutoSize = true, MaximumSize = new System.Drawing.Size(700, 0), Padding = new Padding(0, 10, 0, 0)
        };
        settingsPanel.Controls.Add(settingsNote, 0, 5);
        settingsPanel.SetColumnSpan(settingsNote, 2);

        settingsPanel.Controls.Add(new Label
        {
            Text = $"Kurulu sürüm: {Application.ProductVersion}",
            AutoSize = true,
            Anchor = AnchorStyles.Left
        }, 0, 6);
        checkUpdateButton.Text = "GÜNCELLEME DENETLE";
        checkUpdateButton.AutoSize = true;
        checkUpdateButton.Click += async (_, _) =>
        {
            checkUpdateButton.Enabled = false;
            try
            {
                await UpdateService.CheckForUpdatesAsync(this, true, message => status.Text = message);
            }
            finally
            {
                checkUpdateButton.Enabled = true;
            }
        };
        settingsPanel.Controls.Add(checkUpdateButton, 1, 6);
        settingsPage.Controls.Add(settingsPanel);

        var tabs = new TabControl { Dock = DockStyle.Fill };
        tabs.TabPages.Add(windowsPage);
        tabs.TabPages.Add(settingsPage);
        Controls.Add(tabs);

        Shown += async (_, _) =>
        {
            LoadTimingSettings();
            LoadTemplate();
            ScanWindows();
            await UpdateService.CheckForUpdatesAsync(this, false, message => status.Text = message);
        };
        FormClosed += (_, _) => closeButtonTemplate?.Dispose();
    }

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);

        keyboardProc = KeyboardHookCallback;
        using var process = System.Diagnostics.Process.GetCurrentProcess();
        using var module = process.MainModule;
        keyboardHook = SetWindowsHookEx(
            WH_KEYBOARD_LL,
            keyboardProc,
            GetModuleHandle(module?.ModuleName),
            0);

        if (keyboardHook == IntPtr.Zero)
        {
            hotkeyStatus.Text = "Klavye kancası kurulamadı — butonları kullanın";
        }

        mouseProc = MouseHookCallback;
        mouseHook = SetWindowsHookEx(WH_MOUSE_LL, mouseProc, GetModuleHandle(null), 0);
    }

    protected override void OnHandleDestroyed(EventArgs e)
    {
        if (keyboardHook != IntPtr.Zero)
        {
            UnhookWindowsHookEx(keyboardHook);
            keyboardHook = IntPtr.Zero;
        }
        if (mouseHook != IntPtr.Zero)
        {
            UnhookWindowsHookEx(mouseHook);
            mouseHook = IntPtr.Zero;
        }

        keyboardProc = null;
        mouseProc = null;
        base.OnHandleDestroyed(e);
    }

    IntPtr KeyboardHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN || wParam == (IntPtr)WM_SYSKEYDOWN))
        {
            int vk = Marshal.ReadInt32(lParam);

            BeginInvoke(new Action(() =>
            {
                switch (vk)
                {
                    case VK_F8:
                        CaptureCurrentMousePosition();
                        break;

                    case VK_F9:
                        CaptureClickCoordinate();
                        break;

                    case VK_F11:
                        StopContinuousScan();
                        break;

                    case VK_F12:
                        StartContinuousScan();
                        break;
                }
            }));
        }

        return CallNextHookEx(keyboardHook, nCode, wParam, lParam);
    }


    IntPtr MouseHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
    {
        // Tek işlem koordinatı değiştirme modu:
        // Butona basıldıktan sonra gelen İLK sol tıklamanın koordinatını al.
        // UI butonunun kendi tıklaması bu moda girmeden önce gerçekleştiği
        // için buton koordinatı kaydedilmez.
        if (nCode >= 0 && pendingSpecificClickNumber >= 1 &&
            (wParam == (IntPtr)WM_LBUTTONDOWN || wParam == (IntPtr)WM_LBUTTONUP))
        {
            if (wParam == (IntPtr)WM_LBUTTONDOWN)
            {
                var data = Marshal.PtrToStructure<MSLLHOOKSTRUCT>(lParam);
                int x = data.pt.X;
                int y = data.pt.Y;

                BeginInvoke(new Action(() =>
                    CapturePendingSpecificCoordinate(x, y)));

                // Hedef uygulamaya gerçek tıklamayı göndermiyoruz.
                // Böylece sadece koordinat değiştirilir, işlem tetiklenmez.
                suppressSpecificMouseDown = true;
                return (IntPtr)1;
            }

            if (wParam == (IntPtr)WM_LBUTTONUP && suppressSpecificMouseDown)
            {
                suppressSpecificMouseDown = false;
                return (IntPtr)1;
            }
        }

        if (nCode >= 0 && autoCoordinateCapture &&
            (wParam == (IntPtr)WM_LBUTTONDOWN || wParam == (IntPtr)WM_LBUTTONUP))
        {
            if (wParam == (IntPtr)WM_LBUTTONDOWN)
            {
                var data = Marshal.PtrToStructure<MSLLHOOKSTRUCT>(lParam);
                int x = data.pt.X;
                int y = data.pt.Y;

                // Koordinatı her tıklamada kaydet. Her satırın ilk fiziksel
                // tıklamasını uygulamaya geçirmiyoruz.
                BeginInvoke(new Action(() => CaptureAutoCoordinate(x, y)));

                if (suppressFirstAutoClick)
                {
                    suppressFirstAutoClick = false;
                    suppressFirstAutoMouseUp = true;
                    return (IntPtr)1;
                }

                // İlk tıklamadan sonraki tıklamalar normal şekilde hedefe ulaşır.
                return CallNextHookEx(mouseHook, nCode, wParam, lParam);
            }

            // İlk tıklamanın mouse-up olayını da yut; sonraki mouse-up olayları serbest.
            if (wParam == (IntPtr)WM_LBUTTONUP && suppressFirstAutoMouseUp)
            {
                suppressFirstAutoMouseUp = false;
                return (IntPtr)1;
            }
        }

        return CallNextHookEx(mouseHook, nCode, wParam, lParam);
    }

    [StructLayout(LayoutKind.Sequential)]
    struct MSLLHOOKSTRUCT
    {
        public POINT pt;
        public uint mouseData;
        public uint flags;
        public uint time;
        public UIntPtr dwExtraInfo;
    }

    string TimingSettingsPath => AppDataPaths.GetDataFilePath("timing_settings.json");

    void LoadTimingSettings()
    {
        try
        {
            if (!File.Exists(TimingSettingsPath)) return;
            var cfg = System.Text.Json.JsonSerializer.Deserialize<TimingSettings>(
                File.ReadAllText(TimingSettingsPath));
            if (cfg == null) return;

            pageReloadWaitSeconds = Math.Clamp(cfg.ReloadWaitSeconds, 1, 600);
            scanIntervalSeconds = Math.Clamp(cfg.ScanIntervalSeconds, 1, 600);
            actionClickDelayMs = Math.Clamp(cfg.ActionClickDelayMs, 50, 10000);
            reloadWaitBox.Value = pageReloadWaitSeconds;
            scanIntervalBox.Value = scanIntervalSeconds;
            actionClickDelayBox.Value = actionClickDelayMs;
        }
        catch { }
    }

    void SaveTimingSettings()
    {
        try
        {
            File.WriteAllText(
                TimingSettingsPath,
                System.Text.Json.JsonSerializer.Serialize(new TimingSettings
                {
                    ReloadWaitSeconds = pageReloadWaitSeconds,
                    ScanIntervalSeconds = scanIntervalSeconds,
                    ActionClickDelayMs = actionClickDelayMs
                }));
        }
        catch { }
    }

    class TimingSettings
    {
        public int ReloadWaitSeconds { get; set; } = 30;
        public int ScanIntervalSeconds { get; set; } = 60;
        public int ActionClickDelayMs { get; set; } = 500;
    }


    void StartAutoCoordinateCapture()
    {
        if (autoCoordinateCapture)
        {
            StopAutoCoordinateCapture("Otomatik koordinat toplama durduruldu.");
            return;
        }
        if (windows.Count == 0)
        {
            ScanWindows();
            if (windows.Count == 0)
            {
                MessageBox.Show("Önce Chrome pencerelerini tarayın.");
                return;
            }
        }

        autoCoordinateRow = 0;
        autoCaptureStep = 0;
        suppressFirstAutoClick = true;
        suppressFirstAutoMouseUp = false;
        autoCoordinateCapture = true;
        autoCoordinateButton.Text = "⏹ OTOMATİK KOORDİNAT TOPLAMAYI DURDUR";
        CaptureAutoCoordinateInstruction();
    }

    void StopAutoCoordinateCapture(string message)
    {
        autoCoordinateCapture = false;
        suppressFirstAutoClick = false;
        suppressFirstAutoMouseUp = false;
        autoCoordinateButton.Text = "⚡ OTOMATİK KOORDİNAT TOPLA";
        status.Text = message;
    }

    void CaptureAutoCoordinateInstruction()
    {
        if (!autoCoordinateCapture) return;
        if (autoCoordinateRow >= windows.Count)
        {
            StopAutoCoordinateCapture("Tüm pencerelerin 4 koordinatı tamamlandı.");
            SaveCoordinates();
            return;
        }

        if (grid.Rows.Count == windows.Count)
        {
            grid.ClearSelection();
            grid.Rows[autoCoordinateRow].Selected = true;
            grid.CurrentCell = grid.Rows[autoCoordinateRow].Cells[0];
        }

        string step = autoCaptureStep switch
        {
            0 => "YENİLEME",
            1 => "İŞLEM 1",
            2 => "İŞLEM 2",
            _ => "İŞLEM 3"
        };
        status.Text = $"Pencere {autoCoordinateRow + 1}/{windows.Count} — Mouse'u {step} noktasına götürün ve SOL TIKLAYIN.";
    }

    void CaptureAutoCoordinate(int screenX, int screenY)
    {
        if (!autoCoordinateCapture || autoCoordinateRow >= windows.Count) return;
        var w = windows[autoCoordinateRow];
        var (rx, ry) = ToRelative(w, screenX, screenY);
        int row = autoCoordinateRow;

        switch (autoCaptureStep)
        {
            case 0:
                w.RefreshRX = rx; w.RefreshRY = ry;
                w.RefreshOffsetX = screenX - w.X;
                w.RefreshOffsetY = screenY - w.Y;
                grid.Rows[row].Cells["Yenile RX"].Value = FormatRel(rx);
                grid.Rows[row].Cells["Yenile RY"].Value = FormatRel(ry);
                break;
            case 1:
                w.Click1RX = rx; w.Click1RY = ry;
                grid.Rows[row].Cells["İşlem 1 RX"].Value = FormatRel(rx);
                grid.Rows[row].Cells["İşlem 1 RY"].Value = FormatRel(ry);
                break;
            case 2:
                w.Click2RX = rx; w.Click2RY = ry;
                grid.Rows[row].Cells["İşlem 2 RX"].Value = FormatRel(rx);
                grid.Rows[row].Cells["İşlem 2 RY"].Value = FormatRel(ry);
                break;
            case 3:
                w.Click3RX = rx; w.Click3RY = ry;
                grid.Rows[row].Cells["İşlem 3 RX"].Value = FormatRel(rx);
                grid.Rows[row].Cells["İşlem 3 RY"].Value = FormatRel(ry);
                break;
        }

        autoCaptureStep++;
        if (autoCaptureStep >= 4)
        {
            autoCaptureStep = 0;
            autoCoordinateRow++;

            // Her yeni pencereye/satıra geçildiğinde ilk tıklamayı
            // yeniden engelle. Böylece her satırın YENİLEME
            // koordinatı alınırken gerçek uygulamaya tıklanmaz.
            suppressFirstAutoClick = true;
            suppressFirstAutoMouseUp = false;
        }

        CaptureAutoCoordinateInstruction();
    }


    void LoadTemplate()
    {
        closeButtonTemplate?.Dispose();
        closeButtonTemplate = LoadEmbeddedTemplate(EmbeddedTemplates.CloseButton);
        if (closeButtonTemplate == null)
            MessageBox.Show("Gömülü KAPAT butonu şablonu yüklenemedi.");
    }

    static Mat? LoadEmbeddedTemplate(byte[] data)
    {
        try
        {
            using var ms = new MemoryStream(data);
            using var bmp = new Bitmap(ms);
            return BitmapConverter.ToMat(bmp);
        }
        catch { return null; }
    }

    void ScanWindows()
    {
        windows.Clear();
        EnumWindows((hWnd, _) =>
        {
            if (!IsWindowVisible(hWnd)) return true;
            var sb = new StringBuilder(512); GetWindowText(hWnd, sb, sb.Capacity); var title = sb.ToString().Trim(); if (title.Length == 0) return true;
            try
            {
                GetWindowThreadProcessId(hWnd, out uint pid); using var process = Process.GetProcessById((int)pid);
                if (!process.ProcessName.Equals("chrome", StringComparison.OrdinalIgnoreCase)) return true;
                if (!GetWindowRect(hWnd, out var r)) return true;
                int width = r.Right - r.Left, height = r.Bottom - r.Top; if (width < 300 || height < 200) return true;
                windows.Add(new ChromeWindow { Handle = hWnd, Title = title, X = r.Left, Y = r.Top, Width = width, Height = height });
            }
            catch { }
            return true;
        }, IntPtr.Zero);
        windows.Sort((a, b) => { int ra = a.Y / 150, rb = b.Y / 150; return ra != rb ? ra.CompareTo(rb) : a.X.CompareTo(b.X); });
        LoadCoordinates(); RebuildGrid("Taranmadı", "-"); status.Text = $"{windows.Count} adet Chrome penceresi bulundu.";
    }

    void RebuildGrid(string state, string score)
    {
        grid.Rows.Clear();
        for (int i = 0; i < windows.Count; i++)
        {
            var w = windows[i];
            grid.Rows.Add(i + 1, $"0x{w.Handle.ToInt64():X}", w.Title, w.Url ?? "", w.X, w.Y, w.Width, w.Height,
                FormatRel(w.RefreshRX), FormatRel(w.RefreshRY),
                FormatRel(w.Click1RX), FormatRel(w.Click1RY),
                FormatRel(w.Click2RX), FormatRel(w.Click2RY),
                FormatRel(w.Click3RX), FormatRel(w.Click3RY), state, score);
        }
    }

    static string FormatRel(double? value) => value.HasValue ? value.Value.ToString("P1") : "-";

    void CaptureCurrentMousePosition()
    {
        if (!TryGetSelectedWindow(out var w, out int index)) return;
        if (!GetCursorPos(out POINT p)) return;
        var (rx, ry) = ToRelative(w, p.X, p.Y);
        w.RefreshRX = rx; w.RefreshRY = ry;
        w.RefreshOffsetX = p.X - w.X;
        w.RefreshOffsetY = p.Y - w.Y;
        grid.Rows[index].Cells["Yenile RX"].Value = FormatRel(rx);
        grid.Rows[index].Cells["Yenile RY"].Value = FormatRel(ry);
        status.Text = $"{index + 1}. pencerenin yenileme konumu kaydedildi.";
    }

    void CaptureSpecificClickCoordinate(int clickNumber)
    {
        if (clickNumber < 1 || clickNumber > 3) return;
        if (!TryGetSelectedWindow(out var w, out int index)) return;

        // Butona basıldığı anda mouse'un konumunu alma.
        // Kullanıcıyı hedef noktaya götür ve bir sonraki sol tıklamayı bekle.
        pendingSpecificClickNumber = clickNumber;
        suppressSpecificMouseDown = false;

        status.Text =
            $"{index + 1}. pencere — İşlem {clickNumber} koordinatı bekleniyor. " +
            "Mouse'u hedef noktaya götürüp SOL TIKLAYIN.";
    }

    void CapturePendingSpecificCoordinate(int screenX, int screenY)
    {
        int clickNumber = pendingSpecificClickNumber;
        if (clickNumber < 1 || clickNumber > 3) return;
        if (!TryGetSelectedWindow(out var w, out int index))
        {
            pendingSpecificClickNumber = 0;
            return;
        }

        var (rx, ry) = ToRelative(w, screenX, screenY);

        switch (clickNumber)
        {
            case 1:
                w.Click1RX = rx;
                w.Click1RY = ry;
                grid.Rows[index].Cells["İşlem 1 RX"].Value = FormatRel(rx);
                grid.Rows[index].Cells["İşlem 1 RY"].Value = FormatRel(ry);
                break;

            case 2:
                w.Click2RX = rx;
                w.Click2RY = ry;
                grid.Rows[index].Cells["İşlem 2 RX"].Value = FormatRel(rx);
                grid.Rows[index].Cells["İşlem 2 RY"].Value = FormatRel(ry);
                break;

            case 3:
                w.Click3RX = rx;
                w.Click3RY = ry;
                grid.Rows[index].Cells["İşlem 3 RX"].Value = FormatRel(rx);
                grid.Rows[index].Cells["İşlem 3 RY"].Value = FormatRel(ry);
                break;
        }

        SaveCoordinates();
        pendingSpecificClickNumber = 0;
        suppressSpecificMouseDown = false;

        status.Text =
            $"{index + 1}. pencere — sadece İşlem {clickNumber} koordinatı güncellendi.";
    }


    void CaptureClickCoordinate()
    {
        if (!TryGetSelectedWindow(out var w, out int index)) return;
        if (!GetCursorPos(out POINT p)) return;
        var (rx, ry) = ToRelative(w, p.X, p.Y);
        switch (selectedClickNumber)
        {
            case 1:
                w.Click1RX=rx; w.Click1RY=ry;
                grid.Rows[index].Cells["İşlem 1 RX"].Value=FormatRel(rx); grid.Rows[index].Cells["İşlem 1 RY"].Value=FormatRel(ry); break;
            case 2:
                w.Click2RX=rx; w.Click2RY=ry;
                grid.Rows[index].Cells["İşlem 2 RX"].Value=FormatRel(rx); grid.Rows[index].Cells["İşlem 2 RY"].Value=FormatRel(ry); break;
            default:
                w.Click3RX=rx; w.Click3RY=ry;
                grid.Rows[index].Cells["İşlem 3 RX"].Value=FormatRel(rx); grid.Rows[index].Cells["İşlem 3 RY"].Value=FormatRel(ry); break;
        }
        status.Text=$"{index+1}. pencere — İşlem {selectedClickNumber} konumu kaydedildi.";
        selectedClickNumber=selectedClickNumber==3?1:selectedClickNumber+1;
    }

    bool TryGetSelectedWindow(out ChromeWindow w, out int index)
    {
        index=grid.CurrentRow?.Index ?? -1;
        if(index<0 || index>=windows.Count){w=null!; MessageBox.Show("Önce tabloda bir pencere seçin."); return false;}
        w=windows[index]; return true;
    }

    static (double RX,double RY) ToRelative(ChromeWindow w,int x,int y)
    {
        return (Math.Clamp((x-w.X)/(double)Math.Max(1,w.Width),0,1), Math.Clamp((y-w.Y)/(double)Math.Max(1,w.Height),0,1));
    }

    static System.Drawing.Point ToScreenPoint(ChromeWindow w,double rx,double ry)
    {
        return new System.Drawing.Point(w.X+(int)Math.Round(Math.Clamp(rx,0,1)*w.Width), w.Y+(int)Math.Round(Math.Clamp(ry,0,1)*w.Height));
    }


    async Task<bool> ActivateChromeWindowAsync(IntPtr hWnd)
    {
        if (hWnd == IntPtr.Zero || !IsWindow(hWnd)) return false;

        ShowWindow(hWnd, SW_RESTORE);

        uint currentThread = GetCurrentThreadId();
        uint targetThread = GetWindowThreadProcessId(hWnd, out _);
        bool attached = false;

        try
        {
            if (targetThread != 0 && targetThread != currentThread)
            {
                attached = AttachThreadInput(currentThread, targetThread, true);
            }

            BringWindowToTop(hWnd);
            SetForegroundWindow(hWnd);
            await Task.Delay(350);

            return GetForegroundWindow() == hWnd;
        }
        finally
        {
            if (attached)
                AttachThreadInput(currentThread, targetThread, false);
        }
    }

    bool IsValidHttpUrl(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        string s = value.Trim();

        return Uri.TryCreate(s, UriKind.Absolute, out var uri) &&
               (uri.Scheme == Uri.UriSchemeHttp ||
                uri.Scheme == Uri.UriSchemeHttps);
    }

    async Task<string> GetChromeUrlAsync(ChromeWindow w)
    {
        IntPtr previous = GetForegroundWindow();
        try
        {
            ShowWindow(w.Handle, SW_RESTORE);
            SetForegroundWindow(w.Handle);
            await Task.Delay(250);

            // v4.11.1'de çalışan URL okuma yöntemi birebir korunuyor.
            SendKeys.SendWait("^l");
            await Task.Delay(80);
            SendKeys.SendWait("^c");
            await Task.Delay(120);

            string url = Clipboard.ContainsText() ? Clipboard.GetText().Trim() : "";
            SendKeys.SendWait("{ESC}");
            return url;
        }
        catch
        {
            return "";
        }
        finally
        {
            if (previous != IntPtr.Zero && IsWindow(previous))
                SetForegroundWindow(previous);
        }
    }


    async Task ReplaceSavedUrlDomainAsync()
    {
        try
        {
            string oldDomain = oldDomainBox.Text.Trim();
            string newDomain = newDomainBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(oldDomain) || string.IsNullOrWhiteSpace(newDomain))
            {
                MessageBox.Show("Eski ve yeni domain alanlarını doldurun.");
                return;
            }

            if (string.Equals(oldDomain, newDomain, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Eski ve yeni domain aynı.");
                return;
            }

            ScanWindows();
            if (windows.Count == 0)
            {
                MessageBox.Show("Açık Chrome penceresi bulunamadı.");
                return;
            }

            replaceDomainButton.Enabled = false;
            int changedLive = 0;
            int changedSaved = 0;
            var failed = new List<int>();

            // Her pencereyi TEK TEK öne getiriyoruz. Önceki sürümde SendKeys
            // bazen Bot penceresine gidebildiği için URL yalnızca bir pencerede değişebiliyordu.
            for (int i = 0; i < windows.Count; i++)
            {
                var w = windows[i];
                status.Text = $"Pencere {i + 1}/{windows.Count} hazırlanıyor...";
                Application.DoEvents();

                try
                {
                    if (!await ActivateChromeWindowAsync(w.Handle))
                    {
                        failed.Add(i + 1);
                        continue;
                    }

                    // Adres çubuğundaki mevcut URL'yi al.
                    SendKeys.SendWait("^l");
                    await Task.Delay(150);
                    SendKeys.SendWait("^c");
                    await Task.Delay(200);
                    string currentUrl = Clipboard.ContainsText() ? Clipboard.GetText().Trim() : "";
                    SendKeys.SendWait("{ESC}");

                    if (string.IsNullOrWhiteSpace(currentUrl) ||
                        !currentUrl.Contains(oldDomain, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    string newUrl = currentUrl.Replace(oldDomain, newDomain, StringComparison.OrdinalIgnoreCase);

                    status.Text = $"Pencere {i + 1}/{windows.Count} → URL değiştiriliyor...";
                    Application.DoEvents();

                    if (!await ActivateChromeWindowAsync(w.Handle))
                    {
                        failed.Add(i + 1);
                        continue;
                    }

                    Clipboard.SetText(newUrl);
                    SendKeys.SendWait("^l");
                    await Task.Delay(120);
                    SendKeys.SendWait("^v");
                    await Task.Delay(120);
                    SendKeys.SendWait("{ENTER}");
                    await Task.Delay(800);

                    // Pencerenin gerçekten hâlâ aktif olduğundan emin ol.
                    if (GetForegroundWindow() != w.Handle)
                    {
                        failed.Add(i + 1);
                        continue;
                    }

                    w.Url = newUrl;
                    if (i < grid.Rows.Count)
                        grid.Rows[i].Cells["URL"].Value = newUrl;

                    changedLive++;
                }
                catch
                {
                    failed.Add(i + 1);
                }
            }

            // Kayıtlı oturumdaki URL'leri de güncelle.
            var records = LoadSessionRecords();
            foreach (var record in records)
            {
                if (!string.IsNullOrWhiteSpace(record.Url) &&
                    record.Url.Contains(oldDomain, StringComparison.OrdinalIgnoreCase))
                {
                    record.Url = record.Url.Replace(oldDomain, newDomain, StringComparison.OrdinalIgnoreCase);
                    changedSaved++;
                }
            }

            if (records.Count > 0)
            {
                File.WriteAllText(
                    sessionFile,
                    JsonSerializer.Serialize(records, new JsonSerializerOptions { WriteIndented = true }));
            }

            status.Text = $"URL değişikliği tamamlandı. Açık: {changedLive}, kayıtlı: {changedSaved}.";

            string message = $"URL değişikliği tamamlandı.\n\nAçık Chrome pencereleri: {changedLive}\nKayıtlı oturum: {changedSaved}";
            if (failed.Count > 0)
                message += $"\n\nİşlem sırasında erişilemeyen pencereler: {string.Join(", ", failed)}";

            MessageBox.Show(message);
        }
        catch (Exception ex)
        {
            MessageBox.Show("URL'ler güncellenemedi:\n" + ex.Message);
        }
        finally
        {
            replaceDomainButton.Enabled = true;
        }
    }


    async Task SaveSessionAsync()
    {
        try
        {
            if (windows.Count == 0)
            {
                MessageBox.Show("Önce Chrome Pencerelerini Tara ile pencereleri listeleyin.");
                return;
            }

            saveSessionButton.Enabled = false;
            var records = new List<SessionRecord>();

            for (int i = 0; i < windows.Count; i++)
            {
                var w = windows[i];
                status.Text = $"Pencere {i + 1}/{windows.Count} URL kaydediliyor...";
                string url = (await GetChromeUrlAsync(w)).Trim();
                w.Url = IsValidHttpUrl(url) ? url : "";
                records.Add(new SessionRecord
                {
                    WindowNo = i + 1,
                    Url = w.Url,
                    Title = w.Title,
                    X = w.X, Y = w.Y, Width = w.Width, Height = w.Height,
                    RefreshRX = w.RefreshRX, RefreshRY = w.RefreshRY,
                    RefreshOffsetX = w.RefreshOffsetX, RefreshOffsetY = w.RefreshOffsetY,
                    Click1RX = w.Click1RX, Click1RY = w.Click1RY,
                    Click2RX = w.Click2RX, Click2RY = w.Click2RY,
                    Click3RX = w.Click3RX, Click3RY = w.Click3RY
                });

                grid.Rows[i].Cells["URL"].Value = w.Url;
            }

            File.WriteAllText(sessionFile,
                JsonSerializer.Serialize(records, new JsonSerializerOptions { WriteIndented = true }));

            // Koordinatları da ayrıca güncel tut.
            SaveCoordinates();
            status.Text = $"{records.Count} Chrome penceresi oturum olarak kaydedildi.";
        }
        catch (Exception ex)
        {
            MessageBox.Show("Oturum kaydedilemedi:\n" + ex.Message);
        }
        finally
        {
            saveSessionButton.Enabled = true;
        }
    }

    List<SessionRecord> LoadSessionRecords()
    {
        if (!File.Exists(sessionFile))
            return new List<SessionRecord>();

        try
        {
            return JsonSerializer.Deserialize<List<SessionRecord>>(
                File.ReadAllText(sessionFile)) ?? new List<SessionRecord>();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Oturum dosyası okunamadı:\n" + ex.Message);
            return new List<SessionRecord>();
        }
    }

    async Task RestoreSelectedSessionAsync()
    {
        var records = LoadSessionRecords();
        int index = grid.CurrentRow?.Index ?? -1;
        if (index < 0 || index >= records.Count)
        {
            MessageBox.Show("Önce geri yüklemek istediğiniz kayıtlı pencereyi tabloda seçin.");
            return;
        }

        var record = records[index];
        if (string.IsNullOrWhiteSpace(record.Url))
        {
            MessageBox.Show("Bu pencerenin kayıtlı URL'si boş. Pencereyi tekrar açıp Pencereleri Kaydet yapın.");
            return;
        }

        await RestoreSessionRecordAsync(record);
        ScanWindows();
    }

    async Task RestoreMissingSessionsAsync()
    {
        var records = LoadSessionRecords();
        if (records.Count == 0)
        {
            MessageBox.Show("Kayıtlı oturum bulunamadı. Önce PENCERELERİ KAYDET butonuna basın.");
            return;
        }

        // Kullanıcının açık pencerelerini otomatik olarak değiştirmiyoruz.
        // Sadece kayıtlı URL'lerden açıkta olmayanları açıyoruz. Aynı URL birden fazla kez kayıtlıysa adet bazında eşleştiriyoruz.
        ScanWindows();
        var openUrls = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        foreach (var w in windows)
        {
            if (string.IsNullOrWhiteSpace(w.Url))
                w.Url = await GetChromeUrlAsync(w);

            if (!string.IsNullOrWhiteSpace(w.Url))
            {
                openUrls.TryGetValue(w.Url, out int count);
                openUrls[w.Url] = count + 1;
            }
        }

        int restored = 0;
        foreach (var record in records.OrderBy(r => r.WindowNo))
        {
            if (string.IsNullOrWhiteSpace(record.Url)) continue;

            openUrls.TryGetValue(record.Url, out int count);
            if (count > 0)
            {
                openUrls[record.Url] = count - 1;
                continue;
            }

            await RestoreSessionRecordAsync(record);
            restored++;
        }

        ScanWindows();
        status.Text = restored == 0
            ? "Eksik kayıtlı pencere bulunmadı. Açık Chrome pencerelerine dokunulmadı."
            : $"{restored} eksik kayıtlı pencere geri yüklendi.";
    }

    async Task RestoreSessionRecordAsync(SessionRecord record)
    {
        string? chromeExe = FindChromeExe();
        if (chromeExe == null)
        {
            MessageBox.Show("Chrome.exe bulunamadı.");
            return;
        }

        var before = GetChromeWindowHandles();
        var psi = new ProcessStartInfo
        {
            FileName = chromeExe,
            Arguments = $"--new-window \"{record.Url.Replace("\\\"", "\\\\\"")}\"",
            UseShellExecute = true
        };

        Process.Start(psi);
        status.Text = $"Pencere {record.WindowNo} açılıyor...";

        IntPtr newHandle = IntPtr.Zero;
        for (int attempt = 0; attempt < 60; attempt++)
        {
            await Task.Delay(250);
            foreach (var h in GetChromeWindowHandles())
            {
                if (!before.Contains(h))
                {
                    newHandle = h;
                    break;
                }
            }
            if (newHandle != IntPtr.Zero) break;
        }

        if (newHandle == IntPtr.Zero)
        {
            MessageBox.Show($"Pencere {record.WindowNo} açılamadı veya yeni Chrome penceresi bulunamadı.");
            return;
        }

        ShowWindow(newHandle, SW_RESTORE);
        MoveWindow(newHandle, record.X, record.Y, record.Width, record.Height, true);
        SetForegroundWindow(newHandle);
        await Task.Delay(500);
    }

    static HashSet<IntPtr> GetChromeWindowHandles()
    {
        var result = new HashSet<IntPtr>();
        EnumWindows((hWnd, _) =>
        {
            if (!IsWindowVisible(hWnd)) return true;
            try
            {
                GetWindowThreadProcessId(hWnd, out uint pid);
                using var process = Process.GetProcessById((int)pid);
                if (!process.ProcessName.Equals("chrome", StringComparison.OrdinalIgnoreCase)) return true;
                if (GetWindowRect(hWnd, out var r) && (r.Right - r.Left) >= 300 && (r.Bottom - r.Top) >= 200)
                    result.Add(hWnd);
            }
            catch { }
            return true;
        }, IntPtr.Zero);
        return result;
    }

    static string? FindChromeExe()
    {
        var candidates = new[]
        {
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Google", "Chrome", "Application", "chrome.exe"),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Google", "Chrome", "Application", "chrome.exe"),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Google", "Chrome", "Application", "chrome.exe")
        };
        return candidates.FirstOrDefault(File.Exists);
    }

    void SaveCoordinates()
    {
        try
        {
            var data=windows.Select((w,i)=>new RefreshCoordinate
            {
                WindowNo=i+1, WindowWidth=w.Width, WindowHeight=w.Height,
                RefreshRX=w.RefreshRX, RefreshRY=w.RefreshRY,
                RefreshOffsetX=w.RefreshOffsetX, RefreshOffsetY=w.RefreshOffsetY,
                Click1RX=w.Click1RX, Click1RY=w.Click1RY,
                Click2RX=w.Click2RX, Click2RY=w.Click2RY,
                Click3RX=w.Click3RX, Click3RY=w.Click3RY
            }).ToList();
            File.WriteAllText(coordFile,JsonSerializer.Serialize(data,new JsonSerializerOptions{WriteIndented=true}));
            status.Text="Taşınabilir koordinatlar kaydedildi.";
        }
        catch(Exception ex){MessageBox.Show("Koordinatlar kaydedilemedi:\n"+ex.Message);}
    }

    void LoadCoordinates()
    {
        try
        {
            if(!File.Exists(coordFile)) return;
            var data=JsonSerializer.Deserialize<List<RefreshCoordinate>>(File.ReadAllText(coordFile))??new();
            bool migrated=false;
            foreach(var item in data)
            {
                int i=item.WindowNo-1; if(i<0||i>=windows.Count) continue; var w=windows[i];
                if(item.RefreshRX.HasValue)
                {
                    w.RefreshRX=item.RefreshRX; w.RefreshRY=item.RefreshRY;
                    w.RefreshOffsetX=item.RefreshOffsetX; w.RefreshOffsetY=item.RefreshOffsetY;
                    w.Click1RX=item.Click1RX; w.Click1RY=item.Click1RY; w.Click2RX=item.Click2RX; w.Click2RY=item.Click2RY; w.Click3RX=item.Click3RX; w.Click3RY=item.Click3RY;
                }
                else if(item.X.HasValue&&item.Y.HasValue)
                {
                    (w.RefreshRX,w.RefreshRY)=ToRelative(w,item.X.Value,item.Y.Value);
                    if(item.Click1X.HasValue&&item.Click1Y.HasValue)(w.Click1RX,w.Click1RY)=ToRelative(w,item.Click1X.Value,item.Click1Y.Value);
                    if(item.Click2X.HasValue&&item.Click2Y.HasValue)(w.Click2RX,w.Click2RY)=ToRelative(w,item.Click2X.Value,item.Click2Y.Value);
                    if(item.Click3X.HasValue&&item.Click3Y.HasValue)(w.Click3RX,w.Click3RY)=ToRelative(w,item.Click3X.Value,item.Click3Y.Value);
                    migrated=true;
                }
            }
            if(migrated) SaveCoordinates();
        }
        catch(Exception ex){status.Text="Koordinat dosyası okunamadı: "+ex.Message;}
    }

    void DetectErrors()
    {
        if (closeButtonTemplate == null) { LoadTemplate(); if (closeButtonTemplate == null) return; } if (windows.Count == 0) { ScanWindows(); if (windows.Count == 0) return; }
        double threshold = (double)thresholdBox.Value; int errors = 0;
        for (int i = 0; i < windows.Count; i++) { var r = FindError(windows[i], threshold); if (r.Found) errors++; UpdateRow(i, r.Found ? "HATA BULUNDU" : "Normal", r.Score, r.Found ? Color.MistyRose : Color.Honeydew); }
        status.Text = $"Tarama tamamlandı. {errors} pencerede hata bulundu.";
    }

    (bool Found, double Score) FindError(ChromeWindow w, double threshold)
    {
        double bestScore = 0;
        try
        {
            if (closeButtonTemplate == null || closeButtonTemplate.Empty())
                return (false, 0);

            using var bmp = CaptureScreenArea(w.X, w.Y, w.Width, w.Height);
            using var screenColor = BitmapConverter.ToMat(bmp);
            using var screen = new Mat();
            Cv2.CvtColor(screenColor, screen, ColorConversionCodes.BGR2GRAY);

            using var templateGray = new Mat();
            if (closeButtonTemplate.Channels() == 1)
                closeButtonTemplate.CopyTo(templateGray);
            else
                Cv2.CvtColor(closeButtonTemplate, templateGray, ColorConversionCodes.BGR2GRAY);

            // KAPAT butonu ortak hata göstergesi. Pencere/DPI değişikliklerine
            // karşı birden fazla ölçekte ara.
            double baseScale = Math.Clamp(w.Width / 516.0, 0.60, 1.50);
            foreach (double factor in new[] { 0.75, 0.85, 0.95, 1.00, 1.05, 1.15, 1.25 })
            {
                double scale = baseScale * factor;
                int tw = Math.Max(20, (int)Math.Round(templateGray.Width * scale));
                int th = Math.Max(10, (int)Math.Round(templateGray.Height * scale));
                if (tw >= screen.Width || th >= screen.Height) continue;

                using var scaled = new Mat();
                Cv2.Resize(templateGray, scaled, new OpenCvSharp.Size(tw, th), 0, 0, InterpolationFlags.Linear);
                using var result = new Mat();
                Cv2.MatchTemplate(screen, scaled, result, TemplateMatchModes.CCoeffNormed);
                Cv2.MinMaxLoc(result, out _, out double maxValue, out _, out _);
                if (maxValue > bestScore) bestScore = maxValue;
            }
        }
        catch { bestScore = 0; }

        return (bestScore >= threshold, bestScore);
    }

    void UpdateRow(int index, string state, double score, Color color)
    {
        if (index < 0 || index >= windows.Count) return; if (grid.Rows.Count != windows.Count) RebuildGrid("Taranmadı", "-");
        grid.Rows[index].Cells["Hata Durumu"].Value = state; grid.Rows[index].Cells["Eşleşme"].Value = score == 0 ? "-" : $"{score:P1}"; grid.Rows[index].DefaultCellStyle.BackColor = color; grid.Refresh();
    }

    async Task ClickRefreshAsync(ChromeWindow w, CancellationToken token)
    {
        if (!w.RefreshRX.HasValue || !w.RefreshRY.HasValue)
            throw new InvalidOperationException("Yenileme koordinatı eksik.");

        if (!IsWindow(w.Handle))
            throw new InvalidOperationException("Chrome penceresi artık açık değil.");

        ShowWindow(w.Handle, SW_RESTORE);

        // Pencere taşınmış veya boyutu değişmiş olabilir.
        // Tıklamadan hemen önce gerçek güncel pencere geometrisini alıyoruz.
        if (!GetWindowRect(w.Handle, out var rect))
            throw new InvalidOperationException("Chrome penceresinin konumu/boyutu okunamadı.");

        w.X = rect.Left;
        w.Y = rect.Top;
        w.Width = rect.Right - rect.Left;
        w.Height = rect.Bottom - rect.Top;

        if (!await ActivateChromeWindowAsync(w.Handle))
            throw new InvalidOperationException("Chrome penceresi öne getirilemedi.");

        await Task.Delay(150, token);

        int targetX;
        int targetY;

        if (w.RefreshOffsetX.HasValue && w.RefreshOffsetY.HasValue)
        {
            // Yenileme butonu Chrome'un tarayıcı arayüzünde olduğu için
            // pencerenin sol-üstüne göre sabit piksel offset kullanıyoruz.
            targetX = w.X + w.RefreshOffsetX.Value;
            targetY = w.Y + w.RefreshOffsetY.Value;
        }
        else
        {
            // Eski refresh_coordinates.json dosyaları için geriye dönük uyumluluk.
            targetX = w.X + (int)Math.Round(
                Math.Clamp(w.RefreshRX.Value, 0, 1) * w.Width);
            targetY = w.Y + (int)Math.Round(
                Math.Clamp(w.RefreshRY.Value, 0, 1) * w.Height);
        }

        // Güvenlik: pencerenin dışına çıkacak bir noktaya kesinlikle tıklama.
        const int margin = 2;
        if (targetX < w.X + margin || targetX > w.X + w.Width - margin ||
            targetY < w.Y + margin || targetY > w.Y + w.Height - margin)
        {
            throw new InvalidOperationException(
                $"Yenileme koordinatı pencere dışında hesaplandı: ({targetX},{targetY}).");
        }

        SetCursorPos(targetX, targetY);
        await Task.Delay(100, token);

        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, UIntPtr.Zero);
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);
    }


    bool CoordinatesReady() => windows.Count > 0 && windows.All(w => w.RefreshRX.HasValue && w.RefreshRY.HasValue);

    void StartContinuousScan()
    {
        if (scanCts != null) { status.Text = "Zaten çalışıyor. F11 ile durdurun."; return; }
        ScanWindows(); if (windows.Count == 0) { MessageBox.Show("Chrome penceresi bulunamadı."); return; }
        if (!CoordinatesReady()) { var missing = windows.Select((w, i) => new { w, i }).Where(x => !x.w.RefreshRX.HasValue || !x.w.RefreshRY.HasValue).Select(x => x.i + 1); MessageBox.Show("Yenileme koordinatı eksik olan pencereler: " + string.Join(", ", missing)); return; }
        var missingClicks = windows.Select((w, i) => new { w, i }).Where(x => !x.w.Click1RX.HasValue || !x.w.Click1RY.HasValue || !x.w.Click2RX.HasValue || !x.w.Click2RY.HasValue || !x.w.Click3RX.HasValue || !x.w.Click3RY.HasValue).Select(x => x.i + 1).ToList();
        if (missingClicks.Count > 0) { MessageBox.Show("3 işlem koordinatı eksik olan pencereler: " + string.Join(", ", missingClicks)); return; }
        scanCts = new CancellationTokenSource(); hotkeyStatus.Text = "ÇALIŞIYOR — F11: Durdur"; _ = ContinuousScanLoopAsync(scanCts.Token);
    }

    void StopContinuousScan()
    {
        if (scanCts == null) { status.Text = "Çalışan tarama yok."; return; } scanCts.Cancel(); status.Text = "Durdurma istendi...";
    }

    async Task PerformThreeClicksAsync(ChromeWindow w,CancellationToken token)
    {
        var points=new (double? X,double? Y)[]{(w.Click1RX,w.Click1RY),(w.Click2RX,w.Click2RY),(w.Click3RX,w.Click3RY)};
        for(int i=0;i<points.Length;i++)
        {
            token.ThrowIfCancellationRequested();
            if(!points[i].X.HasValue||!points[i].Y.HasValue) throw new InvalidOperationException($"İşlem {i+1} koordinatı eksik.");
            double x = points[i].X!.Value;
            double y = points[i].Y!.Value;
            var p=ToScreenPoint(w, x, y); SetCursorPos(p.X,p.Y); await Task.Delay(100,token);
            mouse_event(MOUSEEVENTF_LEFTDOWN,0,0,0,UIntPtr.Zero); mouse_event(MOUSEEVENTF_LEFTUP,0,0,0,UIntPtr.Zero); await Task.Delay(actionClickDelayMs, token);
        }
    }

    async Task ContinuousScanLoopAsync(CancellationToken token)
    {
        try
        {
            while (true)
            {
                token.ThrowIfCancellationRequested();

                // 1) Önce tüm 11 pencereyi tara ve sadece hatalıları belirle.
                status.Text = "Hata taraması yapılıyor...";
                DetectErrors();

                var errors = new List<int>();
                for (int i = 0; i < windows.Count; i++)
                {
                    if (grid.Rows[i].Cells["Hata Durumu"].Value?.ToString() == "HATA BULUNDU")
                        errors.Add(i);
                }

                // 2) Sadece hatalı pencereleri yenile.
                if (errors.Count > 0)
                {
                    status.Text = $"{errors.Count} hatalı pencere bulundu. Yenileniyor...";

                    foreach (int i in errors)
                    {
                        token.ThrowIfCancellationRequested();
                        await ClickRefreshAsync(windows[i], token);
                        UpdateRow(i, "YENİLENİYOR...", 0, Color.Khaki);
                        await Task.Delay(300, token);
                    }

                    // Yenilenen sayfaların yüklenmesi için 30 sn.
                    status.Text = $"{errors.Count} hata ekranı yenilendi. {pageReloadWaitSeconds} saniye bekleniyor...";
                    await Task.Delay(pageReloadWaitSeconds * 1000, token);

                    // 3) Sadece az önce yenilenen ekranlarda 3 adımlı işlem.
                    foreach (int i in errors)
                    {
                        token.ThrowIfCancellationRequested();
                        var w = windows[i];

                        ShowWindow(w.Handle, SW_RESTORE);
                        SetForegroundWindow(w.Handle);
                        await Task.Delay(300, token);

                        status.Text = $"Pencere {i + 1}: 3 işlem tıklaması yapılıyor...";
                        await PerformThreeClicksAsync(w, token);
                        UpdateRow(i, "İŞLEMLER YAPILDI", 0, Color.Honeydew);
                    }
                }
                else
                {
                    status.Text = $"Hata bulunmadı. {scanIntervalSeconds} saniye bekleniyor...";
                }

                // 4) Bir sonraki taramadan önce sistemin toparlanması için 60 sn.
                await Task.Delay(scanIntervalSeconds * 1000, token);
            }
        }
        catch (OperationCanceledException)
        {
            status.Text = "Tarama F11 ile durduruldu.";
        }
        catch (Exception ex)
        {
            status.Text = "Tarama hatası: " + ex.Message;
            MessageBox.Show("Tarama sırasında hata oluştu:\n\n" + ex.Message);
        }
        finally
        {
            scanCts?.Dispose();
            scanCts = null;
            hotkeyStatus.Text = "F12: Başlat | F11: Durdur | F8: Yenileme | F9: İşlem";
        }
    }

    async Task RefreshAllPagesAsync()
    {
        try
        {
            ScanWindows(); if (!CoordinatesReady()) { MessageBox.Show("Önce tüm yenileme koordinatlarını F8 ile kaydedin."); return; }
            for (int i = 0; i < windows.Count; i++) { await ClickRefreshAsync(windows[i], CancellationToken.None); UpdateRow(i, "YENİLENİYOR...", 0, Color.Khaki); await Task.Delay(300); }
            status.Text = $"Tüm sayfalar yenilendi. {pageReloadWaitSeconds} saniye bekleniyor..."; await Task.Delay(pageReloadWaitSeconds * 1000); DetectErrors();
        }
        catch (Exception ex) { MessageBox.Show("Yenileme hatası:\n" + ex.Message); }
    }

    async Task RefreshDetectedErrorsAsync()
    {
        try
        {
            if (closeButtonTemplate == null) { LoadTemplate(); if (closeButtonTemplate == null) return; }
            ScanWindows(); if (!CoordinatesReady()) { MessageBox.Show("Önce tüm yenileme koordinatlarını F8 ile kaydedin."); return; }
            double threshold = (double)thresholdBox.Value; var errors = new List<int>();
            for (int i = 0; i < windows.Count; i++) { var r = FindError(windows[i], threshold); UpdateRow(i, r.Found ? "HATA BULUNDU" : "Normal", r.Score, r.Found ? Color.MistyRose : Color.Honeydew); if (r.Found) errors.Add(i); }
            foreach (int i in errors) { await ClickRefreshAsync(windows[i], CancellationToken.None); UpdateRow(i, "YENİLENİYOR...", 0, Color.Khaki); await Task.Delay(300); }
            await Task.Delay(pageReloadWaitSeconds * 1000); DetectErrors();
        }
        catch (Exception ex) { MessageBox.Show("Yenileme hatası:\n" + ex.Message); }
    }

    static Bitmap CaptureScreenArea(int x, int y, int width, int height)
    {
        var bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb); using var g = Graphics.FromImage(bmp); g.CopyFromScreen(x, y, 0, 0, new System.Drawing.Size(width, height), CopyPixelOperation.SourceCopy); return bmp;
    }

    sealed class ChromeWindow
    {
        public IntPtr Handle; public string Title=""; public string? Url; public int X,Y,Width,Height;
        public double? RefreshRX,RefreshRY,Click1RX,Click1RY,Click2RX,Click2RY,Click3RX,Click3RY; public int? RefreshOffsetX, RefreshOffsetY;
    }
    sealed class SessionRecord
    {
        public int WindowNo { get; set; }
        public string Url { get; set; } = "";
        public string Title { get; set; } = "";
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public double? RefreshRX { get; set; }
        public double? RefreshRY { get; set; }
        public int? RefreshOffsetX { get; set; }
        public int? RefreshOffsetY { get; set; }
        public double? Click1RX { get; set; }
        public double? Click1RY { get; set; }
        public double? Click2RX { get; set; }
        public double? Click2RY { get; set; }
        public double? Click3RX { get; set; }
        public double? Click3RY { get; set; }
    }

    sealed class RefreshCoordinate
    {
        public int WindowNo {get;set;}
        public int WindowWidth {get;set;} public int WindowHeight {get;set;}
        public double? RefreshRX {get;set;} public double? RefreshRY {get;set;} public int? RefreshOffsetX {get;set;} public int? RefreshOffsetY {get;set;}
        public double? Click1RX {get;set;} public double? Click1RY {get;set;}
        public double? Click2RX {get;set;} public double? Click2RY {get;set;}
        public double? Click3RX {get;set;} public double? Click3RY {get;set;}
        // v4.7 compatibility
        public int? X {get;set;} public int? Y {get;set;}
        public int? Click1X {get;set;} public int? Click1Y {get;set;}
        public int? Click2X {get;set;} public int? Click2Y {get;set;}
        public int? Click3X {get;set;} public int? Click3Y {get;set;}
    }

}
