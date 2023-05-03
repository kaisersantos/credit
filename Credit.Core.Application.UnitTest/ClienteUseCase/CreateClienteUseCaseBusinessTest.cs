using Credit.Core.Application.Adapters;
using Credit.Core.Application.UseCases.Clientes;
using Credit.Core.Application.UseCases.Clientes.Create;
using Credit.Core.Domain.Entities;
using Credit.Core.Domain.ValueObjects;

namespace Credit.Core.Application.UnitTest.ClienteUseCase
{
    public class CreateClienteUseCaseTest
    {
        private readonly ICreateClienteUseCase _createClienteUseCase;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateClienteInput> _createClienteValidator;
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;

        public CreateClienteUseCaseTest()
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

        [Fact]
        public async Task ShouldOccurCpfAlreadyExistsError()
        {
            var input = ClienteMocks.GetCreateClienteInputMock;

            _clienteRepositoryMock
                .Setup(x => x.FindByCpf(It.IsAny<Cpf>()))
                .ReturnsAsync(new Cliente());

            var ex = await Assert.ThrowsAsync<ClienteCoreApplicationException>(
                () => _createClienteUseCase.Execute(input));

            Assert.Equal(ClienteError.CpfAlreadyExists(input.Cpf).Key, ex.Errors.First().Key);

            _clienteRepositoryMock.Verify(m => m.FindByCpf(input.Cpf), Times.Once);
            _clienteRepositoryMock.Verify(m => m.Create(It.IsAny<Cliente>()), Times.Never);
            _clienteRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldCreateNewCliente()
        {
            var input = ClienteMocks.GetCreateClienteInputMock;
            var createClienteOutputMock = ClienteMocks.GetCreateClienteOutputMock;
            var clienteMock = ClienteMocks.GetClienteMock;

            _clienteRepositoryMock
                .Setup(x => x.FindByCpf(It.IsAny<Cpf>()))
                .ReturnsAsync(null as Cliente);

            _clienteRepositoryMock
                .Setup(x => x.Create(It.IsAny<Cliente>()))
                .ReturnsAsync(clienteMock);

            var createdCliente = await _createClienteUseCase.Execute(input);

            Assert.Equivalent(createClienteOutputMock, createdCliente, true);

            _clienteRepositoryMock.Verify(m => m.FindByCpf(input.Cpf), Times.Once);
            _clienteRepositoryMock.Verify(m => m.Create(It.IsAny<Cliente>()), Times.Once);
            _clienteRepositoryMock.VerifyNoOtherCalls();
        }
    }
}
