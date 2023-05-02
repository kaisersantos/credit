using Credit.Core.Domain.ValueObjects;

namespace Credit.Core.Domain.Entities
{
    public class CreditoImobiliario : Credito
    {
        public override TipoCredito TipoCredito { get; } = TipoCredito.Imobiliario;

        public override decimal TaxaJuros { get; } = 9;
    }
}
