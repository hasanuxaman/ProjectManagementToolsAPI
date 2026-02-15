using PMSYSAPI.DTOs.Project;

namespace PMSYSAPI.Services.Interfaces
{
    public interface IProjectStatusService
    {
        Task<ApiResponse<IEnumerable<ProjectStatusDto>>> GetAllStatusesAsync();
        Task<ApiResponse<ProjectStatusDto>> GetStatusByIdAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}