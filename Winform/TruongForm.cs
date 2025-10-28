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
    public partial class TruongForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Truong? _existingTruong;
        private bool _isEditMode = false;

        public TruongForm(IServiceProvider serviceProvider, Truong? existingTruong = null)
        {
            _serviceProvider = serviceProvider;
            _existingTruong = existingTruong;
            _isEditMode = existingTruong != null;
            
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Set form title based on mode
            this.Text = _isEditMode ? "Chỉnh sửa Trường" : "Thêm Trường mới";
            lblTitle.Text = _isEditMode ? "Chỉnh sửa thông tin Trường" : "Thêm thông tin Trường mới";

            // Load ThanhPho data
            LoadThanhPhos();

            // If edit mode, populate fields
            if (_isEditMode && _existingTruong != null)
            {
                PopulateFields();
            }
        }

        private async void LoadThanhPhos()
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                var thanhPhos = await context.ThanhPhos
                    .AsNoTracking()
                    .Where(tp => tp.TrangThai == true)
                    .OrderBy(tp => tp.TenThanhPho)
                    .ToListAsync();

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

        private void PopulateFields()
        {
            if (_existingTruong == null) return;

            txtMaTruong.Text = _existingTruong.MaTruong;
            txtTenTruong.Text = _existingTruong.TenTruong;
            txtTenVietTat.Text = _existingTruong.TenVietTat ?? "";
            txtLoaiTruong.Text = _existingTruong.LoaiTruong ?? "";
            txtHieuTruong.Text = _existingTruong.HieuTruong ?? "";
            txtBiThuDoan.Text = _existingTruong.BiThuDoan ?? "";
            txtSoDienThoai.Text = _existingTruong.SoDienThoai ?? "";
            txtEmail.Text = _existingTruong.Email ?? "";
            txtWebsite.Text = _existingTruong.Website ?? "";
            txtDiaChi.Text = _existingTruong.DiaChi ?? "";
            txtGhiChu.Text = _existingTruong.GhiChu ?? "";
            chkTrangThai.Checked = _existingTruong.TrangThai;
            
            if (_existingTruong.NgayThanhLap.HasValue)
            {
                dtpNgayThanhLap.Value = _existingTruong.NgayThanhLap.Value;
                chkNgayThanhLap.Checked = true;
            }

            // Set ThanhPho
            cmbThanhPho.SelectedValue = _existingTruong.MaTP;

            // Disable MaTruong field in edit mode
            txtMaTruong.Enabled = false;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                Truong truong;
                if (_isEditMode)
                {
                    // Update existing
                    truong = await context.Truongs.FindAsync(_existingTruong!.MaTruong);
                    if (truong == null)
                    {
                        MessageBox.Show("Không tìm thấy trường cần cập nhật!", "Lỗi", 
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    // Create new
                    truong = new Truong();
                    context.Truongs.Add(truong);
                }

                // Set properties
                truong.MaTruong = txtMaTruong.Text.Trim();
                truong.TenTruong = txtTenTruong.Text.Trim();
                truong.TenVietTat = string.IsNullOrWhiteSpace(txtTenVietTat.Text) ? null : txtTenVietTat.Text.Trim();
                truong.LoaiTruong = string.IsNullOrWhiteSpace(txtLoaiTruong.Text) ? null : txtLoaiTruong.Text.Trim();
                truong.HieuTruong = string.IsNullOrWhiteSpace(txtHieuTruong.Text) ? null : txtHieuTruong.Text.Trim();
                truong.BiThuDoan = string.IsNullOrWhiteSpace(txtBiThuDoan.Text) ? null : txtBiThuDoan.Text.Trim();
                truong.SoDienThoai = string.IsNullOrWhiteSpace(txtSoDienThoai.Text) ? null : txtSoDienThoai.Text.Trim();
                truong.Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim();
                truong.Website = string.IsNullOrWhiteSpace(txtWebsite.Text) ? null : txtWebsite.Text.Trim();
                truong.DiaChi = string.IsNullOrWhiteSpace(txtDiaChi.Text) ? null : txtDiaChi.Text.Trim();
                truong.GhiChu = string.IsNullOrWhiteSpace(txtGhiChu.Text) ? null : txtGhiChu.Text.Trim();
                truong.TrangThai = chkTrangThai.Checked;
                truong.MaTP = cmbThanhPho.SelectedValue?.ToString() ?? "";

                if (chkNgayThanhLap.Checked)
                {
                    truong.NgayThanhLap = dtpNgayThanhLap.Value.Date;
                }
                else
                {
                    truong.NgayThanhLap = null;
                }

                if (_isEditMode)
                {
                    truong.NgayCapNhat = DateTime.Now;
                }

                await context.SaveChangesAsync();

                MessageBox.Show(_isEditMode ? "Cập nhật trường thành công!" : "Thêm trường mới thành công!", 
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
            if (string.IsNullOrWhiteSpace(txtMaTruong.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Trường!", "Cảnh báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaTruong.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTenTruong.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên Trường!", "Cảnh báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenTruong.Focus();
                return false;
            }

            if (cmbThanhPho.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Thành phố!", "Cảnh báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbThanhPho.Focus();
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

