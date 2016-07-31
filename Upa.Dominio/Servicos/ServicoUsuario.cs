using System.Collections.Generic;
using Upa.Dominio.Entidades;
using Upa.Dominio.Interfaces.Repositorios;
using Upa.Dominio.Interfaces.Servicos;

namespace Upa.Dominio.Servicos
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


        public IEnumerable<Usuario> RetornausuariosAtivosAsNoTracking()
        {
            return _usuarioRepositorio.RetornausuariosAtivosAsNoTracking();
        }
    }
}
