using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudentManagement5GoodTempp.DataAccess.Context;
using StudentManagement5GoodTempp.DataAccess.Entity;
using StudentManagement5GoodTempp.Services;

namespace StudentManagement5Good.Winform
{
    /// <summary>
    /// Helper class ð? s?a l?i encoding ti?ng Vi?t
    /// </summary>
    public static class VietnameseTextHelper
    {
        public static void SetVietnameseText(Control control, string text)
        {
            if (control != null)
            {
                control.Text = text;
            }
        }

        public static void InitializeVietnameseUI(UserDashboard dashboard)
        {
            // S?a các text ti?ng Vi?t b? encode sai
            dashboard.Text = "H? th?ng Qu?n l? Sinh viên 5 T?t";
            
            // C?p nh?t các filter options
            UpdateFilterOptions(dashboard);
            
            // C?p nh?t role display names
            UpdateRoleNames(dashboard);
        }

        private static void UpdateFilterOptions(UserDashboard dashboard)
        {
            // Update status filter items
            var statusFilter = dashboard.Controls.Find("cmbStatusFilter", true).FirstOrDefault() as ComboBox;
            if (statusFilter != null)
            {
                statusFilter.Items.Clear();
                statusFilter.Items.Add("T?t c?");
                statusFilter.Items.Add("Ch? duy?t");
                statusFilter.Items.Add("Ð? duy?t");
                statusFilter.Items.Add("T? ch?i");
                statusFilter.Items.Add("C?n b? sung");
                statusFilter.SelectedIndex = 0;
            }

            // Update department filter
            var deptFilter = dashboard.Controls.Find("cmbDepartmentFilter", true).FirstOrDefault() as ComboBox;
            if (deptFilter != null)
            {
                deptFilter.Items.Clear();
                deptFilter.Items.Add("T?t c? ðõn v?");
            }

            // Update criteria filter
            var criteriaFilter = dashboard.Controls.Find("cmbCriteriaFilter", true).FirstOrDefault() as ComboBox;
            if (criteriaFilter != null)
            {
                criteriaFilter.Items.Clear();
                criteriaFilter.Items.Add("T?t c? tiêu chí");
            }
        }

        private static void UpdateRoleNames(UserDashboard dashboard)
        {
            // This will be used by the main form to update role display names
        }

        public static string GetCorrectRoleDisplayName(string role)
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

        public static string GetCorrectStatusDisplayName(TrangThaiMinhChung status)
        {
            return status switch
            {
                TrangThaiMinhChung.ChoDuyet => "Ch? duy?t",
                TrangThaiMinhChung.DaDuyet => "Ð? duy?t",
                TrangThaiMinhChung.BiTuChoi => "T? ch?i",
                TrangThaiMinhChung.CanBoSung => "C?n b? sung",
                _ => "Không xác ð?nh"
            };
        }

        public static TrangThaiMinhChung GetStatusFromFilterText(string filterText)
        {
            return filterText switch
            {
                "Ch? duy?t" => TrangThaiMinhChung.ChoDuyet,
                "Ð? duy?t" => TrangThaiMinhChung.DaDuyet,
                "T? ch?i" => TrangThaiMinhChung.BiTuChoi,
                "C?n b? sung" => TrangThaiMinhChung.CanBoSung,
                _ => TrangThaiMinhChung.ChoDuyet
            };
        }

        public static void ShowVietnameseMessage(string message, string title, MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Information)
        {
            MessageBox.Show(message, title, buttons, icon);
        }

        // Common Vietnamese messages
        public static class Messages
        {
            public const string Loading = "Ðang t?i d? li?u...";
            public const string Success = "Thành công";
            public const string Error = "L?i";
            public const string Warning = "C?nh báo";
            public const string Confirm = "Xác nh?n";
            
            public const string LoadDataError = "L?i t?i d? li?u";
            public const string SaveSuccess = "Lýu thành công";
            public const string DeleteConfirm = "B?n có ch?c ch?n mu?n xóa?";
            public const string AccessDenied = "B?n không có quy?n truy c?p ch?c nãng này";
            
            public const string SystemActive = "Ho?t ð?ng";
            public const string RefreshSuccess = "Ð? làm m?i d? li?u!";
            public const string LogoutConfirm = "B?n có ch?c ch?n mu?n ðãng xu?t?";
        }

        // Report related text
        public static class Reports
        {
            public const string StudentsWithTitle = "Danh sách sinh viên ð?t danh hi?u";
            public const string StatisticsByCriteria = "Th?ng kê theo tiêu chí";
            public const string SummaryReport = "Báo cáo t?ng h?p";
            public const string ReviewProgress = "Ti?n ð? xét duy?t";
            
            public const string SchoolLevel = "C?p Trý?ng";
            public const string FacultyLevel = "C?p Khoa";
            public const string ClassLevel = "C?p L?p";
        }
    }
}