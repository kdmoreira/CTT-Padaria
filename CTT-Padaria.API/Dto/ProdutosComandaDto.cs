namespace CTT_Padaria.API.Dto
{
    public class ProdutosComandaDto
    {
        public string Produto { get; set; }
        public float Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal Valor { get; set; }
    }
}
