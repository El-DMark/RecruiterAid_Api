using Microsoft.AspNetCore.Identity;
using RecruiterAid_Api.Domain.Entities.Candidates;
using System.Collections.Generic;

namespace RecruiterAid_Api.Domain.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }

        // Manager relationship
        public string? ManagerId { get; set; }
        public AppUser? Manager { get; set; }
        public ICollection<AppUser> ManagedAgents { get; set; } = new List<AppUser>();

        // Candidate assignments (Agent role)
        public ICollection<CandidateAgentAssignment> CandidateAssignments { get; set; } = new List<CandidateAgentAssignment>();

    }
}
