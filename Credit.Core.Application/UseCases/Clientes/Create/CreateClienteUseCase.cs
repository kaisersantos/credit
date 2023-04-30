using AutoMapper;
using Credit.Core.Application.Adapters;
using Credit.Core.Domain.Entities;
using FluentValidation;

namespace Credit.Core.Application.UseCases.Clientes.Create
{
    internal class CreateClienteUseCase : ICreateClienteUseCase
    {
        private readonly IMapper _mapper;
        private readonly IValidator<CreateClienteInput> _validator;
        private readonly IClienteRepository _clienteRepository;

        public CreateClienteUseCase(
            IMapper mapper,
            IValidator<CreateClienteInput> validator,
            IClienteRepository clienteRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _clienteRepository = clienteRepository ??
                throw new ArgumentNullException(nameof(clienteRepository));
        }

        public async Task<CreateClienteOutput> Execute(CreateClienteInput input)
        {
            await ValidateInput(input);

            var cliente = _mapper.Map<Cliente>(input);

            await ValidateBusiness(cliente);

            var createdCliente = await _clienteRepository.Create(cliente);

            return _mapper.Map<CreateClienteOutput>(createdCliente);
        }

        private async Task ValidateInput(CreateClienteInput input)
        {
            var validation = await _validator.ValidateAsync(input);

            if (!validation.IsValid)
                throw new ClienteDomainException(validation.Errors);
        }

        private async Task ValidateBusiness(Cliente cliente)
        {
            var found = await _clienteRepository.FindByCpf(cliente.Cpf);

            if (found != null)
                throw new ClienteDomainException(ClienteError.CpfAlreadyExists(cliente.Cpf));
        }
    }
}
