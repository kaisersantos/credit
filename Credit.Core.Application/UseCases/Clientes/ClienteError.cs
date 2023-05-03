using Credit.Core.Domain.Exceptions;
using Credit.Core.Domain.Exceptions.Cpf;
using Credit.Core.Domain.ValueObjects;

namespace Credit.Core.Application.UseCases.Clientes
{
    public class ClienteError : CoreError
    {
        public static ClienteError CpfInvalid =>
            new(nameof(CpfInvalid),
                $"This CPF is invalid.");

        public static ClienteError CpfAlreadyExists(Cpf cpf) =>
            new(nameof(CpfAlreadyExists),
                $"A cliente with '{cpf.ToMaskedString()}' CPF already exists.");

        public static ClienteError UidInvalido(string? uid) =>
            new(nameof(UidInvalido),
                $"Invalid Uid '{(uid ?? "null")}'.");

        public static ClienteError ClienteNotFoundByUid(Guid uid) =>
            new(nameof(ClienteNotFoundByUid),
                $"Cliente not found with Uid '{uid}'.");

        public static ClienteError UnableToEditCliente(Guid uid) =>
            new(nameof(UnableToEditCliente),
                $"Unable to edit cliente with Uid '{uid}'.");

        public static ClienteError UnableToRemoveCliente(Guid uid) =>
            new(nameof(UnableToRemoveCliente),
                $"Unable to remove cliente with Uid '{uid}'.");

        public ClienteError(string key, string message) : base(key, message) { }
    }
}
