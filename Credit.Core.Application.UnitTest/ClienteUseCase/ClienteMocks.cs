using Credit.Core.Application.UseCases.Clientes.ChangeCpf;
using Credit.Core.Application.UseCases.Clientes.Create;
using Credit.Core.Application.UseCases.Clientes.Edit;
using Credit.Core.Domain.Entities;

namespace Credit.Core.Application.UnitTest.ClienteUseCase
{
    internal static class ClienteMocks
    {
        public readonly static Guid RandomGuidMock = Guid.NewGuid();

        public static CreateClienteInput GetCreateClienteInputMock => new()
        {
            Cpf = "065.590.440-92",
            Nome = "José Pereira",
            Uf = "MG",
            Celular = "(99) 99999-9999"
        };

        public static EditClienteInput GetEditClienteInputMock => new()
        {
            Nome = "José Pereira",
            Uf = "MG",
            Celular = "(99) 99999-9999"
        };

        public static ChangeCpfClienteInput GetChangeCpfClienteInputMock => new()
        {
            Cpf = "065.590.440-92",
        };

        public static Cliente GetClienteMock => new()
        {
            Id = new Random().NextInt64(),
            Uid = RandomGuidMock,
            Cpf = "065.590.440-92",
            Nome = "José Pereira",
            Uf = "MG",
            Celular = "(99) 99999-9999"
        };

        public static CreateClienteOutput GetCreateClienteOutputMock => new()
        {
            Uid = RandomGuidMock,
            Cpf = "065.590.440-92",
            Nome = "José Pereira",
            Uf = "MG",
            Celular = "(99) 99999-9999"
        };
    }
}
