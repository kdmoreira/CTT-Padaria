using Padaria.Domain.Enum;
using Padaria.Domain.Interface;
using System.Collections.Generic;

namespace Padaria.Domain.Model
{
    public class Produto : IBaseEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public UnidadeDeMedidaEnum UnidadeDeMedida { get; set; }
        public TipoDeProdutoEnum Producao { get; set; }
        public float Valor { get; set; }
        public float Quantidade { get; set; }
        public bool Ativo { get; set; }
        public List<ProdutoMateria> ProdutosMaterias { get; set; }
        public List<ProdutoVenda> ProdutosVendas { get; set; }
    }   
}




