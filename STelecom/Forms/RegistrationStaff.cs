using MySql.Data.MySqlClient;
using STelecom.Classes.Cheack;
using STelecom.DataBase;
using System;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace STelecom.Forms
{
    public partial class RegistrationStaff : Form
    {
        int selectedRow;
        readonly CheckUser _user;
        #region состояние Rows
        enum RowState
        {
            New,
            Deleted
        }
        #endregion
        public RegistrationStaff(CheckUser user)
        {
            InitializeComponent();
            _user = user;
        }
        private void CreateColums()
        {
            dataGridView1.Columns.Add("id", "№");
            dataGridView1.Columns.Add("sectionForeman", "Начальник участка");
            dataGridView1.Columns.Add("engineer", "Инженер");
            dataGridView1.Columns.Add("attorney", "Доверенность");
            dataGridView1.Columns.Add("road", "Дорога");
            dataGridView1.Columns.Add("numberPrintDocument", "№ печати");
            dataGridView1.Columns.Add("curator", "Куратор");
            dataGridView1.Columns.Add("radioCommunicationDirectorate", "Представитель дирекции");
            dataGridView1.Columns.Add("IsNew", String.Empty);
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[0].Width = 45;
            dataGridView1.Columns[8].Width = 80;
        }
        void ReedSingleRow(DataGridView dgw, IDataRecord record)
        {
            dataGridView1.Invoke((MethodInvoker)(() => dgw.Rows.Add(record.GetInt32(0), record.GetString(1),
                record.GetString(2), record.GetString(3), record.GetString(4), record.GetString(5), record.GetString(6),
                record.GetString(7), RowState.New)));
        }
        void RefreshDataGrid(DataGridView dgw)
        {
            if (!InternetCheck.CheackSkyNET())
                return;
            var myCulture = new CultureInfo("ru-RU");
            myCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = myCulture;
            dgw.Rows.Clear();
            using (MySqlCommand command = new MySqlCommand("settingBrigadesSelectFull", DB.GetInstance.GetConnection()))
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
        void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.ReadOnly = false;
            selectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                txbId.Text = row.Cells[0].Value.ToString();
                cmbSectionForemans.Text = row.Cells[1].Value.ToString();
                cmbEngineers.Text = row.Cells[2].Value.ToString();
                txbAttorney.Text = row.Cells[3].Value.ToString();
                cmbRoad.Text = row.Cells[4].Value.ToString();
                txbNumberPrintDocument.Text = row.Cells[5].Value.ToString();
                cmbCurator.Text = row.Cells[6].Value.ToString();
                cmbRadioCommunicationDirectorate.Text = row.Cells[7].Value.ToString();
            }
        }
        void DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 0)
                e.Cancel = true;
        }
        void RegistrationStaff_Load(object sender, EventArgs e)
        {
            using (MySqlCommand command = new MySqlCommand("usersSelectSectionForeman", DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable table = new DataTable();
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        cmbSectionForemans.DataSource = table;
                        cmbSectionForemans.ValueMember = "id";
                        cmbSectionForemans.DisplayMember = "login";
                    }
                    else cmbSectionForemans.Text = String.Empty;
                }
            }
            using (MySqlCommand command = new MySqlCommand("usersSelectEngineer", DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable table = new DataTable();
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        cmbEngineers.DataSource = table;
                        cmbEngineers.ValueMember = "id";
                        cmbEngineers.DisplayMember = "login";
                    }
                    else cmbEngineers.Text = String.Empty;
                }
            }
            using (MySqlCommand command = new MySqlCommand("usersSelectCurator", DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable table = new DataTable();
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        cmbCurator.DataSource = table;
                        cmbCurator.ValueMember = "id";
                        cmbCurator.DisplayMember = "login";
                    }
                    else cmbCurator.Text = String.Empty;
                }
            }
            using (MySqlCommand command = new MySqlCommand("usersSelectRadioCommunicationDirectorate", DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable table = new DataTable();
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        cmbRadioCommunicationDirectorate.DataSource = table;
                        cmbRadioCommunicationDirectorate.ValueMember = "id";
                        cmbRadioCommunicationDirectorate.DisplayMember = "login";
                    }
                    else cmbRadioCommunicationDirectorate.Text = String.Empty;
                }
            }

            if (String.IsNullOrWhiteSpace(cmbRadioCommunicationDirectorate.Text))
                MessageBox.Show("Добавьте представителя дирекции связи!");
            if (String.IsNullOrWhiteSpace(cmbCurator.Text))
                MessageBox.Show("Добавьте куратора!");
            if (String.IsNullOrWhiteSpace(cmbRoad.Text))
                cmbRoad.Text = cmbRoad.Items[0].ToString();
            if (String.IsNullOrWhiteSpace(cmbEngineers.Text))
                MessageBox.Show("Добавьте инженера!");
            if (String.IsNullOrWhiteSpace(cmbSectionForemans.Text))
                MessageBox.Show("Добавьте начальника участка!");
            CreateColums();
            RefreshDataGrid(dataGridView1);
        }
        void PicbUpdate_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
        }
        void PicbClear_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(cmbSectionForemans.Text))
            {
                MessageBox.Show("Поле \"Начальник\" не должен быть пустым, добавьте начальника участка", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (String.IsNullOrWhiteSpace(cmbEngineers.Text))
            {
                MessageBox.Show("Поле \"Инженер\" не должен быть пустым, добавьте инженера", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (String.IsNullOrWhiteSpace(cmbCurator.Text))
            {
                MessageBox.Show("Поле \"Куратор\" не должен быть пустым, добавьте куратора", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (String.IsNullOrWhiteSpace(cmbCurator.Text))
            {
                MessageBox.Show("Поле \"Представитель Дирекции связи\" не должен быть пустым, добавьте представителя диреции связи", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            cmbSectionForemans.Text = cmbSectionForemans.Items[0].ToString();
            cmbEngineers.Text = cmbEngineers.Items[0].ToString();
            cmbRoad.Text = cmbRoad.Items[0].ToString();
            cmbRoad.Text = cmbRoad.Items[0].ToString();
            cmbRadioCommunicationDirectorate.Text = cmbRadioCommunicationDirectorate.Items[0].ToString();
            txbAttorney.Clear();
            txbNumberPrintDocument.Clear();
        }
        void BtnAddRegistrationStaff_Click(object sender, EventArgs e)
        {
            if (!InternetCheck.CheackSkyNET())
                return;
            var re = new Regex(Environment.NewLine);
            txbAttorney.Text = re.Replace(txbAttorney.Text, " ");
            txbAttorney.Text.Trim();
            if (String.IsNullOrWhiteSpace(cmbSectionForemans.Text))
            {
                MessageBox.Show("Поле \"Начальник\" не должен быть пустым, добавьте начальника участка", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (String.IsNullOrWhiteSpace(cmbEngineers.Text))
            {
                MessageBox.Show("Поле \"Инженер\" не должен быть пустым, добавьте инженера", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (String.IsNullOrWhiteSpace(cmbRoad.Text))
            {
                MessageBox.Show("Поле \"Дорога\" не должна быть пустым, добавьте дорогу", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (String.IsNullOrWhiteSpace(cmbCurator.Text))
            {
                MessageBox.Show("Поле \"Куратор\" не должно быть пустым, добавьте куратора", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (String.IsNullOrWhiteSpace(cmbRadioCommunicationDirectorate.Text))
            {
                MessageBox.Show("Поле \"Представитель дирекции связи\" не должно быть пустым, добавьте представителя дирекции связи", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!Regex.IsMatch(txbAttorney.Text, @"^[0-9]{1,}[\/][0-9]{1,}[\s][о][т][\s][0-9]{2,2}[\.][0-9]{2,2}[\.][2][0][0-9]{2,2}[\s][г][о][д][а]$"))
            {
                MessageBox.Show("Введите корректно \"Доверенность\"\n P.s. Пример: 53/53 от 10.01.2023 года", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbAttorney.Select();
                return;
            }
            if (!Regex.IsMatch(txbNumberPrintDocument.Text, @"^[0-9]{2,}$"))
            {
                MessageBox.Show("Введите корректно \"№ печати\"\n P.s. Пример: 53", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbNumberPrintDocument.Select();
                return;
            }
            using (MySqlCommand command = new MySqlCommand("settingBrigadesInsert", DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue($"sectionForeman", cmbSectionForemans.Text);
                command.Parameters.AddWithValue($"engineer", cmbEngineers.Text);
                command.Parameters.AddWithValue($"attorney", txbAttorney.Text);
                command.Parameters.AddWithValue($"road", cmbRoad.Text);
                command.Parameters.AddWithValue($"numberPrintDocument", txbNumberPrintDocument.Text);
                command.Parameters.AddWithValue($"curator", cmbCurator.Text);
                command.Parameters.AddWithValue($"radioCommunicationDirectorate", cmbRadioCommunicationDirectorate.Text);
                command.ExecuteNonQuery();
                DB.GetInstance.CloseConnection();
                MessageBox.Show("Бригада сформирована", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            RefreshDataGrid(dataGridView1);
        }
        void BtnChangeRegistrationStaff_Click(object sender, EventArgs e)
        {
            if (!InternetCheck.CheackSkyNET())
                return;
            var uID = txbId.Text.Trim();
            var re = new Regex(Environment.NewLine);
            txbAttorney.Text = re.Replace(txbAttorney.Text, " ");
            txbAttorney.Text.Trim();
            var re2 = new Regex(Environment.NewLine);
            txbNumberPrintDocument.Text = re2.Replace(txbNumberPrintDocument.Text, " ");
            txbNumberPrintDocument.Text.Trim();
            if (String.IsNullOrWhiteSpace(cmbSectionForemans.Text))
            {
                MessageBox.Show("Поле \"Начальник\" не должен быть пустым, добавьте начальника участка", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (String.IsNullOrWhiteSpace(cmbEngineers.Text))
            {
                MessageBox.Show("Поле \"Инженер\" не должен быть пустым, добавьте инженера", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (String.IsNullOrWhiteSpace(cmbRoad.Text))
            {
                MessageBox.Show("Поле \"Дорога\" не должна быть пустым, добавьте дорогу", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (String.IsNullOrWhiteSpace(cmbCurator.Text))
            {
                MessageBox.Show("Поле \"Куратор\" не должно быть пустым, добавьте куратора", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (String.IsNullOrWhiteSpace(cmbRadioCommunicationDirectorate.Text))
            {
                MessageBox.Show("Поле \"Представитель дирекции связи\" не должно быть пустым, добавьте представителя дирекции связи", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!Regex.IsMatch(txbAttorney.Text, @"^[0-9]{1,}[\/][0-9]{1,}[\s][о][т][\s][0-9]{2,2}[\.][0-9]{2,2}[\.][2][0][0-9]{2,2}[\s][г][о][д][а]$"))
            {
                MessageBox.Show("Введите корректно \"Доверенность\"\n P.s. Пример: 53/53 от 10.01.2023 года", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbAttorney.Select();
                return;
            }
            if (!Regex.IsMatch(txbNumberPrintDocument.Text, @"^[0-9]{2,}$"))
            {
                MessageBox.Show("Введите корректно \"№ печати\"\n P.s. Пример: 53", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbNumberPrintDocument.Select();
                return;
            }
            using (MySqlCommand command = new MySqlCommand("settingBrigadesUpdate", DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue($"sectionForeman", cmbSectionForemans.Text);
                command.Parameters.AddWithValue($"engineer", cmbEngineers.Text);
                command.Parameters.AddWithValue($"attorney", txbAttorney.Text);
                command.Parameters.AddWithValue($"road", cmbRoad.Text);
                command.Parameters.AddWithValue($"numberPrintDocument", txbNumberPrintDocument.Text);
                command.Parameters.AddWithValue($"curator", cmbCurator.Text);
                command.Parameters.AddWithValue($"radioCommunicationDirectorate", cmbRadioCommunicationDirectorate.Text);
                command.Parameters.AddWithValue($"uID", uID);
                command.ExecuteNonQuery();
                DB.GetInstance.CloseConnection();
                MessageBox.Show("Запись успешно изменена", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            RefreshDataGrid(dataGridView1);
        }
        void BtnDeleteRegistrationStaff_Click(object sender, EventArgs e)
        {
            if (!InternetCheck.CheackSkyNET())
                return;
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                dataGridView1.Rows[row.Index].Cells[8].Value = RowState.Deleted;
            DB.GetInstance.OpenConnection();
            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView1.Rows[index].Cells[8].Value;
                if (rowState == RowState.Deleted)
                {
                    var dID = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                    using (MySqlCommand command = new MySqlCommand("settingBrigadesDelete", DB.GetInstance.GetConnection()))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue($"dID", dID);
                        command.ExecuteNonQuery();
                    }
                        
                }
            }
            DB.GetInstance.CloseConnection();
            RefreshDataGrid(dataGridView1);
        }

        void BtnReportCard_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Нет сформированных бригад");
                return;
            }
            using (StaffTabulationForm staffTabulation = new StaffTabulationForm())
            {
                this.Hide();
                staffTabulation.ShowDialog();
                this.Show();
            }
        }
    }
}
