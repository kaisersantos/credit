namespace Credit.Core.Application.UseCases.Clientes.Create
{
    public interface ICreateClienteUseCase
    {
        Task<CreateClienteOutput> Execute(CreateClienteInput input);
    }
}
