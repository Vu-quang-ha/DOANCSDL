using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DTO;


namespace BUS
{
    public class NhanVienBUS
    {
        private NhanVienDAO nhanVienDAO;

        public NhanVienBUS()
        {
            nhanVienDAO = new NhanVienDAO();
        }

        // Lấy tất cả nhân viên từ DAO
        public List<NhanVienDTO> GetAllNhanVien()
        {
            return nhanVienDAO.GetAllNhanVien();
        }

        // Ví dụ thêm các hàm lọc, tìm kiếm nếu cần
        public List<NhanVienDTO> GetNhanVienByGioiTinh(string gioiTinh)
        {
            return nhanVienDAO.GetAllNhanVien()
                              .Where(nv => nv.GioiTinh.ToLower() == gioiTinh.ToLower())
                              .ToList();
        }
        public List<NhanVienDTO> GetAllHoTenAndChucVuOrderedByChucVu()
        {
            return nhanVienDAO.GetAllHoTenAndChucVuOrderedByChucVu();
        }
        public List<NhanVienDTO> SearchNhanVien(string keyword)
        {
            return nhanVienDAO.SearchNhanVien(keyword);
        }
        public bool XoaNhanVienToanBo(int id, string maNV)
        {
            // Xóa các bản ghi liên quan
            bool lichLamViecDeleted = nhanVienDAO.DeleteLichLamViecByMaNV(maNV);
            bool hoaDonDeleted = nhanVienDAO.DeleteHoaDonKhachHangByMaNV(maNV);
            bool datHangDeleted = nhanVienDAO.DeleteDatHangNhapVeByMaNV(maNV);
            bool datMonDeleted = nhanVienDAO.DeleteDatMonByMaNV(maNV);
            bool datBanDeleted = nhanVienDAO.DeleteDatBanByMaNV(maNV);
            bool luongDeleted = nhanVienDAO.DeleteLuongNhanVienByMaNV(maNV);

            // Sau khi xóa các bản ghi liên quan, tiến hành xóa nhân viên chính
            bool nhanVienDeleted = nhanVienDAO.Delete(id);

            // Trả về true nếu xóa thành công tất cả (hoặc bạn có thể tùy chỉnh logic trả về)
            return nhanVienDeleted;
        }

        // Phương thức xóa nhân viên (chỉ xóa từ bảng NhanVien)
        public bool XoaNhanVien(int id)
        {
            return nhanVienDAO.Delete(id);
        }
    }

}
