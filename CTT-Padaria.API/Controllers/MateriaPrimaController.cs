using Microsoft.AspNetCore.Mvc;
using Padaria.Data.Repository.Interface;
using Padaria.Domain.Model;

namespace CTT_Padaria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    //[Authorize(Roles = "Administrador,Estoquista")]
    public class MateriaPrimaController : ControllerBase
    {
        private readonly IMateriaPrimaRepository _repoMateriaPrima;

        public MateriaPrimaController(IMateriaPrimaRepository repoMateriaPrima)
        {
            _repoMateriaPrima = repoMateriaPrima;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] bool inativas, string nome)
        {
            try
            {
                var materiasPrimas = _repoMateriaPrima.SelecionarTudo();
                if (materiasPrimas.Count < 1)
                    return NoContent();

                if (inativas == false && nome != null)
                    return Ok(_repoMateriaPrima.SelecionarPorNome(nome));

                if (inativas == true && nome != null)
                    return Ok(_repoMateriaPrima.SelecionarInativasPorNome(nome));

                if (inativas == true)
                    return Ok(_repoMateriaPrima.SelecionarInativas());

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

                var materiaPrimaExiste = _repoMateriaPrima.SelecionarPorNome(materiaPrima.Nome);
                if (materiaPrimaExiste.Count > 0)
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
