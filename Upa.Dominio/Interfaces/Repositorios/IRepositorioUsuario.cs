using System.Collections.Generic;
using Upa.Dominio.Entidades;

namespace Upa.Dominio.Interfaces.Repositorios
{
    public interface IRepositorioUsuario : IRepositorioBase<Usuario>
    {
        Usuario logaUsuario(string login, string senha);
        IEnumerable<Usuario> RetornausuariosAtivosAsNoTracking();
    }
}
