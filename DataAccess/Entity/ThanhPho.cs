using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement5GoodTempp.DataAccess.Entity
{
    /// <summary>
    /// Entity đại diện cho Thành phố trong hệ thống phân cấp quản lý
    /// </summary>
    [Table("THANHPHO")]
    public class ThanhPho
    {
        [Key]
        [Column("maTP")]
        [MaxLength(20)]
        public string MaTP { get; set; } = string.Empty;

        [Column("tenThanhPho")]
        [MaxLength(100)]
        [Required]
        public string TenThanhPho { get; set; } = string.Empty;

        [Column("maVung")]
        [MaxLength(20)]
        public string? MaVung { get; set; } // Mã vùng (Bắc, Trung, Nam)

        [Column("tenVung")]
        [MaxLength(50)]
        public string? TenVung { get; set; } // Tên vùng

        [Column("chuTichDoanTP")]
        [MaxLength(100)]
        public string? ChuTichDoanTP { get; set; } // Chủ tịch đoàn thành phố

        [Column("soDienThoai")]
        [MaxLength(15)]
        public string? SoDienThoai { get; set; }

        [Column("email")]
        [MaxLength(100)]
        public string? Email { get; set; }

        [Column("diaChi")]
        [MaxLength(200)]
        public string? DiaChi { get; set; }

        [Column("trangThai")]
        [Required]
        public bool TrangThai { get; set; } = true; // Active/Inactive

        [Column("ngayTao")]
        [Required]
        public DateTime NgayTao { get; set; } = DateTime.Now;

        [Column("ngayCapNhat")]
        public DateTime? NgayCapNhat { get; set; }

        [Column("ghiChu")]
        [MaxLength(500)]
        public string? GhiChu { get; set; }

        // Navigation Properties
        /// <summary>
        /// Danh sách các trường trong thành phố
        /// </summary>
        public virtual ICollection<Truong> Truongs { get; set; } = new List<Truong>();

        /// <summary>
        /// Danh sách người dùng thuộc cấp thành phố
        /// </summary>
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
