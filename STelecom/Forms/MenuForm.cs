using MySql.Data.MySqlClient;
using STelecom.Classes.Cheack;
using STelecom.Classes.Other;
using STelecom.DataBase;
using System;
using System.Data;
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
            tutorialEngineers.ForeColor = Color.FromArgb(56, 56, 56);
            sectionForeman.ForeColor = Color.FromArgb(56, 56, 56);
            comparison.ForeColor = Color.FromArgb(56, 56, 56);
        }
        void MenuForm_Load(object sender, EventArgs e)
        {
            string[] queryPost = { "settingBrigadesSelect_1", "settingBrigadesSelect_2", 
                "settingBrigadesSelect_3", "settingBrigadesSelect_4" };
            if (!InternetCheck.CheackSkyNET())
                return;
            DateTime Date = DateTime.Now;
            string inputDate = Date.ToString("yyyy-MM-dd HH:mm:ss");
            DateTime dateTimeInput = CheckDateTimeInputLogUserDatabase(_user.Login);
            if (Date.ToString("yyyy-MM-dd") != dateTimeInput.ToString("yyyy-MM-dd"))
            {
                using (MySqlCommand command = new MySqlCommand("logUsersInsert_1", DB.GetInstance.GetConnection()))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue($"user", _user.Login);
                    command.Parameters.AddWithValue($"dateTimeInput", inputDate);
                    DB.GetInstance.OpenConnection();
                    command.ExecuteNonQuery();
                    DB.GetInstance.CloseConnection();
                }
            }
            if (_user.Post == "Admin")
            {
                settingAdmin.Visible = true;
                settingAdmin.Enabled = true;
                tutorialEngineers.Enabled = true;
                sectionForeman.Enabled = true;
                comparison.Enabled = true;
                settingBrigades.Enabled = true;
            }
            else if (_user.Post == "Руководитель")
            {
                tutorialEngineers.Enabled = true;
                sectionForeman.Enabled = true;
                comparison.Enabled = true;
                settingBrigades.Enabled = true;
            }
            else if (_user.Post == "Начальник участка")
            {
                if (CheckPostUsersSettingBrigades(queryPost[0]))
                {
                    tutorialEngineers.Enabled = true;
                    sectionForeman.Enabled = true;
                }
                else MessageBox.Show("Сообщи руководителю что-бы сформировал тебя в бригаду");

            }
            else if (_user.Post == "Инженер")
            {
                if (CheckPostUsersSettingBrigades(queryPost[1]))
                {
                    tutorialEngineers.Enabled = true;
                    sectionForeman.Enabled = true;
                }
                else MessageBox.Show("Сообщи руководителю что-бы сформировал тебя в бригаду");
            }
            else if (_user.Post == "Куратор")
            {
                if (CheckPostUsersSettingBrigades(queryPost[2]))
                {
                    comparison.Enabled = true;
                    sectionForeman.Enabled = true;
                }
                else MessageBox.Show("Сообщи руководителю что-бы сформировал тебя в бригаду");
            }
            else if (_user.Post == "Дирекция связи")
            {
                if (CheckPostUsersSettingBrigades(queryPost[3]))
                {
                    using (WorkForm workForm = new WorkForm(_user))
                    {
                        this.Hide();
                        workForm.ShowDialog();
                        this.Show();
                    }
                }   
                else MessageBox.Show("Сообщи руководителю что-бы сформировал тебя в бригаду");
            }
        }

        /// <summary>
        /// Проверка должности пользователя
        /// </summary>
        /// <param name="queryPost">Хранимая процедура в массиве</param>
        /// <returns></returns>
        bool CheckPostUsersSettingBrigades(string queryPost)
        {
            using (MySqlCommand command = new MySqlCommand(queryPost, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue($"userLogin", _user.Login);
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    if (table.Rows.Count >= 1) return true;
                    else return false;
                }
            }
        }    
        /// <summary>
        /// Получаем дату регистрации входа пользователя для табеля
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>
        DateTime CheckDateTimeInputLogUserDatabase(string user)
        {
            DateTime Date = DateTime.Now;
            using (MySqlCommand command = new MySqlCommand("logUsersSelect_2", DB.GetInstance.GetConnection()))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue($"userLog", user);
                command.Parameters.AddWithValue($"date", Date.ToString("yyyy-MM-dd"));
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    if (table.Rows.Count > 0) return Convert.ToDateTime(table.Rows[table.Rows.Count - 1].ItemArray[0]);
                    else return DateTime.MinValue;
                }
            }
        }
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
        void MenuForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
        void MenuForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = FormClose.GetInstance.FClose(_user.Login);
        }
        void TutorialEngineers_Click(object sender, EventArgs e)
        {
            using (TutorialEngineerForm tutorialEngineer = new TutorialEngineerForm(_user))
            {
                this.Hide();
                tutorialEngineer.ShowDialog();
                this.Show();
            }
        }
        void SectionForeman_Click(object sender, EventArgs e)
        {
            using (WorkForm workForm = new WorkForm(_user))
            {
                this.Hide();
                workForm.ShowDialog();
                this.Show();
            }
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
    }
}
