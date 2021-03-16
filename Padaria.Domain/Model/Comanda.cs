using Padaria.Domain.Interface;
using System;
using System.Collections.Generic;

namespace Padaria.Domain.Model
{
    public class Comanda : IBaseEntity
    {
        public int Id { get; set; }
        public DateTime DataEntrada { get; set; }
        public List<ProdutoComanda> ProdutosComanda { get; set; }
        public Venda Venda { get; set; }
    }
}
