using Garcom.Aplicacao.Interfaces;
using Garcom.Dados.Verifications;
using Garcom.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Garcom.WebApi.Controllers
{
    public class UsuarioController : ApiController
    {
        private readonly IAppServiceUsuario _serviceUsuario;

        public UsuarioController(IAppServiceUsuario serviceUsuario)
        {
            _serviceUsuario = serviceUsuario;
        }

        // GET api/usuario
        public IEnumerable<Usuario> Get()
        {
            try
            {
                return _serviceUsuario.RetornaTodos(); //logaUsuario(login, senha);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        // GET api/usuario/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/usuario
        public void Post([FromBody]string value)
        {
        }

        // PUT api/usuario/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/usuario/5
        public void Delete(int id)
        {
        }
    }
}
