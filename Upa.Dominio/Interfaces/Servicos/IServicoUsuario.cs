using System.Collections.Generic;
using Upa.Dominio.Entidades;

namespace Upa.Dominio.Interfaces.Servicos
{
    public interface IServicoUsuario : IServicoBase<Usuario>
    {
        Usuario LogaUsuario(string login, string senha);
        IEnumerable<Usuario> RetornausuariosAtivosAsNoTracking();
    }
}
