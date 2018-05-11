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
    /// API responsável pelo grupo de produto
    /// </summary>
    [RoutePrefix("api/grupoproduto")]
    public class GrupoProdutoController : ApiController
    { 
        /// <summary>
        /// Construtor
        /// </summary>
        public GrupoProdutoController()
        {
        }

        /// <summary>
        /// Lista todos os grupos de produtos
        /// </summary>
        /// <remarks>API responsável por listar os grupos de produtos</remarks>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [Authorize(Roles = RegraPerfil.Administrador )]
        [ResponseType(typeof(List<GrupoProdutoDTO>))]
        public async Task<IHttpActionResult> ListarTodos()
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appGrupoProduto = new AppGrupoProduto(unitOfWork))
                    return await Task.Run(() => Ok(appGrupoProduto.ListarTodos()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Seleciona grupo de produto
        /// </summary>
        /// <remarks>API responsável por selecionar o grupo de produto pelo Id</remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ComTamanhos")]
        [Authorize(Roles = RegraPerfil.Administrador )]
        [ResponseType(typeof(GrupoProdutoDTO))]
        public async Task<IHttpActionResult> ListarTodosComTamanhos()
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appGrupoProduto = new AppGrupoProduto(unitOfWork))
                    return await Task.Run(() => Ok(appGrupoProduto.ListarTodosComTamanhos()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Seleciona grupo de produto
        /// </summary>
        /// <remarks>API responsável por selecionar o grupo de produto pelo Id</remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = RegraPerfil.Administrador )]
        [ResponseType(typeof(GrupoProdutoDTO))]
        public async Task<IHttpActionResult> SelecionaPorId(int id)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appGrupoProduto = new AppGrupoProduto(unitOfWork))
                    return await Task.Run(() => Ok(appGrupoProduto.SelecionarPorId(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Adicionar grupo de produto
        /// </summary>
        /// <remarks>API responsável por adicionar grupo de produto</remarks>
        /// <param name="grupoProdutoDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [Authorize(Roles = RegraPerfil.Administrador )]
        [ResponseType(typeof(GrupoProdutoDTO))]
        public async Task<IHttpActionResult> Incluir([FromBody]GrupoProdutoDTO grupoProdutoDTO)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appGrupoProduto = new AppGrupoProduto(unitOfWork))
                {
                    var retorno = appGrupoProduto.Incluir(grupoProdutoDTO);
                    return await Task.Run(() => Ok(retorno));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Altera grupo de produto
        /// </summary>
        /// <remarks>API responsável por alterar o cadastro do grupo de produto</remarks>
        /// <param name="grupoProdutoDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [Authorize(Roles = RegraPerfil.Administrador )]
        [ResponseType(typeof(GrupoProdutoDTO))]
        public async Task<IHttpActionResult> Altera([FromBody]GrupoProdutoDTO grupoProdutoDTO)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appGrupoProduto = new AppGrupoProduto(unitOfWork))
                {
                    var retorno = appGrupoProduto.Alterar(grupoProdutoDTO);
                    return await Task.Run(() => Ok(retorno));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Valida exclusão 
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>API responsável por validar a exclusão do grupo de produto</remarks>
        /// <returns></returns>
        [HttpGet]
        [Route("valida/exclusao/{id}")]
        [Authorize(Roles = RegraPerfil.Administrador )]
        [ResponseType(typeof(List<string>))]
        public async Task<IHttpActionResult> ValidaExclusao(int id)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appGrupoProduto = new AppGrupoProduto(unitOfWork))
                    return await Task.Run(() => Ok(appGrupoProduto.ValidaExclusao(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Exclui grupo de produto
        /// </summary>
        /// <remarks>API responsável por excluir grupo de produto por id</remarks>
        /// <param name="grupoProdutoDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Excluir")]
        [Authorize(Roles = RegraPerfil.Administrador )]
        public async Task<IHttpActionResult> Exclusao([FromBody] GrupoProdutoDTO grupoProdutoDTO)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appGrupoProduto = new AppGrupoProduto(unitOfWork))
                    await Task.Run(() =>{ appGrupoProduto.Excluir(grupoProdutoDTO); });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Desfaz exclusão
        /// </summary>
        /// <remarks>API responsável por desfazer a exclusão</remarks>
        /// <param name="grupoProdutoDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Desfazer")]
        [Authorize(Roles = RegraPerfil.Administrador )]
        public async Task<IHttpActionResult> Desfazer([FromBody] GrupoProdutoDTO grupoProdutoDTO)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appGrupoProduto = new AppGrupoProduto(unitOfWork))
                    await Task.Run(() =>{ appGrupoProduto.Desfazer(grupoProdutoDTO); });
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
