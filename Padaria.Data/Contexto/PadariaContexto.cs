using Microsoft.EntityFrameworkCore;
using Padaria.Data.Map;
using Padaria.Domain.Model;

namespace Padaria.Data.Contexto
{
    public class PadariaContexto : DbContext
    {
        public PadariaContexto(DbContextOptions options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<MateriaPrima> MateriaPrima { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new MateriaPrimaMap());

            base.OnModelCreating(modelBuilder);
        }

    }
}
