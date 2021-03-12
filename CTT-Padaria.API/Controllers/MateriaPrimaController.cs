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
        public IActionResult Get()
        {
            try
            {
                var usuarios = _repoUsuario.SelecionarTudo();
                if (usuarios.Count < 1)
                    return NoContent();

                return Ok(usuarios);

            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("nome/{nome}")]
        public IActionResult GetByName(string nome)
        {
            try
            {
                var usuarios = _repoUsuario.SelecionarPorNome(nome);
                if (usuarios.Count < 1)
                    return BadRequest("Não existem usuários com esse nome.");

                return Ok(usuarios);

            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("email/{email}")]
        public IActionResult GetByEmail(string email)
        {
            try
            {
                var usuario = _repoUsuario.SelecionarPorEmail(email);
                if (usuario == null)
                    return BadRequest("Não existe usuário com esse email.");

                return Ok(usuario);
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
                if(string.IsNullOrEmpty(usuario.Nome) || string.IsNullOrEmpty(usuario.Email) || string.IsNullOrEmpty(usuario.Perfil) 
                    || string.IsNullOrEmpty(usuario.Senha) ||  usuario.DataNascimento == null)
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
