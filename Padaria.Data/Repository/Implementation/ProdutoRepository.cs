using Microsoft.EntityFrameworkCore;
using Padaria.Data.Contexto;
using Padaria.Data.Repository.Interface;
using Padaria.Domain.Model;
using System.Collections.Generic;
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

        public List<Produto> SelecionarTudoCompleto()
        {
            return _contexto.Produtos
                .Include(x => x.ProdutosMaterias).ToList();
        }

        /* public IQueryable<ProdutoMateria> SelecionarProdutoMateria(int id)
        {
            return _contexto.ProdutosMaterias.Where(x => x.ProdutoId.Equals(id));
        } */

        public bool NaoPermiteAbater(Produto produtoEncontrado, float quantidade)
        {
            var receita = _contexto.ProdutosMaterias.Where(x => x.ProdutoId.Equals(produtoEncontrado.Id));
            
            foreach (ProdutoMateria pm in receita)
            {
                var materiaPrimaEncontrada = _contexto.MateriasPrimas.FirstOrDefault(x => x.Id.Equals(pm.MateriaPrimaId));

                if (materiaPrimaEncontrada.Quantidade - pm.MateriaPrima.Quantidade < 0)
                {
                    return true;
                }
            }
            return false;
        }

        /* float qtdResultante = materiaPrima.Quantidade - qtdAbater;

            if (qtdResultante < 0)
            {
                return false;
            }
            return true; */
    }
}
