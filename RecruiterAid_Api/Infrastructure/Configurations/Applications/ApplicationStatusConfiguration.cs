using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruiterAid_Api.Domain.Entities.Applications;

namespace RecruiterAid_Api.Infrastructure.Configurations.Applications
{
    public class ApplicationStatusConfiguration : IEntityTypeConfiguration<ApplicationStatus>
    {
        public void Configure(EntityTypeBuilder<ApplicationStatus> builder)
        {
            builder.ToTable("ApplicationStatus");

            builder.HasKey(s => s.StatusId);

            builder.Property(s => s.NewStatus).IsRequired().HasMaxLength(50);
            builder.Property(s => s.ChangedAt).HasDefaultValueSql("GETDATE()");

            builder.HasOne(s => s.WorkApplication)
                   .WithMany(a => a.StatusHistory)
                   .HasForeignKey(s => s.WorkApplicationId);
        }
    }
}
