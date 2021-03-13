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

        public List<ProdutoMateria> SelecionarTudoCompleto()
        {
            return _contexto.ProdutosMaterias
                .Include(x => x.Produto).Include(x => x.MateriaPrima).ToList();
        }

        //public IQueryable<ProdutoMateria> SelecionarProdutoMateria(int id)
        //{
        //    return _contexto.ProdutosMaterias.Where(x => x.ProdutoId.Equals(id));
        //}

    }
}
