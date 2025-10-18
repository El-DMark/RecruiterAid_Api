using System.Collections.Generic;

namespace RecruiterAid_Api.Presentation.DTOs
{
    public class UserProfileDto
    {
        public string Id { get; set; } = null!;
        public string Email { get; set; } = null!;

        // Maps to AppUser.FullName
        public string FullName { get; set; } = null!;

        // A user can have multiple roles
        public IList<string> Roles { get; set; } = new List<string>();

        // Optional: link to manager if this user is an Agent
        public string? ManagerId { get; set; }
        public string DisplayName { get; set; } = null!;
      
    }
}
