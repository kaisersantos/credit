using Credit.Core.Application.Adapters;
using Credit.Core.Application.UseCases.Clientes;
using Credit.Core.Application.UseCases.Clientes.Create;
using Credit.Core.Domain.Entities;
using Credit.Core.Domain.ValueObjects;

namespace Credit.Core.Application.UnitTest.ClienteUseCase
{
    public class CreateClienteUseCaseValidationTest
    {
        private readonly ICreateClienteUseCase _createClienteUseCase;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateClienteInput> _createClienteValidator;
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;

        public CreateClienteUseCaseValidationTest()
        {
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<CreateClienteProfile>())
                .CreateMapper();
            _createClienteValidator = new CreateClienteInputValidator();
            _clienteRepositoryMock = new Mock<IClienteRepository>();

            _createClienteUseCase = new CreateClienteUseCase(
                _mapper,
                _createClienteValidator,
                _clienteRepositoryMock.Object);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData("123456789100")]
        public async Task ShouldOccurCpfValidationError(string valor)
        {
            var input = ClienteMocks.GetCreateClienteInputMock;
            input.Cpf = valor;

            var ex = await Assert.ThrowsAsync<ClienteCoreApplicationException>(
                () => _createClienteUseCase.Execute(input));

            Assert.Equal(nameof(CreateClienteInput.Cpf), ex.Errors.First().Key);

            _clienteRepositoryMock.Verify(m => m.FindByCpf(It.IsAny<Cpf>()), Times.Never);
            _clienteRepositoryMock.Verify(m => m.Create(It.IsAny<Cliente>()), Times.Never);
            _clienteRepositoryMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public async Task ShouldOccurNomeValidationError(string valor)
        {
            var input = ClienteMocks.GetCreateClienteInputMock;
            input.Nome = valor;

            var ex = await Assert.ThrowsAsync<ClienteCoreApplicationException>(
                () => _createClienteUseCase.Execute(input));

            Assert.Equal(nameof(CreateClienteInput.Nome), ex.Errors.First().Key);

            _clienteRepositoryMock.Verify(m => m.FindByCpf(It.IsAny<Cpf>()), Times.Never);
            _clienteRepositoryMock.Verify(m => m.Create(It.IsAny<Cliente>()), Times.Never);
            _clienteRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldOccurNomeValidationErrorWithMoreThan100Characters()
        {
            var input = ClienteMocks.GetCreateClienteInputMock;
            input.Nome = new string('K', 101);

            var ex = await Assert.ThrowsAsync<ClienteCoreApplicationException>(
                () => _createClienteUseCase.Execute(input));

            Assert.Equal(nameof(CreateClienteInput.Nome), ex.Errors.First().Key);

            _clienteRepositoryMock.Verify(m => m.FindByCpf(It.IsAny<Cpf>()), Times.Never);
            _clienteRepositoryMock.Verify(m => m.Create(It.IsAny<Cliente>()), Times.Never);
            _clienteRepositoryMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData("A")]
        [InlineData("AAA")]
        public async Task ShouldOccurUfValidationError(string valor)
        {
            var input = ClienteMocks.GetCreateClienteInputMock;
            input.Uf = valor;

            var ex = await Assert.ThrowsAsync<ClienteCoreApplicationException>(
                () => _createClienteUseCase.Execute(input));

            Assert.Equal(nameof(CreateClienteInput.Uf), ex.Errors.First().Key);

            _clienteRepositoryMock.Verify(m => m.FindByCpf(It.IsAny<Cpf>()), Times.Never);
            _clienteRepositoryMock.Verify(m => m.Create(It.IsAny<Cliente>()), Times.Never);
            _clienteRepositoryMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData("(11) 1111-1111")]
        [InlineData("(11) 11111-11111")]
        public async Task ShouldOccurCelularValidationError(string valor)
        {
            var input = ClienteMocks.GetCreateClienteInputMock;
            input.Celular = valor;

            var ex = await Assert.ThrowsAsync<ClienteCoreApplicationException>(
                () => _createClienteUseCase.Execute(input));

            Assert.Equal(nameof(CreateClienteInput.Celular), ex.Errors.First().Key);

            _clienteRepositoryMock.Verify(m => m.FindByCpf(It.IsAny<Cpf>()), Times.Never);
            _clienteRepositoryMock.Verify(m => m.Create(It.IsAny<Cliente>()), Times.Never);
            _clienteRepositoryMock.VerifyNoOtherCalls();
        }
    }
}
