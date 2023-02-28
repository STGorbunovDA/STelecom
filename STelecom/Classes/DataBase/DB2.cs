using MySql.Data.MySqlClient;
using STelecom.Classes.Other;

namespace STelecom.Classes.DataBase
{
    internal class DB2
    {
        static volatile DB2 Class;
        static object SyncObject = new object();
        public static DB2 GetInstance
        {
            get
            {
                if (Class == null)
                    lock (SyncObject)
                    {
                        if (Class == null)
                            Class = new DB2();
                    }
                return Class;
            }
        }
        readonly MySqlConnection connection = new MySqlConnection($"server=31.31.198.62;port=3306;" +
            $"username={Encryption.DecryptCipherTextToPlainText("vKGbDdqaoW9LHTIUufr0vw==")};" +
            $"password={Encryption.DecryptCipherTextToPlainText("fuB1hXCQ1pYUBw+qqevUc/HVeLjqAEtcds46B6NqcLs=")};" +
            $"database={Encryption.DecryptCipherTextToPlainText("vKGbDdqaoW+93a+IIGwtEvH5h0BsY+fx")};" +
            $"charset=utf8");
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
