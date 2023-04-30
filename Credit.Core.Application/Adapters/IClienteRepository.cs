using Credit.Core.Domain.Entities;
using Credit.Core.Domain.ValueObjects;

namespace Credit.Core.Application.Adapters
{
    public interface IClienteRepository
    {
        Task<Cliente> Create(Cliente cliente);

        Task<bool> Edit(Cliente cliente);

        Task<bool> Remove(Cliente cliente);

        Task<Cliente?> FindByUid(Guid uid);

        Task<Cliente?> FindByCpf(Cpf cpf);
    }
}
