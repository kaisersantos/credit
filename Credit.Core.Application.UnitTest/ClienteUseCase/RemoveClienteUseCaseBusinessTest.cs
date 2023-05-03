using Credit.Core.Application.Adapters;
using Credit.Core.Application.UseCases.Clientes;
using Credit.Core.Application.UseCases.Clientes.Remove;
using Credit.Core.Domain.Entities;

namespace Credit.Core.Application.UnitTest.ClienteUseCase
{
    public class RemoveClienteUseCaseTest
    {
        private readonly IRemoveClienteUseCase _removeClienteUseCase;
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;

        public RemoveClienteUseCaseTest()
        {
            _clienteRepositoryMock = new Mock<IClienteRepository>();

            _removeClienteUseCase = new RemoveClienteUseCase(_clienteRepositoryMock.Object);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData("String inválido")]
        public async Task ShouldOccurUidInvalidoError(string uid)
        {
            var ex = await Assert.ThrowsAsync<ClienteCoreApplicationException>(
                () => _removeClienteUseCase.Execute(uid));

            Assert.Equal(ClienteError.UidInvalido(uid).Key, ex.Errors.First().Key);

            _clienteRepositoryMock.Verify(m => m.FindByUid(It.IsAny<Guid>()), Times.Never);
            _clienteRepositoryMock.Verify(m => m.Remove(It.IsAny<Cliente>()), Times.Never);
            _clienteRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldOccurClienteNotFoundByUidError()
        {
            var guidMock = ClienteMocks.RandomGuidMock;

            _clienteRepositoryMock
                .Setup(x => x.FindByUid(guidMock))
                .ReturnsAsync(null as Cliente);

            var ex = await Assert.ThrowsAsync<ClienteNotFoundException>(
                () => _removeClienteUseCase.Execute(guidMock.ToString()));

            Assert.Equal(ClienteError.ClienteNotFoundByUid(guidMock).Key, ex.Errors.First().Key);

            _clienteRepositoryMock.Verify(m => m.FindByUid(guidMock), Times.Once);
            _clienteRepositoryMock.Verify(m => m.Remove(It.IsAny<Cliente>()), Times.Never);
            _clienteRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemoveClienteError()
        {
            var guidMock = ClienteMocks.RandomGuidMock;
            var removeClienteMock = ClienteMocks.GetClienteMock;

            _clienteRepositoryMock
                .Setup(x => x.FindByUid(guidMock))
                .ReturnsAsync(removeClienteMock);

            _clienteRepositoryMock
                .Setup(x => x.Remove(removeClienteMock))
                .ReturnsAsync(false);

            var ex = await Assert.ThrowsAsync<ClienteCoreApplicationException>(
                () => _removeClienteUseCase.Execute(guidMock.ToString()));

            Assert.Equal(ClienteError.UnableToRemoveCliente(guidMock).Key, ex.Errors.First().Key);

            _clienteRepositoryMock.Verify(m => m.FindByUid(guidMock), Times.Once);
            _clienteRepositoryMock.Verify(m => m.Remove(removeClienteMock), Times.Once);
            _clienteRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldRemoveCliente()
        {
            var guidMock = ClienteMocks.RandomGuidMock;
            var removeClienteMock = ClienteMocks.GetClienteMock;

            _clienteRepositoryMock
                .Setup(x => x.FindByUid(guidMock))
                .ReturnsAsync(removeClienteMock);

            _clienteRepositoryMock
                .Setup(x => x.Remove(removeClienteMock))
                .ReturnsAsync(true);

            await _removeClienteUseCase.Execute(guidMock.ToString());

            _clienteRepositoryMock.Verify(m => m.FindByUid(guidMock), Times.Once);
            _clienteRepositoryMock.Verify(m => m.Remove(removeClienteMock), Times.Once);
            _clienteRepositoryMock.VerifyNoOtherCalls();
        }
    }
}
