using System.Collections.Generic;

namespace CTT_Padaria.API.Dto
{
    public class ProdutoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string UnidadeDeMedida { get; set; }
        public string Producao { get; set; }
        public float Quantidade { get; set; }
        public bool Ativo { get; set; }
        public float Valor { get; set; }
        public List<MateriasProdutoDto> Materias { get; set; }
       
    }
}
