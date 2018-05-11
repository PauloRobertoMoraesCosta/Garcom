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
    /// Repositório da classe Mesa
    /// </summary>
    public class RepositorioMesa : RepositorioBase<Mesa>
    {
        /// <summary>
        /// Contrutor
        /// </summary>
        /// <param name="dbEscrita"></param>
        /// <param name="dbLeitura"></param>
        public RepositorioMesa(ContextoEscrita dbEscrita, ContextoLeitura dbLeitura)
            :base(dbEscrita, dbLeitura)
        {

        }
        /// <summary>
        /// Método lista todas as mesas
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mesa> ListarTodasMesas()
        {
            var mesas = _dbLeitura.MySqlConnection.Query<Mesa>("select id, descricao, datacadastro, ativo from Mesa where excluido <> 1").ToList();
            
            return mesas;
        }

        /// <summary>
        /// Método lista todos as listas ativas
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mesa> ListarTodasMesasAtivas()
        {
            var mesas = _dbLeitura.MySqlConnection.Query<Mesa>("select id, descricao, datacadastro, ativo from Mesa WHERE ativo = 1 and excluido <> 1").ToList();
            
            return mesas;
        }

        /// <summary>
        /// Método responsável por listar os Ids das Mesas que estão marcados para serem excluídas
        /// </summary>
        /// <returns></returns>
        public IEnumerable<int> ListarTodosIdsMesaExcluido()
        {
            var mesas = _dbLeitura.MySqlConnection.Query<int>("SELECT Id FROM mesa WHERE Excluido = 1").ToList();

            return mesas;
        }

        /// <summary>
        /// Seleciona a mesa pela Descrição
        /// </summary>
        /// <param name="descricao"></param>
        /// <returns></returns>
        public Mesa SelecionaPorDescricao(string descricao)
        {
            var mesa = _dbLeitura.MySqlConnection.Query<Mesa>("select id, descricao, datacadastro, ativo from Mesa where descricao =@Descricao and excluido = 0 ", 
                new { Descricao = descricao }).FirstOrDefault();
            
            return mesa;
        }

        /// <summary>
        /// Método responsável por selecionar a mesa que tenha a mesma descrição e o id diferente 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nome"></param>
        /// <returns></returns>
        public Mesa SelecionaPorDescricaoComIdDiferente(int id, string descricao)
        {
            var mesa = _dbLeitura.MySqlConnection.Query<Mesa>("SELECT id, descricao, datacadastro, ativo FROM mesa WHERE descricao = @Descricao AND id <> @Id and excluido = 0",
                                                                    new { Descricao = descricao, Id = id }).FirstOrDefault();
            
            return mesa;
        }
    }
}
