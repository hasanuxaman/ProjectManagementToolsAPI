using PMSYSAPI.DTOs.Company;

namespace PMSYSAPI.Services.Interfaces
{
    public interface ICompanyGroupService
    {
        Task<ApiResponse<IEnumerable<CompanyGroupDto>>> GetAllGroupsAsync();
        Task<ApiResponse<CompanyGroupDto>> GetGroupByIdAsync(int id);
        Task<ApiResponse<CompanyGroupDto>> CreateGroupAsync(CreateCompanyGroupDto dto);
        Task<ApiResponse<CompanyGroupDto>> UpdateGroupAsync(int id, UpdateCompanyGroupDto dto);
        Task<ApiResponse> DeleteGroupAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}