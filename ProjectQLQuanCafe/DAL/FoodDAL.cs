using ProjectQLQuanCafe.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ProjectQLQuanCafe.DAL
{
    public class FoodDAL
    {
        DataProvider dtPro = new DataProvider();
        public List<FoodDuc> GetFoodByCategoryID(int id)
        {
            List<FoodDuc> listFood = new List<FoodDuc>();
            string query = "select * from Food where idFoodCategory = " + id;
            DataTable dt = dtPro.ExecuteQuery(query);
            foreach(DataRow item in dt.Rows)
            {
                FoodDuc food = new FoodDuc(item);
                listFood.Add(food);
            }

            return listFood;
        }
    }
}
