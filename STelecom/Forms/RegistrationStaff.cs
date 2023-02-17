using MySql.Data.MySqlClient;
using STelecom.Classes.Cheack;
using STelecom.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
    }
}
