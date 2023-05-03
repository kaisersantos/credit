using FluentValidation;

namespace Credit.Core.Application.UseCases.Clientes.Edit
{
    public class EditClienteInputValidator : AbstractValidator<EditClienteInput>
    {
        public EditClienteInputValidator()
        {
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
