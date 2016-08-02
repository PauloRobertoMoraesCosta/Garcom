using Garcom.Dominio.Entidades;

namespace Garcom.Dominio.Interfaces.Repositorios
{
    public interface IRepositorioUsuario : IRepositorioBase<Usuario>
    {
        Usuario logaUsuario(string login, string senha);
    }
}
