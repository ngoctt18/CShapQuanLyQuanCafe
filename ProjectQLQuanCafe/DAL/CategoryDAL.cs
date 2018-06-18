using ProjectQLQuanCafe.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ProjectQLQuanCafe.DAL
{
    public class CategoryDAL
    {
        DataProvider dtPro = new DataProvider();
        public List<CategoryDuc> GetListFoodCategory()
        {
            List<CategoryDuc> listCategory = new List<CategoryDuc>();
            string query = "select * from FoodCategory";
            DataTable dt = dtPro.ExecuteQuery(query);
            foreach (DataRow item in dt.Rows)
            {
                CategoryDuc category = new CategoryDuc(item);
                listCategory.Add(category);
            }
            return listCategory;
        }
        
    }
}
