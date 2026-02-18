using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PMSYSAPI.Data;
using PMSYSAPI.DTOs.Project;
using PMSYSAPI.Models.Entities;
using PMSYSAPI.Repository.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        //public async Task<ProjectDto?> GetByIdAsync(int id)
        //{
        //    var project = await (from p in _context.tbProjList
        //                         join c in _context.tbComp
        //                             on p.ProjCompCod equals c.CompCod into pc
        //                         from c in pc.DefaultIfEmpty() 

        //                         join ph in _context.tbProjPhase
        //                             on p.ProjPhaseCod equals ph.PhaseCod into pp
        //                         from ph in pp.DefaultIfEmpty() 

        //                         where p.ProjCod == id

        //                         select new ProjectDto
        //                         {
        //                             ProjCod = p.ProjCod,
        //                             ProjName = p.ProjName,
        //                             CompName = c != null ? c.Compname : null,
        //                             PhaseName = ph != null ? ph.PhaseName : null,
        //                             InitDate = p.ProjInitDate
        //                         }).FirstOrDefaultAsync();

        //    return project;
        //}
        public async Task<tbProjList?> GetByIdAsync(int id)
        {
            var project = await (from p in _context.tbProjList
                                 join c in _context.tbComp
                                     on p.ProjCompCod equals c.CompCod into pc
                                 from c in pc.DefaultIfEmpty()  // left join

                                 join ph in _context.tbProjPhase
                                     on p.ProjPhaseCod equals ph.PhaseCod into pp
                                 from ph in pp.DefaultIfEmpty()  // left join

                                 where p.ProjCod == id

                                 select new tbProjList
                                 {
                                     ProjCod = p.ProjCod,
                                     ProjName = p.ProjName,
                                     ProjShortname = p.ProjShortname,
                                     ProjDesc = p.ProjDesc,
                                     ProjCompCod = p.ProjCompCod,
                                     ProjPhaseCod = p.ProjPhaseCod,
                                     ProjStsCod = p.ProjStsCod,
                                     ProjInitDate = p.ProjInitDate,
                                     ProjStrtPlndt = p.ProjStrtPlndt,
                                     ProjEndPlandt = p.ProjEndPlandt,
                                     ProjEstAmount = p.ProjEstAmount,
                                   
                                 }).FirstOrDefaultAsync();

            return project;
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