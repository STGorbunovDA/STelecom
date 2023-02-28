using Microsoft.Win32;
using STelecom.Classes.Cheack;
using STelecom.Classes.FormsMethods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
                btnSeachDatabaseCity.Enabled = true;
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
            WorkFromMethod.GettingSettingBrigadesByUser(lblChiefFIO, lblEngineerFIO, lblDoverennost, 
                lblRoad, lblNumberPrintDocument, _user, cmbRoad);
            WorkFromMethod.SelectCityGropByRoad(cmbCity,cmbRoad);
            WorkFromMethod.CreateColums(dataGridView1);
            WorkFromMethod.CreateColums(dataGridView2);
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
            WorkFromMethod.RefreshDataGrid(dataGridView1, cmbCity.Text, cmbRoad.Text);
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
            WorkFromMethod.RefreshDataGridTimerEventProcessor(dataGridView2, taskCity, road);
            new Thread(() => { WorkFromMethod.GetSaveDataGridViewInJson(dataGridView2, taskCity); }) { IsBackground = true }.Start();
            new Thread(() => { WorkFromMethod.AutoSaveFilePC(dataGridView2, taskCity); }) { IsBackground = true }.Start();

        }
    }
}
