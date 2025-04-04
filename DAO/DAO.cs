using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DTO;
namespace DAO
{
    public class NhanVienDAO
    {
        // Chuỗi kết nối tới SQL Server - điều chỉnh cho phù hợp với máy của bạn.
        private string connectionString =
            "Data Source=.;Initial Catalog=QuanLyBanDoAnNhanh;Integrated Security=True;Encrypt=False";

        public List<NhanVienDTO> GetAllNhanVien()
        {
            List<NhanVienDTO> dsNhanVien = new List<NhanVienDTO>();

            // Câu lệnh SQL lấy tất cả dữ liệu
            string query = "SELECT * FROM NhanVien";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            NhanVienDTO nv = new NhanVienDTO
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                MaNV = reader["MaNV"].ToString(),
                                HoTen = reader["HoTen"].ToString(),
                                GioiTinh = reader["GioiTinh"].ToString(),
                                NgaySinh = Convert.ToDateTime(reader["NgaySinh"]),
                                SoDienThoai = reader["SoDienThoai"].ToString(),
                                Email = reader["Email"].ToString(),
                                CCCD = reader["CCCD"].ToString(),
                                ChucVu = reader["ChucVu"].ToString(),
                                NgayBatDauLam = Convert.ToDateTime(reader["NgayBatDauLam"]),
                                TrangThai = reader["TrangThai"].ToString()
                            };
                            dsNhanVien.Add(nv);
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        // Xử lý ngoại lệ nếu cần
                        throw new Exception("Lỗi khi lấy dữ liệu từ bảng NhanVien", ex);
                    }
                }
            }
            return dsNhanVien;
        }
        public List<NhanVienDTO> GetAllHoTenAndChucVuOrderedByChucVu()
        {
            List<NhanVienDTO> dsNhanVien = new List<NhanVienDTO>();
            string query = @"USE QuanLyBanDoAnNhanh;
                           SELECT a.HoTen, a.ChucVu, a.ID, a.MaNV, a.GioiTinh, a.NgaySinh, a.SoDienThoai, a.Email, a.CCCD, a.NgayBatDauLam, a.TrangThai
                           FROM NhanVien a
                           ORDER BY a.ChucVu;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            NhanVienDTO nv = new NhanVienDTO
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                MaNV = reader["MaNV"].ToString(),
                                HoTen = reader["HoTen"].ToString(),
                                GioiTinh = reader["GioiTinh"].ToString(),
                                NgaySinh = Convert.ToDateTime(reader["NgaySinh"]),
                                SoDienThoai = reader["SoDienThoai"].ToString(),
                                Email = reader["Email"].ToString(),
                                CCCD = reader["CCCD"].ToString(),
                                ChucVu = reader["ChucVu"].ToString(),
                                NgayBatDauLam = Convert.ToDateTime(reader["NgayBatDauLam"]),
                                TrangThai = reader["TrangThai"].ToString()
                            };
                            dsNhanVien.Add(nv);
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Lỗi khi lấy dữ liệu Họ Tên và Chức Vụ", ex);
                    }
                }
            }
            return dsNhanVien;

        }
        public List<NhanVienDTO> SearchNhanVien(string keyword)
        {
            List<NhanVienDTO> dsNhanVien = new List<NhanVienDTO>();
            string query = @"SELECT ID, MaNV, HoTen, GioiTinh, NgaySinh, SoDienThoai, Email, CCCD, ChucVu, NgayBatDauLam, TrangThai
                           FROM NhanVien
                           WHERE MaNV LIKE @Keyword OR
                                 HoTen LIKE @Keyword OR
                                 GioiTinh LIKE @Keyword OR
                                 SoDienThoai LIKE @Keyword OR
                                 Email LIKE @Keyword OR
                                 CCCD LIKE @Keyword OR
                                 ChucVu LIKE @Keyword OR
                                 TrangThai LIKE @Keyword
                           ORDER BY ChucVu;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                    try
                    {
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            NhanVienDTO nv = new NhanVienDTO
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                MaNV = reader["MaNV"].ToString(),
                                HoTen = reader["HoTen"].ToString(),
                                GioiTinh = reader["GioiTinh"].ToString(),
                                NgaySinh = Convert.ToDateTime(reader["NgaySinh"]),
                                SoDienThoai = reader["SoDienThoai"].ToString(),
                                Email = reader["Email"].ToString(),
                                CCCD = reader["CCCD"].ToString(),
                                ChucVu = reader["ChucVu"].ToString(),
                                NgayBatDauLam = Convert.ToDateTime(reader["NgayBatDauLam"]),
                                TrangThai = reader["TrangThai"].ToString()
                            };
                            dsNhanVien.Add(nv);
                        }
                        reader.Close();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Lỗi khi tìm kiếm nhân viên", ex);
                    }
                }
            }
            return dsNhanVien;
        }

            public bool DeleteLichLamViecByMaNV(string maNV)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = "DELETE FROM LichLamViec WHERE MaNV = @MaNV";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@MaNV", maNV);
                            int rowsAffected = command.ExecuteNonQuery();
                            return rowsAffected > 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi khi xóa lịch làm việc của nhân viên (MaNV = {maNV}): {ex.Message}");
                        return false;
                    }
                }
            }

            // Phương thức xóa bản ghi liên quan từ bảng HoaDonKhachHang
            public bool DeleteHoaDonKhachHangByMaNV(string maNV)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = "DELETE FROM HoaDonKhachHang WHERE MaNVThanhToan = @MaNV";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@MaNV", maNV);
                            int rowsAffected = command.ExecuteNonQuery();
                            return rowsAffected > 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi khi xóa hóa đơn khách hàng liên quan đến nhân viên (MaNV = {maNV}): {ex.Message}");
                        return false;
                    }
                }
            }

            // Phương thức xóa bản ghi liên quan từ bảng DatHangNhapVe
            public bool DeleteDatHangNhapVeByMaNV(string maNV)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = "DELETE FROM DatHangNhapVe WHERE MaNVThanhToan = @MaNV";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@MaNV", maNV);
                            int rowsAffected = command.ExecuteNonQuery();
                            return rowsAffected > 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi khi xóa đơn đặt hàng nhập về liên quan đến nhân viên (MaNV = {maNV}): {ex.Message}");
                        return false;
                    }
                }
            }

            // Phương thức xóa bản ghi liên quan từ bảng DatMon
            public bool DeleteDatMonByMaNV(string maNV)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = "DELETE FROM DatMon WHERE MaNVPhuTrach = @MaNV";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@MaNV", maNV);
                            int rowsAffected = command.ExecuteNonQuery();
                            return rowsAffected > 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi khi xóa món ăn đã đặt liên quan đến nhân viên (MaNV = {maNV}): {ex.Message}");
                        return false;
                    }
                }
            }

            // Phương thức xóa bản ghi liên quan từ bảng DatBan
            public bool DeleteDatBanByMaNV(string maNV)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = "DELETE FROM DatBan WHERE MaNVThucHien = @MaNV";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@MaNV", maNV);
                            int rowsAffected = command.ExecuteNonQuery();
                            return rowsAffected > 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi khi xóa đặt bàn liên quan đến nhân viên (MaNV = {maNV}): {ex.Message}");
                        return false;
                    }
                }
            }

            // Phương thức xóa bản ghi liên quan từ bảng LuongNhanVien (cho cột MaNV)
            public bool DeleteLuongNhanVienByMaNV(string maNV)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = "DELETE FROM LuongNhanVien WHERE MaNV = @MaNV OR NguoiLap = @MaNV";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@MaNV", maNV);
                            int rowsAffected = command.ExecuteNonQuery();
                            return rowsAffected > 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi khi xóa thông tin lương của nhân viên (MaNV = {maNV}): {ex.Message}");
                        return false;
                    }
                }
            }

            // Phương thức xóa nhân viên theo ID (đã có)
            public bool Delete(int id)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = "DELETE FROM NhanVien WHERE ID = @ID";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@ID", id);
                            int rowsAffected = command.ExecuteNonQuery();
                            return rowsAffected > 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi khi xóa nhân viên theo ID: {ex.Message}");
                        return false;
                    }
                }
            }
    }
}



