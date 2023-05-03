using Credit.Core.Domain.ValueObjects;
using FluentValidation;

namespace Credit.Core.Application.UseCases.Clientes.ChangeCpf
{
    public class ChangeCpfClienteInputValidator : AbstractValidator<ChangeCpfClienteInput>
    {
        public ChangeCpfClienteInputValidator()
        {
            RuleFor(x => x.Cpf)
                .NotEmpty()
                .Must(value => Cpf.Validate(value))
                .WithMessage(ClienteError.CpfInvalid.Messages.First());
        }
    }
}
