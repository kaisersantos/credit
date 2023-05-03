using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Credit.Core.Domain.Entities;

namespace Credit.Infra.Adapter.EfCore.Config.Entities
{
    internal class FinanciamentoConfiguration : IEntityTypeConfiguration<Financiamento>
    {
        public void Configure(EntityTypeBuilder<Financiamento> builder)
        {
            builder.ToTable("Financiamento");
            builder.HasKey(f => f.Id);
            builder.Property(f => f.ValorTotal).HasColumnName("Valor_Total");
            builder.Property(f => f.DataUltimoVencimento).HasColumnName("Data_Ultimo_Vencimento");
            builder.Property(f => f.ClienteId).HasColumnName("Cliente_Id");
            builder.Property(f => f.Credito)
                .HasColumnName("Tipo_Financiamento")
                .HasConversion(c => (char) c!.TipoCredito, c => Credito.GetTipoCredito(c));

            builder.HasOne(f => f.Cliente)
                .WithMany(c => c.Financiamentos);

            builder.Ignore(f => f.QuantidadeParcelas);
            builder.Ignore(f => f.DataPrimeiroVencimento);
        }
    }
}
