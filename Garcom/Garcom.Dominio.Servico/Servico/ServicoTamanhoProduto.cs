using Garcom.Dominio.Entidade.DTOs;
using Garcom.Dominio.Entidade.Models;
using Garcom.Dominio.Servico.Validacao;
using Garcom.Infra.UnitOfWork;
using System;
using System.Collections.Generic;

namespace Garcom.Dominio.Servico.Servico
{
    /// <summary>
    /// Classe serviço do Tamanho de Produto
    /// </summary>
    public class ServicoTamanhoProduto : ServicoBase<TamanhoProduto>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ServicoTamanhoProduto(IUnitOfWork unitOfWork)
            : base(unitOfWork, new ValidacaoTamanhoProduto(unitOfWork))
        {}

        /// <summary>
        /// Método responsável por incluir o tamanho de produto
        /// </summary>
        /// <param name="tamanhoProdutoDTO"></param>
        /// <returns></returns>
        public TamanhoProduto Incluir(TamanhoProduto tamanhoProduto)
        {
            if (tamanhoProduto != null && tamanhoProduto.DataCadastro == DateTime.MinValue) tamanhoProduto.DataCadastro = DateTime.Now;
            tamanhoProduto.Ativo = true;
            _validador.ValidaInclusao(tamanhoProduto);
            tamanhoProduto = _unitOfWork.RepositorioTamanhoProduto.Incluir(tamanhoProduto);
            return tamanhoProduto;
        }

        /// <summary>
        /// Método responsável por realizar a alteração do tamanho do produto
        /// </summary>
        /// <param name="tamanhoProdutoDTO"></param>
        /// <returns></returns>
        public TamanhoProduto Alterar(TamanhoProduto tamanhoProdutoAlterado)
        {
            var tamanhoProduto = this._unitOfWork.RepositorioTamanhoProduto.SelecionarPorId(tamanhoProdutoAlterado.Id);
            if (tamanhoProduto == null)
                throw new Exception(_mensagens.GetMensagem("TamanhoNaoCadastrado"));
            tamanhoProduto.Ativo = tamanhoProdutoAlterado.Ativo;
            tamanhoProduto.Nome = tamanhoProdutoAlterado.Nome;
            _validador.ValidaAlteracao(tamanhoProduto);
            tamanhoProduto = this._unitOfWork.RepositorioTamanhoProduto.Alterar(tamanhoProduto);
            return tamanhoProduto;
        }

        /// <summary>
        /// Método responsável por listar todos os tamanhos de produtos
        /// </summary>
        /// <returns></returns>
        public ICollection<TamanhoProduto> ListarTodos()
        {
            var tamanhos = this._unitOfWork.RepositorioTamanhoProduto.ListarTodos();
            return tamanhos;
        }

        /// <summary>
        /// Método responsável por selecionar o tamanho do produto pelo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TamanhoProduto SelecionarPorId(int id)
        {
            var tamanho = this._unitOfWork.RepositorioTamanhoProduto.SelecionarPorId(id);
            return tamanho;
        }

        /// <summary>
        /// Método responsável retornar se o tamanho de produto possui vinculo com algum produto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool TamanhoPossuiProduto(int id)
        {
            return this._unitOfWork.RepositorioTamanhoProduto.PossuiProdutoVinculado(id);
        }

        /// <summary>
        /// Método responsável por desvincular o Grupo de Produto do Tamanho 
        /// </summary>
        /// <param name="grupoProdutoId"></param>
        public void DesvincularGrupoProduto(int grupoProdutoId)
        {
            var tamanhosProdutos = this._unitOfWork.RepositorioTamanhoProduto.ListarPorGrupoProdutoId(grupoProdutoId);
            foreach (var tamanhoProduto in tamanhosProdutos)
                this._unitOfWork.RepositorioTamanhoProduto.Alterar(tamanhoProduto);
        }

        /// <summary>
        /// Método responsável por verificar quais produtos estão vinculados ao tamanho
        /// e retornar os nomes dos mesmos
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ICollection<string> ValidarExclusao(int id)
        {
            var lista = new List<string>();
            var nomeProdutos = this._unitOfWork.RepositorioTamanhoProduto.ListarNomeProdutosPorTamanhoId(id);
            var nomeGrupoProdutos = this._unitOfWork.RepositorioGrupoProduto.ListarNomeTamanhoId(id);
            lista.AddRange(nomeProdutos);
            lista.AddRange(nomeGrupoProdutos);
            return lista;
        }

        /// <summary>
        /// Método responsável por realizar a exclusão
        /// </summary>
        /// <param name="tamanhoProdutoDTO"></param>
        public void Excluir(int id)
        {
            var tamanhoProduto = this._unitOfWork.RepositorioTamanhoProduto.SelecionarPorId(id);
            if (tamanhoProduto == null)
                return;
            this._unitOfWork.RepositorioTamanhoProduto.Excluir(tamanhoProduto.Id);
            var tamanhosProdutosValorsIds = this._unitOfWork.RepositorioTamanhoProdutoValor.ListarPorTamanhoId(tamanhoProduto.Id);
            var produtosIngredientesTamanhosIds = this._unitOfWork.RepositorioProdutoIngredienteTamanho.ListarPorTamanhoId(tamanhoProduto.Id);
            foreach (var tamanhoProdutoValorId in tamanhosProdutosValorsIds)
                this._unitOfWork.RepositorioTamanhoProdutoValor.Excluir(tamanhoProdutoValorId);
            foreach (var produtoIngredienteTamanhosId in produtosIngredientesTamanhosIds)
                this._unitOfWork.RepositorioProdutoIngredienteTamanho.Excluir(produtoIngredienteTamanhosId);
        }

        public void ExcluirGrupoProdutoTamanhoProduto(int tamanhoId)
        {
            var gruposProdutosTamanho = _unitOfWork.RepositorioGrupoProduto.ListarGrupoProdutoTamanhoProdutoPorTamanhoId(tamanhoId);
            foreach (var item in gruposProdutosTamanho)
                _unitOfWork.RepositorioGrupoProduto.ExcluirGrupoProdutoTamanhoProduto(item);
        }

        /// <summary>
        /// Método responsável por desfazer a exclusão do tamanho
        /// </summary>
        /// <param name="tamanho"></param>
        public void Desfazer(int id)
        {
            var tamanhoProduto = _unitOfWork.RepositorioTamanhoProduto.SelecionarPorId(id);
            if (tamanhoProduto == null)
                throw new Exception(_mensagens.GetMensagem("TamanhoNaoCadastrado"));
            var tamanhosProdutosValorsIds = this._unitOfWork.RepositorioTamanhoProdutoValor.ListarPorTamanhoId(tamanhoProduto.Id);
            var produtosIngredientesTamanhosIds = this._unitOfWork.RepositorioProdutoIngredienteTamanho.ListarPorTamanhoId(tamanhoProduto.Id);
            foreach (var tamanhoProdutoValorId in tamanhosProdutosValorsIds)
                this._unitOfWork.RepositorioTamanhoProdutoValor.DesfazExclusao(tamanhoProdutoValorId);
            foreach (var produtoIngredienteTamanhosId in produtosIngredientesTamanhosIds)
                this._unitOfWork.RepositorioProdutoIngredienteTamanho.DesfazExclusao(produtoIngredienteTamanhosId);
            _unitOfWork.RepositorioTamanhoProduto.DesfazExclusao(tamanhoProduto.Id);
        }

        public void DesfazerGrupoProdutoTamanhoProduto(int grupoProdutoId)
        {
            var gruposProdutosTamanho = _unitOfWork.RepositorioGrupoProduto.ListarGrupoProdutoTamanhoProdutoPorTamanhoId(grupoProdutoId);
            foreach (var item in gruposProdutosTamanho)
                _unitOfWork.RepositorioGrupoProduto.DesfazerGrupoProdutoTamanhoProduto(item);
        }

        public ICollection<string> NomeProdutosVinculos(int grupoProdutoId, int tamanhoId)
        {
            return _unitOfWork.RepositorioTamanhoProduto.NomeProdutosVinculos(grupoProdutoId, tamanhoId);
        }

        /// <summary>
        /// Método responsável por remover do banco os Tamanho Produto que estão marcados para serem excluídos
        /// </summary>
        public void RemoverBanco()
        {
            var tamanhosProdutosIds = _unitOfWork.RepositorioTamanhoProduto.ListarTodosIdsIngredienteExcluido();

            foreach (var tamanhoProdutoId in tamanhosProdutosIds)
                _unitOfWork.RepositorioTamanhoProduto.RemoverBanco(tamanhoProdutoId);
        }

        /// <summary>
        /// Método responsável por remover do banco os Tamanho Produto que estão marcados para serem excluídos.
        /// Utilizado no teste automatizado
        /// </summary>
        /// <param name="id"></param>
        public void RemoverBanco(int id)
        {
            _unitOfWork.RepositorioTamanhoProduto.RemoverBanco(id);
        }
    }
}
