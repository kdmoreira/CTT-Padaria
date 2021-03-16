using Padaria.Domain.Model;
using System.Collections.Generic;

namespace Padaria.Data.Repository.Interface
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        List<Usuario> SelecionarPorNome(string nome);
        Usuario SelecionarPorNomeEmail(string nome, string email);
        Usuario SelecionarPorEmail(string email);
        Usuario SelecionarPorNomeESenha(string nome, string senha);

        
    }
}
