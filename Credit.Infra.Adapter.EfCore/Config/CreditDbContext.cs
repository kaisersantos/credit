using Credit.Core.Domain.Entities;
using Credit.Infra.Adapter.EfCore.Config.Entities;
using Microsoft.EntityFrameworkCore;

namespace Credit.Infra.Adapter.EfCore.Config
{
    internal class CreditDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Financiamento> Financiamentos { get; set; }

        public DbSet<Parcela> Parcelas { get; set; }

        public CreditDbContext(DbContextOptions<CreditDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            modelBuilder.ApplyConfiguration(new FinanciamentoConfiguration());
            modelBuilder.ApplyConfiguration(new ParcelaConfiguration());
        }
    }
}