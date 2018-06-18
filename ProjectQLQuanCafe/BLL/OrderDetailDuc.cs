using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ProjectQLQuanCafe.BLL
{
    public class OrderDetailDuc
    {
        public int ID { get; set; }
        public int FoodOrderID { get; set; }
        public int FoodID { get; set; }
        public int Amount { get; set; }

        public OrderDetailDuc(int id, int foodOrderID, int foodID, int amount)
        {
            this.ID = id;
            this.FoodOrderID = foodOrderID;
            this.FoodID = foodID;
            this.Amount = amount;
        }

        public OrderDetailDuc(DataRow row)
        {
            this.ID = (int)row["id"];
            this.FoodOrderID = (int)row["idFoodOrder"];
            this.FoodID = (int)row["idFood"];
            this.Amount = (int)row["amount"];
        }
    }
}
