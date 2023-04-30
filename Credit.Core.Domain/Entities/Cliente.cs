using Credit.Core.Domain.ValueObjects;

namespace Credit.Core.Domain.Entities
{
    public class Cliente
    {
        public long Id { get; set; }

        public Guid Uid { get; set; }

        public Cpf Cpf { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string Uf { get; set; } = string.Empty;

        public string Celular { get; set; } = string.Empty;

        public virtual ICollection<Financiamento>? Financiamentos { get; set; }
    }
}
