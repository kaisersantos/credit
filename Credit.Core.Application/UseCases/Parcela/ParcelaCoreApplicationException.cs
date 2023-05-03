using Credit.Core.Application.Exceptions;
using Credit.Core.Application.UseCases.Parcelas;
using FluentValidation.Results;
using System.Runtime.Serialization;

namespace Credit.Core.Application.UseCases.Financiamentos
{
    [Serializable]
    public class ParcelaCoreApplicationException : CoreApplicationException
    {
        public override string Key => nameof(ParcelaCoreApplicationException);

        public ParcelaCoreApplicationException(params ParcelaError[] errors) => AddErrors(errors);

        public ParcelaCoreApplicationException(ICollection<ValidationFailure> errors) => AddErrors(errors);

        protected ParcelaCoreApplicationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
