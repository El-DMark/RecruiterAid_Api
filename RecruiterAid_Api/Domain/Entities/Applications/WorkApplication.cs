using System;
using System.Collections.Generic;
using RecruiterAid_Api.Domain.Entities.Candidates;
using RecruiterAid_Api.Domain.Entities.Interviews;
using RecruiterAid_Api.Domain.Entities.Offers;
using RecruiterAid_Api.Domain.Entities.JobPostings;
using RecruiterAid_Api.Domain.Entities.Employers;

namespace RecruiterAid_Api.Domain.Entities.Applications
{
    public class WorkApplication
    {
        public long WorkApplicationId { get; set; }

        // Foreign Keys
        public long CandidateId { get; set; }
        public long JobId { get; set; }

        // Core fields
        public DateTime AppliedAt { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "submitted";
        public string? Source { get; set; }
        public string? ReferralCode { get; set; }
        public string? AnswersJson { get; set; }
        public string? CurrentStage { get; set; }
        public string? Notes { get; set; }

        // Audit
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Candidate Candidate { get; set; } = null!;
        public JobPosting JobPosting { get; set; } = null!;
        public Employer Employer => JobPosting.Employer;

        public ICollection<ApplicationStatus> StatusHistory { get; set; } = new List<ApplicationStatus>();
        public ICollection<ApplicationDocument> Documents { get; set; } = new List<ApplicationDocument>();
        public ICollection<Interview> Interviews { get; set; } = new List<Interview>();
        public ICollection<Offer> Offers { get; set; } = new List<Offer>();
        public ICollection<CandidateAuditLog> AuditLogs { get; set; } = new List<CandidateAuditLog>();
    }
}
