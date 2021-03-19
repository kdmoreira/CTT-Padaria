using Padaria.Domain.Model;
using System;
using System.Collections.Generic;

namespace Padaria.Data.Repository.Interface
{
    public interface ICaixaRepository : IBaseRepository<Caixa>
    {
        Caixa SelecionarFuncionarioId(int id);

        Caixa VerificaExisteCaixaAberto();
        Caixa VerificaExisteCaixaAbertoPorData(DateTime data);
        List<Caixa> SelecionarPorData(DateTime data);
        Caixa SelecionarPorDataEId(DateTime data, int id);
    }
}
