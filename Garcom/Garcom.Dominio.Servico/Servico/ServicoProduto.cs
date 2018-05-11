using Garcom.Core;
using Garcom.Dominio.Entidade.DTOs;
using Garcom.Dominio.Entidade.Models;
using Garcom.Dominio.Entidade.Models.Excecao;
using Garcom.Dominio.Servico.Validacao;
using Garcom.Infra.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Garcom.Dominio.Servico.Servico
{
    /// <summary>
    /// Serviço da classe produto
    /// </summary>
    public class ServicoProduto : ServicoBase<Produto>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ServicoProduto(IUnitOfWork unitOfWork)
            : base(unitOfWork, new ValidacaoProduto(unitOfWork))
        {
        }

        /// <summary>
        /// Método responsável por incluir um produto
        /// </summary>
        /// <param name="produtoDTO"></param>
        /// <returns></returns>
        public Produto Incluir(Produto produto)
        {
            if (produto != null && produto.DataCadastro == DateTime.MinValue) produto.DataCadastro = DateTime.Now;
            produto.Ativo = true;
            _validador.ValidaInclusao(produto);
            produto = _unitOfWork.RepositorioProduto.Incluir(produto);
            return produto;
        }

        /// <summary>
        /// Método responsável por alterar o cadastro de produto
        /// </summary>
        /// <param name="produtoDTO"></param>
        public Produto Alterar(Produto produto)
        {
            var produtoBanco = _unitOfWork.RepositorioProduto.SelecionaProdutoPorId(produto.Id);
            if (produtoBanco == null)
                throw new Exception(_mensagens.GetMensagem("ProdutoNaoCadastrado"));
            _validador.ValidaAlteracao(produto);
            produtoBanco.Nome = produto.Nome;
            produtoBanco.GrupoProdutoId = produto.GrupoProdutoId;
            produtoBanco.Ativo = produto.Ativo;
            produtoBanco.Valor = produto.Valor;
            produtoBanco.CodigoRapido = produto.CodigoRapido;
            produtoBanco.NomeImagem = produto.NomeImagem;

            List<ProdutoTamanhoValor> listaTamanhoProdutoValorInclusao = new List<ProdutoTamanhoValor>();
            AlteraTamanhoProdutoValores(produto.ProdutosTamanhosValor, produtoBanco.ProdutosTamanhosValor, ref listaTamanhoProdutoValorInclusao);

            List<ProdutoIngrediente> listaProdutoIngredienteInclusao = new List<ProdutoIngrediente>();
            List<ProdutoIngredienteTamanhoValor> listaProdutoIngredienteTamanhoInclusao = new List<ProdutoIngredienteTamanhoValor>();
            AlteraProdutoIngrediente(produto.ProdutoIngredientes, produtoBanco.ProdutoIngredientes, ref listaProdutoIngredienteInclusao, ref listaProdutoIngredienteTamanhoInclusao);

            _unitOfWork.RepositorioProduto.AlterarProduto(produtoBanco);
            return produtoBanco;
        }
        
        private void AlteraProdutoIngrediente(ICollection<ProdutoIngrediente> produtosIngrediente, ICollection<ProdutoIngrediente> produtosIngredienteBanco,
                                              ref List<ProdutoIngrediente> listaProdutoIngredienteInclusao, ref List<ProdutoIngredienteTamanhoValor> listaProdutoIngredienteTamanhoInclusao)
        {
            foreach (var produtoIngrediente in produtosIngrediente)
            {
                var produtoIngredienteAuxBanco = produtosIngredienteBanco.Where(p => p.Id == produtoIngrediente.Id).FirstOrDefault();
                ICollection<ProdutoIngredienteTamanhoValor> produtoIngredienteTamanhoBanco = new List<ProdutoIngredienteTamanhoValor>();

                if (produtoIngredienteAuxBanco != null && produtoIngredienteAuxBanco.ProdutosIngredientesTamanhosValor != null)
                    produtoIngredienteTamanhoBanco = produtoIngredienteAuxBanco.ProdutosIngredientesTamanhosValor;

                listaProdutoIngredienteTamanhoInclusao.AddRange(AlteraProdutoIngredienteTamanho(produtoIngrediente.ProdutosIngredientesTamanhosValor,
                                                                                                produtoIngredienteTamanhoBanco));
            }

            List<ProdutoIngrediente> listaProdutoIngredienteExclusao = produtosIngredienteBanco.Except(produtosIngrediente).ToList();
            listaProdutoIngredienteInclusao = produtosIngrediente.Where(t => t.Id == default(int)).ToList();
            List<ProdutoIngrediente> listaProdutoIngredienteAlteracao = produtosIngrediente.Except(listaProdutoIngredienteInclusao).ToList();

            listaProdutoIngredienteInclusao.ForEach(l => _unitOfWork.RepositorioProduto.AddProdutoIngrediente(l));
            listaProdutoIngredienteAlteracao.ForEach(l => _unitOfWork.RepositorioProduto.AlterarProdutoIngredienteSemSaveChange(l));
            listaProdutoIngredienteExclusao.ForEach(l => _unitOfWork.RepositorioProdutoIngrediente.RemoverBanco(l.Id));
        }

        private List<ProdutoIngredienteTamanhoValor> AlteraProdutoIngredienteTamanho(ICollection<ProdutoIngredienteTamanhoValor> produtosIngredienteTamanho,
                                                     ICollection<ProdutoIngredienteTamanhoValor> produtosIngredienteTamanhoBanco)
        {
            List<ProdutoIngredienteTamanhoValor> listaProdutoIngredienteTamanhoExclusao = produtosIngredienteTamanhoBanco.Except(produtosIngredienteTamanho).ToList();
            var listaProdutoIngredienteTamanhoInclusao = produtosIngredienteTamanho.Where(t => t.Id == default(int)).ToList();
            List<ProdutoIngredienteTamanhoValor> listaProdutoIngredienteTamanhoAlteracao = produtosIngredienteTamanho.Except(listaProdutoIngredienteTamanhoInclusao).ToList();

            listaProdutoIngredienteTamanhoInclusao.ForEach(l => _unitOfWork.RepositorioProduto.AddProdutoIngredienteTamanhoValor(l));
            listaProdutoIngredienteTamanhoAlteracao.ForEach(l => _unitOfWork.RepositorioProduto.AlterarProdutoIngredienteTamanhoValorSemSaveChange(l));
            listaProdutoIngredienteTamanhoExclusao.ForEach(l => _unitOfWork.RepositorioProdutoIngredienteTamanho.RemoverBanco(l.Id));

            return listaProdutoIngredienteTamanhoInclusao;
        }

        private void AlteraTamanhoProdutoValores(ICollection<ProdutoTamanhoValor> tamanhosProdutoValor, ICollection<ProdutoTamanhoValor> tamanhosProdutoValorBanco,
                                                 ref List<ProdutoTamanhoValor> listaTamanhoProdutoValorInclusao)
        {
            List<ProdutoTamanhoValor> listaTamanhoProdutoValorExclusao = tamanhosProdutoValorBanco.Except(tamanhosProdutoValor).ToList();
            listaTamanhoProdutoValorInclusao = tamanhosProdutoValor.Where(t => t.Id == default(int)).ToList();
            List<ProdutoTamanhoValor> listaTamanhoProdutoValorAlteracao = tamanhosProdutoValor.Except(listaTamanhoProdutoValorInclusao).ToList();

            listaTamanhoProdutoValorInclusao.ForEach(l => _unitOfWork.RepositorioProduto.AddProdutoTamanhoValor(l));
            listaTamanhoProdutoValorAlteracao.ForEach(l => _unitOfWork.RepositorioProduto.AlterarProdutoTamanhoValorSemSaveChange(l));
            listaTamanhoProdutoValorExclusao.ForEach(l => _unitOfWork.RepositorioProduto.ExcluirProdutoTamanhoValorSemSaveChange(l));
        }

        /// <summary>
        /// Método responsável por desvincular os grupos de produtos
        /// </summary>
        /// <param name="descricaoProduto"></param>
        public void DesvincularGrupoProduto(ICollection<string> listaDescricaoProduto)
        {
            var produtos = _unitOfWork.RepositorioProduto.ListarPorDescricoes(listaDescricaoProduto);

            foreach (var produto in produtos)
            {
                produto.GrupoProdutoId = null;
                _unitOfWork.RepositorioProduto.Alterar(produto);
            }
        }

        /// <summary>
        /// Método responsável por desvincular o grupo de produto dos produtos que estão vinculado
        /// </summary>
        /// <param name="grupoProdutoId"></param>
        public void DesvincularGrupoProduto(int grupoProdutoId)
        {
            var produtos = _unitOfWork.RepositorioProduto.ListaProdutoPeloGrupoProdutoId(grupoProdutoId);

            foreach (var produto in produtos)
            {
                produto.GrupoProdutoId = null;
                _unitOfWork.RepositorioProduto.Alterar(produto);
            }
        }

        public Produto SelecionaProdutoPorId(int id)
        {
            var produto = _unitOfWork.RepositorioProduto.SelecionaProdutoPorId(id);
            if (!string.IsNullOrWhiteSpace(produto.NomeImagem))
                produto.NomeImagem = ConfigurationManager.AppSettings["CaminhoImagem"].ToString() + produto.NomeImagem;
            return produto;
        }


        /// <summary>
        /// Método responsável por buscar um produto por seu código rápido.
        /// </summary>
        /// <param name="codigoRapido"></param>
        /// <returns></returns>
        public Produto SelecionaProdutoPorCodigoRapido(string codigoRapido)
        {
            var produto = _unitOfWork.RepositorioProduto.SelecionaPorCodigoRapido(codigoRapido);
            return produto;
        }

        /// <summary>
        /// Método responsável por alterar o status do produto para remoção
        /// </summary>
        /// <param name="id"></param>
        public void RemoverProduto(int id)
        {
            var produtosIngredientesTamanhos = _unitOfWork.RepositorioProduto.ListarProdutoIngredienteTamanho(id);
            foreach (var produtoIngredienteTamanho in produtosIngredientesTamanhos)
            {
                produtoIngredienteTamanho.Excluido = true;
                _unitOfWork.RepositorioProduto.AlterarProdutoIngredienteTamanhoValorSemSaveChange(produtoIngredienteTamanho);
            }
            _unitOfWork.RepositorioProduto.Excluir(id);
        }

        public void AjustarNomeImagem(Produto produto, ProdutoDTO produtoDTO)
        {
            if (produtoDTO.Imagem != null)
            {
                RemoveImagem(produto.NomeImagem);
                produto.NomeImagem = InsereImagem(produtoDTO.Imagem);
            }
            else if (produtoDTO.Imagem == null && produtoDTO.NomeImagem == null)
            {
                RemoveImagem(produto.NomeImagem);
                produto.NomeImagem = string.Empty;
            }
        }

        /// <summary>
        /// Método responsável por desfazer a exclusão do produto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void Desfazer(int id)
        {
            var produto = _unitOfWork.RepositorioProduto.SelecionaProdutoPorId(id);
            if (produto == null)
                throw new Exception(_mensagens.GetMensagem("ProdutoNaoCadastrado"));
            produto.Excluido = false;
            _unitOfWork.RepositorioProduto.AlterarProduto(produto);
            var produtosIngredientesTamanhos = _unitOfWork.RepositorioProduto.ListarProdutoIngredienteTamanho(id);
            foreach (var produtoIngredienteTamanho in produtosIngredientesTamanhos)
            {
                produtoIngredienteTamanho.Excluido = false;
                _unitOfWork.RepositorioProduto.AlterarProdutoIngredienteTamanhoValorSemSaveChange(produtoIngredienteTamanho);
            }
        }

        /// <summary>
        /// Método responsável por listar os produtos com o nome do grupo de produto
        /// </summary>
        /// <returns></returns>
        public ICollection<ProdutoDTO> Listar()
        {
            return _unitOfWork.RepositorioProduto.ListarProdutoComNomeGrupo();
        }

        /// <summary>
        /// Método responsável por remover o Produto Ingrediente Tamanho do banco de dados
        /// </summary>
        private void RemoverBancoProdutoIngredienteTamanho()
        {
            var produtoIngredienteTamanhoIds = _unitOfWork.RepositorioProdutoIngredienteTamanho.ListarTodosIdsProdutoIngredienteTamanhoExcluido();

            foreach (var produtoIngredienteTamanhoId in produtoIngredienteTamanhoIds)
                _unitOfWork.RepositorioProdutoIngredienteTamanho.RemoverBanco(produtoIngredienteTamanhoId);
        }

        /// <summary>
        /// Método responsável por remover os produtos do banco de dados
        /// </summary>
        private void RemoverBancoProduto()
        {
            var produtos = _unitOfWork.RepositorioProduto.ListarTodosExcluido();

            foreach (var produto in produtos)
            {
                RemoveImagem(produto.NomeImagem);
                _unitOfWork.RepositorioProduto.RemoverBanco(produto.Id);
            }
        }

        private void RemoverBancoProdutoIngrediente()
        {
            var produtoIngredienteIds = _unitOfWork.RepositorioProdutoIngrediente.ListarTodosIdsProdutoIngredienteExcluido();

            foreach (var produtoIngredienteId in produtoIngredienteIds)
                _unitOfWork.RepositorioProdutoIngrediente.RemoverBanco(produtoIngredienteId);
        }

        public void RemoverBanco()
        {
            RemoverBancoProdutoIngredienteTamanho();
            RemoverBancoProdutoIngrediente();
            RemoverBancoProduto();
        }

        public string InsereImagem(string imagem)
        {
            if (string.IsNullOrEmpty(imagem))
                return string.Empty;

            var nomeImagem = Guid.NewGuid().ToString();
            var diretorio = AppDomain.CurrentDomain.BaseDirectory + "\\Imagem\\Produto";
            var imagemArray = imagem.Split('#');

            try
            {
                if (!Directory.Exists(diretorio))
                    Directory.CreateDirectory(diretorio);

                File.WriteAllBytes(diretorio + "\\" + nomeImagem + "." + imagemArray[1], Convert.FromBase64String(imagemArray[0]));
                return nomeImagem + "." + imagemArray[1];
            }
            catch (Exception ex)
            {
                using (var excecaoService = new ServicoExcecao(this._unitOfWork))
                    excecaoService.Incluir(new Excecao
                    {
                        Mensagem = Funcoes.TratamentoMessageExcecao(ex),
                        Rotina = "InsereImagem",
                        Parametros = string.Format("Diretorio: {0}", diretorio)
                    });
                return string.Empty;
            }
        }

        private void RemoveImagem(string nomeImagem)
        {
            if (string.IsNullOrEmpty(nomeImagem))
                return;

            var diretorio = AppDomain.CurrentDomain.BaseDirectory + "\\Imagem\\Produto";
            try
            {
                File.Delete(diretorio + "\\" + nomeImagem);
            }
            catch (Exception ex)
            {
                using (var excecaoService = new ServicoExcecao(this._unitOfWork))
                    excecaoService.Incluir(new Excecao
                    {
                        Mensagem = Funcoes.TratamentoMessageExcecao(ex),
                        Rotina = "InsereImagem",
                        Parametros = string.Format("Diretorio: {0}\nNome Image: {1}", diretorio, nomeImagem)
                    });
            }
        }

        /// <summary>
        /// Método responsável por validar se o produto pode ser excluido
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool JaUtilizado(int id)
        {
            return false;
        }

        public void Inativar(int id)
        {
            var produto = _unitOfWork.RepositorioProduto.SelecionarPorId(id);
            produto.Ativo = false;
            _unitOfWork.RepositorioProduto.Alterar(produto);
        }
    }
}
