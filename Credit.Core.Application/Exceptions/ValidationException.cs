using FluentValidation.Results;
using System.Runtime.Serialization;

namespace Credit.Core.Application.Exceptions
{
    [Serializable]
    public abstract class ValidationException : DomainException
    {
        public override string Key => nameof(ValidationException);

        public ValidationException(params ValidationFailure[] validation)
        {
            var errors = validation.Select(v => new Error(v.PropertyName, v.ErrorMessage)).ToArray();
            AddErrors(errors);
        }

        protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
