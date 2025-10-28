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

namespace StudentManagement5Good.Winform
{
    public partial class KhoaForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Khoa? _existingKhoa;
        private bool _isEditMode = false;

        public KhoaForm(IServiceProvider serviceProvider, Khoa? existingKhoa = null)
        {
            _serviceProvider = serviceProvider;
            _existingKhoa = existingKhoa;
            _isEditMode = existingKhoa != null;
            
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Set form title based on mode
            this.Text = _isEditMode ? "Chỉnh sửa Khoa" : "Thêm Khoa mới";
            lblTitle.Text = _isEditMode ? "Chỉnh sửa thông tin Khoa" : "Thêm thông tin Khoa mới";

            // Load Truong data
            LoadTruongs();

            // If edit mode, populate fields
            if (_isEditMode && _existingKhoa != null)
            {
                PopulateFields();
            }
        }

        private async void LoadTruongs()
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                var truongs = await context.Truongs
                    .AsNoTracking()
                    .Where(t => t.TrangThai == true)
                    .OrderBy(t => t.TenTruong)
                    .ToListAsync();

                cmbTruong.DataSource = truongs;
                cmbTruong.DisplayMember = "TenTruong";
                cmbTruong.ValueMember = "MaTruong";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách trường: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateFields()
        {
            if (_existingKhoa == null) return;

            txtMaKhoa.Text = _existingKhoa.MaKhoa;
            txtTenKhoa.Text = _existingKhoa.TenKhoa;
            txtTruongKhoa.Text = _existingKhoa.TruongKhoa ?? "";
            txtBiThuDoanKhoa.Text = _existingKhoa.BiThuDoanKhoa ?? "";
            txtSoDienThoai.Text = _existingKhoa.SoDienThoai ?? "";
            txtEmail.Text = _existingKhoa.Email ?? "";
            txtGhiChu.Text = _existingKhoa.GhiChu ?? "";
            chkTrangThai.Checked = _existingKhoa.TrangThai;
            
            if (_existingKhoa.NgayThanhLap.HasValue)
            {
                dtpNgayThanhLap.Value = _existingKhoa.NgayThanhLap.Value;
                chkNgayThanhLap.Checked = true;
            }

            // Set Truong
            cmbTruong.SelectedValue = _existingKhoa.MaTruong;

            // Disable MaKhoa field in edit mode
            txtMaKhoa.Enabled = false;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                Khoa khoa;
                if (_isEditMode)
                {
                    // Update existing
                    khoa = await context.Khoas.FindAsync(_existingKhoa!.MaKhoa);
                    if (khoa == null)
                    {
                        MessageBox.Show("Không tìm thấy khoa cần cập nhật!", "Lỗi", 
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    // Create new
                    khoa = new Khoa();
                    context.Khoas.Add(khoa);
                }

                // Set properties
                khoa.MaKhoa = txtMaKhoa.Text.Trim();
                khoa.TenKhoa = txtTenKhoa.Text.Trim();
                khoa.TruongKhoa = string.IsNullOrWhiteSpace(txtTruongKhoa.Text) ? null : txtTruongKhoa.Text.Trim();
                khoa.BiThuDoanKhoa = string.IsNullOrWhiteSpace(txtBiThuDoanKhoa.Text) ? null : txtBiThuDoanKhoa.Text.Trim();
                khoa.SoDienThoai = string.IsNullOrWhiteSpace(txtSoDienThoai.Text) ? null : txtSoDienThoai.Text.Trim();
                khoa.Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim();
                khoa.GhiChu = string.IsNullOrWhiteSpace(txtGhiChu.Text) ? null : txtGhiChu.Text.Trim();
                khoa.TrangThai = chkTrangThai.Checked;
                khoa.MaTruong = cmbTruong.SelectedValue?.ToString() ?? "";

                if (chkNgayThanhLap.Checked)
                {
                    khoa.NgayThanhLap = dtpNgayThanhLap.Value.Date;
                }
                else
                {
                    khoa.NgayThanhLap = null;
                }

                if (_isEditMode)
                {
                    khoa.NgayCapNhat = DateTime.Now;
                }

                await context.SaveChangesAsync();

                MessageBox.Show(_isEditMode ? "Cập nhật khoa thành công!" : "Thêm khoa mới thành công!", 
                              "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lưu dữ liệu: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtMaKhoa.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Khoa!", "Cảnh báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaKhoa.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTenKhoa.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên Khoa!", "Cảnh báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKhoa.Focus();
                return false;
            }

            if (cmbTruong.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Trường!", "Cảnh báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTruong.Focus();
                return false;
            }

            // Validate email format if provided
            if (!string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                if (!IsValidEmail(txtEmail.Text))
                {
                    MessageBox.Show("Email không đúng định dạng!", "Cảnh báo", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtEmail.Focus();
                    return false;
                }
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void chkNgayThanhLap_CheckedChanged(object sender, EventArgs e)
        {
            dtpNgayThanhLap.Enabled = chkNgayThanhLap.Checked;
        }
    }
}

