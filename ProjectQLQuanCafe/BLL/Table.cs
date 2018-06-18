using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectQLQuanCafe.BLL
{
    public class Table
    {

        public int ID { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }
        
        public Table(DataRow row)
        {
            this.ID = (int)row[0];
            this.Name = row[1].ToString();
            this.Status = row[2].ToString();
        }

    }

}

