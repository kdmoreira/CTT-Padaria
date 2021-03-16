using Padaria.Domain.Enum;
using Padaria.Domain.Interface;
using System;
using System.Collections.Generic;

namespace Padaria.Domain.Model
{
    public class Venda :IBaseEntity
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        public int ComandaId { get; set; }
        public Comanda Comanda { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal ValorTotal { get; set; }
        public FormaDePagamentoEnum FormaDePagamento { get; set; }
        public decimal? Dinheiro { get; set; }
        public decimal? Troco { get; set; }
        public StatusDaVendaEnum StatusDaVenda { get; set; }
        public int CaixaId { get; set; }
        public Caixa Caixa { get; set; }

    }
}
