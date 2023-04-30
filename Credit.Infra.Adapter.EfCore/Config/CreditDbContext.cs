using Microsoft.EntityFrameworkCore;

namespace Credit.Infra.Adapter.EfCore.Config
{
    internal class CreditDbContext : DbContext
    {
        public CreditDbContext(DbContextOptions<CreditDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}