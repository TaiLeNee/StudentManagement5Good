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
            var selectedRole = comboBoxRole.SelectedItem?.ToString();

            // 1. Ẩn tất cả các control gán đơn vị
            lblThanhPho.Visible = false;
            cmbThanhPho.Visible = false;
            lblTruong.Visible = false;
            cmbTruong.Visible = false;
            lblKhoa.Visible = false;
            cmbKhoa.Visible = false;
            lblLop.Visible = false;
            cmbLop.Visible = false;

            // 2. Chỉ hiển thị control phù hợp với vai trò được chọn
            switch (selectedRole)
            {
                case UserRoles.DOANTP:
                    lblThanhPho.Visible = true;
                    cmbThanhPho.Visible = true;
                    LoadThanhPhos(); // Tải danh sách TP
                    break;

                case UserRoles.DOANTRUONG:
                    lblTruong.Visible = true;
                    cmbTruong.Visible = true;
                    LoadTruongs(); // Tải danh sách Trường
                    break;

                case UserRoles.DOANKHOA:
                case UserRoles.GIAOVU: // Giáo vụ cũng quản lý theo Khoa
                    lblKhoa.Visible = true;
                    cmbKhoa.Visible = true;
                    LoadKhoas(); // Tải danh sách Khoa
                    break;

                case UserRoles.CVHT:
                case UserRoles.SINHVIEN:
                    lblLop.Visible = true;
                    cmbLop.Visible = true;
                    LoadLops(); // Tải danh sách Lớp
                    break;

                case UserRoles.DOANTU:
                    // Không cần gán đơn vị cụ thể (quản lý toàn quốc)
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

                cmbThanhPho.DataSource = thanhPhos;
                cmbThanhPho.DisplayMember = "TenThanhPho";
                cmbThanhPho.ValueMember = "MaTP"; // QUAN TRỌNG
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
                var query = _context.Truongs.AsNoTracking().Where(t => t.TrangThai == true);

                // LỌC THEO PHẠM VI: Nếu người tạo là DOANTP, chỉ tải Trường thuộc TP của họ
                if (_currentUser.VaiTro == UserRoles.DOANTP)
                {
                    query = query.Where(t => t.MaTP == _currentUser.MaTP);
                }

                var truongs = await query.OrderBy(t => t.TenTruong).ToListAsync();

                cmbTruong.DataSource = truongs;
                cmbTruong.DisplayMember = "TenTruong";
                cmbTruong.ValueMember = "MaTruong"; // QUAN TRỌNG
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
                var query = _context.Khoas.AsNoTracking().Where(k => k.TrangThai == true);

                // LỌC THEO PHẠM VI: Nếu người tạo là DOANTRUONG, chỉ tải Khoa thuộc Trường của họ
                if (_currentUser.VaiTro == UserRoles.DOANTRUONG)
                {
                    query = query.Where(k => k.MaTruong == _currentUser.MaTruong);
                }

                var khoas = await query.OrderBy(k => k.TenKhoa).ToListAsync();

                cmbKhoa.DataSource = khoas;
                cmbKhoa.DisplayMember = "TenKhoa";
                cmbKhoa.ValueMember = "MaKhoa"; // QUAN TRỌNG
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
                var query = _context.Lops.AsNoTracking();

                // LỌC THEO PHẠM VI: Nếu người tạo là DOANKHOA hoặc GIAOVU, chỉ tải Lớp thuộc Khoa của họ
                if (_currentUser.VaiTro == UserRoles.DOANKHOA || _currentUser.VaiTro == UserRoles.GIAOVU)
                {
                    query = query.Where(l => l.MaKhoa == _currentUser.MaKhoa);
                }
                // Nếu người tạo là CVHT, chỉ tải Lớp của họ
                else if (_currentUser.VaiTro == UserRoles.CVHT)
                {
                    query = query.Where(l => l.MaLop == _currentUser.MaLop);
                }

                var lops = await query.OrderBy(l => l.TenLop).ToListAsync();

                cmbLop.DataSource = lops;
                cmbLop.DisplayMember = "TenLop";
                cmbLop.ValueMember = "MaLop"; // QUAN TRỌNG
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách lớp: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                
                // 1. Khởi tạo tất cả các mã là null
                string? selectedClass = null;
                string? selectedKhoa = null;
                string? selectedTruong = null;
                string? selectedThanhPho = null;
                
                // 2. Chỉ đọc giá trị từ ComboBox đang hiển thị (Visible)
                if (cmbLop.Visible && cmbLop.SelectedValue != null)
                {
                    selectedClass = cmbLop.SelectedValue?.ToString();
                }
                if (cmbKhoa.Visible && cmbKhoa.SelectedValue != null)
                {
                    selectedKhoa = cmbKhoa.SelectedValue?.ToString();
                }
                if (cmbTruong.Visible && cmbTruong.SelectedValue != null)
                {
                    selectedTruong = cmbTruong.SelectedValue?.ToString();
                }
                if (cmbThanhPho.Visible && cmbThanhPho.SelectedValue != null)
                {
                    selectedThanhPho = cmbThanhPho.SelectedValue?.ToString();
                }
                
                if (_isEditMode && _user != null)
                {
                    // Update existing user
                    _user.HoTen = txtHoTen.Text.Trim();
                    _user.Email = txtEmail.Text.Trim();
                    _user.SoDienThoai = txtPhone.Text.Trim();
                    _user.VaiTro = comboBoxRole.SelectedItem?.ToString() ?? "";
                    _user.MaLop = selectedClass;     // Gán giá trị (sẽ là null nếu cmbLop bị ẩn)
                    _user.MaKhoa = selectedKhoa;    // Gán giá trị
                    _user.MaTruong = selectedTruong;  // Gán giá trị
                    _user.MaTP = selectedThanhPho;    // Gán giá trị
                    
                    // Tự động gán CapQuanLy dựa trên VaiTro
                    _user.CapQuanLy = _user.VaiTro switch
                    {
                        UserRoles.CVHT => ManagementLevels.LOP,
                        UserRoles.DOANKHOA => ManagementLevels.KHOA,
                        UserRoles.DOANTRUONG => ManagementLevels.TRUONG,
                        UserRoles.DOANTP => ManagementLevels.TP,
                        UserRoles.DOANTU => ManagementLevels.TU,
                        _ => null
                    };
                    
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
                    var selectedRole = comboBoxRole.SelectedItem?.ToString() ?? "";
                    var newUser = new User
                    {
                        UserId = Guid.NewGuid().ToString(),  // Tự động tạo GUID
                        Username = txtUsername.Text.Trim(),
                        Password = BC.HashPassword(txtPassword.Text),
                        HoTen = txtHoTen.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        SoDienThoai = txtPhone.Text.Trim(),
                        VaiTro = selectedRole,
                        MaLop = selectedClass,
                        MaKhoa = selectedKhoa,
                        MaTruong = selectedTruong,
                        MaTP = selectedThanhPho,
                        
                        // Tự động gán CapQuanLy dựa trên VaiTro
                        CapQuanLy = selectedRole switch
                        {
                            UserRoles.CVHT => ManagementLevels.LOP,
                            UserRoles.DOANKHOA => ManagementLevels.KHOA,
                            UserRoles.DOANTRUONG => ManagementLevels.TRUONG,
                            UserRoles.DOANTP => ManagementLevels.TP,
                            UserRoles.DOANTU => ManagementLevels.TU,
                            _ => null
                        },
                        
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

