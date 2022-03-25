using Microsoft.EntityFrameworkCore;
using ZAuth.Database.EntityConfig;
using ZepelimAdm.Business.Models;

namespace ZepelimAdm.Database
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Produto> Produtos  { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<EmpresaUser> EmpresaUser { get; set; }
        public DbSet<Pagina> Pagina  { get; set; }
        public DbSet<Acesso> Acesso { get; set; }
        public DbSet<AcessoPagina> AcessoPagina { get; set; }
        public DbSet<EmpresaProduto> EmpresaProduto { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProdutoConfig());
        }
    }
}
