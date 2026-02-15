using PMSYSAPI.DTOs.Project;

namespace PMSYSAPI.Services.Interfaces
{
    public interface IProjectStatusService
    {
        Task<ApiResponse<IEnumerable<ProjectStatusDto>>> GetAllStatusesAsync();
        Task<ApiResponse<ProjectStatusDto>> GetStatusByIdAsync(int id);

       
        Task<ApiResponse<ProjectStatusDto>> CreateStatusAsync(CreateProjectStatusDto dto);
        Task<ApiResponse<ProjectStatusDto>> UpdateStatusAsync(int id, UpdateProjectStatusDto dto);
        Task<ApiResponse> DeleteStatusAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}