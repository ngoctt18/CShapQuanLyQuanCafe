using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ProjectQLQuanCafe.DAL;

namespace ProjectQLQuanCafe.BLL
{
    public class AdminMonAn
    {
        DataProvider da = new DataProvider();
        public DataTable GetListMonAn()
        {
            string sql = "SELECT f.id AS 'Mã món', f.name AS 'Tên món', f.price AS 'Giá', c.name AS 'Tên danh mục' FROM Food AS f, FoodCategory AS c WHERE f.idFoodCategory = c.id";
            DataTable dt = new DataTable();
            dt = da.GetTable(sql);
            return dt;
        }

        public void insertMonAn(string name, float price, int idC)
        {
            string sql = "INSERT INTO Food VALUES (N'"+ name + "', '"+ price + "', '"+ idC + "')";
            da.UnGetTable(sql);
        }

        public void updateMonAn(string idFood, string name, float price, int idC)
        {
            string sql = "Update Food Set name = N'" + name + "', idFoodCategory = '" + idC + "', price = '" + price + "' Where id = '" + idFood + "' ";
            da.UnGetTable(sql);
        }

        OrderDetail orderDetail = new OrderDetail();
        public void deleteMonAn(string idFood)
        {
            orderDetail.DeleteOrderDetailByFoodID(Convert.ToInt32(idFood));

            string sql = "Delete Food Where id = '" + idFood + "'";
            da.UnGetTable(sql);
        }

        public DataTable searchMonAn(string dk)
        {
            string sql = "SELECT f.id AS 'Mã món', f.name AS 'Tên món', f.price AS 'Giá', c.name AS 'Tên danh mục' FROM Food AS f, FoodCategory AS c WHERE f.idFoodCategory = c.id AND f.name LIKE N'%" + dk + "%'";
            DataTable dt = new DataTable();
            dt = da.GetTable(sql);
            return (dt);
        }
    }
}
