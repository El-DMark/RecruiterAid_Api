using RecruiterAid_Api.Presentation.DTOs;

namespace RecruiterAid_Api.Application.Services
{
    public interface IEmployerService
    {
        Task<IEnumerable<EmployerDto>> GetAllAsync();
        Task<EmployerDto> GetByIdAsync(long id);
        Task<EmployerDto> CreateAsync(CreateEmployerDto dto);
        Task<EmployerDto> UpdateAsync(long id, CreateEmployerDto dto);
        Task<bool> DeleteAsync(long id);
    }

}
