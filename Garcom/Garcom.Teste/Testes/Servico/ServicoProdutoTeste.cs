using Garcom.Dominio.Entidade.DTOs;
using Garcom.Dominio.Entidade.Mapeamento;
using Garcom.Dominio.Servico.Servico;
using Garcom.Infra.UnitOfWork;
using NUnit.Framework;
using System.Collections.Generic;

namespace Garcom.Teste.Testes.Servico
{
    [TestFixture]
    internal class ServicoProdutoTeste
    {
        private readonly IUnitOfWork _unitOfWork;

        public ServicoProdutoTeste()
        {
            this._unitOfWork = new UnitOfWork();
        }

        [Test]
        public void ListarProdutoComNomeGrupo()
        {
            using (var servicoProduto = new ServicoProduto(this._unitOfWork))
            {
                var produtos = servicoProduto.Listar();
                Assert.IsNotNull(produtos);
            }
        }

        [Test]
        public void SelecionaProduto()
        {
            using (var servicoProduto = new ServicoProduto(this._unitOfWork))
            {
                var produtos = servicoProduto.SelecionaProdutoPorId(8);
                Assert.IsNotNull(produtos);
            }
        }

        [Test]
        public void GravaListaTamanhoProdutoValor()
        {
            /*
            MapperConfig.ConfigurarMapper();

            ProdutoDTO p = new ProdutoDTO();
            p.GrupoProdutoId = 2;
            p.Nome = "Produto Teste10";
            p.Valor = null;
            p.Ativo = true;
            p.Id = 10;
            p.UsuarioLogado = "Usuário";       

            
            List<TamanhoProdutoValorDTO> listaFront = new List<TamanhoProdutoValorDTO>();
            listaFront.Add(new TamanhoProdutoValorDTO { Id = 10, ProdutoId = p.Id, TamanhoProdutoId = 1, Valor = 15 });
            //listaFront.Add(new TamanhoProdutoValorDTO { DataCadastro = System.DateTime.Now, ProdutoId = p.Id, TamanhoProdutoId = 4, Valor = 87.5f });
            //listaFront.Add(new TamanhoProdutoValorDTO { DataCadastro = System.DateTime.Now, ProdutoId = p.Id, TamanhoProdutoId = 5, Valor = 888.5f });
            //listaFront.Add(new TamanhoProdutoValorDTO { Id = 4, DataCadastro = System.DateTime.Now, ProdutoId = p.Id, TamanhoProdutoId = 1, Valor = 390.5f });

            p.TamanhoProdutoValores = listaFront;

            ICollection<ProdutoIngredienteTamanhoProdutoDTO> produtosIngredienteTamanho = new List<ProdutoIngredienteTamanhoProdutoDTO>();
            produtosIngredienteTamanho.Add(new ProdutoIngredienteTamanhoProdutoDTO { Id = 3, ProdutoIngredienteId = 10, TamanhoProdutoId = 1, Valor = 20});
            produtosIngredienteTamanho.Add(new ProdutoIngredienteTamanhoProdutoDTO { Id = 0, ProdutoIngredienteId = 10, TamanhoProdutoId = 2, Valor = 15 });

            ICollection<ProdutoIngredienteDTO> produtosIngrediente = new List<ProdutoIngredienteDTO>();
            produtosIngrediente.Add(new ProdutoIngredienteDTO
            {
                Id = 10,
                ProdutoId = p.Id,
                IngredienteId = 1,
                Opcional = true,
                Adicional = false,
                ValorAdicional = 20,
                ProdutosIngredientesTamanhosProdutos = produtosIngredienteTamanho
            });
            produtosIngrediente.Add(new ProdutoIngredienteDTO{ Id = 0, ProdutoId = p.Id, IngredienteId = 2, Opcional = true, Adicional = false, ValorAdicional = null });

            p.ProdutoIngredientes = produtosIngrediente;
            using (var servicoProduto = new ServicoProduto(this._unitOfWork))
            {
                servicoProduto.Alterar(p);
            }*/
        }

        [Test]
        public void Excluir()
        {
            ProdutoDTO produtoDTO = new ProdutoDTO
            {
                Id = 10
            };
            
            using (var servicoProduto = new ServicoProduto(this._unitOfWork))
                servicoProduto.RemoverProduto(produtoDTO.Id);
        }
    }
}
