using Garcom.Aplicacao.Interfaces;
using Garcom.Dominio.Entidade.DTOs;
using Garcom.Dominio.Entidade.Models;
using Garcom.Dominio.Servico.Servico;
using Garcom.Infra.UnitOfWork;
using System.Collections.Generic;

namespace Garcom.Aplicacao.Implementacao
{
    public class AppMesa : AppBase<MesaDTO>, IAppMesa
    {
        private ServicoMesa _servico;

        public AppMesa(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
            _servico = new ServicoMesa(unitOfWork);
        }

        public override MesaDTO Incluir(MesaDTO dto)
        {
            var mesa = _imap.Map<Mesa>(dto);
            _unitOfWork.Transacao();
            mesa = _servico.Incluir(mesa);
            _unitOfWork.Salvar();
            using (var serviceAuditoria = new ServicoAuditoria(this._unitOfWork))
                serviceAuditoria.InclusaoRegistro(dto.UsuarioLogado, "Mesa", mesa.Id);
            _unitOfWork.SalvarECommit();

            return _imap.Map<MesaDTO>(mesa);
        }

        public override MesaDTO Alterar(MesaDTO dto)
        {
            var mesa = _imap.Map<Mesa>(dto);
            _unitOfWork.Transacao();
            mesa = _servico.Altera(mesa);
            var alteracoes = _unitOfWork.RetornaModificacoes();
            using (var serviceAuditoria = new ServicoAuditoria(this._unitOfWork))
                serviceAuditoria.RegistraAuditoria(dto.UsuarioLogado, alteracoes);
            _unitOfWork.SalvarECommit();
            return dto;
        }

        public void Desfazer(MesaDTO mesa)
        {
            _unitOfWork.Transacao();
            _servico.Desfazer(mesa.Id);
            var alteracoes = _unitOfWork.RetornaModificacoes();
            using (var serviceAuditoria = new ServicoAuditoria(this._unitOfWork))
                serviceAuditoria.RegistraAuditoria(mesa.UsuarioLogado, alteracoes);
            _unitOfWork.SalvarECommit();
        }

        public void Excluir(MesaDTO mesaDTO)
        {
            _unitOfWork.Transacao();
            _servico.Excluir(mesaDTO.Id);
            using (var serviceAuditoria = new ServicoAuditoria(this._unitOfWork))
                serviceAuditoria.ExclusaoRegistro(mesaDTO.UsuarioLogado, "Mesa", mesaDTO.Id);
            _unitOfWork.SalvarECommit();
        }

        public ICollection<MesaDTO> ListarTodasAtivas()
        {
            var mesas = _servico.ListarTodasAtivas();
            var mesasDTO = _imap.Map<IEnumerable<Mesa>, ICollection<MesaDTO>>(mesas);
            return mesasDTO;
        }

        public override ICollection<MesaDTO> ListarTodos()
        {
            var mesas = _servico.ListarTodas();
            var mesasDTO = _imap.Map<IEnumerable<Mesa>, ICollection<MesaDTO>>(mesas);
            return mesasDTO;
        }

        public MesaDTO SelecionaPorDescricao(string descricao)
        {
            var mesa = _servico.SelecionaPorDescricao(descricao);
            return _imap.Map<MesaDTO>(mesa);
        }

        public override MesaDTO SelecionarPorId(int id)
        {
            var mesa = _servico.SelecionaPorId(id);
            return _imap.Map<MesaDTO>(mesa);
        }

        public override void Dispose()
        {
            if (_servico != null)
                _servico.Dispose();
        }
    }
}
