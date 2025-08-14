// @TAHAPRO10X — Redesigned, organized, and fully formatted UI — Fixed by ahmed-hassan-coder-x
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;

namespace LightWebBrowser
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        // Core controls (kept for compatibility with existing handlers)
        private TextBox txtUrl;
        private Button btnGo;
        private Button btnBack;
        private Button btnForward;
        private Button btnRefresh;
        private Button btnStop;
        private Button btnHome;
        private Button btnSettings;
        private ProgressBar progressBar1;
        private WebView2 webView21; // (optional placeholder if needed by other parts)

        // Top command/tool areas
        private ToolStrip toolStripTop;
        private ToolStrip toolStripBookmarks;

        // Tabs + downloads
        private TabControl tabControl;
        private SplitContainer splitContainerMain;
        private ListView listViewDownloads;

        // Extra commands
        private ToolStripButton btnNewTab;
        private ToolStripButton btnCloseTab;
        private ToolStripButton btnIncognito;
        private ToolStripButton btnDarkMode;
        private ToolStripComboBox comboSearchEngine;
        private ToolStripButton btnHistory;
        private ToolStripButton btnScreenshot;
        private ToolStripButton btnTranslate;
        private ToolStripButton btnFind;

        // Layout helpers (new):
        private FlowLayoutPanel navBar;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabel;
        private ToolStripProgressBar statusProgress;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new Container();

            // === Base form appearance (polished / compact) ===
            this.SuspendLayout();
            this.AutoScaleMode = AutoScaleMode.None;
            this.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            this.BackColor = Color.FromArgb(30, 33, 36);
            this.ForeColor = Color.WhiteSmoke;
            this.ClientSize = new Size(1100, 700);
            this.MinimumSize = new Size(900, 560);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Light Web Browser";

            // === ToolStrip (Top Command Bar) ===
            this.toolStripTop = new ToolStrip();
            this.toolStripTop.ImageScalingSize = new Size(20, 20);
            this.toolStripTop.GripStyle = ToolStripGripStyle.Hidden;
            this.toolStripTop.Padding = new Padding(6, 4, 6, 4);
            this.toolStripTop.BackColor = Color.FromArgb(40, 43, 48);
            this.toolStripTop.RenderMode = ToolStripRenderMode.System;

            this.btnNewTab = new ToolStripButton("✚ Tab");
            this.btnNewTab.Margin = new Padding(0, 0, 4, 0);
            this.btnNewTab.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.btnNewTab.Click += new EventHandler(this.btnNewTab_Click);

            this.btnCloseTab = new ToolStripButton("✕ Close");
            this.btnCloseTab.Margin = new Padding(0, 0, 10, 0);
            this.btnCloseTab.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.btnCloseTab.Click += new EventHandler(this.btnCloseTab_Click);

            this.btnIncognito = new ToolStripButton("🕶️ Incognito");
            this.btnIncognito.CheckOnClick = true;
            this.btnIncognito.Margin = new Padding(0, 0, 4, 0);
            this.btnIncognito.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.btnIncognito.Click += new EventHandler(this.btnIncognito_Click);

            this.btnDarkMode = new ToolStripButton("🌙 Dark");
            this.btnDarkMode.CheckOnClick = true;
            this.btnDarkMode.Margin = new Padding(0, 0, 10, 0);
            this.btnDarkMode.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.btnDarkMode.Click += new EventHandler(this.btnDarkMode_Click);

            this.comboSearchEngine = new ToolStripComboBox();
            this.comboSearchEngine.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboSearchEngine.AutoSize = false;
            this.comboSearchEngine.Width = 170;
            this.comboSearchEngine.ToolTipText = "Search Engine";
            this.comboSearchEngine.SelectedIndexChanged += new EventHandler(this.comboSearchEngine_SelectedIndexChanged);

            this.btnHistory = new ToolStripButton("🕘 History");
            this.btnHistory.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.btnHistory.Margin = new Padding(10, 0, 4, 0);
            this.btnHistory.Click += new EventHandler(this.btnHistory_Click);

            this.btnScreenshot = new ToolStripButton("📷 Shot");
            this.btnScreenshot.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.btnScreenshot.Margin = new Padding(0, 0, 4, 0);
            this.btnScreenshot.Click += new EventHandler(this.btnScreenshot_Click);

            this.btnTranslate = new ToolStripButton("🌐 Translate");
            this.btnTranslate.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.btnTranslate.Margin = new Padding(0, 0, 4, 0);
            this.btnTranslate.Click += new EventHandler(this.btnTranslate_Click);

            this.btnFind = new ToolStripButton("🔎 Find");
            this.btnFind.DisplayStyle = ToolStripItemDisplayStyle.Text;
            this.btnFind.Margin = new Padding(0);
            this.btnFind.Click += new EventHandler(this.btnFind_Click);

            this.toolStripTop.Items.AddRange(new ToolStripItem[] {
                this.btnNewTab,
                this.btnCloseTab,
                new ToolStripSeparator(),
                this.btnIncognito,
                this.btnDarkMode,
                new ToolStripSeparator(),
                new ToolStripLabel("Engine:"),
                this.comboSearchEngine,
                new ToolStripSeparator(),
                this.btnHistory,
                this.btnScreenshot,
                this.btnTranslate,
                this.btnFind
            });
            this.toolStripTop.Dock = DockStyle.Top;

            // === Navigation Bar (FlowLayout with core buttons + URL) ===
            this.navBar = new FlowLayoutPanel();
            this.navBar.Dock = DockStyle.Top;
            this.navBar.Height = 42;
            this.navBar.Padding = new Padding(8, 6, 8, 6);
            this.navBar.FlowDirection = FlowDirection.LeftToRight;
            this.navBar.WrapContents = false;
            this.navBar.BackColor = Color.FromArgb(36, 39, 43);

            // Buttons style helper
            Size navBtnSize = new Size(40, 28);

            this.btnBack = new Button();
            this.btnBack.Text = "←";
            this.btnBack.Size = navBtnSize;
            this.btnBack.FlatStyle = FlatStyle.Flat;
            this.btnBack.FlatAppearance.BorderColor = Color.FromArgb(60, 64, 70);
            this.btnBack.FlatAppearance.BorderSize = 1;
            this.btnBack.BackColor = Color.FromArgb(50, 54, 59);
            this.btnBack.ForeColor = Color.WhiteSmoke;
            this.btnBack.Margin = new Padding(0, 0, 6, 0);
            this.btnBack.Click += new EventHandler(this.btnBack_Click);

            this.btnForward = new Button();
            this.btnForward.Text = "→";
            this.btnForward.Size = navBtnSize;
            this.btnForward.FlatStyle = FlatStyle.Flat;
            this.btnForward.FlatAppearance.BorderColor = Color.FromArgb(60, 64, 70);
            this.btnForward.FlatAppearance.BorderSize = 1;
            this.btnForward.BackColor = Color.FromArgb(50, 54, 59);
            this.btnForward.ForeColor = Color.WhiteSmoke;
            this.btnForward.Margin = new Padding(0, 0, 6, 0);
            this.btnForward.Click += new EventHandler(this.btnForward_Click);

            this.btnRefresh = new Button();
            this.btnRefresh.Text = "⟳";
            this.btnRefresh.Size = navBtnSize;
            this.btnRefresh.FlatStyle = FlatStyle.Flat;
            this.btnRefresh.FlatAppearance.BorderColor = Color.FromArgb(60, 64, 70);
            this.btnRefresh.FlatAppearance.BorderSize = 1;
            this.btnRefresh.BackColor = Color.FromArgb(50, 54, 59);
            this.btnRefresh.ForeColor = Color.WhiteSmoke;
            this.btnRefresh.Margin = new Padding(0, 0, 6, 0);
            this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);

            this.btnStop = new Button();
            this.btnStop.Text = "■";
            this.btnStop.Size = navBtnSize;
            this.btnStop.FlatStyle = FlatStyle.Flat;
            this.btnStop.FlatAppearance.BorderColor = Color.FromArgb(60, 64, 70);
            this.btnStop.FlatAppearance.BorderSize = 1;
            this.btnStop.BackColor = Color.FromArgb(50, 54, 59);
            this.btnStop.ForeColor = Color.WhiteSmoke;
            this.btnStop.Margin = new Padding(0, 0, 10, 0);
            this.btnStop.Click += new EventHandler(this.btnStop_Click);

            this.btnHome = new Button();
            this.btnHome.Text = "Home";
            this.btnHome.AutoSize = true;
            this.btnHome.Padding = new Padding(8, 2, 8, 2);
            this.btnHome.FlatStyle = FlatStyle.Flat;
            this.btnHome.FlatAppearance.BorderColor = Color.FromArgb(60, 64, 70);
            this.btnHome.FlatAppearance.BorderSize = 1;
            this.btnHome.BackColor = Color.FromArgb(50, 54, 59);
            this.btnHome.ForeColor = Color.WhiteSmoke;
            this.btnHome.Margin = new Padding(0, 0, 10, 0);
            this.btnHome.Click += new EventHandler(this.btnHome_Click);

            // URL textbox (fills remaining space)
            this.txtUrl = new TextBox();
            this.txtUrl.BorderStyle = BorderStyle.FixedSingle;
            this.txtUrl.Font = new Font("Segoe UI", 9.5F);
            this.txtUrl.BackColor = Color.FromArgb(28, 31, 34);
            this.txtUrl.ForeColor = Color.Gainsboro;
            this.txtUrl.Margin = new Padding(0, 0, 6, 0);
            this.txtUrl.Width = 700; // initial; will be stretched by FlowLayoutPanel grow
            this.txtUrl.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            this.txtUrl.KeyDown += new KeyEventHandler(this.txtUrl_KeyDown);

            this.btnGo = new Button();
            this.btnGo.Text = "Go";
            this.btnGo.AutoSize = true;
            this.btnGo.Padding = new Padding(10, 2, 10, 2);
            this.btnGo.FlatStyle = FlatStyle.Flat;
            this.btnGo.FlatAppearance.BorderColor = Color.FromArgb(60, 64, 70);
            this.btnGo.FlatAppearance.BorderSize = 1;
            this.btnGo.BackColor = Color.FromArgb(63, 68, 74);
            this.btnGo.ForeColor = Color.WhiteSmoke;
            this.btnGo.Margin = new Padding(0, 0, 6, 0);
            this.btnGo.Click += new EventHandler(this.btnGo_Click);

            this.btnSettings = new Button();
            this.btnSettings.Text = "Settings";
            this.btnSettings.AutoSize = true;
            this.btnSettings.Padding = new Padding(10, 2, 10, 2);
            this.btnSettings.FlatStyle = FlatStyle.Flat;
            this.btnSettings.FlatAppearance.BorderColor = Color.FromArgb(60, 64, 70);
            this.btnSettings.FlatAppearance.BorderSize = 1;
            this.btnSettings.BackColor = Color.FromArgb(63, 68, 74);
            this.btnSettings.ForeColor = Color.WhiteSmoke;
            this.btnSettings.Margin = new Padding(0);
            this.btnSettings.Click += new EventHandler(this.btnSettings_Click);

            this.navBar.Controls.Add(this.btnBack);
            this.navBar.Controls.Add(this.btnForward);
            this.navBar.Controls.Add(this.btnRefresh);
            this.navBar.Controls.Add(this.btnStop);
            this.navBar.Controls.Add(this.btnHome);
            this.navBar.Controls.Add(this.txtUrl);
            this.navBar.Controls.Add(this.btnGo);
            this.navBar.Controls.Add(this.btnSettings);

            // === Bookmarks Bar ===
            this.toolStripBookmarks = new ToolStrip();
            this.toolStripBookmarks.ImageScalingSize = new Size(20, 20);
            this.toolStripBookmarks.GripStyle = ToolStripGripStyle.Hidden;
            this.toolStripBookmarks.BackColor = Color.FromArgb(44, 48, 53);
            this.toolStripBookmarks.Padding = new Padding(6, 2, 6, 2);
            this.toolStripBookmarks.Dock = DockStyle.Top;

            // === Progress (thin, top) ===
            this.progressBar1 = new ProgressBar();
            this.progressBar1.Style = ProgressBarStyle.Continuous; // switch to Marquee while loading in runtime
            this.progressBar1.Height = 3;
            this.progressBar1.Dock = DockStyle.Top;
            this.progressBar1.ForeColor = Color.MediumSeaGreen;
            this.progressBar1.BackColor = Color.FromArgb(28, 31, 34);

            // === Main Split: Tabs (top) + Downloads (bottom) ===
            this.splitContainerMain = new SplitContainer();
            this.splitContainerMain.Dock = DockStyle.Fill;
            this.splitContainerMain.Orientation = Orientation.Horizontal;
            this.splitContainerMain.SplitterWidth = 6;
            this.splitContainerMain.Panel1MinSize = 200;
            this.splitContainerMain.Panel2MinSize = 90;
            this.splitContainerMain.SplitterDistance = 520;
            this.splitContainerMain.BackColor = Color.FromArgb(36, 39, 43);

            // Tabs area
            this.tabControl = new TabControl();
            this.tabControl.Dock = DockStyle.Fill;
            this.tabControl.Font = new Font("Segoe UI", 9F);
            this.tabControl.Padding = new Point(18, 5);

            this.splitContainerMain.Panel1.Controls.Add(this.tabControl);

            // Downloads list
            this.listViewDownloads = new ListView();
            this.listViewDownloads.Dock = DockStyle.Fill;
            this.listViewDownloads.View = View.Details;
            this.listViewDownloads.FullRowSelect = true;
            this.listViewDownloads.HideSelection = false;
            this.listViewDownloads.BorderStyle = BorderStyle.None;
            this.listViewDownloads.BackColor = Color.FromArgb(28, 31, 34);
            this.listViewDownloads.ForeColor = Color.Gainsboro;
            this.listViewDownloads.GridLines = true;

            var colFile = new ColumnHeader() { Text = "File", Width = 380 };
            var colStatus = new ColumnHeader() { Text = "Status", Width = 140 };
            var colSource = new ColumnHeader() { Text = "Source", Width = 480 };
            this.listViewDownloads.Columns.AddRange(new ColumnHeader[] { colFile, colStatus, colSource });
            this.splitContainerMain.Panel2.Controls.Add(this.listViewDownloads);

            // === Optional: Status strip at bottom for subtle feedback ===
            this.statusStrip = new StatusStrip();
            this.statusStrip.SizingGrip = false;
            this.statusStrip.BackColor = Color.FromArgb(36, 39, 43);
            this.statusStrip.ForeColor = Color.Gainsboro;

            this.statusLabel = new ToolStripStatusLabel("Ready");
            this.statusLabel.Spring = true;

            this.statusProgress = new ToolStripProgressBar();
            this.statusProgress.AutoSize = false;
            this.statusProgress.Size = new Size(160, 16);
            this.statusProgress.Style = ProgressBarStyle.Continuous;

            this.statusStrip.Items.Add(this.statusLabel);
            this.statusStrip.Items.Add(this.statusProgress);

            // === Placeholder WebView2 (not added to controls by default) ===
            this.webView21 = new WebView2();
            // (You can programmatically add WebView2 into tabs elsewhere.)

            // === Add controls to Form in final order ===
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.toolStripBookmarks);
            this.Controls.Add(this.navBar);
            this.Controls.Add(this.toolStripTop);
            this.Controls.Add(this.statusStrip);

            // Events (kept)
            this.Load += new EventHandler(this.Form1_Load);
            this.FormClosing += new FormClosingEventHandler(this.Form1_FormClosing);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
