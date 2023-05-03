using Credit.Core.Application.Adapters;
using Credit.Core.Domain.Entities;
using Credit.Infra.Adapter.EfCore.Config;

namespace Credit.Infra.Adapter.EfCore
{
    internal class FinanciamentoRepository : IFinanciamentoRepository
    {
        private readonly CreditDbContext _context;

        public FinanciamentoRepository(CreditDbContext context) =>
            _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<Financiamento> Create(Financiamento financiamento)
        {
            await _context.AddAsync(financiamento);
            await _context.SaveChangesAsync();

            return financiamento;
        }
    }
}
