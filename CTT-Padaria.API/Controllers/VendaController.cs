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
    //[Authorize(Roles = "Administrador")]
    public class VendaController : ControllerBase
    {
        private readonly IVendaRepository _repoVenda;
        private readonly ICaixaRepository _repoCaixa;
        private readonly IComandaRepository _repoComanda;

        public VendaController(IVendaRepository repoVenda,
                               ICaixaRepository repoCaixa,
                               IComandaRepository repoComanda)
        {
            _repoVenda = repoVenda;
            _repoCaixa = repoCaixa;
            _repoComanda = repoComanda;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var vendas = _repoVenda.SelecionarTudo();
                if (vendas.Count < 1)
                    return NoContent();

                return Ok(vendas);

            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetByName(int id)
        {
            try
            {
                var venda = _repoVenda.Selecionar(id);
                if (venda == null)
                    return NoContent();
               
                return Ok(venda);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }


        [HttpPost]
        public IActionResult Post([FromBody] Venda venda)
        {
            try
            {
                var caixa = _repoCaixa.Selecionar(venda.CaixaId);
                if (caixa == null)
                    return NoContent();

                if (caixa.Status == StatusDoCaixaEnum.Fechado)
                    return BadRequest("Caixa fechado.");

                var comanda = _repoComanda.Selecionar(venda.ComandaId);
                if (comanda == null)
                    return BadRequest("Comanda Inválida");

                if (comanda.DataEntrada.Date < DateTime.Now.Date)
                    return BadRequest("Não é permitido uma venda com a data do dia anterior.");

                var vendaJaRealizada = _repoVenda.SelecionarComandaId(venda.ComandaId);
                if (vendaJaRealizada != null)
                    return BadRequest("Comanda ja registrada. Favor verificar o número da comanda.");

                venda.DataVenda = DateTime.Now;
                venda.ValorTotal = comanda.ProdutosComanda.Sum(pc => pc.PrecoTotal);               
                venda.StatusDaVenda = StatusDaVendaEnum.Realizada;

                if (venda.FormaDePagamento == FormaDePagamentoEnum.Dinheiro)
                {
                    if (venda.Dinheiro < venda.ValorTotal || venda .Dinheiro == null)
                        return BadRequest("Dinheiro insuficiente");

                    venda.Troco = venda.Dinheiro - venda.ValorTotal;
                }

                venda.UsuarioId = caixa.UsuarioId;
                _repoVenda.Incluir(venda);

                return Created("", venda);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }       
    }
}
