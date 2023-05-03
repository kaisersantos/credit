namespace Credit.Core.Application.UseCases.Clientes.ChangeCpf
{
    public interface IChangeCpfClienteUseCase
    {
        Task Execute(string clienteUid, ChangeCpfClienteInput input);
    }
}
