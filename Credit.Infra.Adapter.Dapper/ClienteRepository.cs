using Credit.Core.Application.Adapters;
using Credit.Core.Domain.Entities;
using Credit.Core.Domain.ValueObjects;
using Credit.Infra.Adapter.Dapper.Config;
using System.Data;

namespace Credit.Infra.Adapter.Dapper
{
    internal class ClienteRepository : IClienteRepository
    {
        private readonly CreditDbContext _context;

        private readonly string SELECT = @"
            SELECT
                CLI.ID,
                CLI.UID,
                CLI.CPF,
                CLI.NOME,
                CLI.UF,
                CLI.CELULAR
            FROM CLIENTE CLI";

        public ClienteRepository(CreditDbContext context) =>
            _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<Cliente> Create(Cliente cliente)
        {
            var insert = @"SP_CLIENTE_INSERT";

            var createdId = await _context.QuerySingleAsync<int>(insert, new
            {
                cliente.Uid,
                Cpf = cliente.Cpf.ToMaskedString(),
                cliente.Nome,
                cliente.Uf,
                cliente.Celular
            }, CommandType.StoredProcedure);

            cliente.Id = createdId;

            return cliente;
        }

        public async Task<bool> Edit(Cliente cliente)
        {
            var update = @"SP_CLIENTE_UPDATE";

            var affectedRows = await _context.ExecuteAsync(update, new
            {
                cliente.Id,
                Cpf = cliente.Cpf.ToMaskedString(),
                cliente.Nome,
                cliente.Uf,
                cliente.Celular
            }, CommandType.StoredProcedure);

            return affectedRows > 0;
        }

        public async Task<bool> Remove(Cliente cliente)
        {
            var remove = @"SP_CLIENTE_DELETE";

            var affectedRows = await _context.ExecuteAsync(remove, new
            {
                cliente.Id
            }, CommandType.StoredProcedure);

            return affectedRows > 0;
        }

        public async Task<Cliente?> FindByUid(Guid uid)
        {
            var select = $@"
                {SELECT}
                WHERE CLI.UID = @Uid";

            return await _context.QueryFirstOrDefault<Cliente>(select, new
            {
                Uid = uid
            });
        }

        public async Task<Cliente?> FindByCpf(Cpf cpf)
        {
            var select = $@"
                {SELECT}
                WHERE CLI.CPF = @Cpf";

            return await _context.QueryFirstOrDefault<Cliente>(select, new
            {
                Cpf = cpf.ToMaskedString()
            });
        }
    }
}
