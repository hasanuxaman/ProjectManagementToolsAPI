using PMSYSAPI.DTOs.Project;

namespace PMSYSAPI.Services.Interfaces
{
    public interface IProjectService
    {
        Task<ApiResponse<IEnumerable<ProjectDto>>> GetAllProjectsAsync();
        Task<ApiResponse<ProjectDto>> GetProjectByIdAsync(int id);
        Task<ApiResponse<ProjectDto>> CreateProjectAsync(CreateProjectDto createProjectDto);
        Task<ApiResponse<ProjectDto>> CreateProjectUsingSpAsync(CreateProjectDto createProjectDto);
        Task<ApiResponse<ProjectDto>> UpdateProjectAsync(int id, UpdateProjectDto updateProjectDto);
        Task<ApiResponse> DeleteProjectAsync(int id);
        Task<ApiResponse<IEnumerable<ProjectDto>>> GetProjectsByCompanyAsync(int companyId);
        Task<ApiResponse<IEnumerable<ProjectDto>>> GetProjectsByStatusAsync(int statusId);
        Task<ApiResponse<IEnumerable<ProjectDto>>> GetProjectsByPhaseAsync(int phaseId);
        Task<ApiResponse<IEnumerable<ProjectDto>>> SearchProjectsAsync(string searchTerm);
        Task<ApiResponse<IEnumerable<ProjectDto>>> GetProjectsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<ApiResponse<ProjectSummaryDto>> GetProjectSummaryAsync();
        Task<ApiResponse<ProjectDto>> UpdateProjectStatusAsync(int id, int statusId);
        Task<ApiResponse<ProjectDto>> UpdateProjectPhaseAsync(int id, int phaseId);
    }
}