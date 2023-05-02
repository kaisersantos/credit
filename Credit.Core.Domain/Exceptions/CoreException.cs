using System.Runtime.Serialization;

namespace Credit.Core.Domain.Exceptions
{
    [Serializable]
    public abstract class CoreException : Exception
    {
        public ICollection<CoreError> Errors { get; set; } = new List<CoreError>();

        public abstract string Key { get; }

        protected CoreException() : base("A core error has occurred, please verify the 'errors' property to obtain more details") { }

        protected CoreException(string message) : base(message) { }

        protected CoreException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public CoreException AddErrors(params CoreError[] errors)
        {
            Errors = new List<CoreError>(errors);

            return this;
        }
    }
}
