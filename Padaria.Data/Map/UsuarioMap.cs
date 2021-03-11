using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Padaria.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Padaria.Data.Map
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome)
                .HasColumnType("varchar(150)")
                .IsRequired();
            builder.Property(x => x.Email)
                .HasColumnType("varchar(100)")
                .IsRequired();
            builder.Property(x => x.DataNascimento)
                .HasColumnType("datetime")
                .IsRequired();
            builder.Property(x => x.Perfil)
                .HasColumnType("int")
                .IsRequired();

            builder.HasData(new Usuario
            {
                Nome = "Thaise",
                Email = "thaise@gmail.com",
                DataNascimento = new DateTime(1990, 02, 01),
                Perfil = 0
            });
        }
    }
}
