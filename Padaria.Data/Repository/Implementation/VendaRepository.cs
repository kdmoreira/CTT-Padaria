using Padaria.Data.Contexto;
using Padaria.Data.Repository.Interface;
using Padaria.Domain.Model;

namespace Padaria.Data.Repository.Implementation
{
    public class VendaRepository : BaseRepository<Venda>, IVendaRepository
    {
        public VendaRepository(PadariaContexto contexto) : base(contexto) { }

                
    }
}
