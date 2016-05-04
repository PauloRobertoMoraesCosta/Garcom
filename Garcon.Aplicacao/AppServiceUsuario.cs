using Garcom.Aplicacao.Interfaces;
using Garcom.Dominio.Entidades;
using Garcom.Dominio.Interfaces.Servicos;

namespace Garcom.Aplicacao
{
    public class AppServiceUsuario : AppServiceBase<Usuario>, IAppServiceUsuario
    {
        private readonly IServicoUsuario _serviceUsuario;

        public AppServiceUsuario(IServicoUsuario serviceUsuario) : base(serviceUsuario)
        {
            _serviceUsuario = serviceUsuario;
        }

    }
}
