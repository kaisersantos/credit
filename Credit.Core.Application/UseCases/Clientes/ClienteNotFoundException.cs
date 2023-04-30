using Credit.Core.Application.Exceptions;
using System.Runtime.Serialization;

namespace Credit.Core.Application.UseCases.Clientes
{
    [Serializable]
    public class ClienteNotFoundException : NotFoundException
    {
        public override string Key => nameof(ClienteNotFoundException);

        public ClienteNotFoundException(params ClienteError[] errors) => AddErrors(errors);

        protected ClienteNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}