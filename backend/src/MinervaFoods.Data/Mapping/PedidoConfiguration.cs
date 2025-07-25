using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinervaFoods.Domain.Entities;

namespace MinervaFoods.Data.Mapping
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedidos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                   .HasColumnType("uniqueidentifier")
                   .HasDefaultValueSql("NEWID()");

            builder.Property(p => p.NumeroPedido)
                 .IsRequired();

            builder.Property(p => p.CompradorId)
                   .IsRequired();

            builder.Property(p => p.DataPedido)
                   .IsRequired()
                   .HasColumnType("datetime");

            builder.Property(p => p.Observacao);

            builder.Property(p => p.StatusPedido)
                   .IsRequired()
                   .HasConversion<int>(); 

            builder.Property(p => p.ValorTotal)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Status)
                   .IsRequired();

            builder.Property(p => p.IdUserCreated)
                   .IsRequired();

            builder.Property(p => p.CreatedAt)
                   .HasDefaultValueSql("GETDATE()")
                   .IsRequired();

            builder.Property(p => p.IdUserUpdated);

            builder.Property(p => p.UpdatedAt);

            builder.HasMany(p => p.PedidoItem)
               .WithOne()
                .HasForeignKey(p => p.PedidoId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Comprador)
.WithOne()
.HasForeignKey<Pedido>(p => p.CompradorId);
        }
    }
}
