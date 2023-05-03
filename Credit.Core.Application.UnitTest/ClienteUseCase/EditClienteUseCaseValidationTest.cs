using Credit.Core.Application.Adapters;
using Credit.Core.Application.UseCases.Clientes;
using Credit.Core.Application.UseCases.Clientes.Edit;
using Credit.Core.Domain.Entities;

namespace Credit.Core.Application.UnitTest.ClienteUseCase
{
    public class EditClienteUseCaseValidationTest
    {
        private readonly IEditClienteUseCase _editClienteUseCase;
        private readonly IMapper _mapper;
        private readonly IValidator<EditClienteInput> _editClienteValidator;
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;

        public EditClienteUseCaseValidationTest()
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
        public async Task ShouldOccurNomeValidationError(string valor)
        {
            var guidMock = ClienteMocks.RandomGuidMock;

            var input = ClienteMocks.GetEditClienteInputMock;
            input.Nome = valor;

            var ex = await Assert.ThrowsAsync<ClienteCoreApplicationException>(
                () => _editClienteUseCase.Execute(guidMock.ToString(), input));

            Assert.Equal(nameof(EditClienteInput.Nome), ex.Errors.First().Key);

            _clienteRepositoryMock.Verify(m => m.FindByUid(It.IsAny<Guid>()), Times.Never);
            _clienteRepositoryMock.Verify(m => m.Edit(It.IsAny<Cliente>()), Times.Never);
            _clienteRepositoryMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldOccurNomeValidationErrorWithMoreThan100Characters()
        {
            var guidMock = ClienteMocks.RandomGuidMock;

            var input = ClienteMocks.GetEditClienteInputMock;
            input.Nome = new string('K', 101);

            var ex = await Assert.ThrowsAsync<ClienteCoreApplicationException>(
                () => _editClienteUseCase.Execute(guidMock.ToString(), input));

            Assert.Equal(nameof(EditClienteInput.Nome), ex.Errors.First().Key);

            _clienteRepositoryMock.Verify(m => m.FindByUid(It.IsAny<Guid>()), Times.Never);
            _clienteRepositoryMock.Verify(m => m.Edit(It.IsAny<Cliente>()), Times.Never);
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
            var guidMock = ClienteMocks.RandomGuidMock;

            var input = ClienteMocks.GetEditClienteInputMock;
            input.Uf = valor;

            var ex = await Assert.ThrowsAsync<ClienteCoreApplicationException>(
                () => _editClienteUseCase.Execute(guidMock.ToString(), input));

            Assert.Equal(nameof(EditClienteInput.Uf), ex.Errors.First().Key);

            _clienteRepositoryMock.Verify(m => m.FindByUid(It.IsAny<Guid>()), Times.Never);
            _clienteRepositoryMock.Verify(m => m.Edit(It.IsAny<Cliente>()), Times.Never);
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
            var guidMock = ClienteMocks.RandomGuidMock;

            var input = ClienteMocks.GetEditClienteInputMock;
            input.Celular = valor;

            var ex = await Assert.ThrowsAsync<ClienteCoreApplicationException>(
                () => _editClienteUseCase.Execute(guidMock.ToString(), input));

            Assert.Equal(nameof(EditClienteInput.Celular), ex.Errors.First().Key);

            _clienteRepositoryMock.Verify(m => m.FindByUid(It.IsAny<Guid>()), Times.Never);
            _clienteRepositoryMock.Verify(m => m.Edit(It.IsAny<Cliente>()), Times.Never);
            _clienteRepositoryMock.VerifyNoOtherCalls();
        }
    }
}
