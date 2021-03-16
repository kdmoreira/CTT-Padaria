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
    public class ComandaController : ControllerBase
    {
        private readonly IComandaRepository _repoComanda;

        public ComandaController(IComandaRepository repoComanda)
        {
            _repoComanda = repoComanda;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var comandas = _repoComanda.SelecionarTudo();
                if (comandas.Count < 1)
                    return NoContent();

                return Ok(comandas);

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
                var comanda = _repoComanda.Selecionar(id);
                if (comanda == null)
                    return NoContent();
               
                return Ok(comanda);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]Comanda comanda)
        {
            try
            {
                comanda.DataEntrada = DateTime.Now;
                _repoComanda.Incluir(comanda);

                return Ok();
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }       
    }
}
