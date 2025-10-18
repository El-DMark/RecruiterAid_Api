using System.Threading.Tasks;
using RecruiterAid_Api.Presentation.DTOs;

namespace RecruiterAid_Api.Application.Services
{
    public interface IUserService
    {
        Task<UserProfileDto> GetCurrentUserProfileAsync(string userId);
    }
}
