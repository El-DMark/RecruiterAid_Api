using RecruiterAid_Api.Domain.Entities.Candidates;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecruiterAid_Api.Application.Services
{
    public interface ICandidateService
    {
        Task<List<Candidate>> GetCandidatesForUserAsync(string userId, string role);

        Task AssignCandidateToAgentAsync(long candidateId, string newAgentUserId, string assignedByUserId);
    }
}
