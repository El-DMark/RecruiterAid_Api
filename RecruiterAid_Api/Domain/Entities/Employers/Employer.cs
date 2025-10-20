using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RecruiterAid_Api.Domain.Entities.JobPostings; // ✅ Correct namespace for JobPosting

namespace RecruiterAid_Api.Domain.Entities.Employers
{
    public class Employer
    {
        public long EmployerId { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Industry { get; set; }

        [MaxLength(500)]
        public string WebsiteUrl { get; set; }

        [MaxLength(255)]
        public string? AddressLine1 { get; set; }

        [MaxLength(255)]
        public string? AddressLine2 { get; set; }

        [MaxLength(100)]
        public string City { get; set; }

        [MaxLength(100)]
        public string State { get; set; }

        [MaxLength(100)]
        public string Country { get; set; }

        [MaxLength(200)]
        public string ContactPerson { get; set; }

        [MaxLength(255)]
        public string ContactEmail { get; set; }

        [MaxLength(50)]
        public string ContactPhone { get; set; }

        // Finance / Tax fields
        [MaxLength(50)]
        public string GSTNumber { get; set; }

        [MaxLength(20)]
        public string PANNumber { get; set; }

        [MaxLength(20)]
        public string TANNumber { get; set; }

        [MaxLength(500)]
        public string BillingAddress { get; set; }

        [MaxLength(50)]
        public string BankAccountNumber { get; set; }

        [MaxLength(20)]
        public string IFSCCode { get; set; }

        [MaxLength(100)]
        public string PaymentTerms { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<JobPosting> JobPostings { get; set; } = new List<JobPosting>();
    }
}
