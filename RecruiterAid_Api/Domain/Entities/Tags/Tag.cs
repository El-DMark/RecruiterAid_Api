using RecruiterAid_Api.Domain.Entities.Candidates;

namespace RecruiterAid_Api.Domain.Entities.Tags
{
    public class Tag
    {
        public long TagId { get; set; }
        public string Label { get; set; } = null!;
        public string? Description { get; set; }

        public ICollection<CandidateTag> CandidateTags { get; set; } = new List<CandidateTag>();
    }
}
