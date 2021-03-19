namespace CTT_Padaria.API.Dto
{
    public class ProdutoMateriaDto
    {
        public string Produto { get; set; }
        public string MateriaPrima { get; set; }       
        public float Quantidade { get; set; }
        public int Porcao { get; set; }
    }
}
