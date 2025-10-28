using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement5GoodTempp.DataAccess.Entity
{
    /// <summary>
    /// Thực thể MinhChung - Độc lập, chứa thông tin về sinh viên, tiêu chí, năm học
    /// </summary>
    [Table("MINHCHUNG")]
    public class MinhChung
    {
        [Key]
        [Column("maMC")]
        [MaxLength(20)]
        public string MaMC { get; set; } = string.Empty;

        [Column("maSV")]
        [MaxLength(20)]
        [Required]
        public string MaSV { get; set; } = string.Empty;

        [Column("maTC")]
        [MaxLength(20)]
        [Required]
        public string MaTC { get; set; } = string.Empty;

        [Column("maNH")]
        [MaxLength(20)]
        [Required]
        public string MaNH { get; set; } = string.Empty;

        [Column("tenMinhChung")]
        [MaxLength(255)]
        [Required]
        public string TenMinhChung { get; set; } = string.Empty;

        [Column("duongDanFile")]
        [MaxLength(500)]
        public string? DuongDanFile { get; set; }

        [Column("tenFile")]
        [MaxLength(255)]
        public string? TenFile { get; set; }

        [Column("loaiFile")]
        [MaxLength(10)]
        public string? LoaiFile { get; set; }

        [Column("kichThuocFile")]
        public long? KichThuocFile { get; set; }

        [Column("moTa")]
        [MaxLength(1000)]
        public string? MoTa { get; set; }

        [Column("trangThai")]
        [Required]
        public TrangThaiMinhChung TrangThai { get; set; } = TrangThaiMinhChung.ChoDuyet;

        [Column("lyDoTuChoi")]
        [MaxLength(1000)]
        public string? LyDoTuChoi { get; set; }

        [Column("ngayNop")]
        [Required]
        public DateTime NgayNop { get; set; } = DateTime.Now;

        [Column("ngayDuyet")]
        public DateTime? NgayDuyet { get; set; }

        [Column("nguoiDuyet")]
        [MaxLength(50)]  // Increased to match UserId length
        public string? NguoiDuyet { get; set; }

        [Column("ghiChu")]
        [MaxLength(1000)]
        public string? GhiChu { get; set; }

        // Navigation Properties
        [ForeignKey("MaSV")]
        public virtual SinhVien SinhVien { get; set; } = null!;

        [ForeignKey("MaTC")]
        public virtual TieuChi TieuChi { get; set; } = null!;

        [ForeignKey("MaNH")]
        public virtual NamHoc NamHoc { get; set; } = null!;

        [ForeignKey("NguoiDuyet")]
        public virtual User? NguoiDuyetUser { get; set; }
    }
}
