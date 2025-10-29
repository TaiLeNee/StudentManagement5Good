using System;

namespace StudentManagement5GoodTempp.Services
{
    /// <summary>
    /// DTO cho hiển thị thống kê trong DataGridView
    /// </summary>
    public class StatisticDto
    {
        public string ChiTieu { get; set; } = string.Empty;
        public string GiaTri { get; set; } = string.Empty; // Dùng string để hiển thị linh hoạt
        public string DonVi { get; set; } = string.Empty;
    }
}
