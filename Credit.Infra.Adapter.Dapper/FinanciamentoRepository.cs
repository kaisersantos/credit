using Credit.Core.Application.Adapters;
using Credit.Core.Domain.Entities;
using Credit.Infra.Adapter.Dapper.Config;

namespace Credit.Infra.Adapter.Dapper
{
    internal class FinanciamentoRepository : IFinanciamentoRepository
    {
        private readonly CreditDbContext _context;

        public FinanciamentoRepository(CreditDbContext context) =>
            _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<Financiamento> Create(Financiamento financiamento)
        {
            var insert = @"
                INSERT INTO FINANCIAMENTO (
	                UID,
	                TIPO_FINANCIAMENTO,
	                VALOR_TOTAL,
	                DATA_ULTIMO_VENCIMENTO,
	                CLIENTE_ID
                ) VALUES (
	                @Uid,
	                @TipoFinanciamento,
	                @ValorTotal,
	                @DataUltimoVencimento,
	                @ClienteId
                ) OUTPUT INSERTED.ID";

            var createdId = await _context.QuerySingleAsync<int>(insert, new {
                financiamento.Uid,
                TipoFinanciamento = financiamento.Credito!.TipoCredito.ToString(),
                financiamento.ValorTotal,
                financiamento.DataUltimoVencimento,
                financiamento.ClienteId
            });

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
            var insert = @"
                INSERT INTO PARCELA (
	                UID,
	                NUMERO_PARCELA,
	                VALOR_PARCELA,
	                DATA_VENCIMENTO,
	                FINANCIAMENTO_ID
                ) VALUES (
	                @Uid,
	                @NumeroParcela,
	                @ValorParcela,
	                @DataVencimento,
	                @FinanciamentoId
                ) OUTPUT INSERTED.ID";

            var createdId = await _context.QuerySingleAsync<int>(insert, new
            {
                parcela.Uid,
                parcela.NumeroParcela,
                parcela.ValorParcela,
                parcela.DataVencimento,
                parcela.FinanciamentoId
            });

            parcela.Id = createdId;

            return parcela;
        }
    }
}
