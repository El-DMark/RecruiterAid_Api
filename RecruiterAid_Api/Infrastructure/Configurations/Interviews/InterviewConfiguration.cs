using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruiterAid_Api.Domain.Entities.Interviews;
using RecruiterAid_Api.Domain.Entities.Applications;

namespace RecruiterAid_Api.Infrastructure.Configurations.Interviews
{
    public class InterviewConfiguration : IEntityTypeConfiguration<Interview>
    {
        public void Configure(EntityTypeBuilder<Interview> builder)
        {
            builder.ToTable("Interviews");

            builder.HasKey(i => i.InterviewId);

            builder.Property(i => i.InterviewType)
                   .HasMaxLength(50);

            builder.Property(i => i.CreatedAt)
                   .HasDefaultValueSql("GETDATE()");

            builder.HasOne(i => i.WorkApplication)
                   .WithMany(a => a.Interviews)
                   .HasForeignKey(i => i.WorkApplicationId);
        }
    }
}
