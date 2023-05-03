namespace Credit.Core.Application.UseCases.Financiamentos.Create
{
    public class CreateFinanciamentoParcelaOutput
    {
        public Guid Uid { get; set; }

        public short NumeroParcela { get; set; }

        public decimal ValorParcela { get; set; }

        public DateTime DataVencimento { get; set; }
    }
}
