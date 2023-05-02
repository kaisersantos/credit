using Credit.Core.Domain.ValueObjects;

namespace Credit.Core.Domain.Entities
{
    public class CreditoDireto : Credito
    {
        public override TipoCredito TipoCredito { get; } = TipoCredito.Direto;

        public override decimal TaxaJuros { get; } = 2;
    }
}
