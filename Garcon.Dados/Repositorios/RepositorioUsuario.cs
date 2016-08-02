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
            Usuario usuarioBanco;
            

            try
            {
                usuarioBanco = db.Usuarios.AsNoTracking().FirstOrDefault(u => u.Login.Equals(loginLimpo));
                if (usuarioBanco == null)
                    throw new DadosException("Usuario não cadastrado!");

                usuarioBanco = db.Usuarios.AsNoTracking().FirstOrDefault(u => u.Login.Equals(loginLimpo) && u.Senha.Equals(senhaLimpa));
                if (usuarioBanco == null)
                    throw new DadosException("Senha Inválida!");
                    
                return usuarioBanco;
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

