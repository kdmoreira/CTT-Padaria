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
        }
    }
}
