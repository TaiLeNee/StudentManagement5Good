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
    /// Thực thể KetQuaXetDuyet - Ghi nhận kết quả cuối cùng cho một tiêu chí của sinh viên ở một cấp xét cụ thể
    /// Được tạo ra sau khi các minh chứng đã được thẩm định
    /// </summary>
    [Table("KETQUAXETDUYET")]
    public class KetQuaXetDuyet
    {
        [Key]
        [Column("maKQ")]
        [MaxLength(20)]
        public string MaKQ { get; set; } = string.Empty;

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

        [Column("ketQua")]
        [Required]
        public bool KetQua { get; set; } = false;

        [Column("diem")]
        public decimal? Diem { get; set; }

        [Column("xepLoai")]
        [MaxLength(50)]
        public string? XepLoai { get; set; }

        [Column("ghiChu")]
        [MaxLength(1000)]
        public string? GhiChu { get; set; }

        [Column("ngayXetDuyet")]
        [Required]
        public DateTime NgayXetDuyet { get; set; } = DateTime.Now;

        [Column("nguoiXetDuyet")]
        [MaxLength(50)]  // Increased to match UserId length
        [Required]
        public string NguoiXetDuyet { get; set; } = string.Empty;

        [Column("soMinhChungDaDuyet")]
        public int SoMinhChungDaDuyet { get; set; } = 0;

        [Column("tongSoMinhChung")]
        public int TongSoMinhChung { get; set; } = 0;

        [Column("lyDoKhongDat")]
        [MaxLength(1000)]
        public string? LyDoKhongDat { get; set; }

        [Column("danhSachMinhChung")]
        [MaxLength(2000)]
        public string? DanhSachMinhChung { get; set; } // JSON string chứa danh sách mã minh chứng

        [Column("trangThaiHoSo")]
        [MaxLength(50)]
        public string TrangThaiHoSo { get; set; } = "HOAN_THANH"; // HOAN_THANH, CAN_BO_SUNG, KHONG_DAT

        // Navigation Properties
        [ForeignKey("MaSV")]
        public virtual SinhVien SinhVien { get; set; } = null!;

        [ForeignKey("MaTC")]
        public virtual TieuChi TieuChi { get; set; } = null!;

        [ForeignKey("MaCap")]
        public virtual CapXet CapXet { get; set; } = null!;

        [ForeignKey("MaNH")]
        public virtual NamHoc NamHoc { get; set; } = null!;

        [ForeignKey("NguoiXetDuyet")]
        public virtual User NguoiXetDuyetUser { get; set; } = null!;

        // Collection navigation properties
        public virtual ICollection<MinhChung> MinhChungs { get; set; } = new List<MinhChung>();
    }

    /// <summary>
    /// Enum định nghĩa trạng thái hồ sơ xét duyệt
    /// </summary>
    public enum TrangThaiHoSo
    {
        /// <summary>
        /// Hồ sơ đã hoàn thành và đạt yêu cầu
        /// </summary>
        HOAN_THANH,

        /// <summary>
        /// Hồ sơ cần bổ sung thêm minh chứng
        /// </summary>
        CAN_BO_SUNG,

        /// <summary>
        /// Hồ sơ không đạt yêu cầu
        /// </summary>
        KHONG_DAT
    }

    /// <summary>
    /// Extension methods cho KetQuaXetDuyet
    /// </summary>
    public static class KetQuaXetDuyetExtensions
    {
        /// <summary>
        /// Lấy tỷ lệ phần trăm hoàn thành
        /// </summary>
        public static double GetCompletionPercentage(this KetQuaXetDuyet ketQua)
        {
            if (ketQua.TongSoMinhChung == 0) return 0;
            return (double)ketQua.SoMinhChungDaDuyet / ketQua.TongSoMinhChung * 100;
        }

        /// <summary>
        /// Kiểm tra xem có đạt yêu cầu tối thiểu không
        /// </summary>
        public static bool IsMinimumRequirementMet(this KetQuaXetDuyet ketQua)
        {
            // Logic có thể thay đổi tùy theo yêu cầu cụ thể của từng tiêu chí
            return ketQua.SoMinhChungDaDuyet > 0 && ketQua.GetCompletionPercentage() >= 80;
        }

        /// <summary>
        /// Lấy màu sắc hiển thị theo kết quả
        /// </summary>
        public static System.Drawing.Color GetStatusColor(this KetQuaXetDuyet ketQua)
        {
            if (ketQua.KetQua)
                return System.Drawing.Color.FromArgb(46, 204, 113); // Green
            else if (ketQua.TrangThaiHoSo == "CAN_BO_SUNG")
                return System.Drawing.Color.FromArgb(243, 156, 18); // Orange
            else
                return System.Drawing.Color.FromArgb(231, 76, 60); // Red
        }
    }
}
