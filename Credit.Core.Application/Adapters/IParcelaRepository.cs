using Credit.Core.Domain.Entities;

namespace Credit.Core.Application.Adapters
{
    public interface IParcelaRepository
    {
        Task<Parcela> Create(Parcela parcela);

        Task<bool> Pagar(Parcela parcela);

        Task<Parcela?> FindByUid(Guid uid);
    }
}
