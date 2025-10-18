using RecruiterAid_Api.Domain.Entities.Applications;

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
        public long? OrganizerUserId { get; set; }
        public long? ScheduledByUserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public WorkApplication WorkApplication { get; set; } = null!;
        public ICollection<InterviewFeedback> Feedbacks { get; set; } = new List<InterviewFeedback>();
    }
}
