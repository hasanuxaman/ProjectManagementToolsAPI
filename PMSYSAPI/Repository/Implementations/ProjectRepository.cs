using Microsoft.EntityFrameworkCore;
using PMSYSAPI.Repository.Interfaces;
using PMSYSAPI.Data;
using PMSYSAPI.Models.Entities;

namespace PMSYSAPI.Repository.Implementations
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _context;

        public ProjectRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<tbProjList>> GetAllAsync()
        {
            return await _context.tbProjList.ToListAsync();
        }

        public async Task<tbProjList?> GetByIdAsync(int id)
        {
            return await _context.tbProjList
                .FirstOrDefaultAsync(p => p.ProjCod == id);
        }

        public async Task<tbProjList?> GetByNameAsync(string name)
        {
            return await _context.tbProjList
                .FirstOrDefaultAsync(p => p.ProjName == name);
        }

        public async Task<tbProjList> CreateAsync(tbProjList project)
        {
            _context.tbProjList.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task UpdateAsync(tbProjList project)
        {
            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var project = await GetByIdAsync(id);
            if (project != null)
            {
                _context.tbProjList.Remove(project);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.tbProjList.AnyAsync(p => p.ProjCod == id);
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.tbProjList.AnyAsync(p => p.ProjName == name);
        }
    }
}