using System.Runtime.Serialization;

namespace Credit.Core.Domain.Exceptions.EnumExtensions
{
    public class EnumExtensionsCoreDomainException : CoreDomainException
    {
        public override string Key => nameof(EnumExtensionsCoreDomainException);

        public EnumExtensionsCoreDomainException(params EnumExtensionsError[] errors) => AddErrors(errors);

        protected EnumExtensionsCoreDomainException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}