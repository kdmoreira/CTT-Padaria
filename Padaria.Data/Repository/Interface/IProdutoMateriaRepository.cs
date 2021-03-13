using Padaria.Domain.Model;
using System.Collections.Generic;

namespace Padaria.Data.Repository.Interface
{
    public interface IProdutoMateriaRepository : IBaseRepository<ProdutoMateria>
    {
        List<ProdutoMateria> SelecionarTudoCompleto();
    }
}
