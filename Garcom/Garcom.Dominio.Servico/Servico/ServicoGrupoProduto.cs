using Garcom.Dominio.Entidade.DTOs;
using Garcom.Dominio.Entidade.Models;
using Garcom.Dominio.Entidade.Models.Auditoria;
using Garcom.Dominio.Servico.Validacao;
using Garcom.Infra.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Garcom.Dominio.Servico.Servico
{
    public class ServicoGrupoProduto : ServicoBase<GrupoProduto>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ServicoGrupoProduto(IUnitOfWork unitOfWork)
            : base (unitOfWork, new ValidacaoGrupoProduto(unitOfWork))
        {
        }

        /// <summary>
        /// Método responsável por incluir grupo de produto
        /// </summary>
        /// <param name="grupoProdutoDTO"></param>
        /// <returns></returns>
        public GrupoProduto Incluir(GrupoProduto grupoProduto)
        {
            if (grupoProduto.DataCadastro == null) grupoProduto.DataCadastro = DateTime.Now;
            _validador.ValidaInclusao(grupoProduto);
            grupoProduto = _unitOfWork.RepositorioGrupoProduto.Incluir(grupoProduto);
            return grupoProduto;
        }

        /// <summary>
        /// Método responsável por alterar o grupo de produto
        /// </summary>
        /// <param name="grupoProdutoDTO"></param>
        /// <returns></returns>
        public GrupoProduto Altera(GrupoProduto grupoProdutoAlterado)
        {
            var grupoProduto = _unitOfWork.RepositorioGrupoProduto.SelecionarPorId(grupoProdutoAlterado.Id);
            if (grupoProduto == null)
                throw new Exception(_mensagens.GetMensagem("GrupoProdutoNaoCadastrado"));

            var listaGrupoTamanhoProdutoValor =
                _unitOfWork.RepositorioGrupoProduto.ListarGrupoProdutoTamanhoProdutoPorGrupoProdutoId(grupoProduto.Id);

            var listaTamanhoProdutosIds =
                grupoProdutoAlterado.GruposProdutoTamanhosProduto != null
                    ? listaGrupoTamanhoProdutoValor
                        .Where(l => grupoProdutoAlterado.GruposProdutoTamanhosProduto.All(g =>g.TamanhoProdutoId != l.TamanhoProduto.Id))
                        .Select(l => l.TamanhoProduto.Id).ToList()
                    : listaGrupoTamanhoProdutoValor
                        .Select(l=> l.TamanhoProduto.Id).ToList();

            _unitOfWork.RepositorioTamanhoProdutoValor.RemoverVariosProdutosTamanhosValorPorTamanhoProdutoIds(grupoProduto.Id, listaTamanhoProdutosIds);
            _unitOfWork.RepositorioGrupoProduto.RemoverBancoTodosGrupoProdutoTamanhoProdutoPorGrupoProdutoId(grupoProduto.Id);

            grupoProduto.Nome = grupoProdutoAlterado.Nome;
            grupoProduto.Ativo = grupoProduto.Ativo;
            grupoProduto.PermiteDividir = grupoProdutoAlterado.PermiteDividir;
            grupoProduto.GruposProdutoTamanhosProduto = grupoProdutoAlterado.GruposProdutoTamanhosProduto;
            _validador.ValidaAlteracao(grupoProduto);

            return _unitOfWork.RepositorioGrupoProduto.Alterar(grupoProduto);
        }

        /// <summary>
        /// Método responsável por listar todos os grupos de produtos
        /// </summary>
        /// <returns></returns>
        public IEnumerable<GrupoProduto> ListarTodos()
        {
            var gruposProdutos = _unitOfWork.RepositorioGrupoProduto.ListarTodos();
            return gruposProdutos;
        }

        /// <summary>
        /// Método responsável por selecionar o grupo de produto pelo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GrupoProduto SelecionaPorId(int id)
        {
            var grupoProduto = _unitOfWork.RepositorioGrupoProduto.SelecionarGrupoProdutoPorId(id);
            return grupoProduto;
        }

        /// <summary>
        /// Método responsável por validar a exclusão do grupo de produto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ICollection<string> ValidaExclusao(int id)
        {
            var nomeProdutos = _unitOfWork.RepositorioGrupoProduto.ListaNomeProdutoGrupoPorProdutoId(id);
           // var nomeTamanhoProdutos = _unitOfWork.RepositorioGrupoProduto.ListaNomeTamanhoProdutoPorGrupoProdutoId(id);

            var retorno = new List<string>();
            retorno.AddRange(nomeProdutos);
           // retorno.AddRange(nomeTamanhoProdutos);

            return retorno;
        }

        /// <summary>
        /// Método responsável por excluir um grupo de produto
        /// </summary>
        /// <param name="id"></param>
        public IEnumerable<ChangeLog> Excluir(int id)
        {
            var grupoProduto = _unitOfWork.RepositorioGrupoProduto.SelecionarPorId(id);
            if (grupoProduto == null)
                return new List<ChangeLog>();
            var alteracoes =_unitOfWork.RepositorioGrupoProduto.Excluir(grupoProduto.Id);
            return alteracoes;
        }

        public void ExcluirGrupoProdutoTamanhoProduto(int grupoProdutoId)
        {
            var gruposProdutosTamanho = _unitOfWork.RepositorioGrupoProduto.ListarGrupoProdutoTamanhoProduto(grupoProdutoId);
            foreach(var item in gruposProdutosTamanho)
                _unitOfWork.RepositorioGrupoProduto.ExcluirGrupoProdutoTamanhoProduto(item);
        }

        public void DesfazerGrupoProdutoTamanhoProduto(int grupoProdutoId)
        {
            var gruposProdutosTamanho = _unitOfWork.RepositorioGrupoProduto.ListarGrupoProdutoTamanhoProduto(grupoProdutoId);
            foreach (var item in gruposProdutosTamanho)
                _unitOfWork.RepositorioGrupoProduto.DesfazerGrupoProdutoTamanhoProduto(item);
        }

        /// <summary>
        /// Método responsável por desfazer a exclusão
        /// </summary>
        /// <param name="id"></param>
        public void Desfazer(int id)
        {
            var grupoProduto = _unitOfWork.RepositorioGrupoProduto.SelecionarPorIdSemVerificarExcluido(id);
            if (grupoProduto == null)
                throw new Exception(_mensagens.GetMensagem("GrupoProdutoNaoCadastrado"));
            _unitOfWork.RepositorioGrupoProduto.DesfazExclusao(grupoProduto.Id);
        }
        /// <summary>
        /// Método responsável por verificar se o grupo de produto está excluído
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Excluido(int id)
        {
            return _unitOfWork.RepositorioGrupoProduto.RetornarAtributoExcluidoPorId(id);
        }
        /// <summary>
        /// Método respnsável por desvincular o grupo de produtos dos produtos e tamanhos de produtos 
        /// que estão vinculados ao mesmo
        /// </summary>
        /// <param name="id"></param>
        private void DesvincularGrupoProduto(int id)
        {
            // Seleciona os nomes dos produtos e tamanhos de produtos
            var nomeProdutos = _unitOfWork.RepositorioGrupoProduto.ListaNomeProdutoGrupoPorProdutoId(id);
            var nomeTamanhoProdutos = _unitOfWork.RepositorioGrupoProduto.ListaNomeTamanhoProdutoPorGrupoProdutoId(id);
            if (nomeProdutos != null && nomeProdutos.Count > 0)
            {
                using (var servicoProduto = new ServicoProduto(this._unitOfWork))
                    servicoProduto.DesvincularGrupoProduto(nomeProdutos);
            }
        }

        /// <summary>
        /// Método responsável por remover os Grupos de Produto que estão marcado para ser excluído do banco de dados
        /// </summary>
        public void RemoverBancoDados()
        {
            var gruposProdutosIds = _unitOfWork.RepositorioGrupoProduto.ListarTodosIdsGrupoProdutoExcluido();

            foreach (var grupoProdutoId in gruposProdutosIds)
            {
                using (var servicoTamanhoProduto = new ServicoTamanhoProduto(this._unitOfWork))
                using (var servicoProduto = new ServicoProduto(this._unitOfWork))
                {
                    servicoProduto.DesvincularGrupoProduto(grupoProdutoId);
                    servicoTamanhoProduto.DesvincularGrupoProduto(grupoProdutoId);
                    _unitOfWork.RepositorioGrupoProduto.RemoverBanco(grupoProdutoId);
                }
            }
        }

        /// <summary>
        /// Método responsável por remover os Grupos de Produto que estão marcado para ser excluído do banco de dados
        /// Utilizado no teste automatizado
        /// </summary>
        /// <param name="id"></param>
        public void RemoverBancoDados(int id)
        {
            _unitOfWork.RepositorioGrupoProduto.RemoverBanco(id);
        }
    }
}
