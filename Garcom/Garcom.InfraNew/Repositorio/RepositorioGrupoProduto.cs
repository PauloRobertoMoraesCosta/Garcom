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
    /// Repositório da classe GrupoProduto
    /// </summary>
    public class RepositorioGrupoProduto : RepositorioBase<GrupoProduto>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="dbEscrita"></param>
        /// <param name="dbLeitura"></param>
        public RepositorioGrupoProduto(ContextoEscrita dbEscrita, ContextoLeitura dbLeitura)
            :base(dbEscrita, dbLeitura)
        {}

        /// <summary>
        /// Método responsável por alterar o grupo de produto
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns>Retorna os atributos que foram alterados</returns>
        public IEnumerable<ChangeLog> AlterarComAuditoria(GrupoProduto entidade)
        {
            _dbEscrita.Set<GrupoProduto>().Attach(entidade);
            _dbEscrita.Entry(entidade).State = EntityState.Modified;
            var alteracoes = _dbEscrita.RetornaModificacoes();
            
            return alteracoes;
        }

        /// <summary>
        /// Método responsável por listar todos os grupos de produtos cadastrados no banco de dados juntamente com os tamanhos cadastrados
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GrupoProduto> ListarTodosComTamanhos()
        {
            var gruposProdutos = _dbLeitura.MySqlConnection.Query<GrupoProduto>(@"SELECT Id, Nome, PermiteDividir FROM grupo_produto 
                                                                                  WHERE Excluido = 0 ORDER BY Id").ToList();

            return gruposProdutos;
        }

        /// <summary>
        /// Método responsável por listar todos os grupos de produtos cadastrados no banco de dados
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GrupoProduto> ListarTodos()
        {
            var gruposProdutos = _dbLeitura.MySqlConnection
                                           .Query<GrupoProduto>(@"SELECT Id, Nome, PermiteDividir FROM grupo_produto 
                                                                  WHERE Excluido = 0 ORDER BY Id").ToList();

            return gruposProdutos;
        }

        public IEnumerable<GrupoProdutoTamanhoProduto> ListarGrupoProdutoTamanhoProdutoPorGrupoProdutoId(int grupoProdutoId)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT grupo_produto_tamanho_produto.Ordem, tamanho_produto.* FROM grupo_produto_tamanho_produto");
            sql.AppendLine("INNER JOIN tamanho_produto ON tamanho_produto.Id = grupo_produto_tamanho_produto.TamanhoProdutoId");
            sql.Append("WHERE grupo_produto_tamanho_produto.Excluido = 0 AND tamanho_produto.Ativo = 1 AND tamanho_produto.Excluido = 0 ");
            sql.Append("AND grupo_produto_tamanho_produto.GrupoProdutoId = @GrupoProdutoId");

            var gruposProdutoTamanhosProduto = _dbLeitura.MySqlConnection.Query<GrupoProdutoTamanhoProduto, TamanhoProduto, GrupoProdutoTamanhoProduto>(
            sql.ToString(), (g, t) =>
            {
                if (t != null)
                    g.TamanhoProduto = t;
                return g;
            }, new { GrupoProdutoId = grupoProdutoId }).ToList();

            return gruposProdutoTamanhosProduto;                                            
        }

        public ICollection<GrupoProdutoTamanhoProduto> ListarGrupoProdutoTamanhoProduto(int grupoProdutoId)
        {
            return _dbEscrita.GruposProdutoTamanhosProduto.Where(p => p.GrupoProdutoId == grupoProdutoId).ToList();
        }

        public ICollection<GrupoProdutoTamanhoProduto> ListarGrupoProdutoTamanhoProdutoPorTamanhoId(int tamanhoId)
        {
            return _dbEscrita.GruposProdutoTamanhosProduto.Where(p => p.TamanhoProdutoId == tamanhoId).ToList();
        }

        /// <summary>
        /// Método responsável por retornar a lista dos nomes de grupo de produtos vinculado ao tamanho produto
        /// </summary>
        /// <param name="tamanhoId"></param>
        /// <returns></returns>
        public ICollection<string> ListarNomeTamanhoId(int tamanhoId)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT grupo_produto.Nome FROM grupo_produto");
            sql.AppendLine("INNER JOIN grupo_produto_tamanho_produto ON grupo_produto_tamanho_produto.GrupoProdutoId = grupo_produto.Id");
            sql.Append("WHERE grupo_produto_tamanho_produto.TamanhoProdutoId = @TamanhoId AND grupo_produto.Excluido = 0 ");
            sql.Append("AND grupo_produto_tamanho_produto.Excluido = 0");
            return _dbLeitura.MySqlConnection
                             .Query<string>(sql.ToString(), new { TamanhoId = tamanhoId})
                             .ToList();
        }

        public void ExcluirGrupoProdutoTamanhoProduto(GrupoProdutoTamanhoProduto entidade)
        {
            entidade.Excluido = true;
            _dbEscrita.Set<GrupoProdutoTamanhoProduto>().Attach(entidade);
            _dbEscrita.Entry(entidade).State = EntityState.Modified;
        }

        public void DesfazerGrupoProdutoTamanhoProduto(GrupoProdutoTamanhoProduto entidade)
        {
            entidade.Excluido = false;
            _dbEscrita.Set<GrupoProdutoTamanhoProduto>().Attach(entidade);
            _dbEscrita.Entry(entidade).State = EntityState.Modified;
        }

        /// <summary>
        /// Método responsável por listar os Ids dos Grupos de Produtos que estão configurado para ser 
        /// excluídos
        /// </summary>
        /// <returns></returns>
        public IEnumerable<int> ListarTodosIdsGrupoProdutoExcluido()
        {
            var gruposProdutosIds = _dbLeitura.MySqlConnection.Query<int>(@"SELECT Id FROM grupo_produto WHERE Excluido = 1").ToList();

            return gruposProdutosIds;
        }

        /// <summary>
        /// Método responsável por selecionar o grupo de produto pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GrupoProduto SelecionarGrupoProdutoPorId(int id)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT Id, Nome, PermiteDividir, DataCadastro FROM grupo_produto");
            sql.Append("WHERE id = @Id AND Excluido = 0");
            var grupoProduto = _dbLeitura.MySqlConnection
                                         .Query<GrupoProduto>(sql.ToString(),
                                                              new { Id = id })
                                         .FirstOrDefault();

            return grupoProduto;
        }

        /// <summary>
        /// Método responsável por selecionar o grupo de produto pelo nome
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
        public GrupoProduto SelecionaPorNome(string nome)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT id, nome, datacadastro FROM grupo_produto");
            sql.Append("WHERE nome = @Nome AND Excluido = 0");
            var grupoProduto = _dbLeitura.MySqlConnection
                                         .Query<GrupoProduto>(sql.ToString(),
                                                              new { Nome = nome })
                                         .FirstOrDefault();
            
            return grupoProduto;
        }

        /// <summary>
        /// Método responsável por selecionar o grupo que tenha o mesmo nome e o id diferente 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nome"></param>
        /// <returns></returns>
        public GrupoProduto SelecionaPorNomeComIdDiferente(int id, string nome)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT id, nome, datacadastro FROM grupo_produto");
            sql.Append("WHERE nome = @Nome AND id <> @Id AND Excluido = 0");

            var grupoProduto = _dbLeitura.MySqlConnection
                                         .Query<GrupoProduto>(sql.ToString(),
                                                              new { Nome = nome, Id = id })
                                         .FirstOrDefault();
            
            return grupoProduto;
        }

        /// <summary>
        /// Método responsável por listar os nomes dos produtos vinculado ao grupo de produto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ICollection<string> ListaNomeProdutoGrupoPorProdutoId(int id)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT nome FROM produto");
            sql.AppendLine("WHERE grupoprodutoid = @GrupoProdutoId AND Excluido = 0");
            sql.Append("ORDER BY nome");

            var produtos = _dbLeitura.MySqlConnection
                                     .Query<string>(sql.ToString(),
                                                   new { GrupoProdutoId = id })
                                     .ToList();
       
            return produtos;
        }

        /// <summary>
        /// Método responsável por listar os nomes dos tamanhos de produtos vinculado ao grupo de produto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ICollection<string> ListaNomeTamanhoProdutoPorGrupoProdutoId(int id)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT nome FROM tamanho_produto");
            sql.AppendLine("WHERE grupoprodutoid = @GrupoProdutoId AND Excluido = 0");
            sql.Append("ORDER BY nome");

            var tamanhosProdutos = _dbLeitura.MySqlConnection
                                             .Query<string>(sql.ToString(),
                                                            new { GrupoProdutoId = id })
                                             .ToList();
            
            return tamanhosProdutos;
        }

        public GrupoProduto SelecionarPorIdSemVerificarExcluido(int id)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT Id, Nome, PermiteDividir, DataCadastro FROM grupo_produto");
            sql.Append("WHERE id = @Id");
            var grupoProduto = _dbLeitura.MySqlConnection
                                         .Query<GrupoProduto>(sql.ToString(),
                                                              new { Id = id })
                                         .FirstOrDefault();
            return grupoProduto;
        }

        public bool RetornarAtributoExcluidoPorId(int id)
        {
            var sql = new StringBuilder();
            sql.AppendLine("SELECT Excluido FROM grupo_produto");
            sql.Append("WHERE id = @Id");
            var excluido = _dbLeitura.MySqlConnection
                                     .Query<bool>(sql.ToString(),
                                                  new { Id = id })
                                     .FirstOrDefault();
            return excluido;
        }

        public void RemoverBancoTodosGrupoProdutoTamanhoProdutoPorGrupoProdutoId(int grupoProdutoId)
        {
            _dbEscrita.Set<GrupoProdutoTamanhoProduto>().RemoveRange(_dbEscrita.GruposProdutoTamanhosProduto.Where(p => p.GrupoProdutoId == grupoProdutoId).ToList());
        }
    }
}
