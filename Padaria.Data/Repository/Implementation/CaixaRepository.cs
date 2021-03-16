using Microsoft.EntityFrameworkCore;
using Padaria.Data.Contexto;
using Padaria.Data.Repository.Interface;
using Padaria.Domain.Enum;
using Padaria.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Padaria.Data.Repository.Implementation
{
    public class CaixaRepository : BaseRepository<Caixa>, ICaixaRepository
    {
        public CaixaRepository(PadariaContexto contexto) : base(contexto) { }

        public override List<Caixa> SelecionarTudo()
        {
            return _contexto.Caixas
                             .Include(c => c.Vendas)
                             .ToList();
        }

        public override Caixa Selecionar(int id)
        {
            return _contexto.Caixas
                            .Include(c => c.Vendas)
                            .FirstOrDefault(c => c.Id.Equals(id));
        }

        public Caixa SelecionarFuncionarioId(int id)
        {
            return _contexto.Caixas.FirstOrDefault(c => c.UsuarioId.Equals(id));
        }

        public Caixa VerificaExisteCaixaAberto()
        {
            return _contexto.Caixas
                            .FirstOrDefault(c => c.Status.Equals(StatusDoCaixaEnum.Aberto));
        }

        //public Caixa SelecionarPorCaixaIdEData(int id, DateTime data)
        //{
        //    var caixa = _contexto.Caixas.FirstOrDefault(c => c.Id.Equals(id));

            //return _contexto.Entry(caixa)
            //                .Collection(v => v.Vendas)
            //                .Query()
            //                .Where(v => v.DataVenda.Date == data.Date)
            //                .Load();
                            
                            
               
            //return _contexto.Caixas
            //                .Include(c => c.Vendas)
            //                .ThenInclude(v => v.DataVenda.Date == data.Date)
            //                .FirstOrDefault(v => v.Id.Equals(id));
        //}
    }
}
