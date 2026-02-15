using Microsoft.EntityFrameworkCore;
using PMSYSAPI.Models.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace PMSYSAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<tbComp> tbComp { get; set; }
        public DbSet<tbCompGrp> tbCompGrp { get; set; }
        public DbSet<tbProjList> tbProjList { get; set; }
        public DbSet<tbProjPhase> tbProjPhase { get; set; }
        public DbSet<tbStatus> tbStatus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
        }
    }
}
