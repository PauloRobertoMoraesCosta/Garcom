using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;

namespace Garcom.Teste.Teste_Automatizado
{
    internal class CadastroTamanhoProduto
    {
        const string NOME = "Tamanho4";
        const string NOME_ALTERADO = NOME + "Alterado";
        
        Configuracao _configuracao;

        [SetUp]
        public void Initialize()
        {
            _configuracao = new Configuracao();
            _configuracao.Inicializacao();
            _configuracao.Login();
            _configuracao.CampoCadastros().Click();
            Thread.Sleep(1000);
            _configuracao.driver.FindElement(By.XPath("/html/body/div[2]/div/div[1]/div[2]/section/ul/li[1]/ul/li[4]/div")).Click();
            Thread.Sleep(1000);
        }

        [Test]
        public void TestarCRUDTamanho()
        {
            ClicarNovo();
            Incluir();
            Thread.Sleep(3000);
            ClicarNovo();
            IncluirTamanhoCadastrado();
            Thread.Sleep(3000);
            Alterar();
            Thread.Sleep(3000);
            Desfazer();
            Thread.Sleep(3000);
            Excluir();
        }

        [TearDown]
        public void EndTest()
        {
            _configuracao.Finalizacao();
        }

        private void Incluir()
        {
            ClicarSalvar();
            VerificarCamposVazios();
            PreencherCampoNome(NOME);
            Thread.Sleep(500);
            ClicarSalvar();
            Assert.IsTrue(Toast().Text.Contains("Tamanho cadastrado com sucesso."));
        }

        private void Alterar()
        {
            AbrirTelaAlteracao(NOME);
            PreencherCampoNome(NOME_ALTERADO);
            ClicarSalvar();
            Assert.IsTrue(Toast().Text.Contains("Tamanho cadastrado com sucesso."));
        }

        private void Excluir()
        {
            var index = RetornarIndexTamanhoGrid(NOME_ALTERADO);
            ClicarExcluir(index);
            index = RetornarIndexTamanhoGrid(NOME_ALTERADO);
            Assert.IsTrue(index == -1);
        }

        private void VerificarCamposVazios()
        {
            _configuracao.driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[1]/div/span"));
        }
        
        private void IncluirTamanhoCadastrado()
        {
            PreencherCampoNome(NOME);
            ClicarSalvar();
            Assert.IsTrue(Toast().Text.Contains(_configuracao.Mensagens.GetMensagem("TamanhoJaCadastrado")));
        }

        private void AbrirTelaAlteracao(string tamanho)
        {
            var grid = _configuracao.driver.FindElements(By.XPath("//*[@class='table-content ng-scope']"));

            foreach (var item in grid)
            {
                if (item.Text.Split(' ')[0].Equals(tamanho))
                {
                    item.Click();
                    break;
                }
            }
            Thread.Sleep(2000);
        }

        private void ClicarSalvar()
        {
            _configuracao.driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[3]/button[2]")).Click();
            Thread.Sleep(1000);
        }

        private void ClicarNovo()
        {
            _configuracao.driver.FindElement(By.XPath("html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div[1]/button")).Click();
            Thread.Sleep(2000);
        }

        private void ClicarCancelar()
        {
            _configuracao.driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[4]/button[1]")).Click();
            Thread.Sleep(2000);
        }

        private void PreencherCampoNome(string nome)
        {
            var campo = _configuracao.driver.FindElement(By.Name("descricao"));
            campo.Clear();
            campo.SendKeys(nome);
        }

        private IWebElement Toast()
        {
            return _configuracao.driver.FindElement(By.XPath("html/body/div[1]/ul/li/div"));
        }

        private void ClicarExcluir(int index)
        {
            string xPath = $"/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div[2]/table/tbody/tr[{index + 2}]/td[4]/div";
            _configuracao.driver.FindElement(By.XPath(xPath)).Click();
            Thread.Sleep(2000);
        }

        private void Desfazer()
        {
            var index = RetornarIndexTamanhoGrid(NOME_ALTERADO);
            ClicarExcluir(index);
            _configuracao.driver.FindElement(By.XPath("/html/body/div[1]/ul/li/div/a")).Click();
            Thread.Sleep(2000);
            index = RetornarIndexTamanhoGrid(NOME_ALTERADO);
            Assert.IsTrue(index != -1);
        }

        private int RetornarIndexTamanhoGrid(string tamanho)
        {
            var grid = _configuracao.driver.FindElements(By.XPath("//*[@class='table-content ng-scope']"));
            var index = -1;
            bool achou = false;

            foreach (var item in grid)
            {
                index++;
                if (item.Text.Split(' ')[0].Equals(tamanho))
                {
                    achou = true;
                    break;
                }
            }

            if (!achou)
                index = -1;

            return index;
        }
    }
}
