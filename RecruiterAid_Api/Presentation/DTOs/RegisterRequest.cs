namespace RecruiterAid_Api.Presentation.DTOs
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string Role { get; set; } // Agent, Manager, Admin
        public string TeamId { get; set; } // Optional for grouping
    }
}
