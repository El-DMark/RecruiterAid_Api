using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecruiterAid_Api.Domain.Entities.Candidates;
using RecruiterAid_Api.Domain.Entities.Applications;
using RecruiterAid_Api.Domain.Entities.Interviews;
using RecruiterAid_Api.Domain.Entities.Offers;
using RecruiterAid_Api.Domain.Entities.Tags;
using RecruiterAid_Api.Domain.Entities.Identity;
using System.Reflection;
using RecruiterAid_Api.Domain.Entities;

namespace RecruiterAid_Api.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // Candidates
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<CandidateResume> CandidateResumes { get; set; }
        public DbSet<CandidateTag> CandidateTags { get; set; }
        public DbSet<CandidateAuditLog> CandidateAuditLogs { get; set; }
        public DbSet<CandidateAgentAssignment> CandidateAgentAssignments { get; set; }

        // Applications (renamed to WorkApplication)
        public DbSet<WorkApplication> WorkApplications { get; set; }
        public DbSet<ApplicationStatus> ApplicationStatuses { get; set; }
        public DbSet<ApplicationDocument> ApplicationDocuments { get; set; }

        // Interviews
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<InterviewFeedback> InterviewFeedbacks { get; set; }

        // Offers
        public DbSet<Offer> Offers { get; set; }

        // Tags
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Identity table renames
            builder.Entity<AppUser>().ToTable("AppUsers");
            builder.Entity<IdentityRole>().ToTable("UserRoles");
            builder.Entity<IdentityUserRole<string>>().ToTable("AppUserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("AppUserClaims");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("UserRoleClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("AppUserLogins");
            builder.Entity<IdentityUserToken<string>>().ToTable("AppUserTokens");

            // Composite key for CandidateTags
            builder.Entity<CandidateTag>()
                .HasKey(ct => new { ct.CandidateId, ct.TagId });

            // Relationships
            builder.Entity<Candidate>()
                .HasMany(c => c.Applications)
                .WithOne(a => a.Candidate)
                .HasForeignKey(a => a.CandidateId);

            builder.Entity<WorkApplication>()
                .HasMany(a => a.StatusHistory)
                .WithOne(s => s.WorkApplication)
                .HasForeignKey(s => s.WorkApplicationId);

            builder.Entity<WorkApplication>()
                .HasMany(a => a.Documents)
                .WithOne(d => d.WorkApplication)
                .HasForeignKey(d => d.WorkApplicationId);

            builder.Entity<WorkApplication>()
                .HasMany(a => a.Interviews)
                .WithOne(i => i.WorkApplication)
                .HasForeignKey(i => i.WorkApplicationId);

            builder.Entity<WorkApplication>()
                .HasMany(a => a.Offers)
                .WithOne(o => o.WorkApplication)
                .HasForeignKey(o => o.WorkApplicationId);

            builder.Entity<Interview>()
                .HasMany(i => i.Feedbacks)
                .WithOne(f => f.Interview)
                .HasForeignKey(f => f.InterviewId);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
