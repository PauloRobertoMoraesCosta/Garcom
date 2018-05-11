using Dapper;
using Garcom.Dominio.Entidade.Models;
using Garcom.Infra.DBEscrita;
using Garcom.Infra.DBLeitura;
using System.Collections.Generic;

namespace Garcom.Infra.Repositorio
{
    public class RepositorioProdutosIngredienteTamanhoValor : RepositorioBase<ProdutoIngredienteTamanhoValor>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="dbEscrita"></param>
        /// <param name="dbLeitura"></param>
        public RepositorioProdutosIngredienteTamanhoValor(ContextoEscrita dbEscrita, ContextoLeitura dbLeitura)
            : base(dbEscrita, dbLeitura)
        {

        }

        /// <summary>
        /// Método responsável por listar o Produto Ingrediente Tamanho Produto pelo id do Tamanho Produto
        /// </summary>
        /// <param name="tamanhoId"></param>
        /// <returns></returns>
        public IEnumerable<int> ListarPorTamanhoId(int tamanhoId)
        {
            return _dbLeitura.MySqlConnection.Query<int>("SELECT id FROM produto_ingrediente_tamanho_valor WHERE Excluido = 0 AND TamanhoProdutoId = " + tamanhoId);
        }

        public IEnumerable<int> ListarTodosIdsProdutoIngredienteTamanhoExcluido()
        {
            return _dbLeitura.MySqlConnection.Query<int>("SELECT Id FROM produto_ingrediente_tamanho_valor WHERE Excluido = 1");
        }
    }
}
