using Credit.Core.Application.UseCases.Parcelas.Pagar;
using Credit.Core.Domain.Entities;

namespace Credit.Core.Application.UnitTest.ParcelaUseCase
{
    internal static class ParcelaMocks
    {
        public readonly static Guid RandomGuidMock = Guid.NewGuid();
        public readonly static DateTime DataAtual = DateTime.Now;

        public static PagarParcelaInput GetPagarParcelaInputMock => new()
        {
            Uid = RandomGuidMock.ToString(),
            DataPagamento = DataAtual.Date
        };

        public static Parcela GetParcelaMock => new()
        {
            Uid = RandomGuidMock,
            NumeroParcela = 1,
            ValorParcela = 1100,
            DataVencimento = DataAtual.Date,
            FinanciamentoId = 1
        };
    }
}
