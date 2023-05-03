namespace Credit.Core.Application.UseCases.Financiamentos.Create
{
    public interface ICreateFinanciamentoUseCase
    {
        Task<CreateFinanciamentoOutput> Execute(CreateFinanciamentoInput input);
    }
}
