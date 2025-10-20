using Microsoft.AspNetCore.Mvc;
using RecruiterAid_Api.Application.Services;
using RecruiterAid_Api.Presentation.DTOs;

namespace RecruiterAid_Api.Api.Controllers
{
    [ApiController]
    [Route("api/v1/employers")]
    public class EmployersController : Controller
    {
        private readonly IEmployerService _service;

        public EmployersController(IEmployerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployerDto>>> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployerDto>> GetById(long id)
        {
            var employer = await _service.GetByIdAsync(id);
            return employer == null ? NotFound() : Ok(employer);
        }

        [HttpPost]
        public async Task<ActionResult<EmployerDto>> Create(CreateEmployerDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.EmployerId }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployerDto>> Update(long id, CreateEmployerDto dto)
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
