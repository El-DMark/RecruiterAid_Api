using RecruiterAid_Api.Domain.Entities.Applications;
using RecruiterAid_Api.Domain.Entities.Identity; // 👈 for AppUser

namespace RecruiterAid_Api.Domain.Entities.Interviews
{
    public class Interview
    {
        public long InterviewId { get; set; }
        public long WorkApplicationId { get; set; }
        public string? InterviewType { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Location { get; set; }

        // ✅ Changed from long? to string? to match AppUser.Id
        public string? OrganizerUserId { get; set; }
        public string? ScheduledByUserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public WorkApplication WorkApplication { get; set; } = null!;
        public ICollection<InterviewFeedback> Feedbacks { get; set; } = new List<InterviewFeedback>();

        // ✅ Optional navigation properties (if you want reverse lookup)
        public AppUser OrganizerUser { get; set; } = null!;
        public AppUser ScheduledByUser { get; set; } = null!;
    }
}
