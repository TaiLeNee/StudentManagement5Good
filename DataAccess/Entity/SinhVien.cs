using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement5GoodTempp.DataAccess.Entity
{
    [Table("SINHVIEN")]
    public class SinhVien
    {
        [Key]
        [Column("maSV")]
        [MaxLength(20)]
        public string MaSV { get; set; } = string.Empty;

        [Column("hoTen")]
        [MaxLength(100)]
        [Required]
        public string HoTen { get; set; } = string.Empty;

        [Column("ngaySinh")]
        [Required]
        public DateTime NgaySinh { get; set; }

        [Column("gioiTinh")]
        [MaxLength(10)]
        public string? GioiTinh { get; set; }

        [Column("email")]
        [MaxLength(100)]
        public string? Email { get; set; }

        [Column("soDienThoai")]
        [MaxLength(15)]
        public string? SoDienThoai { get; set; }

        [Column("maLop")]
        [MaxLength(20)]
        [Required]
        public string MaLop { get; set; } = string.Empty;

        [ForeignKey("MaLop")]
        public virtual Lop Lop { get; set; } = null!;

        public virtual ICollection<MinhChung> MinhChungs { get; set; } = new List<MinhChung>();
        public virtual ICollection<KetQuaXetDuyet> KetQuaXetDuyets { get; set; } = new List<KetQuaXetDuyet>();
        public virtual ICollection<KetQuaDanhHieu> KetQuaDanhHieus { get; set; } = new List<KetQuaDanhHieu>();
    }
}
