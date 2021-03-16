using Padaria.Domain.Model;
using System;
using System.Collections.Generic;

namespace Padaria.Data.Repository.Interface
{
    public interface ICaixaRepository : IBaseRepository<Caixa>
    {
        Caixa SelecionarFuncionarioId(int id);

        Caixa VerificaExisteCaixaAberto();
        //Caixa SelecionarPorCaixaIdEData(int id, DateTime data);
    }
}
