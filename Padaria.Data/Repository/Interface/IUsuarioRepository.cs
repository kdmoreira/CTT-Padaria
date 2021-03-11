using Padaria.Domain.Model;
using System.Collections.Generic;

namespace Padaria.Data.Repository.Interface
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        public List<Usuario> SelecionarPorNome(string nome);
        public Usuario SelecionarPorEmail(string email);
    }
}
