using MySql.Data.MySqlClient;
using STelecom.Classes.Cheack;
using STelecom.Classes.Other;
using STelecom.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STelecom.Forms
{
    public partial class SettingAdminForm : Form
    {
        int selectedRow;

        #region состояние Rows
        enum RowState
        {
            New,
            Deleted
        }
        #endregion


        public SettingAdminForm()
        {
            InitializeComponent();
        }
        private void CreateColums()
        {
            dataGridView1.Columns.Add("id", "№");
            dataGridView1.Columns.Add("login", "Логин");
            dataGridView1.Columns.Add("password", "Пароль");
            dataGridView1.Columns.Add("is_users", "Должность");
            dataGridView1.Columns.Add("IsNew", String.Empty);
            dataGridView1.Columns[4].Visible = false;
        }
        void ReedSingleRow(DataGridView dgw, IDataRecord record)
        {
            dataGridView1.Invoke((MethodInvoker)(() => dgw.Rows.Add(record.GetInt32(0), record.GetString(1),
                record.GetString(2), record.GetString(3), RowState.New)));
        }
        void RefreshDataGrid(DataGridView dgw)
        {
            if (InternetCheck.CheackSkyNET())
            {
                var myCulture = new CultureInfo("ru-RU");
                myCulture.NumberFormat.NumberDecimalSeparator = ".";
                Thread.CurrentThread.CurrentCulture = myCulture;
                dgw.Rows.Clear();
                using (MySqlCommand command = new MySqlCommand("usersSelectFull", DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                                ReedSingleRow(dgw, reader);
                            reader.Close();
                        }
                    }
                    command.ExecuteNonQuery();
                    DB.GetInstance.CloseConnection();
                }
                dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dataGridView1.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
            }
        }
        void SettingAdminForm_Load(object sender, EventArgs e)
        {
            if (!InternetCheck.CheackSkyNET())
                return;
            CreateColums();
            RefreshDataGrid(dataGridView1);
        }    
        void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.ReadOnly = false;
            selectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                txbId.Text = row.Cells[0].Value.ToString();
                txbLogin.Text = row.Cells[1].Value.ToString();
                txbPass.Text = Encryption.DecryptCipherTextToPlainText(row.Cells[2].Value.ToString());
                cmbIsUsersPost.Text = row.Cells[3].Value.ToString();
            }
        }
        void DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 0)
                e.Cancel = true;
        }
        void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (InternetCheck.CheackSkyNET())
                RefreshDataGrid(dataGridView1);
        }
        void PicbClear_Click(object sender, EventArgs e)
        {
            foreach (Control control in panel2.Controls)
                if (control is TextBox)
                    control.Text = String.Empty;
        }
        void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!InternetCheck.CheackSkyNET())
                return;
            string loginUser = txbLogin.Text;
            if (!loginUser.Contains("-"))
            {
                if (!Regex.IsMatch(loginUser, @"^[А-ЯЁ][а-яё]*(([\s]+[А-Я][\.]+[А-Я]+[\.])$)"))
                {
                    MessageBox.Show("Введите корректно поле \"Логин\"\nP.s. пример: Иванов В.В.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txbLogin.Select();
                    return;
                }
            }
            else if (loginUser.Contains("-"))
            {
                if (!Regex.IsMatch(loginUser, @"^[А-ЯЁ][а-яё]*(([\-][А-Я][а-яё]*[\s]+[А-Я]+[\.]+[А-Я]+[\.])$)"))
                {
                    MessageBox.Show("Введите корректно поле \"Логин\"\nP.s. пример: Иванов-Петров В.В.", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txbLogin.Select();
                    return;
                }
            }
            string passUser = Encryption.EncryptPlainTextToCipherText(txbPass.Text);
            if (UserCheck(loginUser, passUser))
            {
                MessageBox.Show("Такой пользователь уже существует!");
                return;
            }
            if (String.IsNullOrEmpty(cmbIsUsersPost.Text))
            {
                MessageBox.Show("Заполни должность!");
                return;
            }

            using (MySqlCommand command = new MySqlCommand("usersInsertLoginPassword", DB.GetInstance.GetConnection()))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue($"loginUser", loginUser);
                command.Parameters.AddWithValue($"passUser", passUser);
                command.Parameters.AddWithValue($"post", cmbIsUsersPost.Text);
                DB.GetInstance.OpenConnection();
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Аккаунт успешно создан!");
                    RefreshDataGrid(dataGridView1);
                }
                else MessageBox.Show("Аккаунт не создан!");
                DB.GetInstance.CloseConnection();
            }
        }
        bool UserCheck(string loginUser, string passUser)
        {
            if (!InternetCheck.CheackSkyNET())
                return true;
            using (MySqlCommand command = new MySqlCommand($"usersSelectLoginPassword", DB.GetInstance.GetConnection()))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue($"loginUser", loginUser);
                command.Parameters.AddWithValue($"passUser", passUser);
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    if (table.Rows.Count == 1) return true;
                    else return false;
                }
            }
        }

        
    }
}
