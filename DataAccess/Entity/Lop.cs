using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement5GoodTempp.DataAccess.Entity
{
    [Table("LOP")]
    public class Lop
    {
        [Key]
        [Column("maLop")]
        [MaxLength(20)]
        public string MaLop { get; set; } = string.Empty;

        [Column("tenLop")]
        [MaxLength(100)]
        [Required]
        public string TenLop { get; set; } = string.Empty;

        [Column("GVCN")]
        [MaxLength(100)]
        public string? GVCN { get; set; }

        [Column("maKhoa")]
        [MaxLength(20)]
        [Required]
        public string MaKhoa { get; set; } = string.Empty;

        // Navigation Properties
        [ForeignKey("MaKhoa")]
        public virtual Khoa Khoa { get; set; } = null!;

        public virtual ICollection<SinhVien> SinhViens { get; set; } = new List<SinhVien>();
    }
}

