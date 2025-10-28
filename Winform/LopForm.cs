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
    public partial class LopForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Lop? _existingLop;
        private bool _isEditMode = false;

        public LopForm(IServiceProvider serviceProvider, Lop? existingLop = null)
        {
            _serviceProvider = serviceProvider;
            _existingLop = existingLop;
            _isEditMode = existingLop != null;
            
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Set form title based on mode
            this.Text = _isEditMode ? "Chỉnh sửa Lớp" : "Thêm Lớp mới";
            lblTitle.Text = _isEditMode ? "Chỉnh sửa thông tin Lớp" : "Thêm thông tin Lớp mới";

            // Load Khoa data
            LoadKhoas();

            // If edit mode, populate fields
            if (_isEditMode && _existingLop != null)
            {
                PopulateFields();
            }
        }

        private async void LoadKhoas()
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                var khoas = await context.Khoas
                    .AsNoTracking()
                    .Where(k => k.TrangThai == true)
                    .OrderBy(k => k.TenKhoa)
                    .ToListAsync();

                cmbKhoa.DataSource = khoas;
                cmbKhoa.DisplayMember = "TenKhoa";
                cmbKhoa.ValueMember = "MaKhoa";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách khoa: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateFields()
        {
            if (_existingLop == null) return;

            txtMaLop.Text = _existingLop.MaLop;
            txtTenLop.Text = _existingLop.TenLop;
            txtGVCN.Text = _existingLop.GVCN ?? "";
            
            // Set Khoa
            cmbKhoa.SelectedValue = _existingLop.MaKhoa;

            // Disable MaLop field in edit mode
            txtMaLop.Enabled = false;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                Lop lop;
                if (_isEditMode)
                {
                    // Update existing
                    lop = await context.Lops.FindAsync(_existingLop!.MaLop);
                    if (lop == null)
                    {
                        MessageBox.Show("Không tìm thấy lớp cần cập nhật!", "Lỗi", 
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    // Create new
                    lop = new Lop();
                    context.Lops.Add(lop);
                }

                // Set properties
                lop.MaLop = txtMaLop.Text.Trim();
                lop.TenLop = txtTenLop.Text.Trim();
                lop.GVCN = string.IsNullOrWhiteSpace(txtGVCN.Text) ? null : txtGVCN.Text.Trim();
                lop.MaKhoa = cmbKhoa.SelectedValue?.ToString() ?? "";

                await context.SaveChangesAsync();

                MessageBox.Show(_isEditMode ? "Cập nhật lớp thành công!" : "Thêm lớp mới thành công!", 
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
            if (string.IsNullOrWhiteSpace(txtMaLop.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Lớp!", "Cảnh báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaLop.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTenLop.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên Lớp!", "Cảnh báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenLop.Focus();
                return false;
            }

            if (cmbKhoa.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Khoa!", "Cảnh báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbKhoa.Focus();
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

