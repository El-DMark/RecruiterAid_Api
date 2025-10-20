using Microsoft.AspNetCore.Mvc;
using RecruiterAid_Api.Application.Services;
using RecruiterAid_Api.Presentation.DTOs;

namespace RecruiterAid_Api.Api.Controllers
{
    [ApiController]
    [Route("api/v1/jobpostings")]
    public class JobPostingsController : ControllerBase
    {
        private readonly IJobPostingService _service;

        public JobPostingsController(IJobPostingService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobPostingDto>>> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<JobPostingDto>> GetById(long id)
        {
            var job = await _service.GetByIdAsync(id);
            return job == null ? NotFound() : Ok(job);
        }

        [HttpPost]
        public async Task<ActionResult<JobPostingDto>> Create(CreateJobPostingDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.JobId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<JobPostingDto>> Update(long id, CreateJobPostingDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return updated == null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }

}
