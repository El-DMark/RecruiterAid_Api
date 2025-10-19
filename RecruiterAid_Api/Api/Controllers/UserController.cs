using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruiterAid_Api.Application.Services;
using RecruiterAid_Api.Presentation.DTOs;
using System.Collections.Generic;
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

        /// <summary>
        /// Get the currently authenticated user's profile.
        /// </summary>
        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<UserProfileDto>> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var profile = await _userService.GetCurrentUserProfileAsync(userId);
            if (profile == null)
                return NotFound();

            return Ok(profile);
        }

        /// <summary>
        /// Get all agents reporting to a specific manager.
        /// Accessible to Admins and Managers.
        /// </summary>
        [Authorize(Policy = "CanAssignCandidates")] // Admins + Managers
        [HttpGet("{managerId}/agents")]
        public async Task<ActionResult<IEnumerable<UserProfileDto>>> GetAgentsForManager(string managerId)
        {
            var agents = await _userService.GetAgentsForManagerAsync(managerId);
            return Ok(agents);
        }

        /// <summary>
        /// Get a specific manager and their team of agents.
        /// Accessible to Admins only.
        /// </summary>
        [Authorize(Policy = "AdminOnly")] // 🔒 Admins only
        [HttpGet("{managerId}/team")]
        public async Task<ActionResult<ManagerTeamDto>> GetManagerTeam(string managerId)
        {
            var team = await _userService.GetManagerTeamAsync(managerId);
            if (team == null)
                return NotFound();

            return Ok(team);
        }

        /// <summary>
        /// Get all managers and their teams of agents.
        /// Accessible to Admins only.
        /// </summary>
        [Authorize(Policy = "AdminOnly")] // 🔒 Admins only
        [HttpGet("teams")]
        public async Task<ActionResult<IEnumerable<ManagerTeamDto>>> GetAllManagerTeams()
        {
            var teams = await _userService.GetAllManagerTeamsAsync();
            return Ok(teams);
        }

        /// <summary>
        /// Simple health check endpoint.
        /// </summary>
        [HttpGet("ping")]
        public IActionResult Ping() => Ok(new { Message = "pong" });
    }
}
