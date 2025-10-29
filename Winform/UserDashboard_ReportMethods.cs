using System;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using StudentManagement5GoodTempp.DataAccess.Context;
using StudentManagement5GoodTempp.DataAccess.Entity;
using StudentManagement5GoodTempp.Services;
using System.Drawing;
using System.Collections.Generic;
using System.Globalization;

namespace StudentManagement5Good.Winform
{
    /// <summary>
    /// UserDashboard - Partial Class cho các phương thức Báo cáo & Thống kê
    /// Triển khai lại hoàn toàn từ đầu
    /// </summary>
    public partial class UserDashboard
    {
        #region Báo cáo & Thống kê - Main Methods

        /// <summary>
        /// Load dữ liệu thống kê tổng quan
        /// </summary>
        private async Task LoadReportStatistics()
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

                var currentYear = await GetCurrentAcademicYear(context);
                if (string.IsNullOrEmpty(currentYear))
                {
                    MessageBox.Show("Chưa có năm học nào được kích hoạt!", "Thông báo",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Lấy cấp quản lý của user hiện tại
                var userLevel = GetUserManagementLevel();
                
                // Load thống kê dựa trên cấp
                var statistics = await GetStatisticsByLevel(context, currentYear, userLevel);
                
                // Extract values for cards
                int totalStudents = 0, achievedStudents = 0, totalEvidence = 0, approvedEvidence = 0;
                
                foreach (var stat in statistics)
                {
                    if (stat.ChiTieu.Contains("Tổng số sinh viên"))
                        int.TryParse(stat.GiaTri.Replace(",", ""), out totalStudents);
                    else if (stat.ChiTieu.Contains("Sinh viên đạt danh hiệu"))
                        int.TryParse(stat.GiaTri.Replace(",", ""), out achievedStudents);
                    else if (stat.ChiTieu.Contains("Tổng số minh chứng"))
                        int.TryParse(stat.GiaTri.Replace(",", ""), out totalEvidence);
                    else if (stat.ChiTieu.Contains("Minh chứng đã duyệt"))
                        int.TryParse(stat.GiaTri.Replace(",", ""), out approvedEvidence);
                }
                
                // Update Statistics Cards
                UpdateStatisticsCards(totalStudents, achievedStudents, totalEvidence, approvedEvidence);
                
                // Hiển thị lên DataGridView
                DisplayStatistics(statistics);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải thống kê: {ex.Message}", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Xử lý khi click nút "Tạo báo cáo"
        /// </summary>
        private async void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                var reportType = cmbReportType.SelectedItem?.ToString();
                if (string.IsNullOrEmpty(reportType))
                {
                    MessageBox.Show("Vui lòng chọn loại báo cáo!", "Thông báo",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Hiển thị loading
                UpdateReportDataGridView(null, null);
                UpdateReportLabel("Đang tạo báo cáo...");
                
                using var scope = _serviceProvider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();
                
                var currentYear = await GetCurrentAcademicYear(context);

                switch (reportType)
                {
                    case "Danh sách sinh viên đạt danh hiệu":
                        await GenerateStudentAchievementReport(context, currentYear);
                        break;
                        
                    case "Thống kê theo tiêu chí":
                        await GenerateCriteriaStatisticsReport(context, currentYear);
                        break;
                        
                    case "Báo cáo tổng hợp":
                        await GenerateSummaryReport(context, currentYear);
                        break;
                        
                    case "Tiến độ xét duyệt":
                        await GenerateApprovalProgressReport(context, currentYear);
                        break;
                        
                    default:
                        MessageBox.Show("Loại báo cáo không được hỗ trợ!", "Thông báo",
                                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                }
                
                UpdateReportLabel($"📊 {reportType}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tạo báo cáo: {ex.Message}", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateReportLabel("Báo cáo & Thống kê");
            }
        }

        /// <summary>
        /// <summary>
        /// Xử lý khi click nút "Xuất Excel"
        /// </summary>
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            // Mở form xuất báo cáo mới (SimpleReportForm)
            OpenSimpleReportForm();
        }

        /// <summary>
        /// Mở form SimpleReportForm để xuất báo cáo Excel
        /// </summary>
        private void OpenSimpleReportForm()
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<StudentManagementDbContext>>();
                var reportService = scope.ServiceProvider.GetRequiredService<SimpleReportService>();
                
                var reportForm = new SimpleReportForm(contextFactory, reportService, _currentUser);
                reportForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi mở form báo cáo: {ex.Message}", "Lỗi",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Báo cáo chi tiết - Generate Methods

        /// <summary>
        /// Báo cáo: Danh sách sinh viên đạt danh hiệu
        /// </summary>
        private async Task GenerateStudentAchievementReport(StudentManagementDbContext context, string academicYear)
        {
            var level = cmbReportLevel.SelectedItem?.ToString() ?? "Tất cả";
            var userLevel = GetUserManagementLevel();
            
            var query = context.KetQuaDanhHieus
                .Include(k => k.SinhVien)
                    .ThenInclude(sv => sv.Lop)
                        .ThenInclude(l => l.Khoa)
                .Where(k => k.MaNH == academicYear && k.DatDanhHieu == true);

            // Lọc theo cấp quản lý của user
            query = ApplyUserLevelFilter(query, userLevel);

            // Lọc theo cấp được chọn
            if (level != "Tất cả")
            {
                var levelCode = GetLevelCode(level);
                query = query.Where(k => k.MaCap == levelCode);
            }

            // Fetch data from database first (without calling C# method in LINQ)
            var rawResults = await query
                .OrderByDescending(k => k.NgayDat)
                .Select(k => new
                {
                    MaSV = k.SinhVien.MaSV,
                    HoTen = k.SinhVien.HoTen,
                    Lop = k.SinhVien.Lop.TenLop,
                    Khoa = k.SinhVien.Lop.Khoa.TenKhoa,
                    MaCap = k.MaCap, // Keep raw code
                    NgayDat = k.NgayDat,
                    GhiChu = k.GhiChu
                })
                .ToListAsync();

            // Apply C# method transformation after data is fetched
            var results = rawResults.Select(k => new
            {
                k.MaSV,
                k.HoTen,
                k.Lop,
                k.Khoa,
                CapDat = GetLevelName(k.MaCap), // Now safe to call C# method
                k.NgayDat,
                k.GhiChu
            }).ToList();

            // Thread-safe update DataGridView
            UpdateReportDataGridView(results, grid =>
            {
                grid.Columns["MaSV"].HeaderText = "Mã SV";
                grid.Columns["MaSV"].Width = 100;
                grid.Columns["HoTen"].HeaderText = "Họ và Tên";
                grid.Columns["HoTen"].Width = 200;
                grid.Columns["Lop"].HeaderText = "Lớp";
                grid.Columns["Lop"].Width = 120;
                grid.Columns["Khoa"].HeaderText = "Khoa";
                grid.Columns["Khoa"].Width = 150;
                grid.Columns["CapDat"].HeaderText = "Cấp đạt";
                grid.Columns["CapDat"].Width = 100;
                grid.Columns["NgayDat"].HeaderText = "Ngày đạt";
                grid.Columns["NgayDat"].Width = 100;
                grid.Columns["GhiChu"].HeaderText = "Ghi chú";
                grid.Columns["GhiChu"].Width = 200;
            });
        }

        /// <summary>
        /// Báo cáo: Thống kê theo tiêu chí
        /// </summary>
        private async Task GenerateCriteriaStatisticsReport(StudentManagementDbContext context, string academicYear)
        {
            var userLevel = GetUserManagementLevel();
            
            // Lấy tất cả tiêu chí
            var criteria = await context.TieuChis.OrderBy(tc => tc.MaTC).ToListAsync();
            
            var statistics = new List<object>();

            foreach (var criterion in criteria)
            {
                // Đếm số minh chứng đạt theo tiêu chí
                var approvedCount = await context.MinhChungs
                    .Where(mc => mc.MaTC == criterion.MaTC && 
                                mc.MaNH == academicYear && 
                                mc.TrangThai == TrangThaiMinhChung.DaDuyet)
                    .CountAsync();

                // Đếm tổng số minh chứng
                var totalCount = await context.MinhChungs
                    .Where(mc => mc.MaTC == criterion.MaTC && mc.MaNH == academicYear)
                    .CountAsync();

                // Tính tỷ lệ
                var percentage = totalCount > 0 ? (approvedCount * 100.0 / totalCount) : 0;

                statistics.Add(new
                {
                    MaTieuChi = criterion.MaTC,
                    TenTieuChi = criterion.TenTieuChi,
                    NhomTieuChi = GetCriteriaGroupName(criterion.MaTC),
                    SoMinhChungDaDuyet = approvedCount,
                    TongSoMinhChung = totalCount,
                    TyLe = $"{percentage:F1}%",
                    DiemTrungBinh = percentage
                });
            }

            UpdateReportDataGridView(statistics.OrderByDescending(s => ((dynamic)s).DiemTrungBinh).ToList(), grid =>
            {
                grid.Columns["MaTieuChi"].HeaderText = "Mã TC";
                grid.Columns["MaTieuChi"].Width = 80;
                grid.Columns["TenTieuChi"].HeaderText = "Tên tiêu chí";
                grid.Columns["TenTieuChi"].Width = 250;
                grid.Columns["NhomTieuChi"].HeaderText = "Nhóm";
                grid.Columns["NhomTieuChi"].Width = 120;
                grid.Columns["SoMinhChungDaDuyet"].HeaderText = "Đã duyệt";
                grid.Columns["SoMinhChungDaDuyet"].Width = 100;
                grid.Columns["TongSoMinhChung"].HeaderText = "Tổng số";
                grid.Columns["TongSoMinhChung"].Width = 100;
                grid.Columns["TyLe"].HeaderText = "Tỷ lệ";
                grid.Columns["TyLe"].Width = 80;
                grid.Columns["DiemTrungBinh"].Visible = false;
            });
        }

        /// <summary>
        /// Báo cáo: Báo cáo tổng hợp
        /// </summary>
        private async Task GenerateSummaryReport(StudentManagementDbContext context, string academicYear)
        {
            var userLevel = GetUserManagementLevel();
            
            // Thống kê theo đơn vị
            var summaries = new List<object>();

            if (userLevel == "LOP" || userLevel == "KHOA" || userLevel == "TRUONG" || userLevel == "TU")
            {
                // Thống kê theo Khoa
                var faculties = await context.Khoas.ToListAsync();
                
                foreach (var faculty in faculties)
                {
                    var totalStudents = await context.SinhViens
                        .Where(sv => sv.Lop.MaKhoa == faculty.MaKhoa)
                        .CountAsync();

                    var achievedStudents = await context.KetQuaDanhHieus
                        .Where(k => k.MaNH == academicYear && 
                                   k.DatDanhHieu == true &&
                                   k.SinhVien.Lop.MaKhoa == faculty.MaKhoa)
                        .Select(k => k.MaSV)
                        .Distinct()
                        .CountAsync();

                    var percentage = totalStudents > 0 ? (achievedStudents * 100.0 / totalStudents) : 0;

                    summaries.Add(new
                    {
                        DonVi = faculty.TenKhoa,
                        LoaiDonVi = "Khoa",
                        TongSoSinhVien = totalStudents,
                        SoSVDatDanhHieu = achievedStudents,
                        TyLe = $"{percentage:F1}%",
                        XepHang = percentage
                    });
                }
            }

            if (summaries.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu báo cáo tổng hợp!", "Thông báo",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            UpdateReportDataGridView(summaries.OrderByDescending(s => ((dynamic)s).XepHang).ToList(), grid =>
            {
                grid.Columns["DonVi"].HeaderText = "Đơn vị";
                grid.Columns["DonVi"].Width = 250;
                grid.Columns["LoaiDonVi"].HeaderText = "Loại";
                grid.Columns["LoaiDonVi"].Width = 80;
                grid.Columns["TongSoSinhVien"].HeaderText = "Tổng SV";
                grid.Columns["TongSoSinhVien"].Width = 100;
                grid.Columns["SoSVDatDanhHieu"].HeaderText = "SV đạt";
                grid.Columns["SoSVDatDanhHieu"].Width = 100;
                grid.Columns["TyLe"].HeaderText = "Tỷ lệ";
                grid.Columns["TyLe"].Width = 100;
                grid.Columns["XepHang"].Visible = false;
            });
        }

        /// <summary>
        /// Báo cáo: Tiến độ xét duyệt
        /// </summary>
        private async Task GenerateApprovalProgressReport(StudentManagementDbContext context, string academicYear)
        {
            var userLevel = GetUserManagementLevel();
            
            var progressData = new List<object>();

            // Lấy danh sách sinh viên trong phạm vi quản lý
            var studentsQuery = context.SinhViens.AsQueryable();
            
            // Lọc theo cấp quản lý
            studentsQuery = ApplyUserLevelFilterForStudents(studentsQuery, userLevel);

            var students = await studentsQuery.Take(100).ToListAsync(); // Giới hạn 100 để tránh quá tải

            foreach (var student in students)
            {
                // Đếm minh chứng
                var totalEvidence = await context.MinhChungs
                    .Where(mc => mc.MaSV == student.MaSV && mc.MaNH == academicYear)
                    .CountAsync();

                var approvedEvidence = await context.MinhChungs
                    .Where(mc => mc.MaSV == student.MaSV && 
                                mc.MaNH == academicYear && 
                                mc.TrangThai == TrangThaiMinhChung.DaDuyet)
                    .CountAsync();

                var rejectedEvidence = await context.MinhChungs
                    .Where(mc => mc.MaSV == student.MaSV && 
                                mc.MaNH == academicYear && 
                                mc.TrangThai == TrangThaiMinhChung.BiTuChoi)
                    .CountAsync();

                var pendingEvidence = totalEvidence - approvedEvidence - rejectedEvidence;

                var progressPercentage = totalEvidence > 0 ? (approvedEvidence * 100.0 / totalEvidence) : 0;

                progressData.Add(new
                {
                    MaSV = student.MaSV,
                    HoTen = student.HoTen,
                    TongMinhChung = totalEvidence,
                    DaDuyet = approvedEvidence,
                    BiTuChoi = rejectedEvidence,
                    ChoDuyet = pendingEvidence,
                    TienDo = $"{progressPercentage:F1}%",
                    TrangThai = progressPercentage >= 80 ? "Hoàn thành tốt" : 
                               progressPercentage >= 50 ? "Đang thực hiện" : "Chậm tiến độ"
                });
            }

            UpdateReportDataGridView(progressData, grid =>
            {
                grid.Columns["MaSV"].HeaderText = "Mã SV";
                grid.Columns["MaSV"].Width = 100;
                grid.Columns["HoTen"].HeaderText = "Họ và Tên";
                grid.Columns["HoTen"].Width = 200;
                grid.Columns["TongMinhChung"].HeaderText = "Tổng MC";
                grid.Columns["TongMinhChung"].Width = 80;
                grid.Columns["DaDuyet"].HeaderText = "Đã duyệt";
                grid.Columns["DaDuyet"].Width = 80;
                grid.Columns["BiTuChoi"].HeaderText = "Bị từ chối";
                grid.Columns["BiTuChoi"].Width = 80;
                grid.Columns["ChoDuyet"].HeaderText = "Chờ duyệt";
                grid.Columns["ChoDuyet"].Width = 80;
                grid.Columns["TienDo"].HeaderText = "Tiến độ";
                grid.Columns["TienDo"].Width = 100;
                grid.Columns["TrangThai"].HeaderText = "Trạng thái";
                grid.Columns["TrangThai"].Width = 130;
            });
        }

        #endregion

        #region Helper Methods - Thống kê

        /// <summary>
        /// Lấy thống kê theo cấp quản lý
        /// </summary>
        private async Task<List<StatisticDto>> GetStatisticsByLevel(StudentManagementDbContext context, string academicYear, string level)
        {
            var stats = new List<StatisticDto>();

            // Tổng số sinh viên
            var totalStudents = await context.SinhViens.CountAsync();
            
            // Số sinh viên đạt danh hiệu
            var achievedStudents = await context.KetQuaDanhHieus
                .Where(k => k.MaNH == academicYear && k.DatDanhHieu == true)
                .Select(k => k.MaSV)
                .Distinct()
                .CountAsync();

            // Tổng minh chứng
            var totalEvidence = await context.MinhChungs
                .Where(mc => mc.MaNH == academicYear)
                .CountAsync();

            // Minh chứng đã duyệt
            var approvedEvidence = await context.MinhChungs
                .Where(mc => mc.MaNH == academicYear && mc.TrangThai == TrangThaiMinhChung.DaDuyet)
                .CountAsync();

            stats.Add(new StatisticDto
            {
                ChiTieu = "Tổng số sinh viên",
                GiaTri = totalStudents.ToString("N0", CultureInfo.CurrentCulture),
                DonVi = "sinh viên"
            });

            stats.Add(new StatisticDto
            {
                ChiTieu = "Sinh viên đạt danh hiệu",
                GiaTri = achievedStudents.ToString("N0", CultureInfo.CurrentCulture),
                DonVi = "sinh viên"
            });

            stats.Add(new StatisticDto
            {
                ChiTieu = "Tỷ lệ đạt danh hiệu",
                GiaTri = totalStudents > 0 ? $"{(achievedStudents * 100.0 / totalStudents):F2}" : "0.00",
                DonVi = "%"
            });

            stats.Add(new StatisticDto
            {
                ChiTieu = "Tổng số minh chứng",
                GiaTri = totalEvidence.ToString("N0", CultureInfo.CurrentCulture),
                DonVi = "minh chứng"
            });

            stats.Add(new StatisticDto
            {
                ChiTieu = "Minh chứng đã duyệt",
                GiaTri = approvedEvidence.ToString("N0", CultureInfo.CurrentCulture),
                DonVi = "minh chứng"
            });

            stats.Add(new StatisticDto
            {
                ChiTieu = "Tỷ lệ minh chứng đã duyệt",
                GiaTri = totalEvidence > 0 ? $"{(approvedEvidence * 100.0 / totalEvidence):F2}" : "0.00",
                DonVi = "%"
            });

            return stats;
        }

        /// <summary>
        /// Hiển thị thống kê lên DataGridView (Thread-safe)
        /// </summary>
        private void DisplayStatistics(List<StatisticDto> statistics)
        {
            if (dataGridViewReports.InvokeRequired)
            {
                dataGridViewReports.Invoke(new Action(() => DisplayStatistics(statistics)));
                return;
            }
            
            dataGridViewReports.DataSource = statistics;
            
            if (dataGridViewReports.Columns.Count > 0)
            {
                dataGridViewReports.Columns["ChiTieu"].HeaderText = "Chỉ tiêu";
                dataGridViewReports.Columns["ChiTieu"].Width = 300;
                dataGridViewReports.Columns["GiaTri"].HeaderText = "Giá trị";
                dataGridViewReports.Columns["GiaTri"].Width = 150;
                dataGridViewReports.Columns["GiaTri"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridViewReports.Columns["DonVi"].HeaderText = "Đơn vị";
                dataGridViewReports.Columns["DonVi"].Width = 150;
            }
        }

        /// <summary>
        /// Lấy cấp quản lý của user hiện tại
        /// </summary>
        private string GetUserManagementLevel()
        {
            return _currentUser.VaiTro switch
            {
                UserRoles.CVHT => "LOP",
                UserRoles.DOANKHOA => "KHOA",
                UserRoles.DOANTRUONG => "TRUONG",
                UserRoles.DOANTP => "TP",
                UserRoles.DOANTU => "TU",
                _ => "TU" // Mặc định là cấp cao nhất
            };
        }

        /// <summary>
        /// Lấy năm học hiện tại
        /// </summary>
        private async Task<string> GetCurrentAcademicYear(StudentManagementDbContext context)
        {
            // Lấy năm học gần nhất (không có IsActive property)
            var currentYear = await context.NamHocs
                .OrderByDescending(nh => nh.MaNH)
                .Select(nh => nh.MaNH)
                .FirstOrDefaultAsync();

            return currentYear ?? DateTime.Now.Year.ToString();
        }

        /// <summary>
        /// Áp dụng filter theo cấp quản lý của user
        /// </summary>
        private IQueryable<KetQuaDanhHieu> ApplyUserLevelFilter(IQueryable<KetQuaDanhHieu> query, string userLevel)
        {
            switch (userLevel)
            {
                case "LOP":
                    // Chỉ xem lớp mình quản lý
                    if (!string.IsNullOrEmpty(_currentUser.MaLop))
                        query = query.Where(k => k.SinhVien.MaLop == _currentUser.MaLop);
                    break;
                    
                case "KHOA":
                    // Xem cả khoa
                    if (!string.IsNullOrEmpty(_currentUser.MaKhoa))
                        query = query.Where(k => k.SinhVien.Lop.MaKhoa == _currentUser.MaKhoa);
                    break;
                    
                case "TRUONG":
                    // Xem cả trường
                    if (!string.IsNullOrEmpty(_currentUser.MaTruong))
                        query = query.Where(k => k.SinhVien.Lop.Khoa.MaTruong == _currentUser.MaTruong);
                    break;
                    
                // TP và TU xem tất cả
            }
            
            return query;
        }

        /// <summary>
        /// Áp dụng filter cho Students query
        /// </summary>
        private IQueryable<SinhVien> ApplyUserLevelFilterForStudents(IQueryable<SinhVien> query, string userLevel)
        {
            switch (userLevel)
            {
                case "LOP":
                    if (!string.IsNullOrEmpty(_currentUser.MaLop))
                        query = query.Where(sv => sv.MaLop == _currentUser.MaLop);
                    break;
                    
                case "KHOA":
                    if (!string.IsNullOrEmpty(_currentUser.MaKhoa))
                        query = query.Where(sv => sv.Lop.MaKhoa == _currentUser.MaKhoa);
                    break;
                    
                case "TRUONG":
                    if (!string.IsNullOrEmpty(_currentUser.MaTruong))
                        query = query.Where(sv => sv.Lop.Khoa.MaTruong == _currentUser.MaTruong);
                    break;
            }
            
            return query;
        }

        /// <summary>
        /// Chuyển đổi code cấp sang tên cấp
        /// </summary>
        private string GetLevelName(string levelCode)
        {
            return levelCode switch
            {
                "LOP" => "Lớp",
                "KHOA" => "Khoa",
                "TRUONG" => "Trường",
                "TP" => "Thành phố",
                "TU" => "Trung ương",
                _ => levelCode
            };
        }

        /// <summary>
        /// Chuyển đổi tên cấp sang code cấp
        /// </summary>
        private string GetLevelCode(string levelName)
        {
            return levelName switch
            {
                "Lớp" => "LOP",
                "Khoa" => "KHOA",
                "Trường" => "TRUONG",
                "Thành phố" => "TP",
                "Trung ương" => "TU",
                _ => levelName
            };
        }

        /// <summary>
        /// Lấy tên nhóm tiêu chí
        /// </summary>
        private string GetCriteriaGroupName(string criteriaCode)
        {
            if (criteriaCode.StartsWith("HT")) return "Học tập";
            if (criteriaCode.StartsWith("DD")) return "Đạo đức";
            if (criteriaCode.StartsWith("TL")) return "Thể lực";
            if (criteriaCode.StartsWith("TN")) return "Tình nguyện";
            if (criteriaCode.StartsWith("HN")) return "Hội nhập";
            return "Khác";
        }

        /// <summary>
        /// Xuất dữ liệu ra Excel (CSV format)
        /// </summary>
        private void ExportToExcel(string filePath)
        {
            using var writer = new System.IO.StreamWriter(filePath, false, System.Text.Encoding.UTF8);
            
            // Viết header
            var headers = new List<string>();
            foreach (DataGridViewColumn column in dataGridViewReports.Columns)
            {
                if (column.Visible)
                    headers.Add(column.HeaderText);
            }
            writer.WriteLine(string.Join(",", headers));

            // Viết data
            foreach (DataGridViewRow row in dataGridViewReports.Rows)
            {
                if (row.IsNewRow) continue;
                
                var values = new List<string>();
                foreach (DataGridViewColumn column in dataGridViewReports.Columns)
                {
                    if (column.Visible)
                    {
                        var value = row.Cells[column.Index].Value?.ToString() ?? "";
                        // Escape comma và quotes
                        if (value.Contains(",") || value.Contains("\""))
                            value = $"\"{value.Replace("\"", "\"\"")}\"";
                        values.Add(value);
                    }
                }
                writer.WriteLine(string.Join(",", values));
            }
        }

        /// <summary>
        /// Helper method: Update DataGridView thread-safe
        /// </summary>
        private void UpdateReportDataGridView(object? dataSource, Action<DataGridView>? formatColumns)
        {
            if (dataGridViewReports.InvokeRequired)
            {
                dataGridViewReports.Invoke(new Action(() => UpdateReportDataGridView(dataSource, formatColumns)));
                return;
            }

            dataGridViewReports.DataSource = dataSource;
            
            if (dataGridViewReports.Columns.Count > 0 && formatColumns != null)
            {
                formatColumns(dataGridViewReports);
            }
        }

        /// <summary>
        /// Helper method: Update label thread-safe
        /// </summary>
        private void UpdateReportLabel(string text)
        {
            if (lblReportType.InvokeRequired)
            {
                lblReportType.Invoke(new Action(() => UpdateReportLabel(text)));
                return;
            }
            
            lblReportType.Text = text;
        }

        #endregion
    }
}
