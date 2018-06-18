using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ProjectQLQuanCafe.BLL
{
    public class Account
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string PassWord { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public bool Gender { get; set; }
        public int TypeAccount { get; set; }

        public Account(string username, string fullname, string password, string address, int phone, bool gender, int typeacc)
        {
            this.UserName = username;
            this.FullName = fullname;
            this.PassWord = password;
            this.Address = address;
            this.Phone = phone;
            this.Gender = gender;
            this.TypeAccount = typeacc;
        }

        public Account(DataRow row)
        {
            this.UserName = row["Username"].ToString();
            this.FullName = row["FullName"].ToString();
            this.PassWord = row["Password"].ToString();
            this.Address = row["Address"].ToString();
            this.Phone = (int)row["Phone"];
            this.Gender = (bool)row["Gender"];
            this.TypeAccount = (int)row["TypeAccount"];
        }
    }
}
