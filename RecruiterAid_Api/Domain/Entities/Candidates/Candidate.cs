using RecruiterAid_Api.Domain.Entities.Applications;

namespace RecruiterAid_Api.Domain.Entities.Candidates
{
    public class Candidate
    {
        public long CandidateId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Headline { get; set; }
        public string? ProfileSummary { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? GitHubUrl { get; set; }
        public string? WebsiteUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsActive { get; set; }

        // Navigation
        public ICollection<CandidateResume> Resumes { get; set; } = new List<CandidateResume>();
        public ICollection<WorkApplication> Applications { get; set; } = new List<WorkApplication>();
        public ICollection<CandidateTag> CandidateTags { get; set; } = new List<CandidateTag>();
        public ICollection<CandidateAuditLog> AuditLogs { get; set; } = new List<CandidateAuditLog>();
        public ICollection<CandidateAgentAssignment> AgentAssignments { get; set; } = new List<CandidateAgentAssignment>();

    }
}
