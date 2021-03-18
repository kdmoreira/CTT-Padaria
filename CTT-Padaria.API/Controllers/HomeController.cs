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

        /// <summary>
        /// Faz Login de um Usuário.
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///     Post /api/Home/login
        ///     
        ///         {
        ///             "nome" : "Thaise",
        ///             "senha" : 123
        ///         }
        /// </remarks>
        /// <response code="200">Login realizado com sucesso.</response>
        /// <response code="400">Nome e/ou senha não devem ser nulos.</response>
        /// <response code="404">Nome e/ou senha inválidos.</response>
        /// <response code="500">Erro interno no Servidor.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
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
