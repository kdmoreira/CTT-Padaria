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
            //var a = _contexto.Usuarios.FirstOrDefault(x => x.Email == email);
            //return a;
            return _contexto.Usuarios.FirstOrDefault(x => x.Email == email);
        }

        public List<Usuario> SelecionarPorNome(string nome)
        {
            return _contexto.Usuarios
                               .Where(x => x.Nome.Contains(nome))
                               .OrderBy(u => u.Nome)
                               .ToList();
        }

        public Usuario SelecionarPorNomeESenha(string nome, string senha)
        {
            return _contexto.Usuarios.FirstOrDefault(u => u.Nome == nome && u.Senha == senha);
        }

    }
}
