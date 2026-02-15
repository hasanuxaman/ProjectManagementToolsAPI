using Microsoft.EntityFrameworkCore;
using PMSYSAPI.Data;
using PMSYSAPI.Models.Entities;
using PMSYSAPI.Repository.Interfaces;
using PMSYSAPI.Data;
using PMSYSAPI.Models.Entities;

namespace PMSYSAPI.Repository.Implementations
{
    public class ProjectStatusRepository : IProjectStatusRepository
    {
        private readonly AppDbContext _context;

        public ProjectStatusRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<tbStatus>> GetAllAsync()
        {
            return await _context.tbStatus.ToListAsync();
        }

        public async Task<tbStatus?> GetByIdAsync(int id)
        {
            return await _context.tbStatus.FindAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.tbStatus.AnyAsync(s => s.ProjStatusCod == id);
        }
    }
}