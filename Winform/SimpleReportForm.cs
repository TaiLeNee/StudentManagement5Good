using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using StudentManagement5GoodTempp.DataAccess.Context;
using StudentManagement5GoodTempp.DataAccess.Entity;
using StudentManagement5GoodTempp.Services;

namespace StudentManagement5Good.Winform
{
    public partial class SimpleReportForm : Form
    {
        private readonly StudentManagementDbContext _context;
        private readonly User _currentUser;
        private readonly SimpleReportService _reportService;

        public SimpleReportForm(StudentManagementDbContext context, User currentUser)
        {
            _context = context;
            _currentUser = currentUser;
            _reportService = new SimpleReportService(context);
            
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            this.Text = "Xuất báo cáo đơn giản";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Load academic years
            LoadAcademicYears();
        }

        private async void LoadAcademicYears()
        {
            try
            {
                var years = await _context.NamHocs
                    .OrderByDescending(nh => nh.TuNgay)
                    .Select(nh => new { nh.MaNH, nh.TenNamHoc })
                    .ToListAsync();

                cmbNamHoc.Items.Clear();
                foreach (var year in years)
                {
                    cmbNamHoc.Items.Add($"{year.MaNH} - {year.TenNamHoc}");
                }

                if (cmbNamHoc.Items.Count > 0)
                {
                    cmbNamHoc.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách năm học: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetSelectedNamHoc()
        {
            if (cmbNamHoc.SelectedItem == null) return null;
            
            var selectedText = cmbNamHoc.SelectedItem.ToString();
            var parts = selectedText.Split(new string[] { " - " }, StringSplitOptions.None);
            return parts.Length > 0 ? parts[0] : null;
        }

        private async void btnExportStudentReport_Click(object sender, EventArgs e)
        {
            await ExportReportAsync("student", "Báo cáo danh sách sinh viên 5 tốt");
        }

        private async void btnExportEvidenceReport_Click(object sender, EventArgs e)
        {
            await ExportReportAsync("evidence", "Báo cáo minh chứng");
        }

        private async void btnExportStatisticsReport_Click(object sender, EventArgs e)
        {
            await ExportReportAsync("statistics", "Báo cáo thống kê");
        }

        private async Task ExportReportAsync(string reportType, string reportName)
        {
            try
            {
                // Show loading
                this.Cursor = Cursors.WaitCursor;
                lblStatus.Text = $"Đang xuất {reportName}...";
                lblStatus.ForeColor = Color.Blue;

                string filePath = null;
                string namHoc = GetSelectedNamHoc();

                // Export based on type
                switch (reportType)
                {
                    case "student":
                        filePath = await _reportService.ExportStudentReportAsync(namHoc);
                        break;
                    case "evidence":
                        filePath = await _reportService.ExportEvidenceReportAsync(namHoc);
                        break;
                    case "statistics":
                        filePath = await _reportService.ExportStatisticsReportAsync(namHoc);
                        break;
                }

                if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                {
                    lblStatus.Text = $"Xuất {reportName} thành công!";
                    lblStatus.ForeColor = Color.Green;

                    var result = MessageBox.Show(
                        $"{reportName} đã được xuất thành công!\n\n" +
                        $"File: {Path.GetFileName(filePath)}\n" +
                        $"Vị trí: {filePath}\n\n" +
                        "Bạn có muốn mở file?",
                        "Xuất báo cáo thành công",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                        {
                            FileName = filePath,
                            UseShellExecute = true
                        });
                    }
                }
                else
                {
                    throw new Exception("Không thể tạo file báo cáo");
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Lỗi xuất báo cáo: {ex.Message}";
                lblStatus.ForeColor = Color.Red;
                MessageBox.Show($"Lỗi xuất {reportName}: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
