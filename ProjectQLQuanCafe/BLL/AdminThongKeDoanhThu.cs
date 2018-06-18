using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ProjectQLQuanCafe.DAL
{
    class ThongKeDoanhThu
    {
        DataProvider da = new DataProvider();
        public DataTable GetListDoanhThu(DateTime TimeCheckIn, DateTime TimeCheckOut)
        {
            string sql = "SELECT FoodTable.TableName AS 'Tên bàn', " +
                "FoodOrder.totalPrice AS 'Tổng tiền', TimeCheckIn, " +
                "TimeCheckOut, discount AS 'Giảm giá' FROM FoodTable, " +
                "FoodOrder WHERE FoodTable.id = FoodOrder.idFoodTable AND " +
                "TimeCheckIn >= '" + TimeCheckIn.ToString("yyyy-MM-dd") + 
                "' AND TimeCheckOut <=  '" + TimeCheckOut.ToString("yyyy-MM-dd") + 
                "' AND FoodOrder.status = 1";
            DataTable dt = new DataTable();
            dt = da.GetTable(sql);
            return dt;
        }
    }
}
