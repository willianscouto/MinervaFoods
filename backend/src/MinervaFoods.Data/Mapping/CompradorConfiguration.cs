using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinervaFoods.Domain.Entities;

namespace MinervaFoods.Data.Mapping
{
    public class CompradorConfiguration : IEntityTypeConfiguration<Comprador>
    {
        public void Configure(EntityTypeBuilder<Comprador> builder)
        {
            builder.ToTable("Compradores");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                   .HasColumnType("uniqueidentifier")
                   .HasDefaultValueSql("NEWID()");

            builder.Property(c => c.Nome)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(c => c.Documento)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(c => c.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Telefone)
                   .HasMaxLength(20);

            builder.Property(c => c.Logradouro)
                   .HasMaxLength(150);

            builder.Property(c => c.Complemento)
                   .HasMaxLength(100);

            builder.Property(c => c.Bairro)
                   .HasMaxLength(100);

            builder.Property(c => c.Cidade)
                   .HasMaxLength(100);

            builder.Property(c => c.Estado)
                   .HasMaxLength(50);

            builder.Property(c => c.Cep)
                   .HasMaxLength(20);

            builder.Property(c => c.Pais)
                   .HasMaxLength(80);

            builder.Property(c => c.DataNascimento)
                   .HasColumnType("date");

            builder.Property(c => c.Status)
                   .IsRequired();

            builder.Property(c => c.IdUserCreated)
                   .IsRequired();

            builder.Property(c => c.CreatedAt)
                   .HasDefaultValueSql("GETDATE()") 
                   .IsRequired();

            builder.Property(c => c.IdUserUpdated);

            builder.Property(c => c.UpdatedAt);
        }
    }
}
