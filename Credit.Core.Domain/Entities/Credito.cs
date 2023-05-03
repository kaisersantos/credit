using Credit.Core.Domain.Exceptions.Credito;
using Credit.Core.Domain.Exceptions.EnumExtensions;
using Credit.Core.Domain.Extensions;
using Credit.Core.Domain.ValueObjects;

namespace Credit.Core.Domain.Entities
{
    public abstract class Credito
    {
        public virtual TipoCredito TipoCredito { get; }

        public virtual decimal TaxaJuros { get; }

        public virtual decimal CalcularJuros(decimal principal, short qtdMeses) => principal * (TaxaJuros / 100) * qtdMeses;

        public static Credito GetTipoCredito(string tipoCredito)
        {
            if (tipoCredito.Length != 1)
                throw new CreditoCoreDomainException(CreditoError.InvalidValue);

            return GetTipoCredito(tipoCredito[0]);
        }

        public static Credito GetTipoCredito(char tipoCredito)
        {
            try
            {
                var tipoFinanciamento = tipoCredito.ToEnumIgnoreCase<TipoCredito>();

                Credito credito = tipoFinanciamento switch
                {
                    TipoCredito.Direto => new CreditoDireto(),
                    TipoCredito.Consignado => new CreditoConsignado(),
                    TipoCredito.PessoaJuridica => new CreditoPessoaJuridica(),
                    TipoCredito.PessoaFisica => new CreditoPessoaFisica(),
                    TipoCredito.Imobiliario => new CreditoImobiliario(),
                    _ => throw new CreditoCoreDomainException(CreditoError.InvalidValue),
                };

                return credito;
            }
            catch (EnumExtensionsCoreDomainException)
            {
                throw new CreditoCoreDomainException(CreditoError.InvalidValue);
            }
        }
    }
}
