using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
            webBrowser1.ScriptErrorsSuppressed = true; // Suppress script errors
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtUrl.Text = homepage;
            NavigateToPage(homepage);
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            NavigateToPage(txtUrl.Text);
        }

        private void NavigateToPage(string url)
        {
            try
            {
                if (!url.StartsWith("http"))
                    url = "http://" + url;

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
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (historyIndex > 0)
            {
                historyIndex--;
                NavigateToPage(history[historyIndex]);
            }
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (historyIndex < history.Count - 1)
            {
                historyIndex++;
                NavigateToPage(history[historyIndex]);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            NavigateToPage(homepage);
        }

        private void btnBookmark_Click(object sender, EventArgs e)
        {
            string currentUrl = webBrowser1.Url.ToString();
            MessageBox.Show("Bookmarked: " + currentUrl);
            // Implement a more complex bookmarking system here
        }

        private void webBrowser1_DocumentTitleChanged(object sender, EventArgs e)
        {
            this.Text = webBrowser1.DocumentTitle + " - Light Web Browser";
        }

        private void txtUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // Suppress ding sound on Enter
                NavigateToPage(txtUrl.Text);
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            // Open settings dialog (you need to implement this)
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.ShowDialog();
        }
    }
}
