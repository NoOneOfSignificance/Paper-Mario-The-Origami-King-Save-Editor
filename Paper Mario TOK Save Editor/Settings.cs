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
            if (Properties.Settings.Default.AutoBackups == false)
            {
                Properties.Settings.Default.BackupDestination = "";
            }
        }

        FolderBrowserDialog SelectFolder = new FolderBrowserDialog();

        private void AutoBackupCheck_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AutoBackups = (AutoBackupCheck.Checked ? true : false);
            BackupFileBrowser.Enabled = (AutoBackupCheck.Checked ? true : false);
            BackupFilePath.Enabled = (AutoBackupCheck.Checked ? true : false);

            if (AutoBackupCheck.Checked == false)
            {
                BackupFilePath.Text = "";
            }
        }
        private void BackupFileBrowser_Click(object sender, EventArgs e)
        {
            if (SelectFolder.ShowDialog() == DialogResult.OK)
            {
                BackupFilePath.Text = SelectFolder.SelectedPath;
                Properties.Settings.Default.BackupDestination = SelectFolder.SelectedPath;
            }
        }

        private void SettingsApplyButton_Click(object sender, EventArgs e)
        {
            if (BackupFilePath.Text == "" && AutoBackupCheck.Checked)
            {
                MessageBox.Show("Please select a Backup File Path first");
                AutoBackupCheck.Checked = false;
            }
            else
            {
                Properties.Settings.Default.Save();
            }
        }

        public void GetSettings()
        {
            AutoBackupCheck.Checked = Properties.Settings.Default.AutoBackups;
            BackupFilePath.Text = Properties.Settings.Default.BackupDestination;
        }        
    }
}
