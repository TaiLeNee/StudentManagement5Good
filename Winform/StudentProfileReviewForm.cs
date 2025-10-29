using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudentManagement5GoodTempp.DataAccess.Context;
using StudentManagement5GoodTempp.DataAccess.Entity;
using StudentManagement5GoodTempp.Services;

namespace StudentManagement5Good.Winform
{
    public partial class StudentProfileReviewForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly User _currentUser;
        private string _currentNamHoc;
        private SinhVien? _selectedStudent;
        private Dictionary<string, List<MinhChung>> _criteriaEvidences = new();
        private Dictionary<string, bool> _criteriaStatus = new();

        public StudentProfileReviewForm(IServiceProvider serviceProvider, User currentUser)
        {
            _serviceProvider = serviceProvider;
            _currentUser = currentUser;
            InitializeComponent();
        }

        // Constructor cũ để backward compatibility
        public StudentProfileReviewForm(StudentManagementDbContext context, User currentUser)
            : this(StudentManagement5GoodTempp.Program.ServiceProvider, currentUser)
        {
        }

        private async void StudentProfileReviewForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Delay to ensure parent form has stopped all operations
                await Task.Delay(200);
                
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                // Load current academic year with AsNoTracking to avoid conflicts
                var currentYear = await context.NamHocs
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
            cmbStatusFilter.Items.Add("Hồ sơ chờ duyệt");
            cmbStatusFilter.Items.Add("Cần bổ sung");
            cmbStatusFilter.Items.Add("Đã hoàn tất");
            cmbStatusFilter.SelectedIndex = 1; // Default: Chờ duyệt

            // Unit filter (based on user role)
            cmbUnitFilter.Items.Clear();
            cmbUnitFilter.Items.Add("Tất cả đơn vị");

            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

            if (_currentUser.VaiTro == UserRoles.CVHT)
            {
                // CVHT chỉ thấy lớp mình
                cmbUnitFilter.Items.Add($"Lớp: {_currentUser.MaLop}");
                cmbUnitFilter.SelectedIndex = 1;
            }
            else if (_currentUser.VaiTro == UserRoles.DOANKHOA || _currentUser.VaiTro == UserRoles.GIAOVU)
            {
                // Đoàn Khoa/Giáo vụ thấy theo khoa
                var lops = context.Lops.Where(l => l.MaKhoa == _currentUser.MaKhoa).ToList();
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
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                listViewStudents.Items.Clear();

                string capDangChoDuyet = GetApprovalLevel(); // Lấy cấp xét của User hiện tại

                // 1. Tìm các bản ghi KetQuaDanhHieu đang "Chờ duyệt" ở cấp này
                IQueryable<KetQuaDanhHieu> hoSoChoDuyetQuery = context.KetQuaDanhHieus
                    .AsNoTracking()
                    .Where(kq => kq.MaNH == _currentNamHoc &&
                                 kq.MaCap == capDangChoDuyet &&
                                 kq.TrangThaiWorkflow == "DangChoDuyet")
                    .Include(kq => kq.SinhVien)
                        .ThenInclude(sv => sv.Lop)
                        .ThenInclude(l => l.Khoa)
                        .ThenInclude(k => k.Truong);

                // 2. Lọc theo phạm vi quản lý của User
                switch (_currentUser.VaiTro)
                {
                    case UserRoles.DOANTP:
                        hoSoChoDuyetQuery = hoSoChoDuyetQuery
                            .Where(kq => kq.SinhVien.Lop.Khoa.Truong.MaTP == _currentUser.MaTP);
                        break;
                    case UserRoles.DOANTRUONG:
                        hoSoChoDuyetQuery = hoSoChoDuyetQuery
                            .Where(kq => kq.SinhVien.Lop.Khoa.MaTruong == _currentUser.MaTruong);
                        break;
                    case UserRoles.DOANKHOA:
                    case UserRoles.GIAOVU:
                        hoSoChoDuyetQuery = hoSoChoDuyetQuery
                            .Where(kq => kq.SinhVien.Lop.MaKhoa == _currentUser.MaKhoa);
                        break;
                    case UserRoles.CVHT:
                        hoSoChoDuyetQuery = hoSoChoDuyetQuery
                            .Where(kq => kq.SinhVien.MaLop == _currentUser.MaLop);
                        break;
                    case UserRoles.ADMIN:
                    case UserRoles.DOANTU:
                        // Admin và TW thấy tất cả hồ sơ đang chờ ở cấp của họ
                        break;
                }

                // 3. Lọc theo UI (Search)
                if (!string.IsNullOrWhiteSpace(txtSearch.Text) && 
                    txtSearch.Text != "🔍 Tìm theo tên hoặc mã SV...")
                {
                    var searchText = txtSearch.Text.ToLower();
                    hoSoChoDuyetQuery = hoSoChoDuyetQuery.Where(kq => kq.SinhVien.MaSV.ToLower().Contains(searchText) ||
                                                                     kq.SinhVien.HoTen.ToLower().Contains(searchText));
                }

                // 4. Lấy danh sách Sinh viên từ các hồ sơ chờ duyệt
                var studentsToReview = await hoSoChoDuyetQuery
                    .Select(kq => kq.SinhVien)
                    .Distinct()
                    .OrderBy(sv => sv.HoTen)
                    .ToListAsync();

                // 5. Populate ListView
                foreach (var student in studentsToReview)
                {
                    var item = new ListViewItem(""); // Chỗ cho avatar
                    item.SubItems.Add($"{student.HoTen}\n{student.MaSV}\n{student.Lop?.TenLop ?? "N/A"}");
                    item.SubItems.Add($"Chờ duyệt {capDangChoDuyet}"); // Cập nhật tiến độ
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
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                lblNoStudentSelected.Visible = false;
                panelStudentInfo.Visible = true;
                tabControlCriteria.Visible = true;
                panelFinalDecision.Visible = true;

                // Update student info
                lblStudentName.Text = student.HoTen;
                lblStudentId.Text = $"MSSV: {student.MaSV}";
                lblStudentClass.Text = $"Lớp: {student.Lop?.TenLop ?? "N/A"} - Khoa: {student.Lop?.Khoa?.TenKhoa ?? "N/A"}";

                // Load criteria and evidences (read-only)
                var allCriteria = await context.TieuChis
                    .AsNoTracking()
                    .Include(tc => tc.TieuChiYeuCaus)
                    .OrderBy(tc => tc.MaTC)
                    .ToListAsync();

                var allEvidences = await context.MinhChungs
                    .AsNoTracking()
                    .Include(mc => mc.TieuChi)
                    .Where(mc => mc.MaSV == student.MaSV && mc.MaNH == _currentNamHoc)
                    .ToListAsync();

                // Group evidences by criteria
                _criteriaEvidences.Clear();
                _criteriaStatus.Clear();

                // Sử dụng mapping mới dựa trên MaTC thực tế
                var criteriaGroups = new Dictionary<string, List<TieuChi>>
                {
                    ["HocTap"] = allCriteria.Where(tc => tc.MaTC.Equals("TC02")).ToList(),     // TC02 - Học tập tốt
                    ["DaoDuc"] = allCriteria.Where(tc => tc.MaTC.Equals("TC01")).ToList(),    // TC01 - Đạo đức tốt
                    ["TheLuc"] = allCriteria.Where(tc => tc.MaTC.Equals("TC03")).ToList(),    // TC03 - Thể lực tốt
                    ["TinhNguyen"] = allCriteria.Where(tc => tc.MaTC.Equals("TC04")).ToList(), // TC04 - Tình nguyện tốt
                    ["HoiNhap"] = allCriteria.Where(tc => tc.MaTC.Equals("TC05")).ToList()    // TC05 - Hội nhập tốt
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
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                var evidenceToUpdate = await context.MinhChungs.FindAsync(evidence.MaMC);
                if (evidenceToUpdate != null)
                {
                    evidenceToUpdate.TrangThai = TrangThaiMinhChung.DaDuyet;
                    evidenceToUpdate.NguoiDuyet = _currentUser.UserId;
                    evidenceToUpdate.NgayDuyet = DateTime.Now;

                    await context.SaveChangesAsync();

                    MessageBox.Show("Đã duyệt minh chứng thành công!", "Thành công",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Reload profile
                    if (_selectedStudent != null)
                    {
                        await LoadStudentProfile(_selectedStudent);
                    }
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
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                var evidenceToUpdate = await context.MinhChungs.FindAsync(evidence.MaMC);
                if (evidenceToUpdate != null)
                {
                    evidenceToUpdate.TrangThai = TrangThaiMinhChung.BiTuChoi;
                    evidenceToUpdate.NguoiDuyet = _currentUser.UserId;
                    evidenceToUpdate.NgayDuyet = DateTime.Now;
                    evidenceToUpdate.LyDoTuChoi = reason;

                    await context.SaveChangesAsync();

                    MessageBox.Show("Đã từ chối minh chứng!", "Thành công",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Reload profile
                    if (_selectedStudent != null)
                    {
                        await LoadStudentProfile(_selectedStudent);
                    }
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
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();
                // Lấy workflow service từ scope
                var workflowService = scope.ServiceProvider.GetRequiredService<IApprovalWorkflowService>();

                string currentLevel = GetApprovalLevel();

                // Create or update final result for KetQuaDanhHieu
                var finalResult = await context.KetQuaDanhHieus
                    .FirstOrDefaultAsync(k => k.MaSV == _selectedStudent!.MaSV && 
                                            k.MaNH == _currentNamHoc &&
                                            k.MaCap == currentLevel); // Đảm bảo lấy đúng cấp

                if (finalResult == null)
                {
                    finalResult = new KetQuaDanhHieu
                    {
                        MaKQ = Guid.NewGuid().ToString("N")[..20],
                        MaSV = _selectedStudent!.MaSV,
                        MaNH = _currentNamHoc,
                        MaCap = currentLevel
                    };
                    context.KetQuaDanhHieus.Add(finalResult);
                }

                // Cập nhật trạng thái là ĐÃ ĐẠT ở cấp này
                finalResult.DatDanhHieu = true;
                finalResult.TrangThaiWorkflow = "DaDat"; // Cập nhật trạng thái
                finalResult.NgayDat = DateTime.Now;
                finalResult.GhiChu = txtGeneralNote.Text;

                // Also create KetQuaXetDuyet records for each approved criterion
                var approvedCriteria = await context.MinhChungs
                    .Where(mc => mc.MaSV == _selectedStudent!.MaSV && 
                               mc.MaNH == _currentNamHoc &&
                               mc.TrangThai == TrangThaiMinhChung.DaDuyet)
                    .Select(mc => mc.MaTC)
                    .Distinct()
                    .ToListAsync();

                foreach (var maTC in approvedCriteria)
                {
                    var existingResult = await context.KetQuaXetDuyets
                        .FirstOrDefaultAsync(k => k.MaSV == _selectedStudent!.MaSV &&
                                                k.MaTC == maTC &&
                                                k.MaNH == _currentNamHoc &&
                                                k.MaCap == currentLevel);

                    if (existingResult == null)
                    {
                        var ketQuaXetDuyet = new KetQuaXetDuyet
                        {
                            MaKQ = Guid.NewGuid().ToString("N")[..20],
                            MaSV = _selectedStudent!.MaSV,
                            MaTC = maTC,
                            MaCap = currentLevel,
                            MaNH = _currentNamHoc,
                            KetQua = true,
                            NgayXetDuyet = DateTime.Now,
                            NguoiXetDuyet = _currentUser.UserId,
                            GhiChu = txtGeneralNote.Text
                        };
                        context.KetQuaXetDuyets.Add(ketQuaXetDuyet);
                    }
                }

                await context.SaveChangesAsync();

                // *** BẮT ĐẦU WORKFLOW MỚI: CHUYỂN HỒ SƠ LÊN CẤP TRÊN ***
                await workflowService.ChuyenHoSoLenCapTrenAsync(_selectedStudent!.MaSV, _currentNamHoc, currentLevel);

                MessageBox.Show("Đã công nhận Sinh viên 5 Tốt thành công!", "Thành công",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Tải lại danh sách hàng đợi (sẽ tự động loại bỏ SV vừa duyệt)
                await LoadStudentQueue();
                // Ẩn thông tin SV vừa duyệt
                ShowNoStudentSelected();
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
                UserRoles.CVHT => ManagementLevels.LOP,
                UserRoles.DOANKHOA => ManagementLevels.KHOA,
                UserRoles.DOANTRUONG => ManagementLevels.TRUONG,
                UserRoles.DOANTP => ManagementLevels.TP,   // <-- THÊM VÀO
                UserRoles.DOANTU => ManagementLevels.TU,   // <-- THÊM VÀO
                _ => ManagementLevels.LOP // Mặc định
            };
        }

        #endregion
    }
}

