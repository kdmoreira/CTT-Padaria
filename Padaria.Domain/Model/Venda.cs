using Padaria.Domain.Enum;
using Padaria.Domain.Interface;
using System;
using System.Collections.Generic;

namespace Padaria.Domain.Model
{
    public class Venda :IBaseEntity
    {
        public int Id { get; set; }
        public DateTime DataVenda { get; set; }
        public StatusDaVendaEnum StatusDaVenda { get; set; }
        public List<ProdutoVenda> ProdutosVendas { get; set; }
    }
}
