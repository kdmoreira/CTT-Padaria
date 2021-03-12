using System;
using System.Collections.Generic;
using System.Text;

namespace Padaria.Data.Repository.Implementation
{
    public class MateriaPrimaRepository : BaseRepository<MateriaPrima>, IMateriaPrimaRepository
    {
        public MateriaPrimaRepository(PadariaContexto contexto) : base(contexto) { }

    }
}
