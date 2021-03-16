using Padaria.Domain.Model;

namespace Padaria.Data.Repository.Interface
{
    public interface IProdutoComandaRepository : IBaseRepository<ProdutoComanda>
    {
        ProdutoComanda SelecionarPorComandaIdProdutoId(int comandaId, int produtoId);
       
    }
}
