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
using StudentManagement5GoodTempp.DataAccess.Context;
using StudentManagement5GoodTempp.DataAccess.Entity;
using StudentManagement5GoodTempp.Services;
using StudentManagement5Good.Winform;
namespace StudentManagement5Good.Winform
{
    /// <summary>
    /// Dashboard chuyên dụng cho sinh viên theo dõi tiến độ "5 Tốt"
    /// </summary>
    public partial class StudentDashboard : Form
    {
        private readonly StudentManagementDbContext _context;
        private readonly IUserService _userService;
        private readonly IStudentService _studentService;
        private readonly User _currentUser;
        private SinhVien? _currentStudent;
        private string _currentNamHoc = "";

        public StudentDashboard(StudentManagementDbContext context, IUserService userService, 
                               IStudentService studentService, User currentUser)
        {
            _context = context;
            _userService = userService;
            _studentService = studentService;
            _currentUser = currentUser;
            
            InitializeComponent();
            InitializeStudentInterface();
        }

        private async void StudentDashboard_Load(object sender, EventArgs e)
        {
            try
            {
                // Load current academic year from database
                var currentYear = await _context.NamHocs
                    .OrderByDescending(nh => nh.TuNgay)
                    .FirstOrDefaultAsync();
                
                if (currentYear != null)
                {
                    _currentNamHoc = currentYear.MaNH;
                    lblCurrentYear.Text = $"Năm học {currentYear.TenNamHoc}";
                }
                
                await LoadStudentData();
                await LoadDashboardData();
                await LoadMinhChungData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", 
                              "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                // Load student information
                _currentStudent = await _context.SinhViens
                    .Include(s => s.Lop)
                    .ThenInclude(l => l.Khoa)
                    .ThenInclude(k => k.Truong)
                    .FirstOrDefaultAsync(s => s.MaSV == _currentUser.MaSV);

                if (_currentStudent != null)
                {
                    lblStudentId.Text = _currentStudent.MaSV;
                    lblStudentName.Text = _currentStudent.HoTen;
                    lblClassInfo.Text = $"{_currentStudent.Lop.TenLop} - {_currentStudent.Lop.Khoa.TenKhoa}";
                    lblSchoolInfo.Text = _currentStudent.Lop.Khoa.Truong.TenTruong;
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
                if (_currentStudent == null) return;

                // Load approved evidence for current student and year
                var approvedEvidence = await _context.MinhChungs
                    .Include(m => m.TieuChi)
                    .Where(m => m.MaSV == _currentStudent.MaSV && 
                               m.MaNH == _currentNamHoc && 
                               m.TrangThai == TrangThaiMinhChung.DaDuyet)
                    .GroupBy(m => m.MaTC)
                    .Select(g => new { MaTC = g.Key, Count = g.Count(), TieuChi = g.First().TieuChi })
                    .ToListAsync<dynamic>();

                // Load evaluation results if available
                var ketQuaXetDuyets = await _context.KetQuaXetDuyets
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
                throw new Exception($"Không thể tải dữ liệu đánh giá: {ex.Message}");
            }
        }

        private async Task UpdateCriteriaStatus(List<DanhGia> evaluations)
        {
            // TC01 - Đạo đức tốt
            var daoducEval = evaluations.FirstOrDefault(e => e.MaTC == "TC01");
            UpdateCriteriaCard(panelDaoDuc, lblDaoDucStatus, btnDaoDucAction, daoducEval, "Đạo đức tốt");

            // TC02 - Học tập tốt
            var hoctapEval = evaluations.FirstOrDefault(e => e.MaTC == "TC02");
            UpdateCriteriaCard(panelHocTap, lblHocTapStatus, btnHocTapAction, hoctapEval, "Học tập tốt");

            // TC03 - Thể lực tốt
            var thelucEval = evaluations.FirstOrDefault(e => e.MaTC == "TC03");
            UpdateCriteriaCard(panelTheLuc, lblTheLucStatus, btnTheLucAction, thelucEval, "Thể lực tốt");

            // TC04 - Tình nguyện tốt
            var tinhnguyenEval = evaluations.FirstOrDefault(e => e.MaTC == "TC04");
            UpdateCriteriaCard(panelTinhNguyen, lblTinhNguyenStatus, btnTinhNguyenAction, tinhnguyenEval, "Tình nguyện tốt");

            // TC05 - Hội nhập tốt
            var hoinhapEval = evaluations.FirstOrDefault(e => e.MaTC == "TC05");
            UpdateCriteriaCard(panelHoiNhap, lblHoiNhapStatus, btnHoiNhapAction, hoinhapEval, "Hội nhập tốt");
        }

        private void UpdateCriteriaCard(Panel panel, Label statusLabel, Button actionButton, DanhGia? evaluation, string criteriaName)
        {
            if (evaluation != null && evaluation.DatTieuChi)
            {
                // Đã đạt
                panel.BackColor = Color.FromArgb(46, 204, 113); // Green
                statusLabel.Text = "✓ Đã đạt";
                statusLabel.ForeColor = Color.White;
                actionButton.Text = "Xem chi tiết";
                actionButton.BackColor = Color.FromArgb(39, 174, 96);
            }
            else
            {
                // Chưa đạt
                panel.BackColor = Color.FromArgb(231, 76, 60); // Red
                statusLabel.Text = "✗ Chưa đạt";
                statusLabel.ForeColor = Color.White;
                actionButton.Text = "Nộp minh chứng";
                actionButton.BackColor = Color.FromArgb(192, 57, 43);
            }

            // Set additional info based on criteria type
            if (evaluation != null && !string.IsNullOrEmpty(evaluation.GiaTri))
            {
                switch (evaluation.MaTC)
                {
                    case "TC02": // Học tập
                        statusLabel.Text += $" (GPA: {evaluation.GiaTri})";
                        break;
                    case "TC03": // Thể lực
                        statusLabel.Text += $" ({evaluation.GiaTri} giờ)";
                        break;
                    case "TC04": // Tình nguyện
                        statusLabel.Text += $" ({evaluation.GiaTri} giờ)";
                        break;
                    case "TC05": // Hội nhập
                        statusLabel.Text += $" ({evaluation.GiaTri} điểm)";
                        break;
                }
            }
        }

        private async Task UpdateCriteriaStatusNew(List<dynamic> approvedEvidence, List<KetQuaXetDuyet> ketQuaXetDuyets)
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
        }

        private void UpdateCriteriaCardNew(Panel panel, Label statusLabel, Button actionButton, KetQuaXetDuyet? ketQua, dynamic evidence, string criteriaName)
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
                // Check final result
                var finalResult = await _context.KetQuaDanhHieus
                    .FirstOrDefaultAsync(k => k.MaSV == _currentStudent.MaSV && k.MaNH == _currentNamHoc);

                if (finalResult?.DatDanhHieu == true)
                {
                    lblOverallStatus.Text = $"🏆 Đạt danh hiệu Sinh viên 5 Tốt (Cấp {finalResult.CapXet.TenCap})";
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
                if (_currentStudent == null) return;

                // Load evidence documents - Thiết kế mới: MinhChung độc lập
                var minhchungs = await _context.MinhChungs
                    .Include(m => m.TieuChi)
                    .Include(m => m.NguoiDuyetUser)
                    .Where(m => m.MaSV == _currentStudent.MaSV && m.MaNH == _currentNamHoc)
                    .OrderByDescending(m => m.NgayNop)
                    .ToListAsync();

                // Populate evidence list
                listViewMinhChung.Items.Clear();
                foreach (var mc in minhchungs)
                {
                    var item = new ListViewItem(mc.TenMinhChung);
                    item.SubItems.Add(mc.TieuChi.TenTieuChi);
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
                throw new Exception($"Không thể tải dữ liệu minh chứng: {ex.Message}");
            }
        }

        private string GetStatusDisplayName(string status)
        {
            return status switch
            {
                "PENDING" => "Chờ duyệt",
                "APPROVED" => "Đã duyệt",
                "REJECTED" => "Bị từ chối",
                "NEED_MORE" => "Cần bổ sung",
                _ => status
            };
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

                var form = new MinhChungForm(_context, _currentUser.MaSV, maTC, _currentNamHoc);
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
                var loginForm = new Login(_context, _studentService, _userService);
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
                            _context.MinhChungs.Remove(minhchung);
                            await _context.SaveChangesAsync();
                            
                            MessageBox.Show("Xóa minh chứng thành công!", "Thành công", 
                                          MessageBoxButtons.OK, MessageBoxIcon.Information);
                            
                            // Refresh data
                            await LoadDashboardData();
                            await LoadMinhChungData();
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
