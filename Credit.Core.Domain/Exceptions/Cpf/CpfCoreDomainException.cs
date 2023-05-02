using Credit.Core.Domain.Exceptions;
using System.Runtime.Serialization;

namespace Credit.Core.Domain.Exceptions.Cpf
{
    public class CpfCoreDomainException : CoreDomainException
    {
        public override string Key => nameof(CpfCoreDomainException);

        public CpfCoreDomainException(params CpfError[] errors) => AddErrors(errors);

        protected CpfCoreDomainException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}