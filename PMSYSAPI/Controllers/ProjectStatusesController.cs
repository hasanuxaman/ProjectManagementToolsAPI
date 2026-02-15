using Microsoft.AspNetCore.Mvc;
using PMSYSAPI.DTOs.Project;
using PMSYSAPI.Services.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class ProjectStatusesController : ControllerBase
{
    private readonly IProjectStatusService _projectStatusService;

    public ProjectStatusesController(IProjectStatusService projectStatusService)
    {
        _projectStatusService = projectStatusService;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<ProjectStatusDto>>>> GetProjectStatuses()
    {
        var response = await _projectStatusService.GetAllStatusesAsync();
        if (!response.Success) return BadRequest(response);
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<ProjectStatusDto>>> GetProjectStatus(int id)
    {
        var response = await _projectStatusService.GetStatusByIdAsync(id);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<ProjectStatusDto>>> CreateProjectStatus(CreateProjectStatusDto dto)
    {
        var response = await _projectStatusService.CreateStatusAsync(dto);
        return CreatedAtAction(nameof(GetProjectStatus), new { id = response.Data!.ProjStatusCod }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<ProjectStatusDto>>> UpdateProjectStatus(int id, UpdateProjectStatusDto dto)
    {
        var response = await _projectStatusService.UpdateStatusAsync(id, dto);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse>> DeleteProjectStatus(int id)
    {
        var response = await _projectStatusService.DeleteStatusAsync(id);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }
}
