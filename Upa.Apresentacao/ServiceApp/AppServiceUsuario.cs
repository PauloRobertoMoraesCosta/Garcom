using System;
using System.Collections.Generic;
using Upa.Apresentacao.InterfacesApp;
using Upa.Dominio.Entidades;
using Upa.Dominio.Interfaces.Servicos;

namespace Upa.Apresentacao.ServiceApp
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


        public IEnumerable<Usuario> RetornausuariosAtivosAsNoTracking()
        {
            throw new NotImplementedException();
        }
    }
}
