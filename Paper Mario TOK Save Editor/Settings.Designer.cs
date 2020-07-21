namespace Paper_Mario_TOK_Save_Editor
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.FolderBrowser = new System.Windows.Forms.Button();
            this.BackupRemindersCheck = new System.Windows.Forms.CheckBox();
            this.BackupFilePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AutoBackupCheck = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.FolderBrowser);
            this.groupBox1.Controls.Add(this.BackupRemindersCheck);
            this.groupBox1.Controls.Add(this.BackupFilePath);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.AutoBackupCheck);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(361, 113);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Backups";
            // 
            // FolderBrowser
            // 
            this.FolderBrowser.Location = new System.Drawing.Point(277, 58);
            this.FolderBrowser.Name = "FolderBrowser";
            this.FolderBrowser.Size = new System.Drawing.Size(75, 23);
            this.FolderBrowser.TabIndex = 4;
            this.FolderBrowser.Text = "Browse...";
            this.FolderBrowser.UseVisualStyleBackColor = true;
            this.FolderBrowser.Click += new System.EventHandler(this.FolderBrowser_Click);
            // 
            // BackupRemindersCheck
            // 
            this.BackupRemindersCheck.AutoSize = true;
            this.BackupRemindersCheck.Location = new System.Drawing.Point(6, 86);
            this.BackupRemindersCheck.Name = "BackupRemindersCheck";
            this.BackupRemindersCheck.Size = new System.Drawing.Size(168, 17);
            this.BackupRemindersCheck.TabIndex = 1;
            this.BackupRemindersCheck.Text = "Backup Reminders on Startup";
            this.BackupRemindersCheck.UseVisualStyleBackColor = true;
            this.BackupRemindersCheck.CheckedChanged += new System.EventHandler(this.BackupRemindersCheck_CheckedChanged);
            // 
            // BackupFilePath
            // 
            this.BackupFilePath.Location = new System.Drawing.Point(27, 60);
            this.BackupFilePath.Name = "BackupFilePath";
            this.BackupFilePath.ReadOnly = true;
            this.BackupFilePath.Size = new System.Drawing.Size(244, 20);
            this.BackupFilePath.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Backup Path";
            // 
            // AutoBackupCheck
            // 
            this.AutoBackupCheck.AutoSize = true;
            this.AutoBackupCheck.Location = new System.Drawing.Point(6, 19);
            this.AutoBackupCheck.Name = "AutoBackupCheck";
            this.AutoBackupCheck.Size = new System.Drawing.Size(167, 17);
            this.AutoBackupCheck.TabIndex = 1;
            this.AutoBackupCheck.Text = "Automatically Create Backups";
            this.AutoBackupCheck.UseVisualStyleBackColor = true;
            this.AutoBackupCheck.CheckedChanged += new System.EventHandler(this.AutoBackupCheck_CheckedChanged);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 137);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Settings";
            this.Text = "Settings | Paper Mario: The Origami King Save Editor (v0.7-Dev)";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Settings_FormClosed);
            this.Load += new System.EventHandler(this.Settings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox AutoBackupCheck;
        private System.Windows.Forms.CheckBox BackupRemindersCheck;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button FolderBrowser;
        private System.Windows.Forms.TextBox BackupFilePath;
    }
}