using Credit.Core.Domain.Exceptions;

namespace Credit.Core.Application.UseCases.Parcelas
{
    public class ParcelaError : CoreError
    {
        public static ParcelaError UidInvalido(string? uid) =>
            new(nameof(UidInvalido),
                $"Invalid Uid '{(uid ?? "null")}'.");

        public static ParcelaError ParcelaNotFoundByUid(Guid uid) =>
            new(nameof(ParcelaNotFoundByUid),
                $"Parcela not found with Uid '{uid}'.");

        public static ParcelaError UnableToPagarParcela(Guid uid) =>
            new(nameof(UnableToPagarParcela),
                $"Unable to pagar parcela with Uid '{uid}'.");

        public ParcelaError(string key, string message) : base(key, message) { }
    }
}
