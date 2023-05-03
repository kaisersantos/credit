using Credit.Core.Application.Adapters;
using Credit.Core.Application.UseCases.Financiamentos;
using FluentValidation;

namespace Credit.Core.Application.UseCases.Clientes.ChangeCpf
{
    public class ChangeCpfClienteUseCase : IChangeCpfClienteUseCase
    {
        private readonly IValidator<ChangeCpfClienteInput> _validator;
        private readonly IClienteRepository _clienteRepository;

        public ChangeCpfClienteUseCase(
            IValidator<ChangeCpfClienteInput> validator,
            IClienteRepository clienteRepository)
        {
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _clienteRepository = clienteRepository ??
                throw new ArgumentNullException(nameof(clienteRepository));
        }

        public async Task Execute(string clienteUid, ChangeCpfClienteInput input)
        {
            await ValidateInput(input);

            if (!Guid.TryParse(clienteUid, out var clienteUidParsed))
                throw new ClienteCoreApplicationException(ClienteError.UidInvalido(clienteUid));

            var clienteDb = await _clienteRepository.FindByUid(clienteUidParsed) ??
                throw new ClienteNotFoundException(ClienteError.ClienteNotFoundByUid(clienteUidParsed));

            if (clienteDb.Cpf == input.Cpf)
                return;

            await VerifyOtherClienteWithThisCpf(input);

            clienteDb.Cpf = input.Cpf.Trim();

            var changedCpf = await _clienteRepository.Edit(clienteDb);

            if (!changedCpf)
                throw new ClienteCoreApplicationException(ClienteError.UnableToEditCliente(clienteUidParsed));
        }

        private async Task ValidateInput(ChangeCpfClienteInput input)
        {
            var validation = await _validator.ValidateAsync(input);

            if (!validation.IsValid)
                throw new ClienteCoreApplicationException(validation.Errors);
        }

        private async Task VerifyOtherClienteWithThisCpf(ChangeCpfClienteInput input)
        {
            var clienteDb = await _clienteRepository.FindByCpf(input.Cpf);

            if (clienteDb != null)
                throw new ClienteCoreApplicationException(ClienteError.CpfAlreadyExists(input.Cpf));
        }
    }
}
