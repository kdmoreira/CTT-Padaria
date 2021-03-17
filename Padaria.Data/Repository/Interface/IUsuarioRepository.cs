using Padaria.Domain.Model;
using System.Collections.Generic;

namespace Padaria.Data.Repository.Interface
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        List<Usuario> SelecionarPorNome(string nome);        
        Usuario SelecionarPorEmail(string email);
        Usuario SelecionarPorNomeESenha(string nome, string senha);

        
    }
}
