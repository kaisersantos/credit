using Credit.Core.Application.Adapters;
using Credit.Core.Domain.Entities;
using Credit.Infra.Adapter.Dapper.Config;
using System.Data;

namespace Credit.Infra.Adapter.Dapper
{
    internal class ParcelaRepository : IParcelaRepository
    {
        private readonly CreditDbContext _context;

        private readonly string SELECT = @"
            SELECT
                PAR.ID,
                PAR.UID,
                PAR.NUMERO_PARCELA NUMEROPARCELA,
                PAR.VALOR_PARCELA VALORPARCELA,
                PAR.DATA_VENCIMENTO DATAVENCIMENTO,
                PAR.DATA_PAGAMENTO DATAPAGAMENTO
            FROM PARCELA PAR";

        public ParcelaRepository(CreditDbContext context) =>
            _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<Parcela> Create(Parcela parcela)
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

        public async Task<bool> Pagar(Parcela parcela)
        {
            var update = @"SP_PARCELA_UPDATE_PAGAMENTO";

            var affectedRows = await _context.ExecuteAsync(update, new
            {
                parcela.DataPagamento,
                parcela.Id
            }, CommandType.StoredProcedure);

            return affectedRows > 0;
        }

        public async Task<Parcela?> FindByUid(Guid uid)
        {
            var select = $@"
                {SELECT}
                WHERE PAR.UID = @Uid";

            return await _context.QueryFirstOrDefault<Parcela>(select, new
            {
                Uid = uid
            });
        }
    }
}
