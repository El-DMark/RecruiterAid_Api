using RecruiterAid_Api.Domain.Entities.Identity;

namespace RecruiterAid_Api.Domain.Entities.Interviews
{
    public class InterviewFeedback
    {
        public long FeedbackId { get; set; }
        public long InterviewId { get; set; }

        // ✅ Updated from long to string
        public string InterviewerUserId { get; set; } = null!;
        public AppUser InterviewerUser { get; set; } = null!;

        public int? Score { get; set; }
        public bool? PassFail { get; set; }
        public string? Comments { get; set; }
        public DateTime SubmittedAt { get; set; }

        public Interview Interview { get; set; } = null!;
    }
}
