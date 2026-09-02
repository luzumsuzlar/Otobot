using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Windows.Automation;

namespace Otobot;

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
    [DllImport("user32.dll")] static extern bool IsIconic(IntPtr hWnd);
    [DllImport("user32.dll", SetLastError = true)] static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
    [DllImport("user32.dll")] static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    [DllImport("user32.dll")] static extern bool SetCursorPos(int X, int Y);
    [DllImport("user32.dll")] static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
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
    const uint MOUSEEVENTF_WHEEL = 0x0800;
    const uint MOD_NOREPEAT = 0x4000;
    const int HOTKEY_F8 = 5001, HOTKEY_F12 = 5002, HOTKEY_F11 = 5003, HOTKEY_F9 = 5004;

    readonly DataGridView grid = new();
    readonly Label status = new();
    readonly Label hotkeyStatus = new();
    readonly Panel notificationPanel = new();
    readonly Label notificationLabel = new();
    readonly Button clearNotificationButton = new();
    readonly Button scanButton = new();
    readonly NumericUpDown reloadWaitBox = new();
    readonly NumericUpDown scanIntervalBox = new();
    readonly NumericUpDown actionClickDelayBox = new();

    readonly Button detectButton = new();
    readonly Button errorRefreshButton = new();
    readonly Button refreshAllButton = new();
    readonly Button captureClickButton = new();
    readonly Button startButton = new();
    readonly Button stopButton = new();
    readonly Button saveSessionButton = new();
    readonly Button restoreSelectedButton = new();
    readonly Button restoreAllButton = new();
    readonly NumericUpDown thresholdBox = new();
    readonly NumericUpDown actionTemplateThresholdBox = new();
    readonly TextBox oldDomainBox = new();
    readonly TextBox newDomainBox = new();
    readonly Button replaceDomainButton = new();
    readonly Button checkUpdateButton = new();
    readonly CheckBox useVisualActionsCheckBox = new();
    readonly TextBox gmailAddressBox = new();
    readonly TextBox gmailAppPasswordBox = new();
    readonly TextBox gmailExpectedSenderBox = new();
    readonly Button saveGmailSettingsButton = new();
    readonly Button testGmailCodeButton = new();
    readonly Button fillGmailCodeButton = new();
    readonly GmailCodeService gmailCodeService = new();
    readonly TextBox siteUserNameBox = new();
    readonly TextBox sitePasswordBox = new();
    readonly Button saveSiteLoginButton = new();
    readonly Button captureHomeLoginButton = new();
    readonly Button captureLoginFormButton = new();
    readonly Button startAutomaticLoginButton = new();
    readonly SiteLoginSettingsService siteLoginSettingsService = new();
    readonly TextBox urlListBaseAddressBox = new();
    readonly DataGridView urlListGrid = new();
    readonly Button saveUrlListButton = new();
    readonly Button addUrlListRowButton = new();
    readonly UrlListService urlListService = new();

    readonly List<ChromeWindow> windows = new();
    readonly string coordFile = AppDataPaths.GetDataFilePath("refresh_coordinates.json");
    readonly string sessionFile = AppDataPaths.GetDataFilePath("chrome_session.json");
    Mat? closeButtonTemplate;
    Mat? refreshButtonTemplate;
    VisualTemplateDefinition refreshTemplateDefinition = new();
    Mat? fullscreenButtonTemplate;
    VisualTemplateDefinition fullscreenTemplateDefinition = new();
    Mat? homeLoginButtonTemplate;
    VisualTemplateDefinition homeLoginTemplateDefinition = new();
    Mat? loginSubmitButtonTemplate;
    LoginFormTemplateDefinition loginFormTemplateDefinition = new();
    readonly Mat?[] actionButtonTemplates = new Mat?[3];
    VisualTemplateDefinition[] actionTemplateDefinitions =
    [
        new(), new(), new()
    ];
    CancellationTokenSource? scanCts;
    int pageReloadWaitSeconds = 30;
    int scanIntervalSeconds = 60;
    int actionClickDelayMs = 500;
    bool useVisualActions = true;
    bool urlListLoading;

    int selectedClickNumber = 1;
    IntPtr keyboardHook = IntPtr.Zero;
    LowLevelKeyboardProc? keyboardProc;
    IntPtr mouseHook = IntPtr.Zero;
    LowLevelMouseProc? mouseProc;
    bool autoCoordinateCapture = false;

    int autoCoordinateRow = 0;
    int autoCaptureStep = 0;
    int pendingActionCaptureNumber = 0;
    bool pendingActionCaptureUsesVisual = true;
    bool pendingRefreshTemplateCapture = false;
    bool pendingFullscreenTemplateCapture = false;
    bool pendingHomeLoginTemplateCapture = false;
    int pendingLoginFormCaptureStep = 0;
    System.Drawing.Point pendingLoginUserNamePoint;
    System.Drawing.Point pendingLoginPasswordPoint;
    bool suppressActionCaptureMouseDown = false;

    const int RefreshTemplateWidth = 74;
    const int RefreshTemplateHeight = 42;
    const int RefreshSearchHeight = 180;
    const int ActionTemplateWidth = 100;
    const int ActionTemplateHeight = 50;
    const int TemplateCaptureSettleMs = 350;

    readonly Button autoCoordinateButton = new();
    readonly Button captureClick1Button = new();
    readonly Button captureClick2Button = new();
    readonly Button captureClick3Button = new();
    readonly Button testActionVisualsButton = new();
    readonly Button captureFullscreenVisualButton = new();
    readonly Button testFullscreenVisualButton = new();

    public MainForm()
    {
        Text = "Otobot - Hata Ekranı Algılama";
        Width = 1250; Height = 760; StartPosition = FormStartPosition.CenterScreen;
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

        captureFullscreenVisualButton.Text = "TAM EKRAN GÖRSELİNİ KAYDET";
        captureFullscreenVisualButton.AutoSize = true;
        captureFullscreenVisualButton.Click += (_, _) => BeginCaptureFullscreenTemplate();
        testFullscreenVisualButton.Text = "TAM EKRAN GÖRSELİNİ TEST ET";
        testFullscreenVisualButton.AutoSize = true;
        testFullscreenVisualButton.Click += async (_, _) => await TestFullscreenTemplateAsync();

        captureClickButton.Text = "Sıradaki İşlem Görselini Kaydet (F9)";
        captureClickButton.AutoSize = true;
        captureClickButton.Click += (_, _) => BeginCaptureAction(selectedClickNumber);

        autoCoordinateButton.Text = "⚡ TÜM KOORDİNATLARI TOPLA";
        autoCoordinateButton.AutoSize = true;
        autoCoordinateButton.Font = new Font(Font.FontFamily, 9, FontStyle.Bold);
        autoCoordinateButton.Click += (_, _) => StartAutoCoordinateCapture();

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

        actionTemplateThresholdBox.DecimalPlaces = 2;
        actionTemplateThresholdBox.Minimum = .50m;
        actionTemplateThresholdBox.Maximum = .99m;
        actionTemplateThresholdBox.Increment = .01m;
        actionTemplateThresholdBox.Value = .65m;
        actionTemplateThresholdBox.Width = 75;
        actionTemplateThresholdBox.ValueChanged += (_, _) => SaveTimingSettings();

        hotkeyStatus.Text = "F12: Başlat | F11: Durdur | F8: Yenileme | F9: İşlem kaydı";
        hotkeyStatus.AutoSize = true;
        hotkeyStatus.Padding = new Padding(8, 8, 0, 0);

        // ---------------- PENCERELER SEKME ----------------
        var windowsPage = new TabPage("🪟 Pencereler");

        var windowTop = new FlowLayoutPanel
        {
            Dock = DockStyle.Top,
            Height = 150,
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

        useVisualActionsCheckBox.Text = "GÖRSEL MODU (kapalıysa koordinat kullanılır)";
        useVisualActionsCheckBox.Checked = true;
        useVisualActionsCheckBox.AutoSize = true;
        useVisualActionsCheckBox.Font = new Font(Font.FontFamily, 9, FontStyle.Bold);
        useVisualActionsCheckBox.Padding = new Padding(6, 5, 6, 0);
        useVisualActionsCheckBox.CheckedChanged += (_, _) =>
        {
            useVisualActions = useVisualActionsCheckBox.Checked;
            ApplyActionModeUi();
            SaveTimingSettings();
        };

        captureClickButton.Text = "Sıradaki İşlem Görselini Kaydet (F9)";
        captureClickButton.AutoSize = true;

        captureClick1Button.Text = "İşlem 1 Görselini Kaydet";
        captureClick1Button.AutoSize = true;
        captureClick1Button.Click += (_, _) => BeginCaptureAction(1);

        captureClick2Button.Text = "İşlem 2 Görselini Kaydet";
        captureClick2Button.AutoSize = true;
        captureClick2Button.Click += (_, _) => BeginCaptureAction(2);

        captureClick3Button.Text = "İşlem 3 Görselini Kaydet";
        captureClick3Button.AutoSize = true;
        captureClick3Button.Click += (_, _) => BeginCaptureAction(3);

        testActionVisualsButton.Text = "İŞLEM GÖRSELLERİNİ TEST ET";
        testActionVisualsButton.AutoSize = true;
        testActionVisualsButton.Click += async (_, _) => await TestActionTemplatesAsync();

        saveSessionButton.Text = "PENCERELERİ KAYDET";
        saveSessionButton.AutoSize = true;
        restoreSelectedButton.Text = "SEÇİLİ PENCEREYİ GERİ YÜKLE";
        restoreSelectedButton.AutoSize = true;
        restoreAllButton.Text = "KAYITLI PENCERELERİ GERİ YÜKLE";
        restoreAllButton.AutoSize = true;


        // Bot kontrolleri Pencereler sekmesinde.
        foreach (Control c in new Control[] {
            startButton, stopButton, useVisualActionsCheckBox,
            scanButton, detectButton, errorRefreshButton,
            refreshAllButton, captureClickButton,
            autoCoordinateButton,
            captureClick1Button, captureClick2Button, captureClick3Button,
            testActionVisualsButton, saveSessionButton,
            restoreSelectedButton, restoreAllButton
        })
        {
            windowTop.Controls.Add(c);
        }

        hotkeyStatus.Text = "F12: Başlat | F11: Durdur | F8: Yenileme | F9: İşlem kaydı";
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

        ApplyActionModeUi();

        windowsPage.Controls.Add(grid);
        windowsPage.Controls.Add(windowTop);

        // ---------------- AYARLAR SEKME ----------------
        var settingsPage = new TabPage("⚙ Ayarlar") { AutoScroll = true };
        var settingsPanel = new TableLayoutPanel
        {
            Dock = DockStyle.Top,
            Padding = new Padding(20),
            AutoSize = true,
            ColumnCount = 2,
            RowCount = 12
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

        settingsPanel.Controls.Add(new Label { Text = "İşlem görselleri eşik değeri:", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 1);
        settingsPanel.Controls.Add(actionTemplateThresholdBox, 1, 1);
        settingsPanel.Controls.Add(new Label { Text = "Sayfa yenileme sonrası bekleme (sn):", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 2);
        settingsPanel.Controls.Add(reloadWaitBox, 1, 2);
        settingsPanel.Controls.Add(new Label { Text = "Tarama döngüleri arasındaki bekleme (sn):", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 3);
        settingsPanel.Controls.Add(scanIntervalBox, 1, 3);
        settingsPanel.Controls.Add(new Label { Text = "Görsel tıklamaları arasındaki bekleme (ms):", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 4);
        settingsPanel.Controls.Add(actionClickDelayBox, 1, 4);

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
        settingsPanel.Controls.Add(urlGroup, 0, 6);
        settingsPanel.SetColumnSpan(urlGroup, 2);

        var settingsNote = new Label
        {
            Text = "Not: URL değişikliğinde yalnızca eski domain değiştirilir; URL'nin geri kalan yolu ve parametreleri korunur.",
            AutoSize = true, MaximumSize = new System.Drawing.Size(700, 0), Padding = new Padding(0, 10, 0, 0)
        };
        settingsPanel.Controls.Add(settingsNote, 0, 7);
        settingsPanel.SetColumnSpan(settingsNote, 2);

        var gmailGroup = new GroupBox
        {
            Text = "Otomatik Kullanıcı Girişi ve Gmail Doğrulama",
            Dock = DockStyle.Top,
            Padding = new Padding(12),
            Height = 405
        };
        var gmailPanel = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 12 };
        gmailPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160));
        gmailPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        gmailAddressBox.Dock = DockStyle.Fill;
        gmailAddressBox.PlaceholderText = "ornek@gmail.com";
        gmailAppPasswordBox.Dock = DockStyle.Fill;
        gmailAppPasswordBox.UseSystemPasswordChar = true;
        gmailAppPasswordBox.PlaceholderText = "Google'ın oluşturduğu 16 haneli uygulama şifresi";
        gmailExpectedSenderBox.Dock = DockStyle.Fill;
        gmailExpectedSenderBox.PlaceholderText = "Örn. no-reply@siteadi.com (ilk koddan sonra doldurun)";
        siteUserNameBox.Dock = DockStyle.Fill;
        siteUserNameBox.PlaceholderText = "Site kullanıcı adı";
        sitePasswordBox.Dock = DockStyle.Fill;
        sitePasswordBox.UseSystemPasswordChar = true;
        sitePasswordBox.PlaceholderText = "Site şifresi";
        gmailPanel.Controls.Add(new Label { Text = "Site kullanıcı adı:", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 0);
        gmailPanel.Controls.Add(siteUserNameBox, 1, 0);
        gmailPanel.Controls.Add(new Label { Text = "Site şifresi:", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 1);
        gmailPanel.Controls.Add(sitePasswordBox, 1, 1);
        saveSiteLoginButton.Text = "SİTE GİRİŞ BİLGİLERİNİ GÜVENLE KAYDET";
        saveSiteLoginButton.AutoSize = true;
        saveSiteLoginButton.Click += (_, _) => SaveSiteLoginSettings();
        gmailPanel.Controls.Add(saveSiteLoginButton, 1, 2);
        captureHomeLoginButton.Text = "ANA SAYFA GİRİŞ YAP GÖRSELİNİ KAYDET";
        captureHomeLoginButton.AutoSize = true;
        captureHomeLoginButton.Click += (_, _) => BeginCaptureHomeLoginTemplate();
        gmailPanel.Controls.Add(captureHomeLoginButton, 1, 3);
        captureLoginFormButton.Text = "GİRİŞ FORMU ALANLARINI KAYDET";
        captureLoginFormButton.AutoSize = true;
        captureLoginFormButton.Click += (_, _) => BeginCaptureLoginFormTemplate();
        gmailPanel.Controls.Add(captureLoginFormButton, 1, 4);
        startAutomaticLoginButton.Text = "OTOMATİK GİRİŞİ BAŞLAT";
        startAutomaticLoginButton.AutoSize = true;
        startAutomaticLoginButton.Font = new Font(Font.FontFamily, 9, FontStyle.Bold);
        startAutomaticLoginButton.Click += async (_, _) => await StartAutomaticLoginAsync();
        gmailPanel.Controls.Add(startAutomaticLoginButton, 1, 5);
        gmailPanel.Controls.Add(new Label { Text = "Gmail adresi:", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 6);
        gmailPanel.Controls.Add(gmailAddressBox, 1, 6);
        gmailPanel.Controls.Add(new Label { Text = "Uygulama şifresi:", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 7);
        gmailPanel.Controls.Add(gmailAppPasswordBox, 1, 7);
        gmailPanel.Controls.Add(new Label { Text = "Kod gönderen:", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 8);
        gmailPanel.Controls.Add(gmailExpectedSenderBox, 1, 8);
        saveGmailSettingsButton.Text = "GMAIL AYARLARINI GÜVENLE KAYDET";
        saveGmailSettingsButton.AutoSize = true;
        saveGmailSettingsButton.Click += (_, _) => SaveGmailSettings();
        gmailPanel.Controls.Add(saveGmailSettingsButton, 1, 9);
        testGmailCodeButton.Text = "SON DOĞRULAMA KODUNU TEST ET";
        testGmailCodeButton.AutoSize = true;
        testGmailCodeButton.Click += async (_, _) => await TestGmailCodeAsync();
        gmailPanel.Controls.Add(testGmailCodeButton, 1, 10);
        fillGmailCodeButton.Text = "AÇIK KOD EKRANINA KODU YAZ";
        fillGmailCodeButton.AutoSize = true;
        fillGmailCodeButton.Click += async (_, _) => await FillGmailCodeIntoOpenScreenAsync();
        gmailPanel.Controls.Add(fillGmailCodeButton, 1, 11);
        gmailGroup.Controls.Add(gmailPanel);
        settingsPanel.Controls.Add(gmailGroup, 0, 10);
        settingsPanel.SetColumnSpan(gmailGroup, 2);

        settingsPanel.Controls.Add(new Label
        {
            Text = $"Kurulu sürüm: {Application.ProductVersion}",
            AutoSize = true,
            Anchor = AnchorStyles.Left
        }, 0, 11);
        checkUpdateButton.Text = "GÜNCELLEME DENETLE";
        checkUpdateButton.AutoSize = true;
        checkUpdateButton.Click += async (_, _) =>
        {
            checkUpdateButton.Enabled = false;
            try
            {
                await UpdateService.CheckForUpdatesAsync(
                    this,
                    true,
                    message => status.Text = message,
                    ShowWarning);
            }
            finally
            {
                checkUpdateButton.Enabled = true;
            }
        };
        settingsPanel.Controls.Add(checkUpdateButton, 1, 11);
        settingsPage.Controls.Add(settingsPanel);

        var urlListPage = new TabPage("🔗 URL Listesi") { Padding = new Padding(16) };
        var urlListLayout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 4
        };
        urlListLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 110));
        urlListLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
        urlListLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        urlListLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        urlListLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        urlListLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        urlListLayout.Controls.Add(new Label { Text = "Ana adres:", AutoSize = true, Anchor = AnchorStyles.Left }, 0, 0);
        urlListBaseAddressBox.Dock = DockStyle.Fill;
        urlListBaseAddressBox.PlaceholderText = "https://www.ornek.com";
        urlListBaseAddressBox.TextChanged += (_, _) => RefreshUrlListPreviews();
        urlListLayout.Controls.Add(urlListBaseAddressBox, 1, 0);
        urlListLayout.Controls.Add(new Label
        {
            Text = "Her satıra yalnızca / ile başlayan kalan URL bölümünü yazın. Ana adres değiştiğinde tüm tam adresler otomatik güncellenir.",
            AutoSize = true,
            MaximumSize = new System.Drawing.Size(800, 0),
            Padding = new Padding(0, 8, 0, 8)
        }, 1, 1);

        urlListGrid.Dock = DockStyle.Fill;
        urlListGrid.AllowUserToAddRows = false;
        urlListGrid.AllowUserToDeleteRows = false;
        urlListGrid.RowHeadersVisible = false;
        urlListGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        urlListGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "No",
            HeaderText = "No",
            ReadOnly = true,
            FillWeight = 12
        });
        urlListGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "Remainder",
            HeaderText = "Geri Kalan URL",
            FillWeight = 44
        });
        urlListGrid.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = "FullUrl",
            HeaderText = "Tam Adres Önizlemesi",
            ReadOnly = true,
            FillWeight = 44
        });
        urlListGrid.CellValueChanged += (_, _) => RefreshUrlListPreviews();
        urlListLayout.Controls.Add(urlListGrid, 0, 2);
        urlListLayout.SetColumnSpan(urlListGrid, 2);
        var urlListButtons = new FlowLayoutPanel { AutoSize = true, Dock = DockStyle.Left };
        saveUrlListButton.Text = "URL LİSTESİNİ KAYDET";
        saveUrlListButton.AutoSize = true;
        saveUrlListButton.Click += (_, _) => SaveUrlList();
        addUrlListRowButton.Text = "+ YENİ URL SATIRI";
        addUrlListRowButton.AutoSize = true;
        addUrlListRowButton.Click += (_, _) => AddUrlListRow();
        urlListButtons.Controls.Add(saveUrlListButton);
        urlListButtons.Controls.Add(addUrlListRowButton);
        urlListLayout.Controls.Add(urlListButtons, 1, 3);
        urlListPage.Controls.Add(urlListLayout);

        var tabs = new TabControl { Dock = DockStyle.Fill };
        tabs.TabPages.Add(windowsPage);
        tabs.TabPages.Add(settingsPage);
        tabs.TabPages.Add(urlListPage);

        notificationPanel.Dock = DockStyle.Bottom;
        notificationPanel.Height = 68;
        notificationPanel.Padding = new Padding(10, 8, 8, 8);
        notificationPanel.BorderStyle = BorderStyle.FixedSingle;
        notificationPanel.BackColor = Color.WhiteSmoke;

        notificationLabel.Dock = DockStyle.Fill;
        notificationLabel.Text = "Uyarı yok.";
        notificationLabel.ForeColor = Color.DimGray;
        notificationLabel.Font = new Font(Font.FontFamily, 9, FontStyle.Bold);
        notificationLabel.TextAlign = ContentAlignment.MiddleLeft;
        notificationLabel.AutoEllipsis = true;
        notificationLabel.UseMnemonic = false;
        notificationLabel.Padding = new Padding(4, 0, 8, 0);

        clearNotificationButton.Dock = DockStyle.Right;
        clearNotificationButton.Width = 82;
        clearNotificationButton.Text = "TEMİZLE";
        clearNotificationButton.Click += (_, _) => ClearNotification();

        notificationPanel.Controls.Add(notificationLabel);
        notificationPanel.Controls.Add(clearNotificationButton);
        Controls.Add(tabs);
        Controls.Add(notificationPanel);

        Shown += async (_, _) =>
        {
            LoadTimingSettings();
            LoadGmailSettings();
            LoadSiteLoginSettings();
            LoadUrlList();
            LoadTemplate();
            LoadActionTemplates();
            LoadLoginTemplates();
            ScanWindows();
            await UpdateService.CheckForUpdatesAsync(
                this,
                false,
                message => status.Text = message,
                ShowWarning);
        };
        FormClosed += (_, _) =>
        {
            closeButtonTemplate?.Dispose();
            fullscreenButtonTemplate?.Dispose();
            homeLoginButtonTemplate?.Dispose();
            loginSubmitButtonTemplate?.Dispose();
            DisposeActionTemplates();
        };
    }

    void ShowWarning(string message) => ShowNotification(message, true);

    void ShowInfo(string message) => ShowNotification(message, false);

    void ShowNotification(string message, bool isWarning)
    {
        if (IsDisposed) return;
        if (InvokeRequired)
        {
            BeginInvoke(() => ShowNotification(message, isWarning));
            return;
        }

        string normalized = message.Replace("\r\n", "\n").Trim();
        notificationLabel.Text =
            $"{(isWarning ? "UYARI" : "BİLGİ")} [{DateTime.Now:HH:mm:ss}]  {normalized}";
        notificationLabel.ForeColor = isWarning ? Color.DarkRed : Color.DarkGreen;
        notificationPanel.BackColor = isWarning ? Color.MistyRose : Color.Honeydew;
        notificationPanel.BringToFront();
    }

    void ClearNotification()
    {
        notificationLabel.Text = "Uyarı yok.";
        notificationLabel.ForeColor = Color.DimGray;
        notificationPanel.BackColor = Color.WhiteSmoke;
    }

    void LoadGmailSettings()
    {
        GmailCodeSettings settings = gmailCodeService.LoadSettings();
        gmailAddressBox.Text = settings.Address;
        gmailAppPasswordBox.Text = settings.AppPassword;
        gmailExpectedSenderBox.Text = settings.ExpectedSender;
    }

    void LoadSiteLoginSettings()
    {
        SiteLoginSettings settings = siteLoginSettingsService.Load();
        siteUserNameBox.Text = settings.UserName;
        sitePasswordBox.Text = settings.Password;
    }

    void SaveSiteLoginSettings()
    {
        try
        {
            siteLoginSettingsService.Save(new SiteLoginSettings
            {
                UserName = siteUserNameBox.Text,
                Password = sitePasswordBox.Text
            });
            ShowInfo("Site kullanıcı adı ve şifresi bu Windows kullanıcısı için şifreli olarak kaydedildi.");
        }
        catch (Exception ex)
        {
            ShowWarning("Site giriş bilgileri kaydedilemedi: " + ex.Message);
        }
    }

    void LoadUrlList()
    {
        UrlListSettings settings = urlListService.Load();
        urlListLoading = true;
        try
        {
            ApplyUrlList(settings);
        }
        finally
        {
            urlListLoading = false;
        }
        RefreshUrlListPreviews();
    }

    void ApplyUrlList(UrlListSettings settings)
    {
        urlListBaseAddressBox.Text = settings.BaseAddress;
        urlListGrid.Rows.Clear();
        int rowCount = Math.Max(15, settings.Remainders.Count);
        for (int index = 0; index < rowCount; index++)
        {
            string remainder = index < settings.Remainders.Count ? settings.Remainders[index] : string.Empty;
            urlListGrid.Rows.Add(index + 1, remainder, string.Empty);
        }
    }

    void AddUrlListRow()
    {
        urlListGrid.Rows.Add(urlListGrid.Rows.Count + 1, string.Empty, string.Empty);
        urlListGrid.CurrentCell = urlListGrid.Rows[urlListGrid.Rows.Count - 1].Cells["Remainder"];
        urlListGrid.BeginEdit(true);
    }

    void RefreshUrlListPreviews()
    {
        if (urlListLoading || urlListGrid.Rows.Count == 0) return;
        urlListLoading = true;
        try
        {
            string baseAddress = urlListBaseAddressBox.Text.Trim().TrimEnd('/');
            foreach (DataGridViewRow row in urlListGrid.Rows)
            {
                string remainder = row.Cells["Remainder"].Value?.ToString()?.Trim() ?? string.Empty;
                row.Cells["FullUrl"].Value = string.IsNullOrWhiteSpace(baseAddress) || string.IsNullOrWhiteSpace(remainder)
                    ? string.Empty
                    : baseAddress + (remainder.StartsWith('/') ? remainder : "/" + remainder);
            }
        }
        finally
        {
            urlListLoading = false;
        }
    }

    void SaveUrlList()
    {
        try
        {
            var settings = new UrlListSettings
            {
                BaseAddress = urlListBaseAddressBox.Text,
                Remainders = urlListGrid.Rows.Cast<DataGridViewRow>()
                    .Select(row => row.Cells["Remainder"].Value?.ToString() ?? string.Empty)
                    .ToList()
            };
            urlListService.Save(settings);
            RefreshUrlListPreviews();
            ShowInfo("URL listesi kaydedildi. Ana adres değiştiğinde yalnızca bu alanı güncellemeniz yeterli.");
        }
        catch (Exception ex)
        {
            ShowWarning("URL listesi kaydedilemedi: " + ex.Message);
        }
    }

    void SaveGmailSettings()
    {
        try
        {
            gmailCodeService.SaveSettings(new GmailCodeSettings
            {
                Address = gmailAddressBox.Text,
                AppPassword = gmailAppPasswordBox.Text,
                ExpectedSender = gmailExpectedSenderBox.Text
            });
            ShowInfo("Gmail ayarları bu Windows kullanıcısı için şifreli olarak kaydedildi.");
        }
        catch (Exception ex)
        {
            ShowWarning("Gmail ayarları kaydedilemedi: " + ex.Message);
        }
    }

    async Task TestGmailCodeAsync()
    {
        testGmailCodeButton.Enabled = false;
        try
        {
            var settings = new GmailCodeSettings
            {
                Address = gmailAddressBox.Text,
                AppPassword = gmailAppPasswordBox.Text,
                ExpectedSender = gmailExpectedSenderBox.Text
            };
            gmailCodeService.SaveSettings(settings);
            GmailVerificationCode code = await gmailCodeService.FindRecentCodeAsync(
                settings,
                DateTime.Now.AddHours(-24));
            ShowInfo($"Doğrulama kodu bulundu: {code.Code}. Gönderici filtresi için: {code.Sender}");
        }
        catch (Exception ex)
        {
            ShowWarning("Gmail doğrulama kodu test edilemedi: " + ex.Message);
        }
        finally
        {
            testGmailCodeButton.Enabled = true;
        }
    }

    async Task FillGmailCodeIntoOpenScreenAsync(bool waitForVerificationScreen = false)
    {
        fillGmailCodeButton.Enabled = false;
        try
        {
            ScanWindows();
            // Kod yalnızca tabloda seçili pencereye uygulanır. Tek pencere
            // varsa doğrudan o pencere kullanılır.
            ChromeWindow? target = TryGetSelectedWindow(out var selected, out _)
                ? selected
                : windows.Count == 1 ? windows[0] : null;
            if (target == null)
                throw new InvalidOperationException(
                    "Birden fazla Chrome penceresi açık. Kodun yazılacağı pencereyi tablodan seçin.");
            if (!await ActivateChromeWindowAsync(target.Handle))
                throw new InvalidOperationException("Chrome penceresi öne getirilemedi.");

            bool codeScreenFound = IsVerificationCodeScreen(target);
            if (!codeScreenFound && waitForVerificationScreen)
            {
                for (int attempt = 1; attempt <= 5 && !codeScreenFound; attempt++)
                {
                    status.Text = $"Doğrulama kodu ekranı henüz gelmedi. {attempt}/5 — 30 saniye sonra yeniden kontrol edilecek.";
                    await Task.Delay(TimeSpan.FromSeconds(30));
                    codeScreenFound = IsVerificationCodeScreen(target);
                }
            }
            if (!codeScreenFound)
                throw new InvalidOperationException(waitForVerificationScreen
                    ? "Doğrulama kodu ekranı 5 kez, 30 saniye arayla kontrol edildi ancak bulunamadı."
                    : "Bu pencerede altı haneli doğrulama kodu ekranı bulunamadı.");

            var settings = new GmailCodeSettings
            {
                Address = gmailAddressBox.Text,
                AppPassword = gmailAppPasswordBox.Text,
                ExpectedSender = gmailExpectedSenderBox.Text
            };
            bool loggedIn = false;
            for (int attempt = 1; attempt <= 4; attempt++)
            {
                GmailVerificationCode code = await gmailCodeService.FindRecentCodeAsync(
                    settings,
                    DateTime.Now.AddHours(-24));

                await WriteVerificationCodeAsync(target, code.Code);
                status.Text = $"Giriş doğrulanıyor... Deneme {attempt}/4";
                await Task.Delay(TimeSpan.FromSeconds(5));

                if (!IsVerificationCodeScreen(target))
                {
                    loggedIn = true;
                    ShowInfo($"Giriş doğrulandı. Kod {attempt}. denemede kabul edildi.");
                    await ReloadUrlsAndPerformActionsAsync();
                    break;
                }

                status.Text = $"Kod kabul edilmedi veya hata oluştu. Deneme {attempt}/4.";
                await Task.Delay(1200);
            }

            if (!loggedIn)
                ShowWarning("4 denemeden sonra giriş yapılamadı. Chrome penceresini ve doğrulama e-postasını kontrol edin.");
        }
        catch (Exception ex)
        {
            ShowWarning("Doğrulama kodu ekrana yazılamadı: " + ex.Message);
        }
        finally
        {
            fillGmailCodeButton.Enabled = true;
        }
    }

    static bool IsVerificationCodeScreen(ChromeWindow window)
    {
        using Bitmap image = CaptureScreenArea(window.X, window.Y, window.Width, window.Height);
        return TryFindVerificationCodeInputs(image, out _);
    }

    static bool TryFindVerificationCodeInputs(Bitmap image, out List<System.Drawing.Point> inputs)
    {
        inputs = [];
        int left = (int)(image.Width * .10);
        int right = (int)(image.Width * .90);
        int top = (int)(image.Height * .15);
        int bottom = (int)(image.Height * .85);
        // Kod kutuları yaklaşık 50 piksel yüksekliğinde kalır; Chrome pencere
        // yüksekliği büyüdükçe eşik yükseltilirse yeni form yanlışlıkla elenir.
        int minimumColumnPixels = Math.Clamp((bottom - top) / 28, 4, 20);
        var runs = new List<(int Start, int End)>();
        int runStart = -1;

        for (int x = left; x < right; x++)
        {
            int bluePixels = 0;
            for (int y = top; y < bottom; y++)
            {
                Color pixel = image.GetPixel(x, y);
                if (IsCodeBoxBlue(pixel)) bluePixels++;
            }
            if (bluePixels >= minimumColumnPixels && runStart < 0) runStart = x;
            if (bluePixels < minimumColumnPixels && runStart >= 0)
            {
                if (x - runStart >= Math.Max(8, image.Width / 100)) runs.Add((runStart, x - 1));
                runStart = -1;
            }
        }
        if (runStart >= 0 && right - runStart >= Math.Max(8, image.Width / 100))
            runs.Add((runStart, right - 1));

        // Yeni pencerede altı aynı aralıklı kod kutusu vardır. Bu kontrol, iki
        // alanlı kullanıcı adı/şifre formunun kod ekranı sanılmasını önler.
        for (int start = 0; start + 5 < runs.Count; start++)
        {
            double[] gaps = Enumerable.Range(start, 5)
                .Select(index => (runs[index + 1].Start + runs[index + 1].End - runs[index].Start - runs[index].End) / 2.0)
                .ToArray();
            if (gaps.Any(gap => gap < 4 || gap > image.Width * .30)) continue;
            if (gaps.Max() - gaps.Min() > gaps.Average() * .80) continue;

            var candidate = new List<System.Drawing.Point>(6);
            foreach (var run in runs.Skip(start).Take(6))
            {
                int centerX = (run.Start + run.End) / 2;
                int bestStart = -1, bestEnd = -1, currentStart = -1;
                for (int y = top; y < bottom; y++)
                {
                    int count = 0;
                    for (int x = run.Start; x <= run.End; x++)
                        if (IsCodeBoxBlue(image.GetPixel(x, y))) count++;
                    if (count >= Math.Max(6, (run.End - run.Start + 1) / 2) && currentStart < 0)
                        currentStart = y;
                    if ((count < Math.Max(6, (run.End - run.Start + 1) / 2) || y == bottom - 1) && currentStart >= 0)
                    {
                        int end = count < Math.Max(6, (run.End - run.Start + 1) / 2) ? y - 1 : y;
                        if (end - currentStart > bestEnd - bestStart)
                        {
                            bestStart = currentStart;
                            bestEnd = end;
                        }
                        currentStart = -1;
                    }
                }
                if (bestStart < 0) { candidate.Clear(); break; }
                candidate.Add(new System.Drawing.Point(centerX, (bestStart + bestEnd) / 2));
            }

            if (candidate.Count == 6 && HasLoginButtonRed(image, left, right, top, bottom))
            {
                inputs = candidate;
                return true;
            }
        }
        return false;
    }

    static bool IsCodeBoxBlue(Color color) =>
        color.B > color.R + 6 && color.B > color.G + 3 && color.B >= 45;

    static bool HasLoginButtonRed(Bitmap image, int left, int right, int top, int bottom)
    {
        int redPixels = 0;
        for (int y = top; y < bottom; y += 2)
        for (int x = left; x < right; x += 2)
        {
            Color pixel = image.GetPixel(x, y);
            if (pixel.R > 160 && pixel.G < 100 && pixel.B < 100) redPixels++;
        }
        return redPixels >= 70;
    }

    static async Task WriteVerificationCodeAsync(ChromeWindow target, string code)
    {
        using Bitmap image = CaptureScreenArea(target.X, target.Y, target.Width, target.Height);
        if (!TryFindVerificationCodeInputs(image, out List<System.Drawing.Point> inputs))
            throw new InvalidOperationException("Yeni doğrulama ekranındaki altı kod kutusu bulunamadı.");

        for (int index = 0; index < Math.Min(code.Length, inputs.Count); index++)
        {
            await ClickScreenPointAsync(target.X + inputs[index].X, target.Y + inputs[index].Y, CancellationToken.None);
            SendKeys.SendWait(code[index].ToString());
            await Task.Delay(80);
        }
    }

    string HomeLoginTemplateFilePath => AppDataPaths.GetDataFilePath("home_login_button.png");
    string HomeLoginTemplateSettingsPath => AppDataPaths.GetDataFilePath("home_login_button_settings.json");
    string LoginSubmitTemplateFilePath => AppDataPaths.GetDataFilePath("login_submit_button.png");
    string LoginFormTemplateSettingsPath => AppDataPaths.GetDataFilePath("login_form_settings.json");

    void LoadLoginTemplates()
    {
        homeLoginButtonTemplate?.Dispose();
        loginSubmitButtonTemplate?.Dispose();
        homeLoginButtonTemplate = null;
        loginSubmitButtonTemplate = null;
        homeLoginTemplateDefinition = new();
        loginFormTemplateDefinition = new();
        try
        {
            if (File.Exists(HomeLoginTemplateSettingsPath))
                homeLoginTemplateDefinition = JsonSerializer.Deserialize<VisualTemplateDefinition>(File.ReadAllText(HomeLoginTemplateSettingsPath)) ?? new();
            if (File.Exists(LoginFormTemplateSettingsPath))
                loginFormTemplateDefinition = JsonSerializer.Deserialize<LoginFormTemplateDefinition>(File.ReadAllText(LoginFormTemplateSettingsPath)) ?? new();
            if (File.Exists(HomeLoginTemplateFilePath))
            {
                using var image = new Bitmap(HomeLoginTemplateFilePath);
                homeLoginButtonTemplate = BitmapConverter.ToMat(image);
            }
            if (File.Exists(LoginSubmitTemplateFilePath))
            {
                using var image = new Bitmap(LoginSubmitTemplateFilePath);
                loginSubmitButtonTemplate = BitmapConverter.ToMat(image);
            }
        }
        catch
        {
            homeLoginButtonTemplate?.Dispose();
            loginSubmitButtonTemplate?.Dispose();
            homeLoginButtonTemplate = null;
            loginSubmitButtonTemplate = null;
        }
        captureHomeLoginButton.Text = homeLoginButtonTemplate == null ?
            "ANA SAYFA GİRİŞ YAP GÖRSELİNİ KAYDET" : "✓ ANA SAYFA GİRİŞ YAP GÖRSELİNİ DEĞİŞTİR";
        captureLoginFormButton.Text = loginSubmitButtonTemplate == null ?
            "GİRİŞ FORMU ALANLARINI KAYDET" : "✓ GİRİŞ FORMU ALANLARINI DEĞİŞTİR";
    }

    void BeginCaptureHomeLoginTemplate()
    {
        if (scanCts != null) { ShowWarning("Önce çalışan taramayı F11 ile durdurun."); return; }
        pendingHomeLoginTemplateCapture = true;
        pendingLoginFormCaptureStep = 0;
        status.Text = "Ana sayfadaki GİRİŞ YAP düğmesinin ORTASINA sol tıklayın. Bu tıklama siteye gönderilmeyecek.";
    }

    async Task CaptureHomeLoginTemplateAsync(int screenX, int screenY)
    {
        if (!TryFindWindowAt(screenX, screenY, out var target))
        {
            ShowWarning("Giriş düğmesi bir Chrome penceresi üzerinde seçilmedi.");
            return;
        }
        VisualTemplateDefinition definition = await SaveTemplateAroundPointAsync(target, screenX, screenY, HomeLoginTemplateFilePath);
        homeLoginTemplateDefinition = definition;
        File.WriteAllText(HomeLoginTemplateSettingsPath, JsonSerializer.Serialize(definition, new JsonSerializerOptions { WriteIndented = true }));
        LoadLoginTemplates();
        ShowInfo("Ana sayfa GİRİŞ YAP görseli kaydedildi.");
    }

    void BeginCaptureLoginFormTemplate()
    {
        if (scanCts != null) { ShowWarning("Önce çalışan taramayı F11 ile durdurun."); return; }
        pendingHomeLoginTemplateCapture = false;
        pendingLoginFormCaptureStep = 1;
        status.Text = "1/3: Açık giriş formunda KULLANICI ADI alanının ortasına sol tıklayın.";
    }

    async Task CaptureLoginFormStepAsync(int step, int screenX, int screenY)
    {
        if (step == 1)
        {
            pendingLoginUserNamePoint = new System.Drawing.Point(screenX, screenY);
            pendingLoginFormCaptureStep = 2;
            status.Text = "2/3: ŞİFRE alanının ortasına sol tıklayın.";
            return;
        }
        if (step == 2)
        {
            pendingLoginPasswordPoint = new System.Drawing.Point(screenX, screenY);
            pendingLoginFormCaptureStep = 3;
            status.Text = "3/3: Formdaki kırmızı GİRİŞ YAP düğmesinin ortasına sol tıklayın.";
            return;
        }
        pendingLoginFormCaptureStep = 0;
        if (!TryFindWindowAt(screenX, screenY, out var target))
        {
            ShowWarning("Giriş formu Chrome penceresi üzerinde seçilmedi.");
            return;
        }
        VisualTemplateDefinition submitDefinition = await SaveTemplateAroundPointAsync(target, screenX, screenY, LoginSubmitTemplateFilePath);
        loginFormTemplateDefinition = new LoginFormTemplateDefinition
        {
            ClickOffsetX = submitDefinition.ClickOffsetX,
            ClickOffsetY = submitDefinition.ClickOffsetY,
            UserNameOffsetX = pendingLoginUserNamePoint.X - screenX,
            UserNameOffsetY = pendingLoginUserNamePoint.Y - screenY,
            PasswordOffsetX = pendingLoginPasswordPoint.X - screenX,
            PasswordOffsetY = pendingLoginPasswordPoint.Y - screenY
        };
        File.WriteAllText(LoginFormTemplateSettingsPath, JsonSerializer.Serialize(loginFormTemplateDefinition, new JsonSerializerOptions { WriteIndented = true }));
        LoadLoginTemplates();
        ShowInfo("Giriş formu alanları ve GİRİŞ YAP görseli kaydedildi.");
    }

    async Task<VisualTemplateDefinition> SaveTemplateAroundPointAsync(ChromeWindow window, int screenX, int screenY, string filePath)
    {
        await MoveCursorAwayAndWaitAsync(window, screenX, screenY);
        int width = Math.Min(ActionTemplateWidth, window.Width);
        int height = Math.Min(ActionTemplateHeight, window.Height);
        int left = Math.Clamp(screenX - width / 2, window.X, window.X + window.Width - width);
        int top = Math.Clamp(screenY - height / 2, window.Y, window.Y + window.Height - height);
        using var image = CaptureScreenArea(left, top, width, height);
        image.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
        return new VisualTemplateDefinition { ClickOffsetX = screenX - left, ClickOffsetY = screenY - top };
    }

    async Task StartAutomaticLoginAsync()
    {
        startAutomaticLoginButton.Enabled = false;
        try
        {
            SaveSiteLoginSettings();
            SiteLoginSettings credentials = siteLoginSettingsService.Load();
            LoadLoginTemplates();
            if (homeLoginButtonTemplate == null || loginSubmitButtonTemplate == null)
                throw new InvalidOperationException("Önce ana sayfa GİRİŞ YAP görselini ve giriş formu alanlarını kaydedin.");

            ScanWindows();
            ChromeWindow? target = TryGetSelectedWindow(out var selected, out _) ? selected :
                windows.Count == 1 ? windows[0] : null;
            if (target == null)
                throw new InvalidOperationException("Birden fazla Chrome penceresi açık. Giriş yapılacak pencereyi tablodan seçin.");
            if (!await ActivateChromeWindowAsync(target.Handle))
                throw new InvalidOperationException("Chrome penceresi öne getirilemedi.");

            var homeMatch = FindVisualTemplate(target, homeLoginButtonTemplate, homeLoginTemplateDefinition, .68, target.Height);
            if (!homeMatch.Found)
                throw new InvalidOperationException($"Ana sayfa GİRİŞ YAP düğmesi bulunamadı. En iyi eşleşme: {homeMatch.Score:P1}. Görseli yeniden kaydedin.");
            status.Text = "Ana sayfadaki GİRİŞ YAP düğmesi tıklanıyor...";
            await ClickScreenPointAsync(homeMatch.ScreenX, homeMatch.ScreenY, CancellationToken.None);
            await Task.Delay(700);

            var formMatch = FindVisualTemplate(target, loginSubmitButtonTemplate, loginFormTemplateDefinition, .68, target.Height);
            if (!formMatch.Found)
                throw new InvalidOperationException($"Giriş formu düğmesi bulunamadı. En iyi eşleşme: {formMatch.Score:P1}. Form alanlarını yeniden kaydedin.");
            status.Text = "Kullanıcı adı ve şifre giriliyor...";
            await ClickScreenPointAsync(formMatch.ScreenX + loginFormTemplateDefinition.UserNameOffsetX,
                formMatch.ScreenY + loginFormTemplateDefinition.UserNameOffsetY, CancellationToken.None);
            SendKeys.SendWait("^a");
            SendKeys.SendWait(credentials.UserName);
            await ClickScreenPointAsync(formMatch.ScreenX + loginFormTemplateDefinition.PasswordOffsetX,
                formMatch.ScreenY + loginFormTemplateDefinition.PasswordOffsetY, CancellationToken.None);
            SendKeys.SendWait("^a");
            SendKeys.SendWait(credentials.Password);
            await ClickScreenPointAsync(formMatch.ScreenX, formMatch.ScreenY, CancellationToken.None);
            status.Text = "Giriş isteği gönderildi; doğrulama kodu ekranı bekleniyor...";
            await FillGmailCodeIntoOpenScreenAsync(waitForVerificationScreen: true);
        }
        catch (Exception ex)
        {
            ShowWarning("Otomatik giriş başlatılamadı: " + ex.Message);
        }
        finally { startAutomaticLoginButton.Enabled = true; }
    }

    async Task ReloadUrlsAndPerformActionsAsync()
    {
        UrlListSettings urlList = urlListService.Load();
        if (!Uri.TryCreate(urlList.BaseAddress, UriKind.Absolute, out Uri? baseUri))
            throw new InvalidOperationException("URL Listesi'ndeki ana adres geçerli değil.");

        List<string> urls = urlList.Remainders
            .Where(remainder => !string.IsNullOrWhiteSpace(remainder))
            .Select(remainder => urlList.BaseAddress.Trim().TrimEnd('/') +
                (remainder.StartsWith('/') ? remainder : "/" + remainder))
            .ToList();
        if (urls.Count == 0)
            throw new InvalidOperationException("URL Listesi boş. Önce PENCERELERİ KAYDET ile URL'leri kaydedin.");

        ScanWindows();
        if (windows.Count < urls.Count)
        {
            int missing = urls.Count - windows.Count;
            status.Text = $"Giriş sonrası {missing} eksik Chrome penceresi açılıyor...";
            foreach (string url in urls.Skip(windows.Count))
                await OpenChromeWindowForUrlAsync(url);

            ScanWindows();
            ArrangeRestoredWindowsInGrid(urls.Count);
        }
        if (windows.Count != urls.Count)
            throw new InvalidOperationException(
                $"Açık Chrome penceresi ({windows.Count}) ile URL Listesi ({urls.Count}) eşleşmiyor. " +
                "Güvenli eşleştirme için sayılar aynı olmalı.");
        if (useVisualActions && !ActionTemplatesReady())
            throw new InvalidOperationException("Üç işlem görseli eksik. Önce işlem görsellerini kaydedin.");
        if (!useVisualActions && windows.Any(window =>
            !window.Click1RX.HasValue || !window.Click1RY.HasValue ||
            !window.Click2RX.HasValue || !window.Click2RY.HasValue ||
            !window.Click3RX.HasValue || !window.Click3RY.HasValue))
            throw new InvalidOperationException("Üç işlem koordinatı tüm pencereler için kaydedilmemiş.");

        for (int index = 0; index < windows.Count; index++)
        {
            status.Text = $"Giriş sonrası URL {index + 1}/{urls.Count} yükleniyor...";
            await NavigateChromeWindowAsync(windows[index], urls[index]);
            UpdateRow(index, "URL YÜKLENİYOR...", 0, Color.Khaki);
        }

        status.Text = $"Tüm URL'ler yüklendi. {pageReloadWaitSeconds} saniye bekleniyor...";
        await Task.Delay(TimeSpan.FromSeconds(pageReloadWaitSeconds));

        var failed = new List<int>();
        for (int index = 0; index < windows.Count; index++)
        {
            try
            {
                status.Text = $"Pencere {index + 1}/{windows.Count}: 3 işlem uygulanıyor...";
                if (useVisualActions)
                    await PerformVisualActionsAsync(windows[index], CancellationToken.None);
                else
                    await PerformCoordinateActionsAsync(windows[index], CancellationToken.None);
                UpdateRow(index, "GİRİŞ SONRASI İŞLEMLER TAMAM", 0, Color.Honeydew);
            }
            catch
            {
                failed.Add(index + 1);
                UpdateRow(index, "GİRİŞ SONRASI İŞLEM HATASI", 0, Color.MistyRose);
            }
        }

        if (failed.Count == 0)
            ShowInfo("Giriş sonrası tüm URL'ler yenilendi ve 3 işlem tamamlandı.");
        else
            ShowWarning("Giriş sonrası bazı pencerelerde işlem tamamlanamadı: " + string.Join(", ", failed));
    }

    async Task NavigateChromeWindowAsync(ChromeWindow window, string url)
    {
        if (!await ActivateChromeWindowAsync(window.Handle))
            throw new InvalidOperationException("Chrome penceresi öne getirilemedi.");
        await Task.Delay(100);

        IDataObject? previousClipboard = null;
        try
        {
            if (Clipboard.ContainsData(DataFormats.UnicodeText))
                previousClipboard = Clipboard.GetDataObject();
            Clipboard.SetText(url, TextDataFormat.UnicodeText);
            SendKeys.SendWait("^l");
            await Task.Delay(80);
            SendKeys.SendWait("^v");
            SendKeys.SendWait("{ENTER}");
            await Task.Delay(250);
        }
        finally
        {
            if (previousClipboard != null)
            {
                try { Clipboard.SetDataObject(previousClipboard, true); } catch { }
            }
        }
    }

    // Eski Telegram raporlama akışı kaldırıldı. Yeni tasarım için temiz bir
    // başlangıç yapılana kadar bu kod derlemeye dahil edilmez.
#if false
    async Task CreateAndSendTelegramReportAsync(CancellationToken token)
    {
        if (telegramReportInProgress)
            throw new InvalidOperationException("Başka bir Telegram raporu halen hazırlanıyor.");

        ScanWindows();
        int effectiveWindowNumber = telegramReportWindowNumber;
        if (effectiveWindowNumber < 1 || effectiveWindowNumber > windows.Count)
        {
            if (windows.Count == 1)
            {
                effectiveWindowNumber = 1;
                telegramReportWindowNumber = 1;
                telegramReportWindowLabel.Text = "Rapor penceresi: 1 (normal otomasyondan muaf)";
            }
            else
            {
                throw new InvalidOperationException($"{telegramReportWindowNumber} numaralı Chrome penceresi bulunamadı.");
            }
        }

        ChromeWindow reportWindow = windows[effectiveWindowNumber - 1];
        if (!GetWindowRect(reportWindow.Handle, out RECT rect))
            throw new InvalidOperationException("Rapor penceresinin konumu okunamadı.");

        reportWindow.X = rect.Left;
        reportWindow.Y = rect.Top;
        reportWindow.Width = rect.Right - rect.Left;
        reportWindow.Height = rect.Bottom - rect.Top;
        if (reportWindow.Width < 400 || reportWindow.Height < 300)
            throw new InvalidOperationException("Rapor penceresi çok küçük veya simge durumunda.");

        // Yeni site tasarımında oyun kendi tam ekran düğmesini kullanmıyor.
        // Chrome büyütülür, %75'e getirilir ve Turnuvalar içindeki Sıralamalar
        // sekmesinden ilk görünür beş sıra okunur.
        bool useNewSiteLayout = true;
        if (useNewSiteLayout)
        {
            telegramReportInProgress = true;
            try
            {
                await CreateAndSendNewSiteRankingReportAsync(
                    reportWindow, effectiveWindowNumber, token);
            }
            finally
            {
                telegramReportInProgress = false;
            }
            return;
        }

        IntPtr previousWindow = GetForegroundWindow();
        RECT fullscreenRect = default;
        bool tournamentDialogOpened = false;
        bool gameFullscreenOpened = false;
        bool zoomRaised = false;
        telegramReportInProgress = true;

        try
        {
            if (!await ActivateChromeWindowAsync(reportWindow.Handle))
                throw new InvalidOperationException("Rapor penceresi öne getirilemedi.");
            await Task.Delay(500, token);

            if (closeButtonTemplate == null || closeButtonTemplate.Empty())
                LoadTemplate();
            var errorScreen = FindCloseButton(reportWindow, (double)thresholdBox.Value);
            if (errorScreen.Found)
            {
                status.Text = $"Rapor penceresi {effectiveWindowNumber}: KAPAT hata ekranı bulundu ({errorScreen.Score:P1}); Chrome yenileniyor...";
                await ReloadChromeCurrentAddressAsync(reportWindow, token);
                await Task.Delay(TimeSpan.FromSeconds(pageReloadWaitSeconds), token);
            }
            else
            {
                status.Text = $"Rapor penceresi {effectiveWindowNumber}: hata ekranı yok; kupa adımına geçiliyor...";
            }

            // Sayfa yenilemesi giriş ekranına götürdüyse oturuma müdahale etme;
            // kullanıcı girişini bekle ve bu saatlik turu güvenle atla.
            await WaitForGameToolbarAsync(reportWindow, token);
            GameToolbar toolbar = await PrepareReportGameAsync(reportWindow, token);
            fullscreenRect = await EnterGameFullscreenAsync(reportWindow, toolbar, token);
            gameFullscreenOpened = true;

            ChangeChromeZoom(zoomIn: true);
            zoomRaised = true;
            await Task.Delay(800, token);

            using Bitmap fullscreenGame = CaptureScreenArea(
                fullscreenRect.Left,
                fullscreenRect.Top,
                fullscreenRect.Right - fullscreenRect.Left,
                fullscreenRect.Bottom - fullscreenRect.Top);
            if (!IsGameScreenReady(fullscreenGame))
                throw new InvalidOperationException("Tam ekran oyun görüntüsü hazır değil.");

            // Okuma alanlarını gerçek ekran düzeniyle doğrulamak için son rapor
            // görüntüsünü yalnızca yerel uygulama verisinde tutar.
            fullscreenGame.Save(
                AppDataPaths.GetDataFilePath("last_report_fullscreen.png"),
                System.Drawing.Imaging.ImageFormat.Png);
            decimal balance = gameReportOcrService.ReadBalance(fullscreenGame);

            int fullscreenWidth = fullscreenRect.Right - fullscreenRect.Left;
            int fullscreenHeight = fullscreenRect.Bottom - fullscreenRect.Top;
            await WaitForTournamentButtonAsync(fullscreenRect, token);
            await ClickScreenPointAsync(
                fullscreenRect.Left + (int)Math.Round(fullscreenWidth * .038),
                fullscreenRect.Top + (int)Math.Round(fullscreenHeight * .687),
                token);
            await WaitForTournamentDialogAsync(fullscreenRect, token);
            tournamentDialogOpened = true;

            await ClickScreenPointAsync(
                fullscreenRect.Left + (int)Math.Round(fullscreenWidth * .535),
                fullscreenRect.Top + (int)Math.Round(fullscreenHeight * .327),
                token);
            await Task.Delay(700, token);

            // Tam ekranda liste kaydırılmadan ilk 10 sıra sayısal olarak okunur.
            using Bitmap fullscreenRankings = CaptureScreenArea(
                fullscreenRect.Left,
                fullscreenRect.Top,
                fullscreenWidth,
                fullscreenHeight);
            fullscreenRankings.Save(
                AppDataPaths.GetDataFilePath("last_report_rankings.png"),
                System.Drawing.Imaging.ImageFormat.Png);
            IReadOnlyList<long> scores = gameReportOcrService.ReadTopTen(fullscreenRankings);
            GameReportSnapshot? previous = gameReportHistoryService.Load();
            var current = new GameReportSnapshot
            {
                CapturedAt = DateTime.Now,
                Balance = balance,
                Scores = scores.ToList()
            };

            await telegramService.SendMessageAsync(
                telegramTokenBox.Text,
                telegramChatIdBox.Text,
                BuildTelegramRankingMessage(effectiveWindowNumber, current, previous),
                token);
            gameReportHistoryService.Save(current);
        }
        finally
        {
            if (tournamentDialogOpened && IsWindow(reportWindow.Handle))
            {
                await ClickScreenPointAsync(fullscreenRect.Right - 44, fullscreenRect.Top + 33, CancellationToken.None);
            }
            if (zoomRaised)
                ChangeChromeZoom(zoomIn: false);
            if (gameFullscreenOpened && IsWindow(reportWindow.Handle))
            {
                SetForegroundWindow(reportWindow.Handle);
                SendKeys.SendWait("{ESC}");
                await Task.Delay(350);
            }
            if (previousWindow != IntPtr.Zero && IsWindow(previousWindow))
                SetForegroundWindow(previousWindow);
            telegramReportInProgress = false;
        }
    }

    async Task CreateAndSendNewSiteRankingReportAsync(
        ChromeWindow reportWindow,
        int windowNumber,
        CancellationToken token)
    {
        if (!await ActivateChromeWindowAsync(reportWindow.Handle))
            throw new InvalidOperationException("Rapor penceresi öne getirilemedi.");

        if (!GetWindowRect(reportWindow.Handle, out var rect))
            throw new InvalidOperationException("Chrome penceresinin konumu okunamadı.");
        reportWindow.X = rect.Left; reportWindow.Y = rect.Top;
        reportWindow.Width = rect.Right - rect.Left; reportWindow.Height = rect.Bottom - rect.Top;

        await ClickNewSiteFullscreenButtonAsync(reportWindow, token);
        await Task.Delay(900, token);
        if (!GetWindowRect(reportWindow.Handle, out rect))
            throw new InvalidOperationException("Tam ekran sonrası pencere konumu okunamadı.");
        reportWindow.X = rect.Left; reportWindow.Y = rect.Top;
        reportWindow.Width = rect.Right - rect.Left; reportWindow.Height = rect.Bottom - rect.Top;

        decimal balance;
        using (var balanceImage = CaptureScreenArea(
            reportWindow.X, reportWindow.Y, reportWindow.Width, reportWindow.Height))
        {
            // Tanılama için raporun kaynak görüntüsünü sakla; bu dosya yalnızca
            // kullanıcının yerel uygulama verisindedir.
            balanceImage.Save(AppDataPaths.GetDataFilePath("last_report_game.png"),
                System.Drawing.Imaging.ImageFormat.Png);
            balance = gameReportOcrService.ReadBalance(balanceImage);
        }

        status.Text = "Kupa simgesi bekleniyor...";
        await WaitForNewSiteTournamentButtonAsync(reportWindow, token);
        await ClickScreenPointAsync(
            // Yeni tam ekran görünümünde kupa, ekranın sol kenarında yer alıyor.
            reportWindow.X + (int)Math.Round(reportWindow.Width * .043),
            reportWindow.Y + (int)Math.Round(reportWindow.Height * .690), token);
        // Kupa artık küçük bir pencere açmıyor; ayrı Turnuvalar sayfasına
        // geçiş yapıyor. Sayfanın yüklenmesine zaman tanı.
        await Task.Delay(2500, token);

        status.Text = "Sıralamalar sekmesi açılıyor...";
        await ClickScreenPointAsync(
            // Turnuvalar sayfasının üst menüsündeki Sıralamalar sekmesi.
            // Sıralamalar sekmesi, Turnuvalar penceresinin orta-sağında.
            // .564 değeri Kurallar sekmesine denk geliyordu.
            reportWindow.X + (int)Math.Round(reportWindow.Width * .517),
            reportWindow.Y + (int)Math.Round(reportWindow.Height * .213), token);
        await Task.Delay(1500, token);

        using var rankingsImage = CaptureScreenArea(
            reportWindow.X, reportWindow.Y, reportWindow.Width, reportWindow.Height);
        rankingsImage.Save(AppDataPaths.GetDataFilePath("last_report_rankings.png"),
            System.Drawing.Imaging.ImageFormat.Png);
        IReadOnlyList<long> scores = gameReportOcrService.ReadNewSiteTopTen(rankingsImage);

        GameReportSnapshot? previous = gameReportHistoryService.Load();
        if (previous?.Scores.Count != scores.Count)
            previous = null;
        var current = new GameReportSnapshot
        {
            CapturedAt = DateTime.Now,
            Balance = balance,
            Scores = scores.ToList()
        };
        await telegramService.SendMessageAsync(
            telegramTokenBox.Text, telegramChatIdBox.Text,
            BuildTelegramRankingMessage(windowNumber, current, previous), token);
        gameReportHistoryService.Save(current);
    }

    async Task WaitForNewSiteTournamentButtonAsync(ChromeWindow window, CancellationToken token)
    {
        for (int attempt = 1; attempt <= 30; attempt++)
        {
            using var image = CaptureScreenArea(window.X, window.Y, window.Width, window.Height);
            int goldPixels = 0;
            // Kupa artık sol alt köşede. Eski konum (.10-.17) simgeyi
            // tamamen dışarıda bırakıyordu ve rapor akışını durduruyordu.
            int left = (int)(image.Width * .005), right = (int)(image.Width * .085);
            int top = (int)(image.Height * .62), bottom = (int)(image.Height * .76);
            for (int y = top; y < bottom; y += 3)
            for (int x = left; x < right; x += 3)
            {
                Color pixel = image.GetPixel(x, y);
                if (pixel.R > 150 && pixel.G > 90 && pixel.B < 100) goldPixels++;
            }
            if (goldPixels >= 25) return;
            await Task.Delay(1000, token);
        }
        throw new InvalidOperationException("Kupa simgesi görünmedi; tıklama yapılmadı.");
    }

    static void SendChromeCtrlShortcut(byte key)
    {
        const byte VK_CONTROL = 0x11;
        const uint KEYEVENTF_KEYUP = 0x0002;
        keybd_event(VK_CONTROL, 0, 0, UIntPtr.Zero);
        keybd_event(key, 0, 0, UIntPtr.Zero);
        keybd_event(key, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
        keybd_event(VK_CONTROL, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
    }

    static string BuildNewSiteRankingMessage(
        int windowNumber, GameReportSnapshot current, GameReportSnapshot? previous)
    {
        var message = new StringBuilder();
        message.AppendLine("Otobot Sıralama Raporu");
        message.AppendLine($"Pencere: {windowNumber} | {current.CapturedAt:dd.MM.yyyy HH:mm}");
        message.AppendLine();
        message.AppendLine(previous == null ? "Sıra | Puan" : "Sıra | Puan | Önceki rapora fark");
        for (int i = 0; i < current.Scores.Count; i++)
        {
            if (previous == null)
                message.AppendLine($"{i + 1} | {current.Scores[i]:N0}");
            else
                message.AppendLine($"{i + 1} | {current.Scores[i]:N0} | {FormatDifference(current.Scores[i] - previous.Scores[i])}");
        }
        return message.ToString().TrimEnd();
    }

    static string BuildTelegramRankingMessage(
        int windowNumber,
        GameReportSnapshot current,
        GameReportSnapshot? previous)
    {
        var message = new StringBuilder();
        message.AppendLine("OTOBOT SAATLİK RAPOR");
        message.AppendLine($"Pencere: {windowNumber} | {current.CapturedAt:dd.MM.yyyy HH:mm}");
        string balanceChange = previous == null ? "ilk kayıt" : FormatDifference(current.Balance - previous.Balance);
        message.AppendLine($"Bakiye: {current.Balance:N2} TRY ({balanceChange})");
        message.AppendLine();
        message.AppendLine("Sıra | Puan | 1 saatlik fark");
        for (int i = 0; i < current.Scores.Count; i++)
        {
            string change = previous?.Scores.Count > i
                ? FormatDifference(current.Scores[i] - previous.Scores[i])
                : "ilk kayıt";
            message.AppendLine($"{i + 1,2} | {current.Scores[i]:N0} | {change}");
        }
        return message.ToString().TrimEnd();
    }

    static string FormatDifference(decimal difference) =>
        difference > 0 ? $"+{difference:N2}" : difference.ToString("N2");

    static string FormatDifference(long difference) =>
        difference > 0 ? $"+{difference:N0}" : difference.ToString("N0");

    async Task<GameToolbar> PrepareReportGameAsync(
        ChromeWindow reportWindow,
        CancellationToken token)
    {
        if (closeButtonTemplate == null || closeButtonTemplate.Empty())
            LoadTemplate();
        if (closeButtonTemplate == null || closeButtonTemplate.Empty())
            throw new InvalidOperationException("KAPAT uyarısı görseli yüklenemedi.");

        GameToolbar toolbar = FindGameToolbar(reportWindow);
        double threshold = (double)thresholdBox.Value;
        var match = FindCloseButton(reportWindow, threshold);
        if (!match.Found) return toolbar;

        status.Text =
            $"Rapor öncesi KAPAT uyarısı bulundu ({match.Score:P1}); " +
            "oyun yenileniyor ve 30 saniye beklenecek...";

        // KAPAT düğmesine dokunma; Chrome sayfasını doğrudan yenile.
        await ClickRefreshAsync(reportWindow, token);
        await Task.Delay(TimeSpan.FromSeconds(30), token);

        var remaining = FindCloseButton(reportWindow, threshold);
        if (remaining.Found)
            throw new InvalidOperationException(
                $"Oyun yenilenip 30 saniye beklendiği halde KAPAT uyarısı devam ediyor " +
                $"({remaining.Score:P1}). Rapor bu döngüde atlandı.");

        status.Text = "Oyun yenilendi; tam ekran raporuna geçiliyor...";
        return await WaitForGameToolbarAsync(reportWindow, token);
    }

    GameToolbar FindGameToolbar(ChromeWindow window)
    {
        using Bitmap image = CaptureScreenArea(window.X, window.Y, window.Width, window.Height);
        int bestY = -1;
        int bestFirstX = 0;
        int bestLastX = 0;
        int bestCount = 0;
        int startY = Math.Min(95, image.Height - 1);
        int endY = Math.Min(image.Height - 1, 205);

        for (int y = startY; y <= endY; y++)
        {
            int count = 0;
            int firstX = -1;
            int lastX = -1;
            for (int x = 0; x < image.Width; x++)
            {
                Color pixel = image.GetPixel(x, y);
                if (pixel.R < 165 || pixel.G > 85 || pixel.B > 100) continue;
                if (firstX < 0) firstX = x;
                lastX = x;
                count++;
            }

            if (firstX < 0 || lastX - firstX < 220 || count <= bestCount) continue;
            bestY = y;
            bestFirstX = firstX;
            bestLastX = lastX;
            bestCount = count;
        }

        if (bestY < 0)
            throw new InvalidOperationException("Oyunun üst kırmızı araç çubuğu bulunamadı.");

        int top = bestY;
        while (top > startY &&
               CountToolbarRedPixels(image, top - 1, bestFirstX, bestLastX) >= bestCount * .45)
            top--;
        int bottom = bestY;
        while (bottom < endY &&
               CountToolbarRedPixels(image, bottom + 1, bestFirstX, bestLastX) >= bestCount * .45)
            bottom++;

        return new GameToolbar(
            window.X + bestFirstX,
            window.X + bestLastX,
            window.Y + top,
            window.Y + bottom);
    }

    static int CountToolbarRedPixels(Bitmap image, int y, int left, int right)
    {
        int count = 0;
        for (int x = left; x <= right; x++)
        {
            Color pixel = image.GetPixel(x, y);
            if (pixel.R >= 165 && pixel.G <= 85 && pixel.B <= 100)
                count++;
        }
        return count;
    }

    async Task<GameToolbar> WaitForGameToolbarAsync(ChromeWindow window, CancellationToken token)
    {
        for (int attempt = 0; attempt < 15; attempt++)
        {
            try { return FindGameToolbar(window); }
            catch (InvalidOperationException) when (attempt < 14)
            {
                await Task.Delay(2000, token);
            }
        }
        throw new InvalidOperationException("Oyun yenilemeden sonra 30 saniye içinde yüklenmedi.");
    }

    async Task<RECT> WaitForGameFullscreenAsync(ChromeWindow window, CancellationToken token)
    {
        for (int attempt = 0; attempt < 15; attempt++)
        {
            if (GetWindowRect(window.Handle, out RECT rect) &&
                rect.Right - rect.Left >= 1000 &&
                rect.Bottom - rect.Top >= 700)
                return rect;

            await Task.Delay(200, token);
        }
        throw new InvalidOperationException("Oyun tam ekran moduna geçemedi.");
    }

    async Task<RECT> EnterGameFullscreenAsync(
        ChromeWindow window,
        GameToolbar initialToolbar,
        CancellationToken token)
    {
        GameToolbar toolbar = initialToolbar;
        for (int attempt = 1; attempt <= 3; attempt++)
        {
            if (!await ActivateChromeWindowAsync(window.Handle))
                throw new InvalidOperationException("Rapor penceresi tam ekran için öne getirilemedi.");
            await Task.Delay(500, token);

            if (attempt > 1)
                toolbar = FindGameToolbar(window);
            status.Text = $"Rapor için tam ekran açılıyor... Deneme {attempt}/3";
            if (useVisualActions && fullscreenButtonTemplate != null)
            {
                var match = FindVisualTemplate(
                    window,
                    fullscreenButtonTemplate,
                    fullscreenTemplateDefinition,
                    .72,
                    Math.Min(RefreshSearchHeight + 80, window.Height));
                if (!match.Found)
                    throw new InvalidOperationException(
                        $"Tam ekran görseli bulunamadı. En iyi eşleşme: {match.Score:P1}. " +
                        "Ayarlar sekmesinden görseli yeniden kaydedin veya eşik değerini azaltın.");

                status.Text = $"Tam ekran görseli bulundu ({match.Score:P1}); tıklanıyor... Deneme {attempt}/3";
                await ClickScreenPointAsync(match.ScreenX, match.ScreenY, token);
            }
            else
            {
                await ClickScreenPointAsync(toolbar.Right - 36, toolbar.CenterY, token);
            }

            try
            {
                return await WaitForGameFullscreenAsync(window, token);
            }
            catch (InvalidOperationException) when (attempt < 3)
            {
                await Task.Delay(350, token);
            }
        }

        throw new InvalidOperationException("Oyun tam ekran moduna üç denemede geçemedi.");
    }

    static void ChangeChromeZoom(bool zoomIn)
    {
        string keys = zoomIn ? "^{+}" : "^-";
        for (int step = 0; step < 3; step++)
        {
            SendKeys.SendWait(keys);
            Thread.Sleep(120);
        }
    }

    static bool IsFullscreenTournamentDialogVisible(Bitmap image)
    {
        int darkPixels = 0;
        int sampledPixels = 0;
        int left = (int)(image.Width * .25);
        int right = (int)(image.Width * .75);
        int bottom = (int)(image.Height * .92);
        for (int y = 20; y < bottom; y += 4)
        {
            for (int x = left; x < right; x += 4)
            {
                Color pixel = image.GetPixel(x, y);
                sampledPixels++;
                if (pixel.R < 70 && pixel.G < 70 && pixel.B < 70)
                    darkPixels++;
            }
        }
        return sampledPixels > 0 && darkPixels / (double)sampledPixels >= .55;
    }

    static bool IsTournamentButtonVisible(Bitmap image)
    {
        // Kupa sol taraftaki oyun panelinde bulunur. Altın sarısı piksel
        // yoğunluğu, oyun henüz yüklenmemişken oluşan yanlış tıklamaları önler.
        int left = 0;
        int right = Math.Min(image.Width, (int)(image.Width * .11));
        int top = Math.Max(0, (int)(image.Height * .55));
        int bottom = Math.Min(image.Height, (int)(image.Height * .80));
        int goldPixels = 0;
        for (int y = top; y < bottom; y += 3)
        {
            for (int x = left; x < right; x += 3)
            {
                Color pixel = image.GetPixel(x, y);
                if (pixel.R > 150 && pixel.G > 90 && pixel.G < 210 && pixel.B < 90)
                    goldPixels++;
            }
        }
        return goldPixels >= 45;
    }

    async Task WaitForTournamentButtonAsync(RECT fullscreenRect, CancellationToken token)
    {
        int width = fullscreenRect.Right - fullscreenRect.Left;
        int height = fullscreenRect.Bottom - fullscreenRect.Top;
        for (int attempt = 1; attempt <= 30; attempt++)
        {
            using var image = CaptureScreenArea(fullscreenRect.Left, fullscreenRect.Top, width, height);
            if (IsTournamentButtonVisible(image))
            {
                status.Text = "Kupa simgesi görüldü; turnuva listesi açılıyor...";
                return;
            }
            status.Text = $"Kupa simgesi bekleniyor... {attempt}/30";
            await Task.Delay(1000, token);
        }
        throw new InvalidOperationException("Kupa simgesi 30 saniye içinde görünmedi; tıklama yapılmadı.");
    }

    async Task WaitForTournamentDialogAsync(RECT fullscreenRect, CancellationToken token)
    {
        int width = fullscreenRect.Right - fullscreenRect.Left;
        int height = fullscreenRect.Bottom - fullscreenRect.Top;
        for (int attempt = 1; attempt <= 15; attempt++)
        {
            using var image = CaptureScreenArea(fullscreenRect.Left, fullscreenRect.Top, width, height);
            if (IsFullscreenTournamentDialogVisible(image))
                return;
            await Task.Delay(1000, token);
        }
        throw new InvalidOperationException("Kupa tıklamasından sonra Turnuvalar penceresi açılamadı.");
    }

    static bool IsGameScreenReady(Bitmap image)
    {
        int visiblePixels = 0;
        int sampledPixels = 0;
        for (int y = 0; y < image.Height; y += 3)
        {
            for (int x = 0; x < image.Width; x += 3)
            {
                Color pixel = image.GetPixel(x, y);
                sampledPixels++;
                if (pixel.R > 28 || pixel.G > 28 || pixel.B > 28)
                    visiblePixels++;
            }
        }
        return sampledPixels > 0 && visiblePixels / (double)sampledPixels >= .18;
    }

    static async Task ClickScreenPointAsync(int x, int y, CancellationToken token)
    {
        SetCursorPos(x, y);
        await Task.Delay(100, token);
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, UIntPtr.Zero);
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);
    }

    static System.Drawing.Rectangle RelativeRectangle(
        Bitmap source,
        double x,
        double y,
        double width,
        double height) => new(
            (int)Math.Round(source.Width * x),
            (int)Math.Round(source.Height * y),
            (int)Math.Round(source.Width * width),
            (int)Math.Round(source.Height * height));

    static Bitmap CropBitmap(Bitmap source, System.Drawing.Rectangle area)
    {
        var clipped = System.Drawing.Rectangle.Intersect(
            new System.Drawing.Rectangle(0, 0, source.Width, source.Height), area);
        if (clipped.Width <= 0 || clipped.Height <= 0)
            throw new InvalidOperationException("Rapor görüntü alanı hesaplanamadı.");

        return source.Clone(clipped, source.PixelFormat);
    }

    static Bitmap BuildTelegramReportImage(Bitmap balance, Bitmap rankings)
    {
        const int padding = 20;
        const int headerHeight = 58;
        const int sectionLabelHeight = 34;
        int width = Math.Max(1000, rankings.Width + padding * 2);
        int height = headerHeight + sectionLabelHeight + balance.Height +
            sectionLabelHeight + rankings.Height + padding * 3;
        var result = new Bitmap(width, height);
        using Graphics graphics = Graphics.FromImage(result);
        graphics.Clear(Color.FromArgb(24, 24, 27));
        using var titleFont = new Font("Segoe UI", 20, FontStyle.Bold);
        using var labelFont = new Font("Segoe UI", 14, FontStyle.Bold);
        using var whiteBrush = new SolidBrush(Color.White);
        using var yellowBrush = new SolidBrush(Color.Gold);
        graphics.DrawString($"Otobot Raporu  •  {DateTime.Now:dd.MM.yyyy HH:mm:ss}", titleFont, whiteBrush, padding, 14);

        int y = headerHeight + padding;
        graphics.DrawString("KALAN BAKİYE", labelFont, yellowBrush, padding, y);
        y += sectionLabelHeight;
        graphics.DrawImage(balance, padding, y, balance.Width, balance.Height);
        y += balance.Height + padding;
        graphics.DrawString("PUAN SIRALAMASI", labelFont, yellowBrush, padding, y);
        y += sectionLabelHeight;
        graphics.DrawImageUnscaled(rankings, padding, y);
        return result;
    }

    readonly record struct GameToolbar(int Left, int Right, int Top, int Bottom)
    {
        public int CenterY => Top + Math.Max(1, Bottom - Top) / 2;
    }
#endif

    static async Task ClickScreenPointAsync(int x, int y, CancellationToken token)
    {
        SetCursorPos(x, y);
        await Task.Delay(100, token);
        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, UIntPtr.Zero);
        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);
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
                        if (useVisualActions)
                            BeginCaptureRefreshTemplate();
                        else
                            CaptureCurrentMousePosition();
                        break;

                    case VK_F9:
                        BeginCaptureAction(selectedClickNumber);
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
        if (nCode >= 0 && wParam == (IntPtr)WM_LBUTTONUP &&
            suppressActionCaptureMouseDown)
        {
            suppressActionCaptureMouseDown = false;
            return (IntPtr)1;
        }

        if (nCode >= 0 && wParam == (IntPtr)WM_LBUTTONDOWN &&
            (pendingHomeLoginTemplateCapture || pendingLoginFormCaptureStep > 0))
        {
            var data = Marshal.PtrToStructure<MSLLHOOKSTRUCT>(lParam);
            int x = data.pt.X;
            int y = data.pt.Y;
            bool captureHome = pendingHomeLoginTemplateCapture;
            int formStep = pendingLoginFormCaptureStep;
            pendingHomeLoginTemplateCapture = false;
            suppressActionCaptureMouseDown = true;

            BeginInvoke(new Action(async () =>
            {
                if (captureHome)
                    await CaptureHomeLoginTemplateAsync(x, y);
                else
                    await CaptureLoginFormStepAsync(formStep, x, y);
            }));
            return (IntPtr)1;
        }

        // Seçili moda göre ilk sol tıklamadan görsel ya da koordinat kaydı al.
        if (nCode >= 0 &&
            (pendingRefreshTemplateCapture || pendingFullscreenTemplateCapture || pendingActionCaptureNumber >= 1) &&
            wParam == (IntPtr)WM_LBUTTONDOWN)
        {
            var data = Marshal.PtrToStructure<MSLLHOOKSTRUCT>(lParam);
            int actionNumber = pendingActionCaptureNumber;
            bool captureVisual = pendingActionCaptureUsesVisual;
            bool captureRefresh = pendingRefreshTemplateCapture;
            bool captureFullscreen = pendingFullscreenTemplateCapture;
            int x = data.pt.X;
            int y = data.pt.Y;
            pendingActionCaptureNumber = 0;
            pendingRefreshTemplateCapture = false;
            pendingFullscreenTemplateCapture = false;
            suppressActionCaptureMouseDown = true;

            BeginInvoke(new Action(async () =>
            {
                if (captureRefresh)
                    await CapturePendingRefreshTemplateAsync(x, y);
                else if (captureFullscreen)
                    await CapturePendingFullscreenTemplateAsync(x, y);
                else if (captureVisual)
                    await CapturePendingActionTemplateAsync(actionNumber, x, y);
                else
                    CapturePendingActionCoordinate(actionNumber, x, y);
            }));

            // Hedef uygulamaya gerçek tıklamayı göndermiyoruz.
            return (IntPtr)1;
        }

        if (nCode >= 0 && autoCoordinateCapture &&
            (wParam == (IntPtr)WM_LBUTTONDOWN || wParam == (IntPtr)WM_LBUTTONUP))
        {
            if (wParam == (IntPtr)WM_LBUTTONDOWN)
            {
                var data = Marshal.PtrToStructure<MSLLHOOKSTRUCT>(lParam);
                int x = data.pt.X;
                int y = data.pt.Y;

                // Koordinatı her tıklamada kaydet ve gerçek tıklamanın hedefe
                // ulaşmasına izin ver. Böylece kullanıcı adımları doğal sırayla
                // ilerletebilir.
                BeginInvoke(new Action(() => CaptureAutoCoordinate(x, y)));
                return CallNextHookEx(mouseHook, nCode, wParam, lParam);
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
            decimal actionVisualThreshold = Math.Clamp(cfg.ActionTemplateThreshold, .50m, .99m);
            reloadWaitBox.Value = pageReloadWaitSeconds;
            scanIntervalBox.Value = scanIntervalSeconds;
            actionClickDelayBox.Value = actionClickDelayMs;
            actionTemplateThresholdBox.Value = actionVisualThreshold;
            useVisualActions = cfg.UseVisualActions;
            useVisualActionsCheckBox.Checked = useVisualActions;
            ApplyActionModeUi();
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
                    ActionClickDelayMs = actionClickDelayMs,
                    RefreshTemplateThreshold = .72m,
                    ActionTemplateThreshold = actionTemplateThresholdBox.Value,
                    UseVisualActions = useVisualActions
                }));
        }
        catch { }
    }

    class TimingSettings
    {
        public int ReloadWaitSeconds { get; set; } = 30;
        public int ScanIntervalSeconds { get; set; } = 60;
        public int ActionClickDelayMs { get; set; } = 500;
        public decimal RefreshTemplateThreshold { get; set; } = .72m;
        public decimal ActionTemplateThreshold { get; set; } = .65m;
        public bool UseVisualActions { get; set; } = true;
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
                ShowWarning("Önce Chrome pencerelerini tarayın.");
                return;
            }
        }

        autoCoordinateRow = 0;
        autoCaptureStep = 0;
        autoCoordinateCapture = true;
        useVisualActionsCheckBox.Enabled = false;
        autoCoordinateButton.Text = "⏹ KOORDİNAT TOPLAMAYI DURDUR";
        CaptureAutoCoordinateInstruction();
    }

    void StopAutoCoordinateCapture(string message)
    {
        autoCoordinateCapture = false;
        useVisualActionsCheckBox.Enabled = scanCts == null;
        ApplyActionModeUi();
        status.Text = message;
    }

    void CaptureAutoCoordinateInstruction()
    {
        if (!autoCoordinateCapture) return;
        if (autoCoordinateRow >= windows.Count)
        {
            StopAutoCoordinateCapture("Tüm pencerelerin 3 işlem koordinatı tamamlandı.");
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
            0 => "İŞLEM 1",
            1 => "İŞLEM 2",
            _ => "İŞLEM 3"
        };
        status.Text =
            $"Pencere {autoCoordinateRow + 1}/{windows.Count} — Mouse'u {step} noktasına götürün ve SOL TIKLAYIN.";
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
                w.Click1RX = rx; w.Click1RY = ry;
                grid.Rows[row].Cells["İşlem 1 RX"].Value = FormatRel(rx);
                grid.Rows[row].Cells["İşlem 1 RY"].Value = FormatRel(ry);
                break;
            case 1:
                w.Click2RX = rx; w.Click2RY = ry;
                grid.Rows[row].Cells["İşlem 2 RX"].Value = FormatRel(rx);
                grid.Rows[row].Cells["İşlem 2 RY"].Value = FormatRel(ry);
                break;
            case 2:
                w.Click3RX = rx; w.Click3RY = ry;
                grid.Rows[row].Cells["İşlem 3 RX"].Value = FormatRel(rx);
                grid.Rows[row].Cells["İşlem 3 RY"].Value = FormatRel(ry);
                break;
        }

        // Her tıklamayı anında kalıcılaştır. Toplama yarıda durdurulsa veya
        // uygulama kapanırsa, o ana kadar alınan koordinatlar kaybolmaz.
        SaveCoordinates();

        if (++autoCaptureStep >= 3)
        {
            autoCaptureStep = 0;
            autoCoordinateRow++;
        }

        CaptureAutoCoordinateInstruction();
    }


    void LoadTemplate()
    {
        closeButtonTemplate?.Dispose();
        string templatePath = Path.Combine(AppContext.BaseDirectory, "Assets", "close_button.png");
        if (File.Exists(templatePath))
        {
            using var bitmap = new Bitmap(templatePath);
            closeButtonTemplate = BitmapConverter.ToMat(bitmap);
        }
        else
        {
            closeButtonTemplate = LoadEmbeddedTemplate(EmbeddedTemplates.CloseButton);
        }
        if (closeButtonTemplate == null)
            ShowWarning("Gömülü KAPAT butonu şablonu yüklenemedi.");
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

    string RefreshTemplateSettingsPath =>
        AppDataPaths.GetDataFilePath("refresh_template_settings.json");

    string RefreshTemplateFilePath =>
        AppDataPaths.GetDataFilePath("refresh_button.png");

    void LoadRefreshTemplate()
    {
        refreshButtonTemplate?.Dispose();
        refreshButtonTemplate = null;
        refreshTemplateDefinition = new VisualTemplateDefinition();

        try
        {
            if (File.Exists(RefreshTemplateSettingsPath))
            {
                refreshTemplateDefinition =
                    JsonSerializer.Deserialize<VisualTemplateDefinition>(
                        File.ReadAllText(RefreshTemplateSettingsPath)) ?? new();
            }

            if (File.Exists(RefreshTemplateFilePath))
            {
                using var bitmap = new Bitmap(RefreshTemplateFilePath);
                var template = BitmapConverter.ToMat(bitmap);
                if (template.Width >= 10 && template.Height >= 10)
                    refreshButtonTemplate = template;
                else
                    template.Dispose();
            }
        }
        catch
        {
            refreshButtonTemplate?.Dispose();
            refreshButtonTemplate = null;
            refreshTemplateDefinition = new VisualTemplateDefinition();
        }

        UpdateRefreshTemplateButtonLabel();
    }

    void SaveRefreshTemplateSettings()
    {
        File.WriteAllText(
            RefreshTemplateSettingsPath,
            JsonSerializer.Serialize(
                refreshTemplateDefinition,
                new JsonSerializerOptions { WriteIndented = true }));
    }

    string FullscreenTemplateSettingsPath =>
        AppDataPaths.GetDataFilePath("fullscreen_template_settings.json");

    string FullscreenTemplateFilePath =>
        AppDataPaths.GetDataFilePath("fullscreen_button.png");

    void LoadFullscreenTemplate()
    {
        fullscreenButtonTemplate?.Dispose();
        fullscreenButtonTemplate = null;
        fullscreenTemplateDefinition = new VisualTemplateDefinition();
        try
        {
            string bundled = Path.Combine(AppContext.BaseDirectory, "Assets", "fullscreen_button.png");
            if (File.Exists(bundled))
            {
                using var bitmap = new Bitmap(bundled);
                fullscreenButtonTemplate = BitmapConverter.ToMat(bitmap);
                UpdateFullscreenTemplateButtonLabel();
                return;
            }
            if (File.Exists(FullscreenTemplateSettingsPath))
                fullscreenTemplateDefinition = JsonSerializer.Deserialize<VisualTemplateDefinition>(
                    File.ReadAllText(FullscreenTemplateSettingsPath)) ?? new();
            if (File.Exists(FullscreenTemplateFilePath))
            {
                using var bitmap = new Bitmap(FullscreenTemplateFilePath);
                var template = BitmapConverter.ToMat(bitmap);
                if (template.Width >= 10 && template.Height >= 10) fullscreenButtonTemplate = template;
                else template.Dispose();
            }
        }
        catch
        {
            fullscreenButtonTemplate?.Dispose();
            fullscreenButtonTemplate = null;
            fullscreenTemplateDefinition = new VisualTemplateDefinition();
        }
        UpdateFullscreenTemplateButtonLabel();
    }

    async Task ClickNewSiteFullscreenButtonAsync(ChromeWindow window, CancellationToken token)
    {
        if (fullscreenButtonTemplate == null) LoadFullscreenTemplate();
        if (fullscreenButtonTemplate == null)
            throw new InvalidOperationException("Tam ekran görseli yüklenemedi.");
        var match = FindVisualTemplate(window, fullscreenButtonTemplate,
            fullscreenTemplateDefinition, .70, window.Height);
        if (!match.Found)
            throw new InvalidOperationException($"Tam ekran düğmesi bulunamadı. En iyi eşleşme: {match.Score:P1}.");
        status.Text = $"Tam ekran düğmesi bulundu ({match.Score:P1}); tıklanıyor...";
        await ClickScreenPointAsync(match.ScreenX, match.ScreenY, token);
    }

    void SaveFullscreenTemplateSettings() => File.WriteAllText(
        FullscreenTemplateSettingsPath,
        JsonSerializer.Serialize(fullscreenTemplateDefinition, new JsonSerializerOptions { WriteIndented = true }));

    void UpdateFullscreenTemplateButtonLabel() =>
        captureFullscreenVisualButton.Text = fullscreenButtonTemplate == null
            ? "TAM EKRAN GÖRSELİNİ KAYDET"
            : "✓ TAM EKRAN GÖRSELİNİ DEĞİŞTİR";

    string ActionTemplateSettingsPath =>
        AppDataPaths.GetDataFilePath("action_template_settings.json");

    static string GetActionTemplateFileName(int actionNumber) =>
        $"action_button_{actionNumber}.png";

    void LoadActionTemplates()
    {
        DisposeActionTemplates();

        try
        {
            if (File.Exists(ActionTemplateSettingsPath))
            {
                var saved = JsonSerializer.Deserialize<VisualTemplateDefinition[]>(
                    File.ReadAllText(ActionTemplateSettingsPath));
                if (saved?.Length == 3)
                    actionTemplateDefinitions = saved;
            }
        }
        catch
        {
            actionTemplateDefinitions = [new(), new(), new()];
        }

        for (int i = 0; i < actionButtonTemplates.Length; i++)
        {
            string path = AppDataPaths.GetDataFilePath(GetActionTemplateFileName(i + 1));
            if (!File.Exists(path)) continue;

            try
            {
                using var bitmap = new Bitmap(path);
                var template = BitmapConverter.ToMat(bitmap);
                if (template.Width >= 10 && template.Height >= 10)
                    actionButtonTemplates[i] = template;
                else
                    template.Dispose();
            }
            catch
            {
                actionButtonTemplates[i] = null;
            }
        }

        UpdateActionTemplateButtonLabels();
    }

    void DisposeActionTemplates()
    {
        for (int i = 0; i < actionButtonTemplates.Length; i++)
        {
            actionButtonTemplates[i]?.Dispose();
            actionButtonTemplates[i] = null;
        }
    }

    void SaveActionTemplateSettings()
    {
        File.WriteAllText(
            ActionTemplateSettingsPath,
            JsonSerializer.Serialize(
                actionTemplateDefinitions,
                new JsonSerializerOptions { WriteIndented = true }));
    }

    void UpdateActionTemplateButtonLabels()
    {
        Button[] buttons = [captureClick1Button, captureClick2Button, captureClick3Button];
        for (int i = 0; i < buttons.Length; i++)
        {
            if (!useVisualActions)
            {
                buttons[i].Text = $"İşlem {i + 1} Koordinatını Kaydet";
                continue;
            }

            buttons[i].Text = actionButtonTemplates[i] == null
                ? $"İşlem {i + 1} Görselini Kaydet"
                : $"✓ İşlem {i + 1} Görselini Değiştir";
        }
    }

    void UpdateRefreshTemplateButtonLabel() { }

    void ApplyActionModeUi()
    {
        captureClickButton.Text = useVisualActions
            ? "Sıradaki İşlem Görselini Kaydet (F9)"
            : "Sıradaki İşlem Koordinatını Kaydet (F9)";
        testActionVisualsButton.Visible = useVisualActions;
        captureFullscreenVisualButton.Enabled = useVisualActions;
        testFullscreenVisualButton.Enabled = useVisualActions;
        actionTemplateThresholdBox.Enabled = useVisualActions;
        hotkeyStatus.Text = useVisualActions
            ? "F12: Başlat | F11: Durdur | F8: Yenileme | F9: İşlem görseli"
            : "F12: Başlat | F11: Durdur | F8: Yenileme | F9: İşlem koordinatı";

        foreach (string columnName in new[] {
            "Yenile RX", "Yenile RY",
            "İşlem 1 RX", "İşlem 1 RY", "İşlem 2 RX", "İşlem 2 RY",
            "İşlem 3 RX", "İşlem 3 RY"
        })
        {
            if (grid.Columns.Contains(columnName))
                grid.Columns[columnName].Visible = !useVisualActions;
        }

        if (!autoCoordinateCapture)
            autoCoordinateButton.Text = "⚡ TÜM KOORDİNATLARI TOPLA";

        UpdateFullscreenTemplateButtonLabel();
        UpdateActionTemplateButtonLabels();
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

    void BeginCaptureRefreshTemplate()
    {
        if (!useVisualActions)
        {
            ShowWarning("Yenileme görseli kaydetmek için önce GÖRSEL MODU seçim kutusunu işaretleyin.");
            return;
        }
        if (scanCts != null)
        {
            ShowWarning("Yenileme görselini kaydetmeden önce çalışan taramayı F11 ile durdurun.");
            return;
        }
        if (windows.Count == 0)
        {
            ScanWindows();
            if (windows.Count == 0)
            {
                ShowWarning("Önce en az bir Chrome penceresi açın.");
                return;
            }
        }

        pendingActionCaptureNumber = 0;
        pendingRefreshTemplateCapture = true;
        suppressActionCaptureMouseDown = false;
        status.Text =
            "Yenileme görseli bekleniyor — Chrome araç çubuğundaki yenileme " +
            "simgesinin ORTASINA sol tıklayın. Bu kayıt tıklaması Chrome'a gönderilmeyecek.";
    }

    async Task CapturePendingRefreshTemplateAsync(int screenX, int screenY)
    {
        if (!TryFindWindowAt(screenX, screenY, out var targetWindow))
        {
            ShowWarning("Tıklanan nokta taranmış bir Chrome penceresinin içinde değil.");
            status.Text = "Yenileme görseli kaydedilemedi.";
            return;
        }

        if (screenY >= targetWindow.Y + Math.Min(RefreshSearchHeight, targetWindow.Height))
        {
            ShowWarning("Yenileme simgesini Chrome penceresinin üst araç çubuğundan seçin.");
            status.Text = "Yenileme görseli kaydedilemedi.";
            return;
        }

        try
        {
            await MoveCursorAwayAndWaitAsync(targetWindow, screenX, screenY);

            int captureWidth = Math.Min(RefreshTemplateWidth, targetWindow.Width);
            int captureHeight = Math.Min(RefreshTemplateHeight, targetWindow.Height);
            int left = Math.Clamp(
                screenX - captureWidth / 2,
                targetWindow.X,
                targetWindow.X + targetWindow.Width - captureWidth);
            int top = Math.Clamp(
                screenY - captureHeight / 2,
                targetWindow.Y,
                targetWindow.Y + targetWindow.Height - captureHeight);

            using var bitmap = CaptureScreenArea(left, top, captureWidth, captureHeight);
            bitmap.Save(RefreshTemplateFilePath, System.Drawing.Imaging.ImageFormat.Png);

            refreshTemplateDefinition = new VisualTemplateDefinition
            {
                ClickOffsetX = screenX - left,
                ClickOffsetY = screenY - top
            };
            SaveRefreshTemplateSettings();
            LoadRefreshTemplate();

            status.Text =
                $"Yenileme görseli kaydedildi ({captureWidth}×{captureHeight}). " +
                "Otobot eşleşmenin kaydettiğiniz noktasına tıklayacak.";
        }
        catch (Exception ex)
        {
            ShowWarning("Yenileme görseli kaydedilemedi:\n" + ex.Message);
            status.Text = "Yenileme görseli kaydedilemedi.";
        }
    }

    void BeginCaptureFullscreenTemplate()
    {
        if (!useVisualActions)
        {
            ShowWarning("Tam ekran görseli kaydetmek için önce GÖRSEL MODU seçim kutusunu işaretleyin.");
            return;
        }
        if (scanCts != null)
        {
            ShowWarning("Tam ekran görselini kaydetmeden önce çalışan taramayı F11 ile durdurun.");
            return;
        }
        if (windows.Count == 0)
        {
            ScanWindows();
            if (windows.Count == 0)
            {
                ShowWarning("Önce en az bir Chrome penceresi açın.");
                return;
            }
        }

        pendingActionCaptureNumber = 0;
        pendingRefreshTemplateCapture = false;
        pendingFullscreenTemplateCapture = true;
        suppressActionCaptureMouseDown = false;
        status.Text = "Tam ekran görseli bekleniyor — oyun çubuğundaki tam ekran simgesinin ORTASINA sol tıklayın. Bu kayıt tıklaması oyuna gönderilmeyecek.";
    }

    async Task CapturePendingFullscreenTemplateAsync(int screenX, int screenY)
    {
        if (!TryFindWindowAt(screenX, screenY, out var targetWindow) ||
            screenY < targetWindow.Y + 75 ||
            screenY >= targetWindow.Y + Math.Min(RefreshSearchHeight + 80, targetWindow.Height))
        {
            ShowWarning("Tam ekran simgesini oyun araç çubuğundan seçin.");
            status.Text = "Tam ekran görseli kaydedilemedi.";
            return;
        }

        try
        {
            await MoveCursorAwayAndWaitAsync(targetWindow, screenX, screenY);
            int captureWidth = Math.Min(RefreshTemplateWidth, targetWindow.Width);
            int captureHeight = Math.Min(RefreshTemplateHeight, targetWindow.Height);
            int left = Math.Clamp(screenX - captureWidth / 2, targetWindow.X, targetWindow.X + targetWindow.Width - captureWidth);
            int top = Math.Clamp(screenY - captureHeight / 2, targetWindow.Y, targetWindow.Y + targetWindow.Height - captureHeight);
            using var bitmap = CaptureScreenArea(left, top, captureWidth, captureHeight);
            bitmap.Save(FullscreenTemplateFilePath, System.Drawing.Imaging.ImageFormat.Png);
            fullscreenTemplateDefinition = new VisualTemplateDefinition
            {
                ClickOffsetX = screenX - left,
                ClickOffsetY = screenY - top
            };
            SaveFullscreenTemplateSettings();
            LoadFullscreenTemplate();
            status.Text = $"Tam ekran görseli kaydedildi ({captureWidth}×{captureHeight}). Rapor bu görseli bularak tıklayacak.";
        }
        catch (Exception ex)
        {
            ShowWarning("Tam ekran görseli kaydedilemedi:\n" + ex.Message);
            status.Text = "Tam ekran görseli kaydedilemedi.";
        }
    }

    bool TryFindWindowAt(int screenX, int screenY, out ChromeWindow targetWindow)
    {
        foreach (var candidate in windows)
        {
            if (GetWindowRect(candidate.Handle, out var rect))
            {
                candidate.X = rect.Left;
                candidate.Y = rect.Top;
                candidate.Width = rect.Right - rect.Left;
                candidate.Height = rect.Bottom - rect.Top;
            }

            if (screenX >= candidate.X && screenX < candidate.X + candidate.Width &&
                screenY >= candidate.Y && screenY < candidate.Y + candidate.Height)
            {
                targetWindow = candidate;
                return true;
            }
        }

        targetWindow = null!;
        return false;
    }

    async Task MoveCursorAwayAndWaitAsync(ChromeWindow targetWindow, int targetX, int targetY)
    {
        const int margin = 12;
        var safePoints = new[]
        {
            new System.Drawing.Point(targetWindow.X + margin, targetWindow.Y + margin),
            new System.Drawing.Point(targetWindow.X + targetWindow.Width - margin, targetWindow.Y + margin),
            new System.Drawing.Point(targetWindow.X + margin, targetWindow.Y + targetWindow.Height - margin),
            new System.Drawing.Point(
                targetWindow.X + targetWindow.Width - margin,
                targetWindow.Y + targetWindow.Height - margin)
        };

        var safePoint = safePoints
            .OrderByDescending(point =>
                Math.Pow(point.X - targetX, 2) + Math.Pow(point.Y - targetY, 2))
            .First();

        SetCursorPos(safePoint.X, safePoint.Y);
        await Task.Delay(TemplateCaptureSettleMs);
    }

    void BeginCaptureAction(int actionNumber)
    {
        if (useVisualActions)
            BeginCaptureActionTemplate(actionNumber);
        else
            BeginCaptureActionCoordinate(actionNumber);
    }

    void BeginCaptureActionTemplate(int actionNumber)
    {
        if (actionNumber < 1 || actionNumber > 3) return;
        if (scanCts != null)
        {
            ShowWarning("Görsel kaydetmeden önce çalışan taramayı F11 ile durdurun.");
            return;
        }
        if (windows.Count == 0)
        {
            ScanWindows();
            if (windows.Count == 0)
            {
                ShowWarning("Önce en az bir Chrome penceresi açın.");
                return;
            }
        }

        pendingRefreshTemplateCapture = false;
        pendingActionCaptureNumber = actionNumber;
        pendingActionCaptureUsesVisual = true;
        suppressActionCaptureMouseDown = false;

        status.Text =
            $"İşlem {actionNumber} görseli bekleniyor — Chrome'daki hedef düğmenin " +
            "ORTASINA sol tıklayın. Bu kayıt tıklaması işleme gönderilmeyecek.";
    }

    void BeginCaptureActionCoordinate(int actionNumber)
    {
        if (actionNumber < 1 || actionNumber > 3) return;
        if (scanCts != null)
        {
            ShowWarning("Koordinat kaydetmeden önce çalışan taramayı F11 ile durdurun.");
            return;
        }
        if (!TryGetSelectedWindow(out _, out int index)) return;

        pendingRefreshTemplateCapture = false;
        pendingActionCaptureNumber = actionNumber;
        pendingActionCaptureUsesVisual = false;
        suppressActionCaptureMouseDown = false;
        status.Text =
            $"{index + 1}. pencere — İşlem {actionNumber} koordinatı bekleniyor. " +
            "Chrome'daki hedef noktaya sol tıklayın; kayıt tıklaması işleme gönderilmeyecek.";
    }

    void CapturePendingActionCoordinate(int actionNumber, int screenX, int screenY)
    {
        if (actionNumber < 1 || actionNumber > 3) return;
        if (!TryGetSelectedWindow(out var w, out int index)) return;

        if (GetWindowRect(w.Handle, out var rect))
        {
            w.X = rect.Left;
            w.Y = rect.Top;
            w.Width = rect.Right - rect.Left;
            w.Height = rect.Bottom - rect.Top;
        }

        if (screenX < w.X || screenX >= w.X + w.Width ||
            screenY < w.Y || screenY >= w.Y + w.Height)
        {
            ShowWarning("Tıklanan nokta seçili Chrome penceresinin içinde değil.");
            status.Text = $"İşlem {actionNumber} koordinatı kaydedilemedi.";
            return;
        }

        var (rx, ry) = ToRelative(w, screenX, screenY);
        switch (actionNumber)
        {
            case 1:
                w.Click1RX = rx; w.Click1RY = ry;
                grid.Rows[index].Cells["İşlem 1 RX"].Value = FormatRel(rx);
                grid.Rows[index].Cells["İşlem 1 RY"].Value = FormatRel(ry);
                break;
            case 2:
                w.Click2RX = rx; w.Click2RY = ry;
                grid.Rows[index].Cells["İşlem 2 RX"].Value = FormatRel(rx);
                grid.Rows[index].Cells["İşlem 2 RY"].Value = FormatRel(ry);
                break;
            case 3:
                w.Click3RX = rx; w.Click3RY = ry;
                grid.Rows[index].Cells["İşlem 3 RX"].Value = FormatRel(rx);
                grid.Rows[index].Cells["İşlem 3 RY"].Value = FormatRel(ry);
                break;
        }

        SaveCoordinates();
        selectedClickNumber = actionNumber == 3 ? 1 : actionNumber + 1;
        status.Text = $"{index + 1}. pencere — İşlem {actionNumber} koordinatı kaydedildi.";
    }

    async Task CapturePendingActionTemplateAsync(int actionNumber, int screenX, int screenY)
    {
        if (actionNumber < 1 || actionNumber > 3) return;

        if (!TryFindWindowAt(screenX, screenY, out var targetWindow))
        {
            ShowWarning("Tıklanan nokta taranmış bir Chrome penceresinin içinde değil.");
            status.Text = $"İşlem {actionNumber} görseli kaydedilemedi.";
            return;
        }

        try
        {
            await MoveCursorAwayAndWaitAsync(targetWindow, screenX, screenY);

            int captureWidth = Math.Min(ActionTemplateWidth, targetWindow.Width);
            int captureHeight = Math.Min(ActionTemplateHeight, targetWindow.Height);
            int minLeft = targetWindow.X;
            int maxLeft = targetWindow.X + targetWindow.Width - captureWidth;
            int minTop = targetWindow.Y;
            int maxTop = targetWindow.Y + targetWindow.Height - captureHeight;
            int left = Math.Clamp(screenX - captureWidth / 2, minLeft, maxLeft);
            int top = Math.Clamp(screenY - captureHeight / 2, minTop, maxTop);

            using var bitmap = CaptureScreenArea(left, top, captureWidth, captureHeight);
            string path = AppDataPaths.GetDataFilePath(GetActionTemplateFileName(actionNumber));
            bitmap.Save(path, System.Drawing.Imaging.ImageFormat.Png);

            actionTemplateDefinitions[actionNumber - 1] = new VisualTemplateDefinition
            {
                ClickOffsetX = screenX - left,
                ClickOffsetY = screenY - top
            };
            SaveActionTemplateSettings();
            LoadActionTemplates();

            selectedClickNumber = actionNumber == 3 ? 1 : actionNumber + 1;
            status.Text =
                $"İşlem {actionNumber} görseli kaydedildi ({captureWidth}×{captureHeight}). " +
                "Otobot eşleşmenin kaydettiğiniz noktasına tıklayacak.";
        }
        catch (Exception ex)
        {
            ShowWarning("İşlem görseli kaydedilemedi:\n" + ex.Message);
            status.Text = $"İşlem {actionNumber} görseli kaydedilemedi.";
        }
    }

    bool TryGetSelectedWindow(out ChromeWindow w, out int index)
    {
        index=grid.CurrentRow?.Index ?? -1;
        if(index<0 || index>=windows.Count){w=null!; ShowWarning("Önce tabloda bir pencere seçin."); return false;}
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

        // Büyütülmüş pencereyi normal boyuta geri döndürme. Yalnızca
        // simge durumundaysa geri yükleyip odaklan.
        if (IsIconic(hWnd))
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
        // Chrome'un yeni arayüzünde Ctrl+L/C ile panoya URL alınamayabiliyor.
        // Erişilebilirlik ağacındaki adres çubuğu bu durumda doğrudan değeri verir.
        try
        {
            var root = AutomationElement.FromHandle(w.Handle);
            var addressBar = root.FindFirst(
                TreeScope.Descendants,
                new PropertyCondition(AutomationElement.AutomationIdProperty, "view_1012"));
            if (addressBar != null &&
                addressBar.TryGetCurrentPattern(ValuePattern.Pattern, out var addressPattern))
            {
                string address = ((ValuePattern)addressPattern).Current.Value.Trim();
                if (!address.StartsWith("http://", StringComparison.OrdinalIgnoreCase) &&
                    !address.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                    address = "https://" + address;
                if (IsValidHttpUrl(address))
                    return address;
            }
        }
        catch { }

        IntPtr previous = GetForegroundWindow();
        try
        {
            ShowWindow(w.Handle, SW_RESTORE);
            if (!await ActivateChromeWindowAsync(w.Handle))
                return "";
            await Task.Delay(350);

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
                ShowWarning("Eski ve yeni domain alanlarını doldurun.");
                return;
            }

            if (string.Equals(oldDomain, newDomain, StringComparison.OrdinalIgnoreCase))
            {
                ShowWarning("Eski ve yeni domain aynı.");
                return;
            }

            ScanWindows();
            if (windows.Count == 0)
            {
                ShowWarning("Açık Chrome penceresi bulunamadı.");
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

            if (failed.Count > 0)
                ShowWarning(message);
            else
                ShowInfo(message);
        }
        catch (Exception ex)
        {
            ShowWarning("URL'ler güncellenemedi:\n" + ex.Message);
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
            // Kaydetme anındaki tüm Chrome pencerelerini kullan. Önceki tarama
            // listesi sonradan açılan pencereleri içermeyebilir.
            ScanWindows();
            if (windows.Count == 0)
            {
                ShowWarning("Kaydedilecek açık Chrome penceresi bulunamadı.");
                return;
            }

            saveSessionButton.Enabled = false;
            var records = new List<SessionRecord>();

            for (int i = 0; i < windows.Count; i++)
            {
                var w = windows[i];
                status.Text = $"Pencere {i + 1}/{windows.Count} URL kaydediliyor...";
                string url = string.Empty;
                for (int attempt = 0; attempt < 3 && !IsValidHttpUrl(url); attempt++)
                {
                    url = (await GetChromeUrlAsync(w)).Trim();
                    if (!IsValidHttpUrl(url)) await Task.Delay(300);
                }
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

            int unreadUrlCount = records.Count(record => string.IsNullOrWhiteSpace(record.Url));
            if (unreadUrlCount == 0)
                SaveUrlsToUrlList(records);
            else
                ShowWarning(
                    $"{unreadUrlCount} Chrome penceresinin URL'si okunamadı; eksik liste kaydedilmedi. " +
                    "Pencereleri Kaydet düğmesine tekrar basın.");

            // Koordinatları da ayrıca güncel tut.
            SaveCoordinates();
            status.Text = unreadUrlCount == 0
                ? $"{records.Count} Chrome penceresi ve URL Listesi kaydedildi."
                : $"{records.Count - unreadUrlCount}/{records.Count} Chrome URL'si kaydedildi.";
        }
        catch (Exception ex)
        {
            ShowWarning("Oturum kaydedilemedi:\n" + ex.Message);
        }
        finally
        {
            saveSessionButton.Enabled = true;
        }
    }

    void SaveUrlsToUrlList(IEnumerable<SessionRecord> records)
    {
        var urls = records
            .Select(record => Uri.TryCreate(record.Url, UriKind.Absolute, out Uri? uri) ? uri : null)
            .Where(uri => uri != null && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
            .Cast<Uri>()
            .ToList();
        if (urls.Count == 0) return;

        string baseAddress = urls
            .GroupBy(uri => uri.GetLeftPart(UriPartial.Authority), StringComparer.OrdinalIgnoreCase)
            .OrderByDescending(group => group.Count())
            .ThenBy(group => group.Key, StringComparer.OrdinalIgnoreCase)
            .First()
            .Key;
        List<string> remainders = urls
            .Where(uri => string.Equals(uri.GetLeftPart(UriPartial.Authority), baseAddress, StringComparison.OrdinalIgnoreCase))
            .Select(uri => uri.PathAndQuery + uri.Fragment)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();
        if (remainders.Count == 0) return;

        var settings = new UrlListSettings
        {
            BaseAddress = baseAddress,
            Remainders = remainders
        };
        urlListService.Save(settings);

        urlListLoading = true;
        try { ApplyUrlList(urlListService.Load()); }
        finally { urlListLoading = false; }
        RefreshUrlListPreviews();
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
            ShowWarning("Oturum dosyası okunamadı:\n" + ex.Message);
            return new List<SessionRecord>();
        }
    }

    async Task RestoreSelectedSessionAsync()
    {
        var records = LoadSessionRecords();
        int index = grid.CurrentRow?.Index ?? -1;
        if (index < 0 || index >= records.Count)
        {
            ShowWarning("Önce geri yüklemek istediğiniz kayıtlı pencereyi tabloda seçin.");
            return;
        }

        var record = records[index];
        if (string.IsNullOrWhiteSpace(record.Url))
        {
            ShowWarning("Bu pencerenin kayıtlı URL'si boş. Pencereyi tekrar açıp Pencereleri Kaydet yapın.");
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
            ShowWarning("Kayıtlı oturum bulunamadı. Önce PENCERELERİ KAYDET butonuna basın.");
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
        ArrangeRestoredWindowsInGrid(records.Count);
        status.Text = restored == 0
            ? "Kayıtlı Chrome pencereleri ızgara düzenine yerleştirildi."
            : $"{restored} eksik kayıtlı pencere geri yüklendi ve ızgara düzenine yerleştirildi.";
    }

    void ArrangeRestoredWindowsInGrid(int savedWindowCount)
    {
        int count = Math.Min(savedWindowCount, windows.Count);
        if (count == 0) return;

        const int columns = 4;
        int rows = (int)Math.Ceiling(count / (double)columns);
        var area = Screen.PrimaryScreen?.WorkingArea ?? new Rectangle(0, 0, 1920, 1080);
        const int gap = 4;
        int cellWidth = Math.Max(260, (area.Width - gap * (columns - 1)) / columns);
        int cellHeight = Math.Max(220, (area.Height - gap * (rows - 1)) / rows);

        for (int i = 0; i < count; i++)
        {
            int column = i % columns;
            int row = i / columns;
            int x = area.Left + column * (cellWidth + gap);
            int y = area.Top + row * (cellHeight + gap);
            IntPtr handle = windows[i].Handle;
            if (!IsWindow(handle)) continue;
            ShowWindow(handle, SW_RESTORE);
            MoveWindow(handle, x, y, cellWidth, cellHeight, true);
        }
    }

    async Task RestoreSessionRecordAsync(SessionRecord record)
    {
        string? chromeExe = FindChromeExe();
        if (chromeExe == null)
        {
            ShowWarning("Chrome.exe bulunamadı.");
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
            ShowWarning($"Pencere {record.WindowNo} açılamadı veya yeni Chrome penceresi bulunamadı.");
            return;
        }

        ShowWindow(newHandle, SW_RESTORE);
        MoveWindow(newHandle, record.X, record.Y, record.Width, record.Height, true);
        SetForegroundWindow(newHandle);
        await Task.Delay(500);
    }

    async Task OpenChromeWindowForUrlAsync(string url)
    {
        string? chromeExe = FindChromeExe();
        if (chromeExe == null)
            throw new InvalidOperationException("Chrome.exe bulunamadı.");

        var before = GetChromeWindowHandles();
        Process.Start(new ProcessStartInfo
        {
            FileName = chromeExe,
            Arguments = $"--new-window \"{url.Replace("\\\"", "\\\\\"")}\"",
            UseShellExecute = true
        });

        for (int attempt = 0; attempt < 60; attempt++)
        {
            await Task.Delay(250);
            if (GetChromeWindowHandles().Any(handle => !before.Contains(handle)))
                return;
        }

        throw new InvalidOperationException("Eksik Chrome penceresi açılamadı.");
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
        catch(Exception ex){ShowWarning("Koordinatlar kaydedilemedi:\n"+ex.Message);}
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
        for (int i = 0; i < windows.Count; i++)
        {
            var r = FindError(windows[i], threshold);
            if (r.Found) errors++;
            UpdateRow(i, r.Found ? "HATA BULUNDU" : "Normal", r.Score, r.Found ? Color.MistyRose : Color.Honeydew);
        }
        status.Text = $"Tarama tamamlandı. {errors} pencerede hata bulundu.";
    }

    (bool Found, double Score) FindError(ChromeWindow w, double threshold)
    {
        var match = FindCloseButton(w, threshold);
        return (match.Found, match.Score);
    }

    (bool Found, double Score, int ScreenX, int ScreenY) FindCloseButton(
        ChromeWindow w,
        double threshold)
    {
        double bestScore = 0;
        int bestScreenX = 0;
        int bestScreenY = 0;
        try
        {
            if (closeButtonTemplate == null || closeButtonTemplate.Empty())
                return (false, 0, 0, 0);

            using var bmp = CaptureScreenArea(w.X, w.Y, w.Width, w.Height);
            using var screenColor = BitmapConverter.ToMat(bmp);
            using var screen = new Mat();
            ConvertToGray(screenColor, screen);

            using var templateGray = new Mat();
            ConvertToGray(closeButtonTemplate, templateGray);

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
                Cv2.MinMaxLoc(result, out _, out double maxValue, out _, out OpenCvSharp.Point location);
                if (maxValue <= bestScore) continue;

                bestScore = maxValue;
                bestScreenX = w.X + location.X + tw / 2;
                bestScreenY = w.Y + location.Y + th / 2;
            }
        }
        catch
        {
            bestScore = 0;
            bestScreenX = 0;
            bestScreenY = 0;
        }

        return (bestScore >= threshold, bestScore, bestScreenX, bestScreenY);
    }

    void UpdateRow(int index, string state, double score, Color color)
    {
        if (index < 0 || index >= windows.Count) return; if (grid.Rows.Count != windows.Count) RebuildGrid("Taranmadı", "-");
        grid.Rows[index].Cells["Hata Durumu"].Value = state; grid.Rows[index].Cells["Eşleşme"].Value = score == 0 ? "-" : $"{score:P1}"; grid.Rows[index].DefaultCellStyle.BackColor = color; grid.Refresh();
    }

    async Task ClickRefreshAsync(ChromeWindow w, CancellationToken token)
    {
        await ReloadChromeCurrentAddressAsync(w, token);
        status.Text = "Chrome'un Yeniden Yükle düğmesiyle sayfa yenileniyor...";
    }

    async Task ReloadChromeCurrentAddressAsync(ChromeWindow w, CancellationToken token)
    {
        if (!IsWindow(w.Handle))
            throw new InvalidOperationException("Chrome penceresi artık açık değil.");
        ShowWindow(w.Handle, SW_RESTORE);
        if (!await ActivateChromeWindowAsync(w.Handle))
            throw new InvalidOperationException("Chrome penceresi öne getirilemedi.");
        await Task.Delay(150, token);

        // Adres çubuğundaki mevcut adresi yeniden çalıştır. Bu, oyun alanının
        // Ctrl+R/F5 kısayolunu veya normal yenileme düğmesini etkisiz kıldığı
        // durumlarda da tam sayfa yüklemesi yapar.
        try
        {
            var root = AutomationElement.FromHandle(w.Handle);
            var addressBar = root.FindFirst(
                TreeScope.Descendants,
                new PropertyCondition(AutomationElement.AutomationIdProperty, "view_1012"));
            if (addressBar != null &&
                addressBar.TryGetCurrentPattern(ValuePattern.Pattern, out var valuePattern))
            {
                string currentAddress = ((ValuePattern)valuePattern).Current.Value;
                addressBar.SetFocus();
                ((ValuePattern)valuePattern).SetValue(currentAddress);
                keybd_event(0x0D, 0, 0, UIntPtr.Zero);
                keybd_event(0x0D, 0, 0x0002, UIntPtr.Zero);
                return;
            }
        }
        catch { }

        // Chrome arayüzü erişilemezse son çare olarak adres çubuğunu yeniden aç.
        SendKeys.SendWait("^l");
        await Task.Delay(100, token);
        SendKeys.SendWait("{ENTER}");
    }

    bool EnsureRefreshMethodReady()
    {
        return true;
    }

    bool ActionTemplatesReady() => actionButtonTemplates.All(template => template != null);

    bool ActionCoordinatesReady() => windows.Select((w, index) => new { w, index })
        .All(item =>
            item.w.Click1RX.HasValue && item.w.Click1RY.HasValue &&
            item.w.Click2RX.HasValue && item.w.Click2RY.HasValue &&
            item.w.Click3RX.HasValue && item.w.Click3RY.HasValue);

    async Task TestFullscreenTemplateAsync()
    {
        if (!useVisualActions)
        {
            ShowWarning("Görsel testi için önce GÖRSEL MODU seçim kutusunu işaretleyin.");
            return;
        }
        if (fullscreenButtonTemplate == null)
        {
            ShowWarning("Önce tam ekran görselini kaydedin.");
            return;
        }
        if (!TryGetSelectedWindow(out var w, out _)) return;

        testFullscreenVisualButton.Enabled = false;
        try
        {
            if (!GetWindowRect(w.Handle, out var rect))
                throw new InvalidOperationException("Chrome penceresinin konumu okunamadı.");
            w.X = rect.Left; w.Y = rect.Top;
            w.Width = rect.Right - rect.Left; w.Height = rect.Bottom - rect.Top;
            var match = FindVisualTemplate(w, fullscreenButtonTemplate,
                fullscreenTemplateDefinition, .72,
                Math.Min(RefreshSearchHeight + 80, w.Height));
            ShowInfo("Tam ekran görseli testi — " +
                $"{(match.Found ? "BULUNDU" : "bulunamadı")} — {match.Score:P1}" +
                " | Eşik: %72" +
                " | Test sırasında tıklama yapılmadı.");
        }
        catch (Exception ex)
        {
            ShowWarning("Tam ekran görseli test edilemedi:\n" + ex.Message);
        }
        finally { testFullscreenVisualButton.Enabled = true; }
    }

    async Task TestActionTemplatesAsync()
    {
        if (!useVisualActions)
        {
            ShowWarning("Görsel testi için önce GÖRSEL MODU seçim kutusunu işaretleyin.");
            return;
        }
        if (!ActionTemplatesReady())
        {
            var missing = Enumerable.Range(1, 3)
                .Where(number => actionButtonTemplates[number - 1] == null);
            ShowWarning(
                "Önce eksik işlem görsellerini kaydedin: " + string.Join(", ", missing));
            return;
        }
        if (!TryGetSelectedWindow(out var w, out _)) return;

        testActionVisualsButton.Enabled = false;
        try
        {
            ShowWindow(w.Handle, SW_RESTORE);
            if (!GetWindowRect(w.Handle, out var rect))
                throw new InvalidOperationException("Chrome penceresinin konumu okunamadı.");

            w.X = rect.Left;
            w.Y = rect.Top;
            w.Width = rect.Right - rect.Left;
            w.Height = rect.Bottom - rect.Top;

            if (!await ActivateChromeWindowAsync(w.Handle))
                throw new InvalidOperationException("Chrome penceresi öne getirilemedi.");

            await Task.Delay(200);
            var results = new List<string>();
            for (int i = 0; i < actionButtonTemplates.Length; i++)
            {
                var match = FindVisualTemplate(
                    w,
                    actionButtonTemplates[i]!,
                    actionTemplateDefinitions[i],
                    (double)actionTemplateThresholdBox.Value);
                results.Add(
                    $"İşlem {i + 1}: {(match.Found ? "BULUNDU" : "bulunamadı")} — {match.Score:P1}");
            }

            ShowInfo(
                "İşlem görselleri testi — " + string.Join(" | ", results) +
                $" | Eşik: {actionTemplateThresholdBox.Value:P0}" +
                " | Test sırasında tıklama yapılmadı.");
        }
        catch (Exception ex)
        {
            ShowWarning("Görseller test edilemedi:\n" + ex.Message);
        }
        finally
        {
            testActionVisualsButton.Enabled = true;
        }
    }

    void StartContinuousScan()
    {
        if (scanCts != null) { status.Text = "Zaten çalışıyor. F11 ile durdurun."; return; }
        ScanWindows(); if (windows.Count == 0) { ShowWarning("Chrome penceresi bulunamadı."); return; }
        if (!EnsureRefreshMethodReady()) return;
        if (useVisualActions && !ActionTemplatesReady())
        {
            var missing = Enumerable.Range(1, 3)
                .Where(number => actionButtonTemplates[number - 1] == null);
            ShowWarning(
                "Önce eksik işlem görsellerini kaydedin: " + string.Join(", ", missing));
            return;
        }
        if (!useVisualActions && !ActionCoordinatesReady())
        {
            var missing = windows.Select((w, index) => new { w, index })
                .Where(item => !item.w.Click1RX.HasValue || !item.w.Click1RY.HasValue ||
                    !item.w.Click2RX.HasValue || !item.w.Click2RY.HasValue ||
                    !item.w.Click3RX.HasValue || !item.w.Click3RY.HasValue)
                .Select(item => item.index + 1);
            ShowWarning(
                "Üç işlem koordinatı eksik olan pencereler: " + string.Join(", ", missing));
            return;
        }

        useVisualActionsCheckBox.Enabled = false;
        scanCts = new CancellationTokenSource();
        hotkeyStatus.Text = useVisualActions
            ? "ÇALIŞIYOR — GÖRSEL MODU — F11: Durdur"
            : "ÇALIŞIYOR — KOORDİNAT MODU — F11: Durdur";
        _ = ContinuousScanLoopAsync(scanCts.Token);
    }

    void StopContinuousScan()
    {
        if (scanCts == null) { status.Text = "Çalışan tarama yok."; return; } scanCts.Cancel(); status.Text = "Durdurma istendi...";
    }

    async Task PerformVisualActionsAsync(ChromeWindow w, CancellationToken token)
    {
        for (int i = 0; i < actionButtonTemplates.Length; i++)
        {
            token.ThrowIfCancellationRequested();
            var template = actionButtonTemplates[i]
                ?? throw new InvalidOperationException($"İşlem {i + 1} görseli eksik.");

            if (!IsWindow(w.Handle))
                throw new InvalidOperationException("Chrome penceresi artık açık değil.");

            ShowWindow(w.Handle, SW_RESTORE);
            if (!GetWindowRect(w.Handle, out var rect))
                throw new InvalidOperationException("Chrome penceresinin konumu okunamadı.");

            w.X = rect.Left;
            w.Y = rect.Top;
            w.Width = rect.Right - rect.Left;
            w.Height = rect.Bottom - rect.Top;

            if (!await ActivateChromeWindowAsync(w.Handle))
                throw new InvalidOperationException("Chrome penceresi öne getirilemedi.");

            await Task.Delay(150, token);

            var match = FindVisualTemplate(
                w,
                template,
                actionTemplateDefinitions[i],
                (double)actionTemplateThresholdBox.Value);

            if (!match.Found)
            {
                throw new InvalidOperationException(
                    $"İşlem {i + 1} görseli bulunamadı. En iyi eşleşme: {match.Score:P1}. " +
                    "Görseli yeniden kaydedin veya görsel eşik değerini kontrollü biçimde azaltın.");
            }

            status.Text =
                $"İşlem {i + 1} görseli bulundu ({match.Score:P1}); düğmeye tıklanıyor...";
            SetCursorPos(match.ScreenX, match.ScreenY);
            await Task.Delay(100, token);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, UIntPtr.Zero);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);
            await Task.Delay(actionClickDelayMs, token);
        }
    }

    async Task PerformCoordinateActionsAsync(ChromeWindow w, CancellationToken token)
    {
        var points = new (double? X, double? Y)[]
        {
            (w.Click1RX, w.Click1RY),
            (w.Click2RX, w.Click2RY),
            (w.Click3RX, w.Click3RY)
        };

        if (!IsWindow(w.Handle))
            throw new InvalidOperationException("Chrome penceresi artık açık değil.");

        ShowWindow(w.Handle, SW_RESTORE);
        if (!GetWindowRect(w.Handle, out var rect))
            throw new InvalidOperationException("Chrome penceresinin konumu okunamadı.");

        w.X = rect.Left;
        w.Y = rect.Top;
        w.Width = rect.Right - rect.Left;
        w.Height = rect.Bottom - rect.Top;

        if (!await ActivateChromeWindowAsync(w.Handle))
            throw new InvalidOperationException("Chrome penceresi öne getirilemedi.");

        await Task.Delay(150, token);
        for (int i = 0; i < points.Length; i++)
        {
            token.ThrowIfCancellationRequested();
            if (!points[i].X.HasValue || !points[i].Y.HasValue)
                throw new InvalidOperationException($"İşlem {i + 1} koordinatı eksik.");

            var point = ToScreenPoint(w, points[i].X!.Value, points[i].Y!.Value);
            if (point.X < w.X || point.X >= w.X + w.Width ||
                point.Y < w.Y || point.Y >= w.Y + w.Height)
            {
                throw new InvalidOperationException(
                    $"İşlem {i + 1} koordinatı pencere dışında hesaplandı.");
            }

            status.Text = $"İşlem {i + 1} koordinatına tıklanıyor...";
            SetCursorPos(point.X, point.Y);
            await Task.Delay(100, token);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, UIntPtr.Zero);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);
            await Task.Delay(actionClickDelayMs, token);
        }
    }

    (bool Found, double Score, int ScreenX, int ScreenY) FindVisualTemplate(
        ChromeWindow w,
        Mat template,
        VisualTemplateDefinition definition,
        double threshold,
        int? maximumSearchHeight = null)
    {
        double bestScore = 0;
        int bestScreenX = 0;
        int bestScreenY = 0;

        int searchHeight = Math.Clamp(maximumSearchHeight ?? w.Height, 1, w.Height);
        using var bitmap = CaptureScreenArea(w.X, w.Y, w.Width, searchHeight);
        using var screenColor = BitmapConverter.ToMat(bitmap);
        using var screenGray = new Mat();
        ConvertToGray(screenColor, screenGray);
        using var screenPrepared = new Mat();
        Cv2.GaussianBlur(screenGray, screenPrepared, new OpenCvSharp.Size(3, 3), 0);
        using var screenEdges = new Mat();
        Cv2.Canny(screenPrepared, screenEdges, 40, 120);

        using var templateGray = new Mat();
        ConvertToGray(template, templateGray);
        using var templatePrepared = new Mat();
        Cv2.GaussianBlur(templateGray, templatePrepared, new OpenCvSharp.Size(3, 3), 0);

        foreach (double scale in new[] { .67, .75, .82, .88, .94, 1.00, 1.06, 1.12, 1.20, 1.30, 1.40 })
        {
            int width = Math.Max(10, (int)Math.Round(templatePrepared.Width * scale));
            int height = Math.Max(10, (int)Math.Round(templatePrepared.Height * scale));
            if (width >= screenPrepared.Width || height >= screenPrepared.Height) continue;

            using var scaled = new Mat();
            Cv2.Resize(templatePrepared, scaled, new OpenCvSharp.Size(width, height),
                0, 0, InterpolationFlags.Linear);
            using var grayResult = new Mat();
            Cv2.MatchTemplate(screenPrepared, scaled, grayResult, TemplateMatchModes.CCoeffNormed);

            using var scaledEdges = new Mat();
            Cv2.Canny(scaled, scaledEdges, 40, 120);
            using var combinedResult = new Mat();

            if (Cv2.CountNonZero(scaledEdges) >= 12)
            {
                using var edgeResult = new Mat();
                Cv2.MatchTemplate(screenEdges, scaledEdges, edgeResult, TemplateMatchModes.CCoeffNormed);
                Cv2.AddWeighted(grayResult, .72, edgeResult, .28, 0, combinedResult);
            }
            else
            {
                grayResult.CopyTo(combinedResult);
            }

            Cv2.MinMaxLoc(
                combinedResult,
                out _,
                out double score,
                out _,
                out OpenCvSharp.Point location);

            if (score <= bestScore) continue;

            int originalAnchorX = definition.ClickOffsetX > 0
                ? definition.ClickOffsetX
                : templateGray.Width / 2;
            int originalAnchorY = definition.ClickOffsetY > 0
                ? definition.ClickOffsetY
                : templateGray.Height / 2;

            bestScore = score;
            bestScreenX = w.X + location.X + (int)Math.Round(originalAnchorX * scale);
            bestScreenY = w.Y + location.Y + (int)Math.Round(originalAnchorY * scale);
        }

        bool insideWindow =
            bestScreenX >= w.X && bestScreenX < w.X + w.Width &&
            bestScreenY >= w.Y && bestScreenY < w.Y + w.Height;

        return (bestScore >= threshold && insideWindow, bestScore, bestScreenX, bestScreenY);
    }

    static void ConvertToGray(Mat source, Mat destination)
    {
        switch (source.Channels())
        {
            case 1:
                source.CopyTo(destination);
                break;
            case 4:
                Cv2.CvtColor(source, destination, ColorConversionCodes.BGRA2GRAY);
                break;
            default:
                Cv2.CvtColor(source, destination, ColorConversionCodes.BGR2GRAY);
                break;
        }
    }

    async Task ContinuousScanLoopAsync(CancellationToken token)
    {
        try
        {
            while (true)
            {
                token.ThrowIfCancellationRequested();

                try
                {

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

                        if (useVisualActions)
                        {
                            status.Text = $"Pencere {i + 1}: 3 işlem görseli aranıyor...";
                            await PerformVisualActionsAsync(w, token);
                            UpdateRow(i, "GÖRSEL İŞLEMLER YAPILDI", 0, Color.Honeydew);
                        }
                        else
                        {
                            status.Text = $"Pencere {i + 1}: 3 işlem koordinatı uygulanıyor...";
                            await PerformCoordinateActionsAsync(w, token);
                            UpdateRow(i, "KOORDİNAT İŞLEMLERİ YAPILDI", 0, Color.Honeydew);
                        }
                    }
                }
                else
                {
                    status.Text = $"Hata bulunmadı. {scanIntervalSeconds} saniye bekleniyor...";
                }

                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    status.Text =
                        $"Tarama hatası: {ex.Message} — {scanIntervalSeconds} saniye sonra yeniden denenecek.";
                    ShowWarning(
                        "Tarama sırasında hata oluştu; tarama durdurulmadı ve sonraki döngüde yeniden denenecek:\n" +
                        ex.Message);
                }

                // 4) Bir sonraki taramadan önce sistemin toparlanması için 60 sn.
                await Task.Delay(scanIntervalSeconds * 1000, token);
            }
        }
        catch (OperationCanceledException)
        {
            status.Text = "Tarama F11 ile durduruldu.";
        }
        finally
        {
            scanCts?.Dispose();
            scanCts = null;
            useVisualActionsCheckBox.Enabled = true;
            ApplyActionModeUi();
        }
    }

    async Task RefreshAllPagesAsync()
    {
        try
        {
            ScanWindows(); if (windows.Count == 0 || !EnsureRefreshMethodReady()) return;
            for (int i = 0; i < windows.Count; i++)
            {
                await ClickRefreshAsync(windows[i], CancellationToken.None);
                UpdateRow(i, "YENİLENİYOR...", 0, Color.Khaki);
                await Task.Delay(300);
            }
            status.Text = $"Tüm sayfalar yenilendi. {pageReloadWaitSeconds} saniye bekleniyor..."; await Task.Delay(pageReloadWaitSeconds * 1000); DetectErrors();
        }
        catch (Exception ex) { ShowWarning("Yenileme hatası:\n" + ex.Message); }
    }

    async Task RefreshDetectedErrorsAsync()
    {
        try
        {
            if (closeButtonTemplate == null) { LoadTemplate(); if (closeButtonTemplate == null) return; }
            ScanWindows(); if (windows.Count == 0 || !EnsureRefreshMethodReady()) return;
            double threshold = (double)thresholdBox.Value; var errors = new List<int>();
            for (int i = 0; i < windows.Count; i++)
            {
                var r = FindError(windows[i], threshold);
                UpdateRow(i, r.Found ? "HATA BULUNDU" : "Normal", r.Score, r.Found ? Color.MistyRose : Color.Honeydew);
                if (r.Found) errors.Add(i);
            }
            foreach (int i in errors) { await ClickRefreshAsync(windows[i], CancellationToken.None); UpdateRow(i, "YENİLENİYOR...", 0, Color.Khaki); await Task.Delay(300); }
            await Task.Delay(pageReloadWaitSeconds * 1000); DetectErrors();
        }
        catch (Exception ex) { ShowWarning("Yenileme hatası:\n" + ex.Message); }
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

    class VisualTemplateDefinition
    {
        public int ClickOffsetX { get; set; }
        public int ClickOffsetY { get; set; }
    }

    sealed class LoginFormTemplateDefinition : VisualTemplateDefinition
    {
        public int UserNameOffsetX { get; set; }
        public int UserNameOffsetY { get; set; }
        public int PasswordOffsetX { get; set; }
        public int PasswordOffsetY { get; set; }
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
