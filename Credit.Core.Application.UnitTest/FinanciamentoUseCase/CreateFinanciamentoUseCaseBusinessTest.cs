using Credit.Core.Application.Adapters;
using Credit.Core.Application.UnitTest.ClienteUseCase;
using Credit.Core.Application.UnitTest.ParcelaUseCase;
using Credit.Core.Application.UseCases.Clientes;
using Credit.Core.Application.UseCases.Financiamentos;
using Credit.Core.Application.UseCases.Financiamentos.Create;
using Credit.Core.Domain.Entities;

namespace Credit.Core.Application.UnitTest.FinanciamentoUseCase
{
    public class CreateFinanciamentoUseCaseTest
    {
        private readonly ICreateFinanciamentoUseCase _createFinanciamentoUseCase;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateFinanciamentoInput> _createFinanciamentoValidator;
        private readonly Mock<IFinanciamentoRepository> _financiamentoRepositoryMock;
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;
        private readonly Mock<IParcelaRepository> _parcelaRepositoryMock;

        public CreateFinanciamentoUseCaseTest()
        {
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<CreateFinanciamentoProfile>())
                .CreateMapper();
            _createFinanciamentoValidator = new CreateFinanciamentoInputValidator();
            _financiamentoRepositoryMock = new Mock<IFinanciamentoRepository>();
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _parcelaRepositoryMock = new Mock<IParcelaRepository>();

            _createFinanciamentoUseCase = new CreateFinanciamentoUseCase(
                _mapper,
                _createFinanciamentoValidator,
                _financiamentoRepositoryMock.Object,
                _clienteRepositoryMock.Object,
                _parcelaRepositoryMock.Object);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData("String inválido")]
        public async Task ShouldOccurUidInvalidoError(string uid)
        {
            var input = FinanciamentoMocks.GetCreateFinanciamentoInputMock;
            input.ClienteUid = uid;

            var ex = await Assert.ThrowsAsync<FinanciamentoCoreApplicationException>(
                () => _createFinanciamentoUseCase.Execute(input));

            Assert.Equal(ClienteError.UidInvalido(input.ClienteUid).Key, ex.Errors.First().Key);

            _clienteRepositoryMock.Verify(m => m.FindByUid(It.IsAny<Guid>()), Times.Never);
            _financiamentoRepositoryMock.Verify(m => m.Create(It.IsAny<Financiamento>()), Times.Never);

            _clienteRepositoryMock.VerifyNoOtherCalls();
            _financiamentoRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldOccurClienteNotFoundError()
        {
            var guidMock = FinanciamentoMocks.ClienteMock.Uid;
            var input = FinanciamentoMocks.GetCreateFinanciamentoInputMock;

            _clienteRepositoryMock
                .Setup(x => x.FindByUid(guidMock))
                .ReturnsAsync(null as Cliente);

            var ex = await Assert.ThrowsAsync<ClienteNotFoundException>(
                () => _createFinanciamentoUseCase.Execute(input));

            Assert.Equal(ClienteError.ClienteNotFoundByUid(guidMock).Key, ex.Errors.First().Key);

            _clienteRepositoryMock.Verify(m => m.FindByUid(guidMock), Times.Once);
            _financiamentoRepositoryMock.Verify(m => m.Create(It.IsAny<Financiamento>()), Times.Never);

            _clienteRepositoryMock.VerifyNoOtherCalls();
            _financiamentoRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldNotCreateFinanciamentoWhenValorCreditoIsGreaterThanOneMillion()
        {
            var clienteMock = FinanciamentoMocks.ClienteMock;

            var input = FinanciamentoMocks.GetCreateFinanciamentoInputMock;
            input.ValorCredito = 1000000.01M;

            var createdFinanciamentoOutputMock = new CreateFinanciamentoOutput()
            {
                StatusCredito = false,
            };

            _clienteRepositoryMock
                .Setup(x => x.FindByUid(clienteMock.Uid))
                .ReturnsAsync(clienteMock);

            _financiamentoRepositoryMock
                .Setup(x => x.Create(It.IsAny<Financiamento>()))
                .ReturnsAsync(FinanciamentoMocks.GetFinanciamentoMock);

            var createdFinanciamento = await _createFinanciamentoUseCase.Execute(input);

            Assert.Equivalent(createdFinanciamentoOutputMock, createdFinanciamento, true);

            _clienteRepositoryMock.Verify(m => m.FindByUid(clienteMock.Uid), Times.Once);
            _financiamentoRepositoryMock.Verify(m => m.Create(It.IsAny<Financiamento>()), Times.Never);

            _clienteRepositoryMock.VerifyNoOtherCalls();
            _financiamentoRepositoryMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(4)]
        [InlineData(73)]
        public async Task ShouldNotCreateFinanciamentoWhenQuantidadeParcelasInvalid(int quantidadeParcelas)
        {
            var clienteMock = FinanciamentoMocks.ClienteMock;

            var input = FinanciamentoMocks.GetCreateFinanciamentoInputMock;
            input.QuantidadeParcelas = quantidadeParcelas;

            var createdFinanciamentoOutputMock = new CreateFinanciamentoOutput()
            {
                StatusCredito = false,
            };

            _clienteRepositoryMock
                .Setup(x => x.FindByUid(clienteMock.Uid))
                .ReturnsAsync(clienteMock);

            _financiamentoRepositoryMock
                .Setup(x => x.Create(It.IsAny<Financiamento>()))
                .ReturnsAsync(FinanciamentoMocks.GetFinanciamentoMock);

            var createdFinanciamento = await _createFinanciamentoUseCase.Execute(input);

            Assert.Equivalent(createdFinanciamentoOutputMock, createdFinanciamento, true);

            _clienteRepositoryMock.Verify(m => m.FindByUid(clienteMock.Uid), Times.Once);
            _financiamentoRepositoryMock.Verify(m => m.Create(It.IsAny<Financiamento>()), Times.Never);

            _clienteRepositoryMock.VerifyNoOtherCalls();
            _financiamentoRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldNotCreateFinanciamentoWhenValorMinimoLessThan15KForTipoCreditoPessoaJuridica()
        {
            var clienteMock = FinanciamentoMocks.ClienteMock;

            var input = FinanciamentoMocks.GetCreateFinanciamentoInputMock;
            input.TipoCredito = "J";
            input.ValorCredito = 14999.99M;

            var createdFinanciamentoOutputMock = new CreateFinanciamentoOutput()
            {
                StatusCredito = false,
            };

            _clienteRepositoryMock
                .Setup(x => x.FindByUid(clienteMock.Uid))
                .ReturnsAsync(clienteMock);

            _financiamentoRepositoryMock
                .Setup(x => x.Create(It.IsAny<Financiamento>()))
                .ReturnsAsync(FinanciamentoMocks.GetFinanciamentoMock);

            var createdFinanciamento = await _createFinanciamentoUseCase.Execute(input);

            Assert.Equivalent(createdFinanciamentoOutputMock, createdFinanciamento, true);

            _clienteRepositoryMock.Verify(m => m.FindByUid(clienteMock.Uid), Times.Once);
            _financiamentoRepositoryMock.Verify(m => m.Create(It.IsAny<Financiamento>()), Times.Never);

            _clienteRepositoryMock.VerifyNoOtherCalls();
            _financiamentoRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldNotCreateFinanciamentoWhenDataPrimeiroVencimentoLessThan15DaysFromNow()
        {
            var clienteMock = FinanciamentoMocks.ClienteMock;

            var input = FinanciamentoMocks.GetCreateFinanciamentoInputMock;
            input.DataPrimeiroVencimento = FinanciamentoMocks.DataAtual.AddDays(14).Date;

            var createdFinanciamentoOutputMock = new CreateFinanciamentoOutput()
            {
                StatusCredito = false,
            };

            _clienteRepositoryMock
                .Setup(x => x.FindByUid(clienteMock.Uid))
                .ReturnsAsync(clienteMock);

            _financiamentoRepositoryMock
                .Setup(x => x.Create(It.IsAny<Financiamento>()))
                .ReturnsAsync(FinanciamentoMocks.GetFinanciamentoMock);

            var createdFinanciamento = await _createFinanciamentoUseCase.Execute(input);

            Assert.Equivalent(createdFinanciamentoOutputMock, createdFinanciamento, true);

            _clienteRepositoryMock.Verify(m => m.FindByUid(clienteMock.Uid), Times.Once);
            _financiamentoRepositoryMock.Verify(m => m.Create(It.IsAny<Financiamento>()), Times.Never);

            _clienteRepositoryMock.VerifyNoOtherCalls();
            _financiamentoRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldNotCreateFinanciamentoWhenDataPrimeiroVencimentoGreaterThan40DaysFromNow()
        {
            var clienteMock = FinanciamentoMocks.ClienteMock;

            var input = FinanciamentoMocks.GetCreateFinanciamentoInputMock;
            input.DataPrimeiroVencimento = FinanciamentoMocks.DataAtual.AddDays(41).Date;

            var createdFinanciamentoOutputMock = new CreateFinanciamentoOutput()
            {
                StatusCredito = false,
            };

            _clienteRepositoryMock
                .Setup(x => x.FindByUid(clienteMock.Uid))
                .ReturnsAsync(clienteMock);

            _financiamentoRepositoryMock
                .Setup(x => x.Create(It.IsAny<Financiamento>()))
                .ReturnsAsync(FinanciamentoMocks.GetFinanciamentoMock);

            var createdFinanciamento = await _createFinanciamentoUseCase.Execute(input);

            Assert.Equivalent(createdFinanciamentoOutputMock, createdFinanciamento, true);

            _clienteRepositoryMock.Verify(m => m.FindByUid(clienteMock.Uid), Times.Once);
            _financiamentoRepositoryMock.Verify(m => m.Create(It.IsAny<Financiamento>()), Times.Never);

            _clienteRepositoryMock.VerifyNoOtherCalls();
            _financiamentoRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldCreateFinanciamento()
        {
            var clienteMock = FinanciamentoMocks.ClienteMock;
            var input = FinanciamentoMocks.GetCreateFinanciamentoInputMock;
            var financiamentoMock = FinanciamentoMocks.GetFinanciamentoMock;

            _clienteRepositoryMock
                .Setup(x => x.FindByUid(clienteMock.Uid))
                .ReturnsAsync(clienteMock);

            _financiamentoRepositoryMock
                .Setup(x => x.Create(It.IsAny<Financiamento>()))
                .ReturnsAsync(financiamentoMock);

            _financiamentoRepositoryMock
                .Setup(x => x.Create(It.IsAny<Financiamento>()))
                .ReturnsAsync(financiamentoMock);

            _parcelaRepositoryMock
                .Setup(x => x.Create(It.IsAny<Parcela>()))
                .ReturnsAsync(ParcelaMocks.GetParcelaMock);

            await _createFinanciamentoUseCase.Execute(input);

            _clienteRepositoryMock.Verify(m => m.FindByUid(clienteMock.Uid), Times.Once);
            _financiamentoRepositoryMock.Verify(m => m.Create(It.IsAny<Financiamento>()), Times.Once);
            _parcelaRepositoryMock.Verify(m => m.Create(It.IsAny<Parcela>()), Times.Exactly(input.QuantidadeParcelas));

            _clienteRepositoryMock.VerifyNoOtherCalls();
            _financiamentoRepositoryMock.VerifyNoOtherCalls();
        }
    }
}