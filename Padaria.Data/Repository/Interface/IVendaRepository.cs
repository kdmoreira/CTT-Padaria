using Padaria.Domain.Model;
using System.Collections.Generic;

namespace Padaria.Data.Repository.Interface
{
    public interface IVendaRepository : IBaseRepository<Venda>
    {
        Venda SelecionarComandaId(int id);
    }
}
