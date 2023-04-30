using FluentValidation.Results;
using System.Runtime.Serialization;

namespace Credit.Core.Application.Exceptions
{
    [Serializable]
    public abstract class DomainException : Exception
    {
        public ICollection<Error> Errors { get; set; } = new List<Error>();

        public abstract string Key { get; }

        protected DomainException() : base("A domain error has occurred, please verify the 'errors' property to obtain more details") { }

        protected DomainException(string message) : base(message) { }

        protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public DomainException AddErrors(params Error[] errors)
        {
            Errors = new List<Error>(errors);

            return this;
        }

        public DomainException AddErrors(ICollection<ValidationFailure> validations)
        {
            var errors = validations
                .GroupBy(item => item.PropertyName,
                    (key, group) => new Error(key, group.Select(item => item.ErrorMessage).ToList()));

            Errors = new List<Error>(errors);

            return this;
        }
    }
}
