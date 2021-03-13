using Padaria.Data.Contexto;
using Padaria.Data.Repository.Interface;
using Padaria.Domain.Model;

namespace Padaria.Data.Repository.Implementation
{
    public class ProdutoVendaRepository : BaseRepository<ProdutoVenda>, IProdutoVendaRepository
    {
        public ProdutoVendaRepository(PadariaContexto contexto) : base(contexto) { }
        
    }
}
