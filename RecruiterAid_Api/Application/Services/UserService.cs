using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RecruiterAid_Api.Domain.Entities.Identity;
using RecruiterAid_Api.Infrastructure.Data;
using RecruiterAid_Api.Presentation.DTOs;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace RecruiterAid_Api.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _dbContext;


        public UserService(UserManager<AppUser> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Get the profile of the currently authenticated user by their Id.
        /// Includes roles and manager/agent relationships.
        /// </summary>
        public async Task<UserProfileDto?> GetCurrentUserProfileAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return null;

            var roles = await _userManager.GetRolesAsync(user);

            return new UserProfileDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName ?? user.UserName ?? string.Empty,
                Roles = roles,
                ManagerId = user.ManagerId
            };
        }

        /// <summary>
        /// Get all roles assigned to a user.
        /// </summary>
        public async Task<IList<string>> GetUserRolesAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new List<string>();

            return await _userManager.GetRolesAsync(user);
        }

        /// <summary>
        /// Get all agents reporting to a specific manager.
        /// </summary>
        public async Task<IEnumerable<UserProfileDto>> GetAgentsForManagerAsync(string managerId)
        {
            var agents = await _userManager.Users
                .Where(u => u.ManagerId == managerId)
                .ToListAsync();

            var result = new List<UserProfileDto>();
            foreach (var agent in agents)
            {
                var roles = await _userManager.GetRolesAsync(agent);
                result.Add(new UserProfileDto
                {
                    Id = agent.Id,
                    Email = agent.Email,
                    FullName = agent.FullName ?? agent.UserName ?? string.Empty,
                    Roles = roles,
                    ManagerId = agent.ManagerId
                });
            }

            return result;
        }

        /// <summary>
        /// Get a user profile by email.
        /// </summary>
        public async Task<UserProfileDto?> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return null;

            var roles = await _userManager.GetRolesAsync(user);

            return new UserProfileDto
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName ?? user.UserName ?? string.Empty,
                Roles = roles,
                ManagerId = user.ManagerId
            };
        }

        public async Task<ManagerTeamDto?> GetManagerTeamAsync(string managerId)
        {
            var manager = await _userManager.FindByIdAsync(managerId);
            if (manager == null) return null;

            var managerRoles = await _userManager.GetRolesAsync(manager);

            var managerProfile = new UserProfileDto
            {
                Id = manager.Id,
                Email = manager.Email,
                FullName = manager.FullName ?? manager.UserName ?? string.Empty,
                Roles = managerRoles,
                ManagerId = manager.ManagerId
            };

            var agents = await GetAgentsForManagerAsync(managerId);

            return new ManagerTeamDto
            {
                Manager = managerProfile,
                Agents = agents
            };
        }
       
        public async Task<IEnumerable<ManagerTeamDto>> GetAllManagerTeamsAsync()
        {
            // 1. Get all users in Manager role
            var managers = await (from user in _dbContext.Users
                                  join userRole in _dbContext.UserRoles on user.Id equals userRole.UserId
                                  join role in _dbContext.Roles on userRole.RoleId equals role.Id
                                  where role.Name == "Manager"
                                  select user).ToListAsync();

            var result = new List<ManagerTeamDto>();

            foreach (var manager in managers)
            {
                // 2. Get agents assigned to this manager
                var agents = await _dbContext.CandidateAgentAssignments
                    .Where(ca => ca.AssignedByUserId == manager.Id)
                    .Select(ca => new UserProfileDto
                    {
                        Id = ca.AgentUser.Id,
                        DisplayName = ca.AgentUser.FullName,
                        Email = ca.AgentUser.Email
                    })
                    .Distinct()
                    .ToListAsync();

                result.Add(new ManagerTeamDto
                {
                    Manager = new UserProfileDto
                    {
                        Id = manager.Id,
                        DisplayName = manager.FullName,
                        Email = manager.Email
                    },
                    Agents = agents
                });
            }

            return result;
        }

    }
}
