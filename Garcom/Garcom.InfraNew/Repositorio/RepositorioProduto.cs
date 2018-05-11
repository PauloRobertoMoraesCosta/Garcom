using Dapper;
using Garcom.Dominio.Entidade.DTOs;
using Garcom.Dominio.Entidade.Models;
using Garcom.Dominio.Entidade.Models.Auditoria;
using Garcom.Infra.DBEscrita;
using Garcom.Infra.DBLeitura;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Garcom.Infra.Repositorio
{
    /// <summary>
    /// Repositório da classe produto
    /// </summary>
    public class RepositorioProduto : RepositorioBase<Produto>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="dbEscrita"></param>
        /// <param name="dbLeitura"></param>
        public RepositorioProduto(ContextoEscrita dbEscrita, ContextoLeitura dbLeitura)
            : base(dbEscrita, dbLeitura)
        {
        }

        /// <summary>
        /// Método responsável por retornar as alterações realizadas no cadastro do produto
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ChangeLog> RetornaAlteracoes()
        {
            var alteracoes = _dbEscrita.RetornaModificacoes();
            return alteracoes;
        }

        public void AlterarProduto(Produto produto)
        {
            _dbEscrita.Produtos.AddOrUpdate(produto);
        }
        /// <summary>
        /// Método responsável por alterar o produto e retornar os atributos que foram alterados
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns>Retorna os atributos que foram alterados</returns>
        public IEnumerable<ChangeLog> AlterarComAuditoria(Produto entidade)
        {
            _dbEscrita.Set<Produto>().Attach(entidade);
            _dbEscrita.Entry(entidade).State = EntityState.Modified;
            var alteracoes = _dbEscrita.RetornaModificacoes();

            return alteracoes;
        }

        /// <summary>
        /// Método responsável por selecionar o produto pelo seu nome
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
        public Produto SelecionaPorNomeComIdDiferente(int id, string nome)
        {
            var produto = _dbLeitura.MySqlConnection.Query<Produto>("SELECT * FROM produto WHERE Id <> @Id AND nome = @Nome AND Excluido = 0",
                                                                    new { Id = id, Nome = nome }).FirstOrDefault();

            return produto;
        }

        /// <summary>
        /// Método responsável por selecionar um produto por seu código rápido e que o produto seja diferente do codigo informado
        /// </summary>
        /// <param name="codigoRapido"></param>
        /// <returns></returns>
        public Produto SelecionaPorCodigoRapidoComIdDiferente(string codigoRapido, int id)
        {
            var produto = _dbLeitura.MySqlConnection.Query<Produto>("select * from produto where Id <> @Id AND CodigoRapido = @codigo AND Excluido = 0",
                                                                    new {Id = id, codigo = codigoRapido }).FirstOrDefault();
            return produto;

        }

        /// <summary>
        /// Método responsável por selecionar um produto por seu código rápido
        /// </summary>
        /// <param name="codigoRapido"></param>
        /// <returns></returns>
        public Produto SelecionaPorCodigoRapido(string codigoRapido)
        {
            var produto = _dbLeitura.MySqlConnection.Query<Produto>("select * from produto where CodigoRapido = @codigo",
                                                                    new { codigo = codigoRapido }).FirstOrDefault();
            return produto;

        }

        public string NomeImagem(int Id)
        {
            return _dbLeitura.MySqlConnection.Query<string>("select nomeimagem from produto where id = @id", new { id = Id }).FirstOrDefault();
        }

        /// <summary>
        /// Método responsável por selecionar o produto pela sua descricação
        /// </summary>
        /// <param name="descricoes"></param>
        /// <returns></returns>
        public ICollection<Produto> ListarPorDescricoes(ICollection<string> nomes)
        {
            var produtos = _dbLeitura.MySqlConnection.Query<Produto>("SELECT * FROM produto WHERE nome IN @Nome",
                                                                    new { Nome = nomes }).ToList();

            return produtos;
        }

        /// <summary>
        /// Método responsável por listar todos os produtos com o nome do grupo de produto vinculado
        /// </summary>
        /// <returns></returns>
        public ICollection<ProdutoDTO> ListarProdutoComNomeGrupo()
        {
            string sql = @"
                SELECT Id, Nome, GrupoProdutoId, Ativo, CodigoRapido FROM produto 
                WHERE Excluido = 0
              ";

            return _dbLeitura.MySqlConnection
                             .Query<ProdutoDTO>(sql)
                             .ToList();
        }

        /// <summary>
        /// Método responsável por listar os Produtos pelo id do Grupo de Produto vinculado
        /// </summary>
        /// <param name="grupoProdutoId"></param>
        /// <returns></returns>
        public ICollection<Produto> ListaProdutoPeloGrupoProdutoId(int grupoProdutoId)
        {
            return _dbLeitura.MySqlConnection
                             .Query<Produto>("SELECT * FROM produto WHERE GrupoProdutoId = " + grupoProdutoId)
                             .ToList();
        }

        /// <summary>
        /// Método responsável por selecionar um produto pelo seu id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Produto SelecionaProdutoPorId(int id)
        {
            string sql = @"
                SELECT 
                    produto.*,
                    produto_ingrediente.*,
                    produto_tamanho_valor.*,
                    produto_ingrediente_tamanho_valor.*
                FROM PRODUTO
                LEFT JOIN produto_ingrediente on produto.Id = produto_ingrediente.ProdutoId
                LEFT JOIN produto_tamanho_valor on produto.Id = produto_tamanho_valor.ProdutoId
                LEFT JOIN produto_ingrediente_tamanho_valor 
                          on produto_ingrediente.Id = produto_ingrediente_tamanho_valor.ProdutoIngredienteId
                LEFT JOIN grupo_produto_tamanho_produto ON produto_tamanho_valor.TamanhoProdutoId = grupo_produto_tamanho_produto.TamanhoProdutoId
                                                         AND grupo_produto_tamanho_produto.GrupoProdutoId = produto.GrupoProdutoId 
                WHERE produto.Id = @ID
                ORDER BY grupo_produto_tamanho_produto.Ordem
            ";

            var dicionario = new Dictionary<int, Produto>();

            _dbLeitura.MySqlConnection.Query<Produto, ProdutoIngrediente, ProdutoTamanhoValor, ProdutoIngredienteTamanhoValor, Produto>
            (sql, (p, pi, pt, ge) =>
            {
                Produto produto;

                if (!dicionario.TryGetValue(p.Id, out produto))
                    dicionario.Add(p.Id, produto = p);

                if (produto != null && produto.ProdutoIngredientes.Where(x => x.Id == pi.Id).Count() == 0)
                {
                    if (pi != null && ge != null && pi.ProdutosIngredientesTamanhosValor.Where(x => x.Id == ge.Id).Count() == 0 && !ge.Excluido)
                        pi.ProdutosIngredientesTamanhosValor.Add(ge);
                    if(pi != null && !pi.Excluido)
                        produto.ProdutoIngredientes.Add(pi);
                }
                else
                {
                    var produtoIngrediente = produto.ProdutoIngredientes.Where(x => x.Id == pi.Id).FirstOrDefault();

                    if (produtoIngrediente != null && ge != null && produtoIngrediente.ProdutosIngredientesTamanhosValor
                                                                        .Where(x => x.Id == ge.Id).Count() == 0 && !ge.Excluido)
                        produtoIngrediente.ProdutosIngredientesTamanhosValor.Add(ge);
                }

                if (produto != null && pt != null && produto.ProdutosTamanhosValor.Where(x => x.Id == pt.Id).Count() == 0 && !pt.Excluido)
                    produto.ProdutosTamanhosValor.Add(pt);
                
                return produto;
            }, new { ID = id }).AsQueryable();
            
            return dicionario.Values.FirstOrDefault();
        }

        public ICollection<ProdutoIngredienteTamanhoValor> ListarProdutoIngredienteTamanho(int produtoId)
        {
            string sql = @"SELECT produto_ingrediente_tamanho_valor.* FROM produto_ingrediente_tamanho_valor
                           INNER JOIN PRODUTO_INGREDIENTE ON (produto_ingrediente_tamanho_valor.ProdutoIngredienteId = PRODUTO_INGREDIENTE.Id)
                           INNER JOIN PRODUTO ON (PRODUTO_INGREDIENTE.ProdutoId = PRODUTO.Id)
                           WHERE PRODUTO.Id = @ProdutoId";
            var produtosIngredientesTamanhos = _dbLeitura.MySqlConnection.Query<ProdutoIngredienteTamanhoValor>(sql, new { ProdutoId = produtoId }).ToList();

            return produtosIngredientesTamanhos;
        }

        /// <summary>
        /// Adiciona um tamanho produto valor sem o savechange
        /// </summary>
        /// <param name="tamanhoProdutoValor"></param>
        /// <returns></returns>
        public ProdutoTamanhoValor AddProdutoTamanhoValor(ProdutoTamanhoValor produtoTamanhoValor)
        {
            return _dbEscrita.ProdutosTamanhosValores.Add(produtoTamanhoValor);
        }

        /// <summary>
        /// Método responsável por excluir um tamanho produto valor
        /// </summary>
        /// <param name="tamanhoProdutoValor"></param>
        public void ExcluirProdutoTamanhoValorSemSaveChange(ProdutoTamanhoValor produtoTamanhoValor)
        {
            _dbEscrita.Set<ProdutoTamanhoValor>().Attach(produtoTamanhoValor);
            _dbEscrita.Entry(produtoTamanhoValor).State = EntityState.Deleted;
        }

        /// <summary>
        /// Método responsável por alterar um tamanho produto valor
        /// </summary>
        /// <param name="entidade"></param>
        public void AlterarProdutoTamanhoValorSemSaveChange(ProdutoTamanhoValor entidade)
        {
            _dbEscrita.Set<ProdutoTamanhoValor>().Attach(entidade);
            _dbEscrita.Entry(entidade).State = EntityState.Modified;
        }

        /// <summary>
        /// Adiciona um produto ingrediente
        /// </summary>
        /// <param name="produtoIngrediente"></param>
        /// <returns></returns>
        public ProdutoIngrediente AddProdutoIngrediente(ProdutoIngrediente produtoIngrediente)
        {
            return _dbEscrita.ProdutoIngredientes.Add(produtoIngrediente);
        }

        /// <summary>
        /// Método responsável por excluir um produto ingrediente
        /// </summary>
        /// <param name="produtoIngredienteTamanho"></param>
        public void ExcluirProdutoIngredienteSemSaveChange(ProdutoIngrediente produtoIngrediente)
        {
            _dbEscrita.Set<ProdutoIngrediente>().Attach(produtoIngrediente);
            _dbEscrita.Entry(produtoIngrediente).State = EntityState.Deleted;
        }

        /// <summary>
        /// Método responsável por alterar um produto ingrediente
        /// </summary>
        /// <param name="entidade"></param>
        public void AlterarProdutoIngredienteSemSaveChange(ProdutoIngrediente entidade)
        {
            _dbEscrita.ProdutoIngredientes.AddOrUpdate(entidade);
        }

        /// <summary>
        /// Adiciona um produto ingrediente tamanho produto sem o savechange
        /// </summary>
        /// <param name="produtoIngredienteTamanho"></param>
        /// <returns></returns>
        public ProdutoIngredienteTamanhoValor AddProdutoIngredienteTamanhoValor(ProdutoIngredienteTamanhoValor produtoIngredienteTamanhoValor)
        {
            return _dbEscrita.ProdutosIngredientesTamanhosValores.Add(produtoIngredienteTamanhoValor);
        }

        /// <summary>
        /// Método responsável por excluir um produto ingrediente tamanho produto
        /// </summary>
        /// <param name="produtoIngredienteTamanho"></param>
        public void ExcluirProdutoIngredienteTamanhoValorSemSaveChange(ProdutoIngredienteTamanhoValor produtoIngredienteTamanhoValor)
        {
            _dbEscrita.Set<ProdutoIngredienteTamanhoValor>().Attach(produtoIngredienteTamanhoValor);
            _dbEscrita.Entry(produtoIngredienteTamanhoValor).State = EntityState.Deleted;
        }

        /// <summary>
        /// Método responsável por alterar um produto ingrediente tamanho produto
        /// </summary>
        /// <param name="entidade"></param>
        public void AlterarProdutoIngredienteTamanhoValorSemSaveChange(ProdutoIngredienteTamanhoValor entidade)
        {
            _dbEscrita.ProdutosIngredientesTamanhosValores.AddOrUpdate(entidade);
        }

        /// <summary>
        /// Método responsável por listar os produtos marcados para serem excluídos
        /// </summary>
        public IEnumerable<Produto> ListarTodosExcluido()
        {
            var produtos = _dbLeitura.MySqlConnection
                                     .Query<Produto>("SELECT Id, NomeImagem FROM produto WHERE Excluido = 1")
                                     .ToList();

            return produtos;
        }
    }
}
