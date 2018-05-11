using Garcom.Dominio.Entidade.Models;
using Garcom.Infra.UnitOfWork;
using System;

namespace Garcom.Dominio.Servico.Validacao
{
    public class ValidacaoProduto : ValidacaoBase<Produto>
    {
        public ValidacaoProduto(IUnitOfWork unitOfWork)
            : base (unitOfWork)
        {}

        public override bool ValidaInclusao(Produto entidade)
        {
            if (!base.ValidaInclusao(entidade))
                return false;

            Valida(entidade);

            return true;
        }

        public override bool ValidaAlteracao(Produto entidade)
        {
            if (!base.ValidaAlteracao(entidade))
                return false;

            Valida(entidade);
            return true;
        }

        private bool Valida(Produto entidade)
        {
            entidade.ValidarValor();

            if (_unitOfWork.RepositorioProduto.SelecionaPorNomeComIdDiferente(entidade.Id, entidade.Nome) != null)
                throw new Exception(_mensagens.GetMensagem("ProdutoJaCadastrado"));

            if ((!string.IsNullOrWhiteSpace(entidade.CodigoRapido)) &&
               (_unitOfWork.RepositorioProduto.SelecionaPorCodigoRapidoComIdDiferente(entidade.CodigoRapido, entidade.Id) != null))
                throw new Exception(_mensagens.GetMensagem("CodigoRapidoJaCadastrado"));
                        
            return true;
        }
    }
}
