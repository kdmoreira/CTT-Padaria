using Padaria.Domain.Model;
using System;
using System.Collections.Generic;

namespace Padaria.Data.Repository.Interface
{
    public interface ICaixaRepository : IBaseRepository<Caixa>
    {
        Caixa SelecionarFuncionarioId(int id);

        Caixa VerificaExisteCaixaAberto();
        List<Caixa> SelecionarPorData(DateTime data);
    }
}
