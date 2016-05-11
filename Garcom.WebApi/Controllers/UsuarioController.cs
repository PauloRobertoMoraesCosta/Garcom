using System.Web.Http;

namespace Garcom.WebApi.Controllers
{
    [RoutePrefix("Usuario/logar")]
    public class UsuarioController : ApiController
    {
        [HttpGet]
        [Route("{login}/{senha}")]
        public void Get(string login, string senha)
        {
            
        }
    }
}
