using AutoMapper;
using Credit.Core.Domain.Entities;

namespace Credit.Core.Application.UseCases.Clientes.Create
{
    public class CreateClienteProfile : Profile
    {
        public CreateClienteProfile()
        {
            CreateMap<CreateClienteInput, Cliente>()
                .BeforeMap((input, _) =>
                {
                    input.Cpf = input.Cpf.Trim();
                    input.Nome = input.Nome.Trim();
                    input.Uf = input.Uf.Trim();
                    input.Celular = input.Celular.Trim();
                });

            CreateMap<Cliente, CreateClienteOutput>();
        }
    }
}
