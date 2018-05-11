using Garcom.Core;
using Garcom.Dominio.Entidade.Models;
using Garcom.Infra.UnitOfWork;
using System;

namespace Garcom.Dominio.Servico.Validacao
{
    public abstract class ValidacaoBase<T> : IDisposable where T : ClasseBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IGerenciadorMensagens _mensagens;

        public ValidacaoBase(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            _mensagens = new GerenciadorMensagensRetorno();
        }

        /// <summary>
        /// Valida se pode incluir a entidade
        /// </summary>
        /// <param name="entidade">Entidade</param>
        /// <returns>True</returns>
        public virtual bool ValidaInclusao(T entidade)
        {
            if (entidade == null)
                throw new Exception("Entidade não pode ser nula");

            if (!entidade.Validacao())
                return false;

            return true;
        }

        /// <summary>
        /// Valida se pode alterar a entidade 
        /// </summary>
        /// <param name="entidade">Entidade</param>
        /// <returns>True</returns>
        public virtual bool ValidaAlteracao(T entidade)
        {
            if (entidade == null)
                throw new Exception("Entidade não pode ser nula");

            if (!entidade.Validacao())
                return false;

            return true;
        }

        /// <summary>
        /// Limpa o objeto da memoria
        /// </summary>
        public void Dispose()
        {
        }
    }
}
