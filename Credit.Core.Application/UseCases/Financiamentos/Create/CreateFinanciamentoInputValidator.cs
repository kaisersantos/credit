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
                .NotEmpty()
                .Matches("^(D|C|J|F|I)$", RegexOptions.IgnoreCase)
                .WithMessage(CreditoError.InvalidValue.Messages.First());

            RuleFor(x => x.ValorCredito)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.QuantidadeParcelas)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.DataPrimeiroVencimento)
                .NotEmpty();
        }
    }
}
