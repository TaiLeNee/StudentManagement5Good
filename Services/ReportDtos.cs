using System;
using System.Collections.Generic;

namespace StudentManagement5GoodTempp.Services
{
    /// <summary>
    /// DTO cho báo cáo sinh viên 5 tốt
    /// </summary>
    public class StudentReportDto
    {
        public string MaSV { get; set; } = string.Empty;
        public string HoTen { get; set; } = string.Empty;
        public string Lop { get; set; } = string.Empty;
        public string Khoa { get; set; } = string.Empty;
        public string Truong { get; set; } = string.Empty;
        public string NamHoc { get; set; } = string.Empty;
        public string TrangThaiTongQuat { get; set; } = string.Empty;
        public int SoMinhChungDaDuyet { get; set; }
        public int SoMinhChungChoDuyet { get; set; }
        public int SoMinhChungBiTuChoi { get; set; }
        public string DanhHieuDatDuoc { get; set; } = string.Empty;
        public DateTime? NgayHoanThanh { get; set; }
    }

    /// <summary>
    /// DTO cho báo cáo minh chứng theo tiêu chí
    /// </summary>
    public class EvidenceReportDto
    {
        public string MaSV { get; set; } = string.Empty;
        public string HoTen { get; set; } = string.Empty;
        public string MaTC { get; set; } = string.Empty;
        public string TenTieuChi { get; set; } = string.Empty;
        public int SoMinhChung { get; set; }
        public int SoMinhChungDaDuyet { get; set; }
        public int SoMinhChungChoDuyet { get; set; }
        public int SoMinhChungBiTuChoi { get; set; }
        public string TrangThai { get; set; } = string.Empty;
        public bool DatTieuChi { get; set; }
        public decimal? Diem { get; set; }
    }

    /// <summary>
    /// DTO cho báo cáo thống kê tổng quan
    /// </summary>
    public class StatisticsReportDto
    {
        public string NamHoc { get; set; } = string.Empty;
        public int TongSoSinhVien { get; set; }
        public int SoSinhVienThamGia { get; set; }
        public int SoSinhVienHoanThanh { get; set; }
        public int SoSinhVienDatDanhHieu { get; set; }
        public double TyLeHoanThanh { get; set; }
        public double TyLeDatDanhHieu { get; set; }
        public Dictionary<string, int> ThongKeTheoTieuChi { get; set; } = new Dictionary<string, int>();
    }
}
