using System.Windows.Forms;
using System.Diagnostics;

namespace Paper_Mario_TOK_Save_Editor
{
    public partial class ApplicationInfo : Form
    {
        public ApplicationInfo()
        {
            InitializeComponent();
        }

        private void RepoLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/zSupremoz/Paper-Mario-The-Origami-King-Save-Editor");
        }
    }
}
