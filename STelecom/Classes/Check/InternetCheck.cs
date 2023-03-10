using System;
using System.Net;
using System.Windows.Forms;

namespace STelecom.Classes.Cheack
{
    class InternetCheck
    {
        /// <summary>
        /// Проверка Интернета
        /// </summary>
        /// <returns></returns>
        public static bool CheackSkyNET()
        {
            try
            {
                Dns.GetHostEntry("yandex.com");
                return true;
            }
            catch (Exception)
            {
                MessageBox.Show(@"Отсутствует подключение к Интернету. Проверьте настройки сети и повторите попытку",
                        "Сеть недоступна");
                return false;
            }
        }
    }
}
