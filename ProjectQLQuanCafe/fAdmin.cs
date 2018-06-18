using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using ProjectQLQuanCafe.DAL;
using ProjectQLQuanCafe.BLL;

namespace ProjectQLQuanCafe
{
    public partial class fAdmin : Form
    {
        public fAdmin()
        {
            InitializeComponent();
        }
        private void fAdmin_Load(object sender, EventArgs e)
        {
            LoadAllData();
        }
        private void LoadAllData()
        {
            DoanhThuLoadDateTimePicker();
            DoanhThuLoadList(dtpNgayBD.Value, dtpNgayKT.Value);
            
            MonAnLoadList();
            MonAnLoadDanhMuc(cmbMonAnDanhMuc);

            DanhMucLoadList();

            cmbBanAnTrangThai.Items.Add("Trống");
            cmbBanAnTrangThai.Items.Add("Có người");
            BanAnLoadList();

            cmbTaiKhoanLoaiTK.Items.Add("Staff");
            cmbTaiKhoanLoaiTK.Items.Add("Admin");
            TaiKhoanLoadList();

        }

        // ---------- MonAn Load list Món ăn
        AdminMonAn monAn = new AdminMonAn();
        private void btnMonAnXem_Click(object sender, EventArgs e)
        {
            MonAnLoadList();
            MonAnLoadDanhMuc(cmbMonAnDanhMuc);
        }
        void MonAnLoadList()
        {
            DataTable dt = new DataTable();
            dt = monAn.GetListMonAn();
            dgvMonAn.DataSource = dt;
        }
        void MonAnLoadDanhMuc(ComboBox cmb)
        {
            cmb.DataSource = danhMuc.GetListdanhMuc();
            cmb.DisplayMember = "Name";
        }

        int vt = 0;
        private void dgvMonAn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            vt = e.RowIndex;
            try
            {
                if (vt >= 0)
                {
                    txtMonAnID.Text = dgvMonAn.Rows[vt].Cells[0].Value.ToString();
                    txtMonAnTenMon.Text = dgvMonAn.Rows[vt].Cells[1].Value.ToString();
                    numMonAnGia.Value = Convert.ToInt32(dgvMonAn.Rows[vt].Cells[2].Value.ToString());
                    cmbMonAnDanhMuc.Text = dgvMonAn.Rows[vt].Cells[3].Value.ToString();
                }
            }
            catch (Exception)
            {
                //MessageBox.Show("Rất tiếc. Đã sảy ra lỗi khi Click!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void ResetTextBoxMonAn()
        {
            txtMonAnTenMon.Focus();
            txtMonAnTenMon.Clear();
            numMonAnGia.Value = 0;
        }
        private void btnMonAnThem_Click(object sender, EventArgs e)
        {
            if (txtMonAnTenMon.Text == "" || cmbMonAnDanhMuc.Text == "")
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin", "Cảnh báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } else
            {
                try
                {
                    int idFoodCategory = 0;
                    DataTable dt = new DataTable();
                    dt = danhMuc.GetIDCategoryByName(cmbMonAnDanhMuc.Text);
                    
                    DataRow dr = dt.Rows[0];
                    idFoodCategory = Convert.ToInt32(dr[0].ToString());

                    monAn.insertMonAn(txtMonAnTenMon.Text, Convert.ToInt32(numMonAnGia.Text), idFoodCategory);
                    MessageBox.Show("Thêm món ăn thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    ResetTextBoxMonAn();
                    MonAnLoadList();
                }
                catch (Exception)
                {
                    MessageBox.Show("Rất tiếc. Đã sảy ra lỗi khi thêm", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnMonAnSua_Click(object sender, EventArgs e)
        {
            if (txtMonAnTenMon.Text == "" || cmbMonAnDanhMuc.Text == "")
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin", "Cảnh báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    int idFoodCategory = 0;
                    DataTable dt = new DataTable();
                    dt = danhMuc.GetIDCategoryByName(cmbMonAnDanhMuc.Text);

                    DataRow dr = dt.Rows[0];
                    idFoodCategory = Convert.ToInt32(dr[0].ToString());
                    monAn.updateMonAn(txtMonAnID.Text, txtMonAnTenMon.Text, Convert.ToInt32(numMonAnGia.Text), idFoodCategory);
                    MessageBox.Show("Sửa món ăn thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    MonAnLoadList();
                }
                catch (Exception)
                {
                    MessageBox.Show("Rất tiếc. Đã sảy ra lỗi khi sửa", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnMonAnXoa_Click(object sender, EventArgs e)
        {
            if (txtMonAnID.Text=="")
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin", "Cảnh báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    monAn.deleteMonAn(txtMonAnID.Text);
                    MessageBox.Show("Xóa món ăn thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    MonAnLoadList();
                }
                catch (Exception)
                {
                    MessageBox.Show("Rất tiếc. Đã sảy ra lỗi khi xóa", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void btnMonAnTimKiem_Click(object sender, EventArgs e)
        {
            if (txtMonAnTimKiem.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập từ khóa tìm kiếm", "Cảnh báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } else
            {
                DataTable dt = new DataTable();
                dt = monAn.searchMonAn(txtMonAnTimKiem.Text);
                dgvMonAn.DataSource = dt;
            }
        }
        // ---------- MonAn


        // ---------- DanhMuc
        AdminDanhMuc danhMuc = new AdminDanhMuc();
        private void btnDanhMucXem_Click(object sender, EventArgs e)
        {
            DanhMucLoadList();
        }
        void DanhMucLoadList()
        {
            DataTable dt = new DataTable();
            dt = danhMuc.GetListdanhMuc();
            dgvDanhMuc.DataSource = dt;
        }
        private void dgvDanhMuc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            vt = e.RowIndex;
            try
            {
                if (vt >= 0)
                {
                    txtDanhMucID.Text = dgvDanhMuc.Rows[vt].Cells[0].Value.ToString();
                    txtDanhMucTenDM.Text = dgvDanhMuc.Rows[vt].Cells[1].Value.ToString();
                }
            }
            catch (Exception){}
        }
        private void btnDanhMucThem_Click(object sender, EventArgs e)
        {
            //fQuanLyDatMon n = new fQuanLyDatMon();

            if (txtDanhMucTenDM.Text == "")
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin", "Cảnh báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    danhMuc.insertDanhMuc(txtDanhMucTenDM.Text);
                    MessageBox.Show("Thêm danh mục thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    txtDanhMucTenDM.Clear();
                    DanhMucLoadList();
                    MonAnLoadList();
                    MonAnLoadDanhMuc(cmbMonAnDanhMuc); // load lại cmbDanhMuc
                }
                catch (Exception)
                {
                    MessageBox.Show("Rất tiếc. Đã sảy ra lỗi khi thêm", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //n.LoadCategory();
        }

        private void btnDanhMucSua_Click(object sender, EventArgs e)
        {
            if (txtDanhMucTenDM.Text == "")
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin", "Cảnh báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    danhMuc.updateDanhMuc(txtDanhMucID.Text, txtDanhMucTenDM.Text);
                    MessageBox.Show("Sửa danh mục thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    DanhMucLoadList();
                    MonAnLoadList();
                    MonAnLoadDanhMuc(cmbMonAnDanhMuc); // load lại cmbDanhMuc
                }
                catch (Exception)
                {
                    MessageBox.Show("Rất tiếc. Đã sảy ra lỗi khi sửa", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDanhMucXoa_Click(object sender, EventArgs e)
        {
            if (txtDanhMucID.Text == "")
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin", "Cảnh báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    danhMuc.deleteDanhMuc(txtDanhMucID.Text);
                    MessageBox.Show("Xóa danh mục thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    DanhMucLoadList();
                    MonAnLoadList();
                    MonAnLoadDanhMuc(cmbMonAnDanhMuc); // load lại cmbDanhMuc
                }
                catch (Exception)
                {
                    MessageBox.Show("Rất tiếc. Đã sảy ra lỗi khi xóa", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        // ---------- DanhMuc



        // ---------- BanAn
        AdminBanAn banAn = new AdminBanAn();
        void BanAnLoadList()
        {
            DataTable dt = new DataTable();
            dt = banAn.GetListBanAn();
            dgvBanAn.DataSource = dt;
        }
        private void btnBanAnXem_Click(object sender, EventArgs e)
        {
            BanAnLoadList();
        }
        private void dgvBanAn_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            vt = e.RowIndex;
            try
            {
                if (vt >= 0)
                {
                    txtBanAnID.Text = dgvBanAn.Rows[vt].Cells[0].Value.ToString();
                    txtBanAnTenBan.Text = dgvBanAn.Rows[vt].Cells[1].Value.ToString();
                    cmbBanAnTrangThai.Text = dgvBanAn.Rows[vt].Cells[2].Value.ToString();
                }
            }
            catch (Exception) { }
        }
        private void btnBanAnThem_Click(object sender, EventArgs e)
        {
            if (txtBanAnTenBan.Text == "")
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin", "Cảnh báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    banAn.insertBanAn(txtBanAnTenBan.Text, cmbBanAnTrangThai.Text);
                    MessageBox.Show("Thêm bàn ăn thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    txtBanAnTenBan.Clear();
                    BanAnLoadList();
                }
                catch (Exception)
                {
                    MessageBox.Show("Rất tiếc. Đã sảy ra lỗi khi thêm", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBanAnSua_Click(object sender, EventArgs e)
        {
            if (txtBanAnTenBan.Text == "")
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin", "Cảnh báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    banAn.updateBanAn(txtBanAnID.Text, txtBanAnTenBan.Text, cmbBanAnTrangThai.Text);
                    MessageBox.Show("Sửa bàn ăn thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    BanAnLoadList();
                }
                catch (Exception)
                {
                    MessageBox.Show("Rất tiếc. Đã sảy ra lỗi khi sửa", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBanAnXoa_Click(object sender, EventArgs e)
        {
            if (txtBanAnID.Text == "")
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin", "Cảnh báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } else
            {
                try
                {
                    banAn.deleteBanAn(txtBanAnID.Text);
                    MessageBox.Show("Xóa bàn ăn thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    BanAnLoadList();
                }
                catch (Exception)
                {
                    MessageBox.Show("Rất tiếc. Đã sảy ra lỗi khi xóa", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        // ---------- BanAn


        // ---------- TaiKhoan
        AdminTaiKhoan taiKhoan = new AdminTaiKhoan();
        void TaiKhoanLoadList()
        {
            DataTable dt = new DataTable();
            dt = taiKhoan.GetListTaiKhoan();
            dgvTaiKhoan.DataSource = dt;
        }

        private void dgvTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            vt = e.RowIndex;
            try
            {
                if (vt >= 0)
                {
                    txtTaiKhoanUsername.Text = dgvTaiKhoan.Rows[vt].Cells[0].Value.ToString();
                    txtTaiKhoanHoTen.Text = dgvTaiKhoan.Rows[vt].Cells[1].Value.ToString();
                    txtTaiKhoanDiaChi.Text = dgvTaiKhoan.Rows[vt].Cells[2].Value.ToString();
                    txtTaiKhoanSDT.Text = dgvTaiKhoan.Rows[vt].Cells[3].Value.ToString();
                    if (Boolean.Parse(dgvTaiKhoan.Rows[vt].Cells[4].Value.ToString()) == true)
                        rdoTaiKhoanNam.Checked = true;
                    else rdoTaiKhoanNu.Checked = true;
                    if (Convert.ToInt32(dgvTaiKhoan.Rows[vt].Cells[5].Value.ToString()) == 1)
                        cmbTaiKhoanLoaiTK.Text = "Admin";
                    else cmbTaiKhoanLoaiTK.Text = "Staff";
                }
            }
            catch (Exception)
            {
                //MessageBox.Show("Rất tiếc. Đã sảy ra lỗi khi Click!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void ResetTextBoxTaiKhoan()
        {
            txtTaiKhoanHoTen.Focus();
            txtTaiKhoanHoTen.Clear();
            txtTaiKhoanDiaChi.Clear();
            txtTaiKhoanSDT.Clear();
            cmbTaiKhoanLoaiTK.Text = "Staff";
        }

        private void btnTaiKhoanXem_Click(object sender, EventArgs e)
        {
            TaiKhoanLoadList();
        }

        private void btnTaiKhoanThem_Click(object sender, EventArgs e)
        {
            if (txtTaiKhoanUsername.Text==""|| txtTaiKhoanHoTen.Text==""||txtTaiKhoanDiaChi.Text==""||txtTaiKhoanSDT.Text=="")
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin", "Cảnh báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } else
            {
                try
                {
                    Boolean gt;
                    if (rdoTaiKhoanNam.Checked)
                        gt = true;
                    else gt = false;
                    int type = 0;
                    if (cmbTaiKhoanLoaiTK.Text == "Admin")
                        type = 1;
                    taiKhoan.insertTaiKhoan(txtTaiKhoanUsername.Text, txtTaiKhoanHoTen.Text, txtTaiKhoanDiaChi.Text, Convert.ToInt32(txtTaiKhoanSDT.Text), gt, type);
                    MessageBox.Show("Thêm tài khoản thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    ResetTextBoxTaiKhoan();
                    TaiKhoanLoadList();
                }
                catch (Exception)
                {
                    MessageBox.Show("Rất tiếc. Đã sảy ra lỗi khi thêm", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTaiKhoanSua_Click(object sender, EventArgs e)
        {
            if (txtTaiKhoanUsername.Text == "")
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin", "Cảnh báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    Boolean gt;
                    if (rdoTaiKhoanNam.Checked)
                        gt = true;
                    else gt = false;
                    int type = 0;
                    if (cmbTaiKhoanLoaiTK.Text == "Admin")
                        type = 1;
                    taiKhoan.updateTaiKhoan(txtTaiKhoanUsername.Text, txtTaiKhoanHoTen.Text, txtTaiKhoanDiaChi.Text, Convert.ToInt32(txtTaiKhoanSDT.Text), gt, type);
                    MessageBox.Show("Sửa tài khoản thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    TaiKhoanLoadList();
                }
                catch (Exception)
                {
                    MessageBox.Show("Rất tiếc. Đã sảy ra lỗi khi sửa", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTaiKhoanXoa_Click(object sender, EventArgs e)
        {
            if (txtTaiKhoanUsername.Text == "")
            {
                MessageBox.Show("Hãy nhập đầy đủ thông tin", "Cảnh báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    taiKhoan.deleteTaiKhoan(txtTaiKhoanUsername.Text);
                    MessageBox.Show("Xóa tài khoản thành công", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    TaiKhoanLoadList();
                }
                catch (Exception)
                {
                    MessageBox.Show("Rất tiếc. Đã sảy ra lỗi khi xóa", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } 
        }

        // ---------- TaiKhoan



        // ---------- DoanhThu
        // Load ra list doanh thu theo tháng
        ThongKeDoanhThu thongKe = new ThongKeDoanhThu();
        void DoanhThuLoadList(DateTime TimeCheckIn, DateTime TimeCheckOut)
        {
            DataTable dt = new DataTable();
            dt = thongKe.GetListDoanhThu(TimeCheckIn, TimeCheckOut);
            dgvDoanhThu.DataSource = dt;
        }
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            try
            {
                DoanhThuLoadList(dtpNgayBD.Value, dtpNgayKT.Value);
            }
            catch (Exception)
            {
                MessageBox.Show("Rất tiếc. Đã sảy ra lỗi gì đó!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        void DoanhThuLoadDateTimePicker()
        {
            // Set dateTimePicker từ đầu tháng đến cuối tháng
            DateTime today = DateTime.Now;
            dtpNgayBD.Value = new DateTime(today.Year, today.Month, 1);
            //set năm, tháng hiện tại, ngày = 1
            dtpNgayKT.Value = dtpNgayBD.Value.AddMonths(1).AddDays(-1);
            // get tháng hiện tại + 1, set ngày - 1 ngày
        }
        // ---------- DoanhThu

        
        private void fAdmin_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Đức thêm đoạn code load lại bàn và các món ăn khi tắt form Admin 
            fQuanLyDatMon f = new fQuanLyDatMon();
            f.LoadTable();
        }




    }
}
