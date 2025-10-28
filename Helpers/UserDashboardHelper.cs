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
    /// Helper class �? x? l? UserDashboard thread-safe v� fix ti?ng Vi?t
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
                    ThreadSafeUIHelper.SetText(lblSystemStatusInfo, "Ho?t �?ng");
                    ThreadSafeUIHelper.SetForeColor(lblSystemStatusInfo, Color.White);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating dashboard stats: {ex.Message}");
            }
        }

        /// <summary>
        /// Initialize filters v?i ti?ng Vi?t ��ng
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
                        "�? duy?t",
                        "T? ch?i",
                        "C?n b? sung"
                    }, 0);
                }

                // Department filter
                if (cmbDepartmentFilter != null)
                {
                    ThreadSafeUIHelper.UpdateComboBoxItems(cmbDepartmentFilter, new[]
                    {
                        "T?t c? ��n v?"
                    }, 0);
                }

                // Criteria filter
                if (cmbCriteriaFilter != null)
                {
                    ThreadSafeUIHelper.UpdateComboBoxItems(cmbCriteriaFilter, new[]
                    {
                        "T?t c? ti�u ch�"
                    }, 0);
                }

                // Report type filter
                if (cmbReportType != null)
                {
                    ThreadSafeUIHelper.UpdateComboBoxItems(cmbReportType, new[]
                    {
                        "Danh s�ch sinh vi�n �?t danh hi?u",
                        "Th?ng k� theo ti�u ch�",
                        "B�o c�o t?ng h?p", 
                        "Ti?n �? x�t duy?t"
                    }, 0);
                }

                // Report level filter
                if (cmbReportLevel != null)
                {
                    ThreadSafeUIHelper.UpdateComboBoxItems(cmbReportLevel, new[]
                    {
                        "C?p Tr�?ng",
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
        /// Fix DataGridView v?i ti?ng Vi?t ��ng
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
                            col.HeaderText = "Tr?ng th�i";
                            break;
                        case "donvi":
                        case "department":
                            col.HeaderText = "��n v?";
                            break;
                        case "masv":
                            col.HeaderText = "M? SV";
                            break;
                        case "hoten":
                            col.HeaderText = "H? t�n";
                            break;
                        case "tieuchi":
                            col.HeaderText = "Ti�u ch�";
                            break;
                        case "ngaynop":
                            col.HeaderText = "Ng�y n?p";
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
                                case "ho?t �?ng":
                                case "active":
                                    cell.Value = "Ho?t �?ng";
                                    break;
                                case "v� hi?u h�a":
                                case "inactive":
                                    cell.Value = "V� hi?u h�a";
                                    break;
                                case "ch? duy?t":
                                case "pending":
                                    cell.Value = "Ch? duy?t";
                                    break;
                                case "�? duy?t":
                                case "approved":
                                    cell.Value = "�? duy?t";
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

        /// <summary>
        /// Get correct Vietnamese status display name
        /// </summary>
        public static string GetVietnameseStatusDisplayName(TrangThaiMinhChung status)
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

        /// <summary>
        /// Show Vietnamese MessageBox thread-safe
        /// </summary>
        public static DialogResult ShowVietnameseMessage(Control parent, string message, string title = "Th�ng b�o",
            MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Information)
        {
            return ThreadSafeUIHelper.ShowMessageBox(parent, message, title, buttons, icon);
        }
    }
}