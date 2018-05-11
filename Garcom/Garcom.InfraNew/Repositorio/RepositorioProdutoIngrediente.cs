using Garcom.Dominio.Entidade.Models;
using Garcom.Infra.DBEscrita;
using Garcom.Infra.DBLeitura;
using Dapper;
using System.Collections.Generic;
using System.Linq;

namespace Garcom.Infra.Repositorio
{
    public class RepositorioProdutoIngrediente : RepositorioBase<ProdutoIngrediente>
    {
        public RepositorioProdutoIngrediente(ContextoEscrita dbEscrita, ContextoLeitura dbLeitura)
            :base(dbEscrita, dbLeitura)
        {}

        public void SetaProdutoIngredienteTamanhoExcluidoPorIngredienteId(int ingredienteId, bool excluido)
        {
            string sql = string.Format(@"UPDATE produto_ingrediente_tamanho_valor SET Excluido = @Excluido
                                        WHERE ProdutoIngredienteId IN (SELECT produto_ingrediente.Id FROM produto_ingrediente 
							                                          WHERE produto_ingrediente.IngredienteId = @IngredienteId)");
            this._dbLeitura.MySqlConnection.Execute(sql, new { Excluido = excluido ? 1 : 0, IngredienteId = ingredienteId });
        }

        public void SetaProdutoIngredienteExcluido(int ingredienteId, bool excluido)
        {
            string sql = string.Format(@"UPDATE produto_ingrediente SET Excluido = @Excluido
                                        WHERE IngredienteId = @IngredienteId");
            this._dbLeitura.MySqlConnection.Execute(sql, new { Excluido = excluido ? 1 : 0, IngredienteId = ingredienteId });
        }

        /// <summary>
        /// Método responsável por listar os Ids dos produtos ingrediente marcados para serem excluídos
        /// </summary>
        /// <returns></returns>
        public IEnumerable<int> ListarTodosIdsProdutoIngredienteExcluido()
        {
            var produtosIds = _dbLeitura.MySqlConnection.Query<int>("SELECT Id FROM produto_ingrediente WHERE Excluido = 1").ToList();

            return produtosIds;
        }
    }
}
