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
using System.Windows.Forms.DataVisualization.Charting;
using StudentManagement5Good.Winform;
namespace StudentManagement5Good.Winform
{
    public partial class UserDashboard : Form
    {
        private readonly StudentManagementDbContext _context;
        private readonly IUserService _userService;
        private readonly IStudentService _studentService;
        private readonly User _currentUser;
        private string? _currentNamHoc;
        private MinhChung? _selectedEvidence;
        private System.Windows.Forms.Timer? _refreshTimer;

        public UserDashboard(StudentManagementDbContext context, IUserService userService, 
                           IStudentService studentService, User currentUser)
        {
            _context = context;
            _userService = userService;
            _studentService = studentService;
            _currentUser = currentUser;
            
            InitializeComponent();
            InitializeUserInterface();
        }

        private async void UserDashboard_Load(object sender, EventArgs e)
        {
            try
            {
                await LoadCurrentAcademicYear();
                await LoadDashboardData();
                ConfigureUIBasedOnRole();
                ShowDashboardModule();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu dashboard: {ex.Message}", 
                              "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeUserInterface()
        {
            // Set form properties
            this.Text = $"Hệ thống Quản lý Sinh viên 5 Tốt - {_currentUser.HoTen}";
            
            // Set user info in header and navigation
            lblUserInfo.Text = $"Chào mừng, {_currentUser.HoTen}";
            lblUserName.Text = _currentUser.HoTen;
            lblUserRole.Text = GetRoleDisplayName(_currentUser.VaiTro);
            
            // Set current date time
            lblCurrentDateTime.Text = DateTime.Now.ToString("dddd, dd/MM/yyyy HH:mm");
            
            // Initialize filters
            InitializeFilters();
            
            // Start timer for real-time updates
            _refreshTimer = new System.Windows.Forms.Timer();
            _refreshTimer.Interval = 60000; // Update every minute
            _refreshTimer.Tick += Timer_Tick;
            _refreshTimer.Start();
        }

        private async Task LoadCurrentAcademicYear()
        {
            var currentYear = await _context.NamHocs
                .AsNoTracking()
                .Where(nh => nh.TuNgay <= DateTime.Now && nh.DenNgay >= DateTime.Now)
                .FirstOrDefaultAsync();
            
            _currentNamHoc = currentYear?.MaNH ?? DateTime.Now.Year.ToString();
        }

        private async Task LoadDashboardData()
        {
            try
            {
                // Load pending approvals count
                var pendingCount = await _context.MinhChungs
                    .AsNoTracking()
                    .Where(mc => mc.TrangThai == TrangThaiMinhChung.ChoDuyet)
                    .CountAsync();
                lblPendingCount.Text = pendingCount.ToString();

                // Load processed files count
                var processedCount = await _context.MinhChungs
                    .AsNoTracking()
                    .Where(mc => mc.TrangThai == TrangThaiMinhChung.DaDuyet || 
                                mc.TrangThai == TrangThaiMinhChung.BiTuChoi)
                    .CountAsync();
                lblProcessedCount.Text = processedCount.ToString();

                // Load deadline info
                var currentAcademicYear = await _context.NamHocs
                    .AsNoTracking()
                    .Where(nh => nh.MaNH == _currentNamHoc)
                    .FirstOrDefaultAsync();
                
                if (currentAcademicYear != null)
                {
                    lblDeadlineInfo.Text = currentAcademicYear.DenNgay.ToString("dd/MM/yyyy");
                }

                // System status
                lblSystemStatusInfo.Text = "Hoạt động";
                lblSystemStatusInfo.ForeColor = Color.White;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureUIBasedOnRole()
        {
            // Configure UI based on user role hierarchy
            switch (_currentUser.VaiTro)
            {
                case UserRoles.ADMIN:
                    // Super Admin: Full access to all modules
                    break;
                    
                case UserRoles.GIAOVU:
                    // Giáo vụ: Manage student data, import students, limited system config
                    btnSystemConfig.Enabled = false; // No access to system config
                    break;
                    
                case UserRoles.DOANTRUONG:
                    // Đoàn Trường: Approve school-level, view all reports, manage Đoàn Khoa accounts
                    btnSystemConfig.Enabled = false; // No system config
                    break;
                    
                case UserRoles.DOANKHOA:
                    // Đoàn Khoa: Approve faculty-level, manage CVHT accounts, faculty reports
                    btnSystemConfig.Enabled = false; // No system config
                    break;
                    
                case UserRoles.CVHT:
                    // Cố vấn Học tập: Approve class-level only, import class students
                    btnUserManagement.Enabled = true; // Can import students for their class
                    btnReportsStats.Enabled = false; // Limited reporting
                    btnSystemConfig.Enabled = false; // No system config
                    break;
                    
                case UserRoles.DOANTP:
                    // Đoàn Thành phố: View reports, manage Đoàn Trường accounts
                    btnApprovalCenter.Enabled = false; // Read-only approval view
                    btnSystemConfig.Enabled = false;
                    break;
                    
                case UserRoles.DOANTU:
                    // Đoàn Trung ương: View high-level reports, manage Đoàn TP accounts
                    btnApprovalCenter.Enabled = false; // Read-only approval view
                    btnSystemConfig.Enabled = false;
                    break;
                    
                default:
                    // Other roles: Minimal access
                    btnUserManagement.Enabled = false;
                    btnReportsStats.Enabled = false;
                    btnSystemConfig.Enabled = false;
                    break;
            }
        }

        private void InitializeFilters()
        {
            // Initialize status filter
            cmbStatusFilter.Items.Clear();
            cmbStatusFilter.Items.Add("Tất cả");
            cmbStatusFilter.Items.Add("Chờ duyệt");
            cmbStatusFilter.Items.Add("Đã duyệt");
            cmbStatusFilter.Items.Add("Từ chối");
            cmbStatusFilter.Items.Add("Cần bổ sung");
            cmbStatusFilter.SelectedIndex = 0;

            // Initialize department filter
            cmbDepartmentFilter.Items.Clear();
            cmbDepartmentFilter.Items.Add("Tất cả đơn vị");
            // Add departments from database
            LoadDepartments();

            // Initialize criteria filter
            cmbCriteriaFilter.Items.Clear();
            cmbCriteriaFilter.Items.Add("Tất cả tiêu chí");
            LoadCriteria();

            // Initialize report filters
            InitializeReportFilters();
        }

        private async void LoadDepartments()
        {
            try
            {
                var departments = await _context.Khoas
                    .AsNoTracking()
                    .Select(k => k.TenKhoa)
                    .Distinct()
                    .ToListAsync();
                foreach (var dept in departments)
                {
                    cmbDepartmentFilter.Items.Add(dept);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading departments: {ex.Message}");
            }
        }

        private async void LoadCriteria()
        {
            try
            {
                var criteria = await _context.TieuChis
                    .AsNoTracking()
                    .Select(tc => tc.TenTieuChi)
                    .ToListAsync();
                foreach (var criterion in criteria)
                {
                    cmbCriteriaFilter.Items.Add(criterion);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading criteria: {ex.Message}");
            }
        }

        private void InitializeReportFilters()
        {
            // Report type filter
            cmbReportType.Items.Clear();
            cmbReportType.Items.Add("Danh sách sinh viên đạt danh hiệu");
            cmbReportType.Items.Add("Thống kê theo tiêu chí");
            cmbReportType.Items.Add("Báo cáo tổng hợp");
            cmbReportType.Items.Add("Tiến độ xét duyệt");
            cmbReportType.SelectedIndex = 0;

            // Report level filter
            cmbReportLevel.Items.Clear();
            cmbReportLevel.Items.Add("Cấp Trường");
            cmbReportLevel.Items.Add("Cấp Khoa");
            cmbReportLevel.Items.Add("Cấp Lớp");
            cmbReportLevel.SelectedIndex = 0;

            // Set default date range
            dateTimePickerFrom.Value = new DateTime(DateTime.Now.Year, 1, 1);
            dateTimePickerTo.Value = DateTime.Now;
        }

        private string GetRoleDisplayName(string role)
        {
            return role switch
            {
                // Nhóm Quản trị Hệ thống
                UserRoles.ADMIN => "Quản trị viên Tối cao",
                UserRoles.GIAOVU => "Giáo vụ",
                
                // Nhóm Xét duyệt & Quản lý Nghiệp vụ (từ thấp đến cao)
                UserRoles.CVHT => "Cố vấn Học tập",
                UserRoles.DOANKHOA => "BCH Đoàn Khoa",
                UserRoles.DOANTRUONG => "BCH Đoàn Trường",
                UserRoles.DOANTP => "BCH Đoàn Thành phố",
                UserRoles.DOANTU => "BCH Đoàn Trung ương",
                
                // Nhóm Người tham gia
                UserRoles.SINHVIEN => "Sinh viên",
                
                _ => "Người dùng"
            };
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            // Update current date time
            lblCurrentDateTime.Text = DateTime.Now.ToString("dddd, dd/MM/yyyy HH:mm");
            
            // Refresh dashboard data if on dashboard module
            if (panelDashboardModule.Visible)
            {
                // Avoid Task.Run() to prevent multi-threading DbContext issues
                await LoadDashboardData();
            }
        }

        #region Navigation Events

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            ShowDashboardModule();
            UpdateNavigationButtons(btnDashboard);
        }

        private async void btnApprovalCenter_Click(object sender, EventArgs e)
        {
            try
            {
                // Stop timer to avoid concurrent DbContext operations
                var timerWasEnabled = _refreshTimer?.Enabled ?? false;
                if (_refreshTimer != null)
                {
                    _refreshTimer.Stop();
                }
                
                // Wait longer for any pending database operations to complete
                await Task.Delay(500);
                
                // Force UI to process all pending messages
                for (int i = 0; i < 5; i++)
                {
                    Application.DoEvents();
                    await Task.Delay(50);
                }
                
                var reviewForm = new StudentProfileReviewForm(_context, _currentUser);
                reviewForm.ShowDialog();
                
                // Restart timer if it was running
                if (timerWasEnabled && _refreshTimer != null)
                {
                    _refreshTimer.Start();
                }
                
                // Refresh dashboard after review
                await LoadDashboardData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở form xét duyệt: {ex.Message}\n\nChi tiết: {ex.InnerException?.Message}", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUserManagement_Click(object sender, EventArgs e)
        {
            ShowUserManagementModule();
            UpdateNavigationButtons(btnUserManagement);
        }

        private void btnReportsStats_Click(object sender, EventArgs e)
        {
            ShowReportsModule();
            UpdateNavigationButtons(btnReportsStats);
        }

        private void btnSystemConfig_Click(object sender, EventArgs e)
        {
            ShowSystemConfigModule();
            UpdateNavigationButtons(btnSystemConfig);
        }

        private void UpdateNavigationButtons(Button activeButton)
        {
            // Reset all buttons
            btnDashboard.BackColor = Color.FromArgb(52, 73, 94);
            btnApprovalCenter.BackColor = Color.FromArgb(52, 73, 94);
            btnUserManagement.BackColor = Color.FromArgb(52, 73, 94);
            btnReportsStats.BackColor = Color.FromArgb(52, 73, 94);
            btnSystemConfig.BackColor = Color.FromArgb(52, 73, 94);

            // Highlight active button
            activeButton.BackColor = Color.FromArgb(41, 128, 185);
        }

        #endregion

        #region Module Display Methods

        private void ShowDashboardModule()
        {
            HideAllModules();
            panelDashboardModule.Visible = true;
            Task.Run(async () => await LoadDashboardData());
        }

        private void ShowApprovalModule()
        {
            HideAllModules();
            panelApprovalModule.Visible = true;
            LoadApprovalData();
        }

        private void ShowUserManagementModule()
        {
            HideAllModules();
            panelUserManagementModule.Visible = true;
            LoadUserManagementData();
        }

        private void ShowReportsModule()
        {
            HideAllModules();
            panelReportsModule.Visible = true;
            LoadReportsData();
        }

        private void ShowSystemConfigModule()
        {
            HideAllModules();
            panelSystemConfigModule.Visible = true;
            LoadSystemConfigData();
        }

        private void HideAllModules()
        {
            panelDashboardModule.Visible = false;
            panelApprovalModule.Visible = false;
            panelUserManagementModule.Visible = false;
            panelReportsModule.Visible = false;
            panelSystemConfigModule.Visible = false;
        }

        #endregion

        #region Approval Module Methods

        private async void LoadApprovalData()
        {
            try
            {
                var query = _context.MinhChungs
                    .AsNoTracking()
                    .Include(mc => mc.SinhVien)
                    .Include(mc => mc.TieuChi)
                    .Where(mc => mc.MaNH == _currentNamHoc);

                // Apply filters
                if (cmbStatusFilter.SelectedIndex > 0)
                {
                    var status = GetStatusFromFilter(cmbStatusFilter.SelectedItem?.ToString() ?? "");
                    query = query.Where(mc => mc.TrangThai == status);
                }

                if (!string.IsNullOrEmpty(txtSearchStudent.Text))
                {
                    var searchText = txtSearchStudent.Text.ToLower();
                    query = query.Where(mc => mc.SinhVien.MaSV.ToLower().Contains(searchText) ||
                                            mc.SinhVien.HoTen.ToLower().Contains(searchText));
                }

                var approvals = await query.ToListAsync();

                dataGridViewApprovals.Rows.Clear();
                foreach (var approval in approvals)
                {
                    var row = new DataGridViewRow();
                    row.CreateCells(dataGridViewApprovals);
                    row.Cells[0].Value = approval.SinhVien?.MaSV ?? "";
                    row.Cells[1].Value = approval.SinhVien?.HoTen ?? "";
                    row.Cells[2].Value = approval.TieuChi?.TenTieuChi ?? "";
                    row.Cells[3].Value = approval.NgayNop.ToString("dd/MM/yyyy");
                    row.Cells[4].Value = GetStatusDisplayName(approval.TrangThai);
                    row.Tag = approval;
                    dataGridViewApprovals.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu xét duyệt: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private TrangThaiMinhChung GetStatusFromFilter(string filterText)
        {
            return filterText switch
            {
                "Chờ duyệt" => TrangThaiMinhChung.ChoDuyet,
                "Đã duyệt" => TrangThaiMinhChung.DaDuyet,
                "Từ chối" => TrangThaiMinhChung.BiTuChoi,
                "Cần bổ sung" => TrangThaiMinhChung.CanBoSung,
                _ => TrangThaiMinhChung.ChoDuyet
            };
        }

        private string GetStatusDisplayName(TrangThaiMinhChung status)
        {
            return status switch
            {
                TrangThaiMinhChung.ChoDuyet => "Chờ duyệt",
                TrangThaiMinhChung.DaDuyet => "Đã duyệt",
                TrangThaiMinhChung.BiTuChoi => "Từ chối",
                TrangThaiMinhChung.CanBoSung => "Cần bổ sung",
                _ => "Không xác định"
            };
        }

        private void dataGridViewApprovals_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewApprovals.Columns["colActions"].Index)
            {
                var selectedRow = dataGridViewApprovals.Rows[e.RowIndex];
                var evidence = selectedRow.Tag as MinhChung;
                if (evidence != null)
                {
                    LoadEvidenceDetails(evidence);
                }
            }
        }

        private async void LoadEvidenceDetails(MinhChung evidence)
        {
            try
            {
                _selectedEvidence = evidence;
                
                // Load full evidence with related data
                var fullEvidence = await _context.MinhChungs
                    .AsNoTracking()
                    .Include(mc => mc.SinhVien)
                        .ThenInclude(sv => sv.Lop)
                            .ThenInclude(l => l.Khoa)
                    .Include(mc => mc.TieuChi)
                    .FirstOrDefaultAsync(mc => mc.MaMC == evidence.MaMC);

                if (fullEvidence != null)
                {
                    // Display student info
                    lblStudentDetails.Text = $"Sinh viên: {fullEvidence.SinhVien?.HoTen}\n" +
                                           $"MSSV: {fullEvidence.SinhVien?.MaSV}\n" +
                                           $"Lớp: {fullEvidence.SinhVien?.Lop?.TenLop}\n" +
                                           $"Khoa: {fullEvidence.SinhVien?.Lop?.Khoa?.TenKhoa}\n" +
                                           $"Tiêu chí: {fullEvidence.TieuChi?.TenTieuChi}\n" +
                                           $"Ngày nộp: {fullEvidence.NgayNop.ToString("dd/MM/yyyy HH:mm")}";

                    // Load evidence file
                    LoadEvidenceFile(fullEvidence);
                    
                    // Enable/disable approval buttons based on status
                    var canApprove = fullEvidence.TrangThai == TrangThaiMinhChung.ChoDuyet;
                    btnApprove.Enabled = canApprove;
                    btnReject.Enabled = canApprove;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải chi tiết minh chứng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadEvidenceFile(MinhChung evidence)
        {
            try
            {
                if (!string.IsNullOrEmpty(evidence.DuongDanFile) && File.Exists(evidence.DuongDanFile))
                {
                    var extension = Path.GetExtension(evidence.DuongDanFile).ToLower();
                    if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".bmp")
                    {
                        pictureBoxEvidence.Image = Image.FromFile(evidence.DuongDanFile);
                    }
                    else
                    {
                        // For non-image files, show file info
                        pictureBoxEvidence.Image = null;
                        lblEvidenceTitle.Text = $"File: {evidence.TenFile}\nLoại: {extension}\nKích thước: {evidence.KichThuocFile} bytes";
                    }
                }
                else
                {
                    pictureBoxEvidence.Image = null;
                    lblEvidenceTitle.Text = "Không tìm thấy file minh chứng";
                }
            }
            catch (Exception ex)
            {
                pictureBoxEvidence.Image = null;
                lblEvidenceTitle.Text = $"Lỗi tải file: {ex.Message}";
            }
        }

        private async void btnApprove_Click(object sender, EventArgs e)
        {
            if (_selectedEvidence == null) return;

            try
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn duyệt minh chứng này?", 
                                           "Xác nhận duyệt", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    _selectedEvidence.TrangThai = TrangThaiMinhChung.DaDuyet;
                    _selectedEvidence.NgayDuyet = DateTime.Now;
                    _selectedEvidence.NguoiDuyet = _currentUser.UserId;
                    
                    await _context.SaveChangesAsync();
                    
                    MessageBox.Show("Đã duyệt minh chứng thành công!", "Thành công", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    LoadApprovalData(); // Refresh the list
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi duyệt minh chứng: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnReject_Click(object sender, EventArgs e)
        {
            if (_selectedEvidence == null) return;

            if (string.IsNullOrWhiteSpace(txtRejectionReason.Text))
            {
                MessageBox.Show("Vui lòng nhập lý do từ chối!", "Cảnh báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var result = MessageBox.Show("Bạn có chắc chắn muốn từ chối minh chứng này?", 
                                           "Xác nhận từ chối", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    _selectedEvidence.TrangThai = TrangThaiMinhChung.BiTuChoi;
                    _selectedEvidence.NgayDuyet = DateTime.Now;
                    _selectedEvidence.NguoiDuyet = _currentUser.UserId;
                    _selectedEvidence.LyDoTuChoi = txtRejectionReason.Text;
                    
                    await _context.SaveChangesAsync();
                    
                    MessageBox.Show("Đã từ chối minh chứng!", "Thành công", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    txtRejectionReason.Clear();
                    LoadApprovalData(); // Refresh the list
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi từ chối minh chứng: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnApplyFilters_Click(object sender, EventArgs e)
        {
            LoadApprovalData();
        }

        private void btnClearFilters_Click(object sender, EventArgs e)
        {
            cmbStatusFilter.SelectedIndex = 0;
            cmbDepartmentFilter.SelectedIndex = 0;
            cmbCriteriaFilter.SelectedIndex = 0;
            txtSearchStudent.Clear();
            LoadApprovalData();
        }

        #endregion

        #region User Management Module Methods

        private async void LoadUserManagementData()
        {
            try
            {
                var users = await _context.Users
                    .AsNoTracking()
                    .ToListAsync();

                dataGridViewUsers.Rows.Clear();
                foreach (var user in users)
                {
                    var row = new DataGridViewRow();
                    row.CreateCells(dataGridViewUsers);
                    row.Cells[0].Value = user.UserId;
                    row.Cells[1].Value = user.HoTen;
                    row.Cells[2].Value = GetRoleDisplayName(user.VaiTro);
                    row.Cells[3].Value = "Chưa xác định";
                    row.Cells[4].Value = user.TrangThai ? "Hoạt động" : "Vô hiệu hóa";
                    row.Tag = user;
                    dataGridViewUsers.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu người dùng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewUsers.Columns["colUserActions"].Index)
            {
                var selectedRow = dataGridViewUsers.Rows[e.RowIndex];
                var user = selectedRow.Tag as User;
                if (user != null)
                {
                    // Open user edit form
                    var form = new UserManagementForm(_context, _currentUser, user);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadUserManagementData();
                    }
                }
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            var form = new UserManagementForm(_context, _currentUser);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadUserManagementData();
                MessageBox.Show("Thêm người dùng mới thành công!", "Thành công", 
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnImportStudents_Click(object sender, EventArgs e)
        {
            // Check permission
            if (_currentUser.VaiTro != UserRoles.GIAOVU && _currentUser.VaiTro != UserRoles.CVHT)
            {
                MessageBox.Show("Bạn không có quyền import sinh viên!", "Từ chối truy cập", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var importForm = new ImportStudentsForm(_context, _currentUser);
            if (importForm.ShowDialog() == DialogResult.OK)
            {
                // Refresh user list after successful import
                LoadUserManagementData();
                MessageBox.Show("Import sinh viên thành công!", "Thành công", 
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExportUsers_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng xuất danh sách người dùng sẽ được triển khai", "Thông báo", 
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Reports Module Methods

        private void LoadReportsData()
        {
            // Initialize chart
            InitializeChart();
            
            // Load sample data
            LoadSampleReportData();
        }

        private void InitializeChart()
        {
            chartStatistics.Series.Clear();
            chartStatistics.ChartAreas.Clear();
            
            var chartArea = new ChartArea("MainArea");
            chartStatistics.ChartAreas.Add(chartArea);
            
            var series = new Series("Statistics");
            series.ChartType = SeriesChartType.Column;
            chartStatistics.Series.Add(series);
        }

        private void LoadSampleReportData()
        {
            // Sample data for demonstration
            chartStatistics.Series["Statistics"].Points.AddXY("Học tập tốt", 85);
            chartStatistics.Series["Statistics"].Points.AddXY("Đạo đức tốt", 92);
            chartStatistics.Series["Statistics"].Points.AddXY("Thể lực tốt", 78);
            chartStatistics.Series["Statistics"].Points.AddXY("Tình nguyện tốt", 65);
            chartStatistics.Series["Statistics"].Points.AddXY("Hội nhập tốt", 73);
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng tạo báo cáo sẽ được triển khai", "Thông báo", 
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng xuất Excel sẽ được triển khai", "Thông báo", 
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng xuất PDF sẽ được triển khai", "Thông báo", 
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region System Configuration Module Methods

        private async void LoadSystemConfigData()
        {
            await LoadAcademicYears();
            await LoadCriteriaConfig();
        }

        private async Task LoadAcademicYears()
        {
            try
            {
                var academicYears = await _context.NamHocs
                    .AsNoTracking()
                    .ToListAsync();

                dataGridViewAcademicYears.DataSource = academicYears;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải năm học: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadCriteriaConfig()
        {
            try
            {
                var criteria = await _context.TieuChis
                    .AsNoTracking()
                    .ToListAsync();

                dataGridViewCriteria.DataSource = criteria;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải tiêu chí: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddAcademicYear_Click(object sender, EventArgs e)
        {
            var form = new AcademicYearForm(_context);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadAcademicYears();
                MessageBox.Show("Thêm năm học mới thành công!", "Thành công", 
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEditAcademicYear_Click(object sender, EventArgs e)
        {
            if (dataGridViewAcademicYears.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn năm học cần chỉnh sửa!", "Cảnh báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var selectedRow = dataGridViewAcademicYears.SelectedRows[0];
            var maNH = selectedRow.Cells["MaNH"].Value.ToString();
            var namHoc = _context.NamHocs
                .AsNoTracking()
                .FirstOrDefault(nh => nh.MaNH == maNH);
            
            if (namHoc != null)
            {
                var form = new AcademicYearForm(_context, namHoc);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadAcademicYears();
                    MessageBox.Show("Cập nhật năm học thành công!", "Thành công", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private async void btnDeleteAcademicYear_Click(object sender, EventArgs e)
        {
            if (dataGridViewAcademicYears.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn năm học cần xóa!", "Cảnh báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var selectedRow = dataGridViewAcademicYears.SelectedRows[0];
            var maNH = selectedRow.Cells["MaNH"].Value.ToString();
            var tenNH = selectedRow.Cells["TenNH"].Value.ToString();
            
            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa năm học '{tenNH}'?\n\n" +
                "Lưu ý: Điều này có thể ảnh hưởng đến dữ liệu liên quan.",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            
            if (result == DialogResult.Yes)
            {
                try
                {
                    var namHoc = await _context.NamHocs.FirstOrDefaultAsync(nh => nh.MaNH == maNH);
                    if (namHoc != null)
                    {
                        _context.NamHocs.Remove(namHoc);
                        await _context.SaveChangesAsync();
                        
                        LoadAcademicYears();
                        MessageBox.Show("Xóa năm học thành công!", "Thành công", 
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể xóa năm học: {ex.Message}", "Lỗi", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAddCriteria_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng thêm tiêu chí đã bị vô hiệu hóa.", "Thông báo", 
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEditCriteria_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng chỉnh sửa tiêu chí đã bị vô hiệu hóa.", "Thông báo", 
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDeleteCriteria_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng xóa tiêu chí đã bị vô hiệu hóa.", "Thông báo", 
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Header Events

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                await LoadDashboardData();
                
                // Refresh current module
                if (panelApprovalModule.Visible)
                    LoadApprovalData();
                else if (panelUserManagementModule.Visible)
                    LoadUserManagementData();
                else if (panelReportsModule.Visible)
                    LoadReportsData();
                else if (panelSystemConfigModule.Visible)
                    LoadSystemConfigData();
                
                MessageBox.Show("Đã làm mới dữ liệu!", "Thông báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi làm mới dữ liệu: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận đăng xuất", 
                                       MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                this.Hide();
                var loginForm = new Login(_context, _studentService, _userService);
                loginForm.ShowDialog();
            }
        }

        #endregion
    }
}