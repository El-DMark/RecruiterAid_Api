using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruiterAid_Api.Domain.Entities.Applications;

namespace RecruiterAid_Api.Infrastructure.Configurations.Applications
{
    public class ApplicationDocumentConfiguration : IEntityTypeConfiguration<ApplicationDocument>
    {
        public void Configure(EntityTypeBuilder<ApplicationDocument> builder)
        {
            builder.ToTable("ApplicationDocuments");

            builder.HasKey(d => d.ApplicationDocId);

            builder.Property(d => d.DocumentType)
                   .HasMaxLength(50);

            builder.Property(d => d.FileUrl)
                   .IsRequired()
                   .HasMaxLength(500);

            builder.Property(d => d.UploadedAt)
                   .HasDefaultValueSql("NOW()");

            builder.HasOne(d => d.WorkApplication)
                   .WithMany(a => a.Documents)
                   .HasForeignKey(d => d.WorkApplicationId);
        }
    }
}
