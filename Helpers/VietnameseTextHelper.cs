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
    /// Helper class �? s?a l?i encoding ti?ng Vi?t
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
            // S?a c�c text ti?ng Vi?t b? encode sai
            dashboard.Text = "H? th?ng Qu?n l? Sinh vi�n 5 T?t";
            
            // C?p nh?t c�c filter options
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
                statusFilter.Items.Add("�? duy?t");
                statusFilter.Items.Add("T? ch?i");
                statusFilter.Items.Add("C?n b? sung");
                statusFilter.SelectedIndex = 0;
            }

            // Update department filter
            var deptFilter = dashboard.Controls.Find("cmbDepartmentFilter", true).FirstOrDefault() as ComboBox;
            if (deptFilter != null)
            {
                deptFilter.Items.Clear();
                deptFilter.Items.Add("T?t c? ��n v?");
            }

            // Update criteria filter
            var criteriaFilter = dashboard.Controls.Find("cmbCriteriaFilter", true).FirstOrDefault() as ComboBox;
            if (criteriaFilter != null)
            {
                criteriaFilter.Items.Clear();
                criteriaFilter.Items.Add("T?t c? ti�u ch�");
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

        public static string GetCorrectStatusDisplayName(TrangThaiMinhChung status)
        {
            return status switch
            {
                TrangThaiMinhChung.ChoDuyet => "Ch? duy?t",
                TrangThaiMinhChung.DaDuyet => "�? duy?t",
                TrangThaiMinhChung.BiTuChoi => "T? ch?i",
                TrangThaiMinhChung.CanBoSung => "C?n b? sung",
                _ => "Kh�ng x�c �?nh"
            };
        }

        public static TrangThaiMinhChung GetStatusFromFilterText(string filterText)
        {
            return filterText switch
            {
                "Ch? duy?t" => TrangThaiMinhChung.ChoDuyet,
                "�? duy?t" => TrangThaiMinhChung.DaDuyet,
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
            public const string Loading = "�ang t?i d? li?u...";
            public const string Success = "Th�nh c�ng";
            public const string Error = "L?i";
            public const string Warning = "C?nh b�o";
            public const string Confirm = "X�c nh?n";
            
            public const string LoadDataError = "L?i t?i d? li?u";
            public const string SaveSuccess = "L�u th�nh c�ng";
            public const string DeleteConfirm = "B?n c� ch?c ch?n mu?n x�a?";
            public const string AccessDenied = "B?n kh�ng c� quy?n truy c?p ch?c n�ng n�y";
            
            public const string SystemActive = "Ho?t �?ng";
            public const string RefreshSuccess = "�? l�m m?i d? li?u!";
            public const string LogoutConfirm = "B?n c� ch?c ch?n mu?n ��ng xu?t?";
        }

        // Report related text
        public static class Reports
        {
            public const string StudentsWithTitle = "Danh s�ch sinh vi�n �?t danh hi?u";
            public const string StatisticsByCriteria = "Th?ng k� theo ti�u ch�";
            public const string SummaryReport = "B�o c�o t?ng h?p";
            public const string ReviewProgress = "Ti?n �? x�t duy?t";
            
            public const string SchoolLevel = "C?p Tr�?ng";
            public const string FacultyLevel = "C?p Khoa";
            public const string ClassLevel = "C?p L?p";
        }
    }
}