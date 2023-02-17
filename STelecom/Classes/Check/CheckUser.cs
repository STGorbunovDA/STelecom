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
        public string Post { get; }
        public CheckUser(string login, string post)
        {
            Login = login.Trim();
            Post = post;
        }
    }
}
