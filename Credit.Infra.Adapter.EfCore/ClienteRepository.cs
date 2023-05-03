using Credit.Core.Application.Adapters;
using Credit.Core.Domain.Entities;
using Credit.Core.Domain.ValueObjects;
using Credit.Infra.Adapter.EfCore.Config;
using Microsoft.EntityFrameworkCore;

namespace Credit.Infra.Adapter.EfCore
{
    internal class ClienteRepository : IClienteRepository
    {
        private readonly CreditDbContext _context;

        public ClienteRepository(CreditDbContext context) =>
            _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<Cliente> Create(Cliente cliente)
        {
            await _context.AddAsync(cliente);
            await _context.SaveChangesAsync();

            return cliente;
        }

        public async Task<bool> Edit(Cliente cliente)
        {
            _context.Update(cliente);
            var affectedRows = await _context.SaveChangesAsync();

            return affectedRows > 0;
        }

        public async Task<bool> Remove(Cliente cliente)
        {
            _context.Remove(cliente);
            var affectedRows = await _context.SaveChangesAsync();

            return affectedRows > 0;
        }

        public async Task<Cliente?> FindByUid(Guid uid)
        {
            return await _context.Clientes
                .Where(cliente => cliente.Uid == uid)
                .FirstOrDefaultAsync();
        }

        public async Task<Cliente?> FindByCpf(Cpf cpf)
        {
            return await _context.Clientes
                .Where(cliente => cliente.Cpf == cpf)
                .FirstOrDefaultAsync();
        }
    }
}
