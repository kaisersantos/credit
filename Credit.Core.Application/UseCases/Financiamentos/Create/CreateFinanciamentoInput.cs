namespace Credit.Core.Application.UseCases.Financiamentos.Create
{
    public class CreateFinanciamentoInput
    {
        public string ClienteUid { get; set; } = string.Empty;

        public decimal ValorCredito { get; set; }

        public string TipoCredito { get; set; } = string.Empty;

        public int QuantidadeParcelas { get; set; }

        public DateTime DataPrimeiroVencimento { get; set; }
    }
}
