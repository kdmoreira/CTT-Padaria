using AutoMapper;
using CTT_Padaria.API.Dto;
using Microsoft.AspNetCore.Mvc;
using Padaria.Data.Repository.Interface;
using Padaria.Domain.Model;
using System;
using System.Collections.Generic;

namespace CTT_Padaria.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    //[Authorize(Roles = "Administrador,Estoquista,Padeiro")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _repoProduto;
        private readonly ICaixaRepository _repoCaixa;
        private readonly IMapper _mapper;

        public ProdutoController(IProdutoRepository repoProduto,
                                 ICaixaRepository repoCaixa,  
                                 IMapper mapper)
        {
            _repoProduto = repoProduto;
            _repoCaixa = repoCaixa;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get([FromQuery]bool inativos, string nome)
        {
            try
            {
                if (inativos == false && nome != null)
                {
                    var produto = _repoProduto.SelecionarPorNome(nome);
                    if (produto.Count < 1)
                        return NoContent();
                    return Ok(_mapper.Map<IEnumerable<ProdutoDto>>(produto));
                }

                if (inativos == true && nome != null)
                {
                    var produto = _repoProduto.SelecionarInativosPorNome(nome);
                    if (produto.Count < 1)
                        return NoContent();
                    return Ok(_mapper.Map<IEnumerable<ProdutoDto>>(produto));
                }

                if (inativos == true)
                {
                    var produto = _repoProduto.SelecionarInativos();
                    if (produto.Count < 1)
                        return NoContent();
                    return Ok(_mapper.Map<IEnumerable<ProdutoDto>>(produto));
                }

                var produtos = _repoProduto.SelecionarTudo();
                if (produtos.Count < 1)
                    return NoContent();

                return Ok(_mapper.Map<IEnumerable<ProdutoDto>>(produtos));                
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

                    var produtoExiste = _repoProduto.SelecionarProdutoPorNome(produto.Nome);

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
                if (produtoEncontrado == null)
                    return NoContent();

                if (produtoEncontrado.Producao == 0 &&
                    produto.Quantidade > 0)
                {
                    var producao = _repoProduto.Produzir(produtoEncontrado, produto.Quantidade);
                    if (producao == null)
                        return BadRequest("Matéria prima insuficiente para esta quantidade de produto.");
                }

                produto.Quantidade += produtoEncontrado.Quantidade;
                var produtoAlterado = _repoProduto.Alterar(produto);
                return Ok("Produto alterado com sucesso.");

            }
            catch (System.Exception ex)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("descarte")]
        public IActionResult PutDescarte()
        {
            try
            {
                DateTime data = DateTime.Now;

                var caixaAberto = _repoCaixa.VerificaExisteCaixaAbertoPorData(data);
                if (caixaAberto != null)
                    return BadRequest($"O caixa Id{caixaAberto.Id} " +
                        $"está aberto. Para descartar os produtos, todos os caixas devem estar fechados.");

                var produtosDescartados = _repoProduto.SelecionarProdutosProprios();
                _repoProduto.DescarteProduzidos();

                var pd = produtosDescartados;
                return Ok(_mapper.Map<IEnumerable<ProdutoDescarteDto>>(pd));

            }
            catch (System.Exception)
            {
                return StatusCode(500);
            }
        }


    }
}
