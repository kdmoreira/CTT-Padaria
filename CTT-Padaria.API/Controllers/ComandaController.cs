using AutoMapper;
using CTT_Padaria.API.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Padaria.Data.Repository.Interface;
using Padaria.Domain.Model;
using System;

namespace CTT_Padaria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Authorize(Roles = "Administrador,Vendedor")]
    public class ComandaController : ControllerBase
    {
        private readonly IComandaRepository _repoComanda;
        private readonly IMapper _mapper;

        public ComandaController(IComandaRepository repoComanda, IMapper mapper)
        {
            _repoComanda = repoComanda;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna uma comanda pelo Id.
        /// </summary>
        /// <param name="id">Identificador da comanda.</param>
        /// <remarks>
        /// Exemplo de request:
        ///     GET /api/Comanda/1
        /// </remarks>
        /// <response code="200">Retorna a comanda pelo Id.</response>
        /// <response code="204">Não existe comanda com este Id.</response>
        /// <response code="500">Erro interno no Servidor.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var comanda = _repoComanda.Selecionar(id);
                if (comanda == null)
                    return NoContent();

                return Ok(_mapper.Map<ComandaDto>(comanda));
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Criar comanda.
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///     POST /api/Comanda
        ///      
        ///         {
        ///             
        ///         }
        /// </remarks>
        /// <response code="200">Retorna o identificador da comanda.</response>
        /// <response code="500">Erro interno no Servidor.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        [HttpPost]
        public IActionResult Post([FromBody]Comanda comanda)
        {
            try
            {
                comanda.DataEntrada = DateTime.Now;
                _repoComanda.Incluir(comanda);

                return Ok($"Número da comanda: {comanda.Id}");
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }       
    }
}
