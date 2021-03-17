using AutoMapper;
using CTT_Padaria.API.Dto;
using Microsoft.AspNetCore.Mvc;
using Padaria.Data.Repository.Interface;
using Padaria.Domain.Model;
using System.Collections.Generic;

namespace CTT_Padaria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    //[Authorize(Roles = "Administrador,Estoquista,Padeiro")]
    public class ProdutoMateriaController : ControllerBase
    {
        private readonly IProdutoMateriaRepository _repoProdutoMateria;
        private readonly IMapper _mapper;

        public ProdutoMateriaController(IProdutoMateriaRepository repoProdutoMateria, IMapper mapper)
        {
            _repoProdutoMateria = repoProdutoMateria;
            _mapper = mapper;
        }

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
            catch (System.Exception ex)
            {
                return StatusCode(500);
            }
        }


        [HttpPost]
        public IActionResult Post([FromBody] ProdutoMateria produtoMateria)
        {
            try
            {  
                _repoProdutoMateria.Incluir(produtoMateria);

                return Created("","Receita (ProdutoMateria) cadastrada com sucesso.");
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] ProdutoMateria produtoMateria)
        {
            try
            {                
                var produtoMateriaAlterado = _repoProdutoMateria.Alterar(produtoMateria);

                if (produtoMateriaAlterado == null)
                    return NoContent();

                return Ok("Usuário alterado com sucesso.");
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var produtoMateriaExiste = _repoProdutoMateria.Selecionar(id);
                if (produtoMateriaExiste == null)
                    return NoContent();

                _repoProdutoMateria.Excluir(produtoMateriaExiste);

                return Ok("Usuário removido com sucesso.");
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
