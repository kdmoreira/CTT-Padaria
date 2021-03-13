using Padaria.Data.Contexto;
using Padaria.Data.Repository.Interface;
using Padaria.Domain.Model;

namespace Padaria.Data.Repository.Implementation
{
    public class ProdutoMateriaRepository : BaseRepository<ProdutoMateria>, IProdutoMateriaRepository
    {
        public ProdutoMateriaRepository(PadariaContexto contexto) : base(contexto) { }
        
    }
}
