using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruiterAid_Api.Domain.Entities.Identity;

namespace RecruiterAid_Api.Infrastructure.Configurations.Identity
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUsers");

            builder.Property(u => u.FullName).HasMaxLength(200);
            // Self-referencing manager relationship
            builder.HasOne(u => u.Manager)
                   .WithMany(m => m.ManagedAgents)
                   .HasForeignKey(u => u.ManagerId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
