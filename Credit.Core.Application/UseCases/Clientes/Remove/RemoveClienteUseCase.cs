using Credit.Core.Application.Adapters;
using Credit.Core.Application.UseCases.Financiamentos;

namespace Credit.Core.Application.UseCases.Clientes.Remove
{
    public class RemoveClienteUseCase : IRemoveClienteUseCase
    {
        private readonly IClienteRepository _clienteRepository;

        public RemoveClienteUseCase(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository ??
                throw new ArgumentNullException(nameof(clienteRepository));
        }

        public async Task Execute(string clienteUid)
        {
            if (!Guid.TryParse(clienteUid, out var clienteUidParsed))
                throw new ClienteCoreApplicationException(ClienteError.UidInvalido(clienteUid));

            var clienteDb = await _clienteRepository.FindByUid(clienteUidParsed) ??
                throw new ClienteNotFoundException(ClienteError.ClienteNotFoundByUid(clienteUidParsed));

            var removed = await _clienteRepository.Remove(clienteDb);

            if (!removed)
                throw new ClienteCoreApplicationException(ClienteError.UnableToRemoveCliente(clienteUidParsed));
        }
    }
}
