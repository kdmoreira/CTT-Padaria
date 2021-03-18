using System.Collections.Generic;

namespace CTT_Padaria.API.Dto
{
    public class CaixaTotalDto
    {
        public decimal ValorTotal { get; set; }
        public List<CaixasDto> Caixas { get; set; }
    }
}
