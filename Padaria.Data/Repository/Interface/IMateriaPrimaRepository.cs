using Padaria.Domain.Model;

namespace Padaria.Data.Repository.Interface
{
    public interface IMateriaPrimaRepository : IBaseRepository<MateriaPrima>
    {
        bool PermiteAbater(float qtdAbater, MateriaPrima materiaPrima);
        void Abater(float qtdAbater, MateriaPrima materiaPrima);
        MateriaPrima SelecionarPorNome(string nome);
    }
}
