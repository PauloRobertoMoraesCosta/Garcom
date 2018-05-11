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
    /// API responsável pelos Ingredientes
    /// </summary>
    [RoutePrefix("api/ingredientes")]
    public class IngredienteController : ApiController
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public IngredienteController()
        {
        }

        /// <summary>
        /// Lista todos os ingredientes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [Authorize(Roles = RegraPerfil.Administrador )]
        [ResponseType(typeof(List<IngredienteDTO>))]
        public async Task<IHttpActionResult> ListarTodos()
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appIngrediente = new AppIngrediente(unitOfWork))
                    return await Task.Run(() => Ok(appIngrediente.ListarTodos()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Adicionar ingrediente
        /// </summary>
        /// <remarks>API responsável por incluir um novo ingrediente</remarks>
        /// <param name="ingrediente"></param>
        /// <returns></returns>        
        [HttpPost]
        [Route("")]
        [Authorize(Roles = RegraPerfil.Administrador )]
        [ResponseType(typeof(IngredienteDTO))]
        public async Task<IHttpActionResult> Incluir([FromBody] IngredienteDTO ingrediente)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appIngrediente = new AppIngrediente(unitOfWork))
                {
                    var retorno = appIngrediente.Incluir(ingrediente);
                    return await Task.Run(() => Ok(retorno));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        /// <summary>
        /// Adicionar ingrediente
        /// </summary>
        /// <remarks>API responsável por alterar um ingrediente</remarks>
        /// <param name="ingrediente"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [Authorize(Roles = RegraPerfil.Administrador )]
        [ResponseType(typeof(IngredienteDTO))]
        public async Task<IHttpActionResult> Alterar([FromBody] IngredienteDTO ingrediente)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appIngrediente = new AppIngrediente(unitOfWork))
                {
                    var retorno = appIngrediente.Alterar(ingrediente);
                    return await Task.Run(() => Ok(retorno));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Lista ingrediente
        /// </summary>
        /// <remarks>API responsável por listar um ingrediente pelo id</remarks>
        /// <param name="id"> id do ingrediente</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = RegraPerfil.Administrador )]
        [ResponseType(typeof(IngredienteDTO))]
        public async Task<IHttpActionResult> SelecionaPorId(int id)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appIngrediente = new AppIngrediente(unitOfWork))
                    return await Task.Run(() => Ok(appIngrediente.SelecionarPorId(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Lista os produtos vinculados 
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>API responsável por exibir a lista de Produtos vinculados</remarks>
        /// <returns></returns>
        [HttpGet]
        [Route("validaExcluir/{id}")]
        [Authorize(Roles = RegraPerfil.Administrador )]
        [ResponseType(typeof(List<ProdutoDTO>))]
        public async Task<IHttpActionResult> ValidaExclusao(int id)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appIngrediente = new AppIngrediente(unitOfWork))
                    return await Task.Run(() => Ok(appIngrediente.ValidaExclusao(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Exclui um ingrediente
        /// </summary>
        /// <remarks>API responsável por excluir um ingrediente</remarks>
        /// <param name="ingrediente"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Excluir")]
        [Authorize(Roles = RegraPerfil.Administrador )]
        public async Task<IHttpActionResult> Exclusao(IngredienteDTO ingrediente)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appIngrediente = new AppIngrediente(unitOfWork))
                {
                    await Task.Run(() =>
                    {
                        appIngrediente.Excluir(ingrediente);
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
        /// <remarks>API responsável por desfazer a exclusão do ingrediente</remarks>
        /// <param name="ingrediente"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Desfazer")]
        [Authorize(Roles = RegraPerfil.Administrador )]
        public async Task<IHttpActionResult> Desfazer([FromBody] IngredienteDTO ingrediente)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appIngrediente = new AppIngrediente(unitOfWork))
                {
                    await Task.Run(() =>
                    {
                        appIngrediente.Desfazer(ingrediente);
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
