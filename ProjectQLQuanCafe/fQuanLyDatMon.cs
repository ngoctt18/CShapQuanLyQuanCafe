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
    public partial class fQuanLyDatMon : Form
    {
        OrderDetailDAL odtDAL = new OrderDetailDAL();
        FoodOrderDAL foDAL = new FoodOrderDAL();
        MenuDAL mDAL = new MenuDAL();
        FoodTableDAL ftDAL = new FoodTableDAL();

        private Account loginAccount;
        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount.TypeAccount); }
        }
        public fQuanLyDatMon()
        {
            InitializeComponent();

            LoadTable();
            LoadCategory();
            //LoadCmbTable();
        }

        void ChangeAccount(int typeacc)
        {
            adminToolStripMenuItem.Enabled = typeacc == 1;
            thôngTinTàiKhoảnToolStripMenuItem.Text += " (" + LoginAccount.FullName + ")";

        }
        public fQuanLyDatMon(Account acc)
        {
            //this.loginAccount = loginAccount;

            InitializeComponent();
            this.LoginAccount = acc;
            LoadTable();
            LoadCategory();
        }

        public void LoadTable()
        {
            flpTable.Controls.Clear();

            List<Table> listTable = new List<Table>();

            FoodTableDAL TblDAL = new FoodTableDAL();
            listTable = TblDAL.GetListTable();

            foreach (Table item in listTable)
            {
                Button btn = new Button() { Width = 80, Height = 80 };
                btn.Text = item.Name + "\n" + item.Status;


                btn.Click += btn_Click;
                // Lưu giá trị của table này vào thẻ Tag
                btn.Tag = item;

                
                if(item.Status == "Trống")
                {
                    btn.BackColor = Color.Blue;
                }
                else
                {
                    btn.BackColor = Color.Red;
                }
                flpTable.Controls.Add(btn);
            }  
        }
        
        void ShowOrder(int id)
        {
            lsvMonAn.Items.Clear();
            List<ProjectQLQuanCafe.BLL.Menu> listOrderDetail = mDAL.GetListMenuByTableID(id);

            float Tong = 0;
            float giam = (float)(100 - (int)(nmGiaGia.Value)) / 100;
            float TongTien = 0;
            foreach (ProjectQLQuanCafe.BLL.Menu item in listOrderDetail)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString());
                lsvItem.SubItems.Add(item.Amount.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());

                Tong += item.TotalPrice;
                
                lsvMonAn.Items.Add(lsvItem);
            }
            TongTien = Tong * giam;

            txtTongTien.Text = TongTien.ToString();
        }

        public void LoadCategory()
        {
            CategoryDAL cateDAL = new CategoryDAL();
            List<CategoryDuc> listCate = cateDAL.GetListFoodCategory();
            cmbDanhMuc.DataSource = listCate;
            cmbDanhMuc.DisplayMember = "name";

        }
        void LoadFoodListByCategoryID(int id)
        {
            FoodDAL fooDAL = new FoodDAL();
            List<FoodDuc> listFood = fooDAL.GetFoodByCategoryID(id);
            cmbTenMon.DataSource = listFood;
            cmbTenMon.DisplayMember = "name";
        }

        void LoadCmbTable(ComboBox cmb)
        {
            cmb.DataSource = ftDAL.GetListTable();
            cmb.DisplayMember = "TableName";
        }


            // --------    EVENT   -----------

            // Sự kiện click từng button Bàn
        void btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as Table).ID;

            lsvMonAn.Tag = (sender as Button).Tag;

            ShowOrder(tableID);
        }


        // KHAC
        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.ShowDialog();
        }

        private void thôngTinTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccount f = new fAccount(loginAccount);
            f.ShowDialog();
        }

        // Sự kiện Load FoodList theo CategoryID sau mỗi lần thay đổi CategoryID
        private void cmbDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cmb = sender as ComboBox;
            if (cmb.SelectedItem == null)
                return;
            CategoryDuc selected = cmb.SelectedItem as CategoryDuc;
            id = selected.ID;

            LoadFoodListByCategoryID(id);
        }
        private void fQuanLyDatMon_Load(object sender, EventArgs e)
        {
        }
        
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            // Lấy ra Table hiện tại
            Table table = lsvMonAn.Tag as Table;

            int idOrder = foDAL.GetUnCheckOrderByTableID(table.ID);

            float tongtien = float.Parse(txtTongTien.Text);

            int Discount = (int)nmGiaGia.Value;

            if (idOrder != -1)
            {
                if(MessageBox.Show(string.Format("Bạn chắc chấc thanh toán bàn {0} với giảm giá = {1}%\n Tổng tiền sẽ là: {2}", table.Name, Discount, tongtien), "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    foDAL.CheckOut(idOrder, tongtien, Discount);
                    ShowOrder(table.ID);

                    LoadTable();
                } 
            }
            else
            {
                MessageBox.Show("Bàn này đã thanh toán rồi nhé !!!");
            }
            
        }
        
        private void btnThemMon_Click(object sender, EventArgs e)
        {
            // Lấy idTable hiện tại
            Table table = lsvMonAn.Tag as Table;

            int idOrder = foDAL.GetUnCheckOrderByTableID(table.ID);
            int idFood = (cmbTenMon.SelectedItem as FoodDuc).ID;
            int soluong = (int)nmSoLuongMon.Value;
            int discount = (int)nmGiaGia.Value;
            //float tong = (float)Convert.ToDouble(txtTongTien.ToString());

            if(idOrder == -1)
            {
                foDAL.InsertOrder(table.ID, discount);
                odtDAL.InsertOD(foDAL.GetMaxIDOrder(), idFood, soluong);
            }
            else
            {
                odtDAL.InsertOD(idOrder, idFood, soluong);
            }

            ShowOrder(table.ID);

            LoadTable();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            fLogin ff = new fLogin();
            ff.resetTextboxLogin();
        }

        private void DoiMatKhau_Click(object sender, EventArgs e)
        {
            fAdmin pw = new fAdmin();
            pw.ShowDialog();
        }
    }
}
