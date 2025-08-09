using System;
using System.Windows.Forms;

namespace LightWebBrowser
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Save settings logic here
            MessageBox.Show("Settings saved!");
            this.Close();
        }
    }
}
