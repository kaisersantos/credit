using Credit.Core.Application.Adapters;
using Credit.Core.Domain.Entities;
using Credit.Core.Domain.ValueObjects;
using Credit.Infra.Adapter.Dapper.Config;

namespace Credit.Infra.Adapter.Dapper
{
    internal class ClienteRepository : IClienteRepository
    {
        private readonly CreditDbContext _context;

        private readonly string SELECT = @"
            SELECT
                ID,
                UID,
                CPF,
                NOME,
                UF,
                CELULAR
            FROM CLIENTE";

        public ClienteRepository(CreditDbContext context) =>
            _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<Cliente> Create(Cliente cliente)
        {
            var insert = @"
                INSERT INTO CLIENTE (
                    UID, 
                    CPF, 
                    NOME, 
                    UF, 
                    CELULAR
                ) VALUES (
                    @Uid,
                    @Cpf,
                    @Nome,
                    @Uf,
                    @Celular
                ) OUTPUT INSERTED.ID";

            var createdId = await _context.QuerySingleAsync<int>(insert, new {
                cliente.Uid,
                Cpf = cliente.Cpf.ToMaskedString(),
                cliente.Nome,
                cliente.Uf,
                cliente.Celular
            });

            cliente.Id = createdId;

            return cliente;
        }

        public async Task<bool> Edit(Cliente cliente)
        {
            var update = @"
                UPDATE CLIENTE SET 
                    CPF = @Cpf, 
                    NOME = @Nome, 
                    UF = @Uf, 
                    CELULAR = @Celular
                WHERE ID = @Id";

            var affectedRows = await _context.ExecuteAsync(update, new
            {
                cliente.Id,
                Cpf = cliente.Cpf.ToMaskedString(),
                cliente.Nome,
                cliente.Uf,
                cliente.Celular
            });

            return affectedRows > 0;
        }

        public async Task<bool> Remove(Cliente cliente)
        {
            var update = @"
                DELETE CLIENTE
                WHERE ID = @Id";

            var affectedRows = await _context.ExecuteAsync(update, new
            {
                cliente.Id
            });

            return affectedRows > 0;
        }

        public async Task<Cliente?> FindByUid(Guid uid)
        {
            var select = $@"
                {SELECT}
                WHERE UID = @Uid";

            return await _context.QueryFirstOrDefault<Cliente>(select, new
            {
                Uid = uid
            });
        }

        public async Task<Cliente?> FindByCpf(Cpf cpf)
        {
            var select = $@"
                {SELECT}
                WHERE CPF = @Cpf";

            return await _context.QueryFirstOrDefault<Cliente>(select, new
            {
                Cpf = cpf.ToMaskedString()
            });
        }
    }
}
