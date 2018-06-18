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
    class AdminDanhMuc
    {
        DataProvider da = new DataProvider();
        public DataTable GetListdanhMuc()
        {
            string sql = "Select * From FoodCategory";
            DataTable dt = new DataTable();
            dt = da.GetTable(sql);
            return dt;
        }

        public List<Category> GetListCategory()
        {
            List<Category> list = new List<Category>();
            string sql = "Select * From FoodCategory";
            DataTable dt = new DataTable();
            dt = da.GetTable(sql);
            foreach (DataRow item in dt.Rows)
            {
                Category category = new Category(item);
                list.Add(category);
            }
            return (list);
        }

        public Category GetCategoryByID(int id)
        {
            Category category = null;
            string sql = "Select * From FoodCategory Where id = '" + id + "'";
            DataTable dt = new DataTable();
            foreach (DataRow item in dt.Rows)
            {
                category = new Category(item);
                return category;
            }

            return category;
        }

        public DataTable GetIDCategoryByName(string name)
        {
            string sql = "Select id From FoodCategory Where name = N'" + name + "'";
            DataTable dt = new DataTable();
            dt = da.GetTable(sql);
            return (dt);
        }

        public void insertDanhMuc(string nameC)
        {
            string sql = "Insert Into FoodCategory Values (N'" + nameC + "')";
            da.UnGetTable(sql);
        }

        public void updateDanhMuc(string idCategory, string name)
        {
            string sql = "Update FoodCategory Set name = N'" + name + "' Where id = '" + idCategory + "'";
            da.UnGetTable(sql);
        }

        public void DeleteFoodByIDCategory(int idCategory)
        {
            string sql = "Delete Food Where idFoodCategory = '" + idCategory + "'";
            da.UnGetTable(sql);
        }
        public void deleteDanhMuc(string idCategory)
        {
            DeleteFoodByIDCategory(Convert.ToInt32(idCategory));

            string sql = "Delete FoodCategory Where id = '" + idCategory + "'";
            da.UnGetTable(sql);
        }
    }
}
