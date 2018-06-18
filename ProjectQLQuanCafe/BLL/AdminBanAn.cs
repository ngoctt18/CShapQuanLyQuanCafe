using ProjectQLQuanCafe.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
//using ProjectQLQuanCafe.DAL;

namespace ProjectQLQuanCafe.BLL
{
    class AdminBanAn
    {
        DataProvider da = new DataProvider();
        public DataTable GetListBanAn()
        {
            string sql = "Select * From FoodTable";
            DataTable dt = new DataTable();
            dt = da.GetTable(sql);
            return (dt);
        }

        public void insertBanAn(string name, string status)
        {
            string sql = "insert Into FoodTable Values (N'" + name + "', N'" + status + "')";
            da.UnGetTable(sql);
        }

        public void updateBanAn(string idFoodTable, string name, string status)
        {
            string sql = "Update FoodTable Set TableName = N'" + name + "', status = N'" + status + "' Where id = '" + idFoodTable + "'";
            da.UnGetTable(sql);
        }

        public void deleteBanAn(string idFoodTable)
        {
            string sql = "Delete FoodTable Where id = '" + idFoodTable + "' ";
            da.UnGetTable(sql);
        }

    }
}
