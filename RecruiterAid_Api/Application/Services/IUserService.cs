using System.Collections.Generic;
using System.Threading.Tasks;
using RecruiterAid_Api.Presentation.DTOs;

namespace RecruiterAid_Api.Application.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Get the profile of the currently authenticated user by their Id.
        /// Includes roles and manager/agent relationships.
        /// </summary>
        Task<UserProfileDto?> GetCurrentUserProfileAsync(string userId);

        /// <summary>
        /// Get all roles assigned to a user.
        /// </summary>
        Task<IList<string>> GetUserRolesAsync(string userId);

        /// <summary>
        /// Get all agents reporting to a specific manager.
        /// Useful for ManagerOwnTeam policy and dashboards.
        /// </summary>
        Task<IEnumerable<UserProfileDto>> GetAgentsForManagerAsync(string managerId);

        /// <summary>
        /// Get a user profile by email (optional helper).
        /// </summary>
        Task<UserProfileDto?> GetUserByEmailAsync(string email);

        Task<ManagerTeamDto?> GetManagerTeamAsync(string managerId);
        Task<IEnumerable<ManagerTeamDto>> GetAllManagerTeamsAsync();


    }
}
