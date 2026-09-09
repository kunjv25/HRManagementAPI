using HRManagementAPI.DTO.Auth;

namespace HRManagementAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterDto dto);                                                  // Register a new user
        Task<string?> LoginAsync(LoginDto dto);                                                     // Login user and return JWT token
    }
}