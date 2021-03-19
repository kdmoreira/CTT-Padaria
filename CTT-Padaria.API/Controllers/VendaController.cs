using AutoMapper;
using CTT_Padaria.API.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Padaria.Data.Repository.Interface;
using Padaria.Domain.Enum;
using Padaria.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CTT_Padaria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Authorize(Roles = "Administrador,Vendedor")]
    public class VendaController : ControllerBase
    {
        private readonly IVendaRepository _repoVenda;
        private readonly ICaixaRepository _repoCaixa;
        private readonly IComandaRepository _repoComanda;
        private readonly IMapper _mapper;

        public VendaController(IVendaRepository repoVenda,
                               ICaixaRepository repoCaixa,
                               IComandaRepository repoComanda,
                               IMapper mapper)
        {
            _repoVenda = repoVenda;
            _repoCaixa = repoCaixa;
            _repoComanda = repoComanda;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna todas as vendas.
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///     GET /api/Venda
        /// </remarks>
        /// <response code="200">Retorna todas as vendas.</response>
        /// <response code="204">Venda não existe.</response>
        /// <response code="500">Erro interno no Servidor.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var vendas = _repoVenda.SelecionarTudo();
                if (vendas.Count < 1)
                    return NoContent();

                return Ok(_mapper.Map<IEnumerable<VendaDto>>(vendas));

            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Retorna uma venda pelo identificador da comanda.
        /// </summary>
        /// <param name="id">Identificador da comanda.</param>
        /// <remarks>
        /// Exemplo de request:
        ///     GET /api/Venda/1
        /// </remarks>
        /// <response code="200">Retorna a venda.</response>
        /// <response code="204">Venda não existe.</response>
        /// <response code="500">Erro interno no Servidor.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var venda = _repoVenda.SelecionarComandaId(id);
                if (venda == null)
                    return NoContent();

                return Ok(_mapper.Map<VendaDto>(venda));
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Realizar venda.
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///     POST /api/Venda
        ///     
        ///         Dinheiro:
        ///         {
        ///             "comandaId" : 1,
        ///             "formaDePagamento" : 0,
        ///             "dinheiro": 5,
        ///             "caixaId": 1
        ///         }
        ///         
        ///         Cartão:
        ///         {
        ///             "comandaId" : 2,
        ///             "formaDePagamento" : 1,
        ///             "caixaId": 2
        ///         }
        /// </remarks>
        /// <response code="200">Realiza uma venda.</response>
        /// <response code="204">Id do caixa incorreto.</response>
        /// <response code="400">Erro de validação.</response>
        /// <response code="500">Erro interno no Servidor.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
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
                venda.UsuarioId = caixa.UsuarioId;


                if (venda.FormaDePagamento == FormaDePagamentoEnum.Dinheiro)
                {
                    if (venda.Dinheiro < venda.ValorTotal || venda.Dinheiro == null)
                        return BadRequest("Dinheiro insuficiente");

                    venda.Troco = venda.Dinheiro - venda.ValorTotal;
                }

                _repoVenda.Incluir(venda);

                return Ok(_mapper.Map<VendaDto>(venda));
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
