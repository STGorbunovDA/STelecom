using STelecom.Classes.Cheack;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace STelecom.Forms
{
    public partial class MenuForm : Form
    {
        private readonly CheckUser _user;
        public MenuForm(CheckUser user)
        {
            InitializeComponent();
            _user = user;
            IsAdmin();
            tutorialEngineers.ForeColor = Color.FromArgb(56, 56, 56);
            sectionForeman.ForeColor = Color.FromArgb(56, 56, 56);
            comparison.ForeColor = Color.FromArgb(56, 56, 56);
        }
        void IsAdmin()
        {
            if (_user.IsAdmin == "Admin")
            {
                settingAdmin.Visible = true;
                director.Visible = true;
            }
            if (_user.IsAdmin == "Руководитель")
                director.Visible = true;
        }
        void MenuForm_Load(object sender, EventArgs e)
        {
            if (!InternetCheck.CheackSkyNET())
                return;

        }

        #region подсветка
        void TutorialEngineers_MouseEnter(object sender, EventArgs e)
        {
            tutorialEngineers.ForeColor = Color.White;
        }
        void TutorialEngineers_MouseLeave(object sender, EventArgs e)
        {
            tutorialEngineers.ForeColor = Color.Black;
        }
        void SectionForeman_MouseEnter(object sender, EventArgs e)
        {
            sectionForeman.ForeColor = Color.White;
        }
        void SectionForeman_MouseLeave(object sender, EventArgs e)
        {
            sectionForeman.ForeColor = Color.Black;
        }
        void Comparison_MouseEnter(object sender, EventArgs e)
        {
            comparison.ForeColor = Color.White;
        }
        void Comparison_MouseLeave(object sender, EventArgs e)
        {
            comparison.ForeColor = Color.Black;
        }
        void Director_MouseEnter(object sender, EventArgs e)
        {
            director.ForeColor = Color.White;
        }
        void Director_MouseLeave(object sender, EventArgs e)
        {
            director.ForeColor = Color.Black;
        }
        #endregion

        void SettingAdmin_Click(object sender, EventArgs e)
        {
            using (SettingAdminForm SettingAdmin = new SettingAdminForm())
            {
                this.Hide();
                SettingAdmin.ShowDialog();
                this.Show();
            }
        }
    }
}
