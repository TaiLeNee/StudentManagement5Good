using Microsoft.EntityFrameworkCore;
using StudentManagement5GoodTempp.DataAccess.Context;
using StudentManagement5GoodTempp.DataAccess.Entity;

namespace StudentManagement5GoodTempp.Services
{
    public interface IStudentService
    {
        Task<List<SinhVien>> GetAllStudentsAsync();
        Task<SinhVien?> GetStudentByIdAsync(string studentId);
        Task<bool> AddStudentAsync(SinhVien student);
        Task<bool> UpdateStudentAsync(SinhVien student);
        Task<bool> DeleteStudentAsync(string studentId);
    }

    public class StudentService : IStudentService
    {
        private readonly IDbContextFactory<StudentManagementDbContext> _contextFactory;

        public StudentService(IDbContextFactory<StudentManagementDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<List<SinhVien>> GetAllStudentsAsync()
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.SinhViens
                .Include(s => s.Lop)
                .ThenInclude(l => l.Khoa)
                .ToListAsync();
        }

        public async Task<SinhVien?> GetStudentByIdAsync(string studentId)
        {
            using var context = _contextFactory.CreateDbContext();
            return await context.SinhViens
                .Include(s => s.Lop)
                .ThenInclude(l => l.Khoa)
                .FirstOrDefaultAsync(s => s.MaSV == studentId);
        }

        public async Task<bool> AddStudentAsync(SinhVien student)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                context.SinhViens.Add(student);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateStudentAsync(SinhVien student)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                context.SinhViens.Update(student);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteStudentAsync(string studentId)
        {
            try
            {
                using var context = _contextFactory.CreateDbContext();
                var student = await context.SinhViens.FindAsync(studentId);
                if (student != null)
                {
                    context.SinhViens.Remove(student);
                    await context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}