using Garcom.Dominio.Entidade.Models;
using Garcom.Infra.UnitOfWork;
using System;

namespace Garcom.Dominio.Servico.Validacao
{
    /// <summary>
    /// Classe de validaçao do Tamnho do Grupo
    /// </summary>
    public class ValidacaoTamanhoProduto : ValidacaoBase<TamanhoProduto>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public ValidacaoTamanhoProduto(IUnitOfWork unitOfWork)
            : base (unitOfWork)
        {}

        /// <summary>
        /// Método responsável pela validação antes da inclusão
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public override bool ValidaInclusao(TamanhoProduto entidade)
        {
            if (!base.ValidaInclusao(entidade))
                return false;

            var tamanhoProduto = _unitOfWork.RepositorioTamanhoProduto.SelecionaPorNome(entidade.Nome);

            if (tamanhoProduto != null)
                throw new Exception(_mensagens.GetMensagem("TamanhoJaCadastrado"));
                
            return true;
        }

        /// <summary>
        /// Método responsável pela validação antes da alteração
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public override bool ValidaAlteracao(TamanhoProduto entidade)
        {
            if (!base.ValidaAlteracao(entidade))
                return false;

            var tamanhoProduto = _unitOfWork.RepositorioTamanhoProduto.SelecionaPorNomeComIdDiferente(entidade.Id, entidade.Nome);

            if (tamanhoProduto != null)
                throw new Exception(_mensagens.GetMensagem("TamanhoJaCadastrado"));

            return true;
        }
    }
}
