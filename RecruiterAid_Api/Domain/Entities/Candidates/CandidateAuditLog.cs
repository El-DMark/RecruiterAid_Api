using RecruiterAid_Api.Domain.Entities.Applications;

namespace RecruiterAid_Api.Domain.Entities.Candidates
{
    public class CandidateAuditLog
    {
        public long AuditId { get; set; }
        public long? CandidateId { get; set; }
        public long? WorkApplicationId { get; set; }
        public long? ChangedByUserId { get; set; }
        public string? EventType { get; set; }
        public string? FieldChanged { get; set; }
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public DateTime EventTimestamp { get; set; }
        public string? Reason { get; set; }

        public Candidate? Candidate { get; set; }
        public WorkApplication? WorkApplication { get; set; }
    }
}
