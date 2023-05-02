using Credit.Core.Application.Exceptions;
using FluentValidation.Results;
using System.Runtime.Serialization;

namespace Credit.Core.Application.UseCases.Financiamentos
{
    public class FinanciamentoCoreApplicationException : CoreApplicationException
    {
        public override string Key => nameof(FinanciamentoCoreApplicationException);

        public FinanciamentoCoreApplicationException(params FinanciamentoError[] errors) => AddErrors(errors);

        public FinanciamentoCoreApplicationException(ICollection<ValidationFailure> errors) => AddErrors(errors);

        protected FinanciamentoCoreApplicationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
