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
    public partial class TieuChiYeuCauForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private TieuChiYeuCau? _existingTieuChiYeuCau;
        private bool _isEditMode = false;

        public TieuChiYeuCauForm(IServiceProvider serviceProvider, TieuChiYeuCau? existingTieuChiYeuCau = null)
        {
            _serviceProvider = serviceProvider;
            _existingTieuChiYeuCau = existingTieuChiYeuCau;
            _isEditMode = existingTieuChiYeuCau != null;
            
            InitializeComponent();
            InitializeForm();
        }

        private void InitializeForm()
        {
            // Set form title based on mode
            this.Text = _isEditMode ? "Chỉnh sửa Tiêu chí Yêu cầu" : "Thêm Tiêu chí Yêu cầu mới";
            lblTitle.Text = _isEditMode ? "Chỉnh sửa cấu hình Tiêu chí Yêu cầu" : "Thêm cấu hình Tiêu chí Yêu cầu mới";

            // Load data
            LoadTieuChis();
            LoadCapXets();
            LoadExistingData();

            // If edit mode, populate fields
            if (_isEditMode && _existingTieuChiYeuCau != null)
            {
                PopulateFields();
            }
        }

        private async void LoadTieuChis()
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                var tieuChis = await context.TieuChis
                    .AsNoTracking()
                    .OrderBy(tc => tc.TenTieuChi)
                    .ToListAsync();

                cmbTieuChi.DataSource = tieuChis;
                cmbTieuChi.DisplayMember = "TenTieuChi";
                cmbTieuChi.ValueMember = "MaTC";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách tiêu chí: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void LoadCapXets()
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                var capXets = await context.CapXets
                    .AsNoTracking()
                    .OrderBy(cx => cx.TenCap)
                    .ToListAsync();

                cmbCapXet.DataSource = capXets;
                cmbCapXet.DisplayMember = "TenCap";
                cmbCapXet.ValueMember = "MaCap";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách cấp xét: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void LoadExistingData()
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                var data = await context.TieuChiYeuCaus
                    .AsNoTracking()
                    .Include(tcyc => tcyc.TieuChi)
                    .Include(tcyc => tcyc.CapXet)
                    .OrderBy(tcyc => tcyc.TieuChi.TenTieuChi)
                    .ThenBy(tcyc => tcyc.CapXet.TenCap)
                    .ToListAsync();

                dataGridViewTieuChiYeuCau.Rows.Clear();
                foreach (var item in data)
                {
                    var row = new DataGridViewRow();
                    row.CreateCells(dataGridViewTieuChiYeuCau);
                    row.Cells[0].Value = item.TieuChi?.TenTieuChi ?? "";
                    row.Cells[1].Value = item.CapXet?.TenCap ?? "";
                    row.Cells[2].Value = item.NguongDat?.ToString("F2") ?? "";
                    row.Cells[3].Value = item.BatBuoc ? "Có" : "Không";
                    row.Cells[4].Value = item.MoTaYeuCau ?? "";
                    row.Tag = item;
                    dataGridViewTieuChiYeuCau.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateFields()
        {
            if (_existingTieuChiYeuCau == null) return;

            cmbTieuChi.SelectedValue = _existingTieuChiYeuCau.MaTC;
            cmbCapXet.SelectedValue = _existingTieuChiYeuCau.MaCap;
            txtNguongDat.Text = _existingTieuChiYeuCau.NguongDat?.ToString() ?? "";
            txtMoTaYeuCau.Text = _existingTieuChiYeuCau.MoTaYeuCau ?? "";
            chkBatBuoc.Checked = _existingTieuChiYeuCau.BatBuoc;

            // Disable combo boxes in edit mode
            cmbTieuChi.Enabled = false;
            cmbCapXet.Enabled = false;
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                TieuChiYeuCau tieuChiYeuCau;
                if (_isEditMode)
                {
                    // Update existing
                    tieuChiYeuCau = await context.TieuChiYeuCaus
                        .FirstOrDefaultAsync(tcyc => tcyc.MaTC == _existingTieuChiYeuCau!.MaTC && 
                                                     tcyc.MaCap == _existingTieuChiYeuCau.MaCap);
                    if (tieuChiYeuCau == null)
                    {
                        MessageBox.Show("Không tìm thấy tiêu chí yêu cầu cần cập nhật!", "Lỗi", 
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    // Check if combination already exists
                    var selectedTieuChi = cmbTieuChi.SelectedValue?.ToString();
                    var selectedCapXet = cmbCapXet.SelectedValue?.ToString();
                    var existing = await context.TieuChiYeuCaus
                        .FirstOrDefaultAsync(tcyc => tcyc.MaTC == selectedTieuChi && 
                                                     tcyc.MaCap == selectedCapXet);
                    if (existing != null)
                    {
                        MessageBox.Show("Cấu hình này đã tồn tại!", "Cảnh báo", 
                                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Create new
                    tieuChiYeuCau = new TieuChiYeuCau();
                    context.TieuChiYeuCaus.Add(tieuChiYeuCau);
                }

                // Set properties
                tieuChiYeuCau.MaTC = cmbTieuChi.SelectedValue?.ToString() ?? "";
                tieuChiYeuCau.MaCap = cmbCapXet.SelectedValue?.ToString() ?? "";
                
                if (float.TryParse(txtNguongDat.Text, out float nguongDat))
                {
                    tieuChiYeuCau.NguongDat = nguongDat;
                }
                else
                {
                    tieuChiYeuCau.NguongDat = null;
                }
                
                tieuChiYeuCau.MoTaYeuCau = string.IsNullOrWhiteSpace(txtMoTaYeuCau.Text) ? null : txtMoTaYeuCau.Text.Trim();
                tieuChiYeuCau.BatBuoc = chkBatBuoc.Checked;

                await context.SaveChangesAsync();

                MessageBox.Show(_isEditMode ? "Cập nhật tiêu chí yêu cầu thành công!" : "Thêm tiêu chí yêu cầu mới thành công!", 
                              "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Refresh data
                LoadExistingData();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lưu dữ liệu: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (cmbTieuChi.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Tiêu chí!", "Cảnh báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbTieuChi.Focus();
                return false;
            }

            if (cmbCapXet.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn Cấp xét!", "Cảnh báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCapXet.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtNguongDat.Text))
            {
                if (!float.TryParse(txtNguongDat.Text, out _))
                {
                    MessageBox.Show("Ngưỡng đạt phải là số!", "Cảnh báo", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNguongDat.Focus();
                    return false;
                }
            }

            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            cmbTieuChi.SelectedIndex = -1;
            cmbCapXet.SelectedIndex = -1;
            txtNguongDat.Clear();
            txtMoTaYeuCau.Clear();
            chkBatBuoc.Checked = false;
            
            // Re-enable combo boxes
            cmbTieuChi.Enabled = true;
            cmbCapXet.Enabled = true;
            
            _isEditMode = false;
            this.Text = "Thêm Tiêu chí Yêu cầu mới";
            lblTitle.Text = "Thêm cấu hình Tiêu chí Yêu cầu mới";
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewTieuChiYeuCau.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn dòng cần xóa!", "Cảnh báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedRow = dataGridViewTieuChiYeuCau.SelectedRows[0];
            var tieuChiYeuCau = selectedRow.Tag as TieuChiYeuCau;
            
            if (tieuChiYeuCau == null) return;

            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa cấu hình này?\n\n" +
                $"Tiêu chí: {tieuChiYeuCau.TieuChi?.TenTieuChi}\n" +
                $"Cấp xét: {tieuChiYeuCau.CapXet?.TenCap}",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                    var toDelete = await context.TieuChiYeuCaus
                        .FirstOrDefaultAsync(tcyc => tcyc.MaTC == tieuChiYeuCau.MaTC && 
                                                     tcyc.MaCap == tieuChiYeuCau.MaCap);
                    
                    if (toDelete != null)
                    {
                        context.TieuChiYeuCaus.Remove(toDelete);
                        await context.SaveChangesAsync();
                        
                        MessageBox.Show("Xóa cấu hình thành công!", "Thành công", 
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        LoadExistingData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi xóa dữ liệu: {ex.Message}", "Lỗi", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridViewTieuChiYeuCau_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var selectedRow = dataGridViewTieuChiYeuCau.Rows[e.RowIndex];
                var tieuChiYeuCau = selectedRow.Tag as TieuChiYeuCau;
                
                if (tieuChiYeuCau != null)
                {
                    _existingTieuChiYeuCau = tieuChiYeuCau;
                    _isEditMode = true;
                    PopulateFields();
                    this.Text = "Chỉnh sửa Tiêu chí Yêu cầu";
                    lblTitle.Text = "Chỉnh sửa cấu hình Tiêu chí Yêu cầu";
                }
            }
        }
    }
}

