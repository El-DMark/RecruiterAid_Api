using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruiterAid_Api.Domain.Entities;
using RecruiterAid_Api.Domain.Entities.Interviews;

namespace RecruiterAid_Api.Infrastructure.Configurations.Interviews
{
    public class InterviewFeedbackConfiguration : IEntityTypeConfiguration<InterviewFeedback>
    {
        public void Configure(EntityTypeBuilder<InterviewFeedback> builder)
        {
            builder.ToTable("InterviewFeedback");

            builder.HasKey(f => f.FeedbackId);

            builder.Property(f => f.SubmittedAt).HasDefaultValueSql("GETDATE()");

            builder.HasOne(f => f.Interview)
                   .WithMany(i => i.Feedbacks)
                   .HasForeignKey(f => f.InterviewId);
        }
    }
}
