using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinervaFoods.Domain.Entities;

namespace MinervaFoods.Data.Mapping
{
    internal class PaisConfiguration : IEntityTypeConfiguration<Pais>
    {
        public void Configure(EntityTypeBuilder<Pais> builder)
        {
            builder.ToTable("Paises");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                   .HasColumnType("uniqueidentifier")
                   .HasDefaultValueSql("NEWID()");

            builder.Property(u => u.Sigla)
              .IsRequired();

            builder.Property(u => u.Nome)
             .IsRequired();

            builder.Property(u => u.Status)
                .IsRequired();

            builder.Property(u => u.IdUserCreated)
                   .IsRequired();

            builder.Property(u => u.CreatedAt)
                   .IsRequired();

            builder.Property(u => u.IdUserUpdated);

            builder.Property(u => u.UpdatedAt);
        }
    }
}
