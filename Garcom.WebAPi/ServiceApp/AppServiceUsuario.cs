using Garcom.Dominio.Entidades;
using Garcom.Dominio.Interfaces.Servicos;
using Garcom.WebApi.InterfacesApp;
using Garcom.WebApi.ServiceApp;

namespace Garcom.WebApi
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
