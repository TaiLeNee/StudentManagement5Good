using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using StudentManagement5GoodTempp.DataAccess.Context;
using StudentManagement5GoodTempp.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace StudentManagement5Good.Winform
{
    public partial class AcademicYearForm : Form
    {
        private readonly StudentManagementDbContext _context;
        private NamHoc? _namHoc;
        private bool _isEditMode;

        public AcademicYearForm(StudentManagementDbContext context, NamHoc? namHoc = null)
        {
            _context = context;
            _namHoc = namHoc;
            _isEditMode = namHoc != null;
            
            InitializeComponent();
            InitializeUI();
            
            if (_isEditMode && _namHoc != null)
            {
                LoadData();
            }
        }

        private void InitializeUI()
        {
            this.Text = _isEditMode ? "Chỉnh sửa Năm học" : "Thêm Năm học mới";
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            
            lblTitle.Text = _isEditMode ? "📝 Chỉnh sửa Năm học" : "➕ Thêm Năm học mới";
            btnSave.Text = _isEditMode ? "💾 Cập nhật" : "➕ Thêm mới";
        }

        private void LoadData()
        {
            if (_namHoc == null) return;
            
            txtMaNH.Text = _namHoc.MaNH;
            txtMaNH.ReadOnly = true; // Cannot edit primary key
            txtTenNH.Text = _namHoc.TenNamHoc;
            dateTimePickerStart.Value = _namHoc.TuNgay;
            dateTimePickerEnd.Value = _namHoc.DenNgay;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtMaNH.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã năm học!", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNH.Focus();
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(txtTenNH.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên năm học!", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNH.Focus();
                return false;
            }
            
            if (dateTimePickerEnd.Value <= dateTimePickerStart.Value)
            {
                MessageBox.Show("Ngày kết thúc phải sau ngày bắt đầu!", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateTimePickerEnd.Focus();
                return false;
            }
            
            // Check for duplicate code
            if (!_isEditMode)
            {
                var exists = _context.NamHocs.Any(nh => nh.MaNH == txtMaNH.Text.Trim());
                if (exists)
                {
                    MessageBox.Show("Mã năm học đã tồn tại!", "Lỗi", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaNH.Focus();
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
                
                if (_isEditMode && _namHoc != null)
                {
                    // Update existing
                    _namHoc.TenNamHoc = txtTenNH.Text.Trim();
                    _namHoc.TuNgay = dateTimePickerStart.Value;
                    _namHoc.DenNgay = dateTimePickerEnd.Value;
                    
                    _context.NamHocs.Update(_namHoc);
                }
                else
                {
                    // Add new
                    var newNamHoc = new NamHoc
                    {
                        MaNH = txtMaNH.Text.Trim(),
                        TenNamHoc = txtTenNH.Text.Trim(),
                        TuNgay = dateTimePickerStart.Value,
                        DenNgay = dateTimePickerEnd.Value
                    };
                    
                    _context.NamHocs.Add(newNamHoc);
                }
                
                await _context.SaveChangesAsync();
                
                MessageBox.Show(
                    _isEditMode ? "Cập nhật năm học thành công!" : "Thêm năm học mới thành công!",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu dữ liệu: {ex.Message}", "Lỗi", 
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

