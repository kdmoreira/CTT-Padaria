﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Padaria.Data.Contexto;

namespace Padaria.Data.Migrations
{
    [DbContext(typeof(PadariaContexto))]
    partial class PadariaContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Padaria.Domain.Model.Caixa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataAbertura")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("DataFechamento")
                        .HasColumnType("datetime");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Caixa");
                });

            modelBuilder.Entity("Padaria.Domain.Model.Comanda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataEntrada")
                        .HasColumnType("datetime");

                    b.HasKey("Id");

                    b.ToTable("Comanda");
                });

            modelBuilder.Entity("Padaria.Domain.Model.MateriaPrima", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativa")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<double>("Quantidade")
                        .HasColumnType("float");

                    b.Property<int>("UnidadeDeMedida")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("MateriaPrima");
                });

            modelBuilder.Entity("Padaria.Domain.Model.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(250)");

                    b.Property<int>("Producao")
                        .HasColumnType("int");

                    b.Property<double>("Quantidade")
                        .HasColumnType("float");

                    b.Property<int>("UnidadeDeMedida")
                        .HasColumnType("int");

                    b.Property<double>("Valor")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("Padaria.Domain.Model.ProdutoComanda", b =>
                {
                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<int>("ComandaId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<decimal>("PrecoTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<double>("Quantidade")
                        .HasColumnType("float");

                    b.HasKey("ProdutoId", "ComandaId");

                    b.HasIndex("ComandaId");

                    b.ToTable("ProdutoComanda");
                });

            modelBuilder.Entity("Padaria.Domain.Model.ProdutoMateria", b =>
                {
                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<int>("MateriaPrimaId")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int>("Porcao")
                        .HasColumnType("int");

                    b.Property<double>("Quantidade")
                        .HasColumnType("float");

                    b.HasKey("ProdutoId", "MateriaPrimaId");

                    b.HasIndex("MateriaPrimaId");

                    b.ToTable("ProdutoMateria");
                });

            modelBuilder.Entity("Padaria.Domain.Model.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DataNascimento")
                        .IsRequired()
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Perfil")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Usuario");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DataNascimento = new DateTime(1990, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "thaise@gmail.com",
                            Nome = "Thaise",
                            Perfil = "Administrador",
                            Senha = "123"
                        });
                });

            modelBuilder.Entity("Padaria.Domain.Model.Venda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CaixaId")
                        .HasColumnType("int");

                    b.Property<int>("ComandaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataVenda")
                        .HasColumnType("datetime");

                    b.Property<decimal?>("Dinheiro")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("FormaDePagamento")
                        .HasColumnType("int");

                    b.Property<int>("StatusDaVenda")
                        .HasColumnType("int");

                    b.Property<decimal?>("Troco")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorTotal")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CaixaId");

                    b.HasIndex("ComandaId")
                        .IsUnique();

                    b.HasIndex("UsuarioId");

                    b.ToTable("Venda");
                });

            modelBuilder.Entity("Padaria.Domain.Model.Caixa", b =>
                {
                    b.HasOne("Padaria.Domain.Model.Usuario", "Usuario")
                        .WithMany("Caixas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Padaria.Domain.Model.ProdutoComanda", b =>
                {
                    b.HasOne("Padaria.Domain.Model.Comanda", "Comanda")
                        .WithMany("ProdutosComanda")
                        .HasForeignKey("ComandaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Padaria.Domain.Model.Produto", "Produto")
                        .WithMany("ProdutosComanda")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Padaria.Domain.Model.ProdutoMateria", b =>
                {
                    b.HasOne("Padaria.Domain.Model.MateriaPrima", "MateriaPrima")
                        .WithMany("ProdutosMaterias")
                        .HasForeignKey("MateriaPrimaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Padaria.Domain.Model.Produto", "Produto")
                        .WithMany("ProdutosMaterias")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Padaria.Domain.Model.Venda", b =>
                {
                    b.HasOne("Padaria.Domain.Model.Caixa", "Caixa")
                        .WithMany("Vendas")
                        .HasForeignKey("CaixaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Padaria.Domain.Model.Comanda", "Comanda")
                        .WithOne("Venda")
                        .HasForeignKey("Padaria.Domain.Model.Venda", "ComandaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Padaria.Domain.Model.Usuario", "Usuario")
                        .WithMany("Vendas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
