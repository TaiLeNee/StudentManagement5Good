using StudentManagement5GoodTempp.DataAccess.Entity;

namespace StudentManagement5GoodTempp.Services
{
    public interface IUserService
    {
        // Authentication methods
        Task<User?> AuthenticateAsync(string username, string password);
        Task<bool> ChangePasswordAsync(string userId, string oldPassword, string newPassword);
        Task<bool> ResetPasswordAsync(string userId, string newPassword);

        // CRUD operations
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(string userId);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<List<User>> GetUsersByRoleAsync(string role);
        Task<List<User>> GetUsersByKhoaAsync(string maKhoa);
        Task<List<User>> GetUsersByLopAsync(string maLop);
        Task<bool> AddUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<bool> DeleteUserAsync(string userId);
        Task<bool> ActivateUserAsync(string userId);
        Task<bool> DeactivateUserAsync(string userId);

        // Business logic methods
        Task<bool> CanUserAccessKhoaAsync(string userId, string maKhoa);
        Task<bool> CanUserAccessLopAsync(string userId, string maLop);
        Task<bool> CanUserEvaluateAtLevelAsync(string userId, string capXet);
        Task<List<User>> GetUsersForApprovalLevelAsync(string capXet, string? maKhoa = null);
        Task<bool> UpdateLastLoginAsync(string userId);
        
        // Utility methods
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
        Task<bool> IsUsernameUniqueAsync(string username, string? excludeUserId = null);
        Task<bool> IsEmailUniqueAsync(string email, string? excludeUserId = null);
    }
}