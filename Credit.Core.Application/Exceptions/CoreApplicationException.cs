using Credit.Core.Domain.Exceptions;
using FluentValidation.Results;
using System.Runtime.Serialization;

namespace Credit.Core.Application.Exceptions
{
    public class CoreApplicationException : CoreException
    {
        public override string Key => nameof(CoreApplicationException);

        public CoreApplicationException(params CoreError[] errors) => AddErrors(errors);

        public CoreApplicationException AddErrors(ICollection<ValidationFailure> validations)
        {
            var errors = validations
                .GroupBy(item => item.PropertyName,
                    (key, group) => new CoreError(key, group.Select(item => item.ErrorMessage).ToList()));

            Errors = new List<CoreError>(errors);

            return this;
        }

        protected CoreApplicationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}