using Credit.Core.Domain.Exceptions;

namespace Credit.Core.Domain.Exceptions.Cpf
{
    public class CpfError : CoreError
    {
        public static CpfError CpfEmpty =>
            new(nameof(CpfEmpty),
                $"The CPF cannot be empty.");

        public static CpfError CpfInvalidFormat(string cpf) =>
            new(nameof(CpfInvalidFormat),
                $"The CPF '{cpf}' format is invalid.");

        public static CpfError CpfInvalid(string cpf) =>
            new(nameof(CpfInvalid),
                $"The CPF '{cpf}' is invalid.");

        public CpfError(string key, string message) : base(key, message) { }
    }
}
