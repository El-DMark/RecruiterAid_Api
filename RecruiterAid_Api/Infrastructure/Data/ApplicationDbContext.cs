using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecruiterAid_Api.Domain.Entities;

namespace RecruiterAid_Api.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Candidate> Candidates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Rename Identity tables for clarity
            builder.Entity<ApplicationUser>().ToTable("AppUsers");
            builder.Entity<IdentityRole>().ToTable("UserRoles");
            builder.Entity<IdentityUserRole<string>>().ToTable("AppUserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("AppUserClaims");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("UserRoleClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("AppUserLogins");
            builder.Entity<IdentityUserToken<string>>().ToTable("AppUserTokens");

            // Optional: Rename Candidate table if needed
            builder.Entity<Candidate>().ToTable("Candidates");
        }
    }
}
