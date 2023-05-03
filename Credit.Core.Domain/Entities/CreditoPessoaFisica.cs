using Credit.Core.Domain.ValueObjects;

namespace Credit.Core.Domain.Entities
{
    public class CreditoPessoaFisica : Credito
    {
        public override TipoCredito TipoCredito { get; } = TipoCredito.PessoaFisica;

        public override decimal TaxaJuros { get; } = 3;
    }
}
