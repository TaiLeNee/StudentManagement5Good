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
using System.Windows.Forms.DataVisualization.Charting;
using StudentManagement5Good.Winform;

namespace StudentManagement5Good.Winform
{
    public partial class UserDashboard : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IUserService _userService;
        private readonly IStudentService _studentService;
        private readonly User _currentUser;
        private string? _currentNamHoc;
        private MinhChung? _selectedEvidence;
        private System.Windows.Forms.Timer? _refreshTimer;

        public UserDashboard(IServiceProvider serviceProvider, IUserService userService, 
                           IStudentService studentService, User currentUser)
        {
            _serviceProvider = serviceProvider;
            _userService = userService;
            _studentService = studentService;
            _currentUser = currentUser;
            
            InitializeComponent();
            InitializeUserInterface();
        }

        // Constructor cũ để backward compatibility
        public UserDashboard(StudentManagementDbContext context, IUserService userService, 
                           IStudentService studentService, User currentUser)
            : this(StudentManagement5GoodTempp.Program.ServiceProvider, userService, studentService, currentUser)
        {
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
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

            var currentYear = await context.NamHocs
                .AsNoTracking()
                .Where(nh => nh.TuNgay <= DateTime.Now && nh.DenNgay >= DateTime.Now)
                .FirstOrDefaultAsync();
            
            _currentNamHoc = currentYear?.MaNH ?? DateTime.Now.Year.ToString();
        }

        private async Task LoadDashboardData()
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                // Load pending approvals count
                var pendingCount = await context.MinhChungs
                    .AsNoTracking()
                    .Where(mc => mc.TrangThai == TrangThaiMinhChung.ChoDuyet)
                    .CountAsync();
                lblPendingCount.Text = pendingCount.ToString();

                // Load processed files count
                var processedCount = await context.MinhChungs
                    .AsNoTracking()
                    .Where(mc => mc.TrangThai == TrangThaiMinhChung.DaDuyet || 
                                mc.TrangThai == TrangThaiMinhChung.BiTuChoi)
                    .CountAsync();
                lblProcessedCount.Text = processedCount.ToString();

                // Load deadline info
                var currentAcademicYear = await context.NamHocs
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
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                var departments = await context.Khoas
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
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                var criteria = await context.TieuChis
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
            cmbReportLevel.Items.Add("Tất cả");
            cmbReportLevel.Items.Add("Lớp");
            cmbReportLevel.Items.Add("Khoa");
            cmbReportLevel.Items.Add("Trường");
            cmbReportLevel.Items.Add("Thành phố");
            cmbReportLevel.Items.Add("Trung ương");
            cmbReportLevel.SelectedIndex = 0;

            // Set default date range (if these controls exist)
            try
            {
                if (dateTimePickerFrom != null)
                    dateTimePickerFrom.Value = new DateTime(DateTime.Now.Year, 1, 1);
                if (dateTimePickerTo != null)
                    dateTimePickerTo.Value = DateTime.Now;
            }
            catch { } // Ignore if controls don't exist
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
                
                var reviewForm = new StudentProfileReviewForm(_serviceProvider, _currentUser);
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
            btnDashboard.BackColor = Color.FromArgb(255, 251, 162);
            btnApprovalCenter.BackColor = Color.FromArgb(255, 251, 162);
            btnUserManagement.BackColor = Color.FromArgb(255, 251, 162);
            btnReportsStats.BackColor = Color.FromArgb(255, 251, 162);
            btnSystemConfig.BackColor = Color.FromArgb(255, 251, 162);

            // Highlight active button
            activeButton.BackColor = Color.FromArgb(250, 203, 2);
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
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                var query = context.MinhChungs
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
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                _selectedEvidence = evidence;
                
                // Load full evidence with related data
                var fullEvidence = await context.MinhChungs
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
                    using var scope = _serviceProvider.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                    var evidence = await context.MinhChungs.FindAsync(_selectedEvidence.MaMC);
                    if (evidence != null)
                    {
                        evidence.TrangThai = TrangThaiMinhChung.DaDuyet;
                        evidence.NgayDuyet = DateTime.Now;
                        evidence.NguoiDuyet = _currentUser.UserId;
                        
                        await context.SaveChangesAsync();
                        
                        MessageBox.Show("Đã duyệt minh chứng thành công!", "Thành công", 
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        LoadApprovalData(); // Refresh the list
                    }
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
                    using var scope = _serviceProvider.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                    var evidence = await context.MinhChungs.FindAsync(_selectedEvidence.MaMC);
                    if (evidence != null)
                    {
                        evidence.TrangThai = TrangThaiMinhChung.BiTuChoi;
                        evidence.NgayDuyet = DateTime.Now;
                        evidence.NguoiDuyet = _currentUser.UserId;
                        evidence.LyDoTuChoi = txtRejectionReason.Text;
                        
                        await context.SaveChangesAsync();
                        
                        MessageBox.Show("Đã từ chối minh chứng!", "Thành công", 
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        txtRejectionReason.Clear();
                        LoadApprovalData(); // Refresh the list
                    }
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
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                // Apply hierarchical filtering based on current user's role
                var users = await GetUsersBasedOnRole(context);

                dataGridViewUsers.Rows.Clear();
                foreach (var user in users)
                {
                    var row = new DataGridViewRow();
                    row.CreateCells(dataGridViewUsers);
                    row.Cells[0].Value = user.UserId;
                    row.Cells[1].Value = user.HoTen;
                    row.Cells[2].Value = GetRoleDisplayName(user.VaiTro);
                    row.Cells[3].Value = GetUserDepartment(user);
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

        private async Task<List<User>> GetUsersBasedOnRole(StudentManagementDbContext context)
        {
            return _currentUser.VaiTro switch
            {
                UserRoles.ADMIN => await context.Users
                    .AsNoTracking()
                    .Where(u => u.UserId != _currentUser.UserId) // Exclude self
                    .OrderBy(u => u.HoTen)
                    .ToListAsync(),

                UserRoles.DOANTU => await context.Users
                    .AsNoTracking()
                    .Where(u => u.VaiTro == UserRoles.DOANTP)
                    .OrderBy(u => u.HoTen)
                    .ToListAsync(),

                UserRoles.DOANTP => await context.Users
                    .AsNoTracking()
                    .Where(u => u.VaiTro == UserRoles.DOANTRUONG && u.MaTP == _currentUser.MaTP)
                    .OrderBy(u => u.HoTen)
                    .ToListAsync(),

                UserRoles.DOANTRUONG => await context.Users
                    .AsNoTracking()
                    .Where(u => u.VaiTro == UserRoles.DOANKHOA && u.MaTruong == _currentUser.MaTruong)
                    .OrderBy(u => u.HoTen)
                    .ToListAsync(),

                UserRoles.DOANKHOA => await context.Users
                    .AsNoTracking()
                    .Where(u => u.VaiTro == UserRoles.CVHT && u.MaKhoa == _currentUser.MaKhoa)
                    .OrderBy(u => u.HoTen)
                    .ToListAsync(),

                UserRoles.GIAOVU => await context.Users
                    .AsNoTracking()
                    .Where(u => u.VaiTro == UserRoles.SINHVIEN && u.MaTruong == _currentUser.MaTruong)
                    .OrderBy(u => u.HoTen)
                    .ToListAsync(),

                UserRoles.CVHT => await context.Users
                    .AsNoTracking()
                    .Where(u => u.VaiTro == UserRoles.SINHVIEN && u.MaLop == _currentUser.MaLop)
                    .OrderBy(u => u.HoTen)
                    .ToListAsync(),

                _ => new List<User>() // No access for other roles
            };
        }

        private string GetUserDepartment(User user)
        {
            if (!string.IsNullOrEmpty(user.MaLop))
                return $"Lớp: {user.MaLop}";
            if (!string.IsNullOrEmpty(user.MaKhoa))
                return $"Khoa: {user.MaKhoa}";
            if (!string.IsNullOrEmpty(user.MaTruong))
                return $"Trường: {user.MaTruong}";
            if (!string.IsNullOrEmpty(user.MaTP))
                return $"TP: {user.MaTP}";
            return "Chưa xác định";
        }

        private void dataGridViewUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewUsers.Columns["colUserActions"].Index)
            {
                var selectedRow = dataGridViewUsers.Rows[e.RowIndex];
                var user = selectedRow.Tag as User;
                if (user != null)
                {
                    // Check if current user has permission to edit this user
                    if (!CanEditUser(user))
                    {
                        MessageBox.Show("Bạn không có quyền chỉnh sửa người dùng này!", "Từ chối truy cập", 
                                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Open user edit form
                    using var scope = _serviceProvider.CreateScope();
                    var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<StudentManagementDbContext>>();
                    var form = new UserManagementForm(contextFactory, _currentUser, user);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadUserManagementData();
                    }
                }
            }
        }

        private bool CanEditUser(User targetUser)
        {
            return _currentUser.VaiTro switch
            {
                UserRoles.ADMIN => true, // Admin can edit everyone except themselves
                UserRoles.DOANTU => targetUser.VaiTro == UserRoles.DOANTP,
                UserRoles.DOANTP => targetUser.VaiTro == UserRoles.DOANTRUONG && targetUser.MaTP == _currentUser.MaTP,
                UserRoles.DOANTRUONG => targetUser.VaiTro == UserRoles.DOANKHOA && targetUser.MaTruong == _currentUser.MaTruong,
                UserRoles.DOANKHOA => targetUser.VaiTro == UserRoles.CVHT && targetUser.MaKhoa == _currentUser.MaKhoa,
                UserRoles.GIAOVU => targetUser.VaiTro == UserRoles.SINHVIEN && targetUser.MaTruong == _currentUser.MaTruong,
                UserRoles.CVHT => targetUser.VaiTro == UserRoles.SINHVIEN && targetUser.MaLop == _currentUser.MaLop,
                _ => false
            };
        }

        private bool CanDeleteUser(User targetUser)
        {
            // Same logic as CanEditUser for now, but can be customized
            return CanEditUser(targetUser);
        }

        private async void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (dataGridViewUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn người dùng cần xóa!", "Cảnh báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dataGridViewUsers.SelectedRows[0];
            var user = selectedRow.Tag as User;
            
            if (user == null) return;

            // Check permission
            if (!CanDeleteUser(user))
            {
                MessageBox.Show("Bạn không có quyền xóa người dùng này!", "Từ chối truy cập", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa người dùng '{user.HoTen}'?\n\n" +
                "Lưu ý: Điều này có thể ảnh hưởng đến dữ liệu liên quan.",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                    var toDelete = await context.Users.FindAsync(user.UserId);
                    if (toDelete != null)
                    {
                        context.Users.Remove(toDelete);
                        await context.SaveChangesAsync();
                        
                        MessageBox.Show("Xóa người dùng thành công!", "Thành công", 
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        LoadUserManagementData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể xóa người dùng: {ex.Message}", "Lỗi", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            // Check if current user can create users
            if (!CanCreateUsers())
            {
                MessageBox.Show("Bạn không có quyền tạo người dùng mới!", "Từ chối truy cập", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var scope = _serviceProvider.CreateScope();
            var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<StudentManagementDbContext>>();
            var form = new UserManagementForm(contextFactory, _currentUser);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadUserManagementData();
                MessageBox.Show("Thêm người dùng mới thành công!", "Thành công", 
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool CanCreateUsers()
        {
            return _currentUser.VaiTro switch
            {
                UserRoles.ADMIN => true,
                UserRoles.DOANTU => true,
                UserRoles.DOANTP => true,
                UserRoles.DOANTRUONG => true,
                UserRoles.DOANKHOA => true,
                UserRoles.GIAOVU => true,
                UserRoles.CVHT => true,
                _ => false
            };
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
            
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();
            var importForm = new ImportStudentsForm(context, _currentUser);
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
            // Initialize chart if exists
            if (chartStatistics != null)
            {
                try
                {
                    InitializeChart();
                }
                catch (Exception ex)
                {
                    // Ignore chart initialization errors
                    System.Diagnostics.Debug.WriteLine($"Chart init error: {ex.Message}");
                }
            }
            
            // Load thống kê ban đầu
            Task.Run(async () => await LoadReportStatistics());
        }

        private void InitializeChart()
        {
            if (chartStatistics == null) return;
            
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
            await LoadKhoasConfig();
            await LoadLopsConfig();
            await LoadTruongsConfig();
            await LoadThanhPhosConfig();
        }

        private async Task LoadAcademicYears()
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                var academicYears = await context.NamHocs
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
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                var criteria = await context.TieuChis
                    .AsNoTracking()
                    .ToListAsync();

                dataGridViewCriteria.DataSource = criteria;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải tiêu chí: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadKhoasConfig()
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                var khoas = await context.Khoas
                    .AsNoTracking()
                    .Include(k => k.Truong)
                    .OrderBy(k => k.TenKhoa)
                    .ToListAsync();

                dataGridViewKhoas.DataSource = khoas;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách khoa: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadLopsConfig()
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                var lops = await context.Lops
                    .AsNoTracking()
                    .Include(l => l.Khoa)
                    .OrderBy(l => l.TenLop)
                    .ToListAsync();

                dataGridViewLops.DataSource = lops;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách lớp: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadTruongsConfig()
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                var truongs = await context.Truongs
                    .AsNoTracking()
                    .Include(t => t.ThanhPho)
                    .OrderBy(t => t.TenTruong)
                    .ToListAsync();

                dataGridViewTruongs.DataSource = truongs;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách trường: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadThanhPhosConfig()
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                var thanhPhos = await context.ThanhPhos
                    .AsNoTracking()
                    .OrderBy(tp => tp.TenThanhPho)
                    .ToListAsync();

                dataGridViewThanhPhos.DataSource = thanhPhos;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách thành phố: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddAcademicYear_Click(object sender, EventArgs e)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();
            var form = new AcademicYearForm(context);
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
            
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();
            
            var namHoc = context.NamHocs
                .AsNoTracking()
                .FirstOrDefault(nh => nh.MaNH == maNH);
            
            if (namHoc != null)
            {
                var form = new AcademicYearForm(context, namHoc);
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
            var maNH = selectedRow.Cells["MaNH"].Value?.ToString();
            var tenNH = selectedRow.Cells["TenNH"].Value?.ToString();
            
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
                    using var scope = _serviceProvider.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                    var namHoc = await context.NamHocs.FirstOrDefaultAsync(nh => nh.MaNH == maNH);
                    if (namHoc != null)
                    {
                        context.NamHocs.Remove(namHoc);
                        await context.SaveChangesAsync();
                        
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
            var form = new TieuChiForm(_serviceProvider);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadCriteriaConfig();
                MessageBox.Show("Thêm tiêu chí thành công!", "Thành công", 
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEditCriteria_Click(object sender, EventArgs e)
        {
            if (dataGridViewCriteria.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn tiêu chí cần chỉnh sửa!", "Cảnh báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var selectedRow = dataGridViewCriteria.SelectedRows[0];
            var tieuChi = selectedRow.DataBoundItem as TieuChi;
            
            if (tieuChi != null)
            {
                var form = new TieuChiForm(_serviceProvider, tieuChi);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadCriteriaConfig();
                    MessageBox.Show("Cập nhật tiêu chí thành công!", "Thành công", 
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private async void btnDeleteCriteria_Click(object sender, EventArgs e)
        {
            if (dataGridViewCriteria.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn tiêu chí cần xóa!", "Cảnh báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var selectedRow = dataGridViewCriteria.SelectedRows[0];
            var tieuChi = selectedRow.DataBoundItem as TieuChi;
            
            if (tieuChi == null) return;

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa tiêu chí '{tieuChi.TenTieuChi}'?\n\n" +
                "Lưu ý: Điều này có thể ảnh hưởng đến dữ liệu liên quan.",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            
            if (result == DialogResult.Yes)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                    var toDelete = await context.TieuChis.FindAsync(tieuChi.MaTC);
                    if (toDelete != null)
                    {
                        context.TieuChis.Remove(toDelete);
                        await context.SaveChangesAsync();
                        
                        LoadCriteriaConfig();
                        MessageBox.Show("Xóa tiêu chí thành công!", "Thành công", 
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể xóa tiêu chí: {ex.Message}", "Lỗi", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // CRUD Methods for Khoa
        private void btnAddKhoa_Click(object sender, EventArgs e)
        {
            var form = new KhoaForm(_serviceProvider);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadKhoasConfig();
                MessageBox.Show("Thêm khoa thành công!", "Thành công", 
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEditKhoa_Click(object sender, EventArgs e)
        {
            if (dataGridViewKhoas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khoa cần chỉnh sửa!", "Cảnh báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var selectedRow = dataGridViewKhoas.SelectedRows[0];
            var khoa = selectedRow.DataBoundItem as Khoa;
            
            if (khoa != null)
            {
                var form = new KhoaForm(_serviceProvider, khoa);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadKhoasConfig();
                    MessageBox.Show("Cập nhật khoa thành công!", "Thành công", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private async void btnDeleteKhoa_Click(object sender, EventArgs e)
        {
            if (dataGridViewKhoas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khoa cần xóa!", "Cảnh báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var selectedRow = dataGridViewKhoas.SelectedRows[0];
            var khoa = selectedRow.DataBoundItem as Khoa;
            
            if (khoa == null) return;

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa khoa '{khoa.TenKhoa}'?\n\n" +
                "Lưu ý: Điều này có thể ảnh hưởng đến dữ liệu liên quan.",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            
            if (result == DialogResult.Yes)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                    var toDelete = await context.Khoas.FindAsync(khoa.MaKhoa);
                    if (toDelete != null)
                    {
                        context.Khoas.Remove(toDelete);
                        await context.SaveChangesAsync();
                        
                        LoadKhoasConfig();
                        MessageBox.Show("Xóa khoa thành công!", "Thành công", 
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể xóa khoa: {ex.Message}", "Lỗi", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // CRUD Methods for Lop
        private void btnAddLop_Click(object sender, EventArgs e)
        {
            var form = new LopForm(_serviceProvider);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadLopsConfig();
                MessageBox.Show("Thêm lớp thành công!", "Thành công", 
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEditLop_Click(object sender, EventArgs e)
        {
            if (dataGridViewLops.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn lớp cần chỉnh sửa!", "Cảnh báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var selectedRow = dataGridViewLops.SelectedRows[0];
            var lop = selectedRow.DataBoundItem as Lop;
            
            if (lop != null)
            {
                var form = new LopForm(_serviceProvider, lop);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadLopsConfig();
                    MessageBox.Show("Cập nhật lớp thành công!", "Thành công", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private async void btnDeleteLop_Click(object sender, EventArgs e)
        {
            if (dataGridViewLops.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn lớp cần xóa!", "Cảnh báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var selectedRow = dataGridViewLops.SelectedRows[0];
            var lop = selectedRow.DataBoundItem as Lop;
            
            if (lop == null) return;

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa lớp '{lop.TenLop}'?\n\n" +
                "Lưu ý: Điều này có thể ảnh hưởng đến dữ liệu liên quan.",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            
            if (result == DialogResult.Yes)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                    var toDelete = await context.Lops.FindAsync(lop.MaLop);
                    if (toDelete != null)
                    {
                        context.Lops.Remove(toDelete);
                        await context.SaveChangesAsync();
                        
                        LoadLopsConfig();
                        MessageBox.Show("Xóa lớp thành công!", "Thành công", 
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể xóa lớp: {ex.Message}", "Lỗi", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // CRUD Methods for Truong
        private void btnAddTruong_Click(object sender, EventArgs e)
        {
            var form = new TruongForm(_serviceProvider);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadTruongsConfig();
                MessageBox.Show("Thêm trường thành công!", "Thành công", 
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEditTruong_Click(object sender, EventArgs e)
        {
            if (dataGridViewTruongs.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn trường cần chỉnh sửa!", "Cảnh báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var selectedRow = dataGridViewTruongs.SelectedRows[0];
            var truong = selectedRow.DataBoundItem as Truong;
            
            if (truong != null)
            {
                var form = new TruongForm(_serviceProvider, truong);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadTruongsConfig();
                    MessageBox.Show("Cập nhật trường thành công!", "Thành công", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private async void btnDeleteTruong_Click(object sender, EventArgs e)
        {
            if (dataGridViewTruongs.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn trường cần xóa!", "Cảnh báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var selectedRow = dataGridViewTruongs.SelectedRows[0];
            var truong = selectedRow.DataBoundItem as Truong;
            
            if (truong == null) return;

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa trường '{truong.TenTruong}'?\n\n" +
                "Lưu ý: Điều này có thể ảnh hưởng đến dữ liệu liên quan.",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            
            if (result == DialogResult.Yes)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                    var toDelete = await context.Truongs.FindAsync(truong.MaTruong);
                    if (toDelete != null)
                    {
                        context.Truongs.Remove(toDelete);
                        await context.SaveChangesAsync();
                        
                        LoadTruongsConfig();
                        MessageBox.Show("Xóa trường thành công!", "Thành công", 
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể xóa trường: {ex.Message}", "Lỗi", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // CRUD Methods for ThanhPho
        private void btnAddThanhPho_Click(object sender, EventArgs e)
        {
            var form = new ThanhPhoForm(_serviceProvider);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadThanhPhosConfig();
                MessageBox.Show("Thêm thành phố thành công!", "Thành công", 
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEditThanhPho_Click(object sender, EventArgs e)
        {
            if (dataGridViewThanhPhos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn thành phố cần chỉnh sửa!", "Cảnh báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var selectedRow = dataGridViewThanhPhos.SelectedRows[0];
            var thanhPho = selectedRow.DataBoundItem as ThanhPho;
            
            if (thanhPho != null)
            {
                var form = new ThanhPhoForm(_serviceProvider, thanhPho);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadThanhPhosConfig();
                    MessageBox.Show("Cập nhật thành phố thành công!", "Thành công", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private async void btnDeleteThanhPho_Click(object sender, EventArgs e)
        {
            if (dataGridViewThanhPhos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn thành phố cần xóa!", "Cảnh báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var selectedRow = dataGridViewThanhPhos.SelectedRows[0];
            var thanhPho = selectedRow.DataBoundItem as ThanhPho;
            
            if (thanhPho == null) return;

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa thành phố '{thanhPho.TenThanhPho}'?\n\n" +
                "Lưu ý: Điều này có thể ảnh hưởng đến dữ liệu liên quan.",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            
            if (result == DialogResult.Yes)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                    var toDelete = await context.ThanhPhos.FindAsync(thanhPho.MaTP);
                    if (toDelete != null)
                    {
                        context.ThanhPhos.Remove(toDelete);
                        await context.SaveChangesAsync();
                        
                        LoadThanhPhosConfig();
                        MessageBox.Show("Xóa thành phố thành công!", "Thành công", 
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể xóa thành phố: {ex.Message}", "Lỗi", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // TieuChiYeuCau Configuration
        private void btnConfigTieuChiYeuCau_Click(object sender, EventArgs e)
        {
            var form = new TieuChiYeuCauForm(_serviceProvider);
            form.ShowDialog();
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
                var loginForm = new Login(_serviceProvider);
                loginForm.ShowDialog();
            }
        }

        #endregion
    }
}