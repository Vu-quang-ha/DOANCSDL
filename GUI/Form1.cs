
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DTO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GUI
{

    public partial class Form1_QLNV : Form
    {
        private NhanVienBUS nhanVienBUS = new NhanVienBUS();
        public Form1_QLNV()
        {
            InitializeComponent();
        }

        private void textBox1_VuQuangHa_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_INNVNAM_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = nhanVienBUS.GetNhanVienByGioiTinh("Nam");
        }

        private void Form1_QLNV_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = nhanVienBUS.GetAllNhanVien();

        }

        private void button4_INTHANHVIEN_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = nhanVienBUS.GetAllNhanVien();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button3_INNVNU_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = nhanVienBUS.GetNhanVienByGioiTinh("Nữ");
        }

        private void button1_LOCNV_Click(object sender, EventArgs e)
        {
            List<NhanVienDTO> danhSachNhanVien = nhanVienBUS.GetAllHoTenAndChucVuOrderedByChucVu();
            var danhSachHienThi = danhSachNhanVien.Select(nv => new { HoTen = nv.HoTen, ChucVu = nv.ChucVu }).ToList();
            dataGridView1.DataSource = danhSachHienThi;
        }

        private void button1_CALAM_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string keyword = textBox1_TIM.Text.Trim();
            if (!string.IsNullOrEmpty(keyword))
            {
                List<NhanVienDTO> danhSachTimKiem = nhanVienBUS.SearchNhanVien(keyword);
                dataGridView1.DataSource = danhSachTimKiem;
            }
            else
            {

                dataGridView1.DataSource = nhanVienBUS.GetAllNhanVien();
            }
        }

        private void button7_XOA_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

               
                if (selectedRow.Cells["ID"] != null && selectedRow.Cells["ID"].Value != null &&
                    int.TryParse(selectedRow.Cells["ID"].Value.ToString(), out int idCanXoa))
                {
                   
                    if (selectedRow.Cells["MaNV"] != null && selectedRow.Cells["MaNV"].Value != null)
                    {
                        string maNVCanXoa = selectedRow.Cells["MaNV"].Value.ToString();

                        NhanVienBUS nhanVienBUS = new NhanVienBUS();

                        if (nhanVienBUS.XoaNhanVienToanBo(idCanXoa, maNVCanXoa))
                        {
                            MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dataGridView1.DataSource = nhanVienBUS.GetAllNhanVien();
                        }
                        else
                        {
                            MessageBox.Show("Không thể xóa nhân viên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy Mã nhân viên trong dòng đã chọn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy hoặc ID nhân viên không hợp lệ trong dòng đã chọn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button6_LUU_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Lưu thành công! ");
        }
    }
    
}
    

