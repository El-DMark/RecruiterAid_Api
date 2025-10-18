namespace RecruiterAid_Api.Domain.Entities.Candidates
{
    public class CandidateResume
    {
        public long ResumeId { get; set; }
        public long CandidateId { get; set; }
        public string? DocumentName { get; set; }
        public string? DocumentType { get; set; }
        public string FileUrl { get; set; } = null!;
        public string? FileHash { get; set; }
        public long? FileSize { get; set; }
        public DateTime UploadedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public bool IsPrimary { get; set; }

        public Candidate Candidate { get; set; } = null!;
    }
}
