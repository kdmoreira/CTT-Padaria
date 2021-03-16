using Microsoft.EntityFrameworkCore;
using Padaria.Data.Contexto;
using Padaria.Data.Repository.Interface;
using Padaria.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace Padaria.Data.Repository.Implementation
{
    public class ComandaRepository : BaseRepository<Comanda>, IComandaRepository
    {
        public ComandaRepository(PadariaContexto contexto) : base(contexto) { }

        public override List<Comanda> SelecionarTudo()
        {

            return _contexto.Comandas
                            .Include(c => c.ProdutosComanda)
                            .ThenInclude(pc => pc.Produto)
                            .Include(c => c.Venda)
                            .ToList();
        }

        public override Comanda Selecionar(int id)
        {
            return _contexto.Comandas
                            .Include(c => c.ProdutosComanda)
                            .ThenInclude(pc => pc.Produto)
                            .Include(pc => pc.Venda)
                            .FirstOrDefault(c => c.Id.Equals(id));
        }
    }
}
