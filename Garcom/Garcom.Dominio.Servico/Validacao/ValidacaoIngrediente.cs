using Garcom.Dominio.Entidade.Models;
using Garcom.Infra.UnitOfWork;
using System;

namespace Garcom.Dominio.Servico.Validacao
{
    public class ValidacaoIngrediente : ValidacaoBase<Ingrediente>
    {
        public ValidacaoIngrediente(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override bool ValidaInclusao(Ingrediente entidade)
        {
            if (!base.ValidaInclusao(entidade))
                return false;

            ValidaDescricao(entidade.Descricao, entidade.Id);
            return true;
        }

        public override bool ValidaAlteracao(Ingrediente entidade)
        {
            if (!base.ValidaAlteracao(entidade))
                return false;

            ValidaDescricao(entidade.Descricao, entidade.Id);
            return true;
        }

        private void ValidaDescricao(string descricao, int id)
        {
            if (_unitOfWork.RepositorioIngrediente.SelecionaPelaDescricao(descricao, id) != null)
                throw new Exception(_mensagens.GetMensagem("IngredienteJaCadastrado"));
        }
    }
}
