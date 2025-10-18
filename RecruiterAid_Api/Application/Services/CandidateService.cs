using RecruiterAid_Api.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecruiterAid_Api.Application.Services
{
    public interface ICandidateService
    {
        Task<List<Candidate>> GetCandidatesForUserAsync(string userId, string role);
    }

    public class CandidateService : ICandidateService
    {
        private readonly List<Candidate> _mockCandidates = new()
        {
            new Candidate { Id = "C1", Name = "Alice", AssignedAgentId = "agent-id-123" },
            new Candidate { Id = "C2", Name = "Bob", AssignedAgentId = "agent-id-456" }
        };

        public async Task<List<Candidate>> GetCandidatesForUserAsync(string userId, string role)
        {
            if (role == "Admin") return await Task.FromResult(_mockCandidates);
            if (role == "Manager") return await Task.FromResult(_mockCandidates); // Replace with team filter
            return await Task.FromResult(_mockCandidates.FindAll(c => c.AssignedAgentId == userId));
        }
    }
}
