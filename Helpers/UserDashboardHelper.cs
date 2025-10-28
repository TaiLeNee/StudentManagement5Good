using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudentManagement5GoodTempp.DataAccess.Context;
using StudentManagement5GoodTempp.DataAccess.Entity;

namespace StudentManagement5Good.Winform
{
    /// <summary>
    /// Helper class ð? x? l? UserDashboard thread-safe và fix ti?ng Vi?t
    /// </summary>
    public static class UserDashboardHelper
    {
        /// <summary>
        /// Update dashboard statistics thread-safe
        /// </summary>
        public static async Task UpdateDashboardStatsAsync(IServiceProvider serviceProvider, 
            Label lblPendingCount, Label lblProcessedCount, Label lblSystemStatusInfo, string currentNamHoc)
        {
            try
            {
                await Task.Run(async () =>
                {
                    using var scope = serviceProvider.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                    // Load data
                    var pendingCount = await context.MinhChungs
                        .AsNoTracking()
                        .Where(mc => mc.TrangThai == TrangThaiMinhChung.ChoDuyet)
                        .CountAsync();

                    var processedCount = await context.MinhChungs
                        .AsNoTracking()
                        .Where(mc => mc.TrangThai == TrangThaiMinhChung.DaDuyet || 
                                    mc.TrangThai == TrangThaiMinhChung.BiTuChoi)
                        .CountAsync();

                    // Update UI thread-safe
                    ThreadSafeUIHelper.SetText(lblPendingCount, pendingCount.ToString());
                    ThreadSafeUIHelper.SetText(lblProcessedCount, processedCount.ToString());
                    ThreadSafeUIHelper.SetText(lblSystemStatusInfo, "Ho?t ð?ng");
                    ThreadSafeUIHelper.SetForeColor(lblSystemStatusInfo, Color.White);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating dashboard stats: {ex.Message}");
            }
        }

        /// <summary>
        /// Initialize filters v?i ti?ng Vi?t ðúng
        /// </summary>
        public static void InitializeVietnameseFilters(ComboBox cmbStatusFilter, ComboBox cmbDepartmentFilter, 
            ComboBox cmbCriteriaFilter, ComboBox cmbReportType, ComboBox cmbReportLevel)
        {
            try
            {
                // Status filter
                if (cmbStatusFilter != null)
                {
                    ThreadSafeUIHelper.UpdateComboBoxItems(cmbStatusFilter, new[]
                    {
                        "T?t c?",
                        "Ch? duy?t", 
                        "Ð? duy?t",
                        "T? ch?i",
                        "C?n b? sung"
                    }, 0);
                }

                // Department filter
                if (cmbDepartmentFilter != null)
                {
                    ThreadSafeUIHelper.UpdateComboBoxItems(cmbDepartmentFilter, new[]
                    {
                        "T?t c? ðõn v?"
                    }, 0);
                }

                // Criteria filter
                if (cmbCriteriaFilter != null)
                {
                    ThreadSafeUIHelper.UpdateComboBoxItems(cmbCriteriaFilter, new[]
                    {
                        "T?t c? tiêu chí"
                    }, 0);
                }

                // Report type filter
                if (cmbReportType != null)
                {
                    ThreadSafeUIHelper.UpdateComboBoxItems(cmbReportType, new[]
                    {
                        "Danh sách sinh viên ð?t danh hi?u",
                        "Th?ng kê theo tiêu chí",
                        "Báo cáo t?ng h?p", 
                        "Ti?n ð? xét duy?t"
                    }, 0);
                }

                // Report level filter
                if (cmbReportLevel != null)
                {
                    ThreadSafeUIHelper.UpdateComboBoxItems(cmbReportLevel, new[]
                    {
                        "C?p Trý?ng",
                        "C?p Khoa",
                        "C?p L?p"
                    }, 0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing Vietnamese filters: {ex.Message}");
            }
        }

        /// <summary>
        /// Fix DataGridView v?i ti?ng Vi?t ðúng
        /// </summary>
        public static void FixDataGridViewVietnamese(DataGridView dataGridView)
        {
            if (dataGridView == null) return;

            ThreadSafeUIHelper.UpdateDataGridView(dataGridView, () =>
            {
                // Fix column headers
                foreach (DataGridViewColumn col in dataGridView.Columns)
                {
                    switch (col.Name.ToLower())
                    {
                        case "vaitro":
                        case "role":
                            col.HeaderText = "Vai tr?";
                            break;
                        case "trangthai":
                        case "status":
                            col.HeaderText = "Tr?ng thái";
                            break;
                        case "donvi":
                        case "department":
                            col.HeaderText = "Ðõn v?";
                            break;
                        case "masv":
                            col.HeaderText = "M? SV";
                            break;
                        case "hoten":
                            col.HeaderText = "H? tên";
                            break;
                        case "tieuchi":
                            col.HeaderText = "Tiêu chí";
                            break;
                        case "ngaynop":
                            col.HeaderText = "Ngày n?p";
                            break;
                    }
                }

                // Fix cell values
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value?.ToString() != null)
                        {
                            var cellText = cell.Value.ToString();
                            
                            // Fix common status values
                            switch (cellText.ToLower())
                            {
                                case "ho?t ð?ng":
                                case "active":
                                    cell.Value = "Ho?t ð?ng";
                                    break;
                                case "vô hi?u hóa":
                                case "inactive":
                                    cell.Value = "Vô hi?u hóa";
                                    break;
                                case "ch? duy?t":
                                case "pending":
                                    cell.Value = "Ch? duy?t";
                                    break;
                                case "ð? duy?t":
                                case "approved":
                                    cell.Value = "Ð? duy?t";
                                    break;
                                case "t? ch?i":
                                case "rejected":
                                    cell.Value = "T? ch?i";
                                    break;
                                case "c?n b? sung":
                                case "supplement":
                                    cell.Value = "C?n b? sung";
                                    break;
                            }

                            // Apply global Vietnamese fix
                            var fixedText = GlobalVietnameseFixer.FixVietnameseText(cellText);
                            if (fixedText != cellText)
                            {
                                cell.Value = fixedText;
                            }
                        }
                    }
                }
            });
        }

        /// <summary>
        /// Get correct Vietnamese role display name
        /// </summary>
        public static string GetVietnameseRoleDisplayName(string role)
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

        /// <summary>
        /// Get correct Vietnamese status display name
        /// </summary>
        public static string GetVietnameseStatusDisplayName(TrangThaiMinhChung status)
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

        /// <summary>
        /// Show Vietnamese MessageBox thread-safe
        /// </summary>
        public static DialogResult ShowVietnameseMessage(Control parent, string message, string title = "Thông báo",
            MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Information)
        {
            return ThreadSafeUIHelper.ShowMessageBox(parent, message, title, buttons, icon);
        }
    }
}