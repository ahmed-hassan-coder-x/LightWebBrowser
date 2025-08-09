using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace LightWebBrowser
{
    public partial class Form1 : Form
    {
        private string homepage = "https://www.google.com";
        private List<string> history = new List<string>();
        private int historyIndex = -1;

        private List<string> bookmarks = new List<string>();
        private string bookmarksFile = "bookmarks.txt";

        public Form1()
        {
            InitializeComponent();
            webBrowser1.ScriptErrorsSuppressed = true; // منع أخطاء السكريبت تظهر
            webBrowser1.ProgressChanged += WebBrowser1_ProgressChanged;
            webBrowser1.Navigated += WebBrowser1_Navigated;
            webBrowser1.DocumentTitleChanged += WebBrowser1_DocumentTitleChanged;
            LoadBookmarks();
            UpdateBookmarksList();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtUrl.Text = homepage;
            NavigateToPage(homepage);
        }

        private void NavigateToPage(string url)
        {
            try
            {
                if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                    url = "https://" + url; // https أولاً دائماً

                webBrowser1.Navigate(url);
                UpdateHistory(url);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Navigation error: " + ex.Message);
            }
        }

        private void UpdateHistory(string url)
        {
            if (historyIndex == -1 || historyIndex == history.Count - 1)
            {
                history.Add(url);
                historyIndex++;
            }
            else
            {
                history.RemoveRange(historyIndex + 1, history.Count - historyIndex - 1);
                history.Add(url);
                historyIndex++;
            }
            UpdateNavigationButtons();
        }

        private void UpdateNavigationButtons()
        {
            btnBack.Enabled = historyIndex > 0;
            btnForward.Enabled = historyIndex < history.Count - 1;
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            NavigateToPage(txtUrl.Text);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (historyIndex > 0)
            {
                historyIndex--;
                NavigateToPage(history[historyIndex]);
            }
            UpdateNavigationButtons();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (historyIndex < history.Count - 1)
            {
                historyIndex++;
                NavigateToPage(history[historyIndex]);
            }
            UpdateNavigationButtons();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            webBrowser1.Stop();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            NavigateToPage(homepage);
        }

        private void btnBookmark_Click(object sender, EventArgs e)
        {
            if (webBrowser1.Url != null)
            {
                string currentUrl = webBrowser1.Url.ToString();
                if (!bookmarks.Contains(currentUrl))
                {
                    bookmarks.Add(currentUrl);
                    SaveBookmarks();
                    UpdateBookmarksList();
                    MessageBox.Show("Added to bookmarks: " + currentUrl);
                }
                else
                {
                    MessageBox.Show("This page is already bookmarked.");
                }
            }
        }

        private void btnRemoveBookmark_Click(object sender, EventArgs e)
        {
            if (listBoxBookmarks.SelectedItem != null)
            {
                string toRemove = listBoxBookmarks.SelectedItem.ToString();
                bookmarks.Remove(toRemove);
                SaveBookmarks();
                UpdateBookmarksList();
            }
        }

        private void UpdateBookmarksList()
        {
            listBoxBookmarks.Items.Clear();
            foreach (var bm in bookmarks)
                listBoxBookmarks.Items.Add(bm);
        }

        private void SaveBookmarks()
        {
            try
            {
                File.WriteAllLines(bookmarksFile, bookmarks);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save bookmarks: " + ex.Message);
            }
        }

        private void LoadBookmarks()
        {
            try
            {
                if (File.Exists(bookmarksFile))
                {
                    bookmarks = new List<string>(File.ReadAllLines(bookmarksFile));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load bookmarks: " + ex.Message);
            }
        }

        private void webBrowser1_DocumentTitleChanged(object sender, EventArgs e)
        {
            this.Text = webBrowser1.DocumentTitle + " - Light Web Browser";
        }

        private void WebBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            txtUrl.Text = webBrowser1.Url?.ToString() ?? "";
        }

        private void WebBrowser1_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
            if (e.MaximumProgress > 0)
            {
                int progress = (int)(e.CurrentProgress * 100 / e.MaximumProgress);
                progressBar1.Value = Math.Min(Math.Max(progress, 0), 100);
            }
            else
            {
                progressBar1.Value = 0;
            }
        }

        private void txtUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                NavigateToPage(txtUrl.Text);
            }
        }

        private void listBoxBookmarks_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxBookmarks.SelectedItem != null)
            {
                NavigateToPage(listBoxBookmarks.SelectedItem.ToString());
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (webBrowser1.IsBusy)
            {
                var result = MessageBox.Show("A page is still loading. Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            // يمكن تضيف هنا إعدادات مستقبلية
            MessageBox.Show("Settings window is not implemented yet.");
        }
    }
}
