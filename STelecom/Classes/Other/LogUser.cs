using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace STelecom.Classes.Other
{
    internal class LogUser
    {
        /// <summary>
        /// Храним Log-и с ошибками.
        /// </summary>
        /// <param name="exception"></param>
        internal static void LogExceptionUserSaveFilePC(string exception)
        {
            try
            {
                DateTime today = DateTime.Now;

                string fileNamePath = $@"C:\Documents_ServiceTelekom\Log\Ошибка-{today.ToString("dd.MM.yyyy")}.txt";

                if (!File.Exists($@"C:\Documents_ServiceTelekom\Log\"))
                    Directory.CreateDirectory($@"C:\Documents_ServiceTelekom\Log\");

                using (StreamWriter sw = new StreamWriter(fileNamePath, true, Encoding.Unicode))
                {
                    sw.WriteLine($"{exception}\t{today}");
                    sw.WriteLine($"__________________________");
                    sw.WriteLine(string.Empty);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Файл не сохранен!(LogExceptionUserSaveFilePC)");
            }
        }
    }
}
