using Garcom.API.Helper;
using Garcom.Aplicacao.Implementacao;
using Garcom.Dominio.Entidade.DTOs;
using Garcom.Infra.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Garcom.API.Controllers
{
    /// <summary>
    /// API responsável pelo Tamanho de Produto
    /// </summary>
    [RoutePrefix("api/tamanho")]
    public class TamanhoProdutoController : ApiController
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public TamanhoProdutoController()
        {}

        /// <summary>
        /// Lista todos
        /// </summary>
        /// <remarks>API responsável por listar todos os tamanhos</remarks>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [Authorize(Roles = RegraPerfil.Administrador )]
        [ResponseType(typeof(List<TamanhoProdutoDTO>))]
        public async Task<IHttpActionResult> ListarTodos()
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appTamanhoProduto = new AppTamanhoProduto(unitOfWork))
                    return await Task.Run(() => Ok(appTamanhoProduto.ListarTodos()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Seleciona por id
        /// </summary>
        /// <remarks>API responsável por selecionar um tamanho pelo id</remarks>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = RegraPerfil.Administrador)]
        [ResponseType(typeof(TamanhoProdutoDTO))]
        public async Task<IHttpActionResult> SelecionarPorId(int id)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appTamanhoProduto = new AppTamanhoProduto(unitOfWork))
                    return await Task.Run(() => Ok(appTamanhoProduto.SelecionarPorId(id)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Seleciona ativos
        /// </summary>
        /// <remarks>API responsável por selecionar um tamanho pelo id</remarks>
        /// <returns></returns>
        [HttpGet]
        [Route("ativos")]
        [Authorize(Roles = RegraPerfil.Administrador)]
        [ResponseType(typeof(TamanhoProdutoDTO))]
        public async Task<IHttpActionResult> SelecionarAtivos()
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appTamanhoProduto = new AppTamanhoProduto(unitOfWork))
                    return await Task.Run(() => Ok(appTamanhoProduto.ListarTodos().Where(x => x.Ativo)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Valida exclusão
        /// </summary>
        /// <remarks>API responsável por verifica se o tamanho está vinculado a produto</remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("valida/exclusao/{id}")]
        [Authorize(Roles = RegraPerfil.Administrador )]
        [ResponseType(typeof(List<string>))]
        public async Task<IHttpActionResult> VerificaExclusao(int id)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appTamanhoProduto = new AppTamanhoProduto(unitOfWork))
                {
                    var retorno = appTamanhoProduto.ValidarExclusao(id);
                    return await Task.Run(() => Ok(retorno));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Incluir o tamanho
        /// </summary>
        /// <remarks>API responsável por incluir o tamanho</remarks>
        /// <param name="tamanhoProdutoDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [Authorize(Roles = RegraPerfil.Administrador )]
        [ResponseType(typeof(TamanhoProdutoDTO))]
        public async Task<IHttpActionResult> Incluir([FromBody] TamanhoProdutoDTO tamanhoProdutoDTO)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appTamanhoProduto = new AppTamanhoProduto(unitOfWork))
                {
                    var retorno = appTamanhoProduto.Incluir(tamanhoProdutoDTO);
                    return await Task.Run(() => Ok(retorno));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Altera o tamanho
        /// </summary>
        /// <remarks>API responsável por alterar o tamanho</remarks>
        /// <param name="tamanhoProdutoDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("")]
        [Authorize(Roles = RegraPerfil.Administrador )]
        [ResponseType(typeof(TamanhoProdutoDTO))]
        public async Task<IHttpActionResult> Alterar([FromBody] TamanhoProdutoDTO tamanhoProdutoDTO)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appTamanhoProduto = new AppTamanhoProduto(unitOfWork))
                {
                    var retorno = appTamanhoProduto.Alterar(tamanhoProdutoDTO);
                    return await Task.Run(() => Ok(retorno));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Exclui o tamanho
        /// </summary>
        /// <remarks>API responsável por excluir o tamanho</remarks>
        /// <param name="tamanhoProdutoDTO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Excluir")]
        [Authorize(Roles = RegraPerfil.Administrador )]
        public async Task<IHttpActionResult> Excluir([FromBody] TamanhoProdutoDTO tamanhoProdutoDTO)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appTamanhoProduto = new AppTamanhoProduto(unitOfWork))
                {
                    await Task.Run(() =>
                    {
                        appTamanhoProduto.Excluir(tamanhoProdutoDTO);
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
        /// <remarks>API responsável por retornar os produtos vinculados ao um tamanho de um grupo de produto</remarks>
        /// <param name="dados"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("NomeProdutosVinculados")]
        [Authorize(Roles = RegraPerfil.Administrador)]
        public async Task<IHttpActionResult> Desfazer([FromBody] Dictionary<string, int> value)
        {
            try
            {
                var grupoProdutoId = value["grupoProdutoId"];
                var tamanhoId = value["id"];
                using (var unitOfWork = new UnitOfWork())
                using (var appTamanhoProduto = new AppTamanhoProduto(unitOfWork))
                {
                    var retorno = appTamanhoProduto.NomeProdutosVinculos(grupoProdutoId, tamanhoId).ToList();
                    return await Task.Run(() => Ok(retorno));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Desfazer exclusão
        /// </summary>
        /// <remarks>API responsável por desfazer a exclusão do tamanho de produto</remarks>
        /// <param name="tamanhoProdutoDTO"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Desfazer")]
        [Authorize(Roles = RegraPerfil.Administrador)]
        public async Task<IHttpActionResult> Desfazer([FromBody] TamanhoProdutoDTO tamanhoProdutoDTO)
        {
            try
            {
                using (var unitOfWork = new UnitOfWork())
                using (var appTamanhoProduto = new AppTamanhoProduto(unitOfWork))
                {
                    await Task.Run(() =>
                    {
                        appTamanhoProduto.Desfazer(tamanhoProdutoDTO);
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
