using System;
using RecruiterAid_Api.Domain.Entities.Candidates;
using RecruiterAid_Api.Domain.Entities.Interviews;
using RecruiterAid_Api.Domain.Entities.Offers;

namespace RecruiterAid_Api.Domain.Entities.Applications
{
    public class WorkApplication
    {
        public long WorkApplicationId { get; set; }
        public long CandidateId { get; set; }
        public long JobId { get; set; }
        public DateTime AppliedAt { get; set; }
        public string Status { get; set; } = "submitted";
        public string? Source { get; set; }
        public string? ReferralCode { get; set; }
        public string? AnswersJson { get; set; }
        public string? CurrentStage { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Navigation
        public Candidate Candidate { get; set; } = null!;
        public ICollection<ApplicationStatus> StatusHistory { get; set; } = new List<ApplicationStatus>();
        public ICollection<ApplicationDocument> Documents { get; set; } = new List<ApplicationDocument>();
        public ICollection<Interview> Interviews { get; set; } = new List<Interview>();
        public ICollection<Offer> Offers { get; set; } = new List<Offer>();
        public ICollection<CandidateAuditLog> AuditLogs { get; set; } = new List<CandidateAuditLog>();
    }

}
