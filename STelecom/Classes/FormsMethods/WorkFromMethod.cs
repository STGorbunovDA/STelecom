using MySql.Data.MySqlClient;
using STelecom.Classes.Cheack;
using STelecom.Classes.Other;
using STelecom.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STelecom.Classes.FormsMethods
{
    internal class WorkFromMethod
    {
        #region получение данных о бриагде ФИО Начальника и Инженера, Доверенность, № печати, Дорога
        internal static void GettingTeamData(Label chiefFIO, Label engineerFIO, Label doverennost, Label road, Label numberPrintDocument, CheckUser _user, ComboBox cmbRoad)
        {
            //string userLogin = Encryption.DecryptCipherTextToPlainText(_user.Login);
            if (_user.Post == "Admin" || _user.Post == "Руководитель")
                cmbRoad.Text = cmbRoad.Items[0].ToString();
            else
            {
                if (!InternetCheck.CheackSkyNET())
                    return;
                using (MySqlCommand command = new MySqlCommand("settingBrigadesSelect_7", DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue($"userLogin", _user.Login);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            chiefFIO.Text = Encryption.DecryptCipherTextToPlainText(reader[1].ToString());
                            engineerFIO.Text = Encryption.DecryptCipherTextToPlainText(reader[2].ToString());
                            doverennost.Text = Encryption.DecryptCipherTextToPlainText(reader[3].ToString());
                            road.Text = Encryption.DecryptCipherTextToPlainText(reader[4].ToString());
                            numberPrintDocument.Text = Encryption.DecryptCipherTextToPlainText(reader[5].ToString());
                        }
                        reader.Close();
                    }
                }

                using (MySqlCommand command = new MySqlCommand("settingBrigadesSelect_8", DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue($"userLogin", _user.Login);
                    DataTable table = new DataTable();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(table);
                        if (table.Rows.Count > 0)
                        {
                            cmbRoad.Items.Clear();
                            foreach (DataRow row in table.Rows)
                            {
                                string login = Encryption.DecryptCipherTextToPlainText(row["road"].ToString());
                                cmbRoad.Items.Add(login);
                            }
                            cmbRoad.Text = cmbRoad.Items[0].ToString();
                        }
                        else
                        {
                            MessageBox.Show($"Системная ошибка добавления дороги в Control ComboBox ({_user.Login})");
                            System.Environment.Exit(0);
                        }
                    }
                }
            }
        }

        #endregion
    }
}
