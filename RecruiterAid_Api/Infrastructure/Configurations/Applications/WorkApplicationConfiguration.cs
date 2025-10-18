using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruiterAid_Api.Domain.Entities.Applications;

namespace RecruiterAid_Api.Infrastructure.Configurations.Applications
{
    public class WorkApplicationConfiguration : IEntityTypeConfiguration<WorkApplication>
    {
        public void Configure(EntityTypeBuilder<WorkApplication> builder)
        {
            builder.ToTable("Applications"); // keep DB table name

            builder.HasKey(a => a.WorkApplicationId);

            builder.Property(a => a.Status)
                   .HasMaxLength(50)
                   .HasDefaultValue("submitted");

            builder.Property(a => a.AppliedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.CreatedAt).HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.UpdatedAt).HasDefaultValueSql("GETDATE()");

            builder.HasOne(a => a.Candidate)
                   .WithMany(c => c.Applications)
                   .HasForeignKey(a => a.CandidateId);
        }
    }
}
