using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement5GoodTempp.DataAccess.Entity
{
    [Table("USER")]
    public class User
    {
        [Key]
        [Column("userId")]
        [MaxLength(50)]  // Increased to support GUID (36 chars)
        public string UserId { get; set; } = string.Empty;

        [Column("username")]
        [MaxLength(50)]
        [Required]
        public string Username { get; set; } = string.Empty;

        [Column("password")]
        [MaxLength(255)]
        [Required]
        public string Password { get; set; } = string.Empty;

        [Column("hoTen")]
        [MaxLength(100)]
        [Required]
        public string HoTen { get; set; } = string.Empty;

        [Column("email")]
        [MaxLength(100)]
        public string? Email { get; set; }

        [Column("soDienThoai")]
        [MaxLength(15)]
        public string? SoDienThoai { get; set; }

        [Column("vaiTro")]
        [MaxLength(20)]
        [Required]
        public string VaiTro { get; set; } = string.Empty; // ADMIN, GIAOVU, CVHT, SINHVIEN

        [Column("capQuanLy")]
        [MaxLength(20)]
        public string? CapQuanLy { get; set; } // LOP, KHOA, TRUONG, TP, TU

        [Column("maKhoa")]
        [MaxLength(20)]
        public string? MaKhoa { get; set; } // Khoa được phân công quản lý (nếu có)

        [Column("maLop")]
        [MaxLength(20)]
        public string? MaLop { get; set; } // Lớp được phân công quản lý (nếu có)

        [Column("maTruong")]
        [MaxLength(20)]
        public string? MaTruong { get; set; } // Trường được phân công quản lý (nếu có)

        [Column("maTP")]
        [MaxLength(20)]
        public string? MaTP { get; set; } // Thành phố được phân công quản lý (nếu có)

        [Column("maSV")]
        [MaxLength(20)]
        public string? MaSV { get; set; } // Mã sinh viên (nếu user là sinh viên)

        [Column("trangThai")]
        [Required]
        public bool TrangThai { get; set; } = true; // Active/Inactive

        [Column("ngayTao")]
        [Required]
        public DateTime NgayTao { get; set; } = DateTime.Now;

        [Column("ngayCapNhat")]
        public DateTime? NgayCapNhat { get; set; }

        [Column("lanDangNhapCuoi")]
        public DateTime? LanDangNhapCuoi { get; set; }

        [Column("ghiChu")]
        [MaxLength(500)]
        public string? GhiChu { get; set; }

        // Navigation Properties
        [ForeignKey("MaKhoa")]
        public virtual Khoa? Khoa { get; set; }

        [ForeignKey("MaLop")]
        public virtual Lop? Lop { get; set; }

        [ForeignKey("MaTruong")]
        public virtual Truong? Truong { get; set; }

        [ForeignKey("MaTP")]
        public virtual ThanhPho? ThanhPho { get; set; }

        [ForeignKey("MaSV")]
        public virtual SinhVien? SinhVien { get; set; }

        [ForeignKey("CapQuanLy")]
        public virtual CapXet? CapXet { get; set; }
    }

    /// <summary>
    /// Enum định nghĩa các vai trò người dùng trong hệ thống
    /// </summary>
    public enum UserRole
    {
        ADMIN = 1,          // Quản trị hệ thống
        GIAOVU = 2,         // Giáo vụ khoa
        CVHT = 3,           // Cố vấn học tập (lớp)
        BANTHANG = 4,       // Ban thường vụ đoàn khoa
        SINHVIEN = 5,       // Sinh viên
        DOANKHOA = 6,       // Đoàn khoa
        DOANTRUONG = 7,     // Đoàn trường
        DOANTP = 8,         // Đoàn thành phố
        DOANTU = 9          // Đoàn trung ương
    }

    /// <summary>
    /// Enum định nghĩa các cấp quản lý trong hệ thống
    /// </summary>
    public enum ManagementLevel
    {
        LOP = 1,            // Cấp lớp
        KHOA = 2,           // Cấp khoa
        TRUONG = 3,         // Cấp trường
        TP = 4,             // Cấp thành phố
        TU = 5              // Cấp trung ương
    }

    /// <summary>
    /// Static class để hỗ trợ backward compatibility
    /// </summary>
    public static class UserRoles
    {
        public const string ADMIN = "ADMIN";
        public const string GIAOVU = "GIAOVU";
        public const string CVHT = "CVHT";
        public const string BANTHANG = "BANTHANG";
        public const string SINHVIEN = "SINHVIEN";
        public const string DOANKHOA = "DOANKHOA";
        public const string DOANTRUONG = "DOANTRUONG";
        public const string DOANTP = "DOANTP";
        public const string DOANTU = "DOANTU";
    }

    /// <summary>
    /// Static class để hỗ trợ backward compatibility
    /// </summary>
    public static class ManagementLevels
    {
        public const string LOP = "LOP";
        public const string KHOA = "KHOA";
        public const string TRUONG = "TRUONG";
        public const string TP = "TP";
        public const string TU = "TU";
    }
}
