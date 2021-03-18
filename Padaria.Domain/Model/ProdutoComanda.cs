using Padaria.Domain.Interface;

namespace Padaria.Domain.Model
{
    public class ProdutoComanda : IBaseEntity
    {
        public int Id { get; set; }
        public int ComandaId { get; set; }
        public Comanda Comanda { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public float Quantidade { get; set; }       
        public decimal PrecoTotal { get; set; }               
    }
}
