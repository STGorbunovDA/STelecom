using MySql.Data.MySqlClient;
using STelecom.Classes.Cheack;
using STelecom.Classes.Other;
using STelecom.DataBase;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace STelecom.Forms
{
    public partial class TutorialEngineerForm : Form
    {
        private readonly CheckUser _user;
        int selectedRow;
        string[] queryPost = { "tutorialEngineerSelect_1", "tutorialEngineerSelect_2", "tutorialEngineerSelect_3" };
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
        void ReedSingleRowEnginer(DataGridView dgw, IDataRecord record)
        {
            dgw.Invoke((MethodInvoker)(() => dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2),
                record.GetString(3), record.GetString(4), Encryption.DecryptCipherTextToPlainText(record.GetString(5)), RowState.New)));
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
        void TutorialEngineerForm_Load(object sender, EventArgs e)
        {
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.ColumnHeadersDefaultCellStyle.Font.FontFamily, 12f, FontStyle.Bold); //жирный курсив размера 16
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.White; //цвет текста
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black; //цвет ячейки
            CreateColumsEngineer(dataGridView1);
            RefreshDataGridEngineer(dataGridView1);
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
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
        void DataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
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
        void CmbSeachUniqueEngineer(ComboBox cmbUnique, string queryPost)
        {
            if (!InternetCheck.CheackSkyNET())
                return;
            cmbUnique.Visible = true;
            txbSearch.Visible = false;
            txbSearch.Clear();
            using (MySqlCommand command = new MySqlCommand(queryPost, DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable table = new DataTable();
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(table);
                    cmbUnique.DataSource = table;
                    if (queryPost == "tutorialEngineerSelect_1")
                        cmbUnique.DisplayMember = "model";
                    else if (queryPost == "tutorialEngineerSelect_2")
                        cmbUnique.DisplayMember = "problem";
                    else cmbUnique.DisplayMember = "author";
                    DB.GetInstance.CloseConnection();
                }
            }
        }
        void CmbSeach_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cmbSeach.SelectedIndex == 0)
                CmbSeachUniqueEngineer(cmbUnique, queryPost[0]);
            else if (cmbSeach.SelectedIndex == 1)
                CmbSeachUniqueEngineer(cmbUnique, queryPost[1]);
            else if (cmbSeach.SelectedIndex == 2)
                CmbSeachUniqueEngineer(cmbUnique, queryPost[2]);
            else if (cmbSeach.SelectedIndex == 3)
            {
                txbSearch.Visible = true;
                cmbUnique.Visible = false;
            }
        }
        void CmbUnique_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SearchEngineer(dataGridView1, cmbSeach.Text, txbSearch.Text, cmbUnique.Text);
        }
        void SearchEngineer(DataGridView dgw, string cmbUnique, string txbSearch, string cmbNumberUnique)
        {
            if (!InternetCheck.CheackSkyNET())
                return;
            string colName = string.Empty;
            dgw.Rows.Clear();
            if (cmbUnique == "Модель")
                colName = "model";
            else if (cmbUnique == "Неисправность")
                colName = "problem";
            else if (cmbUnique == "Автор")
                colName = "author";
            else if (cmbUnique == "Описание неисправности")
                colName = "info";
            using (MySqlCommand command = new MySqlCommand("tutorialEngineerSelect_4", DB.GetInstance.GetConnection()))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue($"colName", colName);
                if (colName == "model" || colName == "problem" || colName == "author")
                    command.Parameters.AddWithValue($"search", cmbNumberUnique);
                else if (colName == "info")
                    command.Parameters.AddWithValue($"search", txbSearch);
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
        void TxbSearch_DoubleClick(object sender, EventArgs e)
        {
            SearchEngineer(dataGridView1, cmbSeach.Text, txbSearch.Text, cmbUnique.Text);
        }
        void TxbSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
                SearchEngineer(dataGridView1, cmbSeach.Text, txbSearch.Text, cmbUnique.Text);
        }
        void BtnDeleteProblem_Click(object sender, EventArgs e)
        {
            if (!InternetCheck.CheackSkyNET())
                return;
            if (String.IsNullOrWhiteSpace(txbId.Text))
            {
                MessageBox.Show("Выбери строку которую хочешь удалить!");
                return;
            }
            if (dataGridView1.SelectedRows.Count > 1)
            {
                string Mesage = $"Вы действительно хотите удалить неисправность у модели: {txbModel.Text}?";
                if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                    return;
            }
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                dataGridView1.Rows[row.Index].Cells[6].Value = RowState.Deleted;
            for (int index = 0; index < dataGridView1.Rows.Count; index++)
            {
                var rowState = (RowState)dataGridView1.Rows[index].Cells[6].Value;
                if (rowState == RowState.Deleted)
                {
                    int id = Convert.ToInt32(dataGridView1.Rows[index].Cells[0].Value);
                    using (MySqlCommand command = new MySqlCommand("tutorialEngineerDelete_1", DB.GetInstance.GetConnection()))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue($"dID", id);
                        DB.GetInstance.OpenConnection();
                        command.ExecuteNonQuery();
                        DB.GetInstance.CloseConnection();
                    }
                }
            }
            int currRowIndex = dataGridView1.CurrentCell.RowIndex;
            RefreshDataGridEngineer(dataGridView1);
            dataGridView1.ClearSelection();
            if (dataGridView1.RowCount - currRowIndex > 0)
                dataGridView1.CurrentCell = dataGridView1[0, currRowIndex];
        }
        void BtnNewProblemRST_Click(object sender, EventArgs e)
        {
            if (!InternetCheck.CheackSkyNET())
                return;
            AddToProblemRadiostantionForm addProblemRST = new AddToProblemRadiostantionForm(_user);
            if (Application.OpenForms["AddToProblemRadiostantionForm"] == null)
                addProblemRST.Show();
        }
        void BtnChangeProblem_Click(object sender, EventArgs e)
        {
            if (!InternetCheck.CheackSkyNET())
                return;
            if (String.IsNullOrWhiteSpace(txbId.Text))
                return;
            ChangeToProblemRadiostantionForm changeToProblem = new ChangeToProblemRadiostantionForm(_user);
            if (Application.OpenForms["ChangeToProblemRadiostantionForm"] == null)
            {
                changeToProblem.txbId.Text = txbId.Text;
                changeToProblem.cmbModel.Items.Add(txbModel.Text).ToString();
                changeToProblem.txbProblem.Text = txbProblem.Text;
                changeToProblem.txbInfo.Text = txbInfo.Text;
                changeToProblem.txbActions.Text = txbActions.Text;
                changeToProblem.Show();
            }
        }
        void DataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    if (!String.IsNullOrWhiteSpace(txbId.Text))
                    {
                        ContextMenu m1 = new ContextMenu();
                        m1.MenuItems.Add(new MenuItem("Добавить новую неисправность", BtnNewProblemRST_Click));
                        m1.MenuItems.Add(new MenuItem("Изменить неисправность", BtnChangeProblem_Click));
                        m1.MenuItems.Add(new MenuItem("Удалить неисправность", BtnDeleteProblem_Click));
                        m1.MenuItems.Add(new MenuItem("Сохранить в excel", BtnSaveExcel_Click));
                        m1.MenuItems.Add(new MenuItem("Краткая иформация", BtnBriefInfo_Click));
                        m1.Show(dataGridView1, new Point(e.X, e.Y));
                    }
                }
                if (dataGridView1.Rows.Count == 0)
                {
                    ContextMenu m2 = new ContextMenu();
                    m2.MenuItems.Add(new MenuItem("Добавить новую неисправность", BtnNewProblemRST_Click));
                    m2.MenuItems.Add(new MenuItem("Краткая иформация", BtnBriefInfo_Click));
                }
            }
        }
    }
}
