using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement5GoodTempp.DataAccess.Entity
{
    [Table("KHOA")]
    public class Khoa
    {
        [Key]
        [Column("maKhoa")]
        [MaxLength(20)]
        public string MaKhoa { get; set; } = string.Empty;

        [Column("tenKhoa")]
        [MaxLength(100)]
        [Required]
        public string TenKhoa { get; set; } = string.Empty;

        [Column("truongKhoa")]
        [MaxLength(100)]
        public string? TruongKhoa { get; set; }

        [Column("biThuDoanKhoa")]
        [MaxLength(100)]
        public string? BiThuDoanKhoa { get; set; } // Bí thư đoàn khoa

        [Column("soDienThoai")]
        [MaxLength(15)]
        public string? SoDienThoai { get; set; }

        [Column("email")]
        [MaxLength(100)]
        public string? Email { get; set; }

        [Column("maTruong")]
        [MaxLength(20)]
        [Required]
        public string MaTruong { get; set; } = string.Empty; // Khóa ngoại đến Truong

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
        /// Trường mà khoa thuộc về
        /// </summary>
        [ForeignKey("MaTruong")]
        public virtual Truong Truong { get; set; } = null!;

        /// <summary>
        /// Danh sách các lớp trong khoa
        /// </summary>
        public virtual ICollection<Lop> Lops { get; set; } = new List<Lop>();

        /// <summary>
        /// Danh sách người dùng thuộc khoa
        /// </summary>
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}

