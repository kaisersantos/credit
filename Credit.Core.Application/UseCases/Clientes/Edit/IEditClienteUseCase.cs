namespace Credit.Core.Application.UseCases.Clientes.Edit
{
    public interface IEditClienteUseCase
    {
        Task Execute(string clienteUid, EditClienteInput input);
    }
}
