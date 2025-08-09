using System;
using System.Windows.Forms;

namespace LightWebBrowser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            webBrowser1.ScriptErrorsSuppressed = true; // اخفاء اخطاء السكريبت
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtUrl.Text = "https://www.google.com";
            webBrowser1.Navigate(txtUrl.Text);
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            NavigateToPage();
        }

        private void NavigateToPage()
        {
            try
            {
                if (!txtUrl.Text.StartsWith("http"))
                    webBrowser1.Navigate("http://" + txtUrl.Text);
                else
                    webBrowser1.Navigate(txtUrl.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Navigation error: " + ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoBack)
                webBrowser1.GoBack();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (webBrowser1.CanGoForward)
                webBrowser1.GoForward();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }

        private void webBrowser1_DocumentTitleChanged(object sender, EventArgs e)
        {
            this.Text = webBrowser1.DocumentTitle + " - Light Web Browser";
        }

        private void txtUrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // لمنع صوت الدينغ عند الضغط على Enter
                NavigateToPage();
            }
        }
    }
}
