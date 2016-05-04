using Garcom.Dominio.Entidades;

namespace Garcom.Dominio.Interfaces.Servicos
{
    public interface IServicoUsuario : IServicoBase<Usuario>
    {
        Usuario LogaUsuario(string login, string senha);
    }
}
