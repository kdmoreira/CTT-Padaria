using Padaria.Data.Contexto;
using Padaria.Data.Repository.Interface;
using Padaria.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace Padaria.Data.Repository.Implementation
{
    public class MateriaPrimaRepository : BaseRepository<MateriaPrima>, IMateriaPrimaRepository
    {
        public MateriaPrimaRepository(PadariaContexto contexto) : base(contexto) { }

        public List<MateriaPrima> SelecionarPorNome(string nome)
        {
            return _contexto.MateriasPrimas.Where(x => x.Nome.Contains(nome) && x.Ativa == true).ToList();
        }

        public List<MateriaPrima> SelecionarInativas()
        {
            return _contexto.MateriasPrimas
                .OrderBy(x => x.Nome)
                .ToList();
        }

        public List<MateriaPrima> SelecionarInativasPorNome(string nome)
        {
            return _contexto.MateriasPrimas
                .Where(x => x.Nome.Contains(nome))
                .OrderBy(x => x.Nome)
                .ToList();
        }

        public MateriaPrima SelecionarMateriaPrimaPorNome(string nome)
        {
            return _contexto.MateriasPrimas
                .FirstOrDefault(x => x.Nome.Equals(nome));
        }

        public override List<MateriaPrima> SelecionarTudo()
        {
            return _contexto.MateriasPrimas
                .Where(x => x.Ativa == true)
                .OrderBy(x => x.Nome)
                .ToList();
        }

        public MateriaPrima AlterarMateriaPrima(MateriaPrima materiaPrima)
        {
            var materiaPrimaEncontrada = Selecionar(materiaPrima.Id);

            if (materiaPrimaEncontrada == null)
                return null;

            if (materiaPrima.Quantidade > 0)
            {
                materiaPrima.Quantidade += materiaPrimaEncontrada.Quantidade;
                _contexto.Entry(materiaPrimaEncontrada).CurrentValues.SetValues(materiaPrima);               
                _contexto.SaveChanges();
            }

            return materiaPrima;
        }

        public MateriaPrima ValidarInativacao(MateriaPrima materiaPrima)
        {
            var materiaPrimaEncontrada = Selecionar(materiaPrima.Id);

            // Impedir que a matéria prima seja desativada se estiver ligada a um produto final ativo
            if (materiaPrima.Ativa == false && materiaPrimaEncontrada.Ativa == true)
            {
                var receita = _contexto.ProdutosMaterias.Where(x => x.MateriaPrimaId.Equals(materiaPrimaEncontrada.Id)).ToList();

                foreach (ProdutoMateria pm in receita)
                {
                    var produtoEncontrado = _contexto.Produtos.FirstOrDefault(x => x.Id.Equals(pm.ProdutoId));

                    if (produtoEncontrado.Ativo == true)
                    {
                        return null;
                    }
                }
            }
            return materiaPrima;
        }
    }
}
