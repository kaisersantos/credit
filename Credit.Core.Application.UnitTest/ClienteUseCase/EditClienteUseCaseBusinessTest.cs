using Credit.Core.Application.Adapters;
using Credit.Core.Application.UnitTest.FinanciamentoUseCase;
using Credit.Core.Application.UseCases.Clientes;
using Credit.Core.Application.UseCases.Clientes.Edit;
using Credit.Core.Application.UseCases.Financiamentos;
using Credit.Core.Domain.Entities;
using Credit.Core.Domain.ValueObjects;

namespace Credit.Core.Application.UnitTest.ClienteUseCase
{
    public class EditClienteUseCaseTest
    {
        private readonly IEditClienteUseCase _editClienteUseCase;
        private readonly IMapper _mapper;
        private readonly IValidator<EditClienteInput> _editClienteValidator;
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;

        public EditClienteUseCaseTest()
        {
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<EditClienteProfile>())
                .CreateMapper();
            _editClienteValidator = new EditClienteInputValidator();
            _clienteRepositoryMock = new Mock<IClienteRepository>();

            _editClienteUseCase = new EditClienteUseCase(
                _mapper,
                _editClienteValidator,
                _clienteRepositoryMock.Object);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData("String inválido")]
        public async Task ShouldOccurUidInvalidoError(string uid)
        {
            var input = ClienteMocks.GetEditClienteInputMock;

            var ex = await Assert.ThrowsAsync<ClienteCoreApplicationException>(
                () => _editClienteUseCase.Execute(uid, input));

            Assert.Equal(ClienteError.UidInvalido(uid).Key, ex.Errors.First().Key);

            _clienteRepositoryMock.Verify(m => m.FindByUid(It.IsAny<Guid>()), Times.Never);
            _clienteRepositoryMock.Verify(m => m.Edit(It.IsAny<Cliente>()), Times.Never);
            _clienteRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldOccurClienteNotFoundByUidError()
        {
            var guidMock = ClienteMocks.RandomGuidMock;
            var input = ClienteMocks.GetEditClienteInputMock;

            _clienteRepositoryMock
                .Setup(x => x.FindByUid(guidMock))
                .ReturnsAsync(null as Cliente);

            var ex = await Assert.ThrowsAsync<ClienteNotFoundException>(
                () => _editClienteUseCase.Execute(guidMock.ToString(), input));

            Assert.Equal(ClienteError.ClienteNotFoundByUid(guidMock).Key, ex.Errors.First().Key);

            _clienteRepositoryMock.Verify(m => m.FindByUid(guidMock), Times.Once);
            _clienteRepositoryMock.Verify(m => m.Edit(It.IsAny<Cliente>()), Times.Never);
            _clienteRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldEditClienteError()
        {
            var guidMock = ClienteMocks.RandomGuidMock;
            var input = ClienteMocks.GetEditClienteInputMock;
            var editClienteMock = ClienteMocks.GetClienteMock;

            _clienteRepositoryMock
                .Setup(x => x.FindByUid(guidMock))
                .ReturnsAsync(editClienteMock);

            _clienteRepositoryMock
                .Setup(x => x.Edit(editClienteMock))
                .ReturnsAsync(false);

            var ex = await Assert.ThrowsAsync<ClienteCoreApplicationException>(
                () => _editClienteUseCase.Execute(guidMock.ToString(), input));

            Assert.Equal(ClienteError.UnableToEditCliente(guidMock).Key, ex.Errors.First().Key);

            _clienteRepositoryMock.Verify(m => m.FindByUid(guidMock), Times.Once);
            _clienteRepositoryMock.Verify(m => m.Edit(editClienteMock), Times.Once);
            _clienteRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldEditCliente()
        {
            var guidMock = ClienteMocks.RandomGuidMock;
            var input = ClienteMocks.GetEditClienteInputMock;
            var editClienteMock = ClienteMocks.GetClienteMock;

            _clienteRepositoryMock
                .Setup(x => x.FindByUid(guidMock))
                .ReturnsAsync(editClienteMock);

            _clienteRepositoryMock
                .Setup(x => x.Edit(editClienteMock))
                .ReturnsAsync(true);

            await _editClienteUseCase.Execute(guidMock.ToString(), input);

            _clienteRepositoryMock.Verify(m => m.FindByUid(guidMock), Times.Once);
            _clienteRepositoryMock.Verify(m => m.Edit(editClienteMock), Times.Once);
            _clienteRepositoryMock.VerifyNoOtherCalls();
        }
    }
}
