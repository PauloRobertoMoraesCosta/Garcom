using Garcom.API.Helper;
using Garcom.Aplicacao.Implementacao;
using Garcom.Dominio.Entidade.DTOs;
using Garcom.Infra.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Garcom.API.Controllers
{
    /// <summary>
    /// API responsável pelo usuário
    /// </summary>
    [RoutePrefix("api/usuarios")]
    public class UsuarioController : ApiController
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public UsuarioController()
        {
        }

        /// <summary>
        /// Lista todos os usuários
        /// </summary>
        /// <remarks>API responsável por retornar todos os usuários armazenados no banco de dados</remarks>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [Authorize(Roles = RegraPerfil.Administrador )]
        [ResponseType(typeof(List<UsuarioDTO>))]
        public async Task<IHttpActionResult> ListarTodos()
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appUsuario = new AppUsuario(unitOfWork))
                    return await Task.Run(() => Ok(appUsuario.ListarTodos()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Seleciona usuário por login
        /// </summary>
        /// <remarks>API responsável por retornar um usuário pelo seu login</remarks>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{login}")]
        [Authorize]
        [ResponseType(typeof(UsuarioDTO))]
        public async Task<IHttpActionResult> SelecionarPorLogin(string login)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appUsuario = new AppUsuario(unitOfWork))
                    return await Task.Run(() => Ok(appUsuario.SelecionarPorLogin(login)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Seleciona usuário por login
        /// </summary>
        /// <remarks>API responsável por retornar um usuário pelo seu login</remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        [Authorize(Roles = RegraPerfil.Administrador)]
        [ResponseType(typeof(UsuarioDTO))]
        public async Task<IHttpActionResult> SelecionarPorId(int id)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appUsuario = new AppUsuario(unitOfWork))
                    return await Task.Run(() => Ok(appUsuario.SelecionarPorId(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Adicionar usuário
        /// </summary>
        /// <remarks>API responsável por incluir um novo usuário</remarks>
        /// <param name="usuario"></param>
        /// <returns></returns>        
        [HttpPost]
        [Route("")]
        [Authorize(Roles = RegraPerfil.Administrador)]
        [ResponseType(typeof(UsuarioDTO))]
        public async Task<IHttpActionResult> Incluir([FromBody] UsuarioDTO usuario)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appUsuario = new AppUsuario(unitOfWork))
                {
                    var retorno = appUsuario.Incluir(usuario);
                    return await Task.Run(() => Ok(retorno));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        /// <summary>
        /// Altera usuário
        /// </summary>
        /// <remarks>API responsável por alterar um usuário</remarks>
        /// <param name="usuario"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [Authorize(Roles = RegraPerfil.Administrador)]
        public async Task<IHttpActionResult> Alterar([FromBody] UsuarioDTO usuario)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appUsuario = new AppUsuario(unitOfWork))
                {
                    var retorno = appUsuario.Alterar(usuario);
                    return await Task.Run(() => Ok(retorno));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Lista os perfieis
        /// </summary>
        /// <remarks>Lista todos os perfieis cadastrados</remarks>
        /// <returns></returns>
        [HttpGet]
        [Route("perfieis")]
        [Authorize(Roles = RegraPerfil.Administrador)]
        [ResponseType(typeof(List<PerfilDTO>))]
        public async Task<IHttpActionResult> ListaPerfil()
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appUsuario = new AppUsuario(unitOfWork))
                    return await Task.Run(() => Ok(appUsuario.ListaTodosPerfil()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
