using Credit.Core.Domain.ValueObjects;

namespace Credit.Core.Domain.Entities
{
    public class Financiamento
    {
        public long Id { get; set; }

        public Guid Uid { get; set; }

        public TipoFinanciamento TipoFinanciamento { get; set; }

        public decimal ValorTotal { get; set; }

        public DateTime DataUltimoVencimento { get; set; }

        public long ClienteId { get; set; }

        public virtual Cliente? Cliente { get; set; }

        public virtual ICollection<Parcela>? Parcelas { get; set; }
    }
}
