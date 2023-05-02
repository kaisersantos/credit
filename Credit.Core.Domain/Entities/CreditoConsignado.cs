using Credit.Core.Domain.ValueObjects;

namespace Credit.Core.Domain.Entities
{
    public class CreditoConsignado : Credito
    {
        public override TipoCredito TipoCredito { get; } = TipoCredito.Consignado;

        public override decimal TaxaJuros { get; } = 1;
    }
}
