using System.Text.RegularExpressions;
using Credit.Core.Domain.Exceptions.Cpf;

namespace Credit.Core.Domain.ValueObjects
{
    public readonly struct Cpf : IEquatable<Cpf>
    {
        private readonly string _valor;
        private readonly string _valorSemMascara;

        public Cpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                throw new CpfCoreDomainException(CpfError.CpfEmpty);

            _valor = Regex.Replace(cpf.Trim(), @"(\d{3})(\d{3})(\d{3})(\d{2})", @"$1.$2.$3-$4");

            _valorSemMascara = _valor
                .Replace(".", "")
                .Replace("-", "");

            if (!Regex.IsMatch(_valorSemMascara, @"^[0-9]+$"))
                throw new CpfCoreDomainException(CpfError.CpfInvalidFormat(cpf));

            if (!Validate(_valorSemMascara))
                throw new CpfCoreDomainException(CpfError.CpfInvalid(cpf));
        }

        public static implicit operator string(Cpf cpf) => cpf._valor;

        public static implicit operator Cpf(string cpf) => new(cpf);

        public static implicit operator long(Cpf cpf) => Convert.ToInt32(cpf._valorSemMascara);

        public static implicit operator Cpf(long cpf) => new(cpf.ToString());

        public static bool operator ==(Cpf cpf1, Cpf cpf2) => cpf1.Equals(cpf2);

        public static bool operator !=(Cpf cpf1, Cpf cpf2) => cpf1.Equals(cpf2);

        public bool Equals(Cpf other) => other._valor == _valor;

        public override bool Equals(object? obj) => obj is Cpf cpf && Equals(cpf);

        public override int GetHashCode() => base.GetHashCode();

        public string ToMaskedString() => _valor;

        public override string ToString() => _valorSemMascara;

        private static bool Validate(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf,
                digito;
            int soma,
                resto;

            if (cpf.Length != 11)
                return false;

            tempCpf = cpf[..9];
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += Convert.ToInt32(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCpf += digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += Convert.ToInt32(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito += resto.ToString();

            return cpf.EndsWith(digito);
        }
    }
}
