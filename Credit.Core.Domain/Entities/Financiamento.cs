namespace Credit.Core.Domain.Entities
{
    public class Financiamento
    {
        public long Id { get; set; }

        public Guid Uid { get; set; }

        public decimal ValorTotal { get; set; } = 0;

        public short QuantidadeParcelas { get; set; } = 0;

        public DateTime DataPrimeiroVencimento { get; set; }

        public DateTime DataUltimoVencimento { get => DataPrimeiroVencimento.AddMonths(QuantidadeParcelas); }

        public Credito Credito { get; set; }

        public decimal ValorJuros { get => Credito.CalcularJuros(ValorTotal, QuantidadeParcelas); }

        public long ClienteId { get; set; }

        public virtual Cliente? Cliente { get; set; }

        public virtual ICollection<Parcela>? Parcelas { get; set; }

        public Financiamento()
        {
            if (Credito == null)
                throw new Exception();
        }
    }
}
