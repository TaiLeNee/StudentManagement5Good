using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement5GoodTempp.DataAccess.Entity
{
    [Table("DANHGIA")]
    public class DanhGia
    {
        [Key]
        [Column("maDG")]
        [MaxLength(20)]
        public string MaDG { get; set; } = string.Empty;

        [Column("maSV")]
        [MaxLength(20)]
        [Required]
        public string MaSV { get; set; } = string.Empty;

        [Column("maTC")]
        [MaxLength(20)]
        [Required]
        public string MaTC { get; set; } = string.Empty;

        [Column("maCap")]
        [MaxLength(20)]
        [Required]
        public string MaCap { get; set; } = string.Empty;

        [Column("maNH")]
        [MaxLength(20)]
        [Required]
        public string MaNH { get; set; } = string.Empty;

        [Column("giaTri")]
        [MaxLength(255)]
        public string? GiaTri { get; set; }

        [Column("datTieuChi")]
        [Required]
        public bool DatTieuChi { get; set; } = false;

        [Column("ngayDanhGia")]
        [Required]
        public DateTime NgayDanhGia { get; set; } = DateTime.Now;

        [Column("nguoiDanhGia")]
        [MaxLength(100)]
        public string? NguoiDanhGia { get; set; }

        [ForeignKey("MaSV")]
        public virtual SinhVien SinhVien { get; set; } = null!;

        [ForeignKey("MaTC")]
        public virtual TieuChi TieuChi { get; set; } = null!;

        [ForeignKey("MaCap")]
        public virtual CapXet CapXet { get; set; } = null!;

        [ForeignKey("MaNH")]
        public virtual NamHoc NamHoc { get; set; } = null!;

        public virtual ICollection<MinhChung> MinhChungs { get; set; } = new List<MinhChung>();
    }
}
