using Credit.Core.Domain.ValueObjects;

namespace Credit.Core.Domain.Entities
{
    public class CreditoPessoaJuridica : Credito
    {
        public override TipoCredito TipoCredito { get; } = TipoCredito.PessoaJuridica;

        public override decimal TaxaJuros { get; } = 5;
    }
}
