using Microsoft.EntityFrameworkCore;
using Padaria.Data.Map;
using Padaria.Domain.Model;

namespace Padaria.Data.Contexto
{
    public class PadariaContexto : DbContext
    {
        public PadariaContexto(DbContextOptions options) : base(options) { }

        public DbSet<Caixa> Caixas { get; set; }
        public DbSet<Comanda> Comandas { get; set; }
        public DbSet<MateriaPrima> MateriasPrimas { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<ProdutoComanda> ProdutosComanda { get; set; }
        public DbSet<ProdutoMateria> ProdutosMaterias { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Venda> Vendas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CaixaMap());
            modelBuilder.ApplyConfiguration(new ComandaMap());
            modelBuilder.ApplyConfiguration(new MateriaPrimaMap());
            modelBuilder.ApplyConfiguration(new ProdutoMap());
            modelBuilder.ApplyConfiguration(new ProdutoComandaMap());
            modelBuilder.ApplyConfiguration(new ProdutoMateriaMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new VendaMap());

            base.OnModelCreating(modelBuilder);
        }

    }
}
