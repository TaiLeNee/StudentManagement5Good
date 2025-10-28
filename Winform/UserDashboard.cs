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

        // Constructor c? �? backward compatibility
        public UserDashboard(StudentManagementDbContext context, IUserService userService, 
                           IStudentService studentService, User currentUser)
            : this(StudentManagement5GoodTempp.Program.ServiceProvider, userService, studentService, currentUser)
        {
        }

        private async void UserDashboard_Load(object sender, EventArgs e)
        {
            try
            {
                // Áp dụng patch tiếng Việt ngay sau khi load
                UserDashboardVietnamesePatch.ApplyVietnamesePatch(this);
                
                await LoadCurrentAcademicYear();
                await LoadDashboardData();
                ConfigureUIBasedOnRole();
                ShowDashboardModule();
                
                // Áp dụng lại patch sau khi load dữ liệu
                UserDashboardVietnamesePatch.ApplyVietnamesePatch(this);
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
            this.Text = $"H? th?ng Qu?n l? Sinh vi�n 5 T?t - {_currentUser.HoTen}";
            
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
                lblSystemStatusInfo.Text = "Ho?t �?ng";
                lblSystemStatusInfo.ForeColor = Color.White;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"L?i t?i d? li?u: {ex.Message}", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    // Gi�o v?: Manage student data, import students, limited system config
                    btnSystemConfig.Enabled = false; // No access to system config
                    break;
                    
                case UserRoles.DOANTRUONG:
                    // �o�n Tr�?ng: Approve school-level, view all reports, manage �o�n Khoa accounts
                    btnSystemConfig.Enabled = false; // No system config
                    break;
                    
                case UserRoles.DOANKHOA:
                    // �o�n Khoa: Approve faculty-level, manage CVHT accounts, faculty reports
                    btnSystemConfig.Enabled = false; // No system config
                    break;
                    
                case UserRoles.CVHT:
                    // C? v?n H?c t?p: Approve class-level only, import class students
                    btnUserManagement.Enabled = true; // Can import students for their class
                    btnReportsStats.Enabled = false; // Limited reporting
                    btnSystemConfig.Enabled = false; // No system config
                    break;
                    
                case UserRoles.DOANTP:
                    // �o�n Th�nh ph?: View reports, manage �o�n Tr�?ng accounts
                    btnApprovalCenter.Enabled = false; // Read-only approval view
                    btnSystemConfig.Enabled = false;
                    break;
                    
                case UserRoles.DOANTU:
                    // �o�n Trung ��ng: View high-level reports, manage �o�n TP accounts
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
            cmbStatusFilter.Items.Add("T?t c?");
            cmbStatusFilter.Items.Add("Ch? duy�t");
            cmbStatusFilter.Items.Add("�? duy�t");
            cmbStatusFilter.Items.Add("T? ch?i");
            cmbStatusFilter.Items.Add("C?n b? sung");
            cmbStatusFilter.SelectedIndex = 0;

            // Initialize department filter
            cmbDepartmentFilter.Items.Clear();
            cmbDepartmentFilter.Items.Add("T?t c? ��n v?");
            // Add departments from database
            LoadDepartments();

            // Initialize criteria filter
            cmbCriteriaFilter.Items.Clear();
            cmbCriteriaFilter.Items.Add("T?t c? ti�u ch�");
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
            cmbReportType.Items.Add("Danh s�ch sinh vi�n �?t danh hi?u");
            cmbReportType.Items.Add("Th?ng k� theo ti�u ch�");
            cmbReportType.Items.Add("B�o c�o t?ng h?p");
            cmbReportType.Items.Add("Ti�n �? x�t duy?t");
            cmbReportType.SelectedIndex = 0;

            // Report level filter
            cmbReportLevel.Items.Clear();
            cmbReportLevel.Items.Add("C?p Tr�?ng");
            cmbReportLevel.Items.Add("C?p Khoa");
            cmbReportLevel.Items.Add("C?p L?p");
            cmbReportLevel.SelectedIndex = 0;

            // Set default date range
            dateTimePickerFrom.Value = new DateTime(DateTime.Now.Year, 1, 1);
            dateTimePickerTo.Value = DateTime.Now;
        }

        private string GetRoleDisplayName(string role)
        {
            return role switch
            {
                // Nh�m Qu?n tr? H? th?ng
                UserRoles.ADMIN => "Qu?n tr? vi�n T?i cao",
                UserRoles.GIAOVU => "Gi�o v?",
                
                // Nh�m X�t duy?t & Qu?n l? Nghi?p v? (t? th?p �?n cao)
                UserRoles.CVHT => "C? v?n H?c t?p",
                UserRoles.DOANKHOA => "BCH �o�n Khoa",
                UserRoles.DOANTRUONG => "BCH �o�n Tr�?ng",
                UserRoles.DOANTP => "BCH �o�n Th�nh ph?",
                UserRoles.DOANTU => "BCH �o�n Trung ��ng",
                
                // Nh�m Ng�?i tham gia
                UserRoles.SINHVIEN => "Sinh vi�n",
                
                _ => "Ng�?i d�ng"
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
                MessageBox.Show($"L?i m? form x�t duy?t: {ex.Message}\n\nChi ti�t: {ex.InnerException?.Message}", "L?i",
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
                MessageBox.Show($"L?i t?i d? li?u x�t duy?t: {ex.Message}", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private TrangThaiMinhChung GetStatusFromFilter(string filterText)
        {
            return filterText switch
            {
                "Ch? duy�t" => TrangThaiMinhChung.ChoDuyet,
                "�? duy�t" => TrangThaiMinhChung.DaDuyet,
                "T? ch?i" => TrangThaiMinhChung.BiTuChoi,
                "C?n b? sung" => TrangThaiMinhChung.CanBoSung,
                _ => TrangThaiMinhChung.ChoDuyet
            };
        }

        private string GetStatusDisplayName(TrangThaiMinhChung status)
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
                    lblStudentDetails.Text = $"Sinh vi�n: {fullEvidence.SinhVien?.HoTen}\n" +
                                           $"MSSV: {fullEvidence.SinhVien?.MaSV}\n" +
                                           $"L?p: {fullEvidence.SinhVien?.Lop?.TenLop}\n" +
                                           $"Khoa: {fullEvidence.SinhVien?.Lop?.Khoa?.TenKhoa}\n" +
                                           $"Ti�u ch�: {fullEvidence.TieuChi?.TenTieuChi}\n" +
                                           $"Ng�y n?p: {fullEvidence.NgayNop.ToString("dd/MM/yyyy HH:mm")}";

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
                MessageBox.Show($"L?i t?i chi ti?t minh ch?ng: {ex.Message}", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        lblEvidenceTitle.Text = $"File: {evidence.TenFile}\nLo?i: {extension}\nK�ch th�?c: {evidence.KichThuocFile} bytes";
                    }
                }
                else
                {
                    pictureBoxEvidence.Image = null;
                    lblEvidenceTitle.Text = "Kh�ng t?m th?y file minh ch?ng";
                }
            }
            catch (Exception ex)
            {
                pictureBoxEvidence.Image = null;
                lblEvidenceTitle.Text = $"L?i t?i file: {ex.Message}";
            }
        }

        private async void btnApprove_Click(object sender, EventArgs e)
        {
            if (_selectedEvidence == null) return;

            try
            {
                var result = MessageBox.Show("B?n c� ch?c ch?n mu?n duy?t minh ch?ng n�y?", 
                                           "X�c nh?n duy?t", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
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
                        
                        MessageBox.Show("�? duy?t minh ch?ng th�nh c�ng!", "Th�nh c�ng", 
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        LoadApprovalData(); // Refresh the list
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"L?i khi duy?t minh ch?ng: {ex.Message}", "L?i", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnReject_Click(object sender, EventArgs e)
        {
            if (_selectedEvidence == null) return;

            if (string.IsNullOrWhiteSpace(txtRejectionReason.Text))
            {
                MessageBox.Show("Vui l?ng nh?p l? do t? ch?i!", "C?nh b?o", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var result = MessageBox.Show("B?n c� ch?c ch?n mu?n t? ch?i minh ch?ng n�y?", 
                                           "X�c nh?n t? ch?i", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
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
                        
                        MessageBox.Show("�? t? ch?i minh ch?ng!", "Th�nh c�ng", 
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        txtRejectionReason.Clear();
                        LoadApprovalData(); // Refresh the list
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"L?i khi t? ch?i minh ch?ng: {ex.Message}", "L?i", 
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

                var users = await context.Users
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
                    row.Cells[3].Value = "Ch�a x�c �?nh";
                    row.Cells[4].Value = user.TrangThai ? "Ho?t �?ng" : "V� hi?u h�a";
                    row.Tag = user;
                    dataGridViewUsers.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"L?i t?i d? li?u ng�?i d�ng: {ex.Message}", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    using var scope = _serviceProvider.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();
                    var form = new UserManagementForm(context, _currentUser, user);
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        LoadUserManagementData();
                    }
                }
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();
            var form = new UserManagementForm(context, _currentUser);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadUserManagementData();
                MessageBox.Show("Th�m ng�?i d�ng m?i th�nh c�ng!", "Th�nh c�ng", 
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnImportStudents_Click(object sender, EventArgs e)
        {
            // Check permission
            if (_currentUser.VaiTro != UserRoles.GIAOVU && _currentUser.VaiTro != UserRoles.CVHT)
            {
                MessageBox.Show("B?n kh�ng c� quy?n import sinh vi�n!", "T? ch?i truy c?p", 
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
                MessageBox.Show("Import sinh vi�n th�nh c�ng!", "Th�nh c�ng", 
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExportUsers_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ch?c n�ng xu?t danh s�ch ng�?i d�ng s? ��?c tri?n khai", "Th�ng b�o", 
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
            chartStatistics.Series["Statistics"].Points.AddXY("H?c t?p t?t", 85);
            chartStatistics.Series["Statistics"].Points.AddXY("�?o �?c t?t", 92);
            chartStatistics.Series["Statistics"].Points.AddXY("Th? l?c t?t", 78);
            chartStatistics.Series["Statistics"].Points.AddXY("T?nh nguy�n t?t", 65);
            chartStatistics.Series["Statistics"].Points.AddXY("H?i nh?p t?t", 73);
        }

        private void btnGenerateReport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ch?c n�ng t?o b�o c�o s? ��?c tri?n khai", "Th�ng b�o", 
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ch?c n�ng xu?t Excel s? ��?c tri?n khai", "Th�ng b�o", 
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ch?c n�ng xu?t PDF s? ��?c tri?n khai", "Th�ng b�o", 
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
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                var academicYears = await context.NamHocs
                    .AsNoTracking()
                    .ToListAsync();

                dataGridViewAcademicYears.DataSource = academicYears;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"L?i t?i n�m h?c: {ex.Message}", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show($"L?i t?i ti�u ch�: {ex.Message}", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Th�m n�m h?c m?i th�nh c�ng!", "Th�nh c�ng", 
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEditAcademicYear_Click(object sender, EventArgs e)
        {
            if (dataGridViewAcademicYears.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui l?ng ch?n n�m h?c c?n ch?nh s?a!", "C?nh b?o", 
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
                    MessageBox.Show("C?p nh?t n�m h?c th�nh c�ng!", "Th�nh c�ng", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private async void btnDeleteAcademicYear_Click(object sender, EventArgs e)
        {
            if (dataGridViewAcademicYears.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui l?ng ch?n n�m h?c c?n x�a!", "C?nh b?o", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var selectedRow = dataGridViewAcademicYears.SelectedRows[0];
            var maNH = selectedRow.Cells["MaNH"].Value?.ToString();
            var tenNH = selectedRow.Cells["TenNH"].Value?.ToString();
            
            var result = MessageBox.Show(
                $"B?n c� ch?c ch?n mu?n x�a n�m h?c '{tenNH}'?\n\n" +
                "L�u ?: �i?u n�y c� th? ?nh h�?ng �?n d? li?u li�n quan.",
                "X�c nh?n x�a",
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
                        MessageBox.Show("X�a n�m h?c th�nh c�ng!", "Th�nh c�ng", 
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Kh�ng th? x�a n�m h?c: {ex.Message}", "L?i", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAddCriteria_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ch?c n�ng th�m ti�u ch� �? b? v� hi?u h�a.", "Th�ng b�o", 
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEditCriteria_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ch?c n�ng ch?nh s?a ti�u ch� �? b? v� hi?u h�a.", "Th�ng b�o", 
                          MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDeleteCriteria_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Ch?c n�ng x�o ti�u ch� �? b? v� hi?u h�a.", "Th�ng b�o", 
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
                
                MessageBox.Show("�? l�m m?i d? li?u!", "Th�ng b�o", 
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"L?i l�m m?i d? li?u: {ex.Message}", "L?i", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("B?n c� ch?c ch?n mu?n ��ng xu?t?", "X�c nh�n ��ng xu?t", 
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