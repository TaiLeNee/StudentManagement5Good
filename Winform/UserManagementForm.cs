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
            
            // Define which roles can be created by each role
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
                        UserRoles.DOANTU
                    });
                    break;
                    
                case UserRoles.DOANTRUONG:
                    // Đoàn Trường can manage Đoàn Khoa
                    comboBoxRole.Items.Add(UserRoles.DOANKHOA);
                    break;
                    
                case UserRoles.DOANKHOA:
                    // Đoàn Khoa can manage CVHT
                    comboBoxRole.Items.Add(UserRoles.CVHT);
                    break;
                    
                case UserRoles.DOANTP:
                    // Đoàn TP can manage Đoàn Trường
                    comboBoxRole.Items.Add(UserRoles.DOANTRUONG);
                    break;
                    
                case UserRoles.DOANTU:
                    // Đoàn TU can manage Đoàn TP
                    comboBoxRole.Items.Add(UserRoles.DOANTP);
                    break;
                    
                default:
                    // Others cannot create users
                    comboBoxRole.Enabled = false;
                    break;
            }
            
            if (comboBoxRole.Items.Count > 0)
                comboBoxRole.SelectedIndex = 0;
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
            
            comboBoxClass.SelectedIndex = 0;
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
                
                string? selectedClass = null;
                if (comboBoxClass.SelectedIndex > 0)
                {
                    var classText = comboBoxClass.SelectedItem?.ToString() ?? "";
                    selectedClass = classText.Split('-')[0].Trim();
                }
                
                if (_isEditMode && _user != null)
                {
                    // Update existing user
                    _user.HoTen = txtHoTen.Text.Trim();
                    _user.Email = txtEmail.Text.Trim();
                    _user.SoDienThoai = txtPhone.Text.Trim();
                    _user.VaiTro = comboBoxRole.SelectedItem?.ToString();
                    _user.MaLop = selectedClass;
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

