using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Padaria.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Padaria.Data.Map
{
    public class CaixaMap : IEntityTypeConfiguration<Caixa>
    {
        public void Configure(EntityTypeBuilder<Caixa> builder)
        {
            builder.ToTable("Caixa");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Status)
                .HasColumnType("int")
                .IsRequired();

            builder.HasOne(c => c.Usuario)
                .WithOne(u => u.Caixa)
                .HasForeignKey<Caixa>(c => c.UsuarioId);           
        }

        
    }
}
