using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecruiterAid_Api.Application.Services;
using RecruiterAid_Api.Infrastructure.Data;
using System.Linq;
using System.Threading.Tasks;

namespace RecruiterAid_Api.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _candidateService;
        private readonly ApplicationDbContext _dbContext;

        public CandidatesController(ICandidateService candidateService, ApplicationDbContext dbContext)
        {
            _candidateService = candidateService;
            _dbContext = dbContext;
        }

        /// <summary>
        /// Assign or reassign a candidate to an agent.
        /// Only Admins and Managers are allowed.
        /// </summary>
        [HttpPost("{candidateId:long}/assign/{agentUserId}")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> AssignCandidateToAgent(long candidateId, string agentUserId)
        {
            var assignedByUserId = User?.Identity?.Name ?? "system";

            await _candidateService.AssignCandidateToAgentAsync(candidateId, agentUserId, assignedByUserId);

            return Ok(new
            {
                Message = $"Candidate {candidateId} assigned to agent {agentUserId} successfully.",
                CandidateId = candidateId,
                AgentUserId = agentUserId
            });
        }

        /// <summary>
        /// Get the current agent assignment and full assignment history for a candidate.
        /// </summary>
        [HttpGet("{candidateId:long}/assignments")]
        [Authorize(Roles = "Admin,Manager,Agent")]
        public async Task<IActionResult> GetCandidateAssignments(long candidateId)
        {
            var assignments = await _dbContext.CandidateAgentAssignments
                .Include(a => a.AgentUser)
                .Where(a => a.CandidateId == candidateId)
                .OrderByDescending(a => a.AssignedAt)
                .Select(a => new
                {
                    a.CandidateAgentAssignmentId,
                    a.CandidateId,
                    a.AgentUserId,
                    AgentUserName = a.AgentUser.UserName,
                    a.AssignedAt,
                    a.UnassignedAt,
                    a.AssignedByUserId,
                    IsActive = a.UnassignedAt == null
                })
                .ToListAsync();

            if (!assignments.Any())
                return NotFound(new { Message = $"No assignments found for candidate {candidateId}" });

            var current = assignments.FirstOrDefault(a => a.IsActive);

            return Ok(new
            {
                CandidateId = candidateId,
                CurrentAssignment = current,
                History = assignments
            });
        }

        [HttpGet("~/api/agents/{agentUserId}/candidates")]
        [Authorize(Roles = "Admin,Manager,Agent")]
        public async Task<IActionResult> GetCandidatesForAgent(string agentUserId)
        {
            var candidates = await _dbContext.Candidates
                .Include(c => c.Resumes)
                .Include(c => c.Applications)
                .Include(c => c.CandidateTags)
                .Include(c => c.AgentAssignments)
                .Where(c => c.AgentAssignments.Any(a => a.AgentUserId == agentUserId && a.UnassignedAt == null))
                .Select(c => new
                {
                    c.CandidateId,
                    FullName = c.FirstName + " " + c.LastName,
                    c.Email,
                    c.Phone,
                    c.IsActive,
                    CurrentAssignment = c.AgentAssignments
                        .Where(a => a.AgentUserId == agentUserId && a.UnassignedAt == null)
                        .Select(a => new
                        {
                            a.CandidateAgentAssignmentId,
                            a.AssignedAt,
                            a.AssignedByUserId
                        })
                        .FirstOrDefault()
                })
                .ToListAsync();

            if (!candidates.Any())
                return NotFound(new { Message = $"No active candidates found for agent {agentUserId}" });

            return Ok(new
            {
                AgentUserId = agentUserId,
                CandidateCount = candidates.Count,
                Candidates = candidates
            });
        }

        /// <summary>
        /// Get all candidates assigned to agents under a specific manager.
        /// </summary>
        [HttpGet("~/api/managers/{managerUserId}/candidates")]
        [Authorize(Roles = "Admin,Manager")]
        public async Task<IActionResult> GetCandidatesForManager(string managerUserId)
        {
            // TODO: Replace with actual team/agent-manager relationship logic.
            // For now, assume you have a way to resolve which agents belong to this manager.
            var agentIds = await _dbContext.Users
                .Where(u => u.ManagerId == managerUserId) // requires ManagerId field in ApplicationUser
                .Select(u => u.Id)
                .ToListAsync();

            if (!agentIds.Any())
                return NotFound(new { Message = $"No agents found for manager {managerUserId}" });

            var candidates = await _dbContext.Candidates
                .Include(c => c.Resumes)
                .Include(c => c.Applications)
                .Include(c => c.CandidateTags)
                .Include(c => c.AgentAssignments)
                .Where(c => c.AgentAssignments.Any(a => agentIds.Contains(a.AgentUserId) && a.UnassignedAt == null))
                .Select(c => new
                {
                    c.CandidateId,
                    FullName = c.FirstName + " " + c.LastName,
                    c.Email,
                    c.Phone,
                    c.IsActive,
                    CurrentAssignment = c.AgentAssignments
                        .Where(a => agentIds.Contains(a.AgentUserId) && a.UnassignedAt == null)
                        .Select(a => new
                        {
                            a.CandidateAgentAssignmentId,
                            a.AgentUserId,
                            a.AssignedAt,
                            a.AssignedByUserId
                        })
                        .FirstOrDefault()
                })
                .ToListAsync();

            return Ok(new
            {
                ManagerUserId = managerUserId,
                AgentCount = agentIds.Count,
                CandidateCount = candidates.Count,
                Candidates = candidates
            });
        }

    }
}
