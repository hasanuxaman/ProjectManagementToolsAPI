using PMSYSAPI.Models.Entities;
using PMSYSAPI.Models.Entities;

namespace PMSYSAPI.Repository.Interfaces
{
    public interface IProjectPhaseRepository
    {
        Task<IEnumerable<tbProjPhase>> GetAllAsync();
        Task<tbProjPhase?> GetByIdAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}