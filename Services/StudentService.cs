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
        private readonly StudentManagementDbContext _context;

        public StudentService(StudentManagementDbContext context)
        {
            _context = context;
        }

        public async Task<List<SinhVien>> GetAllStudentsAsync()
        {
            return await _context.SinhViens
                .Include(s => s.Lop)
                .ThenInclude(l => l.Khoa)
                .ToListAsync();
        }

        public async Task<SinhVien?> GetStudentByIdAsync(string studentId)
        {
            return await _context.SinhViens
                .Include(s => s.Lop)
                .ThenInclude(l => l.Khoa)
                .FirstOrDefaultAsync(s => s.MaSV == studentId);
        }

        public async Task<bool> AddStudentAsync(SinhVien student)
        {
            try
            {
                _context.SinhViens.Add(student);
                await _context.SaveChangesAsync();
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
                _context.SinhViens.Update(student);
                await _context.SaveChangesAsync();
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
                var student = await _context.SinhViens.FindAsync(studentId);
                if (student != null)
                {
                    _context.SinhViens.Remove(student);
                    await _context.SaveChangesAsync();
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