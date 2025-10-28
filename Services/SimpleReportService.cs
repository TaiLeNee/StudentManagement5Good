using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using StudentManagement5GoodTempp.DataAccess.Context;
using StudentManagement5GoodTempp.DataAccess.Entity;

namespace StudentManagement5GoodTempp.Services
{
    /// <summary>
    /// Service xuất báo cáo đơn giản sử dụng ClosedXML
    /// </summary>
    public class SimpleReportService
    {
        private readonly StudentManagementDbContext _context;

        public SimpleReportService(StudentManagementDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Xuất báo cáo danh sách sinh viên 5 tốt
        /// </summary>
        public async Task<string> ExportStudentReportAsync(string namHoc = null)
        {
            try
            {
                // Lấy năm học hiện tại nếu không chỉ định
                if (string.IsNullOrEmpty(namHoc))
                {
                    var currentYear = await _context.NamHocs
                        .OrderByDescending(nh => nh.TuNgay)
                        .FirstOrDefaultAsync();
                    namHoc = currentYear?.MaNH ?? DateTime.Now.Year.ToString();
                }

                // Lấy dữ liệu sinh viên
                var students = await _context.SinhViens
                    .Include(s => s.Lop)
                    .ThenInclude(l => l.Khoa)
                    .ThenInclude(k => k.Truong)
                    .Include(s => s.MinhChungs.Where(m => m.MaNH == namHoc))
                    .Include(s => s.KetQuaDanhHieus.Where(k => k.MaNH == namHoc))
                    .ToListAsync();

                // Tạo file Excel
                var fileName = $"BaoCao_SinhVien5Tot_{namHoc}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                var filePath = Path.Combine(Path.GetTempPath(), fileName);

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("DanhSachSinhVien");

                    // Tạo header
                    CreateStudentReportHeader(worksheet);

                    // Thêm dữ liệu
                    int row = 2;
                    foreach (var student in students)
                    {
                        var reportData = CreateStudentReportData(student, namHoc);
                        AddStudentReportRow(worksheet, row, reportData);
                        row++;
                    }

                    // Auto-fit columns
                    worksheet.Columns().AdjustToContents();

                    // Lưu file
                    workbook.SaveAs(filePath);
                }

                return filePath;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi xuất báo cáo sinh viên: {ex.Message}");
            }
        }

        /// <summary>
        /// Xuất báo cáo minh chứng theo tiêu chí
        /// </summary>
        public async Task<string> ExportEvidenceReportAsync(string namHoc = null)
        {
            try
            {
                // Lấy năm học hiện tại nếu không chỉ định
                if (string.IsNullOrEmpty(namHoc))
                {
                    var currentYear = await _context.NamHocs
                        .OrderByDescending(nh => nh.TuNgay)
                        .FirstOrDefaultAsync();
                    namHoc = currentYear?.MaNH ?? DateTime.Now.Year.ToString();
                }

                // Lấy dữ liệu minh chứng
                var evidences = await _context.MinhChungs
                    .Include(m => m.SinhVien)
                    .Include(m => m.TieuChi)
                    .Where(m => m.MaNH == namHoc)
                    .ToListAsync();

                // Tạo file Excel
                var fileName = $"BaoCao_MinhChung_{namHoc}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                var filePath = Path.Combine(Path.GetTempPath(), fileName);

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("MinhChung");

                    // Tạo header
                    CreateEvidenceReportHeader(worksheet);

                    // Thêm dữ liệu
                    int row = 2;
                    foreach (var evidence in evidences)
                    {
                        AddEvidenceReportRow(worksheet, row, evidence);
                        row++;
                    }

                    // Auto-fit columns
                    worksheet.Columns().AdjustToContents();

                    // Lưu file
                    workbook.SaveAs(filePath);
                }

                return filePath;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi xuất báo cáo minh chứng: {ex.Message}");
            }
        }

        /// <summary>
        /// Xuất báo cáo thống kê tổng quan
        /// </summary>
        public async Task<string> ExportStatisticsReportAsync(string namHoc = null)
        {
            try
            {
                // Lấy năm học hiện tại nếu không chỉ định
                if (string.IsNullOrEmpty(namHoc))
                {
                    var currentYear = await _context.NamHocs
                        .OrderByDescending(nh => nh.TuNgay)
                        .FirstOrDefaultAsync();
                    namHoc = currentYear?.MaNH ?? DateTime.Now.Year.ToString();
                }

                // Tạo file Excel
                var fileName = $"BaoCao_ThongKe_{namHoc}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                var filePath = Path.Combine(Path.GetTempPath(), fileName);

                using (var workbook = new XLWorkbook())
                {
                    // Sheet 1: Thống kê tổng quan
                    var statsWorksheet = workbook.Worksheets.Add("ThongKeTongQuan");
                    await CreateStatisticsReport(statsWorksheet, namHoc);

                    // Sheet 2: Thống kê theo tiêu chí
                    var criteriaWorksheet = workbook.Worksheets.Add("ThongKeTieuChi");
                    await CreateCriteriaStatisticsReport(criteriaWorksheet, namHoc);

                    // Auto-fit columns
                    statsWorksheet.Columns().AdjustToContents();
                    criteriaWorksheet.Columns().AdjustToContents();

                    // Lưu file
                    workbook.SaveAs(filePath);
                }

                return filePath;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi xuất báo cáo thống kê: {ex.Message}");
            }
        }

        #region Private Helper Methods

        private void CreateStudentReportHeader(IXLWorksheet worksheet)
        {
            // Headers
            worksheet.Cell(1, 1).Value = "Mã SV";
            worksheet.Cell(1, 2).Value = "Họ tên";
            worksheet.Cell(1, 3).Value = "Lớp";
            worksheet.Cell(1, 4).Value = "Khoa";
            worksheet.Cell(1, 5).Value = "Trường";
            worksheet.Cell(1, 6).Value = "Năm học";
            worksheet.Cell(1, 7).Value = "Trạng thái";
            worksheet.Cell(1, 8).Value = "Minh chứng đã duyệt";
            worksheet.Cell(1, 9).Value = "Minh chứng chờ duyệt";
            worksheet.Cell(1, 10).Value = "Minh chứng bị từ chối";
            worksheet.Cell(1, 11).Value = "Danh hiệu đạt được";
            worksheet.Cell(1, 12).Value = "Ngày hoàn thành";

            // Style headers
            var headerRange = worksheet.Range(1, 1, 1, 12);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;
            headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }

        private StudentReportDto CreateStudentReportData(SinhVien student, string namHoc)
        {
            var approvedCount = student.MinhChungs?.Count(m => m.TrangThai == TrangThaiMinhChung.DaDuyet) ?? 0;
            var pendingCount = student.MinhChungs?.Count(m => m.TrangThai == TrangThaiMinhChung.ChoDuyet) ?? 0;
            var rejectedCount = student.MinhChungs?.Count(m => m.TrangThai == TrangThaiMinhChung.BiTuChoi) ?? 0;

            var finalResult = student.KetQuaDanhHieus?.FirstOrDefault(k => k.MaNH == namHoc);
            var danhHieu = finalResult?.DatDanhHieu == true ? $"Sinh viên 5 Tốt (Cấp {finalResult.MaCap})" : "Chưa đạt";

            var status = approvedCount == 0 ? "Chưa bắt đầu" :
                        approvedCount < 5 ? $"Đang tiến hành ({approvedCount}/5)" :
                        finalResult?.DatDanhHieu == true ? "Đã hoàn thành" : "Chờ xét duyệt";

            return new StudentReportDto
            {
                MaSV = student.MaSV ?? "N/A",
                HoTen = student.HoTen ?? "N/A",
                Lop = student.Lop?.TenLop ?? "N/A",
                Khoa = student.Lop?.Khoa?.TenKhoa ?? "N/A",
                Truong = student.Lop?.Khoa?.Truong?.TenTruong ?? "N/A",
                NamHoc = namHoc,
                TrangThaiTongQuat = status,
                SoMinhChungDaDuyet = approvedCount,
                SoMinhChungChoDuyet = pendingCount,
                SoMinhChungBiTuChoi = rejectedCount,
                DanhHieuDatDuoc = danhHieu,
                NgayHoanThanh = finalResult?.NgayDat
            };
        }

        private void AddStudentReportRow(IXLWorksheet worksheet, int row, StudentReportDto data)
        {
            worksheet.Cell(row, 1).Value = data.MaSV;
            worksheet.Cell(row, 2).Value = data.HoTen;
            worksheet.Cell(row, 3).Value = data.Lop;
            worksheet.Cell(row, 4).Value = data.Khoa;
            worksheet.Cell(row, 5).Value = data.Truong;
            worksheet.Cell(row, 6).Value = data.NamHoc;
            worksheet.Cell(row, 7).Value = data.TrangThaiTongQuat;
            worksheet.Cell(row, 8).Value = data.SoMinhChungDaDuyet;
            worksheet.Cell(row, 9).Value = data.SoMinhChungChoDuyet;
            worksheet.Cell(row, 10).Value = data.SoMinhChungBiTuChoi;
            worksheet.Cell(row, 11).Value = data.DanhHieuDatDuoc;
            worksheet.Cell(row, 12).Value = data.NgayHoanThanh?.ToString("dd/MM/yyyy") ?? "";
        }

        private void CreateEvidenceReportHeader(IXLWorksheet worksheet)
        {
            // Headers
            worksheet.Cell(1, 1).Value = "Mã SV";
            worksheet.Cell(1, 2).Value = "Họ tên";
            worksheet.Cell(1, 3).Value = "Mã tiêu chí";
            worksheet.Cell(1, 4).Value = "Tên tiêu chí";
            worksheet.Cell(1, 5).Value = "Tên minh chứng";
            worksheet.Cell(1, 6).Value = "Ngày nộp";
            worksheet.Cell(1, 7).Value = "Trạng thái";
            worksheet.Cell(1, 8).Value = "Người duyệt";
            worksheet.Cell(1, 9).Value = "Ngày duyệt";
            worksheet.Cell(1, 10).Value = "Ghi chú";

            // Style headers
            var headerRange = worksheet.Range(1, 1, 1, 10);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightGreen;
            headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }

        private void AddEvidenceReportRow(IXLWorksheet worksheet, int row, MinhChung evidence)
        {
            worksheet.Cell(row, 1).Value = evidence.MaSV;
            worksheet.Cell(row, 2).Value = evidence.SinhVien?.HoTen ?? "N/A";
            worksheet.Cell(row, 3).Value = evidence.MaTC;
            worksheet.Cell(row, 4).Value = evidence.TieuChi?.TenTieuChi ?? "N/A";
            worksheet.Cell(row, 5).Value = evidence.TenMinhChung;
            worksheet.Cell(row, 6).Value = evidence.NgayNop.ToString("dd/MM/yyyy");
            worksheet.Cell(row, 7).Value = evidence.TrangThai.ToDisplayString();
            worksheet.Cell(row, 8).Value = evidence.NguoiDuyetUser?.HoTen ?? "N/A";
            worksheet.Cell(row, 9).Value = evidence.NgayDuyet?.ToString("dd/MM/yyyy") ?? "";
            worksheet.Cell(row, 10).Value = evidence.GhiChu ?? "";

            // Set row color based on status
            var rowRange = worksheet.Range(row, 1, row, 10);
            switch (evidence.TrangThai)
            {
                case TrangThaiMinhChung.DaDuyet:
                    rowRange.Style.Fill.BackgroundColor = XLColor.LightGreen;
                    break;
                case TrangThaiMinhChung.BiTuChoi:
                    rowRange.Style.Fill.BackgroundColor = XLColor.LightCoral;
                    break;
                case TrangThaiMinhChung.CanBoSung:
                    rowRange.Style.Fill.BackgroundColor = XLColor.LightYellow;
                    break;
            }
        }

        private async Task CreateStatisticsReport(IXLWorksheet worksheet, string namHoc)
        {
            // Lấy dữ liệu thống kê
            var totalStudents = await _context.SinhViens.CountAsync();
            var participatingStudents = await _context.MinhChungs
                .Where(m => m.MaNH == namHoc)
                .Select(m => m.MaSV)
                .Distinct()
                .CountAsync();
            
            var completedStudents = await _context.KetQuaDanhHieus
                .Where(k => k.MaNH == namHoc)
                .CountAsync();
            
            var awardedStudents = await _context.KetQuaDanhHieus
                .Where(k => k.MaNH == namHoc && k.DatDanhHieu)
                .CountAsync();

            // Headers
            worksheet.Cell(1, 1).Value = "THỐNG KÊ TỔNG QUAN";
            worksheet.Cell(1, 1).Style.Font.Bold = true;
            worksheet.Cell(1, 1).Style.Font.FontSize = 16;

            // Data
            worksheet.Cell(3, 1).Value = "Năm học:";
            worksheet.Cell(3, 2).Value = namHoc;
            worksheet.Cell(4, 1).Value = "Tổng số sinh viên:";
            worksheet.Cell(4, 2).Value = totalStudents;
            worksheet.Cell(5, 1).Value = "Số sinh viên tham gia:";
            worksheet.Cell(5, 2).Value = participatingStudents;
            worksheet.Cell(6, 1).Value = "Số sinh viên hoàn thành:";
            worksheet.Cell(6, 2).Value = completedStudents;
            worksheet.Cell(7, 1).Value = "Số sinh viên đạt danh hiệu:";
            worksheet.Cell(7, 2).Value = awardedStudents;
            worksheet.Cell(8, 1).Value = "Tỷ lệ hoàn thành:";
            worksheet.Cell(8, 2).Value = totalStudents > 0 ? $"{((double)completedStudents / totalStudents * 100):F1}%" : "0%";
            worksheet.Cell(9, 1).Value = "Tỷ lệ đạt danh hiệu:";
            worksheet.Cell(9, 2).Value = totalStudents > 0 ? $"{((double)awardedStudents / totalStudents * 100):F1}%" : "0%";
        }

        private async Task CreateCriteriaStatisticsReport(IXLWorksheet worksheet, string namHoc)
        {
            // Headers
            worksheet.Cell(1, 1).Value = "THỐNG KÊ THEO TIÊU CHÍ";
            worksheet.Cell(1, 1).Style.Font.Bold = true;
            worksheet.Cell(1, 1).Style.Font.FontSize = 16;

            worksheet.Cell(3, 1).Value = "Tiêu chí";
            worksheet.Cell(3, 2).Value = "Số minh chứng";
            worksheet.Cell(3, 3).Value = "Đã duyệt";
            worksheet.Cell(3, 4).Value = "Chờ duyệt";
            worksheet.Cell(3, 5).Value = "Bị từ chối";

            // Style headers
            var headerRange = worksheet.Range(3, 1, 3, 5);
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.BackgroundColor = XLColor.LightBlue;

            // Lấy dữ liệu theo tiêu chí
            var criteriaStats = await _context.MinhChungs
                .Include(m => m.TieuChi)
                .Where(m => m.MaNH == namHoc)
                .GroupBy(m => new { m.MaTC, m.TieuChi.TenTieuChi })
                .Select(g => new
                {
                    TenTieuChi = g.Key.TenTieuChi,
                    Total = g.Count(),
                    Approved = g.Count(m => m.TrangThai == TrangThaiMinhChung.DaDuyet),
                    Pending = g.Count(m => m.TrangThai == TrangThaiMinhChung.ChoDuyet),
                    Rejected = g.Count(m => m.TrangThai == TrangThaiMinhChung.BiTuChoi)
                })
                .ToListAsync();

            // Thêm dữ liệu
            int row = 4;
            foreach (var stat in criteriaStats)
            {
                worksheet.Cell(row, 1).Value = stat.TenTieuChi;
                worksheet.Cell(row, 2).Value = stat.Total;
                worksheet.Cell(row, 3).Value = stat.Approved;
                worksheet.Cell(row, 4).Value = stat.Pending;
                worksheet.Cell(row, 5).Value = stat.Rejected;
                row++;
            }
        }

        #endregion
    }
}
