using Credit.Core.Application.Exceptions;
using FluentValidation.Results;
using System.Runtime.Serialization;

namespace Credit.Core.Application.UseCases.Clientes
{
    public class ClienteCoreApplicationException : CoreApplicationException
    {
        public override string Key => nameof(ClienteCoreApplicationException);

        public ClienteCoreApplicationException(params ClienteError[] errors) => AddErrors(errors);

        public ClienteCoreApplicationException(ICollection<ValidationFailure> errors) => AddErrors(errors);

        protected ClienteCoreApplicationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}