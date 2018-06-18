using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ProjectQLQuanCafe.BLL;

namespace ProjectQLQuanCafe.DAL
{
    public class AccountDAL
    {
        DataProvider dtPro = new DataProvider();

        public bool Login(string userName, string password)
        {
            string query = "Select * from dbo.Account WHERE UserName = '" + userName + "' AND Password = '" + password + "' "; //"USP_login @userName, @password";

            DataTable result = dtPro.ExecuteQuery(query);
            return result.Rows.Count > 0;
        }
        //hop
        public void UpdateAccount(string userName, string fullName, string NewPassword, string Address, int phone, bool gt)
        {
            //string sql = "Update Account Set FullName = N'" + fullName + "', address = N'" + Address + "', phone = '" + phone + "', gender = '" + gt + "' Where Username = '" + userName + "' ";

            string sql = "Update Account Set FullName = N'" + fullName + "', Password = '" + NewPassword + "', address = N'" + Address + "', phone = '" + phone + "', gender = '" + gt + "' Where Username = '" + userName + "' ";

            //dtPro.UnGetTable(sql);
            dtPro.ExecuteNonQuery(sql);
        }

        public bool CheckPass(string userName, string newpassword)
        {
            string sql = "select * from Account Where Username = '" + userName + "' AND Password = '" + newpassword + "' ";

            DataTable result = dtPro.ExecuteQuery(sql);
            if(result.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

            //dtPro.UnGetTable(sql);
            //dtPro.GetTable(sql);
        }

        public Account GetAccountByUserName(string userName)
        {
            DataTable data = dtPro.ExecuteQuery("Select * from Account where userName= '" + userName + "'");
            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }

            return null;
        }

        internal void GetAccountByUserName()
        {
            throw new NotImplementedException();
        }
    }
}
