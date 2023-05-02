namespace Credit.Core.Application.UseCases.Financiamentos.Create
{
    public class CreateFinanciamentoOutput
    {
        public bool StatusCredito { get; set; }

        public decimal ValorTotalComJuros { get; set; }

        public decimal ValorJuros { get; set; }
    }
}
