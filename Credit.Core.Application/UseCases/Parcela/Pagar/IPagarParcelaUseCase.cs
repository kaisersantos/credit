namespace Credit.Core.Application.UseCases.Parcelas.Pagar
{
    public interface IPagarParcelaUseCase
    {
        Task Execute(PagarParcelaInput input);
    }
}
