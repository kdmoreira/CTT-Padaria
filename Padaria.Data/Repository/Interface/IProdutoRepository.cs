using Padaria.Domain.Model;
using System.Collections.Generic;

namespace Padaria.Data.Repository.Interface
{
    public interface IProdutoRepository : IBaseRepository<Produto>
    {
        List<Produto> SelecionarPorNome(string nome);

        void DescarteProduzidos();

        Produto SelecionarProdutoPorNome(string nome);

        List<Produto> SelecionarTudoCompleto();

        Produto Produzir(Produto produtoEncontrado, float quantidade);

        List<Produto> SelecionarInativos();

        List<Produto> SelecionarInativosPorNome(string nome);
    }
}
