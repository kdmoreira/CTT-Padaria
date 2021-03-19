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
    [Authorize(Roles = "Administrador, Vendedor")]
    public class CaixaController : ControllerBase
    {
        private readonly ICaixaRepository _repoCaixa;
        private readonly IUsuarioRepository _repoUsuario;
        private readonly IMapper _mapper;

        public CaixaController(ICaixaRepository repoCaixa,
                               IUsuarioRepository repoUsuario,
                               IMapper mapper)
        {
            _repoCaixa = repoCaixa;
            _repoUsuario = repoUsuario;         
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna todos os caixas, podendo fazê-lo por data.
        /// </summary>
        /// <param name="data">Data de abertura do caixa.</param>
        /// <remarks>
        /// Exemplo de request:
        ///     GET /api/Caixa
        /// </remarks>
        /// <response code="200">Retorna todos os caixas.</response>
        /// <response code="204">Não existem caixas.</response>
        /// <response code="400">Não existe caixas registrados nesta data.</response>
        /// <response code="500">Erro interno no Servidor.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpGet]
        public IActionResult Get([FromQuery] DateTime data)
        {
            try
            {
                if (data.Date != new DateTime())
                {
                    var caixas = _repoCaixa.SelecionarPorData(data);
                    if (caixas.Count < 1)
                        return BadRequest("Não existe caixas registrados nesta data.");

                    var cxData = _mapper.Map<IEnumerable<CaixasDto>>(caixas);
                    var vTotalCaixas = new CaixaTotalDto()
                    {
                        ValorTotal = cxData.Sum(c => c.ValorTotal),
                        Caixas = cxData.ToList()
                    };

                    return Ok(vTotalCaixas);
                }

                var caixasTotal = _repoCaixa.SelecionarTudo();
                if (caixasTotal.Count < 1)
                    return NoContent();

                var cx = _mapper.Map<IEnumerable<CaixasDto>>(caixasTotal);
                var valorTotalCaixas = new CaixaTotalDto()
                {
                    ValorTotal = cx.Sum(c => c.ValorTotal),
                    Caixas = cx.ToList()
                };

                return Ok(valorTotalCaixas);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Retorna um caixa pelo Id.
        /// </summary>
        /// <param name="id">Identificador do caixa.</param>
        /// <remarks>
        /// Exemplo de request:
        ///     GET /api/Caixa/1
        /// </remarks>
        /// <response code="200">Retorna o caixa pelo Id.</response>
        /// <response code="204">Não existe caixa com este Id.</response>
        /// <response code="500">Erro interno no Servidor.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var caixa = _repoCaixa.Selecionar(id);
                if (caixa == null)
                    return NoContent();

                return Ok(_mapper.Map<CaixaDto>(caixa));
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Abrir caixa.
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///     POST /api/Caixa
        ///     
        ///         {
        ///             "usuarioId" : 1
        ///         }
        /// </remarks>
        /// <response code="200">Caixa aberto com sucesso.</response>
        /// <response code="204">Usuário não existe.</response>
        /// <response code="400">Já existe um caixa aberto ou o perfil do usuário não corresponde.</response>
        /// <response code="500">Erro interno no Servidor.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPost]
        public IActionResult Post([FromBody] Caixa caixa)
        {
            try
            {
                var usuario = _repoUsuario.Selecionar(caixa.UsuarioId);
                if (usuario == null)
                    return NoContent();

                var existeCaixaAberto = _repoCaixa.VerificaExisteCaixaAberto();
                if (existeCaixaAberto != null)
                    return BadRequest($"Caixa Id{existeCaixaAberto.Id} está aberto," +
                        $"não é permitido abrir o caixa sem fechar o anterior.");

                if (usuario.Perfil != "Vendedor" && usuario.Perfil != "Administrador")
                    return BadRequest("Perfil do usuário não corresponde ao de 'Vendedor' ou 'Administrador'.");

                caixa.Status = StatusDoCaixaEnum.Aberto;
                caixa.DataAbertura = DateTime.Now;
                caixa.DataFechamento = DateTime.Now;
                caixa.ValorTotal = 0;

                _repoCaixa.Incluir(caixa);

                return Ok($"Caixa {caixa.Id} Aberto.");
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Fechar caixa.
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///     PUT /api/Caixa
        ///     
        ///         {
        ///             "id" : 1,
        ///             "usuarioId" : 1
        ///         }
        /// </remarks>
        /// <response code="200">Caixa fechado com sucesso.</response>
        /// <response code="204">Caixa não existe.</response>
        /// <response code="400">Caixa não está aberto ou usuário não pertence a este caixa.</response>
        /// <response code="500">Erro interno no Servidor.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPut("FecharCaixa")]
        public IActionResult PutFecharCaixa(Caixa caixa)
        {
            try
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
                return Ok(_mapper.Map<CaixasDto>(statusCaixa));
            }
            catch (Exception)
            {

                return StatusCode(500);
            }
        }
    }
}
