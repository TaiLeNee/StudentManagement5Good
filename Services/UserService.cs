using Microsoft.EntityFrameworkCore;
using StudentManagement5GoodTempp.DataAccess.Context;
using StudentManagement5GoodTempp.DataAccess.Entity;
using System.Security.Cryptography;
using System.Text;

namespace StudentManagement5GoodTempp.Services
{
    public class UserService : IUserService
    {
        private readonly StudentManagementDbContext _context;

        public UserService(StudentManagementDbContext context)
        {
            _context = context;
        }

        #region Authentication Methods

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            try
            {
                var user = await _context.Users
                    .Include(u => u.Khoa)
                    .Include(u => u.Lop)
                    .Include(u => u.SinhVien)
                    .Include(u => u.CapXet)
                    .FirstOrDefaultAsync(u => u.Username == username && u.TrangThai);

                if (user != null && VerifyPassword(password, user.Password))
                {
                    await UpdateLastLoginAsync(user.UserId);
                    return user;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null || !VerifyPassword(oldPassword, user.Password))
                    return false;

                user.Password = HashPassword(newPassword);
                user.NgayCapNhat = DateTime.Now;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ResetPasswordAsync(string userId, string newPassword)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                    return false;

                user.Password = HashPassword(newPassword);
                user.NgayCapNhat = DateTime.Now;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region CRUD Operations

        public async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                return await _context.Users
                    .Include(u => u.Khoa)
                    .Include(u => u.Lop)
                    .Include(u => u.SinhVien)
                    .Include(u => u.CapXet)
                    .OrderBy(u => u.HoTen)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return new List<User>();
            }
        }

        public async Task<User?> GetUserByIdAsync(string userId)
        {
            try
            {
                return await _context.Users
                    .Include(u => u.Khoa)
                    .Include(u => u.Lop)
                    .Include(u => u.SinhVien)
                    .Include(u => u.CapXet)
                    .FirstOrDefaultAsync(u => u.UserId == userId);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            try
            {
                return await _context.Users
                    .Include(u => u.Khoa)
                    .Include(u => u.Lop)
                    .Include(u => u.SinhVien)
                    .Include(u => u.CapXet)
                    .FirstOrDefaultAsync(u => u.Username == username);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<User>> GetUsersByRoleAsync(string role)
        {
            try
            {
                return await _context.Users
                    .Include(u => u.Khoa)
                    .Include(u => u.Lop)
                    .Include(u => u.SinhVien)
                    .Include(u => u.CapXet)
                    .Where(u => u.VaiTro == role && u.TrangThai)
                    .OrderBy(u => u.HoTen)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return new List<User>();
            }
        }

        public async Task<List<User>> GetUsersByKhoaAsync(string maKhoa)
        {
            try
            {
                return await _context.Users
                    .Include(u => u.Khoa)
                    .Include(u => u.Lop)
                    .Include(u => u.SinhVien)
                    .Include(u => u.CapXet)
                    .Where(u => u.MaKhoa == maKhoa && u.TrangThai)
                    .OrderBy(u => u.HoTen)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return new List<User>();
            }
        }

        public async Task<List<User>> GetUsersByLopAsync(string maLop)
        {
            try
            {
                return await _context.Users
                    .Include(u => u.Khoa)
                    .Include(u => u.Lop)
                    .Include(u => u.SinhVien)
                    .Include(u => u.CapXet)
                    .Where(u => u.MaLop == maLop && u.TrangThai)
                    .OrderBy(u => u.HoTen)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return new List<User>();
            }
        }

        public async Task<bool> AddUserAsync(User user)
        {
            try
            {
                // Validate unique constraints
                if (!await IsUsernameUniqueAsync(user.Username) || 
                    (!string.IsNullOrEmpty(user.Email) && !await IsEmailUniqueAsync(user.Email)))
                    return false;

                // Hash password
                user.Password = HashPassword(user.Password);
                user.NgayTao = DateTime.Now;

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            try
            {
                // Validate unique constraints (excluding current user)
                if (!await IsUsernameUniqueAsync(user.Username, user.UserId) ||
                    (!string.IsNullOrEmpty(user.Email) && !await IsEmailUniqueAsync(user.Email, user.UserId)))
                    return false;

                var existingUser = await _context.Users.FindAsync(user.UserId);
                if (existingUser == null)
                    return false;

                // Update properties (excluding password and creation date)
                existingUser.Username = user.Username;
                existingUser.HoTen = user.HoTen;
                existingUser.Email = user.Email;
                existingUser.SoDienThoai = user.SoDienThoai;
                existingUser.VaiTro = user.VaiTro;
                existingUser.CapQuanLy = user.CapQuanLy;
                existingUser.MaKhoa = user.MaKhoa;
                existingUser.MaLop = user.MaLop;
                existingUser.MaSV = user.MaSV;
                existingUser.TrangThai = user.TrangThai;
                existingUser.GhiChu = user.GhiChu;
                existingUser.NgayCapNhat = DateTime.Now;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                    return false;

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> ActivateUserAsync(string userId)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                    return false;

                user.TrangThai = true;
                user.NgayCapNhat = DateTime.Now;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeactivateUserAsync(string userId)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                    return false;

                user.TrangThai = false;
                user.NgayCapNhat = DateTime.Now;

                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region Business Logic Methods

        public async Task<bool> CanUserAccessKhoaAsync(string userId, string maKhoa)
        {
            try
            {
                var user = await GetUserByIdAsync(userId);
                if (user == null || !user.TrangThai)
                    return false;

                // Admin c� th? truy c?p t?t c?
                if (user.VaiTro == UserRoles.ADMIN)
                    return true;

                // User ph?i c� quy?n tr�n khoa n�y
                return user.MaKhoa == maKhoa || 
                       user.VaiTro == UserRoles.DOANTRUONG || 
                       user.VaiTro == UserRoles.DOANTP || 
                       user.VaiTro == UserRoles.DOANTU;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> CanUserAccessLopAsync(string userId, string maLop)
        {
            try
            {
                var user = await GetUserByIdAsync(userId);
                if (user == null || !user.TrangThai)
                    return false;

                // Admin c� th? truy c?p t?t c?
                if (user.VaiTro == UserRoles.ADMIN)
                    return true;

                // CVHT ch? c� th? truy c?p l?p ��?c ph�n c�ng
                if (user.VaiTro == UserRoles.CVHT)
                    return user.MaLop == maLop;

                // Ki?m tra quy?n truy c?p khoa c?a l?p
                var lop = await _context.Lops.FindAsync(maLop);
                if (lop != null)
                    return await CanUserAccessKhoaAsync(userId, lop.MaKhoa);

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> CanUserEvaluateAtLevelAsync(string userId, string capXet)
        {
            try
            {
                var user = await GetUserByIdAsync(userId);
                if (user == null || !user.TrangThai)
                    return false;

                // Mapping vai tr? v?i c?p x�t ��?c ph�p
                return capXet switch
                {
                    ManagementLevels.LOP => user.VaiTro == UserRoles.CVHT || user.VaiTro == UserRoles.ADMIN,
                    ManagementLevels.KHOA => user.VaiTro == UserRoles.DOANKHOA || user.VaiTro == UserRoles.GIAOVU || user.VaiTro == UserRoles.ADMIN,
                    ManagementLevels.TRUONG => user.VaiTro == UserRoles.DOANTRUONG || user.VaiTro == UserRoles.ADMIN,
                    ManagementLevels.TP => user.VaiTro == UserRoles.DOANTP || user.VaiTro == UserRoles.ADMIN,
                    ManagementLevels.TU => user.VaiTro == UserRoles.DOANTU || user.VaiTro == UserRoles.ADMIN,
                    _ => false
                };
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<User>> GetUsersForApprovalLevelAsync(string capXet, string? maKhoa = null)
        {
            try
            {
                var query = _context.Users
                    .Include(u => u.Khoa)
                    .Include(u => u.Lop)
                    .Where(u => u.TrangThai);

                // L?c theo c?p x�t
                query = capXet switch
                {
                    ManagementLevels.LOP => query.Where(u => u.VaiTro == UserRoles.CVHT),
                    ManagementLevels.KHOA => query.Where(u => u.VaiTro == UserRoles.DOANKHOA || u.VaiTro == UserRoles.GIAOVU),
                    ManagementLevels.TRUONG => query.Where(u => u.VaiTro == UserRoles.DOANTRUONG),
                    ManagementLevels.TP => query.Where(u => u.VaiTro == UserRoles.DOANTP),
                    ManagementLevels.TU => query.Where(u => u.VaiTro == UserRoles.DOANTU),
                    _ => query.Where(u => false)
                };

                // L?c theo khoa n?u c�
                if (!string.IsNullOrEmpty(maKhoa))
                    query = query.Where(u => u.MaKhoa == maKhoa);

                return await query.OrderBy(u => u.HoTen).ToListAsync();
            }
            catch (Exception)
            {
                return new List<User>();
            }
        }

        public async Task<bool> UpdateLastLoginAsync(string userId)
        {
            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                    return false;

                user.LanDangNhapCuoi = DateTime.Now;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region Utility Methods

        public string HashPassword(string password)
        {
            // Use BCrypt for secure password hashing
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            try
            {
                // Support both old SHA256 format and new BCrypt format
                // Old format: Base64 string (44 chars)
                // New format: BCrypt hash (starts with $2a$ or $2b$)
                
                if (hashedPassword.StartsWith("$2a$") || hashedPassword.StartsWith("$2b$"))
                {
                    // New BCrypt format
                    return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
                }
                else
                {
                    // Old SHA256 + Base64 format - for backward compatibility
                    using var sha256 = SHA256.Create();
                    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + "SALT_5TOT"));
                    var oldHash = Convert.ToBase64String(hashedBytes);
                    return oldHash == hashedPassword;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> IsUsernameUniqueAsync(string username, string? excludeUserId = null)
        {
            try
            {
                var query = _context.Users.Where(u => u.Username == username);
                if (!string.IsNullOrEmpty(excludeUserId))
                    query = query.Where(u => u.UserId != excludeUserId);

                return !await query.AnyAsync();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> IsEmailUniqueAsync(string email, string? excludeUserId = null)
        {
            try
            {
                var query = _context.Users.Where(u => u.Email == email);
                if (!string.IsNullOrEmpty(excludeUserId))
                    query = query.Where(u => u.UserId != excludeUserId);

                return !await query.AnyAsync();
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion
    }
}