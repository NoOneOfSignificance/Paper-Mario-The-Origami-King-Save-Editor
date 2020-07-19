using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paper_Mario_TOK_Save_Editor
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            GetSettings();
        }

        FolderBrowserDialog SelectFolder = new FolderBrowserDialog();

        private void AutoBackupCheck_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AutoBackups = (AutoBackupCheck.Checked);
        }

        public void GetSettings()
        {
            AutoBackupCheck.Checked = Properties.Settings.Default.AutoBackups;
        }

        private void Settings_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
