using ProjectQLQuanCafe.BLL;
using ProjectQLQuanCafe.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectQLQuanCafe
{
    public partial class fAccount : Form
    {
        private Account loginAccount;

        AccountDAL accDAL = new AccountDAL();
        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount); }
        }

        public fAccount(Account acc)
        {
            InitializeComponent();

            LoginAccount = acc;
        }
        void ChangeAccount(Account acc)
        {
            txtDangNhap.Text = LoginAccount.UserName;
            txtHoTen.Text = LoginAccount.FullName;
            txtDiaChi.Text = LoginAccount.Address;
            txtDT.Text = Convert.ToString(LoginAccount.Phone);
            if (LoginAccount.Gender == true)
            {
                rdoNam.Checked = true;
            }
            else
            {
                rdoNu.Checked = true;
            }
        }
        //h
        private void fAccount_Load(object sender, EventArgs e)
        {

        }
        /*
        void UpdateAccount()
        {
            string fullName = txtHoTen.Text;
            string password = txtMatKhau.Text;
            string newpass = txtMatKhauMoi.Text;
            string reenterPass = txtXacNhanMatKhau.Text;
            string userName = txtDangNhap.Text;
            string address = txtDiaChi.Text;
            int phone = Int32.Parse(txtDT.Text);

            bool gt;
            if (rdoNam.Checked == true)
            {
                gt = true;
            }
            else
            {
                gt = false;
            }

            if (!newpass.Equals(reenterPass))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu đúng với mật khẩu mới !");
            }
            else
            {
                //if (accDAL.UpdateAccount(userName, fullName, password, newpass, address, phone, gt))
                //{
                //    MessageBox.Show("Cập nhật thành công !!!");
                //}
                //else
                //{
                //    MessageBox.Show("Vui lòng điền đúng mật khẩu !!!");         
                //}
            }
        }
        */

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            txtDangNhap.Enabled = false;
            string userName = txtDangNhap.Text;

            accDAL.GetAccountByUserName(userName);

            string fullName = txtHoTen.Text;
            string Address = txtDiaChi.Text;
            int phone = Int32.Parse(txtDT.Text);
            bool gt;

            if (rdoNam.Checked == true)
            {
                gt = true;
            }
            else
            {
                gt = false;
            }
            string newPass = txtMatKhauMoi.Text;
            string confirmPass = txtXacNhanMatKhau.Text;

            if (txtMatKhau.Text == "")
            {
                MessageBox.Show("Nhập vào mật khẩu");
            }
            else if(accDAL.CheckPass(userName, newPass) == true)
            {
                MessageBox.Show("Nhập mật khẩu trùng với mật khẩu cũ ! Cảnh báo .");
            }
            else if (txtMatKhauMoi.Text == "")
            {
                MessageBox.Show("Nhập vào mật khẩu mới");
            }
            else if (txtXacNhanMatKhau.Text == "")
            {
                MessageBox.Show("Nhập vào mật khẩu xác nhận");
            }
            else if (txtMatKhauMoi.Text != txtXacNhanMatKhau.Text)
            {
                MessageBox.Show("Mật khẩu mới không trùng với mật khẩu xác nhận");
            }
            else if (txtHoTen.Text == "")
            {
                MessageBox.Show("Họ tên không được bỏ trống");
            }
            else if (txtDiaChi.Text == "")
            {
                MessageBox.Show("Địa chỉ không được bỏ trống");
            }
            else if (txtDT.Text == "")
            {
                MessageBox.Show("SĐT không được bỏ trống");
            }
            else
            {
                accDAL.UpdateAccount(userName, fullName, newPass, Address, phone, gt);
                MessageBox.Show("Cập nhật thành công !!!");
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
