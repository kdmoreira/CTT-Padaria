using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTT_Padaria.API.Dto
{
    public class ComandaDto
    {
        public string Comanda { get; set; }
        public string Data { get; set; }
        public decimal ValorTotal { get; set; }        
        public  List<ProdutosComandaDto> Produtos { get; set; }
    }
}
