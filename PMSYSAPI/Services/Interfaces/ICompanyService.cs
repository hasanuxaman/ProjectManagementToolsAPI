using PMSYSAPI.DTOs.Common;
using PMSYSAPI.DTOs.Company;

namespace PMSYSAPI.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<ApiResponse<IEnumerable<CompanyDto>>> GetAllCompaniesAsync();
        Task<ApiResponse<CompanyDto>> GetCompanyByIdAsync(int id);
        Task<ApiResponse<CompanyDto>> CreateCompanyAsync(CreateCompanyDto createCompanyDto);
        Task<ApiResponse<CompanyDto>> CreateCompanyUsingSpAsync(CreateCompanyDto createCompanyDto);
        Task<ApiResponse<CompanyDto>> UpdateCompanyAsync(int id, UpdateCompanyDto updateCompanyDto);
        Task<ApiResponse> DeleteCompanyAsync(int id);
        Task<ApiResponse<IEnumerable<CompanyDto>>> SearchCompaniesAsync(string searchTerm);
        Task<ApiResponse<CompanySummaryDto>> GetCompanySummaryAsync();
    }
}