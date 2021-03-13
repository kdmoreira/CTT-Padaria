using Padaria.Domain.Model;
using System.Collections.Generic;

namespace Padaria.Data.Repository.Interface
{
    public interface IProdutoRepository : IBaseRepository<Produto>
    {
        Produto SelecionarPorNome(string nome);

        List<Produto> SelecionarTudoCompleto();

        bool NaoPermiteAbater(Produto produtoEncontrado, float quantidade);
    }
}
