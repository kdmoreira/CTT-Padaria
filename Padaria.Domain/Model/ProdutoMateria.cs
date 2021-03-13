using Padaria.Domain.Interface;

namespace Padaria.Domain.Model
{
    public class ProdutoMateria : IBaseEntity
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public int MateriaPrimaId { get; set; }
        public MateriaPrima MateriaPrima { get; set; }
        public float Quantidade { get; set; }
    }
}
