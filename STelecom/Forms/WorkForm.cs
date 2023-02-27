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
        }
    }
}
