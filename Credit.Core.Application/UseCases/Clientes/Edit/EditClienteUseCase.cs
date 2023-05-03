using AutoMapper;
using Credit.Core.Application.Adapters;
using Credit.Core.Application.UseCases.Financiamentos;
using Credit.Core.Domain.Entities;
using FluentValidation;

namespace Credit.Core.Application.UseCases.Clientes.Edit
{
    public class EditClienteUseCase : IEditClienteUseCase
    {
        private readonly IMapper _mapper;
        private readonly IValidator<EditClienteInput> _validator;
        private readonly IClienteRepository _clienteRepository;

        public EditClienteUseCase(
            IMapper mapper,
            IValidator<EditClienteInput> validator,
            IClienteRepository clienteRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _clienteRepository = clienteRepository ??
                throw new ArgumentNullException(nameof(clienteRepository));
        }

        public async Task Execute(string clienteUid, EditClienteInput input)
        {
            await ValidateInput(input);

            if (!Guid.TryParse(clienteUid, out var clienteUidParsed))
                throw new ClienteCoreApplicationException(ClienteError.UidInvalido(clienteUid));

            var editCliente = _mapper.Map<Cliente>(input);

            var clienteDb = await _clienteRepository.FindByUid(clienteUidParsed) ??
                throw new ClienteNotFoundException(ClienteError.ClienteNotFoundByUid(clienteUidParsed));

            clienteDb.Nome = editCliente.Nome;
            clienteDb.Uf = editCliente.Uf;
            clienteDb.Celular = editCliente.Celular;

            var edited = await _clienteRepository.Edit(clienteDb);

            if (!edited)
                throw new ClienteCoreApplicationException(ClienteError.UnableToEditCliente(clienteUidParsed));
        }

        private async Task ValidateInput(EditClienteInput input)
        {
            var validation = await _validator.ValidateAsync(input);

            if (!validation.IsValid)
                throw new ClienteCoreApplicationException(validation.Errors);
        }
    }
}
