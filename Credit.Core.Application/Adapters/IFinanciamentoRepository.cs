using Credit.Core.Domain.Entities;

namespace Credit.Core.Application.Adapters
{
    public interface IFinanciamentoRepository
    {
        Task<Financiamento> Create(Financiamento Financiamento);
    }
}
