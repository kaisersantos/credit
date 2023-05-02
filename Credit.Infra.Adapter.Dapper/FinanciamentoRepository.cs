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
                TipoFinanciamento = financiamento.Credito!.TipoCredito.ToString(),
                financiamento.ValorTotal,
                financiamento.DataUltimoVencimento,
                financiamento.ClienteId
            }, CommandType.StoredProcedure);

            financiamento.Id = createdId;
            financiamento.Parcelas = await CreateParcelas(financiamento.Parcelas!.ToList());

            return financiamento;
        }

        public async Task<ICollection<Parcela>> CreateParcelas(List<Parcela> parcelas)
        {
            var tasks = new List<Task<Parcela>>();

            parcelas?.ToList().ForEach(parcela => tasks.Add(CreateParcela(parcela)));

            return await Task.WhenAll(tasks);
        }

        public async Task<Parcela> CreateParcela(Parcela parcela)
        {
            var insert = @"SP_PARCELA_INSERT";

            var createdId = await _context.QuerySingleAsync<int>(insert, new
            {
                parcela.Uid,
                parcela.NumeroParcela,
                parcela.ValorParcela,
                parcela.DataVencimento,
                parcela.FinanciamentoId
            }, CommandType.StoredProcedure);

            parcela.Id = createdId;

            return parcela;
        }
    }
}
