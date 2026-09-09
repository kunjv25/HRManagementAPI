using HRManagementAPI.DTO.Auth;
using HRManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HRManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /***
         * 
         * Register a new user
         * 
         **/
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);

            if (!result)
                return Conflict(new { message = "A user with this email already exists." });

            return Ok(new { message = "User registered successfully." });
        }


        /***
         * 
         * Login user and return JWT token
         * 
         **/
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var token = await _authService.LoginAsync(dto);

            if (token == null)
                return Unauthorized(new { message = "Invalid email or password." });

            return Ok(new
            {
                message = "Login successful.",
                token = token
            });
        }
    }
}