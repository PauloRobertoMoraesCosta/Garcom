using Dapper;
using Garcom.Dominio.Entidade.Models;
using Garcom.Dominio.Entidade.Models.Auditoria;
using Garcom.Infra.DBEscrita;
using Garcom.Infra.DBLeitura;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Garcom.Infra.Repositorio
{
    /// <summary>
    /// Repositorio da classe TamanhoProduto
    /// </summary>
    public class RepositorioTamanhoProduto : RepositorioBase<TamanhoProduto>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="dbEscrita"></param>
        /// <param name="dbLeitura"></param>
        public RepositorioTamanhoProduto(ContextoEscrita dbEscrita, ContextoLeitura dbLeitura)
            :base(dbEscrita, dbLeitura)
        {

        }
        /// <summary>
        /// Método responsável por alterar o tamanho de produto
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns>Retorna os atributos que foram alterados</returns>
        public IEnumerable<ChangeLog> AlterarComAuditoria(TamanhoProduto entidade)
        {
            _dbEscrita.Set<TamanhoProduto>().Attach(entidade);
            _dbEscrita.Entry(entidade).State = EntityState.Modified;
            var alteracoes = _dbEscrita.RetornaModificacoes();
            
            return alteracoes;
        }

        /// <summary>
        /// Método responsável por listar todos os tamanhos de produtos cadastrados
        /// </summary>
        /// <returns></returns>
        public ICollection<TamanhoProduto> ListarTodos()
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT Id, Nome, Ativo FROM tamanho_produto");
            sql.Append("WHERE Excluido = 0");

            var tamanhosProdutos = _dbLeitura.MySqlConnection
                                             .Query<TamanhoProduto>(sql.ToString())
                                             .ToList();
            
            return tamanhosProdutos;
        }

        /// <summary>
        /// Método responsável por listar os tamanhos de produtos pelo nome
        /// </summary>
        /// <param name="descricoes"></param>
        /// <returns></returns>
        public ICollection<TamanhoProduto> ListarPorNomes(ICollection<string> nomes)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT * FROM tamanho_produto");
            sql.Append("WHERE Nome = @Nome AND Excluido = 0");

            var tamanhosProdutos = _dbLeitura.MySqlConnection
                                             .Query<TamanhoProduto>(sql.ToString(), new { Nome = nomes })
                                             .ToList();
            
            return tamanhosProdutos;
        }

        /// <summary>
        /// Método responsável por selecionar o tamanho que tenha o mesmo nome e o id diferente 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nome"></param>
        /// <returns></returns>
        public TamanhoProduto SelecionaPorNomeComIdDiferente(int id, string nome)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT * FROM tamanho_produto");
            sql.Append("WHERE Nome = @Nome AND id <> @Id AND Excluido = 0");

            var tamanhoProduto = _dbLeitura.MySqlConnection
                                           .Query<TamanhoProduto>(sql.ToString(), new
                                           {
                                               Nome = nome,
                                               Id = id
                                           }).FirstOrDefault();
            
            return tamanhoProduto;
        }

        /// <summary>
        /// Método responsável por listar os nomes dos produtos que estão vinculados ao tamanho
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ICollection<string> ListarNomeProdutosPorTamanhoId(int tamanhoId)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT produto.nome FROM produto");
            sql.AppendLine("INNER JOIN produto_tamanho_valor on (produto.id = produto_tamanho_valor.produtoid)");
            sql.Append("WHERE produto_tamanho_valor.tamanhoprodutoid = @Id AND produto.Excluido = 0");

            var nomesProdutos = _dbLeitura.MySqlConnection
                                          .Query<string>(sql.ToString(), new { Id = tamanhoId })
                                          .ToList();
            
            return nomesProdutos;
        }
        /// <summary>
        /// Método responsável por selecionar um tamanho de produto pelo nome
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
        public TamanhoProduto SelecionaPorNome(string nome)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT * FROM tamanho_produto");
            sql.Append("WHERE Nome = @Nome AND Excluido = 0");

            var tamanhoProduto = _dbLeitura.MySqlConnection
                                           .Query<TamanhoProduto>(sql.ToString(), new { Nome = nome })
                                           .FirstOrDefault();
            
            return tamanhoProduto;
        }
        
        /// <summary>
        /// Método responsável por selecionar o tamanho do produto por grupo de produto
        /// </summary>
        /// <param name="grupoProdutoId"></param>
        /// <returns></returns>
        public ICollection<TamanhoProduto> ListarPorGrupoProdutoId(int grupoProdutoId)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT * FROM tamanho_produto");
            sql.Append("WHERE grupoprodutoid = @GrupoProdutoId AND ativo = 1 AND Excluido = 0");

            var tamanhosProdutos = _dbLeitura.MySqlConnection
                                             .Query<TamanhoProduto>(sql.ToString(),
                                                                    new { GrupoProdutoId = grupoProdutoId })
                                             .ToList();
            
            return tamanhosProdutos;
        }

        /// <summary>
        /// Método responsável por verificar se tem produto vinculado ao tamanho
        /// </summary>
        /// <param name="tamanhoProdutoId"></param>
        /// <returns></returns>
        public bool PossuiProdutoVinculado(int tamanhoProdutoId)
        {
            string sql = "SELECT TamanhoProdutoId FROM produto_tamanho_valor " +
                         "WHERE TamanhoProdutoId = " + tamanhoProdutoId + " AND Excluido = 0";
                    
            return _dbLeitura.MySqlConnection.Query<int>(sql).FirstOrDefault() > 0;
        }

        public bool PossuiProdutoVinculadoPorGrupoId(int grupoProdutoId, int tamanhoProdutoId)
        {
            var sql = @"
                SELECT 
	                TamanhoProdutoId 
                FROM produto_tamanho_valor
                inner join produto on produto.id = produto_tamanho_valor.ProdutoId
                where
		                produto_tamanho_valor.Excluido = 0
                    and produto.Excluido = 0
                    and produto_tamanho_valor.tamanhoprodutoId = @TamanhoProdutoId
                    and produto.GrupoProdutoId = @GrupoProdutoId

            ";

            return _dbLeitura.MySqlConnection.Query<int>(sql,
                    new { GrupoProdutoId = grupoProdutoId, TamanhoProdutoId = tamanhoProdutoId })
                    .FirstOrDefault() > 0;
        }

        public ICollection<string> NomeProdutosVinculos(int grupoProdutoId, int tamanhoProdutoId)
        {
            var sql = @"
                SELECT 
	                produto.Nome 
                FROM produto_tamanho_valor
                inner join produto on produto.id = produto_tamanho_valor.ProdutoId
                where
		                produto_tamanho_valor.Excluido = 0
                    and produto.Excluido = 0
                    and produto_tamanho_valor.tamanhoprodutoId = @TamanhoProdutoId
                    and produto.GrupoProdutoId = @GrupoProdutoId

            ";

            return _dbLeitura.MySqlConnection.Query<string>(sql,
                    new { GrupoProdutoId = grupoProdutoId, TamanhoProdutoId = tamanhoProdutoId })
                    .ToList();
        }

        /// <summary>
        /// Método responsável por listar os Ids dos Tamanhos dos Produto que estão marcados como excluídos
        /// </summary>
        /// <returns></returns>
        public IEnumerable<int> ListarTodosIdsIngredienteExcluido()
        {
            var tamanhosProdutosIds = _dbLeitura.MySqlConnection
                                                .Query<int>(@"SELECT id FROM tamanho_produto 
                                                              WHERE Excluido = 1")
                                                .ToList();

            return tamanhosProdutosIds;
        }
    }
}
