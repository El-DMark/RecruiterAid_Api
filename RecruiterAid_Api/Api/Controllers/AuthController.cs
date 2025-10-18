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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _authService.AuthenticateAsync(request);
            if (token == null) return Unauthorized("Invalid credentials");
            return Ok(new { token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var success = await _authService.RegisterAsync(request);
            if (!success) return BadRequest("Registration failed or role does not exist");
            return Ok("User registered successfully");
        }
    }
}
