using MySql.Data.MySqlClient;
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

        #region Метод при закрытии формы. Сохраняем дату и время выхода пользователя из программы.
        /// <summary>
        /// Метод при закрытии формы. Сохраняем дату и время выхода пользователя из программы.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool FClose(string user)
        {
            DialogResult result = MessageBox.Show("Вы действительно хотите закрыть программу?", "Подтверждение", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.OK)
            {
                DateTime Date = DateTime.Now;
                string exitDate = Date.ToString("yyyy-MM-dd HH:mm:ss");
                DateTime dateTimeInput = CheckDateTimeInputLogUserDatabase(user);
                if (Date.ToString("yyyy-MM-dd") == dateTimeInput.ToString("yyyy-MM-dd"))
                {
                    using (MySqlCommand command = new MySqlCommand("logUsersUpdate_1", DB.GetInstance.GetConnection()))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue($"dateTimeExit", exitDate);
                        command.Parameters.AddWithValue($"user", user);
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
        #endregion

        /// <summary>
        /// Получаем дату регистрации входа пользователя для табеля
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>
        DateTime CheckDateTimeInputLogUserDatabase(string user)
        {
            DateTime Date = DateTime.Now;
            using (MySqlCommand command = new MySqlCommand("logUsersSelect_2", DB.GetInstance.GetConnection()))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue($"userLog", user);
                command.Parameters.AddWithValue($"date", Date.ToString("yyyy-MM-dd"));
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    if (table.Rows.Count > 0) return Convert.ToDateTime(table.Rows[table.Rows.Count - 1].ItemArray[0]);
                    else return DateTime.MinValue;
                }
            }
        }
    }
}
