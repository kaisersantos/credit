using AutoMapper;
using Credit.Core.Application.Adapters;
using Credit.Core.Application.UseCases.Clientes;
using Credit.Core.Application.UseCases.Clientes.Create;
using Credit.Core.Domain.Entities;
using Credit.Core.Domain.Extensions;
using Credit.Core.Domain.ValueObjects;
using FluentValidation;

namespace Credit.Core.Application.UseCases.Financiamentos.Create
{
    public class CreateFinanciamentoUseCase : ICreateFinanciamentoUseCase
    {
        private readonly IMapper _mapper;
        private readonly IValidator<CreateFinanciamentoInput> _validator;
        private readonly IFinanciamentoRepository _financiamentoRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IParcelaRepository _parcelaRepository;

        public CreateFinanciamentoUseCase(
            IMapper mapper,
            IValidator<CreateFinanciamentoInput> validator,
            IFinanciamentoRepository financiamentoRepository,
            IClienteRepository clienteRepository,
            IParcelaRepository parcelaRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _financiamentoRepository = financiamentoRepository ??
                throw new ArgumentNullException(nameof(financiamentoRepository));
            _clienteRepository = clienteRepository ??
                throw new ArgumentNullException(nameof(clienteRepository));
            _parcelaRepository = parcelaRepository ??
                throw new ArgumentNullException(nameof(parcelaRepository));
        }

        public async Task<CreateFinanciamentoOutput> Execute(CreateFinanciamentoInput input)
        {
            await ValidateInput(input);

            var cliente = await ValidateCliente(input);

            if (ValidateBusiness(input))
            {
                var financiamento = _mapper.Map<Financiamento>(input);

                financiamento.Uid = Guid.NewGuid();
                financiamento.ClienteId = cliente.Id;
                financiamento.DataUltimoVencimento = input.DataPrimeiroVencimento.AddMonths(input.QuantidadeParcelas).Date;

                var createdFinanciamento = await _financiamentoRepository.Create(financiamento);

                createdFinanciamento.Parcelas = await CreateParcelas(input, createdFinanciamento);

                var result = _mapper.Map<CreateFinanciamentoOutput>(createdFinanciamento);

                result.StatusCredito = true;

                return result;
            }

            return new CreateFinanciamentoOutput();
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
                throw new FinanciamentoCoreApplicationException(FinanciamentoError.UidInvalido(input.ClienteUid));

            var cliente = await _clienteRepository.FindByUid(clienteUid) ??
                throw new ClienteNotFoundException(ClienteError.ClienteNotFoundByUid(clienteUid));

            return cliente;
        }

        private static bool ValidateBusiness(CreateFinanciamentoInput input)
        {
            decimal oneMillion = 1000000;

            if (input.ValorCredito > oneMillion)
                return false;

            if (input.QuantidadeParcelas < 5 || input.QuantidadeParcelas > 72)
                return false;

            if (input.TipoCredito?.ToEnum<TipoCredito>() == TipoCredito.PessoaJuridica && input.ValorCredito < 15000)
                return false;

            if (input.DataPrimeiroVencimento.Date < DateTime.Now.AddDays(15).Date || input.DataPrimeiroVencimento.Date > DateTime.Now.AddDays(40).Date)
                return false;

            return true;
        }

        private async Task<List<Parcela>> CreateParcelas(CreateFinanciamentoInput input, Financiamento financiamento)
        {
            List<Parcela> createdParcelas = new();

            for (int i = 0; i < input.QuantidadeParcelas; i++)
            {
                createdParcelas.Add(await _parcelaRepository.Create(new()
                {
                    Uid = Guid.NewGuid(),
                    NumeroParcela = (short)(i + 1),
                    DataVencimento = input.DataPrimeiroVencimento.AddMonths(i),
                    ValorParcela = financiamento.ValorTotalComJuros / input.QuantidadeParcelas,
                    FinanciamentoId = financiamento.Id
                }));
            }

            return createdParcelas;
        }
    }
}
