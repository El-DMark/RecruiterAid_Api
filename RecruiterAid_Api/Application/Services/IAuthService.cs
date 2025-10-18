using System.Threading.Tasks;
using RecruiterAid_Api.Presentation.DTOs;

namespace RecruiterAid_Api.Application.Services
{
    public interface IAuthService
    {
        Task<string> AuthenticateAsync(LoginRequest request);
        Task<bool> RegisterAsync(RegisterRequest request);

    }
}