using Garcom.Dominio.Entidades;

namespace Garcom.Aplicacao.Interfaces
{
    public interface IAppServiceUsuario : IAppServiceBase<Usuario>
    {
        Usuario logaUsuario(string login, string senha);
    }
}
