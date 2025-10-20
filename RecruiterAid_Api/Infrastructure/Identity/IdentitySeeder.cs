using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RecruiterAid_Api.Domain.Entities.Candidates;
using RecruiterAid_Api.Domain.Entities.Identity;
using RecruiterAid_Api.Domain.Entities.Applications;
using RecruiterAid_Api.Domain.Entities.Interviews;
using RecruiterAid_Api.Domain.Entities.Offers;
using RecruiterAid_Api.Infrastructure.Data;
using RecruiterAid_Api.Domain.Entities.Employers;
using RecruiterAid_Api.Domain.Entities.JobPostings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecruiterAid_Api.Infrastructure.Identity
{
    public static class IdentitySeeder
    {
        private static readonly string[] Roles = { "Admin", "Manager", "Agent" };

        private static readonly List<(string Email, string UserName, string FullName, string Password, string Role)> DefaultUsers = new()
        {
            ("admin@recruiteraid.local", "admin", "System Admin", "Admin@12345", "Admin"),
            ("manager1@recruiteraid.local", "manager1", "Manager One", "Manager@12345", "Manager"),
            ("manager2@recruiteraid.local", "manager2", "Manager Two", "Manager@12345", "Manager"),
            ("manager3@recruiteraid.local", "manager3", "Manager Three", "Manager@12345", "Manager"),
            ("agent1@recruiteraid.local", "agent1", "Agent One", "Agent@12345", "Agent"),
            ("agent2@recruiteraid.local", "agent2", "Agent Two", "Agent@12345", "Agent"),
            ("agent3@recruiteraid.local", "agent3", "Agent Three", "Agent@12345", "Agent"),
            ("agent4@recruiteraid.local", "agent4", "Agent Four", "Agent@12345", "Agent"),
            ("agent5@recruiteraid.local", "agent5", "Agent Five", "Agent@12345", "Agent")
        };

        public static async Task SeedAsync(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext dbContext)
        {
            foreach (var role in Roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userLookup = new Dictionary<string, AppUser>();
            foreach (var (email, userName, fullName, password, role) in DefaultUsers)
            {
                var user = await userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    user = new AppUser
                    {
                        UserName = userName,
                        Email = email,
                        FullName = fullName,
                        EmailConfirmed = true
                    };
                    var result = await userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                        await userManager.AddToRoleAsync(user, role);
                }
                userLookup[email] = user!;
            }
            var agents = new[]
            {
                    userLookup["agent1@recruiteraid.local"],
                    userLookup["agent2@recruiteraid.local"],
                    userLookup["agent3@recruiteraid.local"],
                    userLookup["agent4@recruiteraid.local"],
                    userLookup["agent5@recruiteraid.local"]
                };


            foreach (var agent in new[] { "agent1@recruiteraid.local", "agent2@recruiteraid.local", "agent3@recruiteraid.local", "agent4@recruiteraid.local", "agent5@recruiteraid.local" })
            {
                await userManager.UpdateAsync(userLookup[agent]);
            }

            var employer = await dbContext.Employers.FirstOrDefaultAsync(e => e.Name == "Deshkar Advertising");
            if (employer == null)
            {
                employer = new Employer
                {
                    Name = "Deshkar Advertising",
                    Industry = "Marketing",
                    GSTNumber = "27ABCDE1234F1Z5",
                    PANNumber = "ABCDE1234F",
                    TANNumber = "TAN123456",
                    BillingAddress = "123 MG Road, Delhi",
                    BankAccountNumber = "1234567890",
                    IFSCCode = "SBIN0001234",
                    PaymentTerms = "Net 30",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsActive = true
                };
                dbContext.Employers.Add(employer);
                await dbContext.SaveChangesAsync();
            }

            for (int i = 0; i < 8; i++)
            {
                var job = new JobPosting
                {
                    JobId = 1000 + i,
                    EmployerId = employer.EmployerId,
                    Title = $"Placeholder Job {i + 1}",
                    Status = "Open",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                dbContext.JobPostings.Add(job);
            }
            await dbContext.SaveChangesAsync();
            var defaultCandidates = new List<(string FirstName, string LastName, string Email, string Phone, string Position)>
            {
                ("Alice", "Johnson", "alice.johnson@test.local", "555-1001", "Software Engineer"),
                ("Bob", "Smith", "bob.smith@test.local", "555-1002", "Data Analyst"),
                ("Charlie", "Brown", "charlie.brown@test.local", "555-1003", "Product Manager"),
                ("Diana", "Prince", "diana.prince@test.local", "555-1004", "UX Designer"),
                ("Ethan", "Hunt", "ethan.hunt@test.local", "555-1005", "DevOps Engineer"),
                ("Fiona", "Clark", "fiona.clark@test.local", "555-1006", "QA Engineer"),
                ("George", "Miller", "george.miller@test.local", "555-1007", "Business Analyst"),
                ("Hannah", "Lee", "hannah.lee@test.local", "555-1008", "Frontend Developer")
            };

            int agentIndex = 0;
            foreach (var (firstName, lastName, email, phone, position) in defaultCandidates)
            {
                var candidate = await dbContext.Candidates.FirstOrDefaultAsync(c => c.Email == email);
                if (candidate == null)
                {
                    candidate = new Candidate
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        Phone = phone,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        IsActive = true
                    };
                    dbContext.Candidates.Add(candidate);
                    await dbContext.SaveChangesAsync();

                    var assignedAgent = agents[agentIndex % agents.Length];
                    dbContext.CandidateAgentAssignments.Add(new CandidateAgentAssignment
                    {
                        CandidateId = candidate.CandidateId,
                        AgentUserId = assignedAgent.Id,
                        AssignedAt = DateTime.UtcNow
                    });
                    await dbContext.SaveChangesAsync();

                    var application = new WorkApplication
                    {
                        CandidateId = candidate.CandidateId,
                        JobId = 1000 + agentIndex,
                        AppliedAt = DateTime.UtcNow,
                        Status = "submitted",
                        CurrentStage = "Screening",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };
                    dbContext.WorkApplications.Add(application);
                    await dbContext.SaveChangesAsync();

                    dbContext.ApplicationStatuses.Add(new ApplicationStatus
                    {
                        WorkApplicationId = application.WorkApplicationId,
                        OldStatus = null,
                        NewStatus = "submitted",
                        ChangedAt = DateTime.UtcNow,
                        Reason = "Initial submission"
                    });
                    await dbContext.SaveChangesAsync();

                    var interview = new Interview
                    {
                        WorkApplicationId = application.WorkApplicationId,
                        InterviewType = "Technical",
                        StartTime = DateTime.UtcNow.AddDays(2),
                        EndTime = DateTime.UtcNow.AddDays(2).AddHours(1),
                        Location = "Zoom",
                        OrganizerUserId = assignedAgent.Id,         // ✅ string
                        ScheduledByUserId = assignedAgent.Id,       // ✅ string
                        CreatedAt = DateTime.UtcNow
                    };
                    dbContext.Interviews.Add(interview);
                    await dbContext.SaveChangesAsync();

                    dbContext.InterviewFeedbacks.Add(new InterviewFeedback
                    {
                        InterviewId = interview.InterviewId,
                        InterviewerUserId = assignedAgent.Id,       // ✅ string
                        Score = 4,
                        PassFail = true,
                        Comments = "Candidate performed well in technical screening.",
                        SubmittedAt = DateTime.UtcNow
                    });
                    await dbContext.SaveChangesAsync();


                    dbContext.Offers.Add(new Offer
                    {
                        WorkApplicationId = application.WorkApplicationId,
                        OfferStatus = "draft",
                        OfferedSalary = 60000 + (agentIndex * 5000),
                        OfferedJobTitle = position,
                        StartDate = DateTime.UtcNow.AddMonths(1).Date,
                        SentAt = DateTime.UtcNow,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    });
                    await dbContext.SaveChangesAsync();
                }

                agentIndex++;
            }
        }
    }
}
