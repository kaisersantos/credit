namespace Credit.Core.Application.UseCases.Parcelas.Pagar
{
    public class PagarParcelaInput
    {
        public string Uid { get; set; } = string.Empty;

        public DateTime DataPagamento { get; set; }
    }
}
