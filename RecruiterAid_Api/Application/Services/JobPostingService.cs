using Microsoft.EntityFrameworkCore;
using RecruiterAid_Api.Domain.Entities.JobPostings;
using RecruiterAid_Api.Infrastructure.Data;
using RecruiterAid_Api.Presentation.DTOs;

namespace RecruiterAid_Api.Application.Services
{
    public class JobPostingService : IJobPostingService
    {
        private readonly ApplicationDbContext _db;

        public JobPostingService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<JobPostingDto>> GetAllAsync()
        {
            return await _db.JobPostings
                .Include(j => j.Employer)
                .Select(j => new JobPostingDto
                {
                    JobId = j.JobId,
                    EmployerId = j.EmployerId,
                    EmployerName = j.Employer.Name,
                    Title = j.Title,
                    Description = j.Description,
                    Location = j.Location,
                    EmploymentType = j.EmploymentType,
                    MinExperienceYears = j.MinExperienceYears,
                    MaxExperienceYears = j.MaxExperienceYears,
                    MinSalary = j.MinSalary,
                    MaxSalary = j.MaxSalary,
                    Qualification = j.Qualification,
                    Status = j.Status,
                    PostedAt = j.PostedAt,
                    ClosingDate = j.ClosingDate
                })
                .ToListAsync();
        }

        public async Task<JobPostingDto?> GetByIdAsync(long id)
        {
            var j = await _db.JobPostings
                .Include(x => x.Employer)
                .FirstOrDefaultAsync(x => x.JobId == id);

            if (j == null) return null;

            return new JobPostingDto
            {
                JobId = j.JobId,
                EmployerId = j.EmployerId,
                EmployerName = j.Employer.Name,
                Title = j.Title,
                Description = j.Description,
                Location = j.Location,
                EmploymentType = j.EmploymentType,
                MinExperienceYears = j.MinExperienceYears,
                MaxExperienceYears = j.MaxExperienceYears,
                MinSalary = j.MinSalary,
                MaxSalary = j.MaxSalary,
                Qualification = j.Qualification,
                Status = j.Status,
                PostedAt = j.PostedAt,
                ClosingDate = j.ClosingDate
            };
        }

        public async Task<JobPostingDto> CreateAsync(CreateJobPostingDto dto)
        {
            var job = new JobPosting
            {
                EmployerId = dto.EmployerId,
                Title = dto.Title,
                Description = dto.Description,
                Location = dto.Location,
                EmploymentType = dto.EmploymentType,
                MinExperienceYears = dto.MinExperienceYears,
                MaxExperienceYears = dto.MaxExperienceYears,
                MinSalary = dto.MinSalary,
                MaxSalary = dto.MaxSalary,
                Qualification = dto.Qualification,
                PostedAt = DateTime.UtcNow,
                ClosingDate = dto.ClosingDate,
                Status = "Open",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _db.JobPostings.Add(job);
            await _db.SaveChangesAsync();

            return await GetByIdAsync(job.JobId);
        }

        public async Task<JobPostingDto?> UpdateAsync(long id, CreateJobPostingDto dto)
        {
            var job = await _db.JobPostings.FindAsync(id);
            if (job == null) return null;

            job.Title = dto.Title;
            job.Description = dto.Description;
            job.Location = dto.Location;
            job.EmploymentType = dto.EmploymentType;
            job.MinExperienceYears = dto.MinExperienceYears;
            job.MaxExperienceYears = dto.MaxExperienceYears;
            job.MinSalary = dto.MinSalary;
            job.MaxSalary = dto.MaxSalary;
            job.Qualification = dto.Qualification;
            job.ClosingDate = dto.ClosingDate;
            job.UpdatedAt = DateTime.UtcNow;

            await _db.SaveChangesAsync();

            return await GetByIdAsync(job.JobId);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var job = await _db.JobPostings.FindAsync(id);
            if (job == null) return false;

            _db.JobPostings.Remove(job);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
