namespace Credit.Core.Domain.Entities
{
    public class Parcela
    {
        public long Id { get; set; }

        public Guid Uid { get; set; }

        public short NumeroParcela { get; set; }

        public decimal ValorParcela { get; set; }

        public DateTime DataVencimento { get; set; }

        public DateTime? DataPagamento { get; set; }

        public long FinanciamentoId { get; set; }

        public virtual Financiamento? Financiamento { get; set; }
    }
}
