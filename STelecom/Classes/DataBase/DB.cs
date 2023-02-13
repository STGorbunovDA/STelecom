using MySql.Data.MySqlClient;

namespace STelecom.DataBase
{
    class DB
    {
        static volatile DB Class;
        static object SyncObject = new object();
        public static DB GetInstance
        {
            get
            {
                if (Class == null)
                    lock (SyncObject)
                    {
                        if (Class == null)
                            Class = new DB();
                    }
                return Class;
            }
        }
        readonly MySqlConnection connection = new MySqlConnection("server=31.31.198.62;port=3306;username=u1748936_admin;password=tankT34PodedaZaNamivVv;database=u1748936_stelecom;charset=utf8");
        public MySqlConnection GetConnection()
        {
            return connection;
        }

        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }

        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }
    }
}
