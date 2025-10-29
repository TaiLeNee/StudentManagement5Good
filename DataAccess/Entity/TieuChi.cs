using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement5GoodTempp.DataAccess.Entity
{
    [Table("TIEUCHI")]
    public class TieuChi
    {
        [Key]
        [Column("maTC")]
        [MaxLength(20)]
        public string MaTC { get; set; } = string.Empty;

        [Column("tenTieuChi")]
        [MaxLength(100)]
        [Required]
        public string TenTieuChi { get; set; } = string.Empty;

        [Column("moTa")]
        [MaxLength(500)]
        public string? MoTa { get; set; }

        [Column("loaiDuLieu")]
        [MaxLength(20)]
        public string? LoaiDuLieu { get; set; }

        [Column("donViTinh")]
        [MaxLength(50)]
        public string? DonViTinh { get; set; }

        // Navigation Properties
        public virtual ICollection<MinhChung> MinhChungs { get; set; } = new List<MinhChung>();
        public virtual ICollection<KetQuaXetDuyet> KetQuaXetDuyets { get; set; } = new List<KetQuaXetDuyet>();
        public virtual ICollection<TieuChiYeuCau> TieuChiYeuCaus { get; set; } = new List<TieuChiYeuCau>();
    }
}
