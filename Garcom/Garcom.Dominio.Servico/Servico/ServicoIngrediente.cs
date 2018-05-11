using Garcom.Dominio.Entidade.DTOs;
using Garcom.Dominio.Entidade.Models;
using Garcom.Dominio.Entidade.Models.Auditoria;
using Garcom.Dominio.Servico.Validacao;
using Garcom.Infra.UnitOfWork;
using System;
using System.Collections.Generic;

namespace Garcom.Dominio.Servico.Servico
{
    /// <summary>
    /// Classe responsável por orquestrar o ingrediente
    /// </summary>
    public class ServicoIngrediente : ServicoBase<Ingrediente>
    {

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ServicoIngrediente(IUnitOfWork unitOfWork) : base(unitOfWork, new ValidacaoIngrediente(unitOfWork))
        {
        }
        
        /// <summary>
        /// Método responsável por incluir um ingrediente no banco
        /// </summary>
        /// <param name="ingredienteDTO"></param>
        /// <returns></returns>
        public Ingrediente Incluir(Ingrediente ingrediente)
        {
            ingrediente.Ativo = true;
            _validador.ValidaInclusao(ingrediente);
            ingrediente = _unitOfWork.RepositorioIngrediente.Incluir(ingrediente);
            return ingrediente;
        }

        /// <summary>
        /// Método responsável por gravar um ingrediente no banco
        /// </summary>
        /// <param name="ingredienteDTO"></param>
        /// <returns></returns>
        public Ingrediente Alterar(Ingrediente ingredienteAlterado)
        {
            var ingrediente = _unitOfWork.RepositorioIngrediente.SelecionarPorId(ingredienteAlterado.Id);
            if (ingrediente == null)
                throw new Exception(_mensagens.GetMensagem("IngredienteNaoCadastrado"));

            ingrediente.Ativo = ingredienteAlterado.Ativo;
            ingrediente.Descricao = ingredienteAlterado.Descricao;
            _validador.ValidaAlteracao(ingrediente);
            return _unitOfWork.RepositorioIngrediente.Alterar(ingrediente);
        }

        /// <summary>
        /// Método responsável por listar todos os ingredientes no banco
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Ingrediente> ListarTodos()
        {
            var ingredientes = _unitOfWork.RepositorioIngrediente.ListarTodosIngrediente();
            return ingredientes;
        }

        /// <summary>
        /// Método responsável por selecionar um ingrediente pelo Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Ingrediente SelecionaPorId(int id)
        {
            var ingrediente = _unitOfWork.RepositorioIngrediente.SelecionarPorId(id);
            if (ingrediente == null)
                throw new Exception(_mensagens.GetMensagem("IngredienteNaoCadastrado"));
            return ingrediente;
        }

        /// <summary>
        /// Método para validar a exclusão
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ICollection<Produto> ValidaExclusao(int id)
        {
            var produtos = _unitOfWork.RepositorioIngrediente.ProdutosVinculados(id);
            return produtos;
        }

        /// <summary>
        /// Método responsável por excluir um ingrediente
        /// </summary>
        /// <param name="id"></param>
        public IEnumerable<ChangeLog> Excluir(int id)
        {
            var ing = _unitOfWork.RepositorioIngrediente.SelecionarPorId(id);
            if (ing == null)
                return new List<ChangeLog>();
            var alteracoes = _unitOfWork.RepositorioIngrediente.Excluir(id);
            _unitOfWork.RepositorioProdutoIngrediente.SetaProdutoIngredienteTamanhoExcluidoPorIngredienteId(ing.Id, true);
            _unitOfWork.RepositorioProdutoIngrediente.SetaProdutoIngredienteExcluido(ing.Id, true);
            return alteracoes;
        }

        /// <summary>
        /// Método responsável por desfazer a exclusão do ingrediente
        /// </summary>
        /// <param name="id"></param>
        public void Desfazer(int id)
        {
            var ing = _unitOfWork.RepositorioIngrediente.SelecionarPorId(id);
            if (ing == null)
                throw new Exception(_mensagens.GetMensagem("IngredienteNaoCadastrado"));
            
            _unitOfWork.RepositorioProdutoIngrediente.SetaProdutoIngredienteTamanhoExcluidoPorIngredienteId(ing.Id, false);
            _unitOfWork.RepositorioProdutoIngrediente.SetaProdutoIngredienteExcluido(ing.Id, false);
            _unitOfWork.RepositorioIngrediente.DesfazExclusao(id);
        }

        /// <summary>
        /// Método responsável por remover do banco de dados os Ingredientes marcados para serem excluídos
        /// </summary>
        public void RemoverBanco()
        {
            var ingredientesIds = _unitOfWork.RepositorioIngrediente.ListarTodosIdsIngredienteExcluido();

            foreach (var ingredienteId in ingredientesIds)
                _unitOfWork.RepositorioIngrediente.RemoverBanco(ingredienteId);
        }

        /// <summary>
        /// Método responsável por remover do banco de dados os Ingredientes marcados para serem excluídos
        /// Utilizado no teste automatizado
        /// </summary>
        /// <param name="id"></param>
        public void RemoverBanco(int id)
        {
            _unitOfWork.RepositorioIngrediente.RemoverBanco(id);
        }
    }
}
