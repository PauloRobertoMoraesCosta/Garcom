using Garcom.API.Helper;
using Garcom.Aplicacao.Implementacao;
using Garcom.Dominio.Entidade.DTOs;
using Garcom.Dominio.Servico.Servico;
using Garcom.Infra.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Garcom.API.Controllers
{
    /// <summary>
    /// API responsável pela Mesa
    /// </summary>
    [RoutePrefix("api/mesa")]
    public class MesaController : ApiController
    {
        /// <summary>
        /// construtor
        /// </summary>
        public MesaController()
        {}

        /// <summary>
        /// Lista todas as mesas ativas
        /// </summary>
        /// <remarks>API responsável por listar todas as mesas ativas</remarks>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [Authorize(Roles = RegraPerfil.Administrador )]
        [ResponseType(typeof(List<MesaDTO>))]
        public async Task<IHttpActionResult> ListarTodas()
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appMesa = new AppMesa(unitOfWork))
                    return await Task.Run(() => Ok(appMesa.ListarTodos()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Seleciona uma mesa
        /// </summary>
        /// <remarks>API responsável por selecionar uma mesa pelo Id</remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = RegraPerfil.Administrador )]
        [ResponseType(typeof(MesaDTO))]
        public async Task<IHttpActionResult> SelecionaPorId(int id)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appMesa = new AppMesa(unitOfWork))
                    return await Task.Run(() => Ok(appMesa.SelecionarPorId(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        /// <summary>
        /// Adicionar uma mesa
        /// </summary>
        /// <remarks>API responsável por adicionar uma mesa</remarks>
        /// <param name="mesaDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [Authorize(Roles = RegraPerfil.Administrador )]
        [ResponseType(typeof(MesaDTO))]
        public async Task<IHttpActionResult> Incluir([FromBody]MesaDTO mesaDTO)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appMesa = new AppMesa(unitOfWork))
                {
                    var retorno = appMesa.Incluir(mesaDTO);
                    return await Task.Run(() => Ok(retorno));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Altera mesa
        /// </summary>
        /// <remarks>API responsável por alterar uma mesa</remarks>
        /// <param name="mesaDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [Authorize(Roles = RegraPerfil.Administrador )]
        [ResponseType(typeof(MesaDTO))]
        public async Task<IHttpActionResult> Altera([FromBody]MesaDTO mesaDTO)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appMesa = new AppMesa(unitOfWork))
                {
                    var retorno = appMesa.Alterar(mesaDTO);
                    return await Task.Run(() => Ok(retorno));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Exclui uma mesa
        /// </summary>
        /// <remarks>API responsável por excluir mesa pelo id</remarks>
        /// <param name="mesaDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Excluir")]
        [Authorize(Roles = RegraPerfil.Administrador )]
        public async Task<IHttpActionResult> Exclusao([FromBody] MesaDTO mesaDTO)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appMesa = new AppMesa(unitOfWork))
                {
                    await Task.Run(() =>
                    {
                        appMesa.Excluir(mesaDTO);
                    });
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Desfazer exclusão
        /// </summary>
        /// <remarks>API responsável por desfazer a exclusão da mesa</remarks>
        /// <param name="mesa"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Desfazer")]
        [Authorize(Roles = RegraPerfil.Administrador )]
        public async Task<IHttpActionResult> Desfazer([FromBody] MesaDTO mesa)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appMesa = new AppMesa(unitOfWork))
                {
                    await Task.Run(() =>
                    {
                        appMesa.Desfazer(mesa);
                    });
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
