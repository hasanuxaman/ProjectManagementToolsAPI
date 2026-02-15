using Microsoft.EntityFrameworkCore;
using PMSYSAPI.Data;
using PMSYSAPI.Models.Entities;
using PMSYSAPI.Repository.Interfaces;
using PMSYSAPI.Data;
using PMSYSAPI.Models.Entities;

namespace PMSYSAPI.Repository.Implementations
{
    public class CompanyGroupRepository : ICompanyGroupRepository
    {
        private readonly AppDbContext _context;

        public CompanyGroupRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<tbCompGrp>> GetAllAsync()
        {
            return await _context.tbCompGrp.ToListAsync();
        }

        public async Task<tbCompGrp?> GetByIdAsync(int id)
        {
            return await _context.tbCompGrp.FindAsync(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.tbCompGrp.AnyAsync(cg => cg.GroupCod == id);
        }
    }
}