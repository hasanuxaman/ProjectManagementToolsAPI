using PMSYSAPI.Models.Entities;
using PMSYSAPI.Models.Entities;

namespace PMSYSAPI.Repository.Interfaces
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<tbComp>> GetAllCompanyAsync();
        Task<tbComp?> GetCompanyByIdAsync(int id);
        Task<tbComp?> GetCompanyByNameAsync(string name);
        Task<tbComp> CreateCompanyAsync(tbComp company);
        Task UpdateCompanyAsync(tbComp company);
        Task DeleteCompanyAsync(int id);
        Task<bool> ExistsCompanyAsync(int id);
        Task<bool> ExistsCompanyByNameAsync(string name);
    }
}