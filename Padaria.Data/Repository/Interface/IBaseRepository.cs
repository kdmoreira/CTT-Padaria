using Padaria.Domain.Interface;
using System.Collections.Generic;

namespace Padaria.Data.Repository.Interface
{
    public interface IBaseRepository<T> where T: class, IBaseEntity
    {
        List<T> SelecionarTudo();
        T Selecionar(int id);
        void Incluir(T entity);
        T Alterar(T entity);
        void Excluir(T entity);
    }
}
