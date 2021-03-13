using Microsoft.AspNetCore.Mvc;
using Padaria.Data.Repository.Interface;
using Padaria.Domain.Model;

namespace CTT_Padaria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    //[Authorize(Roles = "Administrador")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _repoProduto;

        public ProdutoController(IProdutoRepository repoProduto)
        {
            _repoProduto = repoProduto;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var produtos = _repoProduto.SelecionarTudoCompleto();
                if (produtos.Count < 1)
                    return NoContent();

                return Ok(produtos);

            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] Produto produto)
        {
            try
            {
                if (produto.Producao == 0 && produto.Quantidade != 0)
                    return BadRequest("Para produtos de fabricação própria, a quantidade informada deve ser zero.");
                
                if (produto.Producao != 0 && (int)produto.Producao != 1)
                    return BadRequest($"Referência {produto.Producao} para produção não existe. Referências aceitas: 0(Próprio), 1(Terceirizado)");

                if ((int)produto.Producao == 1)
                {
                    if (produto.UnidadeDeMedida != 0 && 
                        (int)produto.UnidadeDeMedida != 1 && 
                        (int)produto.UnidadeDeMedida != 2)
                        return BadRequest($"Referência {produto.UnidadeDeMedida} para Unidade de Medida não existe. Referências aceitas: 0(Grama), 1(Mililitro) e 2(Unidade)");

                    var produtoExiste = _repoProduto.SelecionarPorNome(produto.Nome);

                    if (produtoExiste != null)
                        return BadRequest("Este produto já está cadastrado. Atualize, por favor.");

                    _repoProduto.Incluir(produto);
                    return Created("", "Produto cadastrado com sucesso.");
                }
                
                _repoProduto.Incluir(produto);

                return Created("","Produto cadastrado com sucesso.");
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] Produto produto)
        {
            try
            {
                var produtoEncontrado = _repoProduto.Selecionar(produto.Id);

                if (produtoEncontrado.Producao == 0 &&
                    produto.Quantidade != produtoEncontrado.Quantidade)
                {
                    if (_repoProduto.NaoPermiteAbater(produtoEncontrado, produto.Quantidade))
                        return BadRequest("Matéria prima insuficiente para esta quantidade de produto.");
                }
                
                // Incluir abatimento
                var produtoAlterado = _repoProduto.Alterar(produto);

                if (produtoAlterado == null)
                    return NoContent();

                return Ok("Produto alterado com sucesso.");
            }
            catch (System.Exception ex)
            {
                return StatusCode(500);
            }
        }

        // Não poderemos deletar produtos
        /* 
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var produtoExiste = _repoProduto.Selecionar(id);
                if (produtoExiste == null)
                    return NoContent();

                _repoProduto.Excluir(produtoExiste);

                return Ok("Produto removido com sucesso.");
            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        } */
    }
}
