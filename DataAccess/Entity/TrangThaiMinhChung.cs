using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement5GoodTempp.DataAccess.Entity
{
    /// <summary>
    /// Enum định nghĩa trạng thái của minh chứng trong quy trình duyệt
    /// </summary>
    public enum TrangThaiMinhChung
    {
        /// <summary>
        /// Minh chứng đã được nộp và đang chờ duyệt
        /// </summary>
        ChoDuyet = 0,

        /// <summary>
        /// Minh chứng đã được duyệt và chấp nhận
        /// </summary>
        DaDuyet = 1,

        /// <summary>
        /// Minh chứng bị từ chối
        /// </summary>
        BiTuChoi = 2,

        /// <summary>
        /// Minh chứng cần bổ sung thêm thông tin
        /// </summary>
        CanBoSung = 3
    }

    /// <summary>
    /// Extension methods cho TrangThaiMinhChung
    /// </summary>
    public static class TrangThaiMinhChungExtensions
    {
        /// <summary>
        /// Chuyển đổi enum thành chuỗi hiển thị tiếng Việt
        /// </summary>
        public static string ToDisplayString(this TrangThaiMinhChung trangThai)
        {
            return trangThai switch
            {
                TrangThaiMinhChung.ChoDuyet => "Chờ duyệt",
                TrangThaiMinhChung.DaDuyet => "Đã duyệt",
                TrangThaiMinhChung.BiTuChoi => "Bị từ chối",
                TrangThaiMinhChung.CanBoSung => "Cần bổ sung",
                _ => "Không xác định"
            };
        }

        /// <summary>
        /// Lấy màu sắc tương ứng với trạng thái
        /// </summary>
        public static System.Drawing.Color ToColor(this TrangThaiMinhChung trangThai)
        {
            return trangThai switch
            {
                TrangThaiMinhChung.ChoDuyet => System.Drawing.Color.FromArgb(243, 156, 18), // Orange
                TrangThaiMinhChung.DaDuyet => System.Drawing.Color.FromArgb(46, 204, 113), // Green
                TrangThaiMinhChung.BiTuChoi => System.Drawing.Color.FromArgb(231, 76, 60), // Red
                TrangThaiMinhChung.CanBoSung => System.Drawing.Color.FromArgb(52, 152, 219), // Blue
                _ => System.Drawing.Color.Gray
            };
        }

        /// <summary>
        /// Kiểm tra xem trạng thái có thể chỉnh sửa được không
        /// </summary>
        public static bool CanEdit(this TrangThaiMinhChung trangThai)
        {
            return trangThai == TrangThaiMinhChung.ChoDuyet || trangThai == TrangThaiMinhChung.CanBoSung;
        }
    }
}
