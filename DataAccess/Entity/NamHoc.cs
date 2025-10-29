using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement5GoodTempp.DataAccess.Entity
{
    [Table("NAMHOC")]
    public class NamHoc
    {
        [Key]
        [Column("maNH")]
        [MaxLength(20)]
        public string MaNH { get; set; } = string.Empty;

        [Column("tenNamHoc")]
        [MaxLength(20)]
        [Required]
        public string TenNamHoc { get; set; } = string.Empty;

        [Column("tuNgay")]
        [Required]
        public DateTime TuNgay { get; set; }

        [Column("denNgay")]
        [Required]
        public DateTime DenNgay { get; set; }

        // Navigation Properties
        public virtual ICollection<MinhChung> MinhChungs { get; set; } = new List<MinhChung>();
        public virtual ICollection<KetQuaXetDuyet> KetQuaXetDuyets { get; set; } = new List<KetQuaXetDuyet>();
        public virtual ICollection<KetQuaDanhHieu> KetQuaDanhHieus { get; set; } = new List<KetQuaDanhHieu>();
    }
}
