using Credit.Core.Application.UnitTest.ClienteUseCase;
using Credit.Core.Application.UseCases.Financiamentos.Create;
using Credit.Core.Domain.Entities;

namespace Credit.Core.Application.UnitTest.FinanciamentoUseCase
{
    internal static class FinanciamentoMocks
    {
        public readonly static Guid RandomGuidMock = Guid.NewGuid();
        public readonly static DateTime DataAtual = DateTime.Now;
        public readonly static Cliente ClienteMock = ClienteMocks.GetClienteMock;

        public static CreateFinanciamentoInput GetCreateFinanciamentoInputMock => new()
        {
            ClienteUid = ClienteMocks.GetClienteMock.Uid.ToString(),
            DataPrimeiroVencimento = DataAtual.AddDays(15).Date,
            QuantidadeParcelas = 5,
            TipoCredito = "D",
            ValorCredito = 5000
        };

        public static Financiamento GetFinanciamentoMock => new()
        {
            Id = 1,
            Uid = RandomGuidMock,
            QuantidadeParcelas = 5,
            Credito = new CreditoDireto(),
            DataPrimeiroVencimento = DataAtual.AddDays(15).Date,
            ValorTotal = 5000,
            Parcelas = new List<Parcela>()
            {
                new Parcela()
                {
                    Uid = RandomGuidMock,
                    NumeroParcela = 1,
                    ValorParcela = 1100,
                    DataVencimento = DataAtual.AddDays(15).Date,
                    FinanciamentoId = 1
                },
                new Parcela()
                {
                    Uid = RandomGuidMock,
                    NumeroParcela = 2,
                    ValorParcela = 1100,
                    DataVencimento = DataAtual.AddMonths(1).AddDays(15).Date,
                    FinanciamentoId = 1
                },
                new Parcela()
                {
                    Uid = RandomGuidMock,
                    NumeroParcela = 3,
                    ValorParcela = 1100,
                    DataVencimento = DataAtual.AddMonths(2).AddDays(15).Date,
                    FinanciamentoId = 1
                },
                new Parcela()
                {
                    Uid = RandomGuidMock,
                    NumeroParcela = 4,
                    ValorParcela = 1100,
                    DataVencimento = DataAtual.AddMonths(3).AddDays(15).Date,
                    FinanciamentoId = 1
                },
                new Parcela()
                {
                    Uid = RandomGuidMock,
                    NumeroParcela = 500,
                    ValorParcela = 1100,
                    DataVencimento = DataAtual.AddMonths(4).AddDays(15).Date,
                    FinanciamentoId = 1
                }
            }
        };

        public static CreateFinanciamentoOutput GetFinanciamentoOutputMock => new()
        {
            Uid = RandomGuidMock,
            StatusCredito = true,
            ValorJuros = 500,
            ValorTotalComJuros = 5500,
            Parcelas = new List<CreateFinanciamentoParcelaOutput>()
            {
                new CreateFinanciamentoParcelaOutput()
                {
                    Uid = RandomGuidMock,
                    NumeroParcela = 1,
                    ValorParcela = 1100,
                    DataVencimento = DataAtual.AddDays(15).Date,
                },
                new CreateFinanciamentoParcelaOutput()
                {
                    Uid = RandomGuidMock,
                    NumeroParcela = 2,
                    ValorParcela = 1100,
                    DataVencimento = DataAtual.AddMonths(1).AddDays(15).Date,
                },
                new CreateFinanciamentoParcelaOutput()
                {
                    Uid = RandomGuidMock,
                    NumeroParcela = 3,
                    ValorParcela = 1100,
                    DataVencimento = DataAtual.AddMonths(2).AddDays(15).Date,
                },
                new CreateFinanciamentoParcelaOutput()
                {
                    Uid = RandomGuidMock,
                    NumeroParcela = 4,
                    ValorParcela = 1100,
                    DataVencimento = DataAtual.AddMonths(3).AddDays(15).Date,
                },
                new CreateFinanciamentoParcelaOutput()
                {
                    Uid = RandomGuidMock,
                    NumeroParcela = 5,
                    ValorParcela = 1100,
                    DataVencimento = DataAtual.AddMonths(4).AddDays(15).Date,
                }
            }
        };
    }
}
