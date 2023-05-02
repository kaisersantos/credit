namespace Credit.Core.Domain.Exceptions.Credito
{
    public class CreditoError : CoreError
    {
        public static CreditoError InvalidValue =>
            new(nameof(InvalidValue),
                "TipoCredito must be " +
                "'D' (Crédito Direto), " +
                "'C' (Crédito Consignado), " +
                "'J' (Crédito Pessoa Jurídica), " +
                "'F' (Crédito Pessoa Física) or " +
                "'I' (Crédito Imobiliário).");

        public CreditoError(string key, string message) : base(key, message) { }
    }
}
