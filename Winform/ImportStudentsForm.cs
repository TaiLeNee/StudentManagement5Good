using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using ClosedXML.Excel;
using StudentManagement5GoodTempp.DataAccess.Context;
using StudentManagement5GoodTempp.DataAccess.Entity;
using Microsoft.EntityFrameworkCore;
using BC = BCrypt.Net.BCrypt;

namespace StudentManagement5Good.Winform
{
    public partial class ImportStudentsForm : Form
    {
        private readonly StudentManagementDbContext _context;
        private readonly User _currentUser;
        private DataTable _previewData;
        private string _selectedFilePath = "";

        public ImportStudentsForm(StudentManagementDbContext context, User currentUser)
        {
            _context = context;
            _currentUser = currentUser;
            _previewData = new DataTable();
            
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            this.Text = "Import Sinh viên từ Excel";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            
            // Set instructions based on role
            UpdateInstructionsForRole();
        }

        private void UpdateInstructionsForRole()
        {
            string instructions = _currentUser.VaiTro switch
            {
                UserRoles.GIAOVU => "Bạn có thể import toàn bộ sinh viên trong trường.",
                UserRoles.CVHT => $"Bạn chỉ có thể import sinh viên cho lớp: {_currentUser.MaLop ?? "Chưa được gán lớp"}",
                _ => "Bạn không có quyền import sinh viên."
            };
            
            lblInstructions.Text = instructions;
        }

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files|*.xlsx;*.xls|CSV Files|*.csv|All Files|*.*";
                openFileDialog.Title = "Chọn file import sinh viên";
                
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _selectedFilePath = openFileDialog.FileName;
                    txtFilePath.Text = _selectedFilePath;
                    
                    // Load and preview data
                    LoadFilePreview();
                }
            }
        }

        private void LoadFilePreview()
        {
            try
            {
                progressBar.Visible = true;
                progressBar.Style = ProgressBarStyle.Marquee;
                
                _previewData.Clear();
                
                if (_selectedFilePath.EndsWith(".xlsx") || _selectedFilePath.EndsWith(".xls"))
                {
                    LoadExcelFile();
                }
                else if (_selectedFilePath.EndsWith(".csv"))
                {
                    LoadCsvFile();
                }
                
                dataGridViewPreview.DataSource = _previewData;
                lblRecordCount.Text = $"Tổng số: {_previewData.Rows.Count} bản ghi";
                
                // Validate data
                ValidateData();
                
                progressBar.Visible = false;
            }
            catch (Exception ex)
            {
                progressBar.Visible = false;
                MessageBox.Show($"Lỗi đọc file: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadExcelFile()
        {
            using (var workbook = new XLWorkbook(_selectedFilePath))
            {
                var worksheet = workbook.Worksheet(1);
                var range = worksheet.RangeUsed();
                
                // Read headers
                var headerRow = range.Row(1);
                foreach (var cell in headerRow.Cells())
                {
                    _previewData.Columns.Add(cell.Value.ToString());
                }
                
                // Read data rows
                for (int row = 2; row <= range.RowCount(); row++)
                {
                    var dataRow = _previewData.NewRow();
                    for (int col = 1; col <= range.ColumnCount(); col++)
                    {
                        dataRow[col - 1] = worksheet.Cell(row, col).Value.ToString();
                    }
                    _previewData.Rows.Add(dataRow);
                }
            }
        }

        private void LoadCsvFile()
        {
            var lines = File.ReadAllLines(_selectedFilePath);
            if (lines.Length == 0) return;
            
            // Read headers
            var headers = lines[0].Split(',');
            foreach (var header in headers)
            {
                _previewData.Columns.Add(header.Trim());
            }
            
            // Read data
            for (int i = 1; i < lines.Length; i++)
            {
                var values = lines[i].Split(',');
                if (values.Length == headers.Length)
                {
                    _previewData.Rows.Add(values);
                }
            }
        }

        private void ValidateData()
        {
            int errorCount = 0;
            txtValidationResult.Clear();
            txtValidationResult.ForeColor = Color.Red;
            
            // Check required columns
            string[] requiredColumns = { "MaSV", "HoTen", "Email", "MaLop" };
            foreach (var column in requiredColumns)
            {
                if (!_previewData.Columns.Contains(column))
                {
                    txtValidationResult.AppendText($"❌ Thiếu cột bắt buộc: {column}\r\n");
                    errorCount++;
                }
            }
            
            if (errorCount == 0)
            {
                txtValidationResult.ForeColor = Color.Green;
                txtValidationResult.AppendText("✅ Dữ liệu hợp lệ! Sẵn sàng import.\r\n");
                btnImport.Enabled = true;
            }
            else
            {
                txtValidationResult.AppendText($"\r\n❌ Tổng số lỗi: {errorCount}");
                btnImport.Enabled = false;
            }
        }

        private async void btnImport_Click(object sender, EventArgs e)
        {
            if (_previewData.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để import!", "Cảnh báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn import {_previewData.Rows.Count} sinh viên?\n\n" +
                "Lưu ý: Các sinh viên đã tồn tại sẽ được bỏ qua.",
                "Xác nhận Import",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            
            if (result != DialogResult.Yes) return;
            
            await ImportStudentsAsync();
        }

        private async Task ImportStudentsAsync()
        {
            int successCount = 0;
            int skipCount = 0;
            int errorCount = 0;
            
            progressBar.Visible = true;
            progressBar.Style = ProgressBarStyle.Blocks;
            progressBar.Maximum = _previewData.Rows.Count;
            progressBar.Value = 0;
            
            btnImport.Enabled = false;
            btnBrowseFile.Enabled = false;
            
            txtValidationResult.Clear();
            txtValidationResult.AppendText("🔄 Đang import...\r\n\r\n");
            
            try
            {
                foreach (DataRow row in _previewData.Rows)
                {
                    try
                    {
                        string maSV = row["MaSV"].ToString()?.Trim() ?? "";
                        string hoTen = row["HoTen"].ToString()?.Trim() ?? "";
                        string email = row["Email"].ToString()?.Trim() ?? "";
                        string maLop = row["MaLop"].ToString()?.Trim() ?? "";
                        
                        // Validate role-based permission
                        if (_currentUser.VaiTro == UserRoles.CVHT)
                        {
                            if (maLop != _currentUser.MaLop)
                            {
                                txtValidationResult.AppendText($"⚠️ Bỏ qua {maSV}: Không thuộc lớp của bạn\r\n");
                                skipCount++;
                                progressBar.Value++;
                                continue;
                            }
                        }
                        
                        // Check if student already exists
                        var existingStudent = await _context.SinhViens
                            .FirstOrDefaultAsync(sv => sv.MaSV == maSV);
                        
                        if (existingStudent != null)
                        {
                            txtValidationResult.AppendText($"⚠️ Bỏ qua {maSV}: Đã tồn tại\r\n");
                            skipCount++;
                            progressBar.Value++;
                            continue;
                        }
                        
                        // Check if class exists
                        var lop = await _context.Lops.FirstOrDefaultAsync(l => l.MaLop == maLop);
                        if (lop == null)
                        {
                            txtValidationResult.AppendText($"❌ Lỗi {maSV}: Không tìm thấy lớp {maLop}\r\n");
                            errorCount++;
                            progressBar.Value++;
                            continue;
                        }
                        
                        // Create new student
                        var newStudent = new SinhVien
                        {
                            MaSV = maSV,
                            HoTen = hoTen,
                            Email = email,
                            MaLop = maLop,
                            NgaySinh = DateTime.TryParse(row["NgaySinh"].ToString(), out var ns) ? ns : DateTime.Now.AddYears(-20),
                            GioiTinh = row["GioiTinh"].ToString()?.Trim() ?? "Nam",
                            SoDienThoai = row["SoDienThoai"].ToString()?.Trim()
                        };
                        
                        _context.SinhViens.Add(newStudent);
                        
                        // Create user account for student
                        var newUser = new User
                        {
                            UserId = $"SV_{maSV}",
                            Username = maSV,
                            Password = BC.HashPassword("123456"), // Default password
                            HoTen = hoTen,
                            Email = email,
                            VaiTro = UserRoles.SINHVIEN,
                            MaSV = maSV,
                            TrangThai = true,
                            NgayTao = DateTime.Now
                        };
                        
                        _context.Users.Add(newUser);
                        
                        successCount++;
                        txtValidationResult.AppendText($"✅ Thêm thành công: {maSV} - {hoTen}\r\n");
                    }
                    catch (Exception ex)
                    {
                        errorCount++;
                        txtValidationResult.AppendText($"❌ Lỗi: {ex.Message}\r\n");
                    }
                    
                    progressBar.Value++;
                }
                
                // Save all changes
                await _context.SaveChangesAsync();
                
                txtValidationResult.AppendText($"\r\n{'=',50}\r\n");
                txtValidationResult.AppendText($"🎉 HOÀN THÀNH!\r\n");
                txtValidationResult.AppendText($"✅ Thành công: {successCount}\r\n");
                txtValidationResult.AppendText($"⚠️ Bỏ qua: {skipCount}\r\n");
                txtValidationResult.AppendText($"❌ Lỗi: {errorCount}\r\n");
                
                MessageBox.Show(
                    $"Import hoàn tất!\n\n" +
                    $"✅ Thành công: {successCount}\n" +
                    $"⚠️ Bỏ qua: {skipCount}\n" +
                    $"❌ Lỗi: {errorCount}",
                    "Kết quả Import",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                
                if (successCount > 0)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi trong quá trình import: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                progressBar.Visible = false;
                btnImport.Enabled = true;
                btnBrowseFile.Enabled = true;
            }
        }

        private void btnDownloadTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel Files|*.xlsx";
                    saveFileDialog.FileName = "MauImportSinhVien.xlsx";
                    
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        CreateExcelTemplate(saveFileDialog.FileName);
                        
                        var result = MessageBox.Show(
                            "Đã tạo file mẫu thành công!\n\nBạn có muốn mở file?",
                            "Thành công",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information);
                        
                        if (result == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                            {
                                FileName = saveFileDialog.FileName,
                                UseShellExecute = true
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tạo file mẫu: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateExcelTemplate(string filePath)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("SinhVien");
                
                // Headers
                worksheet.Cell(1, 1).Value = "MaSV";
                worksheet.Cell(1, 2).Value = "HoTen";
                worksheet.Cell(1, 3).Value = "Email";
                worksheet.Cell(1, 4).Value = "NgaySinh";
                worksheet.Cell(1, 5).Value = "GioiTinh";
                worksheet.Cell(1, 6).Value = "SoDienThoai";
                worksheet.Cell(1, 7).Value = "MaLop";
                
                // Sample data
                worksheet.Cell(2, 1).Value = "2021001";
                worksheet.Cell(2, 2).Value = "Nguyễn Văn A";
                worksheet.Cell(2, 3).Value = "nguyenvana@email.com";
                worksheet.Cell(2, 4).Value = "01/01/2003";
                worksheet.Cell(2, 5).Value = "Nam";
                worksheet.Cell(2, 6).Value = "0901234567";
                worksheet.Cell(2, 7).Value = "CNTT01";
                
                // Style headers
                var headerRange = worksheet.Range(1, 1, 1, 7);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                
                // Auto-fit columns
                worksheet.Columns().AdjustToContents();
                
                workbook.SaveAs(filePath);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

