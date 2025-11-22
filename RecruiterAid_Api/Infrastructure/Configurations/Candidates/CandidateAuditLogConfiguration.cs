using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruiterAid_Api.Domain.Entities.Candidates;
using RecruiterAid_Api.Domain.Entities.Applications;

namespace RecruiterAid_Api.Infrastructure.Configurations.Candidates
{
    public class CandidateAuditLogConfiguration : IEntityTypeConfiguration<CandidateAuditLog>
    {
        public void Configure(EntityTypeBuilder<CandidateAuditLog> builder)
        {
            builder.ToTable("CandidateAuditLog");

            builder.HasKey(a => a.AuditId);

            builder.Property(a => a.EventTimestamp)
                   .HasDefaultValueSql("NOW()");

            builder.HasOne(a => a.Candidate)
                   .WithMany(c => c.AuditLogs)
                   .HasForeignKey(a => a.CandidateId);

            builder.HasOne(a => a.WorkApplication)
                   .WithMany(w => w.AuditLogs)
                   .HasForeignKey(a => a.WorkApplicationId);
        }
    }
}
