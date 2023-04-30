using FluentValidation;

namespace Credit.Core.Application.UseCases.Clientes.Create
{
    public class CreateClienteInputValidator : AbstractValidator<CreateClienteInput>
    {
        public CreateClienteInputValidator()
        {
            RuleFor(x => x.Cpf)
                .NotEmpty()
                .MaximumLength(14);

            RuleFor(x => x.Nome)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Uf)
                .NotEmpty()
                .MaximumLength(2);

            RuleFor(x => x.Celular)
                .NotEmpty()
                .MaximumLength(15);
        }
    }
}
