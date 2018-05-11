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
    /// API responsável pelo produto
    /// </summary>
    [RoutePrefix("api/produto")]
    public class ProdutoController : ApiController
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public ProdutoController()
        {
        }

        /// <summary>
        /// API responsável por listar os produtos 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [Authorize(Roles = RegraPerfil.Administrador)]
        [ResponseType(typeof(List<ProdutoDTO>))]
        public async Task<IHttpActionResult> Listar()
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appProduto = new AppProduto(unitOfWork))
                    return await Task.Run(() => Ok(appProduto.ListarTodos()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Adiciona produto
        /// </summary>
        /// <remarks>API responsável por inserir um produto</remarks>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [Authorize(Roles = RegraPerfil.Administrador)]
        [ResponseType(typeof(ProdutoDTO))]
        public async Task<IHttpActionResult> Incluir([FromBody] ProdutoDTO produtoDTO)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appProduto = new AppProduto(unitOfWork))
                {
                    var retorno = appProduto.Incluir(produtoDTO);
                    return await Task.Run(() => Ok(retorno));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Alterar o produto
        /// </summary>
        /// <remarks>API responsável por alterar o cadastrado do produto</remarks>
        /// <param name="produtoDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [Authorize(Roles = RegraPerfil.Administrador)]
        [ResponseType(typeof(ProdutoDTO))]
        public async Task<IHttpActionResult> Alterar([FromBody] ProdutoDTO produtoDTO)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appProduto = new AppProduto(unitOfWork))
                {
                    var retorno = appProduto.Alterar(produtoDTO);
                    return await Task.Run(() => Ok(retorno));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Lista produto por id
        /// </summary>
        /// <remarks>API responsável por listar um produto pelo id</remarks>
        /// <param name="id"> id do produto</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = RegraPerfil.Administrador)]
        [ResponseType(typeof(ProdutoDTO))]
        public async Task<IHttpActionResult> SelecionaPorId(int id)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appProduto = new AppProduto(unitOfWork))
                    return await Task.Run(() => Ok(appProduto.SelecionarPorId(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Lista produto por Codigo Rapido
        /// </summary>
        /// <param name="codigoRapido"></param>
        /// <remarks>API responsável por listar um produto por seu código rápido</remarks>
        /// <returns></returns>
        [HttpGet]
        [Route("codigoRapido/{codigoRapido}")]
        [Authorize(Roles = RegraPerfil.Administrador)]
        [ResponseType(typeof(ProdutoDTO))]
        public async Task<IHttpActionResult> SelecionaPorCodigoRapido(string codigoRapido)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appProduto = new AppProduto(unitOfWork))
                    return await Task.Run(() => Ok(appProduto.SelecionaProdutoPorCodigoRapido(codigoRapido)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Já utilizado
        /// </summary>
        /// <param name="id"></param>
        /// <remarks>API responsável verificar se o produto já foi utilizado</remarks>
        /// <returns></returns>
        [HttpGet]
        [Route("JaUtilizado/{id}")]
        [Authorize(Roles = RegraPerfil.Administrador)]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> ValidaExclusao(int id)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appProduto = new AppProduto(unitOfWork))
                    return await Task.Run(() => Ok(appProduto.JaUtilizado(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Excluir produto
        /// </summary>
        /// <remarks>API responsável por excluir o produto</remarks>
        /// <param name="produto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Excluir")]
        [Authorize(Roles = RegraPerfil.Administrador)]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Excluir([FromBody]ProdutoDTO produto)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appProduto = new AppProduto(unitOfWork))
                {
                    appProduto.RemoverProduto(produto);
                    return await Task.Run(() => Ok());
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// API responsável por desfazer a alteração do produto
        /// </summary>
        /// <param name="produto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Desfazer")]
        [Authorize(Roles = RegraPerfil.Administrador)]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Desfazer([FromBody]ProdutoDTO produto)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appProduto = new AppProduto(unitOfWork))
                    appProduto.Desfazer(produto);
                return await Task.Run(() => Ok());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Inativar produto
        /// </summary>
        /// <param name="produto"></param>
        /// <remarks>API responsável por inativar o cadastro do produto</remarks>
        /// <returns></returns>
        [HttpPut]
        [Route("Inativar")]
        [Authorize(Roles = RegraPerfil.Administrador)]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Inativar([FromBody]ProdutoDTO produto)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appProduto = new AppProduto(unitOfWork))
                    appProduto.Inativar(produto);
                return await Task.Run(() => Ok());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
