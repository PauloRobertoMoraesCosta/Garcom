using Garcom.Dominio.Entidade.Models;
using Garcom.Infra.UnitOfWork;
using System;

namespace Garcom.Dominio.Servico.Validacao
{
    /// <summary>
    /// Classe responsável pela validação do usuário
    /// </summary>
    public class ValidacaoUsuario : ValidacaoBase<Usuario>
    {
        public ValidacaoUsuario(IUnitOfWork unitOfWork)
            :base(unitOfWork)
        {}

        /// <summary>
        /// Método responsável por realizar a validação antes de incluir um novo usuário
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public override bool ValidaInclusao(Usuario entidade)
        {
            if (!base.ValidaInclusao(entidade))
                return false;

            var usuario = _unitOfWork.RepositorioUsuario.SelecionaUsuarioPorLogin(entidade.Login);

            if (usuario != null)
                throw new Exception(_mensagens.GetMensagem("UsuarioJaCadastrado"));

            return true;
        }

        /// <summary>
        /// Método responsável por validar o usuário que está sendo autenticado
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="senha"></param>
        /// <returns></returns>
        public bool ValidaUsuarioAutenticar(Usuario usuario, string senha)
        {
            if (usuario == null)
                throw new Exception(_mensagens.GetMensagem("UsuarioInvalido"));

            if (!usuario.Ativo)
                throw new Exception(_mensagens.GetMensagem("UsuarioInativo"));
            
            if (usuario.Senha != senha)
                throw new Exception(_mensagens.GetMensagem("SenhaInvalida"));

            return true;
        }

        /// <summary>
        /// Método responsável por validar a senha do novo usuário
        /// </summary>
        /// <param name="senha"></param>
        /// <param name="confirmacaoSenha"></param>
        /// <returns></returns>
        public bool ValidaSenha(string senha, string confirmacaoSenha)
        {
            if (senha != confirmacaoSenha)
                throw new Exception(_mensagens.GetMensagem("ConfirmacaoSenhaDiferente"));

            return true;
        }
        
    }
}
