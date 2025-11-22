using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruiterAid_Api.Domain.Entities;

namespace RecruiterAid_Api.Infrastructure.Configurations.Candidates
{
    public class CandidateTagConfiguration : IEntityTypeConfiguration<CandidateTag>
    {
        public void Configure(EntityTypeBuilder<CandidateTag> builder)
        {
            builder.ToTable("CandidateTags");

            builder.HasKey(ct => new { ct.CandidateId, ct.TagId });

            builder.Property(ct => ct.AssignedAt)
                   .HasDefaultValueSql("NOW()");

            builder.HasOne(ct => ct.Candidate)
                   .WithMany(c => c.CandidateTags)
                   .HasForeignKey(ct => ct.CandidateId);

            builder.HasOne(ct => ct.Tag)
                   .WithMany(t => t.CandidateTags)
                   .HasForeignKey(ct => ct.TagId);
        }
    }
}
