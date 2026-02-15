using Microsoft.AspNetCore.Mvc;
using PMSYSAPI.DTOs.Project;
using PMSYSAPI.Services.Interfaces;

namespace PMSYSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectPhasesController : ControllerBase
    {
        private readonly IProjectPhaseService _projectPhaseService;

        public ProjectPhasesController(IProjectPhaseService projectPhaseService)
        {
            _projectPhaseService = projectPhaseService;
        }

        // ================== GET ==================
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ProjectPhaseDto>>>> GetProjectPhases()
        {
            var response = await _projectPhaseService.GetAllPhasesAsync();
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ProjectPhaseDto>>> GetProjectPhase(int id)
        {
            var response = await _projectPhaseService.GetPhaseByIdAsync(id);
            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }

        // ================== POST ==================
        [HttpPost]
        public async Task<ActionResult<ApiResponse<ProjectPhaseDto>>> CreateProjectPhase([FromBody] CreateProjectPhaseDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<ProjectPhaseDto> { Success = false, Message = "Invalid input", Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList() });

            var response = await _projectPhaseService.CreatePhaseAsync(dto);
            if (!response.Success)
                return BadRequest(response);

            return CreatedAtAction(nameof(GetProjectPhase), new { id = response.Data!.PhaseCod }, response);
        }

        // ================== PUT ==================
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<ProjectPhaseDto>>> UpdateProjectPhase(int id, [FromBody] UpdateProjectPhaseDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<ProjectPhaseDto> { Success = false, Message = "Invalid input", Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList() });

            var response = await _projectPhaseService.UpdatePhaseAsync(id, dto);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        // ================== DELETE ==================
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteProjectPhase(int id)
        {
            var response = await _projectPhaseService.DeletePhaseAsync(id);
            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }
    }
}
