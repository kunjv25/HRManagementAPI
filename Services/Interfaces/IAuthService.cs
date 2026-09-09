using HRManagementAPI.DTO.Auth;

namespace HRManagementAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(LoginDto dto);                                                     // Login user and return JWT token

        Task<string> CreateEmployeeAccountAsync(string email);                                      // Create login account for employee

        Task ChangePasswordAsync(string userId, ChangePasswordDto dto);                             // change password
    }
}