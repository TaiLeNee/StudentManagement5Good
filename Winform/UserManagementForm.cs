using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using StudentManagement5GoodTempp.DataAccess.Context;
using StudentManagement5GoodTempp.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace StudentManagement5Good.Winform
{
    public partial class UserManagementForm : Form
    {
        private readonly StudentManagementDbContext _context;
        private readonly User _currentUser;
        private User? _user;
        private bool _isEditMode;

        public UserManagementForm(StudentManagementDbContext context, User currentUser, User? user = null)
        {
            _context = context;
            _currentUser = currentUser;
            _user = user;
            _isEditMode = user != null;
            
            InitializeComponent();
            InitializeUI();
            
            if (_isEditMode && _user != null)
            {
                LoadData();
            }
        }

        private void InitializeUI()
        {
            this.Text = _isEditMode ? "Chỉnh sửa Người dùng" : "Thêm Người dùng mới";
            this.Size = new Size(700, 650);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            
            lblTitle.Text = _isEditMode ? "📝 Chỉnh sửa Người dùng" : "➕ Thêm Người dùng mới";
            btnSave.Text = _isEditMode ? "💾 Cập nhật" : "➕ Thêm mới";
            
            // Hide UserId field when creating new user (auto-generated GUID)
            if (!_isEditMode)
            {
                lblUserId.Visible = false;
                txtUserId.Visible = false;
                txtUserId.Text = "Auto-generated"; // Placeholder
            }
            else
            {
                txtUserId.ReadOnly = true; // Cannot edit UserId
            }
            
            // Initialize ComboBox for Roles (based on current user permissions)
            InitializeRoleComboBox();
            
            // Load classes
            LoadClasses();
        }

        private void InitializeRoleComboBox()
        {
            comboBoxRole.Items.Clear();
            
            // Define which roles can be created by each role based on hierarchy
            switch (_currentUser.VaiTro)
            {
                case UserRoles.ADMIN:
                    // Super Admin can create all roles except ADMIN
                    comboBoxRole.Items.AddRange(new string[]
                    {
                        UserRoles.GIAOVU,
                        UserRoles.DOANTRUONG,
                        UserRoles.DOANKHOA,
                        UserRoles.CVHT,
                        UserRoles.DOANTP,
                        UserRoles.DOANTU,
                        UserRoles.SINHVIEN
                    });
                    break;
                    
                case UserRoles.DOANTU:
                    // Đoàn TU can manage Đoàn TP
                    comboBoxRole.Items.Add(UserRoles.DOANTP);
                    break;
                    
                case UserRoles.DOANTP:
                    // Đoàn TP can manage Đoàn Trường
                    comboBoxRole.Items.Add(UserRoles.DOANTRUONG);
                    break;
                    
                case UserRoles.DOANTRUONG:
                    // Đoàn Trường can manage Đoàn Khoa
                    comboBoxRole.Items.Add(UserRoles.DOANKHOA);
                    break;
                    
                case UserRoles.DOANKHOA:
                    // Đoàn Khoa can manage CVHT
                    comboBoxRole.Items.Add(UserRoles.CVHT);
                    break;
                    
                case UserRoles.GIAOVU:
                    // Giáo vụ can create students
                    comboBoxRole.Items.Add(UserRoles.SINHVIEN);
                    break;
                    
                case UserRoles.CVHT:
                    // CVHT can create students for their class
                    comboBoxRole.Items.Add(UserRoles.SINHVIEN);
                    break;
                    
                default:
                    // Others cannot create users
                    comboBoxRole.Enabled = false;
                    break;
            }
            
            if (comboBoxRole.Items.Count > 0)
                comboBoxRole.SelectedIndex = 0;
                
            // Add event handler for role change
            comboBoxRole.SelectedIndexChanged += ComboBoxRole_SelectedIndexChanged;
        }

        private void ComboBoxRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Show/hide additional controls based on selected role
            var selectedRole = comboBoxRole.SelectedItem?.ToString();
            
            // Hide all additional controls first
            lblThanhPho.Visible = false;
            cmbThanhPho.Visible = false;
            lblTruong.Visible = false;
            cmbTruong.Visible = false;
            lblKhoa.Visible = false;
            cmbKhoa.Visible = false;
            lblLop.Visible = false;
            cmbLop.Visible = false;
            
            // Show relevant controls based on role
            switch (selectedRole)
            {
                case UserRoles.DOANTP:
                    lblThanhPho.Visible = true;
                    cmbThanhPho.Visible = true;
                    LoadThanhPhos();
                    break;
                    
                case UserRoles.DOANTRUONG:
                    lblTruong.Visible = true;
                    cmbTruong.Visible = true;
                    LoadTruongs();
                    break;
                    
                case UserRoles.DOANKHOA:
                    lblKhoa.Visible = true;
                    cmbKhoa.Visible = true;
                    LoadKhoas();
                    break;
                    
                case UserRoles.CVHT:
                    lblLop.Visible = true;
                    cmbLop.Visible = true;
                    LoadLops();
                    break;
                    
                case UserRoles.SINHVIEN:
                    lblLop.Visible = true;
                    cmbLop.Visible = true;
                    LoadLops();
                    break;
            }
        }

        private async void LoadThanhPhos()
        {
            try
            {
                var thanhPhos = await _context.ThanhPhos
                    .AsNoTracking()
                    .Where(tp => tp.TrangThai == true)
                    .OrderBy(tp => tp.TenThanhPho)
                    .ToListAsync();

                cmbThanhPho.Items.Clear();
                cmbThanhPho.Items.AddRange(thanhPhos.Select(tp => tp.TenThanhPho).ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách thành phố: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void LoadTruongs()
        {
            try
            {
                var truongs = await _context.Truongs
                    .AsNoTracking()
                    .Where(t => t.TrangThai == true)
                    .OrderBy(t => t.TenTruong)
                    .ToListAsync();

                cmbTruong.Items.Clear();
                cmbTruong.Items.AddRange(truongs.Select(t => t.TenTruong).ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách trường: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void LoadKhoas()
        {
            try
            {
                var khoas = await _context.Khoas
                    .AsNoTracking()
                    .Where(k => k.TrangThai == true)
                    .OrderBy(k => k.TenKhoa)
                    .ToListAsync();

                cmbKhoa.Items.Clear();
                cmbKhoa.Items.AddRange(khoas.Select(k => k.TenKhoa).ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách khoa: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void LoadLops()
        {
            try
            {
                var lops = await _context.Lops
                    .AsNoTracking()
                    .OrderBy(l => l.TenLop)
                    .ToListAsync();

                cmbLop.Items.Clear();
                cmbLop.Items.AddRange(lops.Select(l => l.TenLop).ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách lớp: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadClasses()
        {
            comboBoxClass.Items.Clear();
            comboBoxClass.Items.Add("-- Không chọn --");
            
            var classes = _context.Lops.OrderBy(l => l.TenLop).ToList();
            foreach (var lop in classes)
            {
                comboBoxClass.Items.Add($"{lop.MaLop} - {lop.TenLop}");
            }
        }

        private void LoadData()
        {
            if (_user == null) return;
            
            txtUserId.Text = _user.UserId;
            txtUsername.Text = _user.Username;
            txtHoTen.Text = _user.HoTen;
            txtEmail.Text = _user.Email;
            txtPhone.Text = _user.SoDienThoai;
            
            // Set role
            var roleIndex = comboBoxRole.FindString(_user.VaiTro ?? "");
            if (roleIndex >= 0)
                comboBoxRole.SelectedIndex = roleIndex;
            
            // Set class
            if (!string.IsNullOrEmpty(_user.MaLop))
            {
                for (int i = 0; i < comboBoxClass.Items.Count; i++)
                {
                    if (comboBoxClass.Items[i].ToString()!.StartsWith(_user.MaLop))
                    {
                        comboBoxClass.SelectedIndex = i;
                        break;
                    }
                }
            }
            
            checkBoxActive.Checked = _user.TrangThai;
            
            // Password field
            lblPassword.Text = "Mật khẩu mới (để trống nếu không đổi):";
            txtPassword.Text = "";
        }

        private bool ValidateInput()
        {
            // Skip UserId validation when creating new user (auto-generated GUID)
            // Only validate UserId in edit mode
            if (_isEditMode && string.IsNullOrWhiteSpace(txtUserId.Text))
            {
                MessageBox.Show("Vui lòng nhập User ID!", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUserId.Focus();
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Vui lòng nhập Username!", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập Họ tên!", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập Email!", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }
            
            if (!_isEditMode && string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập Mật khẩu!", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }
            
            if (comboBoxRole.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn Vai trò!", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxRole.Focus();
                return false;
            }
            
            // Check for duplicate UserId or Username
            if (!_isEditMode)
            {
                var existsUserId = _context.Users.Any(u => u.UserId == txtUserId.Text.Trim());
                if (existsUserId)
                {
                    MessageBox.Show("User ID đã tồn tại!", "Lỗi", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUserId.Focus();
                    return false;
                }
                
                var existsUsername = _context.Users.Any(u => u.Username == txtUsername.Text.Trim());
                if (existsUsername)
                {
                    MessageBox.Show("Username đã tồn tại!", "Lỗi", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsername.Focus();
                    return false;
                }
            }
            
            return true;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            
            try
            {
                btnSave.Enabled = false;
                btnCancel.Enabled = false;
                
                // Get selected values for organizational units
                string? selectedClass = null;
                string? selectedKhoa = null;
                string? selectedTruong = null;
                string? selectedThanhPho = null;
                
                if (comboBoxClass.SelectedIndex > 0)
                {
                    var classText = comboBoxClass.SelectedItem?.ToString() ?? "";
                    selectedClass = classText.Split('-')[0].Trim();
                }
                
                if (cmbLop.SelectedIndex >= 0 && !string.IsNullOrEmpty(cmbLop.SelectedItem?.ToString()))
                {
                    var lop = await _context.Lops.FirstOrDefaultAsync(l => l.TenLop == cmbLop.SelectedItem.ToString());
                    selectedClass = lop?.MaLop;
                }
                
                if (cmbKhoa.SelectedIndex >= 0 && !string.IsNullOrEmpty(cmbKhoa.SelectedItem?.ToString()))
                {
                    var khoa = await _context.Khoas.FirstOrDefaultAsync(k => k.TenKhoa == cmbKhoa.SelectedItem.ToString());
                    selectedKhoa = khoa?.MaKhoa;
                }
                
                if (cmbTruong.SelectedIndex >= 0 && !string.IsNullOrEmpty(cmbTruong.SelectedItem?.ToString()))
                {
                    var truong = await _context.Truongs.FirstOrDefaultAsync(t => t.TenTruong == cmbTruong.SelectedItem.ToString());
                    selectedTruong = truong?.MaTruong;
                }
                
                if (cmbThanhPho.SelectedIndex >= 0 && !string.IsNullOrEmpty(cmbThanhPho.SelectedItem?.ToString()))
                {
                    var thanhPho = await _context.ThanhPhos.FirstOrDefaultAsync(tp => tp.TenThanhPho == cmbThanhPho.SelectedItem.ToString());
                    selectedThanhPho = thanhPho?.MaTP;
                }
                
                if (_isEditMode && _user != null)
                {
                    // Update existing user
                    _user.HoTen = txtHoTen.Text.Trim();
                    _user.Email = txtEmail.Text.Trim();
                    _user.SoDienThoai = txtPhone.Text.Trim();
                    _user.VaiTro = comboBoxRole.SelectedItem?.ToString();
                    _user.MaLop = selectedClass;
                    _user.MaKhoa = selectedKhoa;
                    _user.MaTruong = selectedTruong;
                    _user.MaTP = selectedThanhPho;
                    _user.TrangThai = checkBoxActive.Checked;
                    
                    // Update password only if provided
                    if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                    {
                        _user.Password = BC.HashPassword(txtPassword.Text);
                    }
                    
                    _context.Users.Update(_user);
                }
                else
                {
                    // Add new user - Auto generate GUID for UserId
                    var newUser = new User
                    {
                        UserId = Guid.NewGuid().ToString(),  // Tự động tạo GUID
                        Username = txtUsername.Text.Trim(),
                        Password = BC.HashPassword(txtPassword.Text),
                        HoTen = txtHoTen.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        SoDienThoai = txtPhone.Text.Trim(),
                        VaiTro = comboBoxRole.SelectedItem?.ToString(),
                        MaLop = selectedClass,
                        MaKhoa = selectedKhoa,
                        MaTruong = selectedTruong,
                        MaTP = selectedThanhPho,
                        TrangThai = checkBoxActive.Checked,
                        NgayTao = DateTime.Now
                    };
                    
                    _context.Users.Add(newUser);
                }
                
                await _context.SaveChangesAsync();
                
                MessageBox.Show(
                    _isEditMode ? "Cập nhật người dùng thành công!" : "Thêm người dùng mới thành công!",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                // Log chi tiết lỗi để debug
                var errorMessage = $"Lỗi khi lưu dữ liệu:\n\n{ex.Message}";
                if (ex.InnerException != null)
                {
                    errorMessage += $"\n\nChi tiết: {ex.InnerException.Message}";
                }
                
                MessageBox.Show(errorMessage, "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSave.Enabled = true;
                btnCancel.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

