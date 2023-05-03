using Credit.Core.Application.Adapters;
using Credit.Core.Application.UseCases.Clientes;
using Credit.Core.Application.UseCases.Clientes.ChangeCpf;
using Credit.Core.Application.UseCases.Clientes.Edit;
using Credit.Core.Domain.Entities;
using Credit.Core.Domain.ValueObjects;

namespace Credit.Core.Application.UnitTest.ClienteUseCase
{
    public class ChangeCpfClienteUseCaseTest
    {
        private readonly IChangeCpfClienteUseCase _changeCpfClienteUseCase;
        private readonly IValidator<ChangeCpfClienteInput> _changeCpfClienteValidator;
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;

        public ChangeCpfClienteUseCaseTest()
        {
            _changeCpfClienteValidator = new ChangeCpfClienteInputValidator();
            _clienteRepositoryMock = new Mock<IClienteRepository>();

            _changeCpfClienteUseCase = new ChangeCpfClienteUseCase(
                _changeCpfClienteValidator,
                _clienteRepositoryMock.Object);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData("String inválido")]
        public async Task ShouldOccurUidInvalidoError(string uid)
        {
            var input = ClienteMocks.GetChangeCpfClienteInputMock;

            var ex = await Assert.ThrowsAsync<ClienteCoreApplicationException>(
                () => _changeCpfClienteUseCase.Execute(uid, input));

            Assert.Equal(ClienteError.UidInvalido(uid).Key, ex.Errors.First().Key);

            _clienteRepositoryMock.Verify(m => m.FindByUid(It.IsAny<Guid>()), Times.Never);
            _clienteRepositoryMock.Verify(m => m.FindByCpf(It.IsAny<Cpf>()), Times.Never);
            _clienteRepositoryMock.Verify(m => m.Edit(It.IsAny<Cliente>()), Times.Never);
            _clienteRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldOccurClienteNotFoundByUidError()
        {
            var guidMock = ClienteMocks.RandomGuidMock;
            var input = ClienteMocks.GetChangeCpfClienteInputMock;

            _clienteRepositoryMock
                .Setup(x => x.FindByUid(guidMock))
                .ReturnsAsync(null as Cliente);

            var ex = await Assert.ThrowsAsync<ClienteNotFoundException>(
                () => _changeCpfClienteUseCase.Execute(guidMock.ToString(), input));

            Assert.Equal(ClienteError.ClienteNotFoundByUid(guidMock).Key, ex.Errors.First().Key);

            _clienteRepositoryMock.Verify(m => m.FindByUid(guidMock), Times.Once);
            _clienteRepositoryMock.Verify(m => m.FindByCpf(It.IsAny<Cpf>()), Times.Never);
            _clienteRepositoryMock.Verify(m => m.Edit(It.IsAny<Cliente>()), Times.Never);
            _clienteRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldNotEditClienteWhenCpfsAreSame()
        {
            var guidMock = ClienteMocks.RandomGuidMock;
            var input = ClienteMocks.GetChangeCpfClienteInputMock;
            var changeCpfClienteMock = ClienteMocks.GetClienteMock;

            _clienteRepositoryMock
                .Setup(x => x.FindByUid(guidMock))
                .ReturnsAsync(changeCpfClienteMock);

            _clienteRepositoryMock
                .Setup(x => x.FindByCpf(changeCpfClienteMock.Cpf))
                .ReturnsAsync(changeCpfClienteMock);

            await _changeCpfClienteUseCase.Execute(guidMock.ToString(), input);

            _clienteRepositoryMock.Verify(m => m.FindByUid(guidMock), Times.Once);
            _clienteRepositoryMock.Verify(m => m.FindByCpf(It.IsAny<Cpf>()), Times.Never);
            _clienteRepositoryMock.Verify(m => m.Edit(It.IsAny<Cliente>()), Times.Never);
            _clienteRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldOccurCpfAlreadyExistsError()
        {
            var guidMock = ClienteMocks.RandomGuidMock;

            var input = ClienteMocks.GetChangeCpfClienteInputMock;
            input.Cpf = "178.160.930-63";

            var changeCpfClienteMock = ClienteMocks.GetClienteMock;

            _clienteRepositoryMock
                .Setup(x => x.FindByUid(guidMock))
                .ReturnsAsync(changeCpfClienteMock);

            _clienteRepositoryMock
                .Setup(x => x.FindByCpf(input.Cpf))
                .ReturnsAsync(new Cliente());

            var ex = await Assert.ThrowsAsync<ClienteCoreApplicationException>(
                () => _changeCpfClienteUseCase.Execute(guidMock.ToString(), input));

            Assert.Equal(ClienteError.CpfAlreadyExists(input.Cpf).Key, ex.Errors.First().Key);

            _clienteRepositoryMock.Verify(m => m.FindByUid(guidMock), Times.Once);
            _clienteRepositoryMock.Verify(m => m.FindByCpf(input.Cpf), Times.Once);
            _clienteRepositoryMock.Verify(m => m.Edit(It.IsAny<Cliente>()), Times.Never);
            _clienteRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldEditClienteError()
        {
            var guidMock = ClienteMocks.RandomGuidMock;

            var input = ClienteMocks.GetChangeCpfClienteInputMock;
            input.Cpf = "178.160.930-63";

            var changeCpfClienteMock = ClienteMocks.GetClienteMock;

            _clienteRepositoryMock
                .Setup(x => x.FindByUid(guidMock))
                .ReturnsAsync(changeCpfClienteMock);

            _clienteRepositoryMock
                .Setup(x => x.FindByCpf(input.Cpf))
                .ReturnsAsync(null as Cliente);

            _clienteRepositoryMock
                .Setup(x => x.Edit(changeCpfClienteMock))
                .ReturnsAsync(false);

            var ex = await Assert.ThrowsAsync<ClienteCoreApplicationException>(
                () => _changeCpfClienteUseCase.Execute(guidMock.ToString(), input));

            Assert.Equal(ClienteError.UnableToEditCliente(guidMock).Key, ex.Errors.First().Key);

            _clienteRepositoryMock.Verify(m => m.FindByUid(guidMock), Times.Once);
            _clienteRepositoryMock.Verify(m => m.FindByCpf(input.Cpf), Times.Once);
            _clienteRepositoryMock.Verify(m => m.Edit(changeCpfClienteMock), Times.Once);
            _clienteRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldEditCliente()
        {
            var guidMock = ClienteMocks.RandomGuidMock;

            var input = ClienteMocks.GetChangeCpfClienteInputMock;
            input.Cpf = "178.160.930-63";

            var changeCpfClienteMock = ClienteMocks.GetClienteMock;

            _clienteRepositoryMock
                .Setup(x => x.FindByUid(guidMock))
                .ReturnsAsync(changeCpfClienteMock);

            _clienteRepositoryMock
                .Setup(x => x.FindByCpf(input.Cpf))
                .ReturnsAsync(null as Cliente);

            _clienteRepositoryMock
                .Setup(x => x.Edit(changeCpfClienteMock))
                .ReturnsAsync(true);

            await _changeCpfClienteUseCase.Execute(guidMock.ToString(), input);

            _clienteRepositoryMock.Verify(m => m.FindByUid(guidMock), Times.Once);
            _clienteRepositoryMock.Verify(m => m.FindByCpf(input.Cpf), Times.Once);
            _clienteRepositoryMock.Verify(m => m.Edit(changeCpfClienteMock), Times.Once);
            _clienteRepositoryMock.VerifyNoOtherCalls();
        }
    }
}
