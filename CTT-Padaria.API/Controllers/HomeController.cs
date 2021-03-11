using CTT_Padaria.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Padaria.Data.Repository.Interface;
using Padaria.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTT_Padaria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IUsuarioRepository _repoUsuario;

        public HomeController(IUsuarioRepository repoUsuario)
        {
            _repoUsuario = repoUsuario;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] Usuario usuario)
        {
            try
            {
                if (string.IsNullOrEmpty(usuario.Nome) || string.IsNullOrEmpty(usuario.Senha))
                    return BadRequest("Nome e/ou senha não devem ser nulos.");

                var usuarioExiste = _repoUsuario.SelecionarPorNomeESenha(usuario.Nome, usuario.Senha);
                if (usuarioExiste == null)
                    return NotFound("Nome e/ou senha inválidos.");

                var token = TokenService.GerarToken(usuarioExiste);

                return Ok(token);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
