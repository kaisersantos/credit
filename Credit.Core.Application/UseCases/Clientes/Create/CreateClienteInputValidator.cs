using Credit.Core.Domain.ValueObjects;
using FluentValidation;

namespace Credit.Core.Application.UseCases.Clientes.Create
{
    public class CreateClienteInputValidator : AbstractValidator<CreateClienteInput>
    {
        public CreateClienteInputValidator()
        {
            RuleFor(x => x.Cpf)
                .NotEmpty()
                .Must(value => Cpf.Validate(value))
                .WithMessage(ClienteError.CpfInvalid.Messages.First());

            RuleFor(x => x.Nome)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Uf)
                .NotEmpty()
                .Length(2);

            RuleFor(x => x.Celular)
                .NotEmpty()
                .Length(15);
        }
    }
}
