using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Padaria.Domain.Model;
using System;

namespace Padaria.Data.Map
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produto");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .HasColumnType("varchar(250)")
                .IsRequired();

            builder.Property(x => x.Quantidade)
                .HasColumnType("float")
                .IsRequired();

            builder.Property(x => x.UnidadeDeMedida)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.Producao)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.Valor)
                .HasColumnType("float")
                .IsRequired();

            builder.Property(x => x.Ativo)
                .HasColumnType("bit")
                .IsRequired();
        }
    }
}
