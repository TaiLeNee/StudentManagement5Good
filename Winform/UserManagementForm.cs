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
        private readonly IDbContextFactory<StudentManagementDbContext> _contextFactory;
        private readonly User _currentUser;
        private User? _user;
        private bool _isEditMode;

        public UserManagementForm(IDbContextFactory<StudentManagementDbContext> contextFactory, User currentUser, User? user = null)
        {
            _contextFactory = contextFactory;
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

            lblTitle.Text = _isEditMode ? "📝 Chỉnh sửa Người dùng" : "➕ Thêm Người dùng mới";
            lblSubtitle.Text = _isEditMode
                ? "Cập nhật thông tin tài khoản người dùng"
                : "Nhập thông tin chi tiết cho tài khoản người dùng mới";
            btnSave.Text = _isEditMode ? "💾 Cập nhật" : "✓ Thêm mới";

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

        private void ComboBoxRole_SelectedIndexChanged(object? sender, EventArgs e)
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
                using var context = _contextFactory.CreateDbContext();
                var query = context.ThanhPhos.AsNoTracking().Where(tp => tp.TrangThai == true);

                // Chỉ DOANTU và ADMIN mới cần thấy tất cả TP
                // Các vai trò khác không cần tải mục này
                if (_currentUser.VaiTro != UserRoles.DOANTU && _currentUser.VaiTro != UserRoles.ADMIN)
                {
                    query = query.Where(tp => false); // Không tải gì cả
                }

                var thanhPhos = await query.OrderBy(tp => tp.TenThanhPho).ToListAsync();
                cmbThanhPho.DataSource = thanhPhos;
                cmbThanhPho.DisplayMember = "TenThanhPho";
                cmbThanhPho.ValueMember = "MaTP";
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
                using var context = _contextFactory.CreateDbContext(); // Dùng Factory
                var query = context.Truongs.AsNoTracking().Where(t => t.TrangThai == true);

                // LỌC THEO PHẠM VI: Chỉ tải Trường thuộc Thành phố của người dùng DOANTP
                if (_currentUser.VaiTro == UserRoles.DOANTP)
                {
                    query = query.Where(t => t.MaTP == _currentUser.MaTP);
                }
                // ADMIN/DOANTU có thể thấy tất cả

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
                using var context = _contextFactory.CreateDbContext(); // Dùng Factory
                var query = context.Khoas.AsNoTracking().Where(k => k.TrangThai == true);

                // LỌC THEO PHẠM VI: Chỉ tải Khoa thuộc Trường của người dùng DOANTRUONG
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
                using var context = _contextFactory.CreateDbContext();
                var query = context.Lops.AsNoTracking().OrderBy(l => l.TenLop);

                // LỌC THEO PHẠM VI: Chỉ tải Lớp thuộc Khoa của người dùng DOANKHOA/CVHT/GIAOVU
                if (_currentUser.VaiTro == UserRoles.DOANKHOA || _currentUser.VaiTro == UserRoles.GIAOVU || _currentUser.VaiTro == UserRoles.CVHT)
                {
                    query = query.Where(l => l.MaKhoa == _currentUser.MaKhoa).OrderBy(l => l.TenLop);
                }

                var lops = await query.ToListAsync();
                cmbLop.DataSource = lops;
                cmbLop.DisplayMember = "TenLop";
                cmbLop.ValueMember = "MaLop";
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
                using var context = _contextFactory.CreateDbContext();
                var existsUserId = context.Users.Any(u => u.UserId == txtUserId.Text.Trim());
                if (existsUserId)
                {
                    MessageBox.Show("User ID đã tồn tại!", "Lỗi",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUserId.Focus();
                    return false;
                }

                var existsUsername = context.Users.Any(u => u.Username == txtUsername.Text.Trim());
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

                using var context = _contextFactory.CreateDbContext(); // Dùng Factory cho toàn bộ thao tác

                // 1. Khởi tạo các mã Kế thừa từ người tạo (QUAN TRỌNG)
                string? selectedRole = comboBoxRole.SelectedItem?.ToString();

                // Validate role is selected (should already be checked in ValidateInput)
                if (string.IsNullOrEmpty(selectedRole))
                {
                    MessageBox.Show("Vui lòng chọn vai trò!", "Lỗi",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string? capQuanLy = GetCapQuanLyFromRole(selectedRole);
                string? selectedClass = null;
                string? selectedKhoa = _currentUser.MaKhoa;     // Kế thừa
                string? selectedTruong = _currentUser.MaTruong;   // Kế thừa
                string? selectedThanhPho = _currentUser.MaTP;     // Kế thừa

                // 2. Ghi đè (Gán mới) mã đơn vị cấp dưới trực tiếp từ ComboBox
                if (cmbLop.Visible)
                {
                    selectedClass = cmbLop.SelectedValue?.ToString();
                }
                if (cmbKhoa.Visible)
                {
                    selectedKhoa = cmbKhoa.SelectedValue?.ToString();
                }
                if (cmbTruong.Visible)
                {
                    selectedTruong = cmbTruong.SelectedValue?.ToString();
                }
                if (cmbThanhPho.Visible)
                {
                    selectedThanhPho = cmbThanhPho.SelectedValue?.ToString();
                }

                if (_isEditMode && _user != null)
                {
                    // Update existing user
                    var userToUpdate = await context.Users.FindAsync(_user.UserId);
                    if (userToUpdate == null) throw new Exception("Không tìm thấy người dùng để cập nhật.");

                    userToUpdate.HoTen = txtHoTen.Text.Trim();
                    userToUpdate.Email = txtEmail.Text.Trim();
                    userToUpdate.SoDienThoai = txtPhone.Text.Trim();
                    userToUpdate.VaiTro = selectedRole;
                    userToUpdate.TrangThai = checkBoxActive.Checked;
                    userToUpdate.NgayCapNhat = DateTime.Now;

                    // Gán lại mã phân cấp
                    userToUpdate.MaLop = selectedClass;
                    userToUpdate.MaKhoa = selectedKhoa;
                    userToUpdate.MaTruong = selectedTruong;
                    userToUpdate.MaTP = selectedThanhPho;
                    userToUpdate.CapQuanLy = capQuanLy;

                    if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                    {
                        userToUpdate.Password = BC.HashPassword(txtPassword.Text);
                    }

                    context.Users.Update(userToUpdate);
                }
                else
                {
                    // Add new user
                    var newUser = new User
                    {
                        UserId = Guid.NewGuid().ToString("N").Substring(0, 20), // Tạo ID ngắn hơn
                        Username = txtUsername.Text.Trim(),
                        Password = BC.HashPassword(txtPassword.Text),
                        HoTen = txtHoTen.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        SoDienThoai = txtPhone.Text.Trim(),
                        TrangThai = checkBoxActive.Checked,
                        NgayTao = DateTime.Now,

                        // Gán vai trò và mã phân cấp
                        VaiTro = selectedRole,
                        MaLop = selectedClass,
                        MaKhoa = selectedKhoa,
                        MaTruong = selectedTruong,
                        MaTP = selectedThanhPho,
                        CapQuanLy = capQuanLy
                    };
                    context.Users.Add(newUser);
                }

                await context.SaveChangesAsync();

                MessageBox.Show(
                    _isEditMode ? "Cập nhật người dùng thành công!" : "Thêm người dùng mới thành công!",
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                var errorMessage = $"Lỗi khi lưu dữ liệu:\n\n{ex.Message}";
                if (ex.InnerException != null) errorMessage += $"\n\nChi tiết: {ex.InnerException.Message}";
                MessageBox.Show(errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        // Helper function to get CapQuanLy from VaiTro
        private string? GetCapQuanLyFromRole(string? role)
        {
            return role switch
            {
                UserRoles.CVHT => ManagementLevels.LOP,
                UserRoles.DOANKHOA => ManagementLevels.KHOA,
                UserRoles.DOANTRUONG => ManagementLevels.TRUONG,
                UserRoles.DOANTP => ManagementLevels.TP,
                UserRoles.DOANTU => ManagementLevels.TU,
                _ => null
            };
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

