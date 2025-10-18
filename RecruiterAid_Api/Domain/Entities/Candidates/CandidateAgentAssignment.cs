using System;
using RecruiterAid_Api.Domain.Entities.Identity;

namespace RecruiterAid_Api.Domain.Entities.Candidates
{
    public class CandidateAgentAssignment
    {
        public long CandidateAgentAssignmentId { get; set; }

        public long CandidateId { get; set; }
        public Candidate Candidate { get; set; } = null!;

        public string AgentUserId { get; set; } = null!;
        public AppUser AgentUser { get; set; } = null!;

        public DateTime AssignedAt { get; set; }
        public DateTime? UnassignedAt { get; set; }

        public string? AssignedByUserId { get; set; }
        public AppUser? AssignedByUser { get; set; }
    }



}
