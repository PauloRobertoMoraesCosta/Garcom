using Garcom.Aplicacao.Interfaces;
using Garcom.Dominio.Entidade.DTOs;
using Garcom.Dominio.Entidade.Models;
using Garcom.Dominio.Servico.Servico;
using Garcom.Dominio.Servico.Validacao;
using Garcom.Infra.UnitOfWork;
using System.Collections.Generic;

namespace Garcom.Aplicacao.Implementacao
{
    public class AppUsuario : AppBase<UsuarioDTO>, IAppUsuario
    {
        private ServicoUsuario _servico;

        public AppUsuario(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _servico = new ServicoUsuario(this._unitOfWork);
        }

        public override UsuarioDTO Alterar(UsuarioDTO dto)
        {
            (_servico.Validador as ValidacaoUsuario).ValidaSenha(dto.Senha, dto.ConfirmacaoSenha);
            _unitOfWork.Transacao();
            var usuario = _imap.Map<Usuario>(dto);
            usuario = _servico.Alterar(usuario);
            var alteracoes = _unitOfWork.RetornaModificacoes();
            using (var serviceAuditoria = new ServicoAuditoria(this._unitOfWork))
                serviceAuditoria.RegistraAuditoria(dto.UsuarioLogado, alteracoes);
            _unitOfWork.SalvarECommit();
            dto.Senha = string.Empty;
            return dto;
        }

        public override UsuarioDTO Incluir(UsuarioDTO dto)
        {
            (_servico.Validador as ValidacaoUsuario).ValidaSenha(dto.Senha, dto.ConfirmacaoSenha);
            _unitOfWork.Transacao();
            var usuario = _imap.Map<Usuario>(dto);
            _servico.Incluir(usuario);
            this._unitOfWork.Salvar();
            using (var serviceAuditoria = new ServicoAuditoria(this._unitOfWork))
                serviceAuditoria.InclusaoRegistro(dto.UsuarioLogado, "Usuario", usuario.Id);
            this._unitOfWork.SalvarECommit();
            return _imap.Map<UsuarioDTO>(usuario);
        }

        public override ICollection<UsuarioDTO> ListarTodos()
        {
            var usuarios = _servico.ListarTodos();
            return _imap.Map<IEnumerable<Usuario>, ICollection<UsuarioDTO>>(usuarios);
        }

        public ICollection<PerfilDTO> ListaTodosPerfil()
        {
            var perfieis = _servico.ListaTodosPerfil();
            return _imap.Map<IEnumerable<Perfil>, ICollection<PerfilDTO>>(perfieis);
        }
        
        public override UsuarioDTO SelecionarPorId(int id)
        {
            var usuario = _servico.SelecionarPorId(id);
            var usuarioDTO = _imap.Map<UsuarioDTO>(usuario);
            usuarioDTO.Senha = null;

            return usuarioDTO;
        }

        public UsuarioDTO SelecionarPorLogin(string login)
        {
            var usuario = _servico.SelecionarPorLogin(login);
            var usuarioDTO = _imap.Map<UsuarioDTO>(usuario);
            usuarioDTO.Senha = null;

            return usuarioDTO;
        }

        public UsuarioDTO Autenticar(string login, string senha)
        {
            var usuario = _servico.Autenticar(login, senha);
            usuario.Senha = string.Empty;
            return _imap.Map<UsuarioDTO>(usuario);
        }

        public override void Dispose()
        {
            if (_servico != null)
                _servico.Dispose();
        }
    }
}
