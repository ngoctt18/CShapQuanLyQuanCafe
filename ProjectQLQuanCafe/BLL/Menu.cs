using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ProjectQLQuanCafe.BLL
{
    public class Menu
    {
        public string FoodName { get; set; }
        public float Price { get; set; }
        public int Amount { get; set; }
        public float TotalPrice { get; set; }

        public Menu(string foodName, float price, int amount, float totalPrice)
        {
            this.FoodName = foodName;
            this.Price = price;
            this.Amount = amount;
            this.TotalPrice = totalPrice;
        }
        public Menu(DataRow row)
        {
            this.FoodName = row["name"].ToString();
            this.Amount = (int)row["amount"];
            this.Price = (float)Convert.ToDouble(row["price"].ToString());
            this.TotalPrice = (float)Convert.ToDouble(row["TongTien"].ToString());
        }

    }
}
