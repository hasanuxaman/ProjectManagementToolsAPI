using Microsoft.AspNetCore.Mvc;
using PMSYSAPI.DTOs.Project;
using PMSYSAPI.Services.Interfaces;

namespace PMSYSAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ProjectDto>>>> GetProjects()
        {
            var response = await _projectService.GetAllProjectsAsync();
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ProjectDto>>> GetProject(int id)
        {
            var response = await _projectService.GetProjectByIdAsync(id);
            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<ProjectDto>>> CreateProject(CreateProjectDto createProjectDto)
        {
            var response = await _projectService.CreateProjectAsync(createProjectDto);
            if (!response.Success)
                return BadRequest(response);

            return CreatedAtAction(nameof(GetProject), new { id = response.Data?.ProjCod }, response);
        }

        [HttpPost("create-via-sp")]
        public async Task<ActionResult<ApiResponse<ProjectDto>>> CreateProjectViaSp(CreateProjectDto createProjectDto)
        {
            var response = await _projectService.CreateProjectUsingSpAsync(createProjectDto);
            if (!response.Success)
                return BadRequest(response);

            return CreatedAtAction(nameof(GetProject), new { id = response.Data?.ProjCod }, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<ProjectDto>>> UpdateProject(int id, UpdateProjectDto updateProjectDto)
        {
            var response = await _projectService.UpdateProjectAsync(id, updateProjectDto);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteProject(int id)
        {
            var response = await _projectService.DeleteProjectAsync(id);
            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }

        [HttpGet("by-company/{companyId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<ProjectDto>>>> GetProjectsByCompany(int companyId)
        {
            var response = await _projectService.GetProjectsByCompanyAsync(companyId);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("by-status/{statusId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<ProjectDto>>>> GetProjectsByStatus(int statusId)
        {
            var response = await _projectService.GetProjectsByStatusAsync(statusId);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("by-phase/{phaseId}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<ProjectDto>>>> GetProjectsByPhase(int phaseId)
        {
            var response = await _projectService.GetProjectsByPhaseAsync(phaseId);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("search")]
        public async Task<ActionResult<ApiResponse<IEnumerable<ProjectDto>>>> SearchProjects([FromQuery] string term)
        {
            var response = await _projectService.SearchProjectsAsync(term);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("by-date-range")]
        public async Task<ActionResult<ApiResponse<IEnumerable<ProjectDto>>>> GetProjectsByDateRange(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            var response = await _projectService.GetProjectsByDateRangeAsync(startDate, endDate);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("summary")]
        public async Task<ActionResult<ApiResponse<ProjectSummaryDto>>> GetProjectSummary()
        {
            var response = await _projectService.GetProjectSummaryAsync();
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPatch("{id}/update-status/{statusId}")]
        public async Task<ActionResult<ApiResponse<ProjectDto>>> UpdateProjectStatus(int id, int statusId)
        {
            var response = await _projectService.UpdateProjectStatusAsync(id, statusId);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPatch("{id}/update-phase/{phaseId}")]
        public async Task<ActionResult<ApiResponse<ProjectDto>>> UpdateProjectPhase(int id, int phaseId)
        {
            var response = await _projectService.UpdateProjectPhaseAsync(id, phaseId);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}