using System.Runtime.Serialization;

namespace Credit.Core.Domain.Exceptions
{
    public class CoreDomainException : CoreException
    {
        public override string Key => nameof(CoreDomainException);

        public CoreDomainException(params CoreError[] errors) => AddErrors(errors);

        protected CoreDomainException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
