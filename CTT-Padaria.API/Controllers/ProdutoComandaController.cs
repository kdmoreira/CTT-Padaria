using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Padaria.Data.Repository.Interface;
using Padaria.Domain.Model;
using System.Linq;

namespace CTT_Padaria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Authorize(Roles = "Administrador,Vendedor")]
    public class ProdutoComandaController : ControllerBase
    {
        private readonly IProdutoComandaRepository _repoProdutoComanda;
        private readonly IProdutoRepository _repoProduto;
        private readonly IComandaRepository _repoComanda;

        public ProdutoComandaController(IProdutoComandaRepository repoProdutoComanda,
                                      IProdutoRepository repoProduto,
                                      IComandaRepository repoComanda)
        {
            _repoProdutoComanda = repoProdutoComanda;
            _repoProduto = repoProduto;
            _repoComanda = repoComanda;
        }



        //[HttpGet("{id}")]
        //public IActionResult GetById(int id)
        //{
        //    try
        //    {
        //        var produtoComanda = _repoProdutoComanda.SelecionarPorComandaId(id);
        //        if (produtoComanda == null)
        //            return NoContent();

        //        return Ok(produtoComanda);

        //    }
        //    catch (System.Exception)
        //    {

        //        return StatusCode(500);
        //    }
        //}


        [HttpPost]
        public IActionResult Post([FromBody] ProdutoComanda produtoComanda)
        {
            try
            {
                var produto = _repoProduto.Selecionar(produtoComanda.ProdutoId);
                if (produto == null)
                    return NoContent();

                if (!produto.Ativo)
                    return BadRequest("O produto não está mais ativo");

                var comanda = _repoComanda.Selecionar(produtoComanda.ComandaId);
                if (comanda == null)
                    return NoContent();

                if (produtoComanda.Quantidade <= 0)
                    return BadRequest("Quantidade deve ser superior a 0.");

                if (produto.Quantidade < produtoComanda.Quantidade)
                    return BadRequest($"A quantidade solicitada não tem no estoque. Contém no estoque {produto.Quantidade}");
                                
                var comandaProdutoExiste = _repoProdutoComanda.SelecionarPorComandaIdProdutoId(produtoComanda.ComandaId, produtoComanda.ProdutoId);
                if (comandaProdutoExiste != null)
                {
                    produto.Quantidade -= produtoComanda.Quantidade;
                    produtoComanda.Quantidade += comandaProdutoExiste.Quantidade;
                    produtoComanda.PrecoTotal = (decimal)(produtoComanda.Quantidade * produto.Valor);
                    _repoProdutoComanda.Alterar(produtoComanda);                                   
                    
                    return Ok("Comanda atualizada com sucesso.");
                }

                produto.Quantidade -= produtoComanda.Quantidade;
                _repoProduto.Alterar(produto);
                 
                produtoComanda.PrecoTotal = (decimal)(produto.Valor * produtoComanda.Quantidade); 
                _repoProdutoComanda.Incluir(produtoComanda);               

                return Created("","Produto foi adicionado com sucesso.");
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
                var produtoComandaExiste = _repoProdutoComanda.Selecionar(id);
                if (produtoComandaExiste == null)
                    return NoContent();

                _repoProdutoComanda.Excluir(produtoComandaExiste);

                return Ok("Usuário removido com sucesso.");
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
