using Microsoft.EntityFrameworkCore;
using Padaria.Data.Map;
using Padaria.Domain.Model;

namespace Padaria.Data.Contexto
{
    public class PadariaContexto : DbContext
    {
        public PadariaContexto(DbContextOptions options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<MateriaPrima> MateriasPrimas { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<ProdutoVenda> ProdutosVendas { get; set; }
        public DbSet<ProdutoMateria> ProdutosMaterias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new MateriaPrimaMap());
            modelBuilder.ApplyConfiguration(new ProdutoMap());
            modelBuilder.ApplyConfiguration(new VendaMap());
            modelBuilder.ApplyConfiguration(new ProdutoVendaMap());
            modelBuilder.ApplyConfiguration(new ProdutoMateriaMap());

            base.OnModelCreating(modelBuilder);
        }

    }
}
