using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Padaria.Domain.Model;

namespace Padaria.Data.Map
{
    public class ProdutoVendaMap : IEntityTypeConfiguration<ProdutoVenda>
    {
        public void Configure(EntityTypeBuilder<ProdutoVenda> builder)
        {
            builder.ToTable("ProdutoVenda");

            builder.HasKey(x => new { x.ProdutoId, x.VendaId });

            builder.HasOne<Produto>(pv => pv.Produto)
                .WithMany(p => p.ProdutosVendas)
                .HasForeignKey(pv => pv.ProdutoId);

            builder.HasOne<Venda>(pv => pv.Venda)
                .WithMany(m => m.ProdutosVendas)
                .HasForeignKey(pv => pv.VendaId);

            builder.HasOne(x => x.Usario)
                .WithMany(x => x.ProdutosVendas)
                .HasForeignKey(x => x.UsarioId);

            builder.Property(x => x.FormaDePagamento)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.PrecoTotal)
                .HasColumnType("float")
                .IsRequired();

            builder.Property(x => x.PrecoUnitario)
                .HasColumnType("float")
                .IsRequired();

            builder.Property(x => x.Quantidade)
                .HasColumnType("float")
                .IsRequired();
        }
    }
}
