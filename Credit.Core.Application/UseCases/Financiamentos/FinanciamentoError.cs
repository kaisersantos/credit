using Credit.Core.Domain.Exceptions;

namespace Credit.Core.Application.UseCases.Financiamentos
{
    public class FinanciamentoError : CoreError
    {

        public static FinanciamentoError FinanciamentoNotFoundByUid(Guid uid) =>
            new(nameof(FinanciamentoNotFoundByUid),
                $"Client not found with Uid '{uid}'.");

        public FinanciamentoError(string key, string message) : base(key, message) { }
    }
}
