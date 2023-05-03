using AutoMapper;
using Credit.Core.Application.UseCases.Financiamentos.Create.ProfileResolvers;
using Credit.Core.Domain.Entities;

namespace Credit.Core.Application.UseCases.Financiamentos.Create
{
    public class CreateFinanciamentoProfile : Profile
    {
        public CreateFinanciamentoProfile()
        {
            CreateMap<CreateFinanciamentoInput, Financiamento>()
                .BeforeMap((input, _) => input.TipoCredito = input.TipoCredito?.Trim())
                .ForMember(dest => dest.ValorTotal, opt => opt.MapFrom(source => source.ValorCredito))
                .ForMember(dest => dest.Credito, opt => opt.MapFrom(source => source.TipoCredito != null ? Credito.GetTipoCredito(source.TipoCredito) : null));

            CreateMap<Financiamento, CreateFinanciamentoOutput>()
                .ForMember(dest => dest.StatusCredito, opt => opt.Ignore())
                .ForMember(dest => dest.Parcelas, opt => opt.MapFrom<CreateFinanciamentoOutputParcelaResolver>());
        }
    }
}
