using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinervaFoods.Domain.Entities;

namespace MinervaFoods.Data.Mapping
{
    public class PedidoItemConfiguration : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            builder.ToTable("PedidosItens");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                   .HasColumnType("uniqueidentifier")
                   .HasDefaultValueSql("NEWID()");

            builder.Property(p => p.PedidoId)
                   .IsRequired();

            builder.Property(p => p.CarneId)
                   .IsRequired();

            builder.Property(p => p.Quantidade)
                   .IsRequired();

            builder.Property(u => u.Moeda)
             .HasConversion<string>()
             .HasMaxLength(20);

            builder.Property(p => p.PrecoUnitario)
                   .IsRequired();     

            builder.Property(p => p.Total)
                .IsRequired();

            builder.Property(p => p.Cotacao)
                    .IsRequired();

            builder.Property(p => p.ValorTotalCotacao)
                   .IsRequired();

            builder.Property(p => p.Status)
                   .IsRequired();

            builder.Property(p => p.IdUserCreated)
                   .IsRequired();

            builder.Property(p => p.CreatedAt)
                   .HasDefaultValueSql("GETDATE()")
                   .IsRequired();

            builder.Property(p => p.IdUserUpdated);

            builder.Property(p => p.UpdatedAt);
        }
    }
}
