namespace RecruiterAid_Api.Domain.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public string TeamId { get; set; } // For manager-agent grouping
    }
}
