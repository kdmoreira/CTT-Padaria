using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Padaria.Domain.Model;

namespace Padaria.Data.Map
{
    public class ProdutoMateriaMap : IEntityTypeConfiguration<ProdutoMateria>
    {
        public void Configure(EntityTypeBuilder<ProdutoMateria> builder)
        {
            builder.ToTable("ProdutoMateria");

            builder.HasKey(x => new { x.ProdutoId, x.MateriaPrimaId });

            builder.HasOne<Produto>(pm => pm.Produto)
                .WithMany(p => p.ProdutosMaterias)
                .HasForeignKey(pm => pm.ProdutoId);

            builder.HasOne<MateriaPrima>(pm => pm.MateriaPrima)
                .WithMany(m => m.ProdutosMaterias)
                .HasForeignKey(pm => pm.MateriaPrimaId);

            builder.Property("Quantidade").HasColumnType("float").IsRequired();

            builder.Property("Porcao").HasColumnType("int").IsRequired();
        }
    }
}
