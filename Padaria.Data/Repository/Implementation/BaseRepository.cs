using Padaria.Data.Contexto;
using Padaria.Data.Repository.Interface;
using Padaria.Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Padaria.Data.Repository.Implementation
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IBaseEntity
    {
        protected readonly PadariaContexto _contexto;

        public BaseRepository(PadariaContexto contexto)
        {
            _contexto = contexto;
        }

        public virtual List<T> SelecionarTudo()
        {
            return _contexto.Set<T>().ToList();
        }

        public T Selecionar(int id)
        {
            return _contexto.Set<T>().FirstOrDefault(x => x.Id.Equals(id));
        }

        public void Incluir(T entity)
        {
            _contexto.Set<T>().Add(entity);
            _contexto.SaveChanges();
        }

        public T Alterar(T entity)
        {
            _contexto.Set<T>().Update(entity);
            _contexto.SaveChanges();

            return entity;
        }

        public void Excluir(T entity)
        {
            _contexto.Set<T>().Remove(entity);
            _contexto.SaveChanges();
        }        
    }
}
