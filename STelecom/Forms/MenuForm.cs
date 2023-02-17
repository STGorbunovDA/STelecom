﻿using STelecom.Classes.Cheack;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace STelecom.Forms
{
    public partial class MenuForm : Form
    {
        readonly CheckUser _user;
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
            if (_user.Post == "Admin")
            {
                settingAdmin.Visible = true;
                settingBrigades.Visible = true;
            }
            if (_user.Post == "Руководитель")
                settingBrigades.Visible = true;
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
            settingBrigades.ForeColor = Color.White;
        }
        void Director_MouseLeave(object sender, EventArgs e)
        {
            settingBrigades.ForeColor = Color.Black;
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

        void SettingBrigades_Click(object sender, EventArgs e)
        {
            using (RegistrationStaff registrationStaff = new RegistrationStaff(_user))
            {
                this.Hide();
                registrationStaff.ShowDialog();
                this.Show();
            }
        }
    }
}
