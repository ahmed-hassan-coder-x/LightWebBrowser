using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;

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
            InitWebView2();
            LoadBookmarks();
            UpdateBookmarksList();
        }

        private async void InitWebView2()
        {
            await webView21.EnsureCoreWebView2Async();
            webView21.CoreWebView2.Navigate(homepage);
            webView21.CoreWebView2.DocumentTitleChanged += (s, e) =>
            {
                this.Text = webView21.CoreWebView2.DocumentTitle + " - Light Web Browser";
            };
            webView21.CoreWebView2.NavigationCompleted += (s, e) =>
            {
                txtUrl.Text = webView21.Source?.ToString() ?? "";
                UpdateHistory(txtUrl.Text);
            };
        }

        private void NavigateToPage(string url)
        {
            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
                url = "https://" + url;
            webView21.CoreWebView2.Navigate(url);
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

        private void btnGo_Click(object sender, EventArgs e) => NavigateToPage(txtUrl.Text);

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

        private void btnRefresh_Click(object sender, EventArgs e) => webView21.Reload();

        private void btnStop_Click(object sender, EventArgs e) => webView21.Stop();

        private void btnHome_Click(object sender, EventArgs e) => NavigateToPage(homepage);

        private void btnBookmark_Click(object sender, EventArgs e)
        {
            if (webView21.Source != null)
            {
                string currentUrl = webView21.Source.ToString();
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

        // الأحداث المفقودة سابقاً
        private void btnSettings_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Settings not implemented yet.");
        }

        private void webBrowser1_DocumentTitleChanged(object sender, EventArgs e)
        {
            this.Text = "Light Web Browser";
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveBookmarks();
        }
    }
}
