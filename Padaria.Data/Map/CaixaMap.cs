using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Padaria.Domain.Model;

namespace Padaria.Data.Map
{
    public class CaixaMap : IEntityTypeConfiguration<Caixa>
    {
        public void Configure(EntityTypeBuilder<Caixa> builder)
        {
            builder.ToTable("Caixa");

            builder.HasKey(x => x.Id);            

            builder.HasOne(c => c.Usuario)
                .WithMany(u => u.Caixas)
                .HasForeignKey(c => c.UsuarioId);

            builder.Property(x => x.DataAbertura)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(x => x.DataFechamento)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(x => x.ValorTotal)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.Status)
               .HasColumnType("int")
               .IsRequired();
        }

        
    }
}
