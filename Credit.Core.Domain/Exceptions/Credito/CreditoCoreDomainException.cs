using System.Runtime.Serialization;

namespace Credit.Core.Domain.Exceptions.Credito
{
    public class CreditoCoreDomainException : CoreDomainException
    {
        public override string Key => nameof(CreditoCoreDomainException);

        public CreditoCoreDomainException(params CreditoError[] errors) => AddErrors(errors);

        protected CreditoCoreDomainException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}