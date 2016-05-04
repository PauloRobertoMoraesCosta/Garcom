using Garcom.Dominio.Entidades;
using Garcom.Dominio.Interfaces.Repositorios;
using Garcom.Dominio.Interfaces.Servicos;

namespace Garcom.Dominio.Servicos
{
    public class ServicoUsuario : ServicoBase<Usuario>, IServicoUsuario
    {
        private readonly IRepositorioUsuario _usuarioRepositorio;

        public ServicoUsuario(IRepositorioUsuario usuarioRepositorio) : base(usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }


        public Usuario LogaUsuario(string login, string senha)
        {
            return _usuarioRepositorio.logaUsuario(login, senha);
        }
    }
}
