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
using StudentManagement5GoodTempp.DataAccess.Context;
using StudentManagement5GoodTempp.DataAccess.Entity;

namespace StudentManagement5Good.Winform
{
    /// <summary>
    /// Form để sinh viên nộp và quản lý minh chứng
    /// </summary>
    public partial class MinhChungForm : Form
    {
        private readonly StudentManagementDbContext _context;
        private readonly string _maSV;
        private readonly string _namHoc;
        private string _selectedTieuChi;

        public MinhChungForm(StudentManagementDbContext context, string maSV, string selectedTieuChi, string namHoc)
        {
            _context = context;
            _maSV = maSV;
            _selectedTieuChi = selectedTieuChi;
            _namHoc = namHoc;
            
            InitializeComponent();
            InitializeForm();
        }

        private async void MinhChungForm_Load(object sender, EventArgs e)
        {
            try
            {
                await LoadTieuChiList();
                if (!string.IsNullOrEmpty(_selectedTieuChi))
                {
                    cmbTieuChi.SelectedValue = _selectedTieuChi;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", 
                              "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeForm()
        {
            this.Text = "Nộp minh chứng - Sinh viên 5 Tốt";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private async Task LoadTieuChiList()
        {
            try
            {
                var tieuChis = await _context.TieuChis
                    .Select(tc => new { tc.MaTC, tc.TenTieuChi })
                    .ToListAsync();

                cmbTieuChi.DataSource = tieuChis;
                cmbTieuChi.DisplayMember = "TenTieuChi";
                cmbTieuChi.ValueMember = "MaTC";
            }
            catch (Exception ex)
            {
                throw new Exception($"Không thể tải danh sách tiêu chí: {ex.Message}");
            }
        }

        private void btnChonFile_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Chọn file minh chứng";
                openFileDialog.Filter = "All Files (*.*)|*.*|Image Files (*.jpg;*.jpeg;*.png;*.gif)|*.jpg;*.jpeg;*.png;*.gif|PDF Files (*.pdf)|*.pdf|Word Documents (*.doc;*.docx)|*.doc;*.docx";
                openFileDialog.FilterIndex = 2; // Default to image files
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = openFileDialog.FileName;
                    lblFileSize.Text = $"Kích thước: {GetFileSize(openFileDialog.FileName)}";
                }
            }
        }

        private string GetFileSize(string filePath)
        {
            try
            {
                var fileInfo = new System.IO.FileInfo(filePath);
                long bytes = fileInfo.Length;
                
                if (bytes < 1024)
                    return $"{bytes} B";
                else if (bytes < 1024 * 1024)
                    return $"{bytes / 1024:F1} KB";
                else
                    return $"{bytes / (1024 * 1024):F1} MB";
            }
            catch
            {
                return "Không xác định";
            }
        }

        private async void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(txtTenMinhChung.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên minh chứng!", "Thông báo", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenMinhChung.Focus();
                    return;
                }

                if (cmbTieuChi.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn tiêu chí!", "Thông báo", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbTieuChi.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtFilePath.Text))
                {
                    MessageBox.Show("Vui lòng chọn file minh chứng!", "Thông báo", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    btnChonFile.Focus();
                    return;
                }

                // Show loading
                btnSubmit.Enabled = false;
                btnSubmit.Text = "Đang xử lý...";

                // Create evidence record
                await CreateMinhChung();

                MessageBox.Show("Nộp minh chứng thành công!\nMinh chứng sẽ được xem xét và phản hồi trong thời gian sớm nhất.", 
                              "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi nộp minh chứng: {ex.Message}", 
                              "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSubmit.Enabled = true;
                btnSubmit.Text = "Nộp minh chứng";
            }
        }

        private async Task CreateMinhChung()
        {
            try
            {
                var selectedTieuChi = cmbTieuChi.SelectedValue.ToString();
                
                // Create evidence record - Thiết kế mới: MinhChung độc lập
                var minhChung = new MinhChung
                {
                    MaMC = Guid.NewGuid().ToString("N")[..20], // Generate unique ID
                    MaSV = _maSV,
                    MaTC = selectedTieuChi,
                    MaNH = _namHoc,
                    TenMinhChung = txtTenMinhChung.Text.Trim(),
                    DuongDanFile = txtFilePath.Text, // In real app, should upload to server
                    TenFile = System.IO.Path.GetFileName(txtFilePath.Text),
                    LoaiFile = System.IO.Path.GetExtension(txtFilePath.Text),
                    KichThuocFile = new System.IO.FileInfo(txtFilePath.Text).Length,
                    MoTa = txtMoTa.Text.Trim(),
                    TrangThai = TrangThaiMinhChung.ChoDuyet,
                    NgayNop = DateTime.Now
                };

                _context.MinhChungs.Add(minhChung);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var errorMessage = $"Không thể tạo bản ghi minh chứng:\n\n{ex.Message}";
                if (ex.InnerException != null)
                {
                    errorMessage += $"\n\nChi tiết: {ex.InnerException.Message}";
                }
                MessageBox.Show(errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void txtTenMinhChung_TextChanged(object sender, EventArgs e)
        {
            lblCharCount.Text = $"{txtTenMinhChung.Text.Length}/100 ký tự";
        }

        private void txtMoTa_TextChanged(object sender, EventArgs e)
        {
            lblDescCharCount.Text = $"{txtMoTa.Text.Length}/500 ký tự";
        }
    }
}
