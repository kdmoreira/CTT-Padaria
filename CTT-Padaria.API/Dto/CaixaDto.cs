using System.Collections.Generic;

namespace CTT_Padaria.API.Dto
{
    public class CaixaDto
    {
        public int Caixa { get; set; }
        public string Usuario { get; set; }
        public string DataAbertura { get; set; }
        public string DataFechamento { get; set; }
        public decimal ValorTotal { get; set; }
        public string Status { get; set; }
        public List<VendaDto> Vendas { get; set; }
    }
}
