using Garcom.Dominio.Entidades;
using Garcom.Dominio.Interfaces.Repositorios;
using Garcom.Dominio.verifications;
using System;
using System.Linq;

namespace Garcom.Dados.Repositorios
{
    public class RepositorioUsuario : RepositorioBase<Usuario>, IRepositorioUsuario
    {
        public Usuario logaUsuario(string login, string senha)
        {
            try
            {
                if (db.Usuarios.First(u => u.Login.Equals(login)) == null)
                {
                    throw new Exception("Usuario não cadastrado");
                }
                else
                {
                    Usuario usu = db.Usuarios.First(u => u.Login.Equals(login) && u.Senha.Equals(Verificacoes.limpaCaracteresEspeciais(senha)));
                    if (usu != null)
                    {
                        return usu;
                    }
                    else
                    {
                        throw new Exception("Senha Errada");
                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Problema ao fazer login! " + ex.Message);
            }
        }
    }
}

