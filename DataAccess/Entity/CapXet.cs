using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement5GoodTempp.DataAccess.Entity
{
    [Table("CAPXET")]
    public class CapXet
    {
        [Key]
        [Column("maCap")]
        [MaxLength(20)]
        public string MaCap { get; set; } = string.Empty;

        [Column("tenCap")]
        [MaxLength(100)]
        [Required]
        public string TenCap { get; set; } = string.Empty;

        // Navigation Properties
        public virtual ICollection<DanhGia> DanhGias { get; set; } = new List<DanhGia>();
        public virtual ICollection<KetQuaXetDuyet> KetQuaXetDuyets { get; set; } = new List<KetQuaXetDuyet>();
        public virtual ICollection<KetQuaDanhHieu> KetQuaDanhHieus { get; set; } = new List<KetQuaDanhHieu>();
        public virtual ICollection<TieuChiYeuCau> TieuChiYeuCaus { get; set; } = new List<TieuChiYeuCau>();
    }
}
