using AutoMapper;
using Credit.Core.Domain.Entities;

namespace Credit.Core.Application.UseCases.Clientes.Edit
{
    public class EditClienteProfile : Profile
    {
        public EditClienteProfile()
        {
            CreateMap<EditClienteInput, Cliente>()
                .BeforeMap((input, _) =>
                {
                    input.Nome = input.Nome.Trim();
                    input.Uf = input.Uf.Trim();
                    input.Celular = input.Celular.Trim();
                });
        }
    }
}
