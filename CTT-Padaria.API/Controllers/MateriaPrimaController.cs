using Microsoft.AspNetCore.Mvc;

namespace CTT_Padaria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    //[Authorize(Roles = "Administrador, estoquista")]
    public class MateriaPrimaController : ControllerBase
    {
        private readonly IMateriaPrimaRepository _repoMateriaPrima;

        public MateriaPrimaController(IMateriaPrimaRepository repoMateriaPrima)
        {
            _repoMateriaPrima = repoMateriaPrima;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var materiasPrimas = _repoMateriaPrima.SelecionarTudo();
                if (materiasPrimas.Count < 1)
                    return NoContent();

                return Ok(materiasPrimas);

            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var materiaPrima = _repoMateriaPrima.Selecionar(id);
                if (materiaPrima == null)
                    return BadRequest("Não existe matéria prima com esse Id.");

                return Ok(materiaPrima);

            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
