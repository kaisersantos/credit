using Credit.Core.Domain.Exceptions;
using FluentValidation.Results;
using System.Runtime.Serialization;

namespace Credit.Core.Application.Exceptions
{
    [Serializable]
    public abstract class ValidationException : CoreApplicationException
    {
        public override string Key => nameof(ValidationException);

        public ValidationException(params ValidationFailure[] validation)
        {
            var errors = validation.Select(v => new CoreError(v.PropertyName, v.ErrorMessage)).ToArray();
            AddErrors(errors);
        }

        protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
