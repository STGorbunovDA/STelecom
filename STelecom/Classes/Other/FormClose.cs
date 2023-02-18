using MySql.Data.MySqlClient;
using STelecom.Classes.SeparateMethodsForm;
using STelecom.DataBase;
using System;
using System.Data;
using System.Windows.Forms;

namespace STelecom.Classes.Other
{
    class FormClose
    {
        static volatile FormClose Class;
        static object SyncObject = new object();
        public static FormClose GetInstance
        {
            get
            {
                if (Class == null)
                    lock (SyncObject)
                    {
                        if (Class == null)
                            Class = new FormClose();
                    }
                return Class;
            }
        }

        public bool FClose(string login)
        {
            DialogResult result = MessageBox.Show("Вы действительно хотите закрыть программу?", "Подтверждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.OK)
            {
                DateTime Date = DateTime.Now;
                string exitDate = Date.ToString("yyyy-MM-dd HH:mm:ss");
                DateTime dateTimeInput = MenuMethod.CheckDateTimeInputLogUserDatabase(login);
                if (Date.ToString("yyyy-MM-dd") == dateTimeInput.ToString("yyyy-MM-dd"))
                {
                    using (MySqlCommand command = new MySqlCommand("logUsersUpdateDateTimeExit", DB.GetInstance.GetConnection()))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue($"dateTimeExit", exitDate);
                        command.Parameters.AddWithValue($"user", login);
                        command.Parameters.AddWithValue($"dateTimeInput", dateTimeInput.ToString("yyyy-MM-dd HH:mm:ss"));
                        DB.GetInstance.OpenConnection();
                        command.ExecuteNonQuery();
                        DB.GetInstance.CloseConnection();
                        return false;
                    }
                }
                return true;
            }
            else return true;
        }
    }
}
