using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STelecom.Classes.Cheack
{
    public class CheckUser
    {
        public string Login { get; set; }
        public string IsAdmin { get; }
        public CheckUser(string login, string isAdmin)
        {
            Login = login.Trim();
            IsAdmin = isAdmin;
        }
    }
}
