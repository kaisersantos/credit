namespace Credit.Core.Application.UseCases.Clientes.Remove
{
    public interface IRemoveClienteUseCase
    {
        Task Execute(string clienteUid);
    }
}
