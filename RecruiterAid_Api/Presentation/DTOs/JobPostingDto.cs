using System.ComponentModel.DataAnnotations;

namespace RecruiterAid_Api.Presentation.DTOs
{
    public class JobPostingDto
    {
        public long JobId { get; set; }
        public long EmployerId { get; set; }
        public string EmployerName { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string EmploymentType { get; set; }
        public int? MinExperienceYears { get; set; }
        public int? MaxExperienceYears { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
        public string Qualification { get; set; }
        public string Status { get; set; }

    }
    public class CreateJobPostingDto
    {
        [Required]
        public long EmployerId { get; set; }
        [Required, MaxLength(200)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string EmploymentType { get; set; }
        public int? MinExperienceYears { get; set; }
        public int? MaxExperienceYears { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
        public string Qualification { get; set; }

    }

}