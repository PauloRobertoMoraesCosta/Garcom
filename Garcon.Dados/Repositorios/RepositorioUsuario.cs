using Garcom.Dados.Verifications;
using Garcom.Dominio.Entidades;
using Garcom.Dominio.Interfaces.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Garcom.Dados.Repositorios
{
    public class RepositorioUsuario : RepositorioBase<Usuario>, IRepositorioUsuario
    {
        public Usuario logaUsuario(String login, String senha)
        {
            string loginLimpo = VerificacoesBanco.LimpaCaracteresEspeciais(login);
            string senhaLimpa = VerificacoesBanco.LimpaCaracteresEspeciais(senha);
            IEnumerable<Usuario> usuarioBanco;
            Usuario usuarioLogado;

            try
            {
                usuarioBanco = from u in db.Usuarios where u.Login.Equals(loginLimpo) select u;
                if (!usuarioBanco.Any())
                    throw new DadosException("Usuario não cadastrado!");

                usuarioBanco = from u in db.Usuarios where u.Login.Equals(loginLimpo) && u.Senha.Equals(senhaLimpa) select u;
                if (!usuarioBanco.Any())
                    throw new DadosException("Senha Inválida!");
                usuarioLogado = (Usuario)usuarioBanco.ElementAt(0);
                return usuarioLogado;
            }
            catch (DadosException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Problema inesperado ao fazer login no banco! " + ex.Message);
            }
        }
    }
}

