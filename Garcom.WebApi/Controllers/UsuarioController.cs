using System.Web.Http;
using Garcom.WebApi.InterfacesApp;
using Garcom.Dominio.Entidades;

namespace Garcom.WebAPi.Controllers
{
    [RoutePrefix("Usuario")]
    public class UsuarioController : ApiController
    {
        private readonly IAppServiceUsuario _serviceUsuario;

        public UsuarioController(IAppServiceUsuario serviceUsuario)
        {
            _serviceUsuario = serviceUsuario;
        }

        [HttpGet]
        [Route("logar/{login}/{senha}")]
        public Usuario Logar(string login, string senha)
        {
            return _serviceUsuario.logaUsuario(login, senha);
        }
    }
}