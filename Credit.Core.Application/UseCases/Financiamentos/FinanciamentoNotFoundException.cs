using Credit.Core.Application.Exceptions;
using System.Runtime.Serialization;

namespace Credit.Core.Application.UseCases.Financiamentos
{
    [Serializable]
    public class FinanciamentoNotFoundException : NotFoundException
    {
        public override string Key => nameof(FinanciamentoNotFoundException);

        public FinanciamentoNotFoundException(params FinanciamentoError[] errors) => AddErrors(errors);

        protected FinanciamentoNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}