using Garcom.Dominio.Entidade.Models;
using Garcom.Infra.UnitOfWork;
using System;

namespace Garcom.Dominio.Servico.Validacao
{
    public class ValidacaoGrupoProduto : ValidacaoBase<GrupoProduto>
    {
        public ValidacaoGrupoProduto(IUnitOfWork unitOfWork)
            : base(unitOfWork) 
        {}

        public override bool ValidaInclusao(GrupoProduto entidade)
        {
            if (!base.ValidaInclusao(entidade))
                return false;
            var grupoProduto = _unitOfWork.RepositorioGrupoProduto.SelecionaPorNome(entidade.Nome);
            if (grupoProduto != null)
                throw new Exception(_mensagens.GetMensagem("GrupoProdutoJaCadastrado"));
            return true;
        }

        public override bool ValidaAlteracao(GrupoProduto entidade)
        {
            if (!base.ValidaAlteracao(entidade))
                return false;

            var grupoProduto = _unitOfWork.RepositorioGrupoProduto.SelecionaPorNomeComIdDiferente(entidade.Id, entidade.Nome);

            if (grupoProduto != null)
                throw new Exception(_mensagens.GetMensagem("GrupoProdutoJaCadastrado"));

            return true;
        }
    }
}
