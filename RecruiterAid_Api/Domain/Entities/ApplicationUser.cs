using Microsoft.AspNetCore.Identity;

namespace RecruiterAid_Api.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public string TeamId { get; set; }
    }
}
