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
        private readonly IDbContextFactory<StudentManagementDbContext> _contextFactory;
        private readonly User _currentUser;
        private readonly SimpleReportService _reportService;
        private bool _isExporting = false;

        public SimpleReportForm(
            IDbContextFactory<StudentManagementDbContext> contextFactory, 
            SimpleReportService reportService,
            User currentUser)
        {
            _contextFactory = contextFactory;
            _reportService = reportService;
            _currentUser = currentUser;
            
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            this.Text = "Xuất báo cáo đơn giản";
            this.Size = new Size(560, 520);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.White;

            // Load academic years
            LoadAcademicYears();
        }

        private async void LoadAcademicYears()
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                
                var years = await context.NamHocs
                    .AsNoTracking()
                    .OrderByDescending(nh => nh.TuNgay)
                    .Select(nh => new { nh.MaNH, nh.TenNamHoc })
                    .ToListAsync();

                cmbNamHoc.DataSource = years;
                cmbNamHoc.DisplayMember = "TenNamHoc";
                cmbNamHoc.ValueMember = "MaNH";

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

        private string? GetSelectedNamHoc()
        {
            return cmbNamHoc.SelectedValue?.ToString();
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
            // Prevent double-click
            if (_isExporting)
            {
                return;
            }

            try
            {
                _isExporting = true;
                
                // Show loading
                this.Cursor = Cursors.WaitCursor;
                lblStatus.Text = $"⏳ Đang xuất {reportName}...";
                lblStatus.ForeColor = Color.FromArgb(52, 152, 219); // Blue

                string? filePath = null;
                string? namHoc = GetSelectedNamHoc();

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
                    lblStatus.Text = $"✅ Xuất {reportName} thành công!";
                    lblStatus.ForeColor = Color.FromArgb(46, 204, 113); // Green

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
                lblStatus.Text = $"❌ Lỗi xuất báo cáo: {ex.Message}";
                lblStatus.ForeColor = Color.FromArgb(231, 76, 60); // Red
                MessageBox.Show($"Lỗi xuất {reportName}: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isExporting = false;
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
