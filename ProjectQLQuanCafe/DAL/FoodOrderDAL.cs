using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ProjectQLQuanCafe.BLL;

namespace ProjectQLQuanCafe.DAL
{
    public class FoodOrderDAL
    {
        DataProvider dtPro = new DataProvider();
        public int GetUnCheckOrderByTableID(int id)
        {
            string query = "select * from FoodOrder where idFoodTable = "+ id +" AND Status = 0";
            DataTable dt = dtPro.ExecuteQuery(query);
            if(dt.Rows.Count > 0)
            {
                FoodOrderDuc order = new FoodOrderDuc(dt.Rows[0]);
                return order.ID;
            }
            return -1;
        }

        
        public void CheckOut(int id, float tong, int discount)
        {
            //string query = "update FoodOrder SET Status = 1 where id = " + id;
            string query = "update FoodOrder SET Status = 1, totalPrice = '" + tong + "', discount = '"+ discount +"' where id = " + id;
            dtPro.ExecuteNonQuery(query);
        }
        
        public void InsertOrder(int idTable, int discount)
        {
            //string query = "insert into FoodOrder values('"+ GETDATE() + "', '"+ null + "', '"+ 0 +"', '"+ idTable +"' )";
            string query = "EXEC proc_InsertOrder '" + idTable + "', '" + discount +"'";
            dtPro.ExecuteNonQuery(query);
        }

        /*
        private string GETDATE()
        {
            throw new NotImplementedException();
        }
        */

        public int GetMaxIDOrder()
        {
            try
            {
                return (int)dtPro.ExecuteScalar("select MAX(id) from FoodOrder");
            }
            catch
            {
                return 1;
            }
        }
    }

}
