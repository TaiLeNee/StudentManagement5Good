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
    /// <summary>
    /// Form duyệt minh chứng cho người có thẩm quyền
    /// </summary>
    public partial class MinhChungApprovalForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly User _currentUser;
        private List<MinhChung> _pendingEvidences;

        public MinhChungApprovalForm(IServiceProvider serviceProvider, User currentUser)
        {
            _serviceProvider = serviceProvider;
            _currentUser = currentUser;
            _pendingEvidences = new List<MinhChung>();
            
            InitializeComponent();
            InitializeApprovalForm();
        }

        // Constructor cũ để backward compatibility
        public MinhChungApprovalForm(StudentManagementDbContext context, User currentUser)
            : this(StudentManagement5GoodTempp.Program.ServiceProvider, currentUser)
        {
        }

        private async void MinhChungApprovalForm_Load(object sender, EventArgs e)
        {
            try
            {
                await LoadPendingEvidences();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", 
                              "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeApprovalForm()
        {
            this.Text = $"Duyệt minh chứng - {_currentUser.HoTen}";
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;

            // Set user info
            lblApproverName.Text = _currentUser.HoTen;
            lblApproverRole.Text = GetRoleDisplayName(_currentUser.VaiTro);

            // Initialize approval status combo box
            var statusItems = new[]
            {
                new { Value = TrangThaiMinhChung.DaDuyet, Text = "Duyệt" },
                new { Value = TrangThaiMinhChung.BiTuChoi, Text = "Từ chối" },
                new { Value = TrangThaiMinhChung.CanBoSung, Text = "Cần bổ sung" }
            };

            cmbApprovalStatus.DataSource = statusItems;
            cmbApprovalStatus.DisplayMember = "Text";
            cmbApprovalStatus.ValueMember = "Value";
            cmbApprovalStatus.SelectedIndex = 0; // Default to approve
        }

        private string GetRoleDisplayName(string role)
        {
            return role switch
            {
                "ADMIN" => "Quản trị viên",
                "GIAOVU" => "Giáo vụ",
                "COVAN" => "Cố vấn học tập",
                "TRUONGKHOA" => "Trưởng khoa",
                "TRUONGLOP" => "Trưởng lớp",
                _ => role
            };
        }

        private async Task LoadPendingEvidences()
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                // Load pending evidences based on user role and authority
                var query = context.MinhChungs
                    .Include(m => m.SinhVien)
                    .ThenInclude(s => s.Lop)
                    .ThenInclude(l => l.Khoa)
                    .Include(m => m.TieuChi)
                    .Include(m => m.NamHoc)
                    .Where(m => m.TrangThai == TrangThaiMinhChung.ChoDuyet);

                // Filter based on user authority
                switch (_currentUser.VaiTro)
                {
                    case "ADMIN":
                        // Admin can see all
                        break;
                    case "TRUONGKHOA":
                        // Trưởng khoa chỉ thấy sinh viên trong khoa
                        if (!string.IsNullOrEmpty(_currentUser.MaKhoa))
                            query = query.Where(m => m.SinhVien.Lop.MaKhoa == _currentUser.MaKhoa);
                        break;
                    case "COVAN":
                        // Cố vấn chỉ thấy sinh viên trong lớp
                        if (!string.IsNullOrEmpty(_currentUser.MaLop))
                            query = query.Where(m => m.SinhVien.MaLop == _currentUser.MaLop);
                        break;
                    default:
                        // Other roles might have limited access
                        query = query.Where(m => false); // No access by default
                        break;
                }

                _pendingEvidences = await query
                    .OrderBy(m => m.NgayNop)
                    .ToListAsync();

                PopulateEvidenceList();
                UpdateStatistics();
            }
            catch (Exception ex)
            {
                throw new Exception($"Không thể tải danh sách minh chứng chờ duyệt: {ex.Message}");
            }
        }

        private async void btnApprove_Click(object sender, EventArgs e)
        {
            if (listViewEvidences.SelectedItems.Count == 0) return;

            var selectedEvidence = listViewEvidences.SelectedItems[0].Tag as MinhChung;
            if (selectedEvidence == null) return;

            try
            {
                // Get approval decision
                var approvalStatus = (TrangThaiMinhChung)cmbApprovalStatus.SelectedValue;
                var feedback = txtFeedback.Text.Trim();

                // Validate feedback for rejection
                if (approvalStatus == TrangThaiMinhChung.BiTuChoi && string.IsNullOrEmpty(feedback))
                {
                    MessageBox.Show("Vui lòng nhập lý do từ chối!", "Thông báo", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtFeedback.Focus();
                    return;
                }

                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                // Tìm lại entity trong context scope mới
                var evidence = await context.MinhChungs.FindAsync(selectedEvidence.MaMC);
                if (evidence == null)
                {
                    MessageBox.Show("Không tìm thấy minh chứng!", "Lỗi", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Update evidence status
                evidence.TrangThai = approvalStatus;
                evidence.NgayDuyet = DateTime.Now;
                evidence.NguoiDuyet = _currentUser.UserId;

                if (approvalStatus == TrangThaiMinhChung.BiTuChoi)
                    evidence.LyDoTuChoi = feedback;
                else
                    evidence.GhiChu = feedback;

                await context.SaveChangesAsync();

                MessageBox.Show($"Đã {approvalStatus.ToDisplayString().ToLower()} minh chứng thành công!", 
                              "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Remove from pending list and refresh
                _pendingEvidences.Remove(selectedEvidence);
                PopulateEvidenceList();
                UpdateStatistics();
                ClearEvidenceDetails();
                EnableApprovalControls(false);

                // Check if we should create evaluation result
                if (approvalStatus == TrangThaiMinhChung.DaDuyet)
                {
                    await CheckAndCreateEvaluationResult(evidence);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi duyệt minh chứng: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task CheckAndCreateEvaluationResult(MinhChung approvedEvidence)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                // Check if there's already an evaluation result for this student-criteria-year
                var existingResult = await context.KetQuaXetDuyets
                    .FirstOrDefaultAsync(k => k.MaSV == approvedEvidence.MaSV &&
                                            k.MaTC == approvedEvidence.MaTC &&
                                            k.MaNH == approvedEvidence.MaNH &&
                                            k.MaCap == "LOP"); // Start with class level

                if (existingResult == null)
                {
                    // Count approved evidences for this criteria
                    var approvedCount = await context.MinhChungs
                        .CountAsync(m => m.MaSV == approvedEvidence.MaSV &&
                                       m.MaTC == approvedEvidence.MaTC &&
                                       m.MaNH == approvedEvidence.MaNH &&
                                       m.TrangThai == TrangThaiMinhChung.DaDuyet);

                    // Create evaluation result if criteria is met (simplified logic)
                    if (approvedCount >= 1) // At least 1 approved evidence
                    {
                        var ketQuaXetDuyet = new KetQuaXetDuyet
                        {
                            MaKQ = Guid.NewGuid().ToString("N")[..20],
                            MaSV = approvedEvidence.MaSV,
                            MaTC = approvedEvidence.MaTC,
                            MaCap = "LOP",
                            MaNH = approvedEvidence.MaNH,
                            KetQua = true,
                            SoMinhChungDaDuyet = approvedCount,
                            TongSoMinhChung = approvedCount,
                            NgayXetDuyet = DateTime.Now,
                            NguoiXetDuyet = _currentUser.UserId,
                            TrangThaiHoSo = "HOAN_THANH"
                        };

                        context.KetQuaXetDuyets.Add(ketQuaXetDuyet);
                        await context.SaveChangesAsync();
                    }
                }
                else
                {
                    // Update existing result
                    existingResult.SoMinhChungDaDuyet = await context.MinhChungs
                        .CountAsync(m => m.MaSV == approvedEvidence.MaSV &&
                                       m.MaTC == approvedEvidence.MaTC &&
                                       m.MaNH == approvedEvidence.MaNH &&
                                       m.TrangThai == TrangThaiMinhChung.DaDuyet);
                    
                    existingResult.NgayXetDuyet = DateTime.Now;
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Log error but don't fail the approval process
                Console.WriteLine($"Error creating evaluation result: {ex.Message}");
            }
        }

        private void PopulateEvidenceList()
        {
            listViewEvidences.Items.Clear();

            foreach (var evidence in _pendingEvidences)
            {
                var item = new ListViewItem(evidence.TenMinhChung);
                item.SubItems.Add(evidence.SinhVien.HoTen);
                item.SubItems.Add(evidence.SinhVien.MaSV);
                item.SubItems.Add(evidence.SinhVien.Lop.TenLop);
                item.SubItems.Add(evidence.TieuChi.TenTieuChi);
                item.SubItems.Add(evidence.NgayNop.ToString("dd/MM/yyyy"));
                item.SubItems.Add(GetFileSizeString(evidence.KichThuocFile));
                
                item.Tag = evidence;
                listViewEvidences.Items.Add(item);
            }
        }

        private string GetFileSizeString(long? fileSize)
        {
            if (!fileSize.HasValue) return "N/A";
            
            var bytes = fileSize.Value;
            if (bytes < 1024)
                return $"{bytes} B";
            else if (bytes < 1024 * 1024)
                return $"{bytes / 1024:F1} KB";
            else
                return $"{bytes / (1024 * 1024):F1} MB";
        }

        private void UpdateStatistics()
        {
            lblTotalCount.Text = $"Tổng số: {_pendingEvidences.Count}";
            
            var criteriaGroups = _pendingEvidences.GroupBy(e => e.TieuChi.TenTieuChi)
                .Select(g => $"{g.Key}: {g.Count()}")
                .ToList();
            
            lblCriteriaBreakdown.Text = string.Join(" | ", criteriaGroups);
        }

        private void listViewEvidences_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewEvidences.SelectedItems.Count > 0)
            {
                var selectedEvidence = listViewEvidences.SelectedItems[0].Tag as MinhChung;
                if (selectedEvidence != null)
                {
                    DisplayEvidenceDetails(selectedEvidence);
                    EnableApprovalControls(true);
                }
            }
            else
            {
                ClearEvidenceDetails();
                EnableApprovalControls(false);
            }
        }

        private void DisplayEvidenceDetails(MinhChung evidence)
        {
            lblStudentName.Text = evidence.SinhVien.HoTen;
            lblStudentId.Text = evidence.SinhVien.MaSV;
            lblStudentClass.Text = evidence.SinhVien.Lop.TenLop;
            lblCriteria.Text = evidence.TieuChi.TenTieuChi;
            lblEvidenceName.Text = evidence.TenMinhChung;
            lblSubmissionDate.Text = evidence.NgayNop.ToString("dd/MM/yyyy HH:mm");
            lblFileName.Text = evidence.TenFile ?? "N/A";
            lblFileSize.Text = GetFileSizeString(evidence.KichThuocFile);
            txtDescription.Text = evidence.MoTa ?? "";
            
            // Clear previous feedback
            txtFeedback.Text = "";
            cmbApprovalStatus.SelectedIndex = 0; // Default to approve
        }

        private void ClearEvidenceDetails()
        {
            lblStudentName.Text = "";
            lblStudentId.Text = "";
            lblStudentClass.Text = "";
            lblCriteria.Text = "";
            lblEvidenceName.Text = "";
            lblSubmissionDate.Text = "";
            lblFileName.Text = "";
            lblFileSize.Text = "";
            txtDescription.Text = "";
            txtFeedback.Text = "";
        }

        private void EnableApprovalControls(bool enabled)
        {
            cmbApprovalStatus.Enabled = enabled;
            txtFeedback.Enabled = enabled;
            btnApprove.Enabled = enabled;
            btnViewFile.Enabled = enabled;
        }

        private void btnViewFile_Click(object sender, EventArgs e)
        {
            if (listViewEvidences.SelectedItems.Count == 0) return;

            var selectedEvidence = listViewEvidences.SelectedItems[0].Tag as MinhChung;
            if (selectedEvidence == null) return;

            if (!string.IsNullOrEmpty(selectedEvidence.DuongDanFile))
            {
                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = selectedEvidence.DuongDanFile,
                        UseShellExecute = true
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Không thể mở file: {ex.Message}", "Lỗi", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Không có đường dẫn file!", "Thông báo", 
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                await LoadPendingEvidences();
                ClearEvidenceDetails();
                EnableApprovalControls(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi làm mới dữ liệu: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbApprovalStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbApprovalStatus.SelectedValue is TrangThaiMinhChung status)
            {
                // Show/hide feedback requirement based on status
                bool requiresFeedback = status == TrangThaiMinhChung.BiTuChoi || status == TrangThaiMinhChung.CanBoSung;
                lblFeedback.Text = requiresFeedback ? "Lý do/Ghi chú (bắt buộc):" : "Ghi chú (tùy chọn):";
                txtFeedback.BackColor = requiresFeedback ? Color.FromArgb(255, 240, 240) : Color.White;
            }
        }
    }
}
