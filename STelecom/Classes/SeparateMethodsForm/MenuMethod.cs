using MySql.Data.MySqlClient;
using STelecom.DataBase;
using System;
using System.Data;

namespace STelecom.Classes.SeparateMethodsForm
{
    class MenuMethod
    {
        #region получение Даты регистрации входа в программу для табеля
        public static DateTime CheckDateTimeInputLogUserDatabase(string user)
        {
            DateTime Date = DateTime.Now;
            using (MySqlCommand command = new MySqlCommand("logUsersSelectUserDateTimeInput", DB.GetInstance.GetConnection()))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue($"userLog", user);
                command.Parameters.AddWithValue($"dateTimeInput", Date.ToString("yyyy-MM-dd"));
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    if (table.Rows.Count > 0) return Convert.ToDateTime(table.Rows[table.Rows.Count - 1].ItemArray[0]);
                    else return DateTime.MinValue;
                }     
            }                 
        }
        #endregion


    }
}
