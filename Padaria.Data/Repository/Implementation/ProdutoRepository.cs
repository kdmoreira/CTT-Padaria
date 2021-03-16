using Microsoft.EntityFrameworkCore;
using Padaria.Data.Contexto;
using Padaria.Data.Repository.Interface;
using Padaria.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace Padaria.Data.Repository.Implementation
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(PadariaContexto contexto) : base(contexto) { }

        public List<Produto> SelecionarInativos()
        {
            return _contexto.Produtos
                .OrderBy(x => x.Nome)
                .ToList();
        }

        public List<Produto> SelecionarInativosPorNome(string nome)
        {
            return _contexto.Produtos
                .Where(x => x.Nome.Contains(nome))
                .OrderBy(x => x.Nome)
                .ToList();
        }

        public List<Produto> SelecionarPorNome(string nome)
        {
            return _contexto.Produtos
                .Where(x => x.Nome.Contains(nome) && x.Ativo == true)
                .ToList();
        }

        public Produto SelecionarProdutoPorNome(string nome)
        {
            return _contexto.Produtos
                .FirstOrDefault(x => x.Nome.Equals(nome));
        }

        public List<Produto> SelecionarTudoCompleto()
        {
            return _contexto.Produtos
                .Include(x => x.ProdutosMaterias).ToList();
        }

        public override List<Produto> SelecionarTudo()
        {
            return _contexto.Produtos
                .Where(x => x.Ativo == true)
                .OrderBy(x => x.Nome)
                .ToList();
        }

        public Produto Produzir(Produto produtoEncontrado, float quantidade)
        {
            var receita = _contexto.ProdutosMaterias.Where(x => x.ProdutoId.Equals(produtoEncontrado.Id)).ToList();

            float diferenca = quantidade - produtoEncontrado.Quantidade;
            List<float> quantidadesProporcionais = new List<float>();

            // Verifica se há alguma matéria prima com estoque insuficiente
            for (int k = receita.Count - 1; k >= 0; k--)
            {
                var materiaPrimaEncontrada = _contexto.MateriasPrimas.FirstOrDefault(x => x.Id.Equals(receita[k].MateriaPrimaId));

                if (materiaPrimaEncontrada.Ativa == false)
                {
                    receita.RemoveAt(k);
                }
                else
                { 
                    // Calcula a quantidade de matéria prima proporcional à quantidade a ser produzida
                    float x = (100 * diferenca) / receita[k].Porcao;
                    quantidadesProporcionais.Add(receita[k].Quantidade);
                    int indice = quantidadesProporcionais.Count();
                    quantidadesProporcionais[indice - 1] *= (x / 100);

                    if (materiaPrimaEncontrada.Quantidade - quantidadesProporcionais[indice - 1] < 0)
                    {
                        return null;
                    }
                }
            }

            // Caso haja estoque de matéria prima suficiente, a quantidade para a produção é abatida
            /*int i = 0;
            foreach (ProdutoMateria pm in receita)
            {
                var materiaPrimaEncontrada = _contexto.MateriasPrimas.FirstOrDefault(x => x.Id.Equals(pm.MateriaPrimaId));

                materiaPrimaEncontrada.Quantidade -= quantidadesProporcionais[i];
                //i++;
            } */

            int j = 4;
            for (int i = 0; i < receita.Count; i++)
            {
                var materiaPrimaEncontrada = _contexto.MateriasPrimas.FirstOrDefault(x => x.Id.Equals(receita[i].MateriaPrimaId));

                materiaPrimaEncontrada.Quantidade -= quantidadesProporcionais[j];
                j--;
            }
            return produtoEncontrado;
        }
        
    }
}
