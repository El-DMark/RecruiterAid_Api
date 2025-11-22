using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruiterAid_Api.Domain.Entities.Candidates;

namespace RecruiterAid_Api.Infrastructure.Configurations.Candidates
{
    public class CandidateResumeConfiguration : IEntityTypeConfiguration<CandidateResume>
    {
        public void Configure(EntityTypeBuilder<CandidateResume> builder)
        {
            builder.ToTable("CandidateResumes");

            builder.HasKey(r => r.ResumeId);

            builder.Property(r => r.DocumentName).HasMaxLength(255);
            builder.Property(r => r.DocumentType).HasMaxLength(50);
            builder.Property(r => r.FileUrl).IsRequired().HasMaxLength(500);

            builder.Property(r => r.UploadedAt).HasDefaultValueSql("NOW()");
            builder.Property(r => r.UpdatedAt).HasDefaultValueSql("NOW()");
            builder.Property(r => r.IsActive).HasDefaultValue(true);
            builder.Property(r => r.IsPrimary).HasDefaultValue(false);

            builder.HasOne(r => r.Candidate)
                   .WithMany(c => c.Resumes)
                   .HasForeignKey(r => r.CandidateId);
        }
    }
}
