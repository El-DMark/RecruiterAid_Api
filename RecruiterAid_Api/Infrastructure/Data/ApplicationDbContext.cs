using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RecruiterAid_Api.Domain.Entities.Candidates;
using RecruiterAid_Api.Domain.Entities.Applications;
using RecruiterAid_Api.Domain.Entities.Interviews;
using RecruiterAid_Api.Domain.Entities.Offers;
using RecruiterAid_Api.Domain.Entities.Tags;
using RecruiterAid_Api.Domain.Entities.Identity;
using RecruiterAid_Api.Domain.Entities.Employers;
using RecruiterAid_Api.Domain.Entities.JobPostings;
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

        // Applications
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

        // Employers & Job Postings
        public DbSet<Employer> Employers { get; set; }
        public DbSet<JobPosting> JobPostings { get; set; }

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

            // Candidate → Applications
            builder.Entity<Candidate>()
                .HasMany(c => c.Applications)
                .WithOne(a => a.Candidate)
                .HasForeignKey(a => a.CandidateId);

            // WorkApplication → StatusHistory
            builder.Entity<WorkApplication>()
                .HasMany(a => a.StatusHistory)
                .WithOne(s => s.WorkApplication)
                .HasForeignKey(s => s.WorkApplicationId);

            // WorkApplication → Documents
            builder.Entity<WorkApplication>()
                .HasMany(a => a.Documents)
                .WithOne(d => d.WorkApplication)
                .HasForeignKey(d => d.WorkApplicationId);

            // WorkApplication → Interviews
            builder.Entity<WorkApplication>()
                .HasMany(a => a.Interviews)
                .WithOne(i => i.WorkApplication)
                .HasForeignKey(i => i.WorkApplicationId);

            // WorkApplication → Offers
            builder.Entity<WorkApplication>()
                .HasMany(a => a.Offers)
                .WithOne(o => o.WorkApplication)
                .HasForeignKey(o => o.WorkApplicationId);

            builder.Entity<Interview>()
     .HasOne(i => i.OrganizerUser)
     .WithMany()
     .HasForeignKey(i => i.OrganizerUserId)
     .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Interview>()
                .HasOne(i => i.ScheduledByUser)
                .WithMany()
                .HasForeignKey(i => i.ScheduledByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<InterviewFeedback>()
                .HasOne(f => f.InterviewerUser)
                .WithMany()
                .HasForeignKey(f => f.InterviewerUserId)
                .OnDelete(DeleteBehavior.Restrict);


            // CandidateAgentAssignment explicit mapping
            builder.Entity<CandidateAgentAssignment>(entity =>
            {
                entity.HasKey(ca => ca.CandidateAgentAssignmentId);

                entity.HasOne(ca => ca.Candidate)
                      .WithMany()
                      .HasForeignKey(ca => ca.CandidateId);

                entity.HasOne(ca => ca.AgentUser)
                      .WithMany()
                      .HasForeignKey(ca => ca.AgentUserId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(ca => ca.AssignedByUser)
                      .WithMany()
                      .HasForeignKey(ca => ca.AssignedByUserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Employer → JobPostings
            builder.Entity<Employer>()
                .HasMany(e => e.JobPostings)
                .WithOne(j => j.Employer)
                .HasForeignKey(j => j.EmployerId)
                .OnDelete(DeleteBehavior.Cascade); // Optional: cascade delete

            // JobPosting → WorkApplications
            builder.Entity<JobPosting>()
                .HasKey(j => j.JobId);

            builder.Entity<JobPosting>()
                .HasMany(j => j.Applications)
                .WithOne(a => a.JobPosting)
                .HasForeignKey(a => a.JobId)
                .OnDelete(DeleteBehavior.Cascade); // Optional: cascade delete


            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
