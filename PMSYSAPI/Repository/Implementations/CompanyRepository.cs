using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PMSYSAPI.Data;
using PMSYSAPI.Models.Entities;
using PMSYSAPI.Repository.Interfaces;

namespace PMSYSAPI.Repository.Implementations
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppDbContext _context;

        public CompanyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<tbComp>> GetAllCompanyAsync()
        {
            return await _context.tbComp
                
                .ToListAsync();
        }

        public async Task<tbComp?> GetCompanyByIdAsync(int id)
        {
            return await _context.tbComp
               
                .FirstOrDefaultAsync(c => c.CompCod == id);
        }

        public async Task<tbComp?> GetCompanyByNameAsync(string name)
        {
            return await _context.tbComp
                .FirstOrDefaultAsync(c => c.Compname == name);
        }

        public async Task<tbComp> CreateCompanyAsync(tbComp company)
        {
            var newId = await _context.Database
                .ExecuteSqlRawAsync(
                    "EXEC SP_comp_add @CompName, @CompShortName, @CompGroupCod",
                    new SqlParameter("@CompName", company.Compname),
                    new SqlParameter("@CompShortName", company.CompShortname),
                    new SqlParameter("@CompGroupCod", company.CmpGroupCod)
                );

            return company;
        }

        public async Task UpdateCompanyAsync(tbComp company)
        {
            _context.Entry(company).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCompanyAsync(int id)
        {
            var company = await GetCompanyByIdAsync(id);
            if (company != null)
            {
                _context.tbComp.Remove(company);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsCompanyAsync(int id)
        {
            return await _context.tbComp.AnyAsync(c => c.CompCod == id);
        }

        public async Task<bool> ExistsCompanyByNameAsync(string name)
        {
            return await _context.tbComp.AnyAsync(c => c.Compname == name);
        }
    }
}