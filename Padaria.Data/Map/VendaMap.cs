using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Padaria.Domain.Model;

namespace Padaria.Data.Map
{
    public class VendaMap : IEntityTypeConfiguration<Venda>
    {
        public void Configure(EntityTypeBuilder<Venda> builder)
        {
            builder.ToTable("Venda");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.DataVenda)
                .HasColumnType("datetime")
                .IsRequired();
           
            builder.Property(x => x.StatusDaVenda)
                .HasColumnType("int");

            builder.Property(x => x.FormaDePagamento)
               .HasColumnType("int")
               .IsRequired();

            builder.Property(x => x.ValorTotal)
               .HasColumnType("decimal(18,2)")
               .IsRequired();

            builder.Property(x => x.Dinheiro)
               .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Troco)
               .HasColumnType("decimal(18,2)");               

            builder.HasOne(v => v.Usuario)
                .WithMany(u => u.Vendas)
                .HasForeignKey(v => v.UsuarioId);

            builder.HasOne(v => v.Comanda)
                .WithOne(c => c.Venda)
                .HasForeignKey<Venda>(v => v.ComandaId);

            builder.HasOne(v => v.Caixa)
                .WithMany(c => c.Vendas)
                .HasForeignKey(v => v.CaixaId)
                .OnDelete(DeleteBehavior.NoAction); ;

        }
    }
}
