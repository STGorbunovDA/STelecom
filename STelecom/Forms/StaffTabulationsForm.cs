using MySql.Data.MySqlClient;
using STelecom.Classes.Cheack;
using STelecom.Classes.Other;
using STelecom.DataBase;
using System;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace STelecom.Forms
{
    public partial class StaffTabulationsForm : Form
    {
        int selectedRow;
        #region состояние Rows
        enum RowState
        {
            New,
            Deleted
        }
        #endregion
        public StaffTabulationsForm()
        {
            InitializeComponent();
        }
        private void CreateColums()
        {
            dataGridView1.Columns.Add("id", "№");
            dataGridView1.Columns.Add("user", "Работник");
            dataGridView1.Columns.Add("dateTimeInput", "Дата входа");
            dataGridView1.Columns.Add("dateTimeExit", "Дата выхода");
            dataGridView1.Columns.Add("TimeCount", "Время нахождения");
            dataGridView1.Columns.Add("IsNew", "Модификатор");
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[5].Visible = false;
        }
        void ReedSingleRow(DataGridView dgw, IDataRecord record)
        {
            dataGridView1.Invoke((MethodInvoker)(() => dgw.Rows.Add(record.GetInt32(0), 
                Encryption.DecryptCipherTextToPlainText(record.GetString(1)),
                record.GetDateTime(2), record.GetDateTime(3),
                record.GetDateTime(3).Subtract(record.GetDateTime(2)), RowState.New)));
        }
        void RefreshDataGrid(DataGridView dgw)
        {
            if (!InternetCheck.CheackSkyNET())
                return;
            var myCulture = new CultureInfo("ru-RU");
            myCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = myCulture;
            dgw.Rows.Clear();
            using (MySqlCommand command = new MySqlCommand("logUsersSelectFull_1", DB.GetInstance.GetConnection()))
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
        void StaffTabulationsForm_Load(object sender, EventArgs e)
        {
            CreateColums();
            RefreshDataGrid(dataGridView1);
            using (MySqlCommand command = new MySqlCommand("logUsersSelect_3", DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable table = new DataTable();
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(table);
                    cmbDateTimeInput.DataSource = table;
                    cmbDateTimeInput.DisplayMember = "DATE(dateTimeInput)";
                    cmbDateTimeInput.ValueMember = "DATE(dateTimeInput)";
                    DB.GetInstance.CloseConnection();
                }
            }
            if (cmbDateTimeInput.Items.Count > 0)
            {
                cmbDateTimeInput.SelectedIndex = cmbDateTimeInput.Items.Count - 1;
                //CmbDateTimeInput_SelectionChangeCommitted(sender, e);
            }
            this.dataGridView1.Sort(this.dataGridView1.Columns["dateTimeInput"], ListSortDirection.Ascending);
        }
        void CmbDateTimeInput_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!InternetCheck.CheackSkyNET())
                return;
            dataGridView1.Rows.Clear();
            if (cmbDateTimeInput.Items.Count == 0)
                return;
            string date = Convert.ToDateTime(cmbDateTimeInput.Text).ToString("yyyy-MM-dd");
            using (MySqlCommand command = new MySqlCommand("logUsersSelectFull_2", DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue($"date", date);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                            ReedSingleRow(dataGridView1, reader);
                        reader.Close();
                    }
                }
                DB.GetInstance.CloseConnection();
            }
        }
        void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.ReadOnly = false;
            selectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                txbId.Text = row.Cells[0].Value.ToString();
                txbUser.Text = row.Cells[1].Value.ToString();
                txbDateTimeInput.Text = row.Cells[2].Value.ToString();
                txbDateTimeExit.Text = row.Cells[3].Value.ToString();
                txbTimeCount.Text = row.Cells[4].Value.ToString();
            }
        }
        void DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 0)
                e.Cancel = true;
        }
        void PicbRefresh_Click(object sender, EventArgs e)
        {
            RefreshDataGrid(dataGridView1);
        }
        void BtnSaveExcel_Click(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            string dateTimeString = dateTime.ToString("dd.MM.yyyy");
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            sfd.FileName = $"Табель сотрудников_{dateTimeString}";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.Unicode))
                {
                    string note = string.Empty;
                    note += $"Работник\tДата входа\tДата выхода\tВремя нахождения";
                    sw.WriteLine(note);
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        {
                            Regex re = new Regex(Environment.NewLine);
                            string value = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            value = re.Replace(value, " ");
                            if (dataGridView1.Columns[j].HeaderText.ToString() == "№")
                            {

                            }
                            else if (dataGridView1.Columns[j].HeaderText.ToString() == "Время нахождения")
                            {
                                sw.Write(value);
                            }
                            else if (dataGridView1.Columns[j].HeaderText.ToString() == "Модификатор")
                            {

                            }
                            else sw.Write(value + "\t");
                        }
                        sw.WriteLine();
                    }
                }
                MessageBox.Show("Файл успешно сохранен!");
            }
        }
        void BtnDelete_Click(object sender, EventArgs e)
        {
            if (!InternetCheck.CheackSkyNET())
                return;
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                dataGridView1.Rows[row.Index].Cells[5].Value = RowState.Deleted;
            DB.GetInstance.OpenConnection();
            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView1.Rows[index].Cells[5].Value;
                if (rowState == RowState.Deleted)
                {
                    int id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                    using (MySqlCommand command = new MySqlCommand("logUsersDelete_1", DB.GetInstance.GetConnection()))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue($"dID", id);
                        command.ExecuteNonQuery();
                    }
                }
            }
            DB.GetInstance.CloseConnection();
            RefreshDataGrid(dataGridView1);
        }
    }
}
