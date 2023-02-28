using STelecom.Classes.Cheack;
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
    }
}
