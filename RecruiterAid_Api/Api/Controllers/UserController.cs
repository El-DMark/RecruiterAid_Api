using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruiterAid_Api.Application.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RecruiterAid_Api.Api.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

       //[Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetProfile()
        {
            Console.WriteLine("UserController: /me endpoint hit");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Console.WriteLine($"Extracted userId: {userId}");

            if (string.IsNullOrEmpty(userId))
            {
                Console.WriteLine("❌ userId is null or empty");
                return Unauthorized("Invalid token or missing user ID claim");
            }

            var profile = await _userService.GetCurrentUserProfileAsync(userId);
            if (profile == null)
            {
                Console.WriteLine("❌ UserService: User not found");
                return NotFound("User not found");
            }

            Console.WriteLine($"✅ UserService: Found user {profile.Email}");
            return Ok(profile);
        }



        [HttpGet("ping")]
        public IActionResult Ping() => Ok("pong");

    }
}
