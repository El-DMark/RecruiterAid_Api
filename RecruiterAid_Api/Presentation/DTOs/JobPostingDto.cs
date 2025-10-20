using System;
using System.ComponentModel.DataAnnotations;

namespace RecruiterAid_Api.Presentation.DTOs
{
    public class JobPostingDto
    {
        public long JobId { get; set; }
        public long EmployerId { get; set; }
        public string EmployerName { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Location { get; set; } = string.Empty;
        public string EmploymentType { get; set; } = string.Empty;

        public int? MinExperienceYears { get; set; }
        public int? MaxExperienceYears { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
        public string? Qualification { get; set; }

        public string Status { get; set; } = "Open";

        // 🔧 Extra fields for client visibility
        public DateTime PostedAt { get; set; }
        public DateTime? ClosingDate { get; set; }
    }

    public class CreateJobPostingDto
    {
        [Required]
        public long EmployerId { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }
        public string? Location { get; set; }
        public string? EmploymentType { get; set; }

        public int? MinExperienceYears { get; set; }
        public int? MaxExperienceYears { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
        public string? Qualification { get; set; }

        // 🔧 Optional: allow setting closing date at creation
        public DateTime? ClosingDate { get; set; }
    }
}
