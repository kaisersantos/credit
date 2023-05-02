using AutoMapper;
using Credit.Core.Application.UseCases.Financiamentos.Create.ProfileResolvers;
using Credit.Core.Domain.Entities;

namespace Credit.Core.Application.UseCases.Financiamentos.Create
{
    internal class CreateFinanciamentoProfile : Profile
    {
        public CreateFinanciamentoProfile()
        {
            CreateMap<CreateFinanciamentoInput, Financiamento>()
                .BeforeMap((input, _) => input.TipoCredito = input.TipoCredito.Trim())
                .ForMember(dest => dest.Credito, opt => opt.MapFrom(source => Credito.GetTipoCredito(source.TipoCredito)))
                .ForMember(dest => dest.Parcelas, opt => opt.MapFrom<CreateFinanciamentoParcelaResolver>());

            CreateMap<Financiamento, CreateFinanciamentoOutput>();
        }
    }
}
