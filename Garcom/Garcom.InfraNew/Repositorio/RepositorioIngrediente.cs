using Dapper;
using Garcom.Dominio.Entidade.Models;
using Garcom.Dominio.Entidade.Models.Auditoria;
using Garcom.Infra.DBEscrita;
using Garcom.Infra.DBLeitura;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Garcom.Infra.Repositorio
{
    /// <summary>
    /// Repositório da classe Ingrediente
    /// </summary>
    public class RepositorioIngrediente : RepositorioBase<Ingrediente>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="dbEscrita"></param>
        /// <param name="dbLeitura"></param>
        public RepositorioIngrediente(ContextoEscrita dbEscrita, ContextoLeitura dbLeitura)
            :base(dbEscrita, dbLeitura)
        {
        }

        /// <summary>
        /// Método lista todos os ingredientes
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Ingrediente> ListarTodosIngrediente()
        {
            var ingredientes = _dbLeitura.MySqlConnection.Query<Ingrediente>("select id, descricao, datacadastro, ativo from ingrediente where excluido <> 1").ToList();
            
            return ingredientes;
        }

        /// <summary>
        /// Método lista todos os ingredientes ativos
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Ingrediente> ListarTodosIngredientesAtivo()
        {
            var ingredientes = _dbLeitura.MySqlConnection.Query<Ingrediente>("select id, descricao from ingrediente WHERE ativo = 1 and excluido <> 1").ToList();
            
            return ingredientes;
        }

        /// <summary>
        /// Método responsável por selecionar um ingrediente pela descrição
        /// </summary>
        /// <returns></returns>
        public Ingrediente SelecionaPelaDescricao(string descricao, int id)
        {
            var ingrediente = _dbLeitura.MySqlConnection
                                        .Query<Ingrediente>($"SELECT Id, Descricao FROM ingrediente WHERE Id <> {id} AND Descricao = '{descricao}' AND Excluido <> 1")
                                        .FirstOrDefault();

            return ingrediente;
        }

        /// <summary>
        /// Método responsável por listar todos os ingredientes marcados para excluir
        /// </summary>
        /// <returns></returns>
        public IEnumerable<int> ListarTodosIdsIngredienteExcluido()
        {
            var ingredientesId = _dbLeitura.MySqlConnection.Query<int>("SELECT Id FROM ingrediente WHERE Excluido = 1");

            return ingredientesId;
        }

        public ICollection<Produto> ProdutosVinculados(int id)
        {
            string query = @"
                select distinct
                    p.Id, 
                    p.Nome 
                from produto p
                inner join produto_ingrediente pi on pi.ProdutoId = p.Id
                where
                     pi.IngredienteId = @id
                     and p.excluido <> 1
                     and pi.excluido <> 1                 
                order by p.Nome
            ";
            var Produtos = _dbLeitura.MySqlConnection.Query<Produto>(query, new { id }).ToList();
            
            return Produtos;
        }
    }
}
