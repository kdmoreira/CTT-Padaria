using Microsoft.EntityFrameworkCore;
using Padaria.Data.Contexto;
using Padaria.Data.Repository.Interface;
using Padaria.Domain.Model;
using System.Linq;

namespace Padaria.Data.Repository.Implementation
{
    public class ProdutoComandaRepository : BaseRepository<ProdutoComanda>, IProdutoComandaRepository
    {
        public ProdutoComandaRepository(PadariaContexto contexto) : base(contexto) { }

        public ProdutoComanda SelecionarPorComandaIdProdutoId(int vendaId, int produtoId)
        {
            return _contexto.ProdutosComanda.FirstOrDefault(pc => pc.ComandaId == vendaId && pc.ProdutoId == produtoId);
        }

        public override ProdutoComanda Alterar(ProdutoComanda entity)
        {
            var resposta = SelecionarPorComandaIdProdutoId(entity.ComandaId, entity.ProdutoId);
            if (resposta == null)
                return null;

            _contexto.Entry(resposta).CurrentValues.SetValues(entity);
            _contexto.SaveChanges();

            return entity;
        }

    }
}
