using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruiterAid_Api.Domain.Entities;
using RecruiterAid_Api.Domain.Entities.Tags;

namespace RecruiterAid_Api.Infrastructure.Configurations.Tags
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable("Tags");

            builder.HasKey(t => t.TagId);

            builder.Property(t => t.Label)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasIndex(t => t.Label)
                   .IsUnique();

            builder.Property(t => t.Description)
                   .HasColumnType("text");
        }
    }
}
