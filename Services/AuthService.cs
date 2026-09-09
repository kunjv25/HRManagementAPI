using HRManagementAPI.DTO.Auth;
using HRManagementAPI.Models;
using HRManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HRManagementAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        // Login user and generate JWT token
        public async Task<string?> LoginAsync(LoginDto dto)
        {
            // Find user by email
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                return null;

            // Check password
            var passwordValid = await _userManager.CheckPasswordAsync(user, dto.Password);

            if (!passwordValid)
                return null;

            // Get user's roles
            var roles = await _userManager.GetRolesAsync(user);

            // Create JWT claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email!)
            };

            // Add each role to JWT claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            // Get JWT key
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            // Create signing credentials
            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            // Create JWT token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);

            // Return JWT token as string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // Create login account for employee
        public async Task<string> CreateEmployeeAccountAsync(string email)
        {
            // Check whether login account already exists
            var existingUser = await _userManager.FindByEmailAsync(email);

            if (existingUser != null)
                throw new InvalidOperationException("Login account already exists for this employee.");

            // Generate random 4-digit number
            var randomNumber = RandomNumberGenerator.GetInt32(1000, 10000);

            // Generate temporary password
            var temporaryPassword = $"Tem@{randomNumber}";

            // Create ApplicationUser
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email
            };

            // Create user with temporary password
            var result = await _userManager.CreateAsync(user, temporaryPassword);

            if (!result.Succeeded)
                throw new InvalidOperationException(
                    string.Join(", ", result.Errors.Select(e => e.Description)));

            // Assign User role
            if (await _roleManager.RoleExistsAsync("User"))
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            return temporaryPassword;
        }

        // Change user password
        public async Task ChangePasswordAsync(string userId, ChangePasswordDto dto)
        {
            // Check new password and confirm password
            if (dto.NewPassword != dto.ConfirmPassword)
            {
                throw new InvalidOperationException("New password and confirm password do not match.");
            }

            // Find user by ID
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            // Change password
            var result = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);

            // Check result
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(
                    string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }
    }
}