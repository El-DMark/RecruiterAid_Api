using System;

namespace RecruiterAid_Api.Domain.Entities.Candidates
{
    public class CandidateAgentAssignment
    {
        public long CandidateAgentAssignmentId { get; set; }

        public long CandidateId { get; set; }
        public string AgentUserId { get; set; } = null!; // FK to ApplicationUser (Identity)

        public DateTime AssignedAt { get; set; }
        public DateTime? UnassignedAt { get; set; } // null = still active
        public string? AssignedByUserId { get; set; } // optional: who made the assignment

        // Navigation
        public Candidate Candidate { get; set; } = null!;
        public ApplicationUser AgentUser { get; set; } = null!;
    }
}
