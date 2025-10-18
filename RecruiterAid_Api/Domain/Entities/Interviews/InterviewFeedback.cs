namespace RecruiterAid_Api.Domain.Entities.Interviews
{
    public class InterviewFeedback
    {
        public long FeedbackId { get; set; }
        public long InterviewId { get; set; }
        public long InterviewerUserId { get; set; }
        public int? Score { get; set; }
        public bool? PassFail { get; set; }
        public string? Comments { get; set; }
        public DateTime SubmittedAt { get; set; }

        public Interview Interview { get; set; } = null!;
    }
}
