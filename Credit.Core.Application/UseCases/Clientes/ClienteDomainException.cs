using Credit.Core.Application.Exceptions;
using FluentValidation.Results;
using System.Runtime.Serialization;

namespace Credit.Core.Application.UseCases.Clientes
{
    public class ClienteDomainException : DomainException
    {
        public override string Key => nameof(ClienteDomainException);

        public ClienteDomainException(params ClienteError[] errors) => AddErrors(errors);

        public ClienteDomainException(ICollection<ValidationFailure> errors) => AddErrors(errors);

        protected ClienteDomainException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
