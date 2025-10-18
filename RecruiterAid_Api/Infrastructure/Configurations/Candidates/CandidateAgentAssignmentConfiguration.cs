using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruiterAid_Api.Domain.Entities.Candidates;

namespace RecruiterAid_Api.Infrastructure.Configurations.Candidates
{
    public class CandidateAgentAssignmentConfiguration : IEntityTypeConfiguration<CandidateAgentAssignment>
    {
        public void Configure(EntityTypeBuilder<CandidateAgentAssignment> builder)
        {
            builder.ToTable("CandidateAgentAssignments");

            builder.HasKey(ca => ca.CandidateAgentAssignmentId);

            builder.Property(ca => ca.AssignedAt)
                   .HasDefaultValueSql("GETDATE()");

            builder.HasOne(ca => ca.Candidate)
                   .WithMany(c => c.AgentAssignments)
                   .HasForeignKey(ca => ca.CandidateId);

            builder.HasOne(ca => ca.AgentUser)
                   .WithMany() // optional: add ICollection<CandidateAgentAssignment> in ApplicationUser if you want reverse nav
                   .HasForeignKey(ca => ca.AgentUserId);
        }
    }
}
