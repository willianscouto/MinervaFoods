using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinervaFoods.Domain.Entities;

namespace MinervaFoods.Data.Mapping
{
    public class CarneConfiguration : IEntityTypeConfiguration<Carne>
    {
        public void Configure(EntityTypeBuilder<Carne> builder)
        {
            builder.ToTable("Carnes");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                   .HasColumnType("uniqueidentifier")
                   .HasDefaultValueSql("NEWID()"); 

            builder.Property(u => u.Nome)
                   .IsRequired()
                   .HasMaxLength(120);

            builder.Property(u => u.UnidadeMedida)
                   .IsRequired();

            builder.Property(u => u.TipoCarne)
                   .HasConversion<string>() 
                   .HasMaxLength(20);

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
