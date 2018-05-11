using AutoMapper;
using Garcom.Core;
using Garcom.Dominio.Entidade.Mapeamento;
using Garcom.Dominio.Entidade.Models;
using Garcom.Dominio.Servico.Validacao;
using Garcom.Infra.UnitOfWork;
using System;

namespace Garcom.Dominio.Servico.Servico
{
    /// <summary>
    /// Classe base do servico
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ServicoBase<T> : IDisposable where T : ClasseBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected ValidacaoBase<T> _validador;
        protected readonly IMapper _imap;
        protected readonly IGerenciadorMensagens _mensagens;

        public ValidacaoBase<T> Validador
        {
            get
            {
                return _validador;
            }
        }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="validador"></param>
        public ServicoBase(IUnitOfWork unitOfWork, ValidacaoBase<T> validador)
        {
            this._unitOfWork = unitOfWork;
            this._validador = validador;
            this._imap = MapperConfig.imap;
            _mensagens = new GerenciadorMensagensRetorno();
        }

        public void Dispose()
        {
        }
    }
}
