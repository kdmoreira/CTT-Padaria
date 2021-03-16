using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Padaria.Domain.Model;

namespace Padaria.Data.Map
{
    public class ProdutoComandaMap : IEntityTypeConfiguration<ProdutoComanda>
    {
        public void Configure(EntityTypeBuilder<ProdutoComanda> builder)
        {
            builder.ToTable("ProdutoComanda");

            builder.HasKey(x => new { x.ProdutoId, x.ComandaId });

            builder.HasOne<Produto>(pc => pc.Produto)
                .WithMany(p => p.ProdutosComanda)
                .HasForeignKey(pc => pc.ProdutoId);

            builder.HasOne<Comanda>(pc => pc.Comanada)
                .WithMany(m => m.ProdutosComanda)
                .HasForeignKey(pc => pc.ComandaId);

            builder.Property(x => x.PrecoTotal)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.Quantidade)
                .HasColumnType("float")
                .IsRequired();
        }
    }
}
