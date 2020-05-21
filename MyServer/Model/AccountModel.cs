using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyServer.Model
{
    public class AccountModel
    {
        public int id { get; set; }
        public string account { get; set; }
        public string password { get; set; }
        public AccountModel()
        {

        }
        public AccountModel(string account,string password)
        {
            this.account = account;
            this.password = password;
        }
    }
}
