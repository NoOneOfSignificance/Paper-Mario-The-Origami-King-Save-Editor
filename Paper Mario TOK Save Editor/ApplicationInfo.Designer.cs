namespace Paper_Mario_TOK_Save_Editor
{
    partial class ApplicationInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApplicationInfo));
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.RepoLink = new System.Windows.Forms.LinkLabel();
            this.ReportIssue = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Credits";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(275, 65);
            this.label4.TabIndex = 3;
            this.label4.Text = "zSupremoz - Main Developer\r\nBlue - Teaching me how to recalculate the save file h" +
    "ash\r\nhaiphongn - Adding on to the Inventory reading function\r\n\r\nDamienG - CRC32 " +
    "class";
            // 
            // RepoLink
            // 
            this.RepoLink.AutoSize = true;
            this.RepoLink.Location = new System.Drawing.Point(13, 108);
            this.RepoLink.Name = "RepoLink";
            this.RepoLink.Size = new System.Drawing.Size(69, 13);
            this.RepoLink.TabIndex = 5;
            this.RepoLink.TabStop = true;
            this.RepoLink.Text = "GitHub Repo";
            this.RepoLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.RepoLink_LinkClicked);
            // 
            // ReportIssue
            // 
            this.ReportIssue.AutoSize = true;
            this.ReportIssue.Location = new System.Drawing.Point(88, 108);
            this.ReportIssue.Name = "ReportIssue";
            this.ReportIssue.Size = new System.Drawing.Size(82, 13);
            this.ReportIssue.TabIndex = 6;
            this.ReportIssue.TabStop = true;
            this.ReportIssue.Text = "Report an Issue";
            this.ReportIssue.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ReportIssue_LinkClicked);
            // 
            // ApplicationInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 132);
            this.Controls.Add(this.ReportIssue);
            this.Controls.Add(this.RepoLink);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ApplicationInfo";
            this.Text = "About";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel RepoLink;
        private System.Windows.Forms.LinkLabel ReportIssue;
    }
}