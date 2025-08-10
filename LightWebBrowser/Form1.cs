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

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            txtUrl.Text = homepage;
            await InitWebView2Async();
        }

        private async System.Threading.Tasks.Task InitWebView2Async()
        {
            try
            {
                if (webView21.CoreWebView2 == null)
                {
                    await webView21.EnsureCoreWebView2Async();
                }

                if (webView21.CoreWebView2 != null)
                {
                    webView21.CoreWebView2.DocumentTitleChanged += CoreWebView2_DocumentTitleChanged;
                    webView21.CoreWebView2.NavigationCompleted += CoreWebView2_NavigationCompleted;
                    webView21.CoreWebView2.NewWindowRequested += CoreWebView2_NewWindowRequested;
                    webView21.Source = new Uri(homepage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("WebView2 initialization failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CoreWebView2_NewWindowRequested(object sender, CoreWebView2NewWindowRequestedEventArgs e)
        {
            // Open popups in the same WebView
            if (!string.IsNullOrEmpty(e.Uri))
            {
                webView21.CoreWebView2.Navigate(e.Uri);
                e.Handled = true;
            }
        }

        private void CoreWebView2_DocumentTitleChanged(object sender, object e)
        {
            try
            {
                this.Text = webView21.CoreWebView2.DocumentTitle + " - Light Web Browser";
            }
            catch
            {
                this.Text = "Light Web Browser";
            }
        }

        private void CoreWebView2_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            try
            {
                txtUrl.Text = webView21.Source?.ToString() ?? "";
                UpdateHistory(txtUrl.Text);
                progressBar1.Value = 0;
            }
            catch { }
        }

        private void NavigateToPage(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return;

            if (!url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) &&
                !url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                url = "https://" + url;
            }

            try
            {
                if (webView21.CoreWebView2 != null)
                {
                    webView21.CoreWebView2.Navigate(url);
                }
                else
                {
                    webView21.Source = new Uri(url);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Navigation error: " + ex.Message);
            }
        }

        private void UpdateHistory(string url)
        {
            if (string.IsNullOrEmpty(url))
                return;

            if (historyIndex == -1 || historyIndex == history.Count - 1)
            {
                history.Add(url);
                historyIndex++;
            }
            else
            {
                if (historyIndex + 1 < history.Count)
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
            NavigateToPage(txtUrl.Text.Trim());
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
            try
            {
                webView21.Reload();
            }
            catch
            {
                // fallback
                if (!string.IsNullOrWhiteSpace(txtUrl.Text))
                    NavigateToPage(txtUrl.Text);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                webView21.CoreWebView2?.Stop();
            }
            catch
            {
                // no-op
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            NavigateToPage(homepage);
        }
        
        private void btnSettings_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Settings not implemented yet.", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                NavigateToPage(txtUrl.Text.Trim());
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Do you want to exit",
                "Confirm exit",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );
        
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
