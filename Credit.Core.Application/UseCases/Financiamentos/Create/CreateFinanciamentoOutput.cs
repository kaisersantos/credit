namespace Credit.Core.Application.UseCases.Financiamentos.Create
{
    public class CreateFinanciamentoOutput
    {
        public Guid Uid { get; set; }

        public bool StatusCredito { get; set; } = false;

        public decimal ValorTotalComJuros { get; set; }

        public decimal ValorJuros { get; set; }

        public IEnumerable<CreateFinanciamentoParcelaOutput> Parcelas { get; set; } = new List<CreateFinanciamentoParcelaOutput>();
    }
}
