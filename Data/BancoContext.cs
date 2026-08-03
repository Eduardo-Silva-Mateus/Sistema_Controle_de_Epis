using Controle_de_Epis.Models;
using Controle_de_Epis.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Controle_de_Epis.Data
{
    public class BancoContext : IdentityDbContext<ApplicationUser>
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
        }

        public DbSet<ColaboradorModel> Colaboradores { get; set; }
        public DbSet<TipoEpiModel> TipoEpi { get; set; }
        public DbSet<EpiModel> Epis { get; set; }
        public DbSet<EstoqueEpiModel> EstoqueEpis { get; set; }
        public DbSet<EmprestimoEpiModel> EmprestimoEpis { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
