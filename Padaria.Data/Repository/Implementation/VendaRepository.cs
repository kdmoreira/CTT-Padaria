using Microsoft.EntityFrameworkCore;
using Padaria.Data.Contexto;
using Padaria.Data.Repository.Interface;
using Padaria.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace Padaria.Data.Repository.Implementation
{
    public class VendaRepository : BaseRepository<Venda>, IVendaRepository
    {
        public VendaRepository(PadariaContexto contexto) : base(contexto) { }

        public override List<Venda> SelecionarTudo()
        {
            return _contexto.Vendas
                            .Include(v => v.Usuario)
                            .ToList();
        }
        public Venda SelecionarComandaId(int id)
        {
            return _contexto.Vendas
                            .Include(v => v.Usuario)
                            .FirstOrDefault(v => v.ComandaId.Equals(id));
        }
    }
}
