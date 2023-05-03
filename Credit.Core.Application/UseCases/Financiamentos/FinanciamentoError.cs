using Credit.Core.Domain.Exceptions;

namespace Credit.Core.Application.UseCases.Financiamentos
{
    public class FinanciamentoError : CoreError
    {
        public static FinanciamentoError UidInvalido(string? uid) =>
            new(nameof(UidInvalido),
                $"Invalid Uid '{(uid ?? "null")}'.");

        public static FinanciamentoError FinanciamentoNotFoundByUid(Guid uid) =>
            new(nameof(FinanciamentoNotFoundByUid),
                $"Financiamento not found with Uid '{uid}'.");

        public FinanciamentoError(string key, string message) : base(key, message) { }
    }
}
