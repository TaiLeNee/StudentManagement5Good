using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StudentManagement5GoodTempp.DataAccess.Context;
using StudentManagement5GoodTempp.DataAccess.Entity;

namespace StudentManagement5GoodTempp.Services
{
    public class ApprovalWorkflowService : IApprovalWorkflowService
    {
        private readonly IServiceProvider _serviceProvider;

        public ApprovalWorkflowService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task ChuyenHoSoLenCapTrenAsync(string maSV, string maNH, string maCapDaDat)
        {
            string? capMoi = GetNextLevel(maCapDaDat);
            if (capMoi == null)
            {
                // Đã là cấp cao nhất (TU), không cần làm gì
                return;
            }

            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<StudentManagementDbContext>();

            // Kiểm tra xem hồ sơ cấp mới đã tồn tại chưa
            var existingRecord = await context.KetQuaDanhHieus
                .FirstOrDefaultAsync(kq => kq.MaSV == maSV && kq.MaNH == maNH && kq.MaCap == capMoi);

            if (existingRecord == null)
            {
                // Tạo hồ sơ mới cho cấp tiếp theo ở trạng thái "Đang chờ duyệt"
                var newRecord = new KetQuaDanhHieu
                {
                    MaKQ = Guid.NewGuid().ToString("N").Substring(0, 20),
                    MaSV = maSV,
                    MaNH = maNH,
                    MaCap = capMoi,
                    DatDanhHieu = false, // Quan trọng: Đang chờ duyệt, chưa Đạt
                    TrangThaiWorkflow = "DangChoDuyet",
                    NgayDat = DateTime.Now // Có thể dùng làm ngày nhận hồ sơ
                };
                context.KetQuaDanhHieus.Add(newRecord);
                await context.SaveChangesAsync();
            }
        }

        private string? GetNextLevel(string currentLevel)
        {
            return currentLevel switch
            {
                ManagementLevels.LOP => ManagementLevels.KHOA,
                ManagementLevels.KHOA => ManagementLevels.TRUONG,
                ManagementLevels.TRUONG => ManagementLevels.TP,
                ManagementLevels.TP => ManagementLevels.TU,
                ManagementLevels.TU => null, // Cấp cuối
                _ => null
            };
        }
    }
}
