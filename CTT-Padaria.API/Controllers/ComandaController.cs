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
