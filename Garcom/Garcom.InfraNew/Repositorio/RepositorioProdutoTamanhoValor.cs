using Garcom.Dominio.Entidade.Models;
using Garcom.Dominio.Entidade.Models.Auditoria;
using Garcom.Infra.DBEscrita;
using Garcom.Infra.DBLeitura;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Dapper;

namespace Garcom.Infra.Repositorio
{
    /// <summary>
    /// Repositório da classe Tamanho Produto Valor
    /// </summary>
    public class RepositorioProdutoTamanhoValor : RepositorioBase<ProdutoTamanhoValor>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="dbEscrita"></param>
        /// <param name="dbLeitura"></param>
        public RepositorioProdutoTamanhoValor(ContextoEscrita dbEscrita, ContextoLeitura dbLeitura)
            :base(dbEscrita, dbLeitura)
        {

        }

        /// <summary>
        /// Adiciona um tamanho produto valor sem o savechange
        /// </summary>
        /// <param name="tamanhoProdutoValor"></param>
        /// <returns></returns>
        public ProdutoTamanhoValor Add(ProdutoTamanhoValor produtoTamanhoValor)
        {
            return _dbEscrita.ProdutosTamanhosValores.Add(produtoTamanhoValor);
        }

        /// <summary>
        /// Método responsável por remover vários produtos
        /// </summary>
        /// <param name="tamanhosProdutoValor"></param>
        public void RemoverVariosProdutosTamanhosValor(ICollection<ProdutoTamanhoValor> produtosTamanhosValor)
        {
            foreach (var produtoTamanhoValor in produtosTamanhosValor)
            {
                this.Excluir(produtoTamanhoValor.Id);
            }
        }

        public void RemoverVariosProdutosTamanhosValorPorTamanhoProdutoIds(int grupoProdutoId, IEnumerable<int> listaTamanhoProdutoids)
        {
            foreach (var tamanhoProdutoId in listaTamanhoProdutoids)
            {
                var produtosTamanhoValor = _dbEscrita.Set<ProdutoTamanhoValor>()
                    .Include(p => p.Produto)
                    .Where(pt => pt.TamanhoProdutoId == tamanhoProdutoId && pt.Produto.GrupoProdutoId == grupoProdutoId).ToList();

                if (produtosTamanhoValor.Count > 0)
                    produtosTamanhoValor.ForEach(ExcluirSemSaveChange);
                    
            }
        }

        

        
        public void ExcluirSemSaveChange(ProdutoTamanhoValor produtoTamanhoValor)
        {
            _dbEscrita.Set<ProdutoTamanhoValor>().Attach(produtoTamanhoValor);
            _dbEscrita.Entry(produtoTamanhoValor).State = EntityState.Deleted;
        }

        
        public IEnumerable<ChangeLog> AlterarComAuditoriaSemSaveChange(ProdutoTamanhoValor entidade)
        {
            _dbEscrita.Set<ProdutoTamanhoValor>().Attach(entidade);
            _dbEscrita.Entry(entidade).State = EntityState.Modified;
            var alteracoes = _dbEscrita.RetornaModificacoes();
            return alteracoes;
        }

        /// <summary>
        /// Método responsável por alterar o produto e retornar os atributos que foram alterados
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns>Retorna os atributos que foram alterados</returns>
        public IEnumerable<ChangeLog> AlterarComAuditoria(ProdutoTamanhoValor entidade)
        {
            _dbEscrita.Set<ProdutoTamanhoValor>().Attach(entidade);
            _dbEscrita.Entry(entidade).State = EntityState.Modified;
            var alteracoes = _dbEscrita.RetornaModificacoes();
            
            return alteracoes;
        }

        /// <summary>
        /// Método responsável por listar o Tamanho Produto Valor pelo id do Tamanho Produto
        /// </summary>
        /// <param name="tamanhoId"></param>
        /// <returns></returns>
        public IEnumerable<int> ListarPorTamanhoId(int tamanhoId)
        {
            return _dbLeitura.MySqlConnection.Query<int>("SELECT id FROM produto_tamanho_valor WHERE Excluido = 0 AND TamanhoProdutoId = " + tamanhoId);
        }
    }
}
