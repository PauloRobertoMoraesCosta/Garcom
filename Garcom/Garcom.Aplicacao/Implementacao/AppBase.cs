using AutoMapper;
using Garcom.Aplicacao.Interfaces;
using Garcom.Core;
using Garcom.Dominio.Entidade.Mapeamento;
using Garcom.Infra.UnitOfWork;
using System.Collections.Generic;

namespace Garcom.Aplicacao.Implementacao
{
    public abstract class AppBase<DTO> : IAppBase<DTO>
        where DTO : class
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _imap;
        protected readonly IGerenciadorMensagens _mensagens;

        public abstract DTO Alterar(DTO dto);
        public abstract DTO Incluir(DTO dto);
        public abstract ICollection<DTO> ListarTodos();
        public abstract DTO SelecionarPorId(int id);

        public abstract void Dispose();

        public AppBase(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._imap = MapperConfig.imap;
            _mensagens = new GerenciadorMensagensRetorno();
        }
    }
}
