using PMSYSAPI.Models.Entities;
using PMSYSAPI.Models.Entities;

namespace PMSYSAPI.Repository.Interfaces
{
    public interface ICompanyGroupRepository
    {
        Task<IEnumerable<tbCompGrp>> GetAllAsync();
        Task<tbCompGrp?> GetByIdAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}