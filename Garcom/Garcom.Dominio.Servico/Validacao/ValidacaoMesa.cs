using Garcom.Dominio.Entidade.Models;
using Garcom.Infra.UnitOfWork;
using System;

namespace Garcom.Dominio.Servico.Validacao
{
    public class ValidacaoMesa : ValidacaoBase<Mesa>
    {
        /// <summary>
        /// Classe responsável por validar alterações no db da table Mesa
        /// </summary>
        public ValidacaoMesa(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {}

        public override bool ValidaInclusao(Mesa entidade)
        {
            if (!base.ValidaInclusao(entidade))
                return false;

            var mesa = _unitOfWork.RepositorioMesa.SelecionaPorDescricao(entidade.Descricao);
                        
            if (mesa != null)
                throw new Exception(_mensagens.GetMensagem("MesaJaCadastrado"));

            return true;
        }

        public override bool ValidaAlteracao(Mesa entidade)
        {
            if (!base.ValidaAlteracao(entidade))
                return false;

            var mesa = _unitOfWork.RepositorioMesa.SelecionaPorDescricaoComIdDiferente(entidade.Id, entidade.Descricao);

            if (mesa != null)
                throw new Exception(_mensagens.GetMensagem("MesaJaCadastrado"));

            return true;
        }
    }
}
