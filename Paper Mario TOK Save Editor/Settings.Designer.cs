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
            this.BackupFilePath = new System.Windows.Forms.TextBox();
            this.BackupFileBrowser = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.AutoBackupCheck = new System.Windows.Forms.CheckBox();
            this.SettingsApplyButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.BackupFilePath);
            this.groupBox1.Controls.Add(this.BackupFileBrowser);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.AutoBackupCheck);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(278, 94);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Backups";
            // 
            // BackupFilePath
            // 
            this.BackupFilePath.Location = new System.Drawing.Point(27, 61);
            this.BackupFilePath.Name = "BackupFilePath";
            this.BackupFilePath.Size = new System.Drawing.Size(189, 20);
            this.BackupFilePath.TabIndex = 4;
            // 
            // BackupFileBrowser
            // 
            this.BackupFileBrowser.Location = new System.Drawing.Point(130, 34);
            this.BackupFileBrowser.Name = "BackupFileBrowser";
            this.BackupFileBrowser.Size = new System.Drawing.Size(86, 23);
            this.BackupFileBrowser.TabIndex = 3;
            this.BackupFileBrowser.Text = "Choose Folder";
            this.BackupFileBrowser.UseVisualStyleBackColor = true;
            this.BackupFileBrowser.Click += new System.EventHandler(this.BackupFileBrowser_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Backup Destination";
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
            // SettingsApplyButton
            // 
            this.SettingsApplyButton.Location = new System.Drawing.Point(204, 112);
            this.SettingsApplyButton.Name = "SettingsApplyButton";
            this.SettingsApplyButton.Size = new System.Drawing.Size(86, 23);
            this.SettingsApplyButton.TabIndex = 1;
            this.SettingsApplyButton.Text = "Apply Settings";
            this.SettingsApplyButton.UseVisualStyleBackColor = true;
            this.SettingsApplyButton.Click += new System.EventHandler(this.SettingsApplyButton_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 144);
            this.Controls.Add(this.SettingsApplyButton);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Settings";
            this.Text = "Settings | Paper Mario: The Origami King Save Editor (v0.7-Dev)";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox AutoBackupCheck;
        private System.Windows.Forms.Button BackupFileBrowser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox BackupFilePath;
        private System.Windows.Forms.Button SettingsApplyButton;
    }
}