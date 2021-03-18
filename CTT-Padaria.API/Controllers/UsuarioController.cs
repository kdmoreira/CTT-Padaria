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
    [Authorize(Roles = "Administrador")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _repoUsuario;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioRepository repoUsuario, IMapper mapper)
        {
            _repoUsuario = repoUsuario;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string nome, string email)
        {
            try
            {
                if (nome != null)
                {
                    var usuariosNome = _repoUsuario.SelecionarPorNome(nome);
                    return Ok(_mapper.Map<IEnumerable<UsuarioDto>>(usuariosNome));
                }

                if (email != null)
                {
                    //var usuarioEmail = _repoUsuario.SelecionarPorEmail(email);
                    //return Ok(_mapper.Map<UsuarioDto>(usuarioEmail));
                    var usuarioEmail = _repoUsuario.SelecionarPorEmail(email);
                    if (usuarioEmail == null)
                        return BadRequest("Não existem usuários com este email.");
                    return Ok(_mapper.Map<UsuarioDto>(usuarioEmail));   
                }

                var usuarios = _repoUsuario.SelecionarTudo();
                return Ok(_mapper.Map<IEnumerable<UsuarioDto>>(usuarios));

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
