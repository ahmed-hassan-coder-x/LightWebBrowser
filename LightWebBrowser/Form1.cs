using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using Newtonsoft.Json;

namespace LightWebBrowser
{
    public partial class Form1 : Form
    {
        private string homepage = "https://www.google.com";
        private List<string> globalHistory = new List<string>();
        private string bookmarksFile => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bookmarks.json");
        private string sessionFile => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sessions.json");
        private string settingsFile => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.json");
        private List<string> bookmarks = new List<string>();
        private bool incognitoMode = false;
        private string userDataFolderBase => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UserData");
        private string defaultUserDataFolder => Path.Combine(userDataFolderBase, "Default");
        private HashSet<string> adBlockList = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "doubleclick.net",
            "googlesyndication.com",
            "adservice.google.com",
            "adsystem.com",
            "ads.twitter.com"
        };

        public Form1()
        {
            InitializeComponent();
            LoadSettings();
            LoadBookmarks();
            SetupShortcuts();
        }

        #region Initialization

        private async void Form1_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(userDataFolderBase))
                Directory.CreateDirectory(userDataFolderBase);

            txtUrl.Text = homepage;

            if (comboSearchEngine.Items.Count == 0)
                comboSearchEngine.Items.AddRange(new string[] { "Google", "Bing", "DuckDuckGo" });

            int idx = comboSearchEngine.Items.IndexOf("Google");
            comboSearchEngine.SelectedIndex = Math.Max(0, idx);

            // create first tab
            CreateNewTab(homepage, true, false);

            await Task.CompletedTask;
        }

        private void SetupShortcuts()
        {
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
        }

        #endregion

        #region Tabs & WebView Factory

        private TabPage CreateNewTabPage(string title = "New Tab")
        {
            var tab = new TabPage(title);
            tab.Padding = new Padding(2);
            return tab;
        }

        private async void CreateNewTab(string url = null, bool makeActive = true, bool incognito = false)
        {
            var tab = CreateNewTabPage("Loading...");
            var webView = new WebView2
            {
                Dock = DockStyle.Fill,
                Name = "webView"
            };

            tab.Controls.Add(webView);

            tabControl.TabPages.Add(tab);
            if (makeActive)
                tabControl.SelectedTab = tab;

            try
            {
                string userDataFolder;
                if (incognito)
                {
                    userDataFolder = Path.Combine(Path.GetTempPath(), "LWB_Incognito_" + Guid.NewGuid().ToString("N"));
                }
                else
                {
                    userDataFolder = defaultUserDataFolder;
                }

                if (!Directory.Exists(userDataFolder))
                    Directory.CreateDirectory(userDataFolder);

                var env = await CoreWebView2Environment.CreateAsync(userDataFolder: userDataFolder);
                await webView.EnsureCoreWebView2Async(env);

                webView.CoreWebView2.DocumentTitleChanged += (s, e) =>
                {
                    try
                    {
                        tab.Text = webView.CoreWebView2.DocumentTitle ?? webView.Source?.ToString() ?? "Tab";
                    }
                    catch
                    {
                        tab.Text = webView.Source?.ToString() ?? "Tab";
                    }
                };

                webView.CoreWebView2.NavigationCompleted += (s, e) => { OnNavigationCompleted(webView); };

                webView.CoreWebView2.NewWindowRequested += (s, e) =>
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(e.Uri))
                        {
                            this.BeginInvoke(new Action(() => CreateNewTab(e.Uri, true, incognito)));
                            e.Handled = true;
                        }
                    }
                    catch { }
                };

                // Basic request filtering for ad blocking
                try
                {
                    webView.CoreWebView2.AddWebResourceRequestedFilter("*", CoreWebView2WebResourceContext.All);
                    webView.CoreWebView2.WebResourceRequested += (s, e) =>
                    {
                        try
                        {
                            var req = e.Request;
                            if (req != null && !string.IsNullOrEmpty(req.Uri))
                            {
                                Uri u = new Uri(req.Uri);
                                foreach (var block in adBlockList)
                                {
                                    if (u.Host.IndexOf(block, StringComparison.OrdinalIgnoreCase) >= 0)
                                    {
                                        e.Response = webView.CoreWebView2.Environment.CreateWebResourceResponse(null, 403, "Blocked", "");
                                        return;
                                    }
                                }
                            }
                        }
                        catch { }
                    };
                }
                catch { }

                // Downloads
                webView.CoreWebView2.DownloadStarting += CoreWebView2_DownloadStarting;

                // navigate
                if (!string.IsNullOrEmpty(url))
                {
                    NavigateWebView(webView, url);
                }
                else
                {
                    NavigateWebView(webView, homepage);
                }

                UpdateBookmarksBar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to create tab: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private WebView2 GetActiveWebView()
        {
            if (tabControl.SelectedTab == null) return null;
            foreach (Control c in tabControl.SelectedTab.Controls)
            {
                WebView2 wv = c as WebView2;
                if (wv != null) return wv;
            }
            return null;
        }

        private IEnumerable<WebView2> GetAllWebViews()
        {
            foreach (TabPage t in tabControl.TabPages)
            {
                foreach (Control c in t.Controls)
                {
                    WebView2 wv = c as WebView2;
                    if (wv != null) yield return wv;
                }
            }
        }

        #endregion

        #region Navigation & History

        private void NavigateWebView(WebView2 webView, string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return;

            if (!url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) &&
                !url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                url = "https://" + url;
            }

            try
            {
                if (webView.CoreWebView2 != null)
                    webView.CoreWebView2.Navigate(url);
                else
                    webView.Source = new Uri(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Navigation error: " + ex.Message);
            }
        }

        private void OnNavigationCompleted(WebView2 webView)
        {
            try
            {
                if (webView == GetActiveWebView())
                {
                    txtUrl.Text = webView.Source?.ToString() ?? "";
                }

                if (!incognitoMode)
                {
                    var url = webView.Source?.ToString();
                    if (!string.IsNullOrEmpty(url))
                    {
                        globalHistory.Add(url);
                    }
                }

                SaveSession();
            }
            catch { }
        }

        #endregion

        #region UI Events (Buttons / Controls)

        private void btnGo_Click(object sender, EventArgs e)
        {
            var wv = GetActiveWebView();
            if (wv != null)
                NavigateWebView(wv, txtUrl.Text.Trim());
            else
                CreateNewTab(txtUrl.Text.Trim(), true, incognitoMode);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            var wv = GetActiveWebView();
            try
            {
                if (wv != null && wv.CoreWebView2 != null && wv.CoreWebView2.CanGoBack)
                    wv.CoreWebView2.GoBack();
            }
            catch { }
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            var wv = GetActiveWebView();
            try
            {
                if (wv != null && wv.CoreWebView2 != null && wv.CoreWebView2.CanGoForward)
                    wv.CoreWebView2.GoForward();
            }
            catch { }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            var wv = GetActiveWebView();
            try
            {
                wv?.Reload();
            }
            catch
            {
                if (!string.IsNullOrWhiteSpace(txtUrl.Text))
                {
                    NavigateWebView(wv, txtUrl.Text);
                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                GetActiveWebView()?.CoreWebView2?.Stop();
            }
            catch { }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            var wv = GetActiveWebView();
            if (wv != null) NavigateWebView(wv, homepage);
            else CreateNewTab(homepage, true, incognitoMode);
        }

        private void btnNewTab_Click(object sender, EventArgs e)
        {
            CreateNewTab(homepage, true, incognitoMode);
        }

        private void btnCloseTab_Click(object sender, EventArgs e)
        {
            if (tabControl.TabPages.Count > 0)
            {
                var current = tabControl.SelectedTab;
                foreach (Control c in current.Controls)
                {
                    try { c.Dispose(); } catch { }
                }
                tabControl.TabPages.Remove(current);
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.Show();
        }

        private void btnIncognito_Click(object sender, EventArgs e)
        {
            incognitoMode = !incognitoMode;
            btnIncognito.Checked = incognitoMode;
            if (incognitoMode)
                MessageBox.Show("Incognito mode enabled for new tabs. Existing tabs remain unchanged.", "Incognito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region History UI

        private void btnHistory_Click(object sender, EventArgs e)
        {
            using (Form historyForm = new Form())
            {
                historyForm.Text = "History";
                historyForm.Size = new Size(600, 400);
                historyForm.StartPosition = FormStartPosition.CenterParent;

                ListView list = new ListView
                {
                    Dock = DockStyle.Fill,
                    View = View.Details,
                    FullRowSelect = true
                };
                list.Columns.Add("URL", -2, HorizontalAlignment.Left);
                list.Columns.Add("Time", 150, HorizontalAlignment.Left);

                // load from memory (most recent first)
                for (int i = globalHistory.Count - 1; i >= 0; i--)
                {
                    string url = globalHistory[i];
                    var li = new ListViewItem(new[] { url, DateTime.Now.ToString("g") });
                    li.Tag = url;
                    list.Items.Add(li);
                }

                list.DoubleClick += (s, ev) =>
                {
                    if (list.SelectedItems.Count > 0)
                    {
                        var url = list.SelectedItems[0].Tag as string;
                        CreateNewTab(url, true, incognitoMode);
                        historyForm.Close();
                    }
                };

                historyForm.Controls.Add(list);
                historyForm.ShowDialog();
            }
        }

        #endregion

        #region Bookmarks

        private void LoadBookmarks()
        {
            try
            {
                if (File.Exists(bookmarksFile))
                {
                    var json = File.ReadAllText(bookmarksFile);
                    bookmarks = JsonConvert.DeserializeObject<List<string>>(json) ?? new List<string>();
                }
                UpdateBookmarksBar();
            }
            catch { bookmarks = new List<string>(); }
        }

        private void SaveBookmarks()
        {
            try
            {
                var json = JsonConvert.SerializeObject(bookmarks, Formatting.Indented);
                File.WriteAllText(bookmarksFile, json);
            }
            catch { }
        }

        private void UpdateBookmarksBar()
        {
            try
            {
                toolStripBookmarks.Items.Clear();
                foreach (var bm in bookmarks)
                {
                    var btn = new ToolStripButton
                    {
                        Text = ShortenForToolbar(bm),
                        Tag = bm,
                        ToolTipText = bm,
                        AutoSize = false,
                        Width = 140
                    };
                    btn.Click += (s, e) =>
                    {
                        var url = (s as ToolStripButton).Tag as string;
                        CreateNewTab(url, true, incognitoMode);
                    };
                    toolStripBookmarks.Items.Add(btn);
                }

                toolStripBookmarks.Items.Add(new ToolStripSeparator());
                var addBtn = new ToolStripButton("★") { ToolTipText = "Add Bookmark" };
                addBtn.Click += (s, e) =>
                {
                    var wv = GetActiveWebView();
                    var url = (wv != null ? (wv.Source != null ? wv.Source.ToString() : null) : null) ?? txtUrl.Text;
                    if (!string.IsNullOrEmpty(url))
                    {
                        bookmarks.Add(url);
                        SaveBookmarks();
                        UpdateBookmarksBar();
                    }
                };
                toolStripBookmarks.Items.Add(addBtn);
            }
            catch { }
        }

        private string ShortenForToolbar(string url)
        {
            if (string.IsNullOrEmpty(url)) return "";
            if (url.Length > 36) return url.Substring(0, 33) + "...";
            return url;
        }

        #endregion

        #region Session Restore

        private void SaveSession()
        {
            try
            {
                var session = new List<string>();
                foreach (var wv in GetAllWebViews())
                {
                    if (wv != null && wv.Source != null)
                        session.Add(wv.Source.ToString());
                }
                var json = JsonConvert.SerializeObject(session, Formatting.Indented);
                File.WriteAllText(sessionFile, json);
            }
            catch { }
        }

        private void RestoreSession()
        {
            try
            {
                if (File.Exists(sessionFile))
                {
                    var json = File.ReadAllText(sessionFile);
                    var urls = JsonConvert.DeserializeObject<List<string>>(json) ?? new List<string>();
                    tabControl.TabPages.Clear();
                    foreach (var u in urls)
                        CreateNewTab(u, false, false);
                    if (tabControl.TabPages.Count > 0)
                        tabControl.SelectedIndex = 0;
                }
            }
            catch { }
        }

        #endregion

        #region Downloads

        private void CoreWebView2_DownloadStarting(object sender, CoreWebView2DownloadStartingEventArgs e)
        {
            try
            {
                var def = e;
                string uri = def.DownloadOperation.Uri;
                string fileName = def.ResultFilePath;
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.FileName = Path.GetFileName(fileName);
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        def.ResultFilePath = sfd.FileName;
                    }
                }

                listViewDownloads.Items.Add(new ListViewItem(new[] { Path.GetFileName(def.ResultFilePath), "Starting", uri }));

                def.DownloadOperation.BytesReceivedChanged += (s, ev) =>
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        foreach (ListViewItem it in listViewDownloads.Items)
                        {
                            if (it.SubItems[0].Text == Path.GetFileName(def.ResultFilePath))
                            {
                                it.SubItems[1].Text = $"{def.DownloadOperation.BytesReceived} bytes";
                                break;
                            }
                        }
                    }));
                };

                def.DownloadOperation.StateChanged += (s, ev) =>
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        foreach (ListViewItem it in listViewDownloads.Items)
                        {
                            if (it.SubItems[0].Text == Path.GetFileName(def.ResultFilePath))
                            {
                                it.SubItems[1].Text = def.DownloadOperation.State.ToString();
                                break;
                            }
                        }
                    }));
                };
            }
            catch { }
        }

        #endregion

        #region Screenshot & Utilities

        private async void btnScreenshot_Click(object sender, EventArgs e)
        {
            var wv = GetActiveWebView();
            if (wv == null || wv.CoreWebView2 == null) return;

            using (SaveFileDialog sfd = new SaveFileDialog { Filter = "PNG Image|*.png", FileName = "screenshot.png" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create, FileAccess.Write))
                    {
                        await wv.CoreWebView2.CapturePreviewAsync(CoreWebView2CapturePreviewImageFormat.Png, fs);
                    }
                    MessageBox.Show("Screenshot saved.", "Screenshot", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnTranslate_Click(object sender, EventArgs e)
        {
            var wv = GetActiveWebView();
            if (wv == null || wv.CoreWebView2 == null) return;

            try
            {
                wv.CoreWebView2.ExecuteScriptAsync("window.getSelection().toString();").ContinueWith(t =>
                {
                    string res = t.Result ?? "";
                    res = TrimJsonString(res);
                    if (string.IsNullOrWhiteSpace(res))
                    {
                        MessageBox.Show("No text selected.", "Translate", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    string url = "https://translate.google.com/?sl=auto&tl=en&text=" + Uri.EscapeDataString(res) + "&op=translate";
                    this.BeginInvoke(new Action(() => CreateNewTab(url, true, incognitoMode)));
                });
            }
            catch { }
        }

        private string TrimJsonString(string json)
        {
            if (string.IsNullOrEmpty(json)) return "";
            // ExecuteScriptAsync may return a JSON string literal like "\"text\"" or primitive like "true"
            // If it's a quoted string, deserialize to get the inner text
            if (json.Length >= 2 && json[0] == '"' && json[json.Length - 1] == '"')
            {
                try
                {
                    return JsonConvert.DeserializeObject<string>(json);
                }
                catch { return json.Trim('"'); }
            }
            // otherwise return raw
            return json;
        }

        #endregion

        #region Search Engine & Omnibox

        private void comboSearchEngine_SelectedIndexChanged(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void txtUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                string text = txtUrl.Text.Trim();
                if (IsLikelyUrl(text))
                {
                    btnGo_Click(null, null);
                }
                else
                {
                    var engine = (comboSearchEngine.SelectedItem != null) ? comboSearchEngine.SelectedItem.ToString() : "Google";
                    string query = Uri.EscapeDataString(text);
                    string url;
                    if (engine == "Bing")
                        url = "https://www.bing.com/search?q=" + query;
                    else if (engine == "DuckDuckGo")
                        url = "https://duckduckgo.com/?q=" + query;
                    else
                        url = "https://www.google.com/search?q=" + query;

                    CreateNewTab(url, true, incognitoMode);
                }
            }
        }

        private bool IsLikelyUrl(string text)
        {
            return text.Contains(".") || text.StartsWith("http://") || text.StartsWith("https://");
        }

        #endregion

        #region Settings Persistence

        private class AppSettings
        {
            public bool IncognitoByDefault { get; set; } = false;
            public string PreferredSearch { get; set; } = "Google";
            public bool DarkMode { get; set; } = false;
        }

        private AppSettings settings = new AppSettings();

        private void LoadSettings()
        {
            try
            {
                if (File.Exists(settingsFile))
                {
                    var json = File.ReadAllText(settingsFile);
                    settings = JsonConvert.DeserializeObject<AppSettings>(json) ?? new AppSettings();
                }
                // apply
                incognitoMode = settings.IncognitoByDefault;
                btnIncognito.Checked = incognitoMode;

                if (comboSearchEngine.Items.Count == 0)
                {
                    comboSearchEngine.Items.AddRange(new string[] { "Google", "Bing", "DuckDuckGo" });
                }
                comboSearchEngine.SelectedItem = settings.PreferredSearch ?? "Google";

                if (settings.DarkMode)
                {
                    btnDarkMode.Checked = true;
                    ApplyDarkMode();
                }
            }
            catch { settings = new AppSettings(); }
        }

        private void SaveSettings()
        {
            try
            {
                settings.IncognitoByDefault = btnIncognito.Checked;
                settings.PreferredSearch = comboSearchEngine.SelectedItem != null ? comboSearchEngine.SelectedItem.ToString() : "Google";
                settings.DarkMode = btnDarkMode.Checked;
                var json = JsonConvert.SerializeObject(settings, Formatting.Indented);
                File.WriteAllText(settingsFile, json);
            }
            catch { }
        }

        private void btnDarkMode_Click(object sender, EventArgs e)
        {
            ApplyDarkMode();
            SaveSettings();
        }

        private void ApplyDarkMode()
        {
            bool dark = btnDarkMode.Checked;
            this.BackColor = dark ? Color.FromArgb(30, 30, 30) : SystemColors.Control;
            foreach (Control c in this.Controls)
                ApplyDarkToControl(c, dark);
        }

        private void ApplyDarkToControl(Control c, bool dark)
        {
            if (c is ToolStrip)
            {
                // keep default rendering for ToolStrip
            }
            else
            {
                try
                {
                    c.BackColor = dark ? Color.FromArgb(45, 45, 45) : SystemColors.Control;
                    c.ForeColor = dark ? Color.White : SystemColors.ControlText;
                }
                catch { }
            }

            foreach (Control child in c.Controls)
                ApplyDarkToControl(child, dark);
        }

        #endregion

        #region Utility: Keyboard shortcuts

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Ctrl+T new tab
            if (e.Control && e.KeyCode == Keys.T)
            {
                e.Handled = true;
                CreateNewTab(homepage, true, incognitoMode);
            }
            // Ctrl+W close tab
            else if (e.Control && e.KeyCode == Keys.W)
            {
                e.Handled = true;
                btnCloseTab_Click(null, null);
            }
            // Ctrl+Shift+T reopen last - naive approach: restore session
            else if (e.Control && e.Shift && e.KeyCode == Keys.T)
            {
                e.Handled = true;
                RestoreSession();
            }
            // Ctrl+L focus address bar
            else if (e.Control && e.KeyCode == Keys.L)
            {
                e.Handled = true;
                txtUrl.SelectAll();
                txtUrl.Focus();
            }
            // Ctrl+F find (open a small find dialog)
            else if (e.Control && e.KeyCode == Keys.F)
            {
                e.Handled = true;
                ShowFindDialog();
            }
        }

        private void ShowFindDialog()
        {
            using (Form f = new Form())
            {
                f.Text = "Find";
                f.Size = new Size(350, 80);
                TextBox tb = new TextBox { Dock = DockStyle.Fill };
                Button b = new Button { Text = "Find", Dock = DockStyle.Right, Width = 80 };
                b.Click += (s, e) =>
                {
                    var wv = GetActiveWebView();
                    if (wv?.CoreWebView2 != null)
                    {
                        try
                        {
                            string search = tb.Text ?? "";
                            string script = "(function(){ try { return window.find(" + JsonConvert.SerializeObject(search) + "); } catch(e) { return false; } })();";
                            wv.CoreWebView2.ExecuteScriptAsync(script).ContinueWith(t =>
                            {
                                string res = t.Result ?? "";
                                bool found = (res.IndexOf("true", StringComparison.OrdinalIgnoreCase) >= 0);
                                this.BeginInvoke(new Action(() =>
                                {
                                    if (!found)
                                        MessageBox.Show("Text not found.", "Find", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }));
                            });
                        }
                        catch { }
                    }
                };
                f.Controls.Add(tb);
                f.Controls.Add(b);
                f.ShowDialog();
            }
        }

        #endregion

        #region Find & Misc

        private void btnFind_Click(object sender, EventArgs e)
        {
            ShowFindDialog();
        }

        #endregion

        #region Closing

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSession();
            SaveSettings();
            // optionally clear temp incognito user data folders
        }

        #endregion
    }
}
