using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ProjectQLQuanCafe.BLL
{
    public class CategoryDuc
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public CategoryDuc(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }

        public CategoryDuc(DataRow row)
        {
            this.ID = (int)row["id"];
            this.Name = row["name"].ToString();
        }
    }
}
