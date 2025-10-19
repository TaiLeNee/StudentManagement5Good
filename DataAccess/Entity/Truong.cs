using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement5GoodTempp.DataAccess.Entity
{
    /// <summary>
    /// Entity đại diện cho Trường trong hệ thống phân cấp quản lý
    /// </summary>
    [Table("TRUONG")]
    public class Truong
    {
        [Key]
        [Column("maTruong")]
        [MaxLength(20)]
        public string MaTruong { get; set; } = string.Empty;

        [Column("tenTruong")]
        [MaxLength(200)]
        [Required]
        public string TenTruong { get; set; } = string.Empty;

        [Column("tenVietTat")]
        [MaxLength(50)]
        public string? TenVietTat { get; set; } // Tên viết tắt của trường

        [Column("loaiTruong")]
        [MaxLength(50)]
        public string? LoaiTruong { get; set; } // Đại học, Cao đẳng, Trung cấp

        [Column("hieuTruong")]
        [MaxLength(100)]
        public string? HieuTruong { get; set; } // Tên hiệu trưởng

        [Column("biThuDoan")]
        [MaxLength(100)]
        public string? BiThuDoan { get; set; } // Bí thư đoàn trường

        [Column("soDienThoai")]
        [MaxLength(15)]
        public string? SoDienThoai { get; set; }

        [Column("email")]
        [MaxLength(100)]
        public string? Email { get; set; }

        [Column("website")]
        [MaxLength(200)]
        public string? Website { get; set; }

        [Column("diaChi")]
        [MaxLength(300)]
        public string? DiaChi { get; set; }

        [Column("maTP")]
        [MaxLength(20)]
        [Required]
        public string MaTP { get; set; } = string.Empty; // Khóa ngoại đến ThanhPho

        [Column("trangThai")]
        [Required]
        public bool TrangThai { get; set; } = true; // Active/Inactive

        [Column("ngayThanhLap")]
        public DateTime? NgayThanhLap { get; set; }

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
        /// Thành phố mà trường thuộc về
        /// </summary>
        [ForeignKey("MaTP")]
        public virtual ThanhPho ThanhPho { get; set; } = null!;

        /// <summary>
        /// Danh sách các khoa trong trường
        /// </summary>
        public virtual ICollection<Khoa> Khoas { get; set; } = new List<Khoa>();

        /// <summary>
        /// Danh sách người dùng thuộc cấp trường
        /// </summary>
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
