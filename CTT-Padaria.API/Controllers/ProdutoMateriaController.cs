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
    [Authorize(Roles = "Administrador,Estoquista,Padeiro")]
    public class ProdutoMateriaController : ControllerBase
    {
        private readonly IProdutoMateriaRepository _repoProdutoMateria;
        private readonly IMapper _mapper;

        public ProdutoMateriaController(IProdutoMateriaRepository repoProdutoMateria, IMapper mapper)
        {
            _repoProdutoMateria = repoProdutoMateria;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna todas as receitas (ProdutoMateria).
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///     GET /api/ProdutoMateria
        /// </remarks>
        /// <response code="200">Retorna as receitas.</response>
        /// <response code="204">Vínculo não existe.</response>
        /// <response code="500">Erro interno no Servidor.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var produtosMaterias = _repoProdutoMateria.SelecionarTudoCompleto();
                if (produtosMaterias.Count < 1)
                    return NoContent();

                return Ok(_mapper.Map<IEnumerable<ProdutoMateriaDto>>(produtosMaterias));

            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Vincula um produto a uma matéria-prima.
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///     POST /api/ProdutoMateria
        ///     
        ///         {
        ///             "produtoId" : 7,
        ///             "materiaPrimaId" : 6,
        ///             "quantidade" : 2,
        ///             "porcao": 1
        ///         }
        /// </remarks>
        /// <response code="201">Vincula um produto a uma matéria-prima.</response>
        /// <response code="400">Esse produto já está vinculado a esta matéria prima.</response>
        /// <response code="500">Erro interno no Servidor.</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpPost]
        public IActionResult Post([FromBody] ProdutoMateria produtoMateria)
        {
            try
            {
                var produtoMaterias = _repoProdutoMateria.SelecionarPorProdutoIdMateriaId(produtoMateria.ProdutoId, produtoMateria.MateriaPrimaId);
                if (produtoMaterias != null)
                    return BadRequest("Esse produto já está vinculado a esta matéria prima.");

                _repoProdutoMateria.Incluir(produtoMateria);

                return Created("","Receita (ProdutoMateria) cadastrada com sucesso.");
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Altera um vínculo de produto e matéria-prima.
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///     PUT /api/ProdutoMateria
        ///     
        ///         {
        ///             "produtoId" : 7,
        ///             "materiaPrimaId" : 6,
        ///             "quantidade" : 1,
        ///             "porcao": 1
        ///         }
        /// </remarks>
        /// <response code="200">Alterações realizadas com sucesso.</response>
        /// <response code="204">Este vínculo não existe.</response>
        /// <response code="500">Erro interno no Servidor.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        [HttpPut]
        public IActionResult Put([FromBody] ProdutoMateria produtoMateria)
        {
            try
            {                
                var produtoMateriaAlterado = _repoProdutoMateria.Alterar(produtoMateria);

                if (produtoMateriaAlterado == null)
                    return NoContent();

                return Ok("Alterações realizadas com sucesso.");
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Deleta um vínculo de produto e matéria-prima.
        /// </summary>
        /// <param name="produtoId">Identificador do produto.</param>
        /// <param name="materiaPrimaId">Identificador da matéria-prima.</param>
        /// <remarks>
        /// Exemplo de request:
        ///     DELETE /api/ProdutoMateria
        /// </remarks>
        /// <response code="200">Matéria foi desvinculada do produto.</response>
        /// <response code="400">Esse produto não está vinculado a esta matéria prima.</response>
        /// <response code="500">Erro interno no Servidor.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [HttpDelete("{produtoId}/{materiaPrimaId}")]
        public IActionResult Delete(int produtoId, int materiaPrimaId)
        {
            try
            {
                var produtoMaterias = _repoProdutoMateria.SelecionarPorProdutoIdMateriaId(produtoId, materiaPrimaId);

                if (produtoMaterias == null)
                    return BadRequest("Esse produto não está vinculado a esta matéria prima.");

                _repoProdutoMateria.Excluir(produtoMaterias);

                return Ok("Matéria foi desvinculada do produto.");
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
