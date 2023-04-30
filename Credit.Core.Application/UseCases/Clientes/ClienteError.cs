using Credit.Core.Application.Exceptions;
using Credit.Core.Domain.ValueObjects;

namespace Credit.Core.Application.UseCases.Clientes
{
    public class ClienteError : Error
    {
        public static ClienteError CpfAlreadyExists(Cpf cpf) =>
            new(nameof(CpfAlreadyExists),
                $"A client with '{cpf.ToMaskedString()}' CPF already exists.");

        public static ClienteError ClienteNotFoundByUid(Guid uid) =>
            new(nameof(ClienteNotFoundByUid),
                $"Client not found with Uid '{uid}'.");

        public static ClienteError UnableToEditClient(Guid uid) =>
            new(nameof(UnableToRemoveClient),
                $"Unable to edit client with Uid '{uid}'.");

        public static ClienteError UnableToRemoveClient(Guid uid) =>
            new(nameof(UnableToRemoveClient),
                $"Unable to remove client with Uid '{uid}'.");

        public ClienteError(string key, string message) : base(key, message) { }
    }
}
