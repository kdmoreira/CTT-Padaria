using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Padaria.Data.Repository.Interface;
using Padaria.Domain.Model;

namespace CTT_Padaria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    //[Authorize(Roles = "Administrador")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _repoUsuario;

        public UsuarioController(IUsuarioRepository repoUsuario)
        {
            _repoUsuario = repoUsuario;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string nome, string email)
        {
            try
            { 
                if (nome != null && email != null)
                   return Ok(_repoUsuario.SelecionarPorNomeEmail(nome, email));

                if (nome != null)
                    return Ok(_repoUsuario.SelecionarPorNome(nome));

                if (email != null)
                    return Ok(_repoUsuario.SelecionarPorEmail(email));
                
                return Ok(_repoUsuario.SelecionarTudo());

            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Usuario usuario)
        {
            try
            {
                if(string.IsNullOrEmpty(usuario.Nome) || 
                    string.IsNullOrEmpty(usuario.Email) || 
                    string.IsNullOrEmpty(usuario.Perfil) || 
                    string.IsNullOrEmpty(usuario.Senha) ||  
                    usuario.DataNascimento == null)
                {
                    return BadRequest("Todos os campos são obrigatórios.");
                }

                _repoUsuario.Incluir(usuario);

                return Created("","Usuário cadastrado com sucesso.");
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Usuario usuario)
        {
            try
            {                
                var usuarioAlterado = _repoUsuario.Alterar(usuario);

                if (usuarioAlterado == null)
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
                var usuarioExiste = _repoUsuario.Selecionar(id);
                if (usuarioExiste == null)
                    return NoContent();

                _repoUsuario.Excluir(usuarioExiste);

                return Ok("Usuário removido com sucesso.");
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
