using AutoMapper;
using CTT_Padaria.API.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Padaria.Data.Repository.Interface;
using Padaria.Domain.Model;
using System.Collections.Generic;

namespace CTT_Padaria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]    
    public class MateriaPrimaController : ControllerBase
    {
        private readonly IMateriaPrimaRepository _repoMateriaPrima;
        private readonly IMapper _mapper;

        public MateriaPrimaController(IMateriaPrimaRepository repoMateriaPrima, IMapper mapper)
        {
            _repoMateriaPrima = repoMateriaPrima;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna todas as matérias primas ativas.
        /// </summary>
        /// <param name="inativas">Opção para retornar inativas também (true).</param>
        /// <param name="nome">Nome da matéria-prima.</param>
        /// <remarks>
        /// Exemplo de request:
        ///     GET /api/MateriaPrima
        /// </remarks>
        /// <response code="200">Retorna as matérias-primas.</response>
        /// <response code="204">Não existem matérias primas cadastradas.</response>
        /// <response code="500">Erro interno no Servidor.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Administrador, Estoquista, Padeiro")]
        [HttpGet]
        public IActionResult Get([FromQuery] bool inativas, string nome)
        {
            try
            {
                var materiasPrimas = _repoMateriaPrima.SelecionarTudo();
                if (materiasPrimas.Count < 1)
                    return NoContent();

                if (inativas == false && nome != null)
                {
                    var resultado = _repoMateriaPrima.SelecionarPorNome(nome);
                    return Ok(_mapper.Map<IEnumerable<MateriaPrimaDto>>(resultado));
                }

                if (inativas == true && nome != null)
                {
                    var resultado = _repoMateriaPrima.SelecionarInativasPorNome(nome);
                    return Ok(_mapper.Map<IEnumerable<MateriaPrimaDto>>(resultado));
                }                  

                if (inativas == true)
                {
                    var resultado = (_repoMateriaPrima.SelecionarInativas());
                    return Ok(_mapper.Map<IEnumerable<MateriaPrimaDto>>(resultado));
                }                   

                return Ok(_mapper.Map<IEnumerable<MateriaPrimaDto>>(materiasPrimas));
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Retorna uma matéria-prima pelo Id.
        /// </summary>
        /// <param name="id">Identificador da matéria-prima.</param>
        /// <remarks>
        /// Exemplo de request:
        ///     GET /api/MateriaPrima/1
        /// </remarks>
        /// <response code="200">Retorna matéria-prima.</response>
        /// <response code="400">Não existe matéria-prima com esse Id.</response>
        /// <response code="500">Erro interno no Servidor.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Administrador, Estoquista, Padeiro")]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var materiaPrima = _repoMateriaPrima.Selecionar(id);
                if (materiaPrima == null)
                    return BadRequest("Não existe matéria prima com esse Id.");

                return Ok(_mapper.Map<MateriaPrimaDto>(materiaPrima));
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Cadastrar matéria-prima.
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///     POST /api/MateriaPrima
        ///         
        ///         {
        ///             "nome": "Leite Condensado Moça",
        ///             "unidadeDeMedida": 1,
        ///             "quantidade": 1000,
        ///             "ativa": false
        ///         }
        /// </remarks>
        /// <response code="201">Cadastra a matéria-prima.</response>
        /// <response code="400">Erro de validação.</response>
        /// <response code="500">Erro interno no Servidor.</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Administrador, Estoquista")]
        [HttpPost]
        public IActionResult Post([FromBody] MateriaPrima materiaPrima)
        {
            try
            {
                if (string.IsNullOrEmpty(materiaPrima.Nome) || materiaPrima.Quantidade < 0)
                    return BadRequest("Todos os campos são obrigatórios.");

                if (materiaPrima.UnidadeDeMedida != 0 &&
                    (int)materiaPrima.UnidadeDeMedida != 1 && 
                    (int)materiaPrima.UnidadeDeMedida != 2)
                    return BadRequest($"Referência {materiaPrima.UnidadeDeMedida} para Unidade de Medida não existe. Referências aceitas: 0(Grama), 1(Mililitro) e 2(Unidade)");

                var materiaPrimaExiste = _repoMateriaPrima.SelecionarMateriaPrimaPorNome(materiaPrima.Nome);
                if (materiaPrimaExiste != null)
                    return BadRequest("Esta matéria prima já está cadastrada. Atualize, por favor.");

                if (materiaPrima.Quantidade <= 0)
                    return BadRequest("Por favor, informar uma quantidade maior que zero.");

                _repoMateriaPrima.Incluir(materiaPrima);

                return Created("", "Matéria Prima cadastrada com sucesso.");
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Alterar matéria-prima.
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///     POST /api/MateriaPrima
        ///         
        ///         {
        ///             "id": 10,
        ///             "nome": "Leite Condensado Moça",
        ///             "unidadeDeMedida": 1,
        ///             "quantidade": 99,
        ///             "ativa": false
        ///         }
        /// </remarks>
        /// <response code="200">Altera a matéria-prima.</response>
        /// <response code="400">Erro de validação.</response>
        /// <response code="500">Erro interno no Servidor.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Authorize(Roles = "Administrador, Estoquista")]
        [HttpPut]
        public IActionResult Put([FromBody] MateriaPrima materiaPrima)
        {
            try
            {
                if (materiaPrima.Quantidade <= 0)
                    return BadRequest("Valores iguais ou menores que zero não permitidos.");

                if (materiaPrima.UnidadeDeMedida != 0 &&
                    (int)materiaPrima.UnidadeDeMedida != 1 &&
                    (int)materiaPrima.UnidadeDeMedida != 2)
                    return BadRequest($"Referência {materiaPrima.UnidadeDeMedida} para Unidade de Medida não existe. Referências aceitas: 0(Grama), 1(Mililitro) e 2(Unidade)");

                var materiaPrimaEncontrada = _repoMateriaPrima.Selecionar(materiaPrima.Id);
                if (materiaPrimaEncontrada == null)
                    return NoContent();

                var retorno = _repoMateriaPrima.ValidarInativacao(materiaPrima);
                if (retorno == null)
                    return BadRequest("Esta matéria prima está vinculada a um produto ativo.");

                _repoMateriaPrima.AlterarMateriaPrima(materiaPrima);

                return Ok("Matéria Prima alterada com sucesso.");
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
