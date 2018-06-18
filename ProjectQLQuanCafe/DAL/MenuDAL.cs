using ProjectQLQuanCafe.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ProjectQLQuanCafe.DAL
{
    public class MenuDAL
    {
        DataProvider dtPro = new DataProvider();
        public List<Menu> GetListMenuByTableID(int id)
        {
            List<Menu> listMenu = new List<Menu>();
            string query = "select Food.name, OrderDetail.amount, Food.price, OrderDetail.amount*Food.price as TongTien from OrderDetail, FoodOrder, Food where OrderDetail.idFoodOrder = FoodOrder.id AND OrderDetail.idFood = Food.id AND FoodOrder.Status = 0 AND FoodOrder.idFoodTable = " + id;
            DataTable dt = dtPro.ExecuteQuery(query);
            foreach (DataRow item in dt.Rows)
            {
                Menu m = new Menu(item);
                listMenu.Add(m);
            }
            return listMenu;
        }
    }
}
