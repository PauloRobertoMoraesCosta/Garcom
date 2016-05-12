using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Garcom.WebApi.Controllers
{
    [RoutePrefix("Usuario")]
    public class UsuarioController : ApiController
    {
        [Route("logar/{login}/{senha}")]
        public void Get(string login, string senha)
        {
            
        }
    }
}
