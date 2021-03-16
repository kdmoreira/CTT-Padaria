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
    //[Authorize(Roles = "Administrador, Vendedor")]
    public class CaixaController : ControllerBase
    {
        private readonly ICaixaRepository _repoCaixa;
        private readonly IUsuarioRepository _repoUsuario;

        public CaixaController(ICaixaRepository repoCaixa,
                               IUsuarioRepository repoUsuario)
        {
            _repoCaixa = repoCaixa;
            _repoUsuario = repoUsuario;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var comandas = _repoCaixa.SelecionarTudo();
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
        public IActionResult GetById([FromQuery] DateTime data, int id)
        {
            try
            {
                var caixa = _repoCaixa.Selecionar(id);
                if (caixa == null)
                    return NoContent();

                if (data.Date != new DateTime())
                {
                    var caixaDataVenda = caixa.Vendas.Where(v => v.DataVenda.Date == data);
                    var ValorTotal = caixaDataVenda.Sum(v => v.ValorTotal);
                    //return Ok(caixa.Vendas.Where(v => v.DataVenda.Date == data));
                    return Ok($"Valor referente as vendas do caixa Id: {caixa.Id} na data {data} " +
                        $"foi de R$:{ValorTotal} {caixaDataVenda}");
                }

                return Ok(caixa);
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]Caixa caixa)
        {
            try
            {
                var usuario = _repoUsuario.Selecionar(caixa.UsuarioId);
                if (usuario == null)
                    return NoContent();

                var caixaExiste = _repoCaixa.SelecionarFuncionarioId(caixa.UsuarioId);
                if (caixaExiste != null)
                    return BadRequest("Usuário já pertence a um caixa");
                _repoCaixa.Incluir(caixa);

                return Ok("Caixa criado com sucesso.");
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("AbrirCaixa")]
        public IActionResult PutAbrirCaixa(Caixa caixa)
        {
            var statusCaixa = _repoCaixa.Selecionar(caixa.Id);
            if (statusCaixa == null)
                return NoContent();

            if (statusCaixa.UsuarioId != caixa.UsuarioId)
                return BadRequest("Usuário não pertence a este caixa");

            var caixaAberto = _repoCaixa.VerificaExisteCaixaAberto();
            if(caixaAberto != null)
                return BadRequest($"Caixa Id:{caixaAberto.Id} está aberto." +
                    $" não é permitido abrir o caixa sem fechar o anterior. ");
                       
            caixa.Status = StatusDoCaixaEnum.Aberto;
            _repoCaixa.Alterar(caixa);
            
            return Ok($"Caixa Aberto");

        }

        [HttpPut("FecharCaixa")]
        public IActionResult PutFecharCaixa(Caixa caixa)
        {
            var statusCaixa = _repoCaixa.Selecionar(caixa.Id);
            if (statusCaixa == null)
                return NoContent();

            if (statusCaixa.Status == StatusDoCaixaEnum.Fechado)
                return BadRequest($"Caixa Id:{statusCaixa.Id} não esta aberto.");

            if (statusCaixa.UsuarioId != caixa.UsuarioId)
                return BadRequest("Usuário não pertence a este caixa");

            caixa.Status = StatusDoCaixaEnum.Fechado;
            _repoCaixa.Alterar(caixa);
            DateTime data = DateTime.Now.Date;
            var vendaData = statusCaixa.Vendas.FindAll(v => v.DataVenda.Date == data);
            var ValorDiario = vendaData.Sum(v => v.ValorTotal);

            return Ok($"Caixa Fechado, Valor de vendas diária R$:{ValorDiario}");
            
        }

        
    }
}
