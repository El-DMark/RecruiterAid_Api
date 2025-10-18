using RecruiterAid_Api.Domain.Entities.Candidates;
using RecruiterAid_Api.Domain.Entities.Tags;

namespace RecruiterAid_Api.Domain.Entities
{
    public class CandidateTag
    {
        public long CandidateId { get; set; }
        public long TagId { get; set; }
        public long? AssignedByUserId { get; set; }
        public DateTime AssignedAt { get; set; }

        public Candidate Candidate { get; set; } = null!;
        public Tag Tag { get; set; } = null!;
    }
}
