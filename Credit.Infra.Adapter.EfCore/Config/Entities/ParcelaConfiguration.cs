using Credit.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Credit.Infra.Adapter.EfCore.Config.Entities
{
    internal class ParcelaConfiguration : IEntityTypeConfiguration<Parcela>
    {
        public void Configure(EntityTypeBuilder<Parcela> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.NumeroParcela).HasColumnName("Numero_Parcela");
            builder.Property(p => p.ValorParcela).HasColumnName("Valor_Parcela");
            builder.Property(p => p.DataVencimento).HasColumnName("Data_Vencimento");
            builder.Property(p => p.DataPagamento).HasColumnName("Data_Pagamento");

            builder.HasOne(p => p.Financiamento)
                .WithMany(f => f.Parcelas);
        }
    }
}
