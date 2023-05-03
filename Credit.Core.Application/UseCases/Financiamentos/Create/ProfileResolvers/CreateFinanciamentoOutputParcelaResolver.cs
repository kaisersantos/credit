using AutoMapper;
using Credit.Core.Domain.Entities;

namespace Credit.Core.Application.UseCases.Financiamentos.Create.ProfileResolvers
{
    internal class CreateFinanciamentoOutputParcelaResolver : IValueResolver<Financiamento, CreateFinanciamentoOutput, IEnumerable<CreateFinanciamentoParcelaOutput>>
    {
        public IEnumerable<CreateFinanciamentoParcelaOutput> Resolve(Financiamento source, CreateFinanciamentoOutput destination, IEnumerable<CreateFinanciamentoParcelaOutput> destMember, ResolutionContext context)
        {
            var parcelas = new List<CreateFinanciamentoParcelaOutput>();

            source.Parcelas?.ToList().ForEach(parcela =>
            {
                parcelas.Add(new CreateFinanciamentoParcelaOutput()
                {
                    Uid = parcela.Uid,
                    NumeroParcela = parcela.NumeroParcela,
                    ValorParcela = parcela.ValorParcela,
                    DataVencimento = parcela.DataVencimento
                });
            });

            return parcelas;
        }
    }
}
