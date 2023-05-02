using Credit.Core.Domain.Exceptions.Credito;
using Credit.Core.Domain.Extensions;
using Credit.Core.Domain.ValueObjects;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Credit.Core.Application.UseCases.Financiamentos.Create
{
    public class CreateFinanciamentoInputValidator : AbstractValidator<CreateFinanciamentoInput>
    {
        public CreateFinanciamentoInputValidator()
        {
            RuleFor(x => x.TipoCredito)
                .Matches("^(D|C|J|F|I)$", RegexOptions.IgnoreCase)
                .WithMessage(CreditoError.InvalidValue.Messages.First());

            RuleFor(x => x.ValorCredito)
                .GreaterThanOrEqualTo(15000)
                .When(x => x.TipoCredito.ToEnum<TipoCredito>() == TipoCredito.PessoaJuridica);

            var oneMillion = 1000000;
            RuleFor(x => x.ValorCredito)
                .LessThanOrEqualTo(oneMillion);

            RuleFor(x => x.QuantidadeParcelas)
                .GreaterThanOrEqualTo(5)
                .LessThanOrEqualTo(72);

            RuleFor(x => x.DataPrimeiroVencimento)
                .GreaterThanOrEqualTo(DateTime.Now.AddDays(15))
                .LessThanOrEqualTo(DateTime.Now.AddDays(40));
        }
    }
}
