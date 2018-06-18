using ProjectQLQuanCafe.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ProjectQLQuanCafe.BLL
{
    class AdminTaiKhoan
    {
        DataProvider da = new DataProvider();
        public DataTable GetListTaiKhoan()
        {
            string sql = "Select Username, Fullname, address, phone, gender, typeAccount From Account";
            DataTable dt = new DataTable();
            dt = da.GetTable(sql);
            return (dt);
        }

        public void insertTaiKhoan(string Username, string FullName, string address, int phone, Boolean gt, int type)
        {
            string sql = "Insert Into Account (Username, FullName, address, phone, gender, TypeAccount) Values (N'" + Username + "' , N'" + FullName + "', N'" + address + "', '" + phone + "', '" + gt + "', '" + type + "')";
            da.UnGetTable(sql);
        }

        public void updateTaiKhoan(string Username, string FullName, string address, int phone, Boolean gt, int type)
        {
            string sql = "Update Account Set FullName = N'" + FullName + "', address = N'" + address + "', phone = '" + phone + "', gender = '" + gt + "', TypeAccount = '" + type + "' Where Username = N'" + Username + "' ";
            da.UnGetTable(sql);
        }

        public void deleteTaiKhoan(string Username)
        {
            string sql = "Delete Account Where Username = N'" + Username + "'";
            da.UnGetTable(sql);
        }
    }
}
