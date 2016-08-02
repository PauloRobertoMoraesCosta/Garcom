using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Description;
using Garcom.Dados.Verifications;
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
        [ResponseType(typeof(Usuario))]
        [Route("logar/{login}/{senha}")]
        public IHttpActionResult Logar(string login, string senha)
        {
            try
            {
                var usu = _serviceUsuario.logaUsuario(login, senha);

                return Ok(usu);
            }
            catch (DadosException dExc)
            {
                return Ok(new { sucess = false, message = dExc.Message });
            }

        }

        [HttpPost]
        [ResponseType(typeof(Usuario))]
        [Route("logarPost/{login}/{senha}")]
        public IHttpActionResult LogarPost(string login, string senha)
        {
            try
            {
                var usu = _serviceUsuario.logaUsuario(login, senha);

                return Ok(usu);
            }
            catch (DadosException dExc)
            {
                return Ok(new { sucess = false, message = dExc.Message });
            }

        }
    }
}