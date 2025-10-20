using RecruiterAid_Api.Presentation.DTOs;

namespace RecruiterAid_Api.Application.Services
{
    public interface IJobPostingService
    {
        Task<IEnumerable<JobPostingDto>> GetAllAsync();
        Task<JobPostingDto> GetByIdAsync(long id);
        Task<JobPostingDto> CreateAsync(CreateJobPostingDto dto);
        Task<JobPostingDto> UpdateAsync(long id, CreateJobPostingDto dto);
        Task<bool> DeleteAsync(long id);
    }

}
