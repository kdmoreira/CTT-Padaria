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
                             .Include(c => c.Usuario)
                             .ToList();
        }

        public override Caixa Selecionar(int id)
        {
            return _contexto.Caixas
                            .Include(c => c.Vendas)
                            .Include(c => c.Usuario)
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

        public Caixa VerificaExisteCaixaAbertoPorData(DateTime data)
        {
            return _contexto.Caixas
                            .FirstOrDefault(c => c.Status.Equals(StatusDoCaixaEnum.Aberto) && c.DataAbertura.Date == data.Date);
        }

        public List<Caixa> SelecionarPorData(DateTime data)
        {
            return _contexto.Caixas
                            .Where(c => c.DataAbertura.Date == data.Date )
                            .OrderBy(c => c.DataAbertura)
                            .Include(c => c.Usuario)
                            .ToList();
        }

        public Caixa SelecionarPorDataEId(DateTime data, int id)
        {
            return _contexto.Caixas
                            .Where(c => c.DataAbertura.Date.Date == data.Date && c.Id == id)
                            .FirstOrDefault();
        }
        
    }
}
