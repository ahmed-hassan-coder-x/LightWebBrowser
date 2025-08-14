private void InitializeComponent()
{
    this.components = new System.ComponentModel.Container();

    // ===== ألوان وثوابت تصميم =====
    System.Drawing.Color ColBg        = System.Drawing.Color.FromArgb(36, 39, 46);   // خلفية النافذة
    System.Drawing.Color ColTopBar    = System.Drawing.Color.FromArgb(45, 48, 56);   // شريط علوي
    System.Drawing.Color ColInput     = System.Drawing.Color.FromArgb(58, 62, 71);   // حقل العنوان
    System.Drawing.Color ColText      = System.Drawing.Color.FromArgb(228, 231, 237);
    System.Drawing.Color ColSubtle    = System.Drawing.Color.FromArgb(153, 158, 167);
    System.Drawing.Color ColAccent    = System.Drawing.Color.FromArgb(76, 147, 255); // حدّ خفيف أو فوكس

    this.txtUrl = new System.Windows.Forms.TextBox();
    this.btnGo = new System.Windows.Forms.Button();
    this.btnBack = new System.Windows.Forms.Button();
    this.btnForward = new System.Windows.Forms.Button();
    this.btnRefresh = new System.Windows.Forms.Button();
    this.btnStop = new System.Windows.Forms.Button();
    this.btnHome = new System.Windows.Forms.Button();
    this.btnSettings = new System.Windows.Forms.Button();
    this.progressBar1 = new System.Windows.Forms.ProgressBar();

    this.toolStripTop = new System.Windows.Forms.ToolStrip();
    this.btnNewTab = new System.Windows.Forms.ToolStripButton();
    this.btnCloseTab = new System.Windows.Forms.ToolStripButton();
    this.btnIncognito = new System.Windows.Forms.ToolStripButton();
    this.btnDarkMode = new System.Windows.Forms.ToolStripButton();
    this.comboSearchEngine = new System.Windows.Forms.ToolStripComboBox();
    this.btnHistory = new System.Windows.Forms.ToolStripButton();
    this.btnScreenshot = new System.Windows.Forms.ToolStripButton();
    this.btnTranslate = new System.Windows.Forms.ToolStripButton();
    this.btnFind = new System.Windows.Forms.ToolStripButton();

    this.toolStripBookmarks = new System.Windows.Forms.ToolStrip();
    this.tabControl = new System.Windows.Forms.TabControl();
    this.splitContainerMain = new System.Windows.Forms.SplitContainer();
    this.listViewDownloads = new System.Windows.Forms.ListView();

    // عناصر إضافية للتخطيط
    var topTabsBar = new System.Windows.Forms.Panel();         // شريط التبويبات
    var topControls = new System.Windows.Forms.Panel();        // سطر الأزرار + حقل العنوان
    var layoutTop = new System.Windows.Forms.TableLayoutPanel(); // لضم الشريطين فوق بعض
    var panelNav = new System.Windows.Forms.FlowLayoutPanel(); // يسار: أزرار تنقل
    var panelRight = new System.Windows.Forms.Panel();         // يمين: ToolStrip (إعدادات وخلافه)

    // ========= إعدادات الفورم العامة =========
    this.SuspendLayout();
    ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
    this.splitContainerMain.Panel1.SuspendLayout();
    this.splitContainerMain.Panel2.SuspendLayout();
    this.splitContainerMain.SuspendLayout();

    this.BackColor = ColBg;
    this.ForeColor = ColText;
    this.Font = new System.Drawing.Font("Segoe UI", 9F);
    this.ClientSize = new System.Drawing.Size(1080, 680);
    this.MinimumSize = new System.Drawing.Size(900, 560);
    this.Name = "Form1";
    this.Text = "Light Web Browser";
    this.Load += new System.EventHandler(this.Form1_Load);
    this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);

    // ========= شريط التبويبات (أعلى) =========
    topTabsBar.Dock = System.Windows.Forms.DockStyle.Top;
    topTabsBar.Height = 36;
    topTabsBar.BackColor = ColTopBar;

    // سنستخدم TabControl مباشرة تحت شريط العنوان، لكن نعرض عنوان التبويب كالمعتاد.
    // لو تحب مظهر تبويبات كروم الحقيقي: فعّل OwnerDrawFixed وارسمها بنفسك لاحقًا.
    this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
    this.tabControl.Appearance = System.Windows.Forms.TabAppearance.Normal;
    this.tabControl.Padding = new System.Drawing.Point(20, 6);
    this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
    this.tabControl.ItemSize = new System.Drawing.Size(150, 28);
    this.tabControl.Font = new System.Drawing.Font("Segoe UI", 9F);

    // ========= تخطيط الشريط العلوي (أزرار تنقل + عنوان + إجراءات يمين) =========
    layoutTop.Dock = System.Windows.Forms.DockStyle.Top;
    layoutTop.BackColor = ColTopBar;
    layoutTop.RowCount = 1;
    layoutTop.ColumnCount = 3;
    layoutTop.Height = 44;
    layoutTop.Margin = new System.Windows.Forms.Padding(0);
    layoutTop.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
    layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize)); // يسار
    layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F)); // وسط (العنوان)
    layoutTop.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.AutoSize)); // يمين

    // ====== يسار: أزرار التنقل (MDL2) ======
    panelNav.AutoSize = true;
    panelNav.WrapContents = false;
    panelNav.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
    panelNav.Dock = System.Windows.Forms.DockStyle.Fill;
    panelNav.Margin = new System.Windows.Forms.Padding(0);

    // إعداد مشترك لأزرار MDL2
    System.Action<System.Windows.Forms.Button, string, string> StylizeIconBtn = (btn, glyph, tooltip) =>
    {
        btn.Text = glyph;
        btn.Font = new System.Drawing.Font("Segoe MDL2 Assets", 13.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
        btn.ForeColor = ColText;
        btn.BackColor = System.Drawing.Color.Transparent;
        btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btn.FlatAppearance.BorderSize = 0;
        btn.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        btn.Size = new System.Drawing.Size(36, 32);
        var tt = new System.Windows.Forms.ToolTip();
        tt.SetToolTip(btn, tooltip);
    };

    StylizeIconBtn(this.btnBack,    "\uE72B", "Back");       // Back
    StylizeIconBtn(this.btnForward, "\uE72A", "Forward");    // Forward
    StylizeIconBtn(this.btnRefresh, "\uE72C", "Refresh");    // Refresh
    StylizeIconBtn(this.btnHome,    "\uE10F", "Home");       // Home
    StylizeIconBtn(this.btnStop,    "\uE71A", "Stop");       // Stop
    panelNav.Controls.Add(this.btnBack);
    panelNav.Controls.Add(this.btnForward);
    panelNav.Controls.Add(this.btnRefresh);
    panelNav.Controls.Add(this.btnHome);
    panelNav.Controls.Add(this.btnStop);

    this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
    this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
    this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
    this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
    this.btnStop.Click += new System.EventHandler(this.btnStop_Click);

    // ====== وسط: مربع العنوان / البحث ======
    var urlHost = new System.Windows.Forms.Panel();
    urlHost.Dock = System.Windows.Forms.DockStyle.Fill;
    urlHost.Height = 32;
    urlHost.Margin = new System.Windows.Forms.Padding(10, 0, 10, 0);
    urlHost.Padding = new System.Windows.Forms.Padding(10, 3, 10, 3);
    urlHost.BackColor = ColInput;
    urlHost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

    this.txtUrl.BorderStyle = System.Windows.Forms.BorderStyle.None;
    this.txtUrl.BackColor = ColInput;
    this.txtUrl.ForeColor = ColText;
    this.txtUrl.Font = new System.Drawing.Font("Segoe UI", 10.5F);
    this.txtUrl.Dock = System.Windows.Forms.DockStyle.Fill;
    this.txtUrl.PlaceholderText = "Search or enter address"; // متاحة في .NET 6+، تجاهلها لو نسخة أقدم
    this.txtUrl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUrl_KeyDown);

    // زر Go كأيقونة عدسة
    StylizeIconBtn(this.btnGo, "\uE721", "Go");
    this.btnGo.Dock = System.Windows.Forms.DockStyle.Right;
    this.btnGo.Width = 36;
    this.btnGo.Click += new System.EventHandler(this.btnGo_Click);

    urlHost.Controls.Add(this.txtUrl);
    urlHost.Controls.Add(this.btnGo);

    // ====== يمين: ToolStrip للإجراءات السريعة ======
    panelRight.AutoSize = true;
    panelRight.Dock = System.Windows.Forms.DockStyle.Fill;

    this.toolStripTop.Dock = System.Windows.Forms.DockStyle.None;
    this.toolStripTop.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
    this.toolStripTop.BackColor = System.Drawing.Color.Transparent;
    this.toolStripTop.AutoSize = true;
    this.toolStripTop.ImageScalingSize = new System.Drawing.Size(18, 18);
    this.toolStripTop.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
    this.toolStripTop.Padding = new System.Windows.Forms.Padding(0);
    this.toolStripTop.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
    this.toolStripTop.ForeColor = ColText;
    this.toolStripTop.Font = new System.Drawing.Font("Segoe MDL2 Assets", 12.5F);

    // تحويل الأزرار إلى أيقونات MDL2 (نفس الأحداث القديمة)
    void StyleTSB(System.Windows.Forms.ToolStripButton b, string glyph, string tip)
    {
        b.Text = glyph;
        b.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        b.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        b.ToolTipText = tip;
    }

    StyleTSB(this.btnNewTab,     "\uE710", "New Tab");
    StyleTSB(this.btnCloseTab,   "\uE711", "Close Tab");
    StyleTSB(this.btnIncognito,  "\uE72E", "Incognito");
    StyleTSB(this.btnDarkMode,   "\uE708", "Dark Mode");
    StyleTSB(this.btnHistory,    "\uE81C", "History");
    StyleTSB(this.btnScreenshot, "\uE722", "Screenshot");
    StyleTSB(this.btnTranslate,  "\uE7F3", "Translate");
    StyleTSB(this.btnFind,       "\uE721", "Find");

    this.comboSearchEngine.AutoSize = false;
    this.comboSearchEngine.Width = 120;
    this.comboSearchEngine.ToolTipText = "Search Engine";
    this.comboSearchEngine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
    this.comboSearchEngine.FlatStyle = System.Windows.Forms.FlatStyle.Standard;

    // ربط الأحداث كما كانت
    this.btnNewTab.Click += new System.EventHandler(this.btnNewTab_Click);
    this.btnCloseTab.Click += new System.EventHandler(this.btnCloseTab_Click);
    this.btnIncognito.Click += new System.EventHandler(this.btnIncognito_Click);
    this.btnDarkMode.Click += new System.EventHandler(this.btnDarkMode_Click);
    this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
    this.btnScreenshot.Click += new System.EventHandler(this.btnScreenshot_Click);
    this.btnTranslate.Click += new System.EventHandler(this.btnTranslate_Click);
    this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
    this.comboSearchEngine.SelectedIndexChanged += new System.EventHandler(this.comboSearchEngine_SelectedIndexChanged);

    // زر الإعدادات كزر منفصل على أقصى اليمين (أيقونة ترس)
    StylizeIconBtn(this.btnSettings, "\uE713", "Settings");
    this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);

    // إضافة عناصر ToolStrip
    this.toolStripTop.Items.Add(this.btnNewTab);
    this.toolStripTop.Items.Add(this.btnCloseTab);
    this.toolStripTop.Items.Add(new System.Windows.Forms.ToolStripSeparator());
    this.toolStripTop.Items.Add(this.btnIncognito);
    this.toolStripTop.Items.Add(this.btnDarkMode);
    this.toolStripTop.Items.Add(new System.Windows.Forms.ToolStripSeparator());
    this.toolStripTop.Items.Add(this.btnHistory);
    this.toolStripTop.Items.Add(this.btnScreenshot);
    this.toolStripTop.Items.Add(this.btnTranslate);
    this.toolStripTop.Items.Add(this.btnFind);
    this.toolStripTop.Items.Add(new System.Windows.Forms.ToolStripSeparator());
    this.toolStripTop.Items.Add(this.comboSearchEngine);

    // ضع الـ ToolStrip ثم زر الإعدادات يمينه
    var rightFlow = new System.Windows.Forms.FlowLayoutPanel();
    rightFlow.AutoSize = true;
    rightFlow.WrapContents = false;
    rightFlow.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
    rightFlow.Dock = System.Windows.Forms.DockStyle.Fill;
    rightFlow.Controls.Add(this.toolStripTop);
    rightFlow.Controls.Add(this.btnSettings);

    // إدراج الأعمدة في الـ TableLayout
    layoutTop.Controls.Add(panelNav, 0, 0);
    layoutTop.Controls.Add(urlHost, 1, 0);
    layoutTop.Controls.Add(rightFlow, 2, 0);

    // ========= شريط المفضلات (اختياري) =========
    this.toolStripBookmarks.Dock = System.Windows.Forms.DockStyle.Top;
    this.toolStripBookmarks.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
    this.toolStripBookmarks.ImageScalingSize = new System.Drawing.Size(18, 18);
    this.toolStripBookmarks.BackColor = ColTopBar;
    this.toolStripBookmarks.ForeColor = ColText;
    this.toolStripBookmarks.Visible = false; // أخفيناه افتراضيًا

    // ========= شريط التقدم النحيف أعلى النافذة =========
    this.progressBar1.Dock = System.Windows.Forms.DockStyle.Top;
    this.progressBar1.Height = 2;
    this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
    this.progressBar1.MarqueeAnimationSpeed = 25;
    this.progressBar1.BackColor = ColTopBar;
    this.progressBar1.ForeColor = ColAccent;
    this.progressBar1.Visible = false; // أظهره فقط عند التحميل

    // ========= منطقة المحتوى (تبويبات + تحميلات) =========
    this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
    this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
    this.splitContainerMain.SplitterWidth = 6;
    this.splitContainerMain.BackColor = ColBg;

    // أعلى: التبويبات + محتوى الويب (TabControl)
    this.splitContainerMain.Panel1.Controls.Add(this.tabControl);

    // أسفل: قائمة التحميلات (مخفية افتراضيًا)
    this.listViewDownloads.Dock = System.Windows.Forms.DockStyle.Fill;
    this.listViewDownloads.BorderStyle = System.Windows.Forms.BorderStyle.None;
    this.listViewDownloads.BackColor = ColBg;
    this.listViewDownloads.ForeColor = ColText;
    this.listViewDownloads.View = System.Windows.Forms.View.Details;
    this.listViewDownloads.FullRowSelect = true;
    this.listViewDownloads.Columns.Add("File", 420);
    this.listViewDownloads.Columns.Add("Status", 120);
    this.listViewDownloads.Columns.Add("Source", 360);
    this.splitContainerMain.Panel2.Controls.Add(this.listViewDownloads);

    // اخفِ لوحة التحميلات كبداية
    this.splitContainerMain.Panel2Collapsed = true;

    // ========= ترتيب عناصر الفورم =========
    // الترتيب: Progress (أعلى) -> Tabs Bar -> Controls -> Bookmarks -> SplitContainer
    this.Controls.Add(this.splitContainerMain);
    this.Controls.Add(this.toolStripBookmarks);
    this.Controls.Add(layoutTop);
    this.Controls.Add(topTabsBar);
    this.Controls.Add(this.progressBar1);

    // إنهاء
    this.ResumeLayout(false);
}
