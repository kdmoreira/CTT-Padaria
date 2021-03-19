namespace CTT_Padaria.API.Dto
{
    public class MateriaPrimaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string UnidadeDeMedida { get; set; }
        public float Quantidade { get; set; }
        public bool Ativa { get; set; }
    }
}
