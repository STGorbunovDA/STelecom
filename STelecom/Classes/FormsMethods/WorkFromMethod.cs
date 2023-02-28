using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using STelecom.Classes.Cheack;
using STelecom.Classes.DataBase;
using STelecom.Classes.Other;
using STelecom.DataBase;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace STelecom.Classes.FormsMethods
{
    internal class WorkFromMethod
    {
        #region состояние Rows
        enum RowState
        {
            New,
            Deleted
        }
        #endregion

        #region получение данных о бриагде ФИО Начальника и Инженера, Доверенность, № печати, Дорога

        /// <summary>
        /// Получение данных о бриагде
        /// </summary>
        /// <param name="chiefFIO">ФИО Начальника</param>
        /// <param name="engineerFIO">ФИО Инженера</param>
        /// <param name="doverennost">№ и Дата Доверенности </param>
        /// <param name="road">Дорога</param>
        /// <param name="numberPrintDocument">№ печати</param>
        /// <param name="_user">Пользователь</param>
        /// <param name="cmbRoad">Control формы "Дорога"</param>
        internal static void GettingSettingBrigadesByUser(Label chiefFIO, Label engineerFIO,
            Label doverennost, Label road, Label numberPrintDocument, CheckUser _user, ComboBox cmbRoad)
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

        #region загрузка уникальных городов по дороге

        /// <summary>
        /// загрузка уникальных городов по дороге
        /// </summary>
        /// <param name="city">ComboBox cmbCity</param>
        /// <param name="road">ComboBox cmbRoad</param>
        internal static void SelectCityGropByRoad(ComboBox city, ComboBox road)
        {
            if (!InternetCheck.CheackSkyNET())
                return;
            string querystring = $"SELECT city FROM radiostantion WHERE road = '{road.Text}' GROUP BY city";
            using (MySqlCommand command = new MySqlCommand("radiostantionSelect_1", DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue($"road", road.Text);
                DataTable table = new DataTable();
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    adapter.Fill(table);
                    if (table.Rows.Count > 0)
                    {
                        city.DataSource = table;
                        city.DisplayMember = "city";
                    }
                    else city.DataSource = null;
                    DB.GetInstance.CloseConnection();
                }
            }

        }
        #endregion

        #region Заполнение DataGridView

        /// <summary>
        /// Создание столбцов
        /// </summary>
        /// <param name="dgw"></param>
        internal static void CreateColums(DataGridView dgw)
        {
            dgw.Columns.Add("id", "№");
            dgw.Columns.Add("poligon", "Полигон");
            dgw.Columns.Add("company", "Предприятие");
            dgw.Columns.Add("location", "Место нахождения");
            dgw.Columns.Add("model", "Модель радиостанции");
            dgw.Columns.Add("serialNumber", "Заводской номер");
            dgw.Columns.Add("inventoryNumber", "Инвентарный номер");
            dgw.Columns.Add("networkNumber", "Сетевой номер");
            dgw.Columns.Add("dateTO", "Дата ТО");
            dgw.Columns.Add("numberAct", "№ акта ТО");
            dgw.Columns.Add("city", "Город");
            dgw.Columns.Add("price", "Цена ТО");
            dgw.Columns.Add("representative", "Представитель предприятия");
            dgw.Columns.Add("post", "Должность");
            dgw.Columns.Add("numberIdentification", "Номер удостоверения");
            dgw.Columns.Add("dateIssue", "Дата выдачи удостоверения");
            dgw.Columns.Add("phoneNumber", "Номер телефона");
            dgw.Columns.Add("numberActRemont", "№ акта ремонта");
            dgw.Columns.Add("category", "Категория");
            dgw.Columns.Add("priceRemont", "Цена ремонта");
            dgw.Columns.Add("antenna", "Антенна");
            dgw.Columns.Add("manipulator", "Манипулятор");
            dgw.Columns.Add("AKB", "АКБ");
            dgw.Columns.Add("batteryСharger", "ЗУ");
            dgw.Columns.Add("completed_works_1", "Выполненные работы_1");
            dgw.Columns.Add("completed_works_2", "Выполненные работы_1");
            dgw.Columns.Add("completed_works_3", "Выполненные работы_1");
            dgw.Columns.Add("completed_works_4", "Выполненные работы_1");
            dgw.Columns.Add("completed_works_5", "Выполненные работы_1");
            dgw.Columns.Add("completed_works_6", "Выполненные работы_1");
            dgw.Columns.Add("completed_works_7", "Выполненные работы_1");
            dgw.Columns.Add("parts_1", "Израсходованные материалы и детали_1");
            dgw.Columns.Add("parts_2", "Израсходованные материалы и детали_2");
            dgw.Columns.Add("parts_3", "Израсходованные материалы и детали_3");
            dgw.Columns.Add("parts_4", "Израсходованные материалы и детали_4");
            dgw.Columns.Add("parts_5", "Израсходованные материалы и детали_5");
            dgw.Columns.Add("parts_6", "Израсходованные материалы и детали_6");
            dgw.Columns.Add("parts_7", "Израсходованные материалы и детали_7");
            dgw.Columns.Add("decommissionNumber", "№ акта списания");
            dgw.Columns.Add("comment", "Примечание");
            dgw.Columns.Add("road", "Дорога");
            dgw.Columns.Add("verifiedRST", "Состояние РСТ");
            dgw.Columns.Add("IsNew", "RowState");
            dgw.Columns[12].Visible = true;
            dgw.Columns[13].Visible = false;
            dgw.Columns[14].Visible = false;
            dgw.Columns[15].Visible = false;
            dgw.Columns[16].Visible = false;
            dgw.Columns[20].Visible = false;
            dgw.Columns[21].Visible = false;
            dgw.Columns[22].Visible = false;
            dgw.Columns[23].Visible = false;
            dgw.Columns[24].Visible = false;
            dgw.Columns[25].Visible = false;
            dgw.Columns[26].Visible = false;
            dgw.Columns[27].Visible = false;
            dgw.Columns[28].Visible = false;
            dgw.Columns[29].Visible = false;
            dgw.Columns[30].Visible = false;
            dgw.Columns[31].Visible = false;
            dgw.Columns[32].Visible = false;
            dgw.Columns[33].Visible = false;
            dgw.Columns[34].Visible = false;
            dgw.Columns[35].Visible = false;
            dgw.Columns[36].Visible = false;
            dgw.Columns[37].Visible = false;
            dgw.Columns[40].Visible = false;
            dgw.Columns[41].Visible = false;
            dgw.Columns[42].Visible = false;
        }

        /// <summary>
        /// Загрузка данных в datagridview
        /// </summary>
        /// <param name="dgw"></param>
        /// <param name="city">город</param>
        /// <param name="road">дорога</param>
        internal static void RefreshDataGrid(DataGridView dgw, string city, string road)
        {
            if (!InternetCheck.CheackSkyNET())
                return;
            if (!String.IsNullOrWhiteSpace(city))
            {
                var myCulture = new CultureInfo("ru-RU");
                myCulture.NumberFormat.NumberDecimalSeparator = ".";
                Thread.CurrentThread.CurrentCulture = myCulture;
                dgw.Rows.Clear();

                using (MySqlCommand command = new MySqlCommand("radiostantionSelect_2", DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue($"city", city);
                    command.Parameters.AddWithValue($"road", road);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            int i = 0;
                            while (reader.Read())
                            {
                                ReedSingleRow(dgw, reader);
                                if (dgw.Rows[i].Cells["verifiedRST"].Value.ToString() == "+")
                                    dgw.Rows[i].Cells["serialNumber"].Style.BackColor = Color.LightGreen;
                                else if (dgw.Rows[i].Cells["verifiedRST"].Value.ToString() == "?")
                                    dgw.Rows[i].Cells["serialNumber"].Style.BackColor = Color.Yellow;
                                else if (dgw.Rows[i].Cells["verifiedRST"].Value.ToString() == "0")
                                    dgw.Rows[i].Cells["serialNumber"].Style.BackColor = Color.Red;
                                i++;
                            }
                            reader.Close();
                        }
                    }
                    command.ExecuteNonQuery();
                    DB.GetInstance.CloseConnection();
                }
            }
            else dgw.Rows.Clear();
            dgw.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dgw.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
            dgw.Columns[0].Width = 45;
            dgw.Columns[3].Width = 170;
            dgw.Columns[4].Width = 170;
            dgw.Columns[5].Width = 170;
            dgw.Columns[6].Width = 170;
            dgw.Columns[7].Width = 178;
            dgw.Columns[8].Width = 100;
            dgw.Columns[9].Width = 110;
            dgw.Columns[10].Width = 100;
            dgw.Columns[11].Width = 100;
            dgw.Columns[17].Width = 120;
            dgw.Columns[39].Width = 300;
            if (dgw.Rows.Count > 1)
                dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[0];
        }
        internal static void ReedSingleRow(DataGridView dgw, IDataRecord record)
        {
            dgw.Invoke((MethodInvoker)(() => dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2),
                record.GetString(3), record.GetString(4), record.GetString(5), record.GetString(6), record.GetString(7),
                Convert.ToDateTime(record.GetString(8)), record.GetString(9), record.GetString(10), record.GetDecimal(11),
                record.GetString(12), record.GetString(13), record.GetString(14), record.GetString(15), record.GetString(16),
                record.GetString(17), record.GetString(18), record.GetDecimal(19), record.GetString(20), record.GetString(21),
                record.GetString(22), record.GetString(23), record.GetString(24), record.GetString(25), record.GetString(26),
                record.GetString(27), record.GetString(28), record.GetString(29), record.GetString(30), record.GetString(31),
                record.GetString(32), record.GetString(33), record.GetString(34), record.GetString(35), record.GetString(36),
                record.GetString(37), record.GetString(38), record.GetString(39), record.GetString(40), record.GetString(41),
                RowState.New)));
        }
        #endregion

        #region TimerEventProcessor 

        #region Заполнение DataGridView2 для Task TimerEventProcessor 

        /// <summary>
        /// Загрузка данных в datagridview2 для Task TimerEventProcessor 
        /// </summary>
        /// <param name="dgw"></param>
        /// <param name="city">город</param>
        /// <param name="road">дорога</param>
        internal static void RefreshDataGridTimerEventProcessor(DataGridView dgw, string city, string road)
        {
            if (!InternetCheck.CheackSkyNET())
                return;
            if (!String.IsNullOrWhiteSpace(city))
            {
                var myCulture = new CultureInfo("ru-RU");
                myCulture.NumberFormat.NumberDecimalSeparator = ".";
                Thread.CurrentThread.CurrentCulture = myCulture;
                dgw.Rows.Clear();
                using (MySqlCommand command = new MySqlCommand("radiostantionSelect_2", DB.GetInstance.GetConnection()))
                {
                    DB.GetInstance.OpenConnection();
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue($"city", city);
                    command.Parameters.AddWithValue($"road", road);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                                ReedSingleRowTimerEventProcessor(dgw, reader);
                            reader.Close();
                        }
                    }
                    command.ExecuteNonQuery();
                    DB.GetInstance.CloseConnection();
                }
            }

            dgw.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dgw.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
            dgw.Columns[0].Width = 45;
            dgw.Columns[3].Width = 170;
            dgw.Columns[4].Width = 170;
            dgw.Columns[5].Width = 170;
            dgw.Columns[6].Width = 170;
            dgw.Columns[7].Width = 178;
            dgw.Columns[8].Width = 100;
            dgw.Columns[9].Width = 110;
            dgw.Columns[10].Width = 100;
            dgw.Columns[11].Width = 100;
            dgw.Columns[17].Width = 120;
            dgw.Columns[39].Width = 300;
            dgw.CurrentCell = dgw.Rows[dgw.Rows.Count - 1].Cells[0];

        }
        internal static void ReedSingleRowTimerEventProcessor(DataGridView dgw, IDataRecord record)
        {
            dgw.Invoke((MethodInvoker)(() => dgw.Rows.Add(record.GetInt32(0), record.GetString(1), record.GetString(2),
                record.GetString(3), record.GetString(4), record.GetString(5), record.GetString(6), record.GetString(7),
                Convert.ToDateTime(record.GetString(8)), record.GetString(9), record.GetString(10), record.GetDecimal(11),
                record.GetString(12), record.GetString(13), record.GetString(14), record.GetString(15), record.GetString(16),
                record.GetString(17), record.GetString(18), record.GetDecimal(19), record.GetString(20), record.GetString(21),
                record.GetString(22), record.GetString(23), record.GetString(24), record.GetString(25), record.GetString(26),
                record.GetString(27), record.GetString(28), record.GetString(29), record.GetString(30), record.GetString(31),
                record.GetString(32), record.GetString(33), record.GetString(34), record.GetString(35), record.GetString(36),
                record.GetString(37), record.GetString(38), record.GetString(39), record.GetString(40), record.GetString(41),
                RowState.New)));
        }

        #endregion

        /// <summary>
        /// Сохранение таблицы DataGridView в Json на PC
        /// </summary>
        /// <param name="dgw"></param>
        /// <param name="city"></param>
        internal static void GetSaveDataGridViewInJson(DataGridView dgw, string city)
        {
            JArray products = new JArray();

            foreach (DataGridViewRow row in dgw.Rows)
            {
                JObject product = JObject.FromObject(new
                {
                    id = row.Cells[0].Value,
                    poligon = row.Cells[1].Value,
                    company = row.Cells[2].Value,
                    location = row.Cells[3].Value,
                    model = row.Cells[4].Value,
                    serialNumber = row.Cells[5].Value,
                    inventoryNumber = row.Cells[6].Value,
                    networkNumber = row.Cells[7].Value,
                    dateTO = Convert.ToDateTime(row.Cells[8].Value).ToString("dd.MM.yyyy"),
                    numberAct = row.Cells[9].Value,
                    city = row.Cells[10].Value,
                    price = row.Cells[11].Value,
                    representative = row.Cells[12].Value,
                    post = row.Cells[13].Value,
                    numberIdentification = row.Cells[14].Value,
                    dateIssue = row.Cells[15].Value,
                    phoneNumber = row.Cells[16].Value,
                    numberActRemont = row.Cells[17].Value,
                    category = row.Cells[18].Value,
                    priceRemont = row.Cells[19].Value,
                    antenna = row.Cells[20].Value,
                    manipulator = row.Cells[21].Value,
                    AKB = row.Cells[22].Value,
                    batteryСharger = row.Cells[23].Value,
                    completed_works_1 = row.Cells[24].Value,
                    completed_works_2 = row.Cells[25].Value,
                    completed_works_3 = row.Cells[26].Value,
                    completed_works_4 = row.Cells[27].Value,
                    completed_works_5 = row.Cells[28].Value,
                    completed_works_6 = row.Cells[29].Value,
                    completed_works_7 = row.Cells[30].Value,
                    parts_1 = row.Cells[31].Value,
                    parts_2 = row.Cells[32].Value,
                    parts_3 = row.Cells[33].Value,
                    parts_4 = row.Cells[34].Value,
                    parts_5 = row.Cells[35].Value,
                    parts_6 = row.Cells[36].Value,
                    parts_7 = row.Cells[37].Value,
                    decommissionSerialNumber = row.Cells[38].Value,
                    comment = row.Cells[39].Value,
                    road = row.Cells[40].Value,
                    verifiedRST = row.Cells[41].Value
                });
                products.Add(product);
            }

            string json = JsonConvert.SerializeObject(products);

            string fileNamePath = $@"C:\Documents_ServiceTelekom\БазаДанныхJson\{city}\БазаДанныхJson.json";

            if (!File.Exists($@"С:\Documents_ServiceTelekom\БазаДанныхJson\{city}\"))
                Directory.CreateDirectory($@"C:\Documents_ServiceTelekom\БазаДанныхJson\{city}\");
            File.WriteAllText(fileNamePath, json);
        }

        /// <summary>
        /// Сохранение резервного файла CSV на PC 
        /// </summary>
        /// <param name="dgw"></param>
        /// <param name="city"></param>
        internal static void AutoSaveFilePC(DataGridView dgw, string city)
        {
            DateTime today = DateTime.Today;
            if (File.Exists($@"C:\Documents_ServiceTelekom\БазаДанныхExcel\{city}\БазаДанных-{city}-{today.ToString("dd.MM.yyyy")}.csv"))
                File.Delete($@"C:\Documents_ServiceTelekom\БазаДанныхExcel\{city}\БазаДанных-{city}-{today.ToString("dd.MM.yyyy")}.csv");
            string fileNamePath = $@"C:\Documents_ServiceTelekom\БазаДанныхExcel\{city}\БазаДанных-{city}-{today.ToString("dd.MM.yyyy")}.csv";
            if (!File.Exists($@"С:\Documents_ServiceTelekom\БазаДанныхExcel\{city}\"))
                Directory.CreateDirectory($@"C:\Documents_ServiceTelekom\БазаДанныхExcel\{city}\");
            using (StreamWriter sw = new StreamWriter(fileNamePath, false, Encoding.Unicode))
            {
                string note = string.Empty;
                note += $"Полигон\tПредприятие\tМесто нахождения\tМодель\tЗаводской номер\t" +
                    $"Инвентарный номер\tСетевой номер\tДата проведения ТО\tНомер акта\tГород\tЦена ТО\t" +
                    $"Представитель предприятия\tДолжность\tНомер удостоверения\tДата выдачи\tНомер телефона\t" +
                    $"Номер Акта ремонта\tКатегория\tЦена ремонта\tАнтенна\tМанипулятор\tАКБ\tЗУ\tВыполненные работы_1\t" +
                    $"Выполненные работы_2\tВыполненные работы_3\tВыполненные работы_4\tВыполненные работы_5\t" +
                    $"Выполненные работы_6\tВыполненные работы_7\tДеталь_1\tДеталь_2\tДеталь_3\tДеталь_4\tДеталь_5\t" +
                    $"Деталь_6\tДеталь_7\t№ Акта списания\tПримечания\tДорога\tСостояние РСТ";
                sw.WriteLine(note);
                for (int i = 0; i < dgw.Rows.Count; i++)
                {
                    for (int j = 0; j < dgw.ColumnCount; j++)
                    {
                        Regex re = new Regex(Environment.NewLine);
                        string value = dgw.Rows[i].Cells[j].Value.ToString();
                        value = re.Replace(value, " ");
                        if (dgw.Columns[j].HeaderText.ToString() == "№")
                        {

                        }
                        else if (dgw.Columns[j].HeaderText.ToString() == "Дата ТО")
                            sw.Write(Convert.ToDateTime(value).ToString("dd.MM.yyyy") + "\t");
                        else if (dgw.Columns[j].HeaderText.ToString() == "Состояние РСТ")
                            sw.Write(value);
                        else if (dgw.Columns[j].HeaderText.ToString() == "RowState")
                        {

                        }
                        else sw.Write(value + "\t");
                    }
                    sw.WriteLine();
                }
            }
        }

        /// <summary>
        /// Копирование БД
        /// </summary>
        internal static void CopyDataBaseRadiostantionInRadiostantionCopy()
        {
            if (!InternetCheck.CheackSkyNET())
                return;
            using (MySqlCommand command = new MySqlCommand("radiostantion_copy_1", DB2.GetInstance.GetConnection()))
            {
                DB2.GetInstance.OpenConnection();
                command.ExecuteNonQuery();
                DB2.GetInstance.CloseConnection();
            }
            using (MySqlCommand command2 = new MySqlCommand("radiostantion_copy_2", DB2.GetInstance.GetConnection()))
            {

                DB2.GetInstance.OpenConnection();
                command2.ExecuteNonQuery();
                DB2.GetInstance.CloseConnection();
            }
        }

        #endregion

        #region загрузка всех данных радиостанций без города по всей дороге
        /// <summary>
        /// Метод получения данных проверки ТО радиостанций по дороге без города
        /// </summary>
        /// <param name="dgw"></param>
        /// <param name="road"></param>
        internal static void FullDataBase(DataGridView dgw, string road)
        {
            if (!InternetCheck.CheackSkyNET())
                return;
            var myCulture = new CultureInfo("ru-RU");
            myCulture.NumberFormat.NumberDecimalSeparator = ".";
            Thread.CurrentThread.CurrentCulture = myCulture;
            dgw.Rows.Clear();
            using (MySqlCommand command = new MySqlCommand("radiostantionSelect_3", DB.GetInstance.GetConnection()))
            {
                DB.GetInstance.OpenConnection();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue($"road", road);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                            ReedSingleRow(dgw, reader);
                        reader.Close();
                    }
                }
                command.ExecuteNonQuery();
                DB.GetInstance.CloseConnection();
            }
            dgw.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dgw.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCells);
            dgw.Columns[0].Width = 45;
            dgw.Columns[3].Width = 170;
            dgw.Columns[4].Width = 180;
            dgw.Columns[5].Width = 150;
            dgw.Columns[6].Width = 178;
            dgw.Columns[7].Width = 178;
            dgw.Columns[8].Width = 100;
            dgw.Columns[9].Width = 110;
            dgw.Columns[10].Width = 100;
            dgw.Columns[11].Width = 100;
            dgw.Columns[17].Width = 120;
        }
        #endregion
    }
}
