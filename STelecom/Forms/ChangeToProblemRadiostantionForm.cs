using MySql.Data.MySqlClient;
using STelecom.Classes.Cheack;
using STelecom.Classes.Other;
using STelecom.DataBase;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace STelecom.Forms
{
    public partial class ChangeToProblemRadiostantionForm : Form
    {
        private readonly CheckUser _user;
        public ChangeToProblemRadiostantionForm(CheckUser user)
        {
            InitializeComponent();
            _user = user;
        }
        void ChangeToProblemRadiostantionForm_Load(object sender, EventArgs e)
        {
            lblAuthor.Text = Encryption.DecryptCipherTextToPlainText(_user.Login);
            cmbModel.Text = cmbModel.Items[0].ToString();
            cmbProblem.Text = cmbProblem.Items[0].ToString();
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
        void CmbModel_Click(object sender, EventArgs e)
        {
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
        void BtnChageRadiostantionProblem_Click(object sender, EventArgs e)
        {
            if (!InternetCheck.CheackSkyNET())
                return;
            if (String.IsNullOrWhiteSpace(cmbModel.Text))
            {
                MessageBox.Show("Модель не может быть пустой", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!chbProblemEnable.Checked)
            {
                if (String.IsNullOrWhiteSpace(txbProblem.Text))
                {
                    MessageBox.Show("Опиши неисправность", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txbProblem.Select();
                    return;
                }
            }
            if (String.IsNullOrWhiteSpace(txbInfo.Text))
            {
                MessageBox.Show("Не заполнено поле \"Описание дефекта\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbInfo.Select();
                return;
            }
            if (String.IsNullOrWhiteSpace(txbActions.Text))
            {
                MessageBox.Show("Не заполнено поле \"Виды работ по устраненнию дефекта\"", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txbActions.Select();
                return;
            }
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    Regex re = new Regex(Environment.NewLine);
                    control.Text = re.Replace(control.Text, " ");
                    control.Text.Trim();
                }
            }
            string problem = String.Empty;
            string model = cmbModel.Text;
            if (chbProblemEnable.Checked)
                problem = cmbProblem.Text;
            else problem = txbProblem.Text;
            string info = txbInfo.Text;
            string actions = txbActions.Text;
            string author = Encryption.EncryptPlainTextToCipherText(lblAuthor.Text);
            string id = txbId.Text;
            using (MySqlCommand command = new MySqlCommand("tutorialEngineerUpdate_1", DB.GetInstance.GetConnection()))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue($"model", model);
                command.Parameters.AddWithValue($"problem", problem);
                command.Parameters.AddWithValue($"info", info);
                command.Parameters.AddWithValue($"actions", actions);
                command.Parameters.AddWithValue($"author", author);
                command.Parameters.AddWithValue($"uID", id);
                DB.GetInstance.OpenConnection();
                command.ExecuteNonQuery();
                DB.GetInstance.CloseConnection();
                MessageBox.Show("Неисправность успешно добавлена!");
            }
        }
    }
}
