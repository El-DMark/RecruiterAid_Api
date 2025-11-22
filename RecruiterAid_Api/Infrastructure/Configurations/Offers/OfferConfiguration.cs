using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruiterAid_Api.Domain.Entities.Offers;
using RecruiterAid_Api.Domain.Entities.Applications;

namespace RecruiterAid_Api.Infrastructure.Configurations.Offers
{
    public class OfferConfiguration : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)
        {
            builder.ToTable("Offers");

            builder.HasKey(o => o.OfferId);

            builder.Property(o => o.OfferStatus)
                   .HasMaxLength(50)
                   .HasDefaultValue("draft");

            builder.Property(o => o.OfferedJobTitle)
                   .HasMaxLength(100);

            builder.Property(o => o.OfferedSalary)
                   .HasColumnType("decimal(12,2)");

            builder.Property(o => o.CreatedAt)
                   .HasDefaultValueSql("NOW()");

            builder.Property(o => o.UpdatedAt)
                   .HasDefaultValueSql("NOW()");

            builder.HasOne(o => o.WorkApplication)
                   .WithMany(a => a.Offers)
                   .HasForeignKey(o => o.WorkApplicationId);
        }
    }
}
