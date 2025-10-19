using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using StudentManagement5GoodTempp.DataAccess.Context;
using StudentManagement5GoodTempp.DataAccess.Entity;

namespace StudentManagement5Good.Winform
{
    public partial class StudentProfileReviewForm : Form
    {
        private readonly StudentManagementDbContext _context;
        private readonly User _currentUser;
        private string _currentNamHoc;
        private SinhVien? _selectedStudent;
        private Dictionary<string, List<MinhChung>> _criteriaEvidences = new();
        private Dictionary<string, bool> _criteriaStatus = new();

        public StudentProfileReviewForm(StudentManagementDbContext context, User currentUser)
        {
            _context = context;
            _currentUser = currentUser;
            InitializeComponent();
        }

        private async void StudentProfileReviewForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Delay to ensure parent form has stopped all operations
                await Task.Delay(200);
                
                // Load current academic year with AsNoTracking to avoid conflicts
                var currentYear = await _context.NamHocs
                    .AsNoTracking()
                    .OrderByDescending(nh => nh.TuNgay)
                    .FirstOrDefaultAsync();

                _currentNamHoc = currentYear?.MaNH ?? DateTime.Now.Year.ToString();

                InitializeFilters();
                await LoadStudentQueue();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}\n\nChi tiết: {ex.InnerException?.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Left Panel - Student Queue

        private void InitializeFilters()
        {
            // Status filter
            cmbStatusFilter.Items.Clear();
            cmbStatusFilter.Items.Add("Tất cả trạng thái");
            cmbStatusFilter.Items.Add("⏳ Hồ sơ chờ duyệt");
            cmbStatusFilter.Items.Add("⚠️ Cần bổ sung");
            cmbStatusFilter.Items.Add("✅ Đã hoàn tất");
            cmbStatusFilter.SelectedIndex = 1; // Default: Chờ duyệt

            // Unit filter (based on user role)
            cmbUnitFilter.Items.Clear();
            cmbUnitFilter.Items.Add("Tất cả đơn vị");

            if (_currentUser.VaiTro == UserRoles.CVHT)
            {
                // CVHT chỉ thấy lớp mình
                cmbUnitFilter.Items.Add($"Lớp: {_currentUser.MaLop}");
                cmbUnitFilter.SelectedIndex = 1;
            }
            else if (_currentUser.VaiTro == UserRoles.DOANKHOA || _currentUser.VaiTro == UserRoles.GIAOVU)
            {
                // Đoàn Khoa/Giáo vụ thấy theo khoa
                var lops = _context.Lops.Where(l => l.MaKhoa == _currentUser.MaKhoa).ToList();
                foreach (var lop in lops)
                {
                    cmbUnitFilter.Items.Add($"Lớp: {lop.TenLop}");
                }
                cmbUnitFilter.SelectedIndex = 0;
            }
            else
            {
                // Admin/Đoàn Trường thấy tất cả
                cmbUnitFilter.SelectedIndex = 0;
            }
        }

        private async Task LoadStudentQueue()
        {
            try
            {
                listViewStudents.Items.Clear();

                // Build query based on filters (read-only, no tracking)
                var query = _context.SinhViens
                    .AsNoTracking()
                    .Include(sv => sv.Lop)
                    .ThenInclude(l => l.Khoa)
                    .AsQueryable();

                // Apply role-based filter
                if (_currentUser.VaiTro == UserRoles.CVHT)
                {
                    query = query.Where(sv => sv.MaLop == _currentUser.MaLop);
                }
                else if (_currentUser.VaiTro == UserRoles.DOANKHOA || _currentUser.VaiTro == UserRoles.GIAOVU)
                {
                    query = query.Where(sv => sv.Lop.MaKhoa == _currentUser.MaKhoa);
                }

                // Apply search filter
                if (!string.IsNullOrWhiteSpace(txtSearch.Text) && 
                    txtSearch.Text != "🔍 Tìm theo tên hoặc mã SV...")
                {
                    var searchText = txtSearch.Text.ToLower();
                    query = query.Where(sv => sv.MaSV.ToLower().Contains(searchText) ||
                                            sv.HoTen.ToLower().Contains(searchText));
                }

                var students = await query.OrderBy(sv => sv.HoTen).ToListAsync();

                // Load evidence count for each student (read-only)
                foreach (var student in students)
                {
                    var evidenceCount = await _context.MinhChungs
                        .AsNoTracking()
                        .Where(mc => mc.MaSV == student.MaSV && mc.MaNH == _currentNamHoc)
                        .CountAsync();

                    var approvedCount = await _context.MinhChungs
                        .AsNoTracking()
                        .Where(mc => mc.MaSV == student.MaSV && 
                                   mc.MaNH == _currentNamHoc &&
                                   mc.TrangThai == TrangThaiMinhChung.DaDuyet)
                        .CountAsync();

                    var pendingCount = await _context.MinhChungs
                        .AsNoTracking()
                        .Where(mc => mc.MaSV == student.MaSV && 
                                   mc.MaNH == _currentNamHoc &&
                                   mc.TrangThai == TrangThaiMinhChung.ChoDuyet)
                        .CountAsync();

                    // Apply status filter
                    bool shouldInclude = true;
                    if (cmbStatusFilter.SelectedIndex == 1) // Chờ duyệt
                    {
                        shouldInclude = pendingCount > 0;
                    }
                    else if (cmbStatusFilter.SelectedIndex == 2) // Cần bổ sung
                    {
                        var rejectedCount = await _context.MinhChungs
                            .Where(mc => mc.MaSV == student.MaSV && 
                                       mc.MaNH == _currentNamHoc &&
                                       mc.TrangThai == TrangThaiMinhChung.BiTuChoi)
                            .CountAsync();
                        shouldInclude = rejectedCount > 0;
                    }
                    else if (cmbStatusFilter.SelectedIndex == 3) // Đã hoàn tất
                    {
                        shouldInclude = evidenceCount > 0 && pendingCount == 0;
                    }

                    if (!shouldInclude) continue;

                    var item = new ListViewItem("");
                    item.SubItems.Add($"{student.HoTen}\n{student.MaSV}\n{student.Lop?.TenLop ?? "N/A"}");
                    item.SubItems.Add($"Đã duyệt: {approvedCount}/{evidenceCount}\n⏳ Chờ: {pendingCount}");
                    item.Tag = student;
                    listViewStudents.Items.Add(item);
                }

                lblQueueTitle.Text = $"📋 Hàng đợi xét duyệt ({listViewStudents.Items.Count})";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách sinh viên: {ex.Message}", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void listViewStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewStudents.SelectedItems.Count == 0)
            {
                ShowNoStudentSelected();
                return;
            }

            _selectedStudent = listViewStudents.SelectedItems[0].Tag as SinhVien;
            if (_selectedStudent != null)
            {
                await LoadStudentProfile(_selectedStudent);
            }
        }

        private void ShowNoStudentSelected()
        {
            lblNoStudentSelected.Visible = true;
            panelStudentInfo.Visible = false;
            tabControlCriteria.Visible = false;
            panelFinalDecision.Visible = false;
        }

        private void ApplyFilters(object sender, EventArgs e)
        {
            _ = LoadStudentQueue();
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "🔍 Tìm theo tên hoặc mã SV...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "🔍 Tìm theo tên hoặc mã SV...";
                txtSearch.ForeColor = Color.Gray;
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            _ = LoadStudentQueue();
        }

        #endregion

        #region Right Panel - Student Profile

        private async Task LoadStudentProfile(SinhVien student)
        {
            try
            {
                lblNoStudentSelected.Visible = false;
                panelStudentInfo.Visible = true;
                tabControlCriteria.Visible = true;
                panelFinalDecision.Visible = true;

                // Update student info
                lblStudentName.Text = student.HoTen;
                lblStudentId.Text = $"MSSV: {student.MaSV}";
                lblStudentClass.Text = $"Lớp: {student.Lop?.TenLop ?? "N/A"} - Khoa: {student.Lop?.Khoa?.TenKhoa ?? "N/A"}";

                // Load criteria and evidences (read-only)
                var allCriteria = await _context.TieuChis
                    .AsNoTracking()
                    .Include(tc => tc.TieuChiYeuCaus)
                    .OrderBy(tc => tc.MaTC)
                    .ToListAsync();

                var allEvidences = await _context.MinhChungs
                    .AsNoTracking()
                    .Include(mc => mc.TieuChi)
                    .Where(mc => mc.MaSV == student.MaSV && mc.MaNH == _currentNamHoc)
                    .ToListAsync();

                // Group evidences by criteria
                _criteriaEvidences.Clear();
                _criteriaStatus.Clear();

                // Group criteria by first 2 characters of MaTC (e.g., HT, DD, TL, TN, HN)
                var criteriaGroups = new Dictionary<string, List<TieuChi>>
                {
                    ["HocTap"] = allCriteria.Where(tc => tc.MaTC.StartsWith("HT")).ToList(),
                    ["DaoDuc"] = allCriteria.Where(tc => tc.MaTC.StartsWith("DD")).ToList(),
                    ["TheLuc"] = allCriteria.Where(tc => tc.MaTC.StartsWith("TL")).ToList(),
                    ["TinhNguyen"] = allCriteria.Where(tc => tc.MaTC.StartsWith("TN")).ToList(),
                    ["HoiNhap"] = allCriteria.Where(tc => tc.MaTC.StartsWith("HN")).ToList()
                };

                // Populate tabs
                PopulateTab(tabPageHocTap, criteriaGroups["HocTap"], allEvidences);
                PopulateTab(tabPageDaoDuc, criteriaGroups["DaoDuc"], allEvidences);
                PopulateTab(tabPageTheLuc, criteriaGroups["TheLuc"], allEvidences);
                PopulateTab(tabPageTinhNguyen, criteriaGroups["TinhNguyen"], allEvidences);
                PopulateTab(tabPageHoiNhap, criteriaGroups["HoiNhap"], allEvidences);

                UpdateFinalDecisionStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải hồ sơ sinh viên: {ex.Message}", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateTab(TabPage tab, List<TieuChi> criteria, List<MinhChung> allEvidences)
        {
            tab.Controls.Clear();

            var panel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Padding = new Padding(10)
            };

            bool tabPassed = criteria.Any();
            foreach (var criterion in criteria)
            {
                var evidences = allEvidences.Where(e => e.MaTC == criterion.MaTC).ToList();

                // Create criterion panel
                var criterionPanel = CreateCriterionPanel(criterion, evidences);
                criterionPanel.Width = tab.Width - 50;
                panel.Controls.Add(criterionPanel);

                // Check if criterion is passed
                bool criterionPassed = evidences.Any(e => e.TrangThai == TrangThaiMinhChung.DaDuyet);
                tabPassed &= criterionPassed;
            }

            tab.Controls.Add(panel);

            // Update tab status
            _criteriaStatus[tab.Name] = tabPassed;
            UpdateTabStatus(tab, tabPassed);
        }

        private Panel CreateCriterionPanel(TieuChi criterion, List<MinhChung> evidences)
        {
            var panel = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(250, 250, 250),
                Padding = new Padding(15),
                AutoSize = true,
                MinimumSize = new Size(800, 100)
            };

            var yPos = 10;

            // Criterion name
            var lblName = new Label
            {
                Text = $"📌 {criterion.TenTieuChi}",
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 152, 219),
                AutoSize = true,
                Location = new Point(10, yPos)
            };
            panel.Controls.Add(lblName);
            yPos += 35;

            // Requirements
            if (criterion.TieuChiYeuCaus?.Any() == true)
            {
                var lblReqTitle = new Label
                {
                    Text = "📋 Yêu cầu:",
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    AutoSize = true,
                    Location = new Point(10, yPos)
                };
                panel.Controls.Add(lblReqTitle);
                yPos += 25;

                foreach (var req in criterion.TieuChiYeuCaus)
                {
                    var lblReq = new Label
                    {
                        Text = $"   • {req.MoTaYeuCau}",
                        Font = new Font("Segoe UI", 9F),
                        AutoSize = true,
                        MaximumSize = new Size(750, 0),
                        Location = new Point(10, yPos)
                    };
                    panel.Controls.Add(lblReq);
                    yPos += lblReq.Height + 5;
                }
                yPos += 10;
            }

            // Evidences
            var lblEvidenceTitle = new Label
            {
                Text = $"📎 Minh chứng ({evidences.Count}):",
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(10, yPos)
            };
            panel.Controls.Add(lblEvidenceTitle);
            yPos += 30;

            if (evidences.Any())
            {
                foreach (var evidence in evidences)
                {
                    var evidencePanel = CreateEvidencePanel(evidence);
                    evidencePanel.Location = new Point(20, yPos);
                    evidencePanel.Width = 750;
                    panel.Controls.Add(evidencePanel);
                    yPos += evidencePanel.Height + 10;
                }
            }
            else
            {
                var lblNoEvidence = new Label
                {
                    Text = "   ⚠️ Sinh viên chưa nộp minh chứng cho tiêu chí này",
                    Font = new Font("Segoe UI", 9F, FontStyle.Italic),
                    ForeColor = Color.Gray,
                    AutoSize = true,
                    Location = new Point(20, yPos)
                };
                panel.Controls.Add(lblNoEvidence);
                yPos += 30;
            }

            panel.Height = yPos + 20;
            return panel;
        }

        private Panel CreateEvidencePanel(MinhChung evidence)
        {
            var panel = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Height = 120,
                Padding = new Padding(10)
            };

            // Evidence name
            var lblName = new Label
            {
                Text = evidence.TenMinhChung,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(10, 10),
                MaximumSize = new Size(500, 0)
            };
            panel.Controls.Add(lblName);

            // Status
            var lblStatus = new Label
            {
                Text = GetStatusText(evidence.TrangThai),
                Font = new Font("Segoe UI", 9F),
                AutoSize = true,
                Location = new Point(10, 35)
            };
            panel.Controls.Add(lblStatus);

            // File info
            var lblFile = new Label
            {
                Text = $"📄 {evidence.TenFile}",
                Font = new Font("Segoe UI", 9F),
                AutoSize = true,
                ForeColor = Color.Gray,
                Location = new Point(10, 55)
            };
            panel.Controls.Add(lblFile);

            // Action buttons (only if pending)
            if (evidence.TrangThai == TrangThaiMinhChung.ChoDuyet)
            {
                var btnApprove = new Button
                {
                    Text = "✅ Duyệt",
                    BackColor = Color.FromArgb(46, 204, 113),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(100, 30),
                    Location = new Point(550, 10)
                };
                btnApprove.FlatAppearance.BorderSize = 0;
                btnApprove.Click += async (s, e) => await ApproveEvidence(evidence);
                panel.Controls.Add(btnApprove);

                var btnReject = new Button
                {
                    Text = "❌ Từ chối",
                    BackColor = Color.FromArgb(231, 76, 60),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(100, 30),
                    Location = new Point(550, 45)
                };
                btnReject.FlatAppearance.BorderSize = 0;
                btnReject.Click += async (s, e) => await RejectEvidence(evidence);
                panel.Controls.Add(btnReject);

                var btnView = new Button
                {
                    Text = "👁️ Xem",
                    BackColor = Color.FromArgb(52, 152, 219),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(100, 30),
                    Location = new Point(550, 80)
                };
                btnView.FlatAppearance.BorderSize = 0;
                btnView.Click += (s, e) => ViewEvidence(evidence);
                panel.Controls.Add(btnView);
            }

            return panel;
        }

        private string GetStatusText(TrangThaiMinhChung status)
        {
            return status switch
            {
                TrangThaiMinhChung.ChoDuyet => "⏳ Chờ duyệt",
                TrangThaiMinhChung.DaDuyet => "✅ Đã duyệt",
                TrangThaiMinhChung.BiTuChoi => "❌ Từ chối",
                TrangThaiMinhChung.CanBoSung => "⚠️ Cần bổ sung",
                _ => "❓ Không xác định"
            };
        }

        private void UpdateTabStatus(TabPage tab, bool passed)
        {
            var statusIcon = passed ? "✅" : "⏳";
            var originalText = tab.Text.Substring(tab.Text.IndexOf(' ') + 1);
            tab.Text = $"{statusIcon} {originalText}";
        }

        private async Task ApproveEvidence(MinhChung evidence)
        {
            try
            {
                evidence.TrangThai = TrangThaiMinhChung.DaDuyet;
                evidence.NguoiDuyet = _currentUser.UserId;
                evidence.NgayDuyet = DateTime.Now;

                await _context.SaveChangesAsync();

                MessageBox.Show("Đã duyệt minh chứng thành công!", "Thành công",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reload profile
                if (_selectedStudent != null)
                {
                    await LoadStudentProfile(_selectedStudent);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi duyệt minh chứng: {ex.Message}", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task RejectEvidence(MinhChung evidence)
        {
            var reason = Microsoft.VisualBasic.Interaction.InputBox(
                "Vui lòng nhập lý do từ chối:",
                "Từ chối minh chứng",
                "",
                -1, -1);

            if (string.IsNullOrWhiteSpace(reason)) return;

            try
            {
                evidence.TrangThai = TrangThaiMinhChung.BiTuChoi;
                evidence.NguoiDuyet = _currentUser.UserId;
                evidence.NgayDuyet = DateTime.Now;
                evidence.MoTa = $"[TỪ CHỐI] {reason}"; // Store reason in description

                await _context.SaveChangesAsync();

                MessageBox.Show("Đã từ chối minh chứng!", "Thành công",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reload profile
                if (_selectedStudent != null)
                {
                    await LoadStudentProfile(_selectedStudent);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi từ chối minh chứng: {ex.Message}", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ViewEvidence(MinhChung evidence)
        {
            if (!string.IsNullOrEmpty(evidence.DuongDanFile) && System.IO.File.Exists(evidence.DuongDanFile))
            {
                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = evidence.DuongDanFile,
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
                MessageBox.Show("File không tồn tại!", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateFinalDecisionStatus()
        {
            bool allCriteriaPassed = _criteriaStatus.Values.All(v => v);

            if (allCriteriaPassed)
            {
                lblFinalStatus.Text = "✅ Quyết định tổng kết: Đủ điều kiện công nhận Sinh viên 5 Tốt";
                lblFinalStatus.ForeColor = Color.FromArgb(46, 204, 113);
                btnApproveAll.Enabled = true;
            }
            else
            {
                lblFinalStatus.Text = "⏳ Quyết định tổng kết: Chưa đủ điều kiện công nhận";
                lblFinalStatus.ForeColor = Color.FromArgb(52, 152, 219);
                btnApproveAll.Enabled = false;
            }
        }

        private async void btnApproveAll_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn công nhận sinh viên {_selectedStudent?.HoTen} là Sinh viên 5 Tốt?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes) return;

            try
            {
                // Create or update final result for KetQuaDanhHieu
                var finalResult = await _context.KetQuaDanhHieus
                    .FirstOrDefaultAsync(k => k.MaSV == _selectedStudent.MaSV && 
                                            k.MaNH == _currentNamHoc);

                if (finalResult == null)
                {
                    finalResult = new KetQuaDanhHieu
                    {
                        MaKQ = Guid.NewGuid().ToString("N")[..20],
                        MaSV = _selectedStudent.MaSV,
                        MaNH = _currentNamHoc,
                        MaCap = GetApprovalLevel(),
                        DatDanhHieu = true,
                        NgayDat = DateTime.Now,
                        GhiChu = txtGeneralNote.Text
                    };
                    _context.KetQuaDanhHieus.Add(finalResult);
                }
                else
                {
                    finalResult.MaCap = GetApprovalLevel();
                    finalResult.DatDanhHieu = true;
                    finalResult.NgayDat = DateTime.Now;
                    finalResult.GhiChu = txtGeneralNote.Text;
                }

                // Also create KetQuaXetDuyet records for each approved criterion
                var approvedCriteria = await _context.MinhChungs
                    .Where(mc => mc.MaSV == _selectedStudent.MaSV && 
                               mc.MaNH == _currentNamHoc &&
                               mc.TrangThai == TrangThaiMinhChung.DaDuyet)
                    .Select(mc => mc.MaTC)
                    .Distinct()
                    .ToListAsync();

                foreach (var maTC in approvedCriteria)
                {
                    var existingResult = await _context.KetQuaXetDuyets
                        .FirstOrDefaultAsync(k => k.MaSV == _selectedStudent.MaSV &&
                                                k.MaTC == maTC &&
                                                k.MaNH == _currentNamHoc &&
                                                k.MaCap == GetApprovalLevel());

                    if (existingResult == null)
                    {
                        var ketQuaXetDuyet = new KetQuaXetDuyet
                        {
                            MaKQ = Guid.NewGuid().ToString("N")[..20],
                            MaSV = _selectedStudent.MaSV,
                            MaTC = maTC,
                            MaCap = GetApprovalLevel(),
                            MaNH = _currentNamHoc,
                            KetQua = true,
                            NgayXetDuyet = DateTime.Now,
                            NguoiXetDuyet = _currentUser.UserId,
                            GhiChu = txtGeneralNote.Text
                        };
                        _context.KetQuaXetDuyets.Add(ketQuaXetDuyet);
                    }
                }

                await _context.SaveChangesAsync();

                MessageBox.Show("Đã công nhận Sinh viên 5 Tốt thành công!", "Thành công",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh queue
                await LoadStudentQueue();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi công nhận: {ex.Message}", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetApprovalLevel()
        {
            return _currentUser.VaiTro switch
            {
                UserRoles.CVHT => "CAP1", // Cấp lớp
                UserRoles.DOANKHOA => "CAP2", // Cấp khoa
                UserRoles.DOANTRUONG => "CAP3", // Cấp trường
                _ => "CAP1"
            };
        }

        #endregion
    }
}

