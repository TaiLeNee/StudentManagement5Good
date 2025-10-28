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
                TrangThaiMinhChung.DaDuyet => "�? duy?t", 
                TrangThaiMinhChung.BiTuChoi => "T? ch?i",
                TrangThaiMinhChung.CanBoSung => "C?n b? sung",
                _ => "Kh�ng x�c �?nh"
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
                TrangThaiMinhChung.ChoDuyet => "Minh ch?ng �ang ch? ��?c x�t duy?t b?i c�n b? ph? tr�ch",
                TrangThaiMinhChung.DaDuyet => "Minh ch?ng �? ��?c duy?t v� ch?p nh?n",
                TrangThaiMinhChung.BiTuChoi => "Minh ch?ng b? t? ch?i, kh�ng ��p ?ng y�u c?u",
                TrangThaiMinhChung.CanBoSung => "Minh ch?ng c?n ��?c b? sung th�m th�ng tin ho?c t�i li?u",
                _ => "Tr?ng th�i kh�ng x�c �?nh"
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
                UserRoles.ADMIN => "Qu?n tr? vi�n T?i cao",
                UserRoles.GIAOVU => "Gi�o v?",
                UserRoles.CVHT => "C? v?n H?c t?p", 
                UserRoles.DOANKHOA => "BCH �o�n Khoa",
                UserRoles.DOANTRUONG => "BCH �o�n Tr�?ng",
                UserRoles.DOANTP => "BCH �o�n Th�nh ph?",
                UserRoles.DOANTU => "BCH �o�n Trung ��ng",
                UserRoles.SINHVIEN => "Sinh vi�n",
                _ => "Ng�?i d�ng"
            };
        }
    }
}