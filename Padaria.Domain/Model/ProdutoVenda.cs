using Padaria.Domain.Enum;
using Padaria.Domain.Interface;

namespace Padaria.Domain.Model
{
    public class ProdutoVenda : IBaseEntity
    {
        public int Id { get; set; }
        public int VendaId { get; set; }
        public Venda Venda { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public float Quantidade { get; set; }
        public float PrecoUnitario { get; set; }
        public float PrecoTotal { get; set; }
        public int UsarioId { get; set; }
        public Usuario Usario { get; set; }
        public FormaDePagamentoEnum FormaDePagamento { get; set; }
    }
}
