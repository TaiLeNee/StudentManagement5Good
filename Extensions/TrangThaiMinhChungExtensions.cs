using System.Drawing;
using StudentManagement5GoodTempp.DataAccess.Entity;

namespace StudentManagement5Good.Winform
{
    /// <summary>
    /// Extension methods for TrangThaiMinhChung enum with proper Vietnamese encoding
    /// </summary>
    public static class TrangThaiMinhChungExtensions
    {
        /// <summary>
        /// Convert status to user-friendly display string
        /// </summary>
        public static string ToDisplayString(this TrangThaiMinhChung trangThai)
        {
            return trangThai switch
            {
                TrangThaiMinhChung.ChoDuyet => "Chờ duyệt",
                TrangThaiMinhChung.DaDuyet => "Đã duyệt", 
                TrangThaiMinhChung.BiTuChoi => "Từ chối",
                TrangThaiMinhChung.CanBoSung => "Cần bổ sung",
                _ => "Không xác định"
            };
        }

        /// <summary>
        /// Return color corresponding to status
        /// </summary>
        public static Color ToColor(this TrangThaiMinhChung trangThai)
        {
            return trangThai switch
            {
                TrangThaiMinhChung.ChoDuyet => Color.Orange,
                TrangThaiMinhChung.DaDuyet => Color.Green,
                TrangThaiMinhChung.BiTuChoi => Color.Red,
                TrangThaiMinhChung.CanBoSung => Color.Blue,
                _ => Color.Gray
            };
        }

        /// <summary>
        /// Check if status can be edited
        /// </summary>
        public static bool CanEdit(this TrangThaiMinhChung trangThai)
        {
            return trangThai == TrangThaiMinhChung.ChoDuyet || 
                   trangThai == TrangThaiMinhChung.CanBoSung;
        }

        /// <summary>
        /// Check if status can be deleted
        /// </summary>
        public static bool CanDelete(this TrangThaiMinhChung trangThai)
        {
            return trangThai == TrangThaiMinhChung.ChoDuyet || 
                   trangThai == TrangThaiMinhChung.CanBoSung ||
                   trangThai == TrangThaiMinhChung.BiTuChoi;
        }

        /// <summary>
        /// Return icon/emoji corresponding to status
        /// </summary>
        public static string ToIcon(this TrangThaiMinhChung trangThai)
        {
            return trangThai switch
            {
                TrangThaiMinhChung.ChoDuyet => "⏳",
                TrangThaiMinhChung.DaDuyet => "✅",
                TrangThaiMinhChung.BiTuChoi => "❌",
                TrangThaiMinhChung.CanBoSung => "📝",
                _ => "❓"
            };
        }

        /// <summary>
        /// Return detailed description of status
        /// </summary>
        public static string ToDescription(this TrangThaiMinhChung trangThai)
        {
            return trangThai switch
            {
                TrangThaiMinhChung.ChoDuyet => "Minh chứng đang chờ được xét duyệt bởi cán bộ phụ trách",
                TrangThaiMinhChung.DaDuyet => "Minh chứng đã được duyệt và chấp nhận",
                TrangThaiMinhChung.BiTuChoi => "Minh chứng bị từ chối, không đáp ứng yêu cầu",
                TrangThaiMinhChung.CanBoSung => "Minh chứng cần được bổ sung thêm thông tin hoặc tài liệu",
                _ => "Trạng thái không xác định"
            };
        }
        
        /// <summary>
        /// Return CSS class corresponding to status (for web views)
        /// </summary>
        public static string ToCssClass(this TrangThaiMinhChung trangThai)
        {
            return trangThai switch
            {
                TrangThaiMinhChung.ChoDuyet => "status-pending",
                TrangThaiMinhChung.DaDuyet => "status-approved",
                TrangThaiMinhChung.BiTuChoi => "status-rejected",
                TrangThaiMinhChung.CanBoSung => "status-supplement",
                _ => "status-unknown"
            };
        }
    }

    /// <summary>
    /// Extension methods for User roles with proper Vietnamese
    /// </summary>
    public static class UserRoleExtensions
    {
        public static string ToVietnameseDisplayName(this string role)
        {
            return role switch
            {
                UserRoles.ADMIN => "Quản trị viên Tối cao",
                UserRoles.GIAOVU => "Giáo vụ",
                UserRoles.CVHT => "Cố vấn Học tập", 
                UserRoles.DOANKHOA => "BCH Đoàn Khoa",
                UserRoles.DOANTRUONG => "BCH Đoàn Trường",
                UserRoles.DOANTP => "BCH Đoàn Thành phố",
                UserRoles.DOANTU => "BCH Đoàn Trung ương",
                UserRoles.SINHVIEN => "Sinh viên",
                _ => "Người dùng"
            };
        }
    }
}