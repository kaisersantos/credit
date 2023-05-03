using Credit.Core.Application.Adapters;
using Credit.Core.Application.UseCases.Clientes;
using Credit.Core.Application.UseCases.Clientes.ChangeCpf;
using Credit.Core.Application.UseCases.Clientes.Create;
using Credit.Core.Domain.Entities;
using Credit.Core.Domain.ValueObjects;

namespace Credit.Core.Application.UnitTest.ClienteUseCase
{
    public class ChangeCpfClienteUseCaseValidationTest
    {
        private readonly IChangeCpfClienteUseCase _changeCpfClienteUseCase;
        private readonly IValidator<ChangeCpfClienteInput> _changeCpfClienteValidator;
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;

        public ChangeCpfClienteUseCaseValidationTest()
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
        [InlineData("123456789100")]
        public async Task ShouldOccurCpfValidationError(string valor)
        {
            var guidMock = ClienteMocks.RandomGuidMock;

            var input = ClienteMocks.GetChangeCpfClienteInputMock;
            input.Cpf = valor;

            var ex = await Assert.ThrowsAsync<ClienteCoreApplicationException>(
                () => _changeCpfClienteUseCase.Execute(guidMock.ToString(), input));

            Assert.Equal(nameof(ChangeCpfClienteInput.Cpf), ex.Errors.First().Key);

            _clienteRepositoryMock.Verify(m => m.FindByUid(It.IsAny<Guid>()), Times.Never);
            _clienteRepositoryMock.Verify(m => m.FindByCpf(It.IsAny<Cpf>()), Times.Never);
            _clienteRepositoryMock.Verify(m => m.Edit(It.IsAny<Cliente>()), Times.Never);
            _clienteRepositoryMock.VerifyNoOtherCalls();
        }
    }
}
