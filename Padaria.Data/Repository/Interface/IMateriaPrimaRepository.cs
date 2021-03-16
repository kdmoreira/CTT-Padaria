using Padaria.Domain.Model;
using System.Collections.Generic;

namespace Padaria.Data.Repository.Interface
{
    public interface IMateriaPrimaRepository : IBaseRepository<MateriaPrima>
    {
        List<MateriaPrima> SelecionarPorNome(string nome);

        MateriaPrima SelecionarMateriaPrimaPorNome(string nome);

        MateriaPrima ValidarInativacao(MateriaPrima materiaPrima);

        List<MateriaPrima> SelecionarInativas();

        List<MateriaPrima> SelecionarInativasPorNome(string nome);
    }
}
