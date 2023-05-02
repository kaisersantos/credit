using AutoMapper;
using Credit.Core.Domain.Entities;

namespace Credit.Core.Application.UseCases.Financiamentos.Create.ProfileResolvers
{
    internal class CreateFinanciamentoParcelaResolver : IValueResolver<CreateFinanciamentoInput, Financiamento, ICollection<Parcela>?>
    {
        public ICollection<Parcela>? Resolve(CreateFinanciamentoInput source, Financiamento destination, ICollection<Parcela>? destMember, ResolutionContext context)
        {
            var parcelas = new List<Parcela>();

            for (int i = 0; i < source.QuantidadeParcelas; i++)
            {
                parcelas.Add(new Parcela()
                {
                    NumeroParcela = (short)i,
                    DataVencimento = source.DataPrimeiroVencimento.AddMonths(i),
                    ValorParcela = destination.ValorJuros / destination.QuantidadeParcelas,
                });
            }

            return parcelas;
        }
    }
}
