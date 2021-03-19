using Padaria.Domain.Enum;
using Padaria.Domain.Interface;
using System.Collections.Generic;

namespace Padaria.Domain.Model
{
    public class MateriaPrima : IBaseEntity
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public UnidadeDeMedidaEnum UnidadeDeMedida { get; set; }
        public float Quantidade { get; set; }
        public bool Ativa { get; set; }
        public List<ProdutoMateria> ProdutosMaterias { get; set; }
    }
}

