using Microsoft.Win32;
using STelecom.Classes.Cheack;
using STelecom.Classes.FormsMethods;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using WinForms = System.Windows.Forms;

namespace STelecom.Forms
{
    public partial class WorkForm : Form
    {
        #region состояние Rows
        enum RowState
        {
            New,
            Deleted
        }
        #endregion

        #region global perem
        private delegate DialogResult ShowOpenFileDialogInvoker();
        int selectedRow;
        private readonly CheckUser _user;
        #endregion

        public WorkForm(CheckUser user)
        {
            InitializeComponent();
            cmbSeach.Text = cmbSeach.Items[2].ToString();
            this.dataGridView1.RowsDefaultCellStyle.BackColor = Color.GhostWhite;
            this.dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.White;
            _user = user;
            IsPost();
        }
        /// <summary>
        /// Смотрим кто зашёл и ограничиваем или нет функционал
        /// </summary>
        void IsPost()
        {
            if (_user.Post == "Дирекция связи")
            {
                panel3.Enabled = false;
                pnlFunctionalLoading.Enabled = false;
                pnlDateRegistrationStaff.Enabled = false;
                pnlRemontInformationCompany.Enabled = false;

                foreach (Control element in panel1.Controls)
                    element.Enabled = false;

                cmbCity.Enabled = true;
                btnLoadingSeachDataBaseCity.Enabled = true;
                btnAddCityInRegistry.Enabled = true;
                btnAllDataBase.Enabled = true;
                picbUpdate.Enabled = true;
                cmbSeach.Enabled = true;
                txbSearch.Enabled = true;
                btnSearch.Enabled = true;
                cmbNumberUnique.Enabled = true;
                menuStrip1.Visible = false;
            }
            if (_user.Post == "Куратор" || _user.Post == "Руководитель")
                mTripFuncionalPanel.Visible = false;

            if (_user.Post == "Начальник участка")
            {
                mTripCurator.Visible = false;
                mTripFuncionalPanel.Visible = false;
            }
            if (_user.Post == "Инженер")
            {
                btnChangeRstForm.Enabled = false;
                mTripCurator.Visible = false;
                mTripFuncionalPanel.Visible = false;
                mTripChangeRst.Visible = false;
                mTripDelete.Visible = false;
                mTripDecommission.Visible = false;
                mTripAddFillFullActTO.Visible = false;
                mTripAddSignatureActTO.Visible = false;
                mTripFuncionalPanel.Visible = false;
                panel4.Visible = false;
            }
        }

        #region Счётчики
        /// <summary>
        /// Счётчики 
        /// </summary>
        void Counters()
        {
            decimal sumTO = 0;
            int colRemont = 0;
            decimal sumRemont = 0;
            int inRepair = 0;
            int verified = 0;
            int decommission = 0;

            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                if ((Boolean)(dataGridView1.Rows[i].Cells["category"].Value.ToString() != "")) colRemont++;
                if ((Boolean)(dataGridView1.Rows[i].Cells["verifiedRST"].Value.ToString() == "+")) verified++;
                if ((Boolean)(dataGridView1.Rows[i].Cells["verifiedRST"].Value.ToString() == "?")) inRepair++;
                if ((Boolean)(dataGridView1.Rows[i].Cells["verifiedRST"].Value.ToString() == "0")) decommission++;
                sumTO += Convert.ToDecimal(dataGridView1.Rows[i].Cells["price"].Value);
                sumRemont += Convert.ToDecimal(dataGridView1.Rows[i].Cells["priceRemont"].Value);
            }
            lblVerified.Text = verified.ToString();
            lblInRepair.Text = inRepair.ToString();
            lblCount.Text = dataGridView1.Rows.Count.ToString();
            lblSum.Text = sumTO.ToString();
            lblCountRemont.Text = colRemont.ToString();
            lblSumRemont.Text = sumRemont.ToString();
            lblDecommission.Text = decommission.ToString();
        }
        #endregion

        void WorkForm_Load(object sender, EventArgs e)
        {
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.ColumnHeadersDefaultCellStyle.Font.FontFamily,
                12f, FontStyle.Bold); //жирный курсив размера 16
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.White; //цвет текста
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black; //цвет ячейки
            WorkFormMethod.GettingSettingBrigadesByUser(lblChiefFIO, lblEngineerFIO, lblDoverennost,
                lblRoad, lblNumberPrintDocument, _user, cmbRoad);
            WorkFormMethod.SelectCityGropByRoad(cmbCity, cmbRoad);
            WorkFormMethod.CreateColums(dataGridView1);
            WorkFormMethod.CreateColums(dataGridView2);
            this.dataGridView1.Sort(this.dataGridView1.Columns["dateTO"], ListSortDirection.Ascending);
            dataGridView1.Columns["dateTO"].ValueType = typeof(DateTime);
            dataGridView1.Columns["dateTO"].DefaultCellStyle.Format = "dd.MM.yyyy";
            dataGridView1.Columns["dateTO"].ValueType = System.Type.GetType("System.Date");
            RegistryKey reg1 = Registry.CurrentUser.OpenSubKey($"SOFTWARE\\ServiceTelekom_Setting\\City");
            if (reg1 != null)
            {
                RegistryKey currentUserKey = Registry.CurrentUser;
                RegistryKey helloKey = currentUserKey.OpenSubKey($"SOFTWARE\\ServiceTelekom_Setting\\City");
                cmbCity.Text = helloKey.GetValue("Город проведения проверки").ToString();

                helloKey.Close();
            }
            WorkFormMethod.RefreshDataGrid(dataGridView1, cmbCity.Text, cmbRoad.Text);
            Counters();
            /// получение актов(незаполненные) из реестра которые которые добавил пользователь
            RegistryKey reg2 = Registry.CurrentUser.OpenSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Акты_Заполняем_До_full");
            if (reg2 != null)
            {
                string registry = String.Empty;
                RegistryKey currentUserKey = Registry.CurrentUser;
                RegistryKey helloKey = currentUserKey.OpenSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Акты_Заполняем_До_full");
                registry = helloKey.GetValue("Акты_незаполненные").ToString();
                string[] split = registry.Split(new Char[] { ';' });

                foreach (string s in split)
                    if (!String.IsNullOrWhiteSpace(s))
                        cmbAddFillFullActTO.Items.Add(s);
                helloKey.Close();
                cmbAddFillFullActTO.Sorted = true;
                if (cmbAddFillFullActTO.Items.Count > 0)
                    cmbAddFillFullActTO.Text = cmbAddFillFullActTO.Items[cmbAddFillFullActTO.Items.Count - 1].ToString();
            }
            /// получение актов(на подпись) из реестра которые которые добавил пользователь
            RegistryKey reg3 = Registry.CurrentUser.OpenSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Акты_на_подпись");
            if (reg3 != null)
            {
                string registry2 = String.Empty;
                RegistryKey currentUserKey = Registry.CurrentUser;
                RegistryKey helloKey = currentUserKey.OpenSubKey($"SOFTWARE\\ServiceTelekom_Setting\\Акты_на_подпись");
                registry2 = helloKey.GetValue("Акты_на_подпись").ToString();
                string[] split = registry2.Split(new Char[] { ';' });

                foreach (string s in split)
                    if (!String.IsNullOrWhiteSpace(s))
                        cmbAddSignature.Items.Add(s);

                helloKey.Close();
                cmbAddSignature.Sorted = true;
                if (cmbAddSignature.Items.Count > 0)
                    cmbAddSignature.Text = cmbAddSignature.Items[cmbAddSignature.Items.Count - 1].ToString();
            }
            ///Таймер
            WinForms::Timer timer = new WinForms::Timer();
            timer.Interval = (31 * 60 * 1000); // 15 mins
            timer.Tick += new EventHandler(TimerEventProcessor);
            timer.Start();

            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
        }

        /// <summary>
        /// Таймер для копирования БД, сохранение в Excel, Json
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TimerEventProcessor(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
                return;
            string taskCity = cmbCity.Text;
            string road = cmbRoad.Text;
            WorkFormMethod.RefreshDataGridTimerEventProcessor(dataGridView2, taskCity, road);
            new Thread(() => { WorkFormMethod.CopyDataBaseRadiostantionInRadiostantionCopy(); }) { IsBackground = true }.Start();
            new Thread(() => { WorkFormMethod.GetSaveDataGridViewInJson(dataGridView2, taskCity); }) { IsBackground = true }.Start();
            new Thread(() => { WorkFormMethod.AutoSaveFilePC(dataGridView2, taskCity); }) { IsBackground = true }.Start();
        }

        #region загрузка всех данных радиостанций без города по всей дороге
        void BtnAllDataBase_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Сначала добавь радиостанцию", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            WorkFormMethod.FullDataBase(dataGridView1, cmbRoad.Text);
            Counters();
            txbFlagAllDataBase.Text = "Вся БД";
        }
        #endregion

        #region загрузка уникальных городов по дороге
        void CmbCity_Click(object sender, EventArgs e)
        {
            WorkFormMethod.SelectCityGropByRoad(cmbCity, cmbRoad);
        }

        #endregion

        #region загрузка данных ТО радиостанций по городу и сохранение в реестр
        void BtnLoadingSeachDataBaseCity_Click(object sender, EventArgs e)
        {
            WorkFormMethod.LoadingSeachDataBaseCity(dataGridView1, cmbCity, cmbRoad);
            Counters();
        }
        void CmbCity_SelectionChangeCommitted(object sender, EventArgs e)
        {
            BtnLoadingSeachDataBaseCity_Click(sender, e);
        }
        #endregion

        #region загрузка данных в cmbCity при выборе дороги
        void CmbRoad_SelectionChangeCommitted(object sender, EventArgs e)
        {
            WorkFormMethod.SelectCityGropByRoad(cmbCity, cmbRoad);
        }


        #endregion

        #region запись города в реестр
        void BtnAddCityInRegistry_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(cmbCity.Text))
            {
                MessageBox.Show("Комбобокс \"Город\" пуст, необходимо добавить новую радиостанцию\n P.s. Ввводи город правильно", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey helloKey = currentUserKey.CreateSubKey("SOFTWARE\\ServiceTelekom_Setting");
            helloKey.SetValue("Город проведения проверки", $"{cmbCity.Text}");
            helloKey.Close();
        }
        #endregion

        #region получение данных в Control-ы, button right mouse
        void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.ReadOnly = false;
            selectedRow = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[selectedRow];
                txbid.Text = row.Cells[0].Value.ToString();
                cmbPoligon.Text = row.Cells[1].Value.ToString();
                txbCompany.Text = row.Cells[2].Value.ToString();
                txbLocation.Text = row.Cells[3].Value.ToString();
                cmbModel.Text = row.Cells[4].Value.ToString();
                txbSerialNumber.Text = row.Cells[5].Value.ToString();
                txbInventoryNumber.Text = row.Cells[6].Value.ToString();
                txbNetworkNumber.Text = row.Cells[7].Value.ToString();
                txbDateTO.Text = row.Cells[8].Value.ToString();
                txbNumberAct.Text = row.Cells[9].Value.ToString();
                txbCity.Text = row.Cells[10].Value.ToString();
                txbPrice.Text = row.Cells[11].Value.ToString();
                txBRepresentative.Text = row.Cells[12].Value.ToString();
                txbPost.Text = row.Cells[13].Value.ToString();
                txbNumberIdentification.Text = row.Cells[14].Value.ToString();
                txbDateIssue.Text = row.Cells[15].Value.ToString();
                txbPhoneNumber.Text = row.Cells[16].Value.ToString();
                txbNumberActRemont.Text = row.Cells[17].Value.ToString();
                cmbCategory.Text = row.Cells[18].Value.ToString();
                txbPriceRemont.Text = row.Cells[19].Value.ToString();
                txbAntenna.Text = row.Cells[20].Value.ToString();
                txbManipulator.Text = row.Cells[21].Value.ToString();
                txbAKB.Text = row.Cells[22].Value.ToString();
                txbBatteryСharger.Text = row.Cells[23].Value.ToString();
                txbCompletedWorks1.Text = row.Cells[24].Value.ToString();
                txbCompletedWorks2.Text = row.Cells[25].Value.ToString();
                txbCompletedWorks3.Text = row.Cells[26].Value.ToString();
                txbCompletedWorks4.Text = row.Cells[27].Value.ToString();
                txbCompletedWorks5.Text = row.Cells[28].Value.ToString();
                txbCompletedWorks6.Text = row.Cells[29].Value.ToString();
                txbCompletedWorks7.Text = row.Cells[30].Value.ToString();
                txbParts1.Text = row.Cells[31].Value.ToString();
                txbParts2.Text = row.Cells[32].Value.ToString();
                txbParts3.Text = row.Cells[33].Value.ToString();
                txbParts4.Text = row.Cells[34].Value.ToString();
                txbParts5.Text = row.Cells[35].Value.ToString();
                txbParts6.Text = row.Cells[36].Value.ToString();
                txbParts7.Text = row.Cells[37].Value.ToString();
                txbDecommissionNumber.Text = row.Cells[38].Value.ToString();
                txbComment.Text = row.Cells[39].Value.ToString();
                //cmbRoad.Text = row.Cells[40].Value.ToString();
            }
        }


        #endregion

        #region Clear contorl-ы
        void PicbClear_Click(object sender, EventArgs e)
        {
            foreach (Control control in panel1.Controls)
                if (control is TextBox)
                    control.Text = String.Empty;
            foreach (Control control in panel2.Controls)
                if (control is TextBox)
                    control.Text = String.Empty;
        }
        #endregion

        #region ProcessKbdCtrlShortCuts
        void ProcessKbdCtrlShortCuts(object sender, KeyEventArgs e)
        {
            TextBox t = (TextBox)sender;
            if (e.KeyData == (Keys.C | Keys.Control))
            {
                t.Copy();
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.X | Keys.Control))
            {
                t.Cut();
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.V | Keys.Control))
            {
                t.Paste();
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.A | Keys.Control))
            {
                t.SelectAll();
                e.Handled = true;
            }
            else if (e.KeyData == (Keys.Z | Keys.Control))
            {
                t.Undo();
                e.Handled = true;
            }
        }
        #endregion

        #region Сохранение БД на PC
        void BtnSaveInFile_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Сначала добавь радиостанцию", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            pnlPrintBase.Visible = true;

        }
        void BtnPnlPrintBaseClose_Click(object sender, EventArgs e)
        {
            pnlPrintBase.Visible = false;
        }
        void BtnSaveDirectorateBase_Click(object sender, EventArgs e)
        {
            pnlPrintBase.Visible = false;
            WorkFormMethod.DirectorateSaveFilePC(dataGridView1, cmbCity.Text);
        }
        void BtnSaveFullBase_Click(object sender, EventArgs e)
        {
            pnlPrintBase.Visible = false;
            WorkFormMethod.SaveFullBasePC(dataGridView1, cmbCity.Text);
        }

        #endregion




    }
}
