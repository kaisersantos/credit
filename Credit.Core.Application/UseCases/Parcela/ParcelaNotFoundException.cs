using Credit.Core.Application.Exceptions;
using System.Runtime.Serialization;

namespace Credit.Core.Application.UseCases.Parcelas
{
    [Serializable]
    public class ParcelaNotFoundException : NotFoundException
    {
        public override string Key => nameof(ParcelaNotFoundException);

        public ParcelaNotFoundException(params ParcelaError[] errors) => AddErrors(errors);

        protected ParcelaNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}