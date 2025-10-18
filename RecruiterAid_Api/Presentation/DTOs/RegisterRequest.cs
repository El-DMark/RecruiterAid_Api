namespace RecruiterAid_Api.Presentation.DTOs
{
    public class RegisterRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        // Maps to AppUser.FullName
        public string FullName { get; set; } = null!;

        // Role assignment: "Admin", "Manager", "Agent"
        public string Role { get; set; } = null!;

        // Optional: link to a manager if registering an Agent
        public string? ManagerId { get; set; }
    }
}
