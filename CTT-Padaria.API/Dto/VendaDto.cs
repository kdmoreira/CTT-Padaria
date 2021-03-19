namespace CTT_Padaria.API.Dto
{
    public class VendaDto
    {
        public int Comanda { get; set; }
        public string Usuario { get; set; }        
        public string DataVenda { get; set; }
        public string FormaDePagamento { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal? ValorPago { get; set; } = null;
        public decimal? Troco { get; set; } = null;
        public string Status { get; set; }

    }
}
