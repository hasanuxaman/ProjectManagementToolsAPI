using PMSYSAPI.DTOs.Project;
using PMSYSAPI.Models.Entities;
using PMSYSAPI.Models.Entities;

namespace PMSYSAPI.Repository.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<tbProjList>> GetAllAsync();
        Task<tbProjList?> GetByIdAsync(int id);
        Task<tbProjList?> GetByNameAsync(string name);
        Task<tbProjList> CreateAsync(tbProjList project);
        Task UpdateAsync(tbProjList project);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<bool> ExistsByNameAsync(string name);
    }
}