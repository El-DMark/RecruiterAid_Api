using Microsoft.AspNetCore.Mvc;
using RecruiterAid_Api.Application.Services;
using RecruiterAid_Api.Presentation.DTOs;
using System.Threading.Tasks;

namespace RecruiterAid_Api.Api.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Authenticate a user and return a JWT token.
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Invalid login request" });

            var token = await _authService.AuthenticateAsync(request);
            if (token == null)
                return Unauthorized(new { Message = "Invalid credentials" });

            return Ok(new
            {
                Message = "Login successful",
                Token = token
            });
        }

        /// <summary>
        /// Register a new user with role and optional manager assignment.
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Message = "Invalid registration request" });

            var success = await _authService.RegisterAsync(request);
            if (!success)
                return BadRequest(new { Message = "Registration failed or role does not exist" });

            return Ok(new
            {
                Message = "User registered successfully",
                User = new
                {
                    request.Email,
                    request.FullName,
                    request.Role,
                    request.ManagerId
                }
            });
        }
    }
}
