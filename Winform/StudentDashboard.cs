using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudentManagement5GoodTempp.DataAccess.Context;
using StudentManagement5GoodTempp.DataAccess.Entity;
using StudentManagement5GoodTempp.Services;
using StudentManagement5Good.Winform;

// Helper class for evidence summary
public class EvidenceSummary
{
    public string MaTC { get; set; } = string.Empty;
    public int Count { get; set; }
    public StudentManagement5GoodTempp.DataAccess.Entity.TieuChi TieuChi { get; set; } = null!;
}

namespace StudentManagement5Good.Winform
{
    /// <summary>
    /// Dashboard chuyên dụng cho sinh viên theo dõi tiến độ "5 Tốt"
    /// </summary>
    public partial class StudentDashboard : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IUserService _userService;
        private readonly IStudentService _studentService;
        private readonly User _currentUser;
        private SinhVien? _currentStudent;
        private string _currentNamHoc = "";

        public StudentDashboard(IServiceProvider serviceProvider, IUserService userService, 
                               IStudentService studentService, User currentUser)
        {
            _serviceProvider = serviceProvider;
            _userService = userService;
            _studentService = studentService;
            _currentUser = currentUser;
            
            InitializeComponent();
            InitializeStudentInterface();
        }

        // Constructor cũ để backward compatibility
        public StudentDashboard(StudentManagementDbContext context, IUserService userService, 
                               IStudentService studentService, User currentUser)
            : this(StudentManagement5GoodTempp.Program.ServiceProvider, userService, studentService, currentUser)
        {
        }

        private async void StudentDashboard_Load(object sender, EventArgs e)
        {
            try
            {
                // Show loading indicator
                this.Cursor = Cursors.WaitCursor;
                lblOverallStatus.Text = "Đang tải dữ liệu...";
                
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                // Load current academic year from database
                var currentYear = await context.NamHocs
                    .AsNoTracking()
                    .OrderByDescending(nh => nh.TuNgay)
                    .FirstOrDefaultAsync();
                
                if (currentYear != null)
                {
                    _currentNamHoc = currentYear.MaNH;
                    lblCurrentYear.Text = $"Năm học {currentYear.TenNamHoc}";
                }
                else
                {
                    // Nếu không có năm học nào, sử dụng mặc định
                    _currentNamHoc = DateTime.Now.Year.ToString();
                    lblCurrentYear.Text = $"Năm học {_currentNamHoc}";
                }
                
                // Load data step by step với error handling riêng
                await SafeExecuteAsync(async () => await LoadStudentData(), "Lỗi tải thông tin sinh viên");
                await SafeExecuteAsync(async () => await LoadDashboardData(), "Lỗi tải dữ liệu đánh giá");
                await SafeExecuteAsync(async () => await LoadMinhChungData(), "Lỗi tải dữ liệu minh chứng");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu dashboard: {ex.Message}", 
                              "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                // Set default status in case of general error
                lblOverallStatus.Text = "Lỗi tải dữ liệu";
                lblOverallStatus.ForeColor = Color.FromArgb(231, 76, 60);
            }
            finally
            {
                // Hide loading indicator
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Helper method để thực thi async operations với error handling
        /// </summary>
        private async Task SafeExecuteAsync(Func<Task> action, string errorMessage)
        {
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                // Log error nhưng không làm crash ứng dụng
                Console.WriteLine($"{errorMessage}: {ex.Message}");
                
                // Có thể hiển thị notification nhỏ thay vì MessageBox popup
                // Hoặc log vào file để debug sau này
            }
        }

        private void InitializeStudentInterface()
        {
            // Set form properties
            this.Text = $"Dashboard Sinh viên 5 Tốt - {_currentUser.HoTen}";
            this.StartPosition = FormStartPosition.CenterScreen;

            // Set basic user info
            lblStudentName.Text = _currentUser.HoTen;
            lblCurrentYear.Text = $"Năm học {_currentNamHoc}";
            lblOverallStatus.Text = "Đang tải...";
        }

        private async Task LoadStudentData()
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                // Load student information với tất cả navigation properties cần thiết
                _currentStudent = await context.SinhViens
                    .Include(s => s.Lop)
                    .ThenInclude(l => l.Khoa)
                    .ThenInclude(k => k.Truong)
                    .FirstOrDefaultAsync(s => s.MaSV == _currentUser.MaSV);

                if (_currentStudent != null)
                {
                    lblStudentId.Text = _currentStudent.MaSV ?? "N/A";
                    lblStudentName.Text = _currentStudent.HoTen ?? "N/A";
                    
                    // Kiểm tra null cho các navigation properties
                    var classText = _currentStudent.Lop?.TenLop ?? "N/A";
                    var facultyText = _currentStudent.Lop?.Khoa?.TenKhoa ?? "N/A";
                    var schoolText = _currentStudent.Lop?.Khoa?.Truong?.TenTruong ?? "N/A";
                    
                    lblClassInfo.Text = $"{classText} - {facultyText}";
                    lblSchoolInfo.Text = schoolText;
                }
                else
                {
                    // Nếu không tìm thấy sinh viên, hiển thị thông tin cơ bản từ User
                    lblStudentId.Text = _currentUser.MaSV ?? "N/A";
                    lblStudentName.Text = _currentUser.HoTen ?? "N/A";
                    lblClassInfo.Text = "Chưa có thông tin lớp";
                    lblSchoolInfo.Text = "Chưa có thông tin trường";
                    
                    MessageBox.Show("Không tìm thấy thông tin sinh viên trong hệ thống!\nVui lòng liên hệ giáo vụ để cập nhật thông tin.", 
                                  "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Không thể tải thông tin sinh viên: {ex.Message}");
            }
        }

        private async Task LoadDashboardData()
        {
            try
            {
                // Kiểm tra _currentStudent null trước khi thực hiện queries
                if (_currentStudent == null || string.IsNullOrEmpty(_currentStudent.MaSV))
                {
                    // Nếu không có thông tin sinh viên, hiển thị trạng thái mặc định
                    SetDefaultCriteriaStatus();
                    lblOverallStatus.Text = "Không có thông tin sinh viên";
                    lblOverallStatus.ForeColor = Color.FromArgb(231, 76, 60);
                    return;
                }

                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                // Load approved evidence for current student and year
                var approvedEvidence = await context.MinhChungs
                    .Include(m => m.TieuChi)
                    .Where(m => m.MaSV == _currentStudent.MaSV && 
                               m.MaNH == _currentNamHoc && 
                               m.TrangThai == TrangThaiMinhChung.DaDuyet)
                    .GroupBy(m => m.MaTC)
                    .Select(g => new EvidenceSummary { 
                        MaTC = g.Key, 
                        Count = g.Count(), 
                        TieuChi = g.First().TieuChi ?? new TieuChi { TenTieuChi = "N/A" }
                    })
                    .ToListAsync();

                // Load evaluation results if available
                var ketQuaXetDuyets = await context.KetQuaXetDuyets
                    .Include(k => k.TieuChi)
                    .Where(k => k.MaSV == _currentStudent.MaSV && k.MaNH == _currentNamHoc)
                    .ToListAsync();

                // Update 5 criteria status
                await UpdateCriteriaStatusNew(approvedEvidence, ketQuaXetDuyets);

                // Update overall status
                await UpdateOverallStatusNew(ketQuaXetDuyets);
            }
            catch (Exception ex)
            {
                // Set default status in case of error
                SetDefaultCriteriaStatus();
                lblOverallStatus.Text = "Lỗi tải dữ liệu";
                lblOverallStatus.ForeColor = Color.FromArgb(231, 76, 60);
                
                throw new Exception($"Không thể tải dữ liệu đánh giá: {ex.Message}");
            }
        }

        private void SetDefaultCriteriaStatus()
        {
            // Set tất cả tiêu chí về trạng thái mặc định
            UpdateCriteriaCardDefault(panelDaoDuc, lblDaoDucStatus, btnDaoDucAction, "Đạo đức tốt");
            UpdateCriteriaCardDefault(panelHocTap, lblHocTapStatus, btnHocTapAction, "Học tập tốt");
            UpdateCriteriaCardDefault(panelTheLuc, lblTheLucStatus, btnTheLucAction, "Thể lực tốt");
            UpdateCriteriaCardDefault(panelTinhNguyen, lblTinhNguyenStatus, btnTinhNguyenAction, "Tình nguyện tốt");
            UpdateCriteriaCardDefault(panelHoiNhap, lblHoiNhapStatus, btnHoiNhapAction, "Hội nhập tốt");
        }

        private void UpdateCriteriaCardDefault(Panel panel, Label statusLabel, Button actionButton, string criteriaName)
        {
            panel.BackColor = Color.FromArgb(149, 165, 166); // Gray
            statusLabel.Text = "Chưa có dữ liệu";
            statusLabel.ForeColor = Color.White;
            actionButton.Text = "Nộp minh chứng";
            actionButton.BackColor = Color.FromArgb(127, 140, 141);
        }

        private Task UpdateCriteriaStatusNew(List<EvidenceSummary> approvedEvidence, List<KetQuaXetDuyet> ketQuaXetDuyets)
        {
            // TC01 - Đạo đức tốt
            var daoducKetQua = ketQuaXetDuyets.FirstOrDefault(k => k.MaTC == "TC01");
            var daoducEvidence = approvedEvidence.FirstOrDefault(e => e.MaTC == "TC01");
            UpdateCriteriaCardNew(panelDaoDuc, lblDaoDucStatus, btnDaoDucAction, daoducKetQua, daoducEvidence, "Đạo đức tốt");

            // TC02 - Học tập tốt
            var hoctapKetQua = ketQuaXetDuyets.FirstOrDefault(k => k.MaTC == "TC02");
            var hoctapEvidence = approvedEvidence.FirstOrDefault(e => e.MaTC == "TC02");
            UpdateCriteriaCardNew(panelHocTap, lblHocTapStatus, btnHocTapAction, hoctapKetQua, hoctapEvidence, "Học tập tốt");

            // TC03 - Thể lực tốt
            var thelucKetQua = ketQuaXetDuyets.FirstOrDefault(k => k.MaTC == "TC03");
            var thelucEvidence = approvedEvidence.FirstOrDefault(e => e.MaTC == "TC03");
            UpdateCriteriaCardNew(panelTheLuc, lblTheLucStatus, btnTheLucAction, thelucKetQua, thelucEvidence, "Thể lực tốt");

            // TC04 - Tình nguyện tốt
            var tinhnguyenKetQua = ketQuaXetDuyets.FirstOrDefault(k => k.MaTC == "TC04");
            var tinhnguyenEvidence = approvedEvidence.FirstOrDefault(e => e.MaTC == "TC04");
            UpdateCriteriaCardNew(panelTinhNguyen, lblTinhNguyenStatus, btnTinhNguyenAction, tinhnguyenKetQua, tinhnguyenEvidence, "Tình nguyện tốt");

            // TC05 - Hội nhập tốt
            var hoinhapKetQua = ketQuaXetDuyets.FirstOrDefault(k => k.MaTC == "TC05");
            var hoinhapEvidence = approvedEvidence.FirstOrDefault(e => e.MaTC == "TC05");
            UpdateCriteriaCardNew(panelHoiNhap, lblHoiNhapStatus, btnHoiNhapAction, hoinhapKetQua, hoinhapEvidence, "Hội nhập tốt");
            
            return Task.CompletedTask;
        }

        private void UpdateCriteriaCardNew(Panel panel, Label statusLabel, Button actionButton, KetQuaXetDuyet? ketQua, EvidenceSummary? evidence, string criteriaName)
        {
            if (ketQua != null && ketQua.KetQua)
            {
                // Đã có kết quả xét duyệt và đạt
                panel.BackColor = Color.FromArgb(46, 204, 113); // Green
                statusLabel.Text = "✓ Đã đạt";
                statusLabel.ForeColor = Color.White;
                actionButton.Text = "Xem kết quả";
                actionButton.BackColor = Color.FromArgb(39, 174, 96);
                
                if (ketQua.Diem.HasValue)
                    statusLabel.Text += $" ({ketQua.Diem:F1} điểm)";
            }
            else if (evidence != null && evidence.Count > 0)
            {
                // Có minh chứng đã được duyệt nhưng chưa có kết quả xét duyệt cuối cùng
                panel.BackColor = Color.FromArgb(52, 152, 219); // Blue
                statusLabel.Text = $"✓ Có {evidence.Count} minh chứng đã duyệt";
                statusLabel.ForeColor = Color.White;
                actionButton.Text = "Chờ xét duyệt";
                actionButton.BackColor = Color.FromArgb(41, 128, 185);
            }
            else
            {
                // Chưa có minh chứng được duyệt
                panel.BackColor = Color.FromArgb(231, 76, 60); // Red
                statusLabel.Text = "✗ Chưa có minh chứng";
                statusLabel.ForeColor = Color.White;
                actionButton.Text = "Nộp minh chứng";
                actionButton.BackColor = Color.FromArgb(192, 57, 43);
            }
        }

        private async Task UpdateOverallStatusNew(List<KetQuaXetDuyet> ketQuaXetDuyets)
        {
            var passedCount = ketQuaXetDuyets.Count(k => k.KetQua);
            var totalCount = 5;

            if (passedCount == 0)
            {
                lblOverallStatus.Text = "Chưa bắt đầu đánh giá";
                lblOverallStatus.ForeColor = Color.FromArgb(149, 165, 166);
            }
            else if (passedCount < totalCount)
            {
                lblOverallStatus.Text = $"Đang tiến hành ({passedCount}/{totalCount} tiêu chí)";
                lblOverallStatus.ForeColor = Color.FromArgb(243, 156, 18);
            }
            else
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                // Check final result with proper Include for CapXet
                var finalResult = await context.KetQuaDanhHieus
                    .Include(k => k.CapXet) // Include CapXet để tránh null reference
                    .FirstOrDefaultAsync(k => k.MaSV == _currentStudent.MaSV && k.MaNH == _currentNamHoc);

                if (finalResult?.DatDanhHieu == true)
                {
                    // Kiểm tra CapXet null trước khi truy cập TenCap
                    var capXetText = finalResult.CapXet?.TenCap ?? finalResult.MaCap ?? "Không xác định";
                    lblOverallStatus.Text = $"Đạt danh hiệu Sinh viên 5 Tốt (Cấp {capXetText})";
                    lblOverallStatus.ForeColor = Color.FromArgb(46, 204, 113);
                }
                else
                {
                    lblOverallStatus.Text = "Hoàn thành 5 tiêu chí - Chờ xét duyệt";
                    lblOverallStatus.ForeColor = Color.FromArgb(52, 152, 219);
                }
            }
        }

        private async Task LoadMinhChungData()
        {
            try
            {
                // Clear existing data first
                listViewMinhChung.Items.Clear();
                lblMinhChungCount.Text = "Tổng: 0";

                if (_currentStudent == null || string.IsNullOrEmpty(_currentStudent.MaSV))
                {
                    lblMinhChungCount.Text = "Không có thông tin sinh viên";
                    return;
                }

                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                // Load evidence documents - Thiết kế mới: MinhChung độc lập
                var minhchungs = await context.MinhChungs
                    .Include(m => m.TieuChi)
                    .Include(m => m.NguoiDuyetUser)
                    .Where(m => m.MaSV == _currentStudent.MaSV && m.MaNH == _currentNamHoc)
                    .OrderByDescending(m => m.NgayNop)
                    .ToListAsync();

                // Populate evidence list
                foreach (var mc in minhchungs)
                {
                    var item = new ListViewItem(mc.TenMinhChung ?? "N/A");
                    item.SubItems.Add(mc.TieuChi?.TenTieuChi ?? "N/A");
                    item.SubItems.Add(mc.NgayNop.ToString("dd/MM/yyyy"));
                    
                    // Trạng thái với màu sắc
                    var statusSubItem = item.SubItems.Add(mc.TrangThai.ToDisplayString());
                    statusSubItem.ForeColor = mc.TrangThai.ToColor();
                    
                    // Phản hồi (lý do từ chối hoặc ghi chú)
                    var feedback = !string.IsNullOrEmpty(mc.LyDoTuChoi) ? mc.LyDoTuChoi : mc.GhiChu ?? "";
                    item.SubItems.Add(feedback);
                    
                    item.Tag = mc;
                    
                    // Set row color based on status
                    if (mc.TrangThai == TrangThaiMinhChung.DaDuyet)
                        item.BackColor = System.Drawing.Color.FromArgb(230, 255, 230); // Light green
                    else if (mc.TrangThai == TrangThaiMinhChung.BiTuChoi)
                        item.BackColor = System.Drawing.Color.FromArgb(255, 230, 230); // Light red
                    else if (mc.TrangThai == TrangThaiMinhChung.CanBoSung)
                        item.BackColor = System.Drawing.Color.FromArgb(230, 240, 255); // Light blue
                    
                    listViewMinhChung.Items.Add(item);
                }

                // Update evidence count with status breakdown
                var totalCount = minhchungs.Count;
                var approvedCount = minhchungs.Count(m => m.TrangThai == TrangThaiMinhChung.DaDuyet);
                var pendingCount = minhchungs.Count(m => m.TrangThai == TrangThaiMinhChung.ChoDuyet);
                var rejectedCount = minhchungs.Count(m => m.TrangThai == TrangThaiMinhChung.BiTuChoi);
                
                lblMinhChungCount.Text = $"Tổng: {totalCount} | Đã duyệt: {approvedCount} | Chờ duyệt: {pendingCount} | Từ chối: {rejectedCount}";
            }
            catch (Exception ex)
            {
                lblMinhChungCount.Text = "Lỗi tải dữ liệu minh chứng";
                throw new Exception($"Không thể tải dữ liệu minh chứng: {ex.Message}");
            }
        }

        #region Event Handlers

        private void btnDaoDucAction_Click(object sender, EventArgs e)
        {
            OpenMinhChungForm("TC01", "Đạo đức tốt");
        }

        private void btnHocTapAction_Click(object sender, EventArgs e)
        {
            OpenMinhChungForm("TC02", "Học tập tốt");
        }

        private void btnTheLucAction_Click(object sender, EventArgs e)
        {
            OpenMinhChungForm("TC03", "Thể lực tốt");
        }

        private void btnTinhNguyenAction_Click(object sender, EventArgs e)
        {
            OpenMinhChungForm("TC04", "Tình nguyện tốt");
        }

        private void btnHoiNhapAction_Click(object sender, EventArgs e)
        {
            OpenMinhChungForm("TC05", "Hội nhập tốt");
        }

        private void btnNopMinhChung_Click(object sender, EventArgs e)
        {
            OpenMinhChungForm("", "Chọn tiêu chí");
        }

        private async void OpenMinhChungForm(string maTC, string tenTieuChi)
        {
            try
            {
                if (_currentUser == null)
                {
                    MessageBox.Show("Không tìm thấy thông tin sinh viên!", "Lỗi", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Validate MaSV
                if (string.IsNullOrEmpty(_currentUser.MaSV))
                {
                    MessageBox.Show("Tài khoản của bạn chưa được liên kết với Mã sinh viên!\n\nVui lòng liên hệ Giáo vụ để cập nhật thông tin.", 
                                  "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                var form = new MinhChungForm(context, _currentUser.MaSV, maTC, _currentNamHoc);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // Refresh data after successful submission
                    await LoadDashboardData();
                    await LoadMinhChungData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở form minh chứng: {ex.Message}", 
                              "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            StudentDashboard_Load(sender, e);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", 
                              MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                
                // Sử dụng constructor mới với ServiceProvider
                var loginForm = new Login(_serviceProvider);
                loginForm.ShowDialog();
            }
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng thông tin cá nhân đang được phát triển...", "Thông báo");
        }

        private void listViewMinhChung_DoubleClick(object sender, EventArgs e)
        {
            if (listViewMinhChung.SelectedItems.Count > 0)
            {
                var selectedItem = listViewMinhChung.SelectedItems[0];
                var minhchung = selectedItem.Tag as MinhChung;
                
                if (minhchung != null)
                {
                    // Show evidence details
                    var details = $"Tên minh chứng: {minhchung.TenMinhChung}\n" +
                                 $"Tiêu chí: {minhchung.TieuChi.TenTieuChi}\n" +
                                 $"Ngày nộp: {minhchung.NgayNop:dd/MM/yyyy HH:mm}\n" +
                                 $"Trạng thái: {minhchung.TrangThai.ToDisplayString()}\n" +
                                 $"File: {minhchung.TenFile}\n" +
                                 $"Kích thước: {GetFileSizeString(minhchung.KichThuocFile)}\n";
                    
                    if (!string.IsNullOrEmpty(minhchung.MoTa))
                        details += $"Mô tả: {minhchung.MoTa}\n";
                    
                    if (minhchung.NgayDuyet.HasValue)
                        details += $"Ngày duyệt: {minhchung.NgayDuyet:dd/MM/yyyy HH:mm}\n";
                    
                    if (!string.IsNullOrEmpty(minhchung.LyDoTuChoi))
                        details += $"Lý do từ chối: {minhchung.LyDoTuChoi}\n";
                    
                    if (!string.IsNullOrEmpty(minhchung.GhiChu))
                        details += $"Ghi chú: {minhchung.GhiChu}";
                    
                    MessageBox.Show(details, "Chi tiết minh chứng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private string GetFileSizeString(long? fileSize)
        {
            if (!fileSize.HasValue) return "Không xác định";
            
            var bytes = fileSize.Value;
            if (bytes < 1024)
                return $"{bytes} B";
            else if (bytes < 1024 * 1024)
                return $"{bytes / 1024:F1} KB";
            else
                return $"{bytes / (1024 * 1024):F1} MB";
        }

        private async void listViewMinhChung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && listViewMinhChung.SelectedItems.Count > 0)
            {
                var selectedItem = listViewMinhChung.SelectedItems[0];
                var minhchung = selectedItem.Tag as MinhChung;
                
                if (minhchung != null)
                {
                    // Chỉ cho phép xóa minh chứng đang chờ duyệt hoặc cần bổ sung
                    if (!minhchung.TrangThai.CanEdit())
                    {
                        MessageBox.Show("Chỉ có thể xóa minh chứng đang chờ duyệt hoặc cần bổ sung!", 
                                      "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (MessageBox.Show($"Bạn có chắc chắn muốn xóa minh chứng '{minhchung.TenMinhChung}'?", 
                                      "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            using var scope = _serviceProvider.CreateScope();
                            var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                            var evidenceToDelete = await context.MinhChungs.FindAsync(minhchung.MaMC);
                            if (evidenceToDelete != null)
                            {
                                context.MinhChungs.Remove(evidenceToDelete);
                                await context.SaveChangesAsync();
                                
                                MessageBox.Show("Xóa minh chứng thành công!", "Thành công", 
                                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                                
                                // Refresh data
                                await LoadDashboardData();
                                await LoadMinhChungData();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi xóa minh chứng: {ex.Message}", "Lỗi", 
                                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        #endregion
    }
}
