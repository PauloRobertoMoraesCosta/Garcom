using System.Collections.Generic;
using Upa.Dominio.Entidades;

namespace Upa.Apresentacao.InterfacesApp
{
    public interface IAppServiceUsuario : IAppServiceBase<Usuario>
    {
        Usuario logaUsuario(string login, string senha);
        IEnumerable<Usuario> RetornausuariosAtivosAsNoTracking();
    }
}
