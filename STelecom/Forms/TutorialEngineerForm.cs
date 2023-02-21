using MySql.Data.MySqlClient;
using STelecom.Classes.Cheack;
using STelecom.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STelecom.Forms
{
    public partial class TutorialEngineerForm : Form
    {
        private readonly CheckUser _user;
        int selectedRow;
        #region состояние Rows
        enum RowState
        {
            New,
            Deleted
        }
        #endregion
        public TutorialEngineerForm(CheckUser user)
        {
            InitializeComponent();
            this.dataGridView1.RowsDefaultCellStyle.BackColor = Color.GhostWhite;
            this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            _user = user;
            cmbSeach.Text = cmbSeach.Items[3].ToString();
        }
        void CreateColumsEngineer(DataGridView dgw)
        {
            dgw.Columns.Add("id", "№");
            dgw.Columns.Add("model", "Модель");
            dgw.Columns.Add("problem", "Неисправность");
            dgw.Columns.Add("info", "Описание неисправности");
            dgw.Columns.Add("actions", "Виды работ по устраненнию дефекта");
            dgw.Columns.Add("author", "Автор");
            dgw.Columns.Add("IsNew", "RowState");
            dgw.Columns[6].Visible = false;
        }
        void RefreshDataGridEngineer(DataGridView dgw)
        {
            if (!InternetCheck.CheackSkyNET())
                return;
            var myCulture = new CultureInfo("ru-RU");
            myCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = myCulture;
            dgw.Rows.Clear();
            using (MySqlCommand command = new MySqlCommand("tutorialEngineerSelectFull_1", DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                            ReedSingleRowEnginer(dgw, reader);
                        reader.Close();
                    }
                }
                command.ExecuteNonQuery();
                DB.GetInstance.CloseConnection();
            }
            dgw.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgw.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgw.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgw.Columns[0].Width = 40;
            dgw.Columns[1].Width = 200;
            dgw.Columns[2].Width = 200;
            dgw.Columns[3].Width = 424;
            dgw.Columns[4].Width = 300;
            dgw.Columns[5].Width = 142;
            for (int i = 0; i < dgw.Rows.Count; i++)
                dgw.Rows[i].Height = 140;

        }
        void ReedSingleRowEnginer(DataGridView dgw, IDataRecord record)
        {
            dgw.Invoke((MethodInvoker)(() => dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2),
                record.GetString(3), record.GetString(4), record.GetString(5), RowState.New)));
        }
        void TutorialEngineerForm_Load(object sender, EventArgs e)
        {
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.ColumnHeadersDefaultCellStyle.Font.FontFamily, 12f, FontStyle.Bold); //жирный курсив размера 16
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.White; //цвет текста
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black; //цвет ячейки
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            CreateColumsEngineer(dataGridView1);
            RefreshDataGridEngineer(dataGridView1);
        }
        void PicbUpdate_Click(object sender, EventArgs e)
        {
            RefreshDataGridEngineer(dataGridView1);
        }
        void BtnBriefInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Перед началом проверки радиостанции необходимо визуально " +
                "осмотреть корпус на сквозные трещины, сколы корпуса, батарейные контакты, " +
                "уплотнитель батарейного контакта, а также ручку регулятора громкости и ручку " +
                "переключения каналов.Проивести чистку корпуса радиостанции, убрать металлическую " +
                "стружку из динамика. Чистка внешних поверхностей радиостанции включают фронтальную " +
                "крышку радиостанции, корпус радиостанции и корпус батареи. Чистку проводить неметаллической " +
                "короткошерстной щёткой для удаления грязи с радиостанции. Так же Используйте мягкую, " +
                "абсорбирующую ткань, кубки для мытья посуды или влажные салфетки.", "Информация", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.ReadOnly = false;
            selectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                txbId.Text = row.Cells[0].Value.ToString();
                txbModel.Text = row.Cells[1].Value.ToString();
                txbProblem.Text = row.Cells[2].Value.ToString();
                txbInfo.Text = row.Cells[3].Value.ToString();
                txbActions.Text = row.Cells[4].Value.ToString();
                txbAuthor.Text = row.Cells[5].Value.ToString();
            }
        }
        void DataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex != 0)
                e.Cancel = true;
        }
        void BtnSaveExcel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Сначала добавь радиостанцию", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DateTime dateTime = DateTime.Now;
            string dateTimeString = dateTime.ToString("dd.MM.yyyy");
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            sfd.FileName = $"ОБЩАЯ База_Неисправностей_{dateTimeString}";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter sw = new StreamWriter(sfd.FileName, false, Encoding.Unicode))
                {
                    string note = string.Empty;
                    note += $"Номер\tМодель\tНеисправность\tОписание неисправности\tВиды работ по устраненнию дефекта\tАвтор";
                    sw.WriteLine(note);
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        {
                            Regex re = new Regex(Environment.NewLine);
                            string value = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            value = re.Replace(value, " ");
                            if (dataGridView1.Columns[j].HeaderText.ToString() == "Автор")
                                sw.Write(value);
                            else if (dataGridView1.Columns[j].HeaderText.ToString() == "RowState")
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

    }
}
