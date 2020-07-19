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

        private void BackupRemindersCheck_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.BackupReminder = (BackupRemindersCheck.Checked);
        }

        private void FolderBrowser_Click(object sender, EventArgs e)
        {
            if (SelectFolder.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.BackupPath = SelectFolder.SelectedPath;
                BackupFilePath.Text = SelectFolder.SelectedPath;
            }
        }

        public void GetSettings()
        {
            AutoBackupCheck.Checked = Properties.Settings.Default.AutoBackups;
            BackupRemindersCheck.Checked = Properties.Settings.Default.BackupReminder;
            BackupFilePath.Text = Properties.Settings.Default.BackupPath;
        }

        private void Settings_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Save();
        }
    }
}
