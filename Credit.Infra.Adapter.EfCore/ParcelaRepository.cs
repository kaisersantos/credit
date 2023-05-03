using Credit.Core.Application.Adapters;
using Credit.Core.Domain.Entities;
using Credit.Infra.Adapter.EfCore.Config;
using Microsoft.EntityFrameworkCore;

namespace Credit.Infra.Adapter.EfCore
{
    internal class ParcelaRepository : IParcelaRepository
    {
        private readonly CreditDbContext _context;

        public ParcelaRepository(CreditDbContext context) =>
            _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<bool> Pagar(Parcela parcela)
        {
            _context.Update(parcela);
            var affectedRows = await _context.SaveChangesAsync();

            return affectedRows > 0;
        }

        public async Task<Parcela?> FindByUid(Guid uid)
        {
            return await _context.Parcelas
                .Where(parcela => parcela.Uid == uid)
                .FirstOrDefaultAsync();
        }
    }
}
