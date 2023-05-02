using Credit.Core.Application.Adapters;

namespace Credit.Core.Application.UseCases.Clientes.Remove
{
    internal class RemoveClienteUseCase : IRemoveClienteUseCase
    {
        private readonly IClienteRepository _clienteRepository;

        public RemoveClienteUseCase(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository ??
                throw new ArgumentNullException(nameof(clienteRepository));
        }

        public async Task Execute(Guid clienteUid)
        {
            var clienteDb = await _clienteRepository.FindByUid(clienteUid) ??
                throw new ClienteNotFoundException(ClienteError.ClienteNotFoundByUid(clienteUid));

            var removed = await _clienteRepository.Remove(clienteDb);

            if (!removed)
                throw new ClienteCoreApplicationException(ClienteError.UnableToRemoveCliente(clienteUid));
        }
    }
}
