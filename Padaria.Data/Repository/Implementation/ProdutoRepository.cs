using Padaria.Data.Contexto;
using Padaria.Data.Repository.Interface;
using Padaria.Domain.Model;
using System.Linq;

namespace Padaria.Data.Repository.Implementation
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(PadariaContexto contexto) : base(contexto) { }

        public Produto SelecionarPorNome(string nome)
        {
            return _contexto.Produtos.FirstOrDefault(x => x.Nome.Equals(nome));
        }
        
    }
}
