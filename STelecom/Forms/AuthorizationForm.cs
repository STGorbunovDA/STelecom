using Microsoft.Win32;
using MySql.Data.MySqlClient;
using STelecom.Classes.Cheack;
using STelecom.Classes.Other;
using STelecom.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STelecom
{
    public partial class AuthorizationForm : Form
    {
        public AuthorizationForm()
        {
            if (!InstanceChecker.TakeMemory())
                StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        void AuthorizationForm_Load(object sender, EventArgs e)
        {
            txbPassword.PasswordChar = '*';
            hidePassword.Visible = false;
            txbLogin.MaxLength = 100;
            txbPassword.MaxLength = 32;
            //добавление логина и пароля из реестра
            try
            {
                RegistryKey reg1 = Registry.CurrentUser.OpenSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Login_Password");
                if (reg1 != null)
                {
                    RegistryKey currentUserKey = Registry.CurrentUser;
                    RegistryKey helloKey = currentUserKey.OpenSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Login_Password");
                    txbLogin.Text = helloKey.GetValue("Login").ToString();
                    txbPassword.Text = helloKey.GetValue("Password").ToString();
                    helloKey.Close();
                }
            }
            catch
            {
            }
        }

        #region взаимодействие 
        void ClearControlForm_Click(object sender, EventArgs e)
        {
            txbLogin.Text = String.Empty;
            txbPassword.Text = String.Empty;
        }

        void OpenPassword_Click(object sender, EventArgs e)
        {
            txbPassword.UseSystemPasswordChar = true;
            hidePassword.Visible = true;
            openPassword.Visible = false;
        }

        void HidePassword_Click(object sender, EventArgs e)
        {
            txbPassword.UseSystemPasswordChar = false;
            hidePassword.Visible = false;
            openPassword.Visible = true;
        }
        #endregion

        #region подсветка

        void ClearControlForm_MouseEnter(object sender, EventArgs e)
        {
            clearControlForm.ForeColor = Color.White;
        }

        void ClearControlForm_MouseLeave(object sender, EventArgs e)
        {
            clearControlForm.ForeColor = Color.Black;
        }

        void OpenPassword_MouseEnter(object sender, EventArgs e)
        {
            openPassword.ForeColor = Color.White;
        }

        void OpenPassword_MouseLeave(object sender, EventArgs e)
        {
            openPassword.ForeColor = Color.Black;
        }

        void HidePassword_MouseEnter(object sender, EventArgs e)
        {
            hidePassword.ForeColor = Color.White;
        }

        void HidePassword_MouseLeave(object sender, EventArgs e)
        {
            hidePassword.ForeColor = Color.Black;
        }
        #endregion

        void BtnAuthorization_Click(object sender, EventArgs e)
        {
            if (!InternetCheck.CheackSkyNET())
                return;

            string loginUser = txbLogin.Text;
            string passUser = txbPassword.Text;
            //string passUser = Encryption.EncryptPlainTextToCipherText(txbPassword.Text);
            string querystring = $"SELECT id, login, password, is_users	FROM users " +
                $"WHERE login = '{loginUser}' AND password = '{passUser}'";
            using (MySqlCommand command = new MySqlCommand(querystring, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    if (table.Rows.Count == 1)
                    {
                        CheckUser user = new CheckUser(table.Rows[0].ItemArray[1].ToString(), table.Rows[0].ItemArray[3].ToString());

                        DB.GetInstance.CloseConnection();
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин и пароль");
                        DB.GetInstance.CloseConnection();
                    }
                }
            }
        }
    }
}
