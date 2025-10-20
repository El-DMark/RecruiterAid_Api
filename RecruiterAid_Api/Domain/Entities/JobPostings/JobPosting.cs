using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RecruiterAid_Api.Domain.Entities.Applications;
using RecruiterAid_Api.Domain.Entities.Employers;

namespace RecruiterAid_Api.Domain.Entities.JobPostings
{
    public class JobPosting
    {
        public long JobId { get; set; }

        public long EmployerId { get; set; }
        public Employer Employer { get; set; } = null!;

        [Required, MaxLength(200)]
        public string Title { get; set; } = null!;
        public string? Description { get; set; }

        [MaxLength(200)]
        public string? Location { get; set; }

        [MaxLength(50)]
        public string? EmploymentType { get; set; }

        public int? MinExperienceYears { get; set; }
        public int? MaxExperienceYears { get; set; }

        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }

        [MaxLength(200)]
        public string? Qualification { get; set; }

        public DateTime PostedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ClosingDate { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = "Open";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ICollection<WorkApplication> Applications { get; set; } = new List<WorkApplication>();
    }
}
