using Garcom.Dominio.Entidade.Models.Excecao;
using Garcom.Infra.UnitOfWork;
using System;

namespace Garcom.Dominio.Servico.Servico
{
    public class ServicoExcecao : IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;

        public ServicoExcecao(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Método responsável por incluir uma exceção
        /// </summary>
        /// <param name="excecao"></param>
        public void Incluir(Excecao excecao)
        {
            this._unitOfWork.RepositorioExcecao.Incluir(excecao);
        }

        public void Dispose()
        {
        }
    }
}
