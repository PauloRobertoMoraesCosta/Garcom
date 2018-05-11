using Garcom.Core;
using Garcom.Dominio.Entidade.DTOs;
using Garcom.Dominio.Entidade.Models;
using Garcom.Dominio.Entidade.Models.Auditoria;
using Garcom.Dominio.Servico.Validacao;
using Garcom.Infra.UnitOfWork;
using System;
using System.Collections.Generic;

namespace Garcom.Dominio.Servico.Servico
{
    /// <summary>
    /// Classe responsável por orquestra o Usuario
    /// </summary>
    public class ServicoUsuario : ServicoBase<Usuario>
    {

        /// <summary>
        /// Construtor
        /// </summary>
        public ServicoUsuario(IUnitOfWork unitOfWork)
                : base(unitOfWork, new ValidacaoUsuario(unitOfWork))
        {
        }

        /// <summary>
        /// Método responsável por incluir um usuário no banco de dados
        /// </summary>
        /// <param name="usuarioDTO"></param>
        /// <returns></returns>
        public Usuario Incluir(Usuario usuario)
        {
            if (usuario.DataCadastro == null) usuario.DataCadastro = DateTime.Now;
            usuario.Ativo = true;
            _validador.ValidaInclusao(usuario);
            usuario.Senha = Seguranca.EncryptString(usuario.Senha);
            usuario = this._unitOfWork.RepositorioUsuario.Incluir(usuario);
            return usuario;
        }

        /// <summary>
        /// Método responsável por alterar o cadastro do usuário e cadastrar a auditoria
        /// </summary>
        /// <param name="usuarioAlterado"></param>
        /// <returns></returns>
        public Usuario Alterar(Usuario usuarioAlterado)
        {
            var usuario = this._unitOfWork.RepositorioUsuario.SelecionarPorId(usuarioAlterado.Id);
            if (usuario == null)
                throw new Exception(_mensagens.GetMensagem("UsuarioNaoCadastrado"));
            
            usuario.Ativo = usuarioAlterado.Ativo;
            usuario.Nome = usuarioAlterado.Nome;
            usuario.PerfilId = usuarioAlterado.PerfilId;
            if (!string.IsNullOrWhiteSpace(usuarioAlterado.Senha))
                usuario.Senha = usuarioAlterado.Senha;
            else
                usuario.Senha = Seguranca.DecryptString(usuario.Senha);
            _validador.ValidaAlteracao(usuario);
            usuario.Senha = Seguranca.EncryptString(usuario.Senha);
            return this._unitOfWork.RepositorioUsuario.Alterar(usuario);
        }

        /// <summary>
        /// Método responsável por retornar todos os usuários cadastrados no banco de dados
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Usuario> ListarTodos()
        {
            var usuarios = this._unitOfWork.RepositorioUsuario.ListaTodosUsuarios();
            return usuarios;
        }

        /// <summary>
        /// Método responsável por selecionar um usuário
        /// </summary>
        /// <param name="login">Login do usuário</param>
        /// <returns>Usuario</returns>
        public Usuario SelecionarPorLogin(string login)
        {
            var usuario = this._unitOfWork.RepositorioUsuario.SelecionaUsuarioPorLogin(login);
            if (usuario == null)
                throw new Exception(_mensagens.GetMensagem("UsuarioNaoCadastrado"));
            
            return usuario;
        }

        /// <summary>
        /// Método responsável por selecionar um usuário pelo id
        /// </summary>
        /// <param name="id">Login do usuário</param>
        /// <returns>Usuario</returns>
        public Usuario SelecionarPorId(int id)
        {
            var usuario = this._unitOfWork.RepositorioUsuario.SelecionaUsuarioPorId(id);
            if (usuario == null)
                throw new Exception(_mensagens.GetMensagem("UsuarioNaoCadastrado"));
            
            return usuario;
        }

        /// <summary>
        /// Método responsável por validar os dados de login do usuário
        /// </summary>
        /// <param name="login">Login</param>
        /// <param name="senha">Senha</param>
        /// <returns></returns>
        public Usuario Autenticar(string login, string senha)
        {
            var usuario = this._unitOfWork.RepositorioUsuario.SelecionaUsuarioPorLogin(login);
            senha = Seguranca.EncryptString(senha);
            (_validador as ValidacaoUsuario).ValidaUsuarioAutenticar(usuario, senha);

            return usuario;

        }

        /// <summary>
        /// Método responsável por listar todos os perfieis cadastrados
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Perfil> ListaTodosPerfil()
        {
            var perfieis = this._unitOfWork.RepositorioUsuario.ListaTodosPerfil();
            return perfieis;
        }

        public void ExcluirPeloLoginBanco(string login)
        {
            var usuario = _unitOfWork.RepositorioUsuario.SelecionaUsuarioPorLogin(login);
            if (usuario == null)
                return;
            _unitOfWork.RepositorioUsuario.RemoverBanco(usuario.Id);
        }
    }
}
