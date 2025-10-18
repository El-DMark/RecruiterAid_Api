using Microsoft.EntityFrameworkCore;
using RecruiterAid_Api.Domain.Entities.Candidates;
using RecruiterAid_Api.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecruiterAid_Api.Application.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ApplicationDbContext _dbContext;

        public CandidateService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Candidate>> GetCandidatesForUserAsync(string userId, string role)
        {
            IQueryable<Candidate> query = _dbContext.Candidates
                .Include(c => c.Resumes)
                .Include(c => c.Applications)
                .Include(c => c.CandidateTags)
                .Include(c => c.AgentAssignments);

            if (role == "Admin")
            {
                return await query.ToListAsync();
            }
            else if (role == "Manager")
            {
                // TODO: refine with team-based filtering
                return await query.ToListAsync();
            }
            else if (role == "Agent")
            {
                return await query
                    .Where(c => c.AgentAssignments
                        .Any(a => a.AgentUserId == userId && a.UnassignedAt == null))
                    .ToListAsync();
            }

            return new List<Candidate>();
        }

        public async Task AssignCandidateToAgentAsync(long candidateId, string newAgentUserId, string assignedByUserId)
        {
            // Close any current active assignment
            var currentAssignment = await _dbContext.CandidateAgentAssignments
                .FirstOrDefaultAsync(a => a.CandidateId == candidateId && a.UnassignedAt == null);

            if (currentAssignment != null)
            {
                currentAssignment.UnassignedAt = DateTime.UtcNow;
            }

            // Add new assignment
            var newAssignment = new CandidateAgentAssignment
            {
                CandidateId = candidateId,
                AgentUserId = newAgentUserId,
                AssignedAt = DateTime.UtcNow,
                AssignedByUserId = assignedByUserId
            };

            _dbContext.CandidateAgentAssignments.Add(newAssignment);

            await _dbContext.SaveChangesAsync();
        }
    }
}
