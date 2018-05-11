using Garcom.Aplicacao.Implementacao;
using Garcom.Dominio.Entidade.DTOs;
using Garcom.Infra.UnitOfWork;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;

namespace Garcom.Teste.Teste_Automatizado
{
    internal class CadastroGrupoProduto
    {
        const string NOME = "Grupo1";
        const string NOME_ALTERADO = NOME + "Alterado";
        const string NOME_VINCULADO_TAMANHO = "GrupoTamanho";
        const string NOME_TAMANHO = "TamanhoTeste";
        const string NOME_PRODUTO = "ProdutoTeste";
        private int _tamanhoProdutoId;
        private int _produtoId;

        Configuracao _configuracao;

        [SetUp]
        public void Initialize()
        {
            _configuracao = new Configuracao();
            _configuracao.Inicializacao();
            _configuracao.Login();

            IncluirGrupoEProdutoVinculadoComTamanho();
            _configuracao.driver.FindElement(By.XPath("html/body/div[2]/div/div[1]/div[2]/section/ul/li[1]/ul/li[3]/div")).Click();
            Thread.Sleep(1000);
        }

        [Test]
        public void TestarCRUDGrupoProduto()
        {
            ClicarNovo();
            Incluir();
            Thread.Sleep(2000);
            ClicarNovo();
            IncluirGrupoJaCadastrado();
            Thread.Sleep(2000);
            Alterar();
            Thread.Sleep(3000);
            Desfazer();
            Thread.Sleep(2000);
            Excluir();
            Thread.Sleep(2000);
            ExcluirGrupoVinculado();
        }

        [TearDown]
        public void EndTest()
        {
            ExcluirTamanhoProduto();
            _configuracao.Finalizacao();
        }

        private void Incluir()
        {
            ClicarSalvar();
            _configuracao.driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[1]/div/span"));
            Thread.Sleep(4000);
            IncluirGrupoCorreto(NOME, "Grupo de produtos cadastrado com sucesso.");
        }

        private void Alterar()
        {
            AbrirTelaAlteracao(NOME);
            IncluirGrupoCorreto(NOME_ALTERADO, "Grupo de produtos alterado com sucesso.");
        }

        private void Desfazer()
        {
            EncontrarIndexGrupoEClicarExcluir(NOME_ALTERADO);
            _configuracao.driver.FindElement(By.XPath("/html/body/div[1]/ul/li/div/a")).Click();
            Thread.Sleep(2000);
            var index = RetornarIndexGrupoGrid(NOME_ALTERADO);
            Assert.IsTrue(index != -1);
        }

        private void Excluir()
        {
            EncontrarIndexGrupoEClicarExcluir(NOME_ALTERADO);
            Thread.Sleep(2000);
            var index = RetornarIndexGrupoGrid(NOME_ALTERADO);
            Assert.IsTrue(index == -1);
        }

        private void ExcluirGrupoVinculado()
        {
            EncontrarIndexGrupoEClicarExcluir(NOME_VINCULADO_TAMANHO);
            Thread.Sleep(2000);
            var retorno = _configuracao.driver.FindElement(By.XPath("//*[@class='modal-body ng-scope']"));
            Assert.IsTrue(retorno.Text.Contains(NOME_TAMANHO));
            Assert.IsTrue(retorno.Text.Contains(NOME_PRODUTO));
            ClicarDesvincularExcluir();
        }

        private void ClicarSalvar()
        {
            _configuracao.driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[3]/button[2]")).Click();
            Thread.Sleep(2000);
        }

        private void ClicarDesvincularExcluir()
        {
            _configuracao.driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[3]/button[2]")).Click();
            Thread.Sleep(2000);
        }
        private void IncluirGrupoJaCadastrado()
        {
            PreencherCampoNome(NOME);
            ClicarSalvar();
            var retorno = _configuracao.driver.FindElement(By.XPath("/html/body/div[1]/ul/li/div"));
            Assert.AreEqual(retorno.Text, _configuracao.Mensagens.GetMensagem("GrupoProdutoJaCadastrado"));
            ClicarCancelar();
        }

        private void ClicarCancelar()
        {
            _configuracao.driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[3]/button[1]")).Click();
            Thread.Sleep(1000);
        }
        private void IncluirGrupoCorreto(string nome, string msgRetorno)
        {
            PreencherCampoNome(nome);
            ClicarSalvar();
            var retorno = _configuracao.driver.FindElement(By.XPath("/html/body/div[1]/ul/li/div"));
            Assert.AreEqual(retorno.Text, msgRetorno);
        }

        private void PreencherCampoNome(string nome)
        {
            var campoNome = _configuracao.driver.FindElement(By.XPath("//input[@type='text']"));
            campoNome.Clear();
            campoNome.SendKeys(nome);
        }

        private void AbrirTelaAlteracao(string grupo)
        {
            var grid = _configuracao.driver.FindElements(By.XPath("//*[@class='table-content ng-scope']"));

            foreach (var item in grid)
            {
                if (item.Text.Split(' ')[0].Equals(grupo))
                {
                    item.Click();
                    break;
                }
            }
            Thread.Sleep(2000);
        }

        private void EncontrarIndexGrupoEClicarExcluir(string nome)
        {
            var index = RetornarIndexGrupoGrid(nome);
            ClicarExcluir(index);
        }

        private void ClicarExcluir(int index)
        {
            string xPath = $"/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div[2]/table/tbody/tr[{index + 2}]/td[3]/div";
            _configuracao.driver.FindElement(By.XPath(xPath)).Click();
            Thread.Sleep(1000);
        }

        private int RetornarIndexGrupoGrid(string grupo)
        {
            var grid = _configuracao.driver.FindElements(By.XPath("//*[@class='table-content ng-scope']"));
            var index = -1;
            bool achou = false;

            foreach (var item in grid)
            {
                index++;
                if (item.Text.Split(' ')[0].Equals(grupo))
                {
                    achou = true;
                    break;
                }
            }

            if (!achou)
                index = -1;

            return index;
        }

        private void ClicarNovo()
        {
            _configuracao.driver.FindElement(By.XPath("html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div[1]/button")).Click();
            Thread.Sleep(2000);
        }

        private void IncluirGrupoEProdutoVinculadoComTamanho()
        {
            using (var unitOfWork = new UnitOfWork())
            using (var appGrupoProduto = new AppGrupoProduto(unitOfWork))
            using (var appTamanhoProduto = new AppTamanhoProduto(unitOfWork))
            using (var appProduto = new AppProduto(unitOfWork))
            {
                var grupoProduto = appGrupoProduto.Incluir(new GrupoProdutoDTO
                {
                    Nome = NOME_VINCULADO_TAMANHO,
                    UsuarioLogado = "usuario"
                });

                var tamanhoProduto = appTamanhoProduto.Incluir(new TamanhoProdutoDTO
                {
                    Ativo = true,
                    Nome = NOME_TAMANHO,
                    Ordem = 1,
                    UsuarioLogado = "usuario"
                });

                var produto = appProduto.Incluir(new ProdutoDTO
                {
                    Ativo = true,
                    GrupoProdutoId = grupoProduto.Id,
                    Nome = NOME_PRODUTO,
                    Valor = 10,
                    UsuarioLogado = "usuario"
                });

                _tamanhoProdutoId = tamanhoProduto.Id;
                _produtoId = produto.Id;
                unitOfWork.Salvar();
            }
        }

        private void ExcluirTamanhoProduto()
        {
            using (var unitOfWork = new UnitOfWork())
            using (var appTamanhoProduto = new AppTamanhoProduto(unitOfWork))
            using (var appProduto = new AppProduto(unitOfWork))
            {
                appTamanhoProduto.Excluir(new TamanhoProdutoDTO
                {
                    Id = _tamanhoProdutoId,
                    UsuarioLogado = "usuario"
                });

                appProduto.RemoverProduto(new ProdutoDTO
                {
                    Id = _produtoId,
                    UsuarioLogado = "usuario"
                });
                unitOfWork.Salvar();
            }
        }
    }
}
