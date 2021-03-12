using Padaria.Data.Contexto;
using Padaria.Data.Repository.Interface;
using Padaria.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Padaria.Data.Repository.Implementation
{
    public class MateriaPrimaRepository : BaseRepository<MateriaPrima>, IMateriaPrimaRepository
    {
        public MateriaPrimaRepository(PadariaContexto contexto) : base(contexto) { }

        public override MateriaPrima Alterar(MateriaPrima materiaPrima)
        {
            var materiaPrimaEncontrada = Selecionar(materiaPrima.Id);
            if (materiaPrimaEncontrada == null)
                return null;

            //float novaQuantidade = materiaPrimaEncontrada.Quantidade + materiaPrima.Quantidade;

            if (materiaPrima.Quantidade > 0)
            {
                //_contexto.Entry(materiaPrimaEncontrada).CurrentValues.SetValues(materiaPrima);
                //materiaPrimaEncontrada.Quantidade = novaQuantidade;
                materiaPrima.Quantidade += materiaPrimaEncontrada.Quantidade;
                _contexto.Entry(materiaPrimaEncontrada).CurrentValues.SetValues(materiaPrima);
                //_contexto.Entry(materiaPrimaEncontrada.Quantidade).Property("Quantidade").IsModified = true;
                _contexto.SaveChanges();
            }

            return materiaPrima;
        }

        // Métodos relacionados à gestão de produtos finais
        // Possível alteração para parâmetros mais adequados
        public bool PermiteAbater(float qtdAbater, MateriaPrima materiaPrima)
        {
            float qtdResultante = materiaPrima.Quantidade - qtdAbater;
            
            if (qtdResultante < 0)
            {
                return false;
            }
            return true;
        }

        // Possível alteração para parâmetros mais adequados e outras regras de abatimento
        public void Abater(float qtdAbater, MateriaPrima materiaPrima)
        {
            var materiaPrimaEncontrada = Selecionar(materiaPrima.Id);

            if (PermiteAbater(qtdAbater, materiaPrima))
            {
                float novaQuantidade = materiaPrima.Quantidade - qtdAbater;
                
                _contexto.Entry(materiaPrimaEncontrada.Quantidade).CurrentValues
                    .SetValues(novaQuantidade);
                _contexto.SaveChanges();
            }
        }

        public MateriaPrima SelecionarPorNome(string nome)
        {
            return _contexto.MateriaPrima.FirstOrDefault(x => x.Nome.Equals(nome));
        }
    }
}
