using Credit.Core.Application.Adapters;
using Credit.Core.Application.UseCases.Financiamentos;
using Credit.Core.Application.UseCases.Financiamentos.Create;
using Credit.Core.Domain.Entities;
using Credit.Core.Domain.ValueObjects;

namespace Credit.Core.Application.UnitTest.FinanciamentoUseCase
{
    public class CreateFinanciamentoUseCaseValidationTest
    {
        private readonly ICreateFinanciamentoUseCase _createFinanciamentoUseCase;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateFinanciamentoInput> _createFinanciamentoValidator;
        private readonly Mock<IFinanciamentoRepository> _financiamentoRepositoryMock;
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;

        public CreateFinanciamentoUseCaseValidationTest()
        {
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<CreateFinanciamentoProfile>())
                .CreateMapper();
            _createFinanciamentoValidator = new CreateFinanciamentoInputValidator();
            _financiamentoRepositoryMock = new Mock<IFinanciamentoRepository>();
            _clienteRepositoryMock = new Mock<IClienteRepository>();

            _createFinanciamentoUseCase = new CreateFinanciamentoUseCase(
                _mapper,
                _createFinanciamentoValidator,
                _financiamentoRepositoryMock.Object,
                _clienteRepositoryMock.Object);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData("A")]
        [InlineData("AB")]
        public async Task ShouldOccurTipoCreditoValidationError(string valor)
        {
            var input = FinanciamentoMocks.GetCreateFinanciamentoInputMock;
            input.TipoCredito = valor;

            var ex = await Assert.ThrowsAsync<FinanciamentoCoreApplicationException>(
                () => _createFinanciamentoUseCase.Execute(input));

            Assert.Equal(nameof(CreateFinanciamentoInput.TipoCredito), ex.Errors.First().Key);

            _clienteRepositoryMock.Verify(m => m.FindByUid(It.IsAny<Guid>()), Times.Never);
            _financiamentoRepositoryMock.Verify(m => m.Create(It.IsAny<Financiamento>()), Times.Never);

            _clienteRepositoryMock.VerifyNoOtherCalls();
            _financiamentoRepositoryMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task ShouldOccurValorCreditoValidationError(decimal valorCredito)
        {
            var input = FinanciamentoMocks.GetCreateFinanciamentoInputMock;
            input.ValorCredito = valorCredito;

            var ex = await Assert.ThrowsAsync<FinanciamentoCoreApplicationException>(
                () => _createFinanciamentoUseCase.Execute(input));

            Assert.Equal(nameof(CreateFinanciamentoInput.ValorCredito), ex.Errors.First().Key);

            _clienteRepositoryMock.Verify(m => m.FindByUid(It.IsAny<Guid>()), Times.Never);
            _financiamentoRepositoryMock.Verify(m => m.Create(It.IsAny<Financiamento>()), Times.Never);

            _clienteRepositoryMock.VerifyNoOtherCalls();
            _financiamentoRepositoryMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task ShouldOccurQuantidadeParcelasValidationError(int quantidadeParcelas)
        {
            var input = FinanciamentoMocks.GetCreateFinanciamentoInputMock;
            input.QuantidadeParcelas = quantidadeParcelas;

            var ex = await Assert.ThrowsAsync<FinanciamentoCoreApplicationException>(
                () => _createFinanciamentoUseCase.Execute(input));

            Assert.Equal(nameof(CreateFinanciamentoInput.QuantidadeParcelas), ex.Errors.First().Key);

            _clienteRepositoryMock.Verify(m => m.FindByUid(It.IsAny<Guid>()), Times.Never);
            _financiamentoRepositoryMock.Verify(m => m.Create(It.IsAny<Financiamento>()), Times.Never);

            _clienteRepositoryMock.VerifyNoOtherCalls();
            _financiamentoRepositoryMock.VerifyNoOtherCalls();
        }
    }
}
