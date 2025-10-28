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
    public partial class TieuChiForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TieuChi? _existingTieuChi;
        private bool _isEditMode = false;

        public TieuChiForm(IServiceProvider serviceProvider, TieuChi? existingTieuChi = null)
        {
            _serviceProvider = serviceProvider;
            _existingTieuChi = existingTieuChi;
            _isEditMode = existingTieuChi != null;
            
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Set form title based on mode
            this.Text = _isEditMode ? "Chỉnh sửa Tiêu chí" : "Thêm Tiêu chí mới";
            lblTitle.Text = _isEditMode ? "Chỉnh sửa thông tin Tiêu chí" : "Thêm thông tin Tiêu chí mới";

            // Load LoaiDuLieu options
            LoadLoaiDuLieuOptions();

            // If edit mode, populate fields
            if (_isEditMode && _existingTieuChi != null)
            {
                PopulateFields();
            }
        }

        private void LoadLoaiDuLieuOptions()
        {
            cmbLoaiDuLieu.Items.Clear();
            cmbLoaiDuLieu.Items.AddRange(new string[] 
            {
                "Số nguyên",
                "Số thực", 
                "Văn bản",
                "Ngày tháng",
                "Boolean",
                "File",
                "Hình ảnh"
            });
        }

        private void PopulateFields()
        {
            if (_existingTieuChi == null) return;

            txtMaTC.Text = _existingTieuChi.MaTC;
            txtTenTieuChi.Text = _existingTieuChi.TenTieuChi;
            txtMoTa.Text = _existingTieuChi.MoTa ?? "";
            cmbLoaiDuLieu.Text = _existingTieuChi.LoaiDuLieu ?? "";
            txtDonViTinh.Text = _existingTieuChi.DonViTinh ?? "";

            // Disable MaTC field in edit mode
            txtMaTC.Enabled = false;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                TieuChi tieuChi;
                if (_isEditMode)
                {
                    // Update existing
                    tieuChi = await context.TieuChis.FindAsync(_existingTieuChi!.MaTC);
                    if (tieuChi == null)
                    {
                        MessageBox.Show("Không tìm thấy tiêu chí cần cập nhật!", "Lỗi", 
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    // Create new
                    tieuChi = new TieuChi();
                    context.TieuChis.Add(tieuChi);
                }

                // Set properties
                tieuChi.MaTC = txtMaTC.Text.Trim();
                tieuChi.TenTieuChi = txtTenTieuChi.Text.Trim();
                tieuChi.MoTa = string.IsNullOrWhiteSpace(txtMoTa.Text) ? null : txtMoTa.Text.Trim();
                tieuChi.LoaiDuLieu = string.IsNullOrWhiteSpace(cmbLoaiDuLieu.Text) ? null : cmbLoaiDuLieu.Text.Trim();
                tieuChi.DonViTinh = string.IsNullOrWhiteSpace(txtDonViTinh.Text) ? null : txtDonViTinh.Text.Trim();

                await context.SaveChangesAsync();

                MessageBox.Show(_isEditMode ? "Cập nhật tiêu chí thành công!" : "Thêm tiêu chí mới thành công!", 
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
            if (string.IsNullOrWhiteSpace(txtMaTC.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Tiêu chí!", "Cảnh báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaTC.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTenTieuChi.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên Tiêu chí!", "Cảnh báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenTieuChi.Focus();
                return false;
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

