using Microsoft.AspNetCore.Identity;
using RecruiterAid_Api.Domain.Entities;
using RecruiterAid_Api.Presentation.DTOs;
using System.Threading.Tasks;

namespace RecruiterAid_Api.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserProfileDto> GetCurrentUserProfileAsync(string userId)
        {
            Console.WriteLine($"UserService: Looking up userId = {userId}");

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                Console.WriteLine("UserService: User not found");
                return null;
            }

            Console.WriteLine($"UserService: Found user {user.Email}");

            var roles = await _userManager.GetRolesAsync(user);
            Console.WriteLine($"UserService: Roles = {string.Join(",", roles)}");

            return new UserProfileDto
            {
                Id = user.Id,
                Email = user.Email,
                DisplayName = user.DisplayName,
                Role = roles.Count > 0 ? roles[0] : "AGENT",
                TeamId = user.TeamId
            };
        }


    }
}
