using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement5GoodTempp.DataAccess.Entity
{
    [Table("TIEUCHIYEUCAU")]
    public class TieuChiYeuCau
    {
        [Key, Column("maTc",Order = 0)]
        [MaxLength(20)]
        public string MaTC { get; set; } = string.Empty;

        [Key, Column("maCap",Order = 1)]
        [MaxLength(20)]
        public string MaCap { get; set; } = string.Empty;

        [Column("nguongDat")]
        public float? NguongDat { get; set; }

        [Column("batBuoc")]
        [Required]
        public bool BatBuoc { get; set; } = false;

        [Column("moTaYeuCau")]
        [MaxLength(500)]
        public string? MoTaYeuCau { get; set; }

        // Navigation Properties
        [ForeignKey("MaTC")]
        public virtual TieuChi TieuChi { get; set; } = null!;

        [ForeignKey("MaCap")]
        public virtual CapXet CapXet { get; set; } = null!;
    }
}
