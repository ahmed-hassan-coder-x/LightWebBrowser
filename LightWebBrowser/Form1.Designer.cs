// @TAHAPRO10X pls redesign the ui :) 
using Microsoft.Web.WebView2.WinForms;

namespace LightWebBrowser
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.ProgressBar progressBar1;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView21;

        private System.Windows.Forms.ToolStrip toolStripTop;
        private System.Windows.Forms.ToolStrip toolStripBookmarks;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.ToolStripButton btnNewTab;
        private System.Windows.Forms.ToolStripButton btnCloseTab;
        private System.Windows.Forms.ToolStripButton btnIncognito;
        private System.Windows.Forms.ToolStripButton btnDarkMode;
        private System.Windows.Forms.ToolStripComboBox comboSearchEngine;
        private System.Windows.Forms.ToolStripButton btnHistory;
        private System.Windows.Forms.ListView listViewDownloads;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.ToolStripButton btnScreenshot;
        private System.Windows.Forms.ToolStripButton btnTranslate;
        private System.Windows.Forms.ToolStripButton btnFind;

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
            this.components = new System.ComponentModel.Container();
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
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            this.toolStripTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtUrl
            // 
            this.txtUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                    | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUrl.Location = new System.Drawing.Point(180, 33);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(560, 22);
            this.txtUrl.TabIndex = 0;
            this.txtUrl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUrl_KeyDown);
            // 
            // btnGo
            // 
            this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGo.Location = new System.Drawing.Point(746, 30);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(50, 26);
            this.btnGo.TabIndex = 1;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(12, 29);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(50, 28);
            this.btnBack.TabIndex = 2;
            this.btnBack.Text = "←";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnForward
            // 
            this.btnForward.Location = new System.Drawing.Point(68, 29);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(50, 28);
            this.btnForward.TabIndex = 3;
            this.btnForward.Text = "→";
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(124, 29);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(50, 28);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "⟳";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(802, 30);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(50, 26);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "■";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnHome
            // 
            this.btnHome.Location = new System.Drawing.Point(858, 30);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(60, 26);
            this.btnHome.TabIndex = 6;
            this.btnHome.Text = "Home";
            this.btnHome.UseVisualStyleBackColor = true;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettings.Location = new System.Drawing.Point(924, 30);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(60, 26);
            this.btnSettings.TabIndex = 7;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                                    | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(12, 62);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(972, 8);
            this.progressBar1.TabIndex = 8;
            // 
            // toolStripTop
            // 
            this.toolStripTop.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripTop.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewTab,
            this.btnCloseTab,
            this.btnIncognito,
            this.btnDarkMode,
            this.comboSearchEngine,
            this.btnHistory,
            this.btnScreenshot,
            this.btnTranslate,
            this.btnFind});
            this.toolStripTop.Location = new System.Drawing.Point(0, 0);
            this.toolStripTop.Name = "toolStripTop";
            this.toolStripTop.Size = new System.Drawing.Size(996, 27);
            this.toolStripTop.TabIndex = 9;
            this.toolStripTop.Text = "toolStrip1";
            // 
            // btnNewTab
            // 
            this.btnNewTab.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnNewTab.Name = "btnNewTab";
            this.btnNewTab.Size = new System.Drawing.Size(50, 24);
            this.btnNewTab.Text = "New";
            this.btnNewTab.Click += new System.EventHandler(this.btnNewTab_Click);
            // 
            // btnCloseTab
            // 
            this.btnCloseTab.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnCloseTab.Name = "btnCloseTab";
            this.btnCloseTab.Size = new System.Drawing.Size(45, 24);
            this.btnCloseTab.Text = "Close";
            this.btnCloseTab.Click += new System.EventHandler(this.btnCloseTab_Click);
            // 
            // btnIncognito
            // 
            this.btnIncognito.CheckOnClick = true;
            this.btnIncognito.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnIncognito.Name = "btnIncognito";
            this.btnIncognito.Size = new System.Drawing.Size(70, 24);
            this.btnIncognito.Text = "Incognito";
            this.btnIncognito.Click += new System.EventHandler(this.btnIncognito_Click);
            // 
            // btnDarkMode
            // 
            this.btnDarkMode.CheckOnClick = true;
            this.btnDarkMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnDarkMode.Name = "btnDarkMode";
            this.btnDarkMode.Size = new System.Drawing.Size(46, 24);
            this.btnDarkMode.Text = "Dark";
            this.btnDarkMode.Click += new System.EventHandler(this.btnDarkMode_Click);
            // 
            // comboSearchEngine
            // 
            this.comboSearchEngine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboSearchEngine.Name = "comboSearchEngine";
            this.comboSearchEngine.Size = new System.Drawing.Size(140, 27);
            this.comboSearchEngine.SelectedIndexChanged += new System.EventHandler(this.comboSearchEngine_SelectedIndexChanged);
            // 
            // btnHistory
            // 
            this.btnHistory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(52, 24);
            this.btnHistory.Text = "History";
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click);
            // 
            // btnScreenshot
            // 
            this.btnScreenshot.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnScreenshot.Name = "btnScreenshot";
            this.btnScreenshot.Size = new System.Drawing.Size(70, 24);
            this.btnScreenshot.Text = "Screenshot";
            this.btnScreenshot.Click += new System.EventHandler(this.btnScreenshot_Click);
            // 
            // btnTranslate
            // 
            this.btnTranslate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnTranslate.Name = "btnTranslate";
            this.btnTranslate.Size = new System.Drawing.Size(63, 24);
            this.btnTranslate.Text = "Translate";
            this.btnTranslate.Click += new System.EventHandler(this.btnTranslate_Click);
            // 
            // btnFind
            // 
            this.btnFind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(36, 24);
            this.btnFind.Text = "Find";
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // toolStripBookmarks
            // 
            this.toolStripBookmarks.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolStripBookmarks.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStripBookmarks.Location = new System.Drawing.Point(0, 70);
            this.toolStripBookmarks.Name = "toolStripBookmarks";
            this.toolStripBookmarks.Size = new System.Drawing.Size(996, 27);
            this.toolStripBookmarks.TabIndex = 10;
            this.toolStripBookmarks.Text = "toolStrip2";
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                    | System.Windows.Forms.AnchorStyles.Left)
                                    | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainerMain.Location = new System.Drawing.Point(12, 96);
            this.splitContainerMain.Name = "splitContainerMain";
            this.splitContainerMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.tabControl);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.listViewDownloads);
            this.splitContainerMain.Size = new System.Drawing.Size(972, 452);
            this.splitContainerMain.SplitterDistance = 350;
            this.splitContainerMain.TabIndex = 11;
            // 
            // tabControl
            // 
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(972, 350);
            this.tabControl.TabIndex = 12;
            // 
            // listViewDownloads
            // 
            this.listViewDownloads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewDownloads.Location = new System.Drawing.Point(0, 0);
            this.listViewDownloads.Name = "listViewDownloads";
            this.listViewDownloads.Size = new System.Drawing.Size(972, 98);
            this.listViewDownloads.TabIndex = 0;
            this.listViewDownloads.View = System.Windows.Forms.View.Details;
            this.listViewDownloads.Columns.Add("File");
            this.listViewDownloads.Columns.Add("Status");
            this.listViewDownloads.Columns.Add("Source");
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(996, 560);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.toolStripBookmarks);
            this.Controls.Add(this.toolStripTop);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnHome);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnForward);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.txtUrl);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "Form1";
            this.Text = "Light Web Browser";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.toolStripTop.ResumeLayout(false);
            this.toolStripTop.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion
    }
}
