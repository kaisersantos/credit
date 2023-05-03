using Credit.Core.Application.Adapters;
using Credit.Core.Domain.Entities;
using Credit.Infra.Adapter.Dapper.Config;
using System.Data;

namespace Credit.Infra.Adapter.Dapper
{
    internal class FinanciamentoRepository : IFinanciamentoRepository
    {
        private readonly CreditDbContext _context;

        public FinanciamentoRepository(CreditDbContext context) =>
            _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<Financiamento> Create(Financiamento financiamento)
        {
            var insert = @"SP_FINANCIAMENTO_INSERT";

            var createdId = await _context.QuerySingleAsync<int>(insert, new
            {
                financiamento.Uid,
                TipoFinanciamento = (char)financiamento.Credito!.TipoCredito,
                financiamento.ValorTotal,
                financiamento.DataUltimoVencimento,
                financiamento.ClienteId
            }, CommandType.StoredProcedure);

            financiamento.Id = createdId;

            return financiamento;
        }
    }
}
