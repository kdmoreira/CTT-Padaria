using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Padaria.Data.Repository.Interface;
using Padaria.Domain.Enum;
using Padaria.Domain.Model;
using System;
using System.Linq;

namespace CTT_Padaria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    //[Authorize(Roles = "Administrador, Vendedor")]
    public class CaixaController : ControllerBase
    {
        private readonly ICaixaRepository _repoCaixa;
        private readonly IUsuarioRepository _repoUsuario;
        private readonly IProdutoRepository _repoProduto;

        public CaixaController(ICaixaRepository repoCaixa,
                               IUsuarioRepository repoUsuario,
                               IProdutoRepository repoProduto)
        {
            _repoCaixa = repoCaixa;
            _repoUsuario = repoUsuario;
            _repoProduto = repoProduto;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] DateTime data)
        {
            try
            {
                if(data.Date != new DateTime(0000-00-00))
                {
                    var caixas = _repoCaixa.SelecionarPorData(data);
                    if (caixas.Count < 1)
                        return BadRequest("Não existe caixas registrados nesta data.");
                    
                    var valorVendas = caixas.Sum(c => c.ValorTotal);

                    return Ok($"Valor Total de vendas referente a {data.ToString("dd/MM/yyyy")} foi de R${valorVendas}.");
                }

                var caixasTotal = _repoCaixa.SelecionarTudo();
                if (caixasTotal.Count < 1)
                    return NoContent();

                var valorVendasTotal = caixasTotal.Sum(c => c.ValorTotal);

                return Ok($"Valor Total de todas as vendas foi de R${valorVendasTotal}.");


            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromQuery] DateTime data, int id)
        {
            try
            {
                var caixa = _repoCaixa.Selecionar(id);
                if (caixa == null)
                    return NoContent();

                if (data.Date != new DateTime())
                {
                    var caixaDataVenda = caixa.Vendas.Where(v => v.DataVenda.Date == data);
                    var ValorTotal = caixaDataVenda.Sum(v => v.ValorTotal);
                    
                    return Ok($"Valor referente as vendas do caixa Id: {caixa.Id} na data {data} " +
                        $"foi de R$:{ValorTotal} {caixaDataVenda}");
                }

                return Ok(caixa);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Caixa caixa)
        {
            try
            {
                var existeCaixaAberto = _repoCaixa.VerificaExisteCaixaAberto();
                if (existeCaixaAberto != null)
                    return BadRequest($"Caixa Id{existeCaixaAberto.Id} está aberto," +
                        $"não é permitido abrir o caixa sem fechar o anterior.");

                var usuario = _repoUsuario.Selecionar(caixa.UsuarioId);
                if (usuario.Perfil != "Vendedor" && usuario.Perfil != "Administrador")
                    return BadRequest("Perfil do usuário não corresponde ao de 'Vendedor' ou 'Administrador'.");

                caixa.Status = StatusDoCaixaEnum.Aberto;
                caixa.DataAbertura = DateTime.Now;
                caixa.DataFechamento = DateTime.Now;
                caixa.ValorTotal = 0;

                _repoCaixa.Incluir(caixa);

                return Ok("Caixa Aberto.");                
            }
            catch (System.Exception ex)
            {
                return StatusCode(500);
            }
        }


        [HttpPut("FecharCaixa")]
        public IActionResult PutFecharCaixa(Caixa caixa)
        {
            var statusCaixa = _repoCaixa.Selecionar(caixa.Id);
            if (statusCaixa == null)
                return NoContent();

            if (statusCaixa.Status == StatusDoCaixaEnum.Fechado)
                return BadRequest($"Caixa Id:{statusCaixa.Id} não esta aberto.");

            if (statusCaixa.UsuarioId != caixa.UsuarioId)
                return BadRequest("Usuário não pertence a este caixa");

            statusCaixa.Status = StatusDoCaixaEnum.Fechado;
            statusCaixa.DataFechamento = DateTime.Now;
            statusCaixa.ValorTotal = statusCaixa.Vendas.Sum(v => v.ValorTotal);

            _repoCaixa.Alterar(statusCaixa);

            //DateTime data = DateTime.Now.Date;
            //var vendaData = statusCaixa.Vendas.FindAll(v => v.DataVenda.Date == data);
            //var ValorDiario = vendaData.Sum(v => v.ValorTotal);

           // _repoProduto.DescarteProduzidos();
            return Ok($"Caixa Fechado, Valor de vendas diária R$:{statusCaixa.ValorTotal}");

        }


    }
}
