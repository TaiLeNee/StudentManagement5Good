using System;
using System.Linq;
using System.Windows.Forms;
using StudentManagement5GoodTempp.DataAccess.Entity;

namespace StudentManagement5Good.Winform
{
    /// <summary>
    /// Helper class ð? patch l?i encoding ti?ng Vi?t trong UserDashboard
    /// </summary>
    public static class UserDashboardVietnamesePatch
    {
        /// <summary>
        /// Áp d?ng patch ti?ng Vi?t cho UserDashboard sau khi form ð? load
        /// </summary>
        public static void ApplyVietnamesePatch(UserDashboard dashboard)
        {
            try
            {
                // Fix form title
                dashboard.Text = "H? th?ng Qu?n l? Sinh viên 5 T?t";

                // Fix filter ComboBox items
                FixStatusFilterItems(dashboard);
                FixDepartmentFilterItems(dashboard);
                FixCriteriaFilterItems(dashboard);
                FixReportFilterItems(dashboard);

                // Fix button texts and labels
                FixButtonTexts(dashboard);
                FixStatusLabels(dashboard);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error applying Vietnamese patch: {ex.Message}");
            }
        }

        private static void FixStatusFilterItems(UserDashboard dashboard)
        {
            var statusFilter = FindControl<ComboBox>(dashboard, "cmbStatusFilter");
            if (statusFilter != null)
            {
                var selectedIndex = statusFilter.SelectedIndex;
                statusFilter.Items.Clear();
                statusFilter.Items.Add("T?t c?");
                statusFilter.Items.Add("Ch? duy?t");
                statusFilter.Items.Add("Ð? duy?t");
                statusFilter.Items.Add("T? ch?i");
                statusFilter.Items.Add("C?n b? sung");
                
                if (selectedIndex >= 0 && selectedIndex < statusFilter.Items.Count)
                    statusFilter.SelectedIndex = selectedIndex;
                else
                    statusFilter.SelectedIndex = 0;
            }
        }

        private static void FixDepartmentFilterItems(UserDashboard dashboard)
        {
            var deptFilter = FindControl<ComboBox>(dashboard, "cmbDepartmentFilter");
            if (deptFilter != null && deptFilter.Items.Count > 0)
            {
                deptFilter.Items[0] = "T?t c? ðõn v?";
            }
        }

        private static void FixCriteriaFilterItems(UserDashboard dashboard)
        {
            var criteriaFilter = FindControl<ComboBox>(dashboard, "cmbCriteriaFilter");
            if (criteriaFilter != null && criteriaFilter.Items.Count > 0)
            {
                criteriaFilter.Items[0] = "T?t c? tiêu chí";
            }
        }

        private static void FixReportFilterItems(UserDashboard dashboard)
        {
            var reportTypeFilter = FindControl<ComboBox>(dashboard, "cmbReportType");
            if (reportTypeFilter != null)
            {
                var selectedIndex = reportTypeFilter.SelectedIndex;
                reportTypeFilter.Items.Clear();
                reportTypeFilter.Items.Add("Danh sách sinh viên ð?t danh hi?u");
                reportTypeFilter.Items.Add("Th?ng kê theo tiêu chí");
                reportTypeFilter.Items.Add("Báo cáo t?ng h?p");
                reportTypeFilter.Items.Add("Ti?n ð? xét duy?t");
                
                if (selectedIndex >= 0 && selectedIndex < reportTypeFilter.Items.Count)
                    reportTypeFilter.SelectedIndex = selectedIndex;
                else
                    reportTypeFilter.SelectedIndex = 0;
            }

            var reportLevelFilter = FindControl<ComboBox>(dashboard, "cmbReportLevel");
            if (reportLevelFilter != null)
            {
                var selectedIndex = reportLevelFilter.SelectedIndex;
                reportLevelFilter.Items.Clear();
                reportLevelFilter.Items.Add("C?p Trý?ng");
                reportLevelFilter.Items.Add("C?p Khoa");
                reportLevelFilter.Items.Add("C?p L?p");
                
                if (selectedIndex >= 0 && selectedIndex < reportLevelFilter.Items.Count)
                    reportLevelFilter.SelectedIndex = selectedIndex;
                else
                    reportLevelFilter.SelectedIndex = 0;
            }
        }

        private static void FixButtonTexts(UserDashboard dashboard)
        {
            // Fix common button texts if needed
            var refreshBtn = FindControl<Button>(dashboard, "btnRefresh");
            if (refreshBtn != null && refreshBtn.Text.Contains("L?m"))
            {
                refreshBtn.Text = "Làm m?i";
            }

            var logoutBtn = FindControl<Button>(dashboard, "btnLogout");
            if (logoutBtn != null && logoutBtn.Text.Contains("??ng"))
            {
                logoutBtn.Text = "Ðãng xu?t";
            }
        }

        private static void FixStatusLabels(UserDashboard dashboard)
        {
            var systemStatusLabel = FindControl<Label>(dashboard, "lblSystemStatusInfo");
            if (systemStatusLabel != null && systemStatusLabel.Text.Contains("Ho?t"))
            {
                systemStatusLabel.Text = "Ho?t ð?ng";
            }
        }

        /// <summary>
        /// T?m control theo tên trong form và t?t c? containers con
        /// </summary>
        private static T FindControl<T>(Control parent, string name) where T : Control
        {
            var controls = parent.Controls.Find(name, true);
            return controls.FirstOrDefault() as T;
        }

        /// <summary>
        /// S?a text c?a DataGridView headers và cells
        /// </summary>
        public static void FixDataGridViewTexts(DataGridView dgv)
        {
            if (dgv == null) return;

            try
            {
                // Fix column headers
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (col.HeaderText.Contains("Vai tr"))
                        col.HeaderText = "Vai tr?";
                    else if (col.HeaderText.Contains("Tr?ng"))
                        col.HeaderText = "Tr?ng thái";
                    else if (col.HeaderText.Contains("??n"))
                        col.HeaderText = "Ðõn v?";
                }

                // Fix cell values for role and status columns
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value?.ToString().Contains("Ho?t") == true)
                        {
                            cell.Value = "Ho?t ð?ng";
                        }
                        else if (cell.Value?.ToString().Contains("Ch?") == true)
                        {
                            cell.Value = "Ch? duy?t";
                        }
                        else if (cell.Value?.ToString().Contains("??") == true)
                        {
                            cell.Value = "Ð? duy?t";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fixing DataGridView texts: {ex.Message}");
            }
        }

        /// <summary>
        /// Tr? v? status text ðúng t? enum
        /// </summary>
        public static string GetCorrectStatusText(TrangThaiMinhChung status)
        {
            return status.ToDisplayString();
        }

        /// <summary>
        /// Tr? v? role text ðúng t? role string
        /// </summary>
        public static string GetCorrectRoleText(string role)
        {
            return role.ToVietnameseDisplayName();
        }
    }
}