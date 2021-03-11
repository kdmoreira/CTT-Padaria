using Padaria.Data.Contexto;
using Padaria.Data.Repository.Interface;
using Padaria.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace Padaria.Data.Repository.Implementation
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(PadariaContexto contexto) : base(contexto) { }

        public override List<Usuario> SelecionarTudo()
        {
            return _contexto.Usuarios
                            .OrderBy(x => x.Nome)
                            .ToList();
        }

        public Usuario SelecionarPorEmail(string email)
        {
            return _contexto.Usuarios
                            .FirstOrDefault(x => x.Email.ToLower().Equals(email.ToLower()));
        }

        public List<Usuario> SelecionarPorNome(string nome)
        {
            return _contexto.Usuarios
                               .Where(x => x.Nome.Contains(nome))
                               .ToList();
        }

        public Usuario SelecionarPorNomeESenha(string nome, string senha)
        {
            return _contexto.Set<Usuario>().FirstOrDefault(u => u.Nome == nome && u.Senha == senha);
        }
    }
}
