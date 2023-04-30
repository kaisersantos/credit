using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Credit.Core.Domain.Entities;
using Credit.Core.Domain.Extensions;
using Credit.Core.Domain.ValueObjects;

namespace Credit.Infra.Adapter.EfCore.Config.Entities
{
    internal class FinanciamentoConfiguration : IEntityTypeConfiguration<Financiamento>
    {
        public void Configure(EntityTypeBuilder<Financiamento> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.ValorTotal).HasColumnName("Valor_Total");
            builder.Property(f => f.DataUltimoVencimento).HasColumnName("Data_Ultimo_Vencimento");
            builder.Property(f => f.TipoFinanciamento)
                .HasColumnName("Tipo_Financiamento")
                .HasConversion(f => (char)f, f => f.ToEnum<TipoFinanciamento>());

            builder.HasOne(f => f.Cliente)
                .WithMany(c => c.Financiamentos);
        }
    }
}
