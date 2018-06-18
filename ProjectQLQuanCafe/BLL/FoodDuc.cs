using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ProjectQLQuanCafe.BLL
{
    public class FoodDuc
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int IDFoodCategory { get; set; }

        public FoodDuc(int id, string name, float price, int idFoodCategory)
        {
            this.ID = id;
            this.Name = name;
            this.Price = price;
            this.IDFoodCategory = IDFoodCategory;
        }

        public FoodDuc(DataRow row)
        {
            this.ID = (int)row["id"];
            this.Name = row["name"].ToString();
            this.Price = (float)Convert.ToDouble(row["price"].ToString());
            this.IDFoodCategory = (int)row["idFoodCategory"];
        }
    }
}
