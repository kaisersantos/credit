namespace Credit.Core.Domain.Entities
{
    public class Financiamento
    {
        public long Id { get; set; }

        public Guid Uid { get; set; }

        public decimal ValorTotal { get; set; } = 0;

        public short QuantidadeParcelas { get; set; } = 0;

        public DateTime DataPrimeiroVencimento { get; set; }

        public DateTime DataUltimoVencimento { get; set; }

        public Credito? Credito { get; set; }

        public decimal ValorJuros { get => Credito != null ? Credito.CalcularJuros(ValorTotal, QuantidadeParcelas) : 0; }

        public decimal ValorTotalComJuros { get => ValorTotal + ValorJuros; }

        public long ClienteId { get; set; }

        public virtual Cliente? Cliente { get; set; }

        public virtual ICollection<Parcela>? Parcelas { get; set; }
    }
}
