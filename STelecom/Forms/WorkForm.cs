using STelecom.Classes.Cheack;
using STelecom.Classes.FormsMethods;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            IsAdmin();
        }
        void IsAdmin()
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

        void WorkForm_Load(object sender, EventArgs e)
        {
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font(dataGridView1.ColumnHeadersDefaultCellStyle.Font.FontFamily, 12f, FontStyle.Bold); //жирный курсив размера 16
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.White; //цвет текста
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black; //цвет ячейки
            WorkFromMethod.GettingTeamData(lblChiefFIO, lblEngineerFIO, lblDoverennost, lblRoad, lblNumberPrintDocument, _user, cmbRoad);
        }
    }
}
