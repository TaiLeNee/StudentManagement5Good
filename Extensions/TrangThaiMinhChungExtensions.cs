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
                TrangThaiMinhChung.ChoDuyet => "Ch? duy?t",
                TrangThaiMinhChung.DaDuyet => "Ð? duy?t", 
                TrangThaiMinhChung.BiTuChoi => "T? ch?i",
                TrangThaiMinhChung.CanBoSung => "C?n b? sung",
                _ => "Không xác ð?nh"
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
                TrangThaiMinhChung.ChoDuyet => "?",
                TrangThaiMinhChung.DaDuyet => "?",
                TrangThaiMinhChung.BiTuChoi => "?",
                TrangThaiMinhChung.CanBoSung => "??",
                _ => "?"
            };
        }

        /// <summary>
        /// Return detailed description of status
        /// </summary>
        public static string ToDescription(this TrangThaiMinhChung trangThai)
        {
            return trangThai switch
            {
                TrangThaiMinhChung.ChoDuyet => "Minh ch?ng ðang ch? ðý?c xét duy?t b?i cán b? ph? trách",
                TrangThaiMinhChung.DaDuyet => "Minh ch?ng ð? ðý?c duy?t và ch?p nh?n",
                TrangThaiMinhChung.BiTuChoi => "Minh ch?ng b? t? ch?i, không ðáp ?ng yêu c?u",
                TrangThaiMinhChung.CanBoSung => "Minh ch?ng c?n ðý?c b? sung thêm thông tin ho?c tài li?u",
                _ => "Tr?ng thái không xác ð?nh"
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
                UserRoles.ADMIN => "Qu?n tr? viên T?i cao",
                UserRoles.GIAOVU => "Giáo v?",
                UserRoles.CVHT => "C? v?n H?c t?p", 
                UserRoles.DOANKHOA => "BCH Ðoàn Khoa",
                UserRoles.DOANTRUONG => "BCH Ðoàn Trý?ng",
                UserRoles.DOANTP => "BCH Ðoàn Thành ph?",
                UserRoles.DOANTU => "BCH Ðoàn Trung ýõng",
                UserRoles.SINHVIEN => "Sinh viên",
                _ => "Ngý?i dùng"
            };
        }
    }
}