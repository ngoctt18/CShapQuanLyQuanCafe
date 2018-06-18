using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ProjectQLQuanCafe.DAL
{
    public class DataProvider
    {
        private string connString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyQuanCafe;Integrated Security=True";

        public static object Instance { get; internal set; }

        public SqlConnection getConnect()
        {
            return new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyQuanCafe;Integrated Security=True");
        }

        // Lệnh sql trả về một bảng
        public DataTable ExecuteQuery(string sql)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                da.Fill(ds);

                dt = ds.Tables[0];
                conn.Close();
            }
            return (dt);
        }

        // Lệnh sql không trả về bảng, trả về số dòng thành công
        public int ExecuteNonQuery(string sql)
        {
            int dt = 0;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                dt = cmd.ExecuteNonQuery();
                conn.Close();
            }
            return (dt);
        }

        // Lệnh sql trả về số lượng đếm
        public object ExecuteScalar(string sql)
        {
            object dt = 0;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(sql, conn);
                dt = cmd.ExecuteScalar();
                conn.Close();
            }
            return (dt);
        }


        // Code Ngoc
        // Lệnh Sql trả về 1 bảng
        public DataTable GetTable(string sql)
        {
            SqlConnection conn = getConnect();
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();

            da.Fill(dt);
            return (dt);
        }
        // Lệnh sql không trả về bảng
        public void UnGetTable(string sql)
        {
            SqlConnection conn = getConnect();
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cmd.Clone();
        }

    }
}
