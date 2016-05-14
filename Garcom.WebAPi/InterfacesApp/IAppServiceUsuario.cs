using Garcom.Dominio.Entidades;

namespace Garcom.WebApi.InterfacesApp
{
    public interface IAppServiceUsuario : IAppServiceBase<Usuario>
    {
        Usuario logaUsuario(string login, string senha);
    }
}
