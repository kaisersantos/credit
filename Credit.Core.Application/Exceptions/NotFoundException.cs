using System.Runtime.Serialization;

namespace Credit.Core.Application.Exceptions
{
    [Serializable]
    public abstract class NotFoundException : DomainException
    {
        public override string Key => nameof(NotFoundException);

        protected NotFoundException() : base() { }

        protected NotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
