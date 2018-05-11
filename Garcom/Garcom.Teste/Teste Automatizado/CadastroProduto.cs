using Garcom.Aplicacao.Implementacao;
using Garcom.Dominio.Entidade.DTOs;
using Garcom.Infra.UnitOfWork;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Threading;

namespace Garcom.Teste.Teste_Automatizado
{
    internal class CadastroProduto
    {
        const string NOME = "Produto";
        const string NOME_ALTERADO = NOME + "Alterado";
        const string VALOR = "1000";
        const string VALOR2 = "1200";
        const string VALOR3 = "500";
        const string VALOR_ALTERADO = "2050";
        const string VALOR_ADICIONAL = "111";
        const string VALOR_ADICIONAL2 = "1112";
        const string VALOR_ADICIONAL3 = "333";
        const string VALOR_ADICIONAL_ALTERADO = "222";
        const string CODIGO_RAPIDO = "999";
        const string CODIGO_RAPIDO_ALTERADO = "888";
        const string GRUPO_PRODUTO = "GrupoProduto3";
        const string GRUPO_PRODUTO_VINCULADO_UM_TAMANHO = "GrupoProdutoTamanho4";
        const string GRUPO_PRODUTO_VINCULADO_TRES_TAMANHOS = "GrupoProdutoTresTamanho5";
        const string TAMANHO1 = "Tamanho9";
        const string TAMANHO2 = "Tamanho10";
        const string TAMANHO3 = "Tamanho11";
        const string TAMANHO4 = "Tamanho12";
        const string INGREDIENTE1 = "IngredienteTeste1";
        const string INGREDIENTE2 = "IngredienteTeste2";
        const string INGREDIENTE3 = "IngredienteTeste3";

        Configuracao _configuracao;
        private int[] _grupoProdutosIds;
        private int[] _tamanhosIds;
        private int[] _ingredientesIds;

        [SetUp]
        public void Initialize()
        {
            _configuracao = new Configuracao();
            _configuracao.Inicializacao();
            _configuracao.Login();

            //IncluirGrupoETamanho();
            _configuracao.driver.FindElement(By.XPath("/html/body/div[2]/div/div[1]/div[2]/section/ul/li[1]/ul/li[5]/div")).Click();
            Thread.Sleep(1000);
        }

        [Test]
        public void TestarCRUDProduto()
        {
            ClicarNovo();
            ClicarSalvar();
            VerificarCamposObrigatorios();
            IncluirSemVinculoTamanho(GRUPO_PRODUTO);
            Thread.Sleep(2000);
            ClicarNovo();
            Thread.Sleep(1000);
            IncluirJaCadastrado();
            Thread.Sleep(2000);
            Alterar();
            Thread.Sleep(3000);
            Desfazer();
            Thread.Sleep(2000);
            Excluir();
            Thread.Sleep(2000);
            ClicarNovo();
            IncluirSemVinculoTamanho(GRUPO_PRODUTO_VINCULADO_UM_TAMANHO);
            Thread.Sleep(2000);
            Alterar();
            Thread.Sleep(2000);
            Excluir();
            Thread.Sleep(2000);
            ClicarNovo();
            IncluirComVinculoTamanho();
            Thread.Sleep(2000);
            AlterarComVinculoTamanho();
            Thread.Sleep(2000);
            Excluir();
        }

        [TearDown]
        public void EndTest()
        {
            //ExcluirGrupoETamanho();
            _configuracao.Finalizacao();
        }
        
        private void IncluirJaCadastrado()
        {
            VisualizarIngrediente();
            PreencherDadosSemVinculoTamanho(NOME, GRUPO_PRODUTO);
            ClicarSalvar();
            Thread.Sleep(1000);
            Assert.AreEqual(RetornaMensagemToast(), _configuracao.Mensagens.GetMensagem("ProdutoJaCadastrado"));
            Thread.Sleep(2000);
            PreencherCampoNome(NOME_ALTERADO);
            ClicarSalvar();
            Thread.Sleep(1000);
            Assert.AreEqual(RetornaMensagemToast(), _configuracao.Mensagens.GetMensagem("CodigoRapidoJaCadastrado"));
            ClicarCancelar();
        }

        private void IncluirSemVinculoTamanho(string grupoProduto)
        {
            PreencherCampoNome(NOME);
            PreencherCampoGrupoProduto(grupoProduto);
            PreencherCampoValor(VALOR);
            PreencherCampoCodigoRapido(CODIGO_RAPIDO);
            VisualizarIngrediente();
            PreencherCampoIngredientes(INGREDIENTE1, 1);
            CheckCampoOpcional(1);
            PreencherCampoIngredientes(INGREDIENTE2, 2);
            CheckCampoAdicional(2);
            ClicarSalvar();
            Thread.Sleep(1000);
            _configuracao.driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[2]/div/div/section/div[2]/div[3]/div/div[2]"));
            PreencherCampoValorAdicional(VALOR_ADICIONAL, 2);
            PreencherCampoIngredientes(INGREDIENTE3, 3);
            CheckCampoAdicional(3);
            PreencherCampoValorAdicional(VALOR_ADICIONAL, 3);
            ClicarSalvar();
        }

        private void IncluirComVinculoTamanho()
        {
            PreencherCampoNome(NOME);
            PreencherCampoGrupoProduto(GRUPO_PRODUTO_VINCULADO_TRES_TAMANHOS);
            _configuracao.driver.FindElement(By.XPath("//*[@id=\"ng-confirm-ok\"]")).Click();
            Thread.Sleep(500);
            PreencherCampoCodigoRapido(CODIGO_RAPIDO);
            PreencherValorComTamanho(VALOR, VALOR2, VALOR3);
            VisualizarIngrediente();
            PreencherCampoIngredientes(INGREDIENTE1, 1);
            CheckCampoOpcional(1);
            PreencherCampoIngredientes(INGREDIENTE2, 2);
            CheckCampoAdicional(2);
            ClicarSalvar();
            Thread.Sleep(1000);
            _configuracao.driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[2]/div/div/section/div[2]/div[4]/div"));
            PreencherValorIngredienteComTamanho(2, VALOR_ADICIONAL, VALOR_ADICIONAL2, VALOR_ADICIONAL3);
            PreencherCampoIngredientes(INGREDIENTE3, 3);
            CheckCampoAdicional(3);
            PreencherValorIngredienteComTamanho(3, VALOR_ADICIONAL2, VALOR_ADICIONAL3, VALOR_ADICIONAL);
            ClicarSalvar();
        }

        public void Desfazer()
        {
            var index = RetornarIndexGrid(NOME_ALTERADO);
            ClicarExcluir(index);
            _configuracao.driver.FindElement(By.XPath("/html/body/div[1]/ul/li/div/a")).Click();
            Thread.Sleep(2000);
            index = RetornarIndexGrid(NOME_ALTERADO);
            Assert.IsTrue(index != -1);
        }

        public void Excluir()
        {
            var index = RetornarIndexGrid(NOME_ALTERADO);
            ClicarExcluir(index);
            index = RetornarIndexGrid(NOME_ALTERADO);
            Assert.IsTrue(index == -1);
        }

        private void PreencherValorComTamanho(string valor, string valor2, string valor3)
        {
            ClicarBotaoDefinirValorProduto();
            Thread.Sleep(1000);
            PreencherValorProdutoTamanho(valor, 1);
            PreencherValorProdutoTamanho(valor2, 2);
            PreencherValorProdutoTamanho(valor3, 3);
            ClicarGravarDefinirValores();
            Thread.Sleep(1000);
        }

        private void PreencherValorIngredienteComTamanho(int index, string valor, string valor2, string valor3)
        {
            ClicarBotaoDefinirValorProdutoIngrediente(index);
            Thread.Sleep(1000);
            PreencherValorProdutoTamanho(valor, 1);
            PreencherValorProdutoTamanho(valor2, 2);
            PreencherValorProdutoTamanho(valor3, 3);
            ClicarGravarDefinirValoresIngrediente();
            Thread.Sleep(1000);
        }

        private void PreencherDadosSemVinculoTamanho(string nome, string grupoProduto)
        {
            PreencherCampoNome(nome);
            PreencherCampoGrupoProduto(grupoProduto);
            PreencherCampoValor(VALOR);
            PreencherCampoCodigoRapido(CODIGO_RAPIDO);
            PreencherCampoIngredientes(INGREDIENTE1, 1);
            CheckCampoOpcional(1);
            PreencherCampoIngredientes(INGREDIENTE2, 2);
            CheckCampoAdicional(2);
            PreencherCampoValorAdicional("111", 2);
            PreencherCampoIngredientes(INGREDIENTE3, 3);
            CheckCampoAdicional(3);
            PreencherCampoValorAdicional("123", 3);
        }

        private void Alterar()
        {
            AbrirTelaAlteracao(NOME);
            ClicarVisualizarIngrediente();
            PreencherCampoNome(NOME_ALTERADO);
            PreencherCampoGrupoProduto(GRUPO_PRODUTO_VINCULADO_UM_TAMANHO);
            PreencherCampoValor(VALOR_ALTERADO);
            PreencherCampoCodigoRapido(CODIGO_RAPIDO_ALTERADO);
            RemoverIngrediente(3);
            CheckCampoAdicional(1);
            CheckCampoOpcional(1);
            PreencherCampoValorAdicional(VALOR_ADICIONAL_ALTERADO,  1);
            CheckCampoAdicional(2);
            CheckCampoOpcional(2);
            ClicarAlterar();
        }

        private void AlterarComVinculoTamanho()
        {
            AbrirTelaAlteracao(NOME);
            ClicarVisualizarIngrediente();
            PreencherCampoNome(NOME_ALTERADO);
            CheckCampoAtivo();
            PreencherValorComTamanho(VALOR2, VALOR3, VALOR);
            PreencherCampoCodigoRapido(CODIGO_RAPIDO_ALTERADO);
            RemoverIngredienteVinculadoTamanho(3);
            CheckCampoAdicional(1);
            CheckCampoOpcional(1);
            PreencherValorIngredienteComTamanho(1, VALOR_ADICIONAL_ALTERADO, VALOR_ADICIONAL, VALOR_ADICIONAL3);
            CheckCampoAdicional(2);
            CheckCampoOpcional(2);
            ClicarAlterar();
        }

        private void AbrirTelaAlteracao(string produto)
        {
            var grid = _configuracao.driver.FindElements(By.XPath("//*[@class='table-content ng-scope']"));

            foreach (var item in grid)
            {
                if (item.Text.Split(' ')[0].Equals(produto))
                {
                    item.Click();
                    break;
                }
            }
            Thread.Sleep(2000);
        }

        private void VisualizarIngrediente()
        {
            _configuracao.driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[2]/div/div/button"));
            Thread.Sleep(1000);
        }

        private void ClicarVisualizarIngrediente()
        {
            _configuracao.driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[2]/div/div/button")).Click();
            Thread.Sleep(1000);
        }

        private void PreencherCampoNome(string nome)
        {
            var campoNome = _configuracao.driver.FindElement(By.Name("nome"));
            campoNome.Clear();
            campoNome.SendKeys(nome);
        }

        private void PreencherCampoGrupoProduto(string grupo)
        {
            var campoNome = _configuracao.driver.FindElement(By.Name("nomeGrupoProduto"));
            campoNome.SendKeys(grupo);
        }

        private void PreencherCampoIngredientes(string ingrediente, int index)
        {
            IWebElement campoIngrediente = null;

            if (index == 0 || index == 1)
                campoIngrediente = _configuracao.driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[2]/div/div/section/div/div[1]/select"));
            else
                campoIngrediente = _configuracao.driver.FindElement(By.XPath($"/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[2]/div/div/section/div[{index}]/div[1]/select"));

            campoIngrediente.SendKeys(ingrediente);
        }

        private void RemoverIngrediente(int index)
        {
            _configuracao.driver.FindElement(By.XPath($"/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[2]/div/div/section/div[{index}]/div[3]/div/div/div[2]/button")).Click();
        }

        private void RemoverIngredienteVinculadoTamanho(int index)
        {
            _configuracao.driver.FindElement(By.XPath($"/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[2]/div/div/section/div[{index}]/div[3]/div/div[2]/button")).Click();
        }

        private void CheckCampoOpcional(int index)
        {
            _configuracao.driver.FindElement(By.XPath($"/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[2]/div/div/section/div[{index}]/div[2]/div[1]/checkbox/div/div/div")).Click();
        }

        private void CheckCampoAdicional(int index)
        {
            _configuracao.driver.FindElement(By.XPath($"/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[2]/div/div/section/div[{index}]/div[2]/div[2]/checkbox/div/div/div")).Click();
        }

        private void CheckCampoAtivo()
        {
            _configuracao.driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[3]/checkbox/div/div/div")).Click();
        }

        private void PreencherCampoValor(string valor)
        {
            var campoNome = _configuracao.driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[1]/div[1]/div[2]/div[2]/input"));
            campoNome.Clear();
            campoNome.SendKeys(valor);
        }

        private void PreencherCampoValorAdicional(string valor, int index)
        {
            var campoValorAdicional = _configuracao.driver.FindElement(By.XPath($"/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[2]/div/div/section/div[{index}]/div[3]/div/div/div[1]/input"));
            campoValorAdicional.Clear();
            campoValorAdicional.SendKeys(valor);
        }

        private void PreencherCampoCodigoRapido(string codigo)
        {
            var campoNome = _configuracao.driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[1]/div[1]/div[2]/div[3]/input"));
            campoNome.Clear();
            campoNome.SendKeys(codigo);
        }

        private void ClicarNovo()
        {
            _configuracao.driver.FindElement(By.XPath("html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div[1]/button")).Click();
            Thread.Sleep(2000);
        }

        private void ClicarSalvar()
        {
            _configuracao.driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[3]/button[2]")).Click();
            Thread.Sleep(2000);
        }

        private void ClicarAlterar()
        {
            _configuracao.driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[4]/button[2]")).Click();
        }

        private void ClicarCancelar()
        {
            _configuracao.driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[3]/button[1]")).Click();
            Thread.Sleep(2000);
        }

        private void VerificarCamposObrigatorios()
        {
            //Campo Nome
            _configuracao.driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[1]/div[1]/div[1]/div/span")).Click();

            //Campo Grupo Produto
            _configuracao.driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[1]/div[1]/div[2]/div[1]/div/span")).Click();

            //Campo Valor
            _configuracao.driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[1]/div[1]/div[2]/div[2]/div")).Click();
        }

        private void ClicarExcluir(int index)
        {
            string xPath = $"/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div[2]/table/tbody/tr[{index + 2}]/td[5]/div";
            _configuracao.driver.FindElement(By.XPath(xPath)).Click();
            Thread.Sleep(1000);
        }

        private int RetornarIndexGrid(string produto)
        {
            var grid = _configuracao.driver.FindElements(By.XPath("//*[@class='table-content ng-scope']"));
            var index = -1;
            bool achou = false;

            foreach (var item in grid)
            {
                index++;
                if (item.Text.Split(' ')[0].Equals(produto))
                {
                    achou = true;
                    break;
                }
            }

            if (!achou)
                index = -1;

            return index;
        }

        private string RetornaMensagemToast()
        {
            var toast = _configuracao.driver.FindElement(By.XPath("/html/body/div[1]/ul/li/div"));
            return toast.Text;
        }

        private void ClicarBotaoDefinirValorProduto()
        {
            _configuracao.driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[1]/div[1]/div[2]/div[2]/button")).Click();
        }

        private void ClicarBotaoDefinirValorProdutoIngrediente(int index)
        {
            _configuracao.driver.FindElement(By.XPath($"/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[2]/div/div/section/div[{index}]/div[3]/div/div[1]/button")).Click();
        }

        private void PreencherValorProdutoTamanho(string valor, int index)
        {
            var campoValor = _configuracao.driver.FindElement(By.XPath($"/html/body/div[1]/div/div/div[2]/form/div[1]/div[2]/div[{index}]/div[2]/input"));
            campoValor.Clear();
            campoValor.SendKeys(valor);
        }

        public void ClicarGravarDefinirValores()
        {
            _configuracao.driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/form/div[2]/div[2]/button[2]")).Click();
        }

        public void ClicarGravarDefinirValoresIngrediente()
        {
            _configuracao.driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/form/div[2]/button[2]")).Click();
        }
        
        public void IncluirGrupoETamanho()
        {
            using (var unitOfWork = new UnitOfWork())
            using (var appGrupoProduto = new AppGrupoProduto(unitOfWork))
            using (var appTamanhoProduto = new AppTamanhoProduto(unitOfWork))
            using (var appIngrediente = new AppIngrediente(unitOfWork))
            {
                _grupoProdutosIds = new int[3];
                _tamanhosIds = new int[4];
                _ingredientesIds = new int[3];

                _grupoProdutosIds[0] = appGrupoProduto.Incluir(new GrupoProdutoDTO
                {
                    Nome = GRUPO_PRODUTO,
                    UsuarioLogado = "usuario"
                }).Id;
                _grupoProdutosIds[1] = appGrupoProduto.Incluir(new GrupoProdutoDTO
                {
                    Nome = GRUPO_PRODUTO_VINCULADO_UM_TAMANHO,
                    UsuarioLogado = "usuario"
                }).Id;
                _grupoProdutosIds[2] = appGrupoProduto.Incluir(new GrupoProdutoDTO
                {
                    Nome = GRUPO_PRODUTO_VINCULADO_TRES_TAMANHOS,
                    UsuarioLogado = "usuario"
                }).Id;
                _tamanhosIds[0] = appTamanhoProduto.Incluir(new TamanhoProdutoDTO
                {
                    Ativo = true,
                    Nome = TAMANHO1,
                    Ordem = 1,
                    UsuarioLogado = "usuario"
                }).Id;

                _tamanhosIds[1] = appTamanhoProduto.Incluir(new TamanhoProdutoDTO
                {
                    Ativo = true,
                    Nome = TAMANHO2,
                    Ordem = 1,
                    UsuarioLogado = "usuario"
                }).Id;

                _tamanhosIds[2] = appTamanhoProduto.Incluir(new TamanhoProdutoDTO
                {
                    Ativo = true,
                    Nome = TAMANHO3,
                    Ordem = 2,
                    UsuarioLogado = "usuario"
                }).Id;

                _tamanhosIds[3] = appTamanhoProduto.Incluir(new TamanhoProdutoDTO
                {
                    Ativo = true,
                    Nome = TAMANHO4,
                    Ordem = 3,
                    UsuarioLogado = "usuario"
                }).Id;

                _ingredientesIds[0] = appIngrediente.Incluir(new IngredienteDTO
                {
                    Descricao = INGREDIENTE1,
                    UsuarioLogado = "usuario",
                    Ativo = true,
                    DataCadastro = DateTime.Now
                }).Id;

                _ingredientesIds[1] = appIngrediente.Incluir(new IngredienteDTO
                {
                    Descricao = INGREDIENTE2,
                    UsuarioLogado = "usuario",
                    Ativo = true,
                    DataCadastro = DateTime.Now
                }).Id;

                _ingredientesIds[2] = appIngrediente.Incluir(new IngredienteDTO
                {
                    Descricao = INGREDIENTE3,
                    UsuarioLogado = "usuario",
                    Ativo = true,
                    DataCadastro = DateTime.Now
                }).Id;
            }
        }

        public void ExcluirGrupoETamanho()
        {
            using (var unitOfWork = new UnitOfWork())
            using (var appGrupoProduto = new AppGrupoProduto(unitOfWork))
            using (var appTamanhoProduto = new AppTamanhoProduto(unitOfWork))
            using (var appIngrediente = new AppIngrediente(unitOfWork))
            {
                foreach (var item in _tamanhosIds)
                    appTamanhoProduto.Excluir(new TamanhoProdutoDTO { Id = item, UsuarioLogado = "usuario" });
                foreach (var item in _grupoProdutosIds)
                    appGrupoProduto.Excluir(new GrupoProdutoDTO { Id = item, UsuarioLogado = "usuario" });
                foreach (var item in _ingredientesIds)
                    appIngrediente.Excluir(new IngredienteDTO { Id = item, UsuarioLogado = "usuario" });

                unitOfWork.Salvar();
            }
        }
    }
}
