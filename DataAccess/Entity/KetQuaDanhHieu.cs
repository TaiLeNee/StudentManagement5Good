using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement5GoodTempp.DataAccess.Entity
{
    [Table("KETQUADANHHIEU")]
    public class KetQuaDanhHieu
    {
        [Key]
        [Column("maKQ")]
        [MaxLength(20)]
        public string MaKQ { get; set; } = string.Empty;

        [Column("maSV")]
        [MaxLength(20)]
        [Required]
        public string MaSV { get; set; } = string.Empty;

        [Column("maCap")]
        [MaxLength(20)]
        [Required]
        public string MaCap { get; set; } = string.Empty;

        [Column("maNH")]
        [MaxLength(20)]
        [Required]
        public string MaNH { get; set; } = string.Empty;

        [Column("datDanhHieu")]
        [Required]
        public bool DatDanhHieu { get; set; } = false;

        [Column("ngayDat")]
        public DateTime? NgayDat { get; set; }

        [Column("ghiChu")]
        [MaxLength(500)]
        public string? GhiChu { get; set; }

        [ForeignKey("MaSV")]
        public virtual SinhVien SinhVien { get; set; } = null!;

        [ForeignKey("MaCap")]
        public virtual CapXet CapXet { get; set; } = null!;

        [ForeignKey("MaNH")]
        public virtual NamHoc NamHoc { get; set; } = null!;
    }
}
