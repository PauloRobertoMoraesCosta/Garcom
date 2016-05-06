using Garcom.Aplicacao.Interfaces;
using Garcom.Aplicacao.Verifications;
using Garcom.Dados.Verifications;
using Garcom.Dominio.Entidades;
using Garcom.Dominio.Interfaces.Servicos;
using System;

namespace Garcom.Aplicacao
{
    public class AppServiceUsuario : AppServiceBase<Usuario>, IAppServiceUsuario
    {
        private readonly IServicoUsuario _serviceUsuario;

        public AppServiceUsuario(IServicoUsuario serviceUsuario) : base(serviceUsuario)
        {
            _serviceUsuario = serviceUsuario;
        }


        public Usuario logaUsuario(string login, string senha)
        {
            return _serviceUsuario.LogaUsuario(login, senha);
        }
    }
}
