using Microsoft.EntityFrameworkCore;
using PMSYSAPI.Data;
using PMSYSAPI.Models.Entities;
using PMSYSAPI.Repository.Interfaces;
using PMSYSAPI.Data;
using PMSYSAPI.Models.Entities;

namespace PMSYSAPI.Repository.Implementations
{
    public class ProjectPhaseRepository : IProjectPhaseRepository
    {
        private readonly AppDbContext _context;

        public ProjectPhaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<tbProjPhase>> GetAllAsync()
        {
            return await _context.tbProjPhase.ToListAsync();
        }

        public async Task<tbProjPhase?> GetByIdAsync(int id)
        {
            return await _context.tbProjPhase.FindAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.tbProjPhase.AnyAsync(p => p.PhaseCod == id);
        }
    }
}