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
    public partial class ThanhPhoForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ThanhPho? _existingThanhPho;
        private bool _isEditMode = false;

        public ThanhPhoForm(IServiceProvider serviceProvider, ThanhPho? existingThanhPho = null)
        {
            _serviceProvider = serviceProvider;
            _existingThanhPho = existingThanhPho;
            _isEditMode = existingThanhPho != null;
            
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Set form title based on mode
            this.Text = _isEditMode ? "Chỉnh sửa Thành phố" : "Thêm Thành phố mới";
            lblTitle.Text = _isEditMode ? "Chỉnh sửa thông tin Thành phố" : "Thêm thông tin Thành phố mới";

            // If edit mode, populate fields
            if (_isEditMode && _existingThanhPho != null)
            {
                PopulateFields();
            }
        }

        private void PopulateFields()
        {
            if (_existingThanhPho == null) return;

            txtMaTP.Text = _existingThanhPho.MaTP;
            txtTenThanhPho.Text = _existingThanhPho.TenThanhPho;
            txtMaVung.Text = _existingThanhPho.MaVung ?? "";
            txtTenVung.Text = _existingThanhPho.TenVung ?? "";
            txtChuTichDoanTP.Text = _existingThanhPho.ChuTichDoanTP ?? "";
            txtSoDienThoai.Text = _existingThanhPho.SoDienThoai ?? "";
            txtEmail.Text = _existingThanhPho.Email ?? "";
            txtDiaChi.Text = _existingThanhPho.DiaChi ?? "";
            txtGhiChu.Text = _existingThanhPho.GhiChu ?? "";
            chkTrangThai.Checked = _existingThanhPho.TrangThai;

            // Disable MaTP field in edit mode
            txtMaTP.Enabled = false;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                ThanhPho thanhPho;
                if (_isEditMode)
                {
                    // Update existing
                    thanhPho = await context.ThanhPhos.FindAsync(_existingThanhPho!.MaTP);
                    if (thanhPho == null)
                    {
                        MessageBox.Show("Không tìm thấy thành phố cần cập nhật!", "Lỗi", 
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    // Create new
                    thanhPho = new ThanhPho();
                    context.ThanhPhos.Add(thanhPho);
                }

                // Set properties
                thanhPho.MaTP = txtMaTP.Text.Trim();
                thanhPho.TenThanhPho = txtTenThanhPho.Text.Trim();
                thanhPho.MaVung = string.IsNullOrWhiteSpace(txtMaVung.Text) ? null : txtMaVung.Text.Trim();
                thanhPho.TenVung = string.IsNullOrWhiteSpace(txtTenVung.Text) ? null : txtTenVung.Text.Trim();
                thanhPho.ChuTichDoanTP = string.IsNullOrWhiteSpace(txtChuTichDoanTP.Text) ? null : txtChuTichDoanTP.Text.Trim();
                thanhPho.SoDienThoai = string.IsNullOrWhiteSpace(txtSoDienThoai.Text) ? null : txtSoDienThoai.Text.Trim();
                thanhPho.Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text.Trim();
                thanhPho.DiaChi = string.IsNullOrWhiteSpace(txtDiaChi.Text) ? null : txtDiaChi.Text.Trim();
                thanhPho.GhiChu = string.IsNullOrWhiteSpace(txtGhiChu.Text) ? null : txtGhiChu.Text.Trim();
                thanhPho.TrangThai = chkTrangThai.Checked;

                if (_isEditMode)
                {
                    thanhPho.NgayCapNhat = DateTime.Now;
                }

                await context.SaveChangesAsync();

                MessageBox.Show(_isEditMode ? "Cập nhật thành phố thành công!" : "Thêm thành phố mới thành công!", 
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
            if (string.IsNullOrWhiteSpace(txtMaTP.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Thành phố!", "Cảnh báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaTP.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTenThanhPho.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên Thành phố!", "Cảnh báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenThanhPho.Focus();
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
    }
}

