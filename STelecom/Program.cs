using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace STelecom
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new ThreadExceptionEventHandler(ExceptionGlobal);
            Application.Run(new AuthorizationForm());
        }

        static void ExceptionGlobal(object sender, ThreadExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
