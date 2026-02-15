using PMSYSAPI.DTOs.Project;

namespace PMSYSAPI.Services.Interfaces
{
    public interface IProjectPhaseService
    {
        Task<ApiResponse<IEnumerable<ProjectPhaseDto>>> GetAllPhasesAsync();
        Task<ApiResponse<ProjectPhaseDto>> GetPhaseByIdAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<ApiResponse<ProjectPhaseDto>> CreatePhaseAsync(CreateProjectPhaseDto dto);
        Task<ApiResponse<ProjectPhaseDto>> UpdatePhaseAsync(int id, UpdateProjectPhaseDto dto);
        Task<ApiResponse> DeletePhaseAsync(int id);
    }
}