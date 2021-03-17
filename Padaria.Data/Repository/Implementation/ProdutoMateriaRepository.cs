using Microsoft.EntityFrameworkCore;
using Padaria.Data.Contexto;
using Padaria.Data.Repository.Interface;
using Padaria.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace Padaria.Data.Repository.Implementation
{
    public class ProdutoMateriaRepository : BaseRepository<ProdutoMateria>, IProdutoMateriaRepository
    {
        public ProdutoMateriaRepository(PadariaContexto contexto) : base(contexto) { }        

        public ProdutoMateria SelecionarPorProdutoIdMateriaId(int produtoId, int materiaId)
        {
            return _contexto.ProdutosMaterias
                            .FirstOrDefault(p => p.ProdutoId.Equals(produtoId) && p.MateriaPrimaId.Equals(materiaId));
        }

        public List<ProdutoMateria> SelecionarTudoCompleto()
        {
            return _contexto.ProdutosMaterias
                .Include(x => x.Produto)
                .Include(x => x.MateriaPrima)
                .ToList();
        }
    }
}
