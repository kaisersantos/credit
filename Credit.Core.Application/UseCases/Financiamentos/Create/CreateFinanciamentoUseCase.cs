using AutoMapper;
using Credit.Core.Application.Adapters;
using Credit.Core.Application.UseCases.Clientes;
using Credit.Core.Domain.Entities;
using FluentValidation;

namespace Credit.Core.Application.UseCases.Financiamentos.Create
{
    internal class CreateFinanciamentoUseCase : ICreateFinanciamentoUseCase
    {
        private readonly IMapper _mapper;
        private readonly IValidator<CreateFinanciamentoInput> _validator;
        private readonly IFinanciamentoRepository _financiamentoRepository;
        private readonly IClienteRepository _clienteRepository;

        public CreateFinanciamentoUseCase(
            IMapper mapper,
            IValidator<CreateFinanciamentoInput> validator,
            IFinanciamentoRepository financiamentoRepository,
            IClienteRepository clienteRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _financiamentoRepository = financiamentoRepository ??
                throw new ArgumentNullException(nameof(financiamentoRepository));
            _clienteRepository = clienteRepository ??
                throw new ArgumentNullException(nameof(clienteRepository));
        }

        public async Task<CreateFinanciamentoOutput> Execute(CreateFinanciamentoInput input)
        {
            await ValidateInput(input);

            var financiamento = _mapper.Map<Financiamento>(input);

            var cliente = await ValidateCliente(input);

            financiamento.ClienteId = cliente.Id;

            var createdFinanciamento = await _financiamentoRepository.Create(financiamento);

            return _mapper.Map<CreateFinanciamentoOutput>(createdFinanciamento);
        }

        private async Task ValidateInput(CreateFinanciamentoInput input)
        {
            var validation = await _validator.ValidateAsync(input);

            if (!validation.IsValid)
                throw new FinanciamentoCoreApplicationException(validation.Errors);
        }

        private async Task<Cliente> ValidateCliente(CreateFinanciamentoInput input)
        {
            if (!Guid.TryParse(input.ClienteUid, out var clienteUid))
                throw new FinanciamentoCoreApplicationException();

            var cliente = await _clienteRepository.FindByUid(clienteUid) ??
                throw new ClienteNotFoundException(ClienteError.ClienteNotFoundByUid(clienteUid));

            return cliente;
        }
    }
}
