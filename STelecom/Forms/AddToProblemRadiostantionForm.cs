using MySql.Data.MySqlClient;
using STelecom.Classes.Cheack;
using STelecom.DataBase;
using System;
using System.Data;
using System.Windows.Forms;

namespace STelecom.Forms
{
    public partial class AddToProblemRadiostantionForm : Form
    {
        private readonly CheckUser _user;
        public AddToProblemRadiostantionForm(CheckUser user)
        {
            InitializeComponent();
            _user = user;
            cmbProblem.Text = cmbProblem.Items[0].ToString();
        }
        void AddToProblemRadiostantionForm_Load(object sender, EventArgs e)
        {
            lbL_Author.Text = _user.Login;
            if (!InternetCheck.CheackSkyNET())
                return;
            using (MySqlCommand command = new MySqlCommand("modelRadiostationSelect_1", DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                DataTable table = new DataTable();
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(table);
                    cmbModel.DataSource = table;
                    cmbModel.ValueMember = "id";
                    cmbModel.DisplayMember = "modelRadiostantionName";
                    DB.GetInstance.CloseConnection();
                }
            }
        }
        void PicbClearControl_Click(object sender, EventArgs e)
        {
            string Mesage = "Вы действительно хотите очистить все введенные вами поля?";
            if (MessageBox.Show(Mesage, "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;
            foreach (Control control in this.Controls)
                if (control is TextBox)
                    control.Text = String.Empty;
        }
        void ChbProblemEnable_Click(object sender, EventArgs e)
        {
            if (chbProblemEnable.Checked)
            {
                cmbProblem.Enabled = false;
                txbProblem.Enabled = true;
                txbProblem.Select();
            }
            else if (!chbProblemEnable.Checked)
            {
                cmbProblem.Enabled = true;
                txbProblem.Enabled = false;
                txbProblem.Clear();
            }
        }
        void BtnSaveAddRadiostantionProblem_Click(object sender, EventArgs e)
        {

        }
    }
}
