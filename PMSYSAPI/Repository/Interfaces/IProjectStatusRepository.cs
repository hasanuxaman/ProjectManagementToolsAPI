using PMSYSAPI.Models.Entities;
using PMSYSAPI.Models.Entities;

namespace PMSYSAPI.Repository.Interfaces
{
    public interface IProjectStatusRepository
    {
        Task<IEnumerable<tbStatus>> GetAllAsync();
        Task<tbStatus?> GetByIdAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}