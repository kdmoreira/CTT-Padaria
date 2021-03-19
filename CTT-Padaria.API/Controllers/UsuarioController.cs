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


        /// <summary>
        /// Retorna todos os usuários.
        /// </summary>
        /// <param name="nome">Nome do usuário.</param>
        /// <param name="email">Email do usuário.</param>
        /// <remarks>
        /// Exemplo de request:
        ///     GET /api/Usuario
        /// </remarks>
        /// <response code="200">Retorna o(s) usuário(s).</response>
        /// <response code="500">Erro interno no Servidor.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
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

        /// <summary>
        /// Cadastrar um usuário.
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///     POST /api/Usuario
        ///     
        ///         {
        ///             "nome" : "Caroline",
        ///             "email" : "carol@gmail.com",
        ///             "dataNascimento" : "1991-10-18",
        ///             "perfil": "Estoquista",
        ///             "senha": 123
        ///         }
        /// </remarks>
        /// <response code="201">Usuário cadastrado com sucesso.</response>
        /// <response code="400">Todos os campos são obrigatórios.</response>
        /// <response code="500">Erro interno no Servidor.</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
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

        /// <summary>
        /// Altera um usuário.
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        ///     PUT /api/Usuario
        ///     
        ///         {
        ///             "id" : 5,
        ///             "nome" : "Caroline",
        ///             "email" : "carol@gmail.com",
        ///             "dataNascimento" : "1991-10-18",
        ///             "perfil": "Estoquista",
        ///             "senha": 123456
        ///         }
        /// </remarks>
        /// <response code="200">Usuário alterado com sucesso.</response>
        /// <response code="204">Usuário não existe.</response>
        /// <response code="500">Erro interno no Servidor.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
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

        /// <summary>
        /// Deleta um usuário.
        /// </summary>
        /// <param name="id">Identificador do usuário.</param>
        /// <remarks>
        /// Exemplo de request:
        ///     DELETE /api/Usuario/5
        /// </remarks>
        /// <response code="200">Usuário removido com sucesso.</response>
        /// <response code="204">Usuário não existe.</response>
        /// <response code="500">Erro interno no Servidor.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
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
