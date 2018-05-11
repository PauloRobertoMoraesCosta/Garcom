using Garcom.Core;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;

namespace Garcom.Teste.Teste_Automatizado
{
    [TestFixture]
    public class CadastroMesa
    {
        const string MESA = "Mesadetestes";
        const string MESAALTERADA = "Mesadetestesalterada";
        IGerenciadorMensagens _mensagens;
        Configuracao _configuracao;

        private IWebElement _mesa;
        
        [SetUp]
        public void Initialize()
        {
            _mensagens = new GerenciadorMensagensRetorno();
            _configuracao = new Configuracao();
            _configuracao.Inicializacao();
            _configuracao.Login();

            _configuracao.driver.FindElement(By.XPath("/html/body/div[2]/div/div[1]/div[2]/section/ul/li[1]/ul/li[6]/div")).Click();
            Thread.Sleep(1000);

        }

        [Test]
        public void TestarCRUDMesa()
        {
            Thread.Sleep(3000);
            botaoNovo().Click();
            Thread.Sleep(2000);

            Assert.IsTrue(InsereDescricaoVazio().Contains("O campo é obrigatório"));

            Thread.Sleep(1000);
            PreencherDadosMesa();
            Thread.Sleep(1000);
            botaoCadastrar().Click();

            Thread.Sleep(1000);

            Assert.IsTrue(Toast().Text.Contains("Mesa cadastrada com sucesso."));

            Thread.Sleep(1000);

            Assert.IsTrue(_mensagens.GetMensagem("MesaJaCadastrado").Contains(InsereMesaJaCadastrada()));

            Thread.Sleep(1000);

            botaoCancelar().Click();
            Thread.Sleep(2000);

            AbreTelaAlteracaoMesa(MESA);
            Thread.Sleep(1000);

            PreencherDadosMesa(MESAALTERADA);
            botaoCadastrar().Click();
            Thread.Sleep(1000);

            Assert.IsTrue(Toast().Text.Contains("Mesa alterada com sucesso."));

            Thread.Sleep(3000);
            Excluir(MESAALTERADA);
            Thread.Sleep(3000);
            //Assert.IsTrue(Toast().Text.Contains("Mesa excluída com sucesso."));
        }
        
        [TearDown]
        public void EndTest()
        {
            _configuracao.Finalizacao();
        }

        private string InsereDescricaoVazio()
        {
            botaoCadastrar().Click();
            Thread.Sleep(1000);
            return _configuracao.driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[1]/div/span")).Text;
            
        }

        private void PreencherDadosMesa(string descricao = "")
        {
            _mesa = _configuracao.driver.FindElement(By.Name("descricao"));
            _mesa.Clear();

            if (!string.IsNullOrWhiteSpace(descricao))
                _mesa.SendKeys(descricao);
            else
                _mesa.SendKeys(MESA);

            Thread.Sleep(1000);
        }

        private string InsereMesaJaCadastrada()
        {
            botaoNovo().Click();
            Thread.Sleep(2000);
            PreencherDadosMesa(MESA);
            botaoCadastrar().Click();
            Thread.Sleep(500);
            return Toast().Text;
        }

        private void AbreTelaAlteracaoMesa(string descricao)
        {
            var mesaGrid = _configuracao.driver.FindElements(By.XPath("//*[@class='table-content ng-scope']"));

            foreach (var mesa in mesaGrid)
            {
                if (mesa.Text.Split(' ')[0].Equals(descricao))
                {
                    mesa.Click();
                    break;
                }
            }
            Thread.Sleep(3000);
        }


        private void Excluir(string descricao)
        {
            EncontrarIndexMesaEClicarExcluir(descricao);
            var index = RetornarIndexMesaGrid(descricao);
            Assert.IsTrue(index == -1);
        }

        private void EncontrarIndexMesaEClicarExcluir(string descricao)
        {
            var index = RetornarIndexMesaGrid(descricao);
            ClicarExcluir(index);
        }

        private int RetornarIndexMesaGrid(string descricao)
        {
            var grid = _configuracao.driver.FindElements(By.XPath("//*[@class='table-content ng-scope']"));
            var index = -1;
            bool achou = false;

            foreach (var item in grid)
            {
                index++;
                if (item.Text.Split(' ')[0].Equals(descricao))
                {
                    achou = true;
                    break;
                }
            }

            if (!achou)
                index = -1;

            return index;
        }

        private void ClicarExcluir(int index)
        {
            string xPath = $"/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div[2]/table/tbody/tr[{index + 2}]/td[3]/div";
            _configuracao.driver.FindElement(By.XPath(xPath)).Click();
            Thread.Sleep(3000);

            //acha botão de confirmação
           /*  xPath = "//*[@id='ng-confirm-1']/div/div/div/button[1]";
            _configuracao.driver.FindElement(By.XPath(xPath)).Click();
            Thread.Sleep(3000);*/
        }

        private IWebElement botaoCadastrar()
        {
            return _configuracao.
                driver.
                FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[3]/button[2]"));

        }

        private IWebElement botaoNovo()
        {
            return _configuracao.driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div[1]/button"));
        }

        private IWebElement botaoCancelar()
        {
            return _configuracao.driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[3]/button[1]"));
        }

        private IWebElement Toast()
        {
            return _configuracao.driver.FindElement(By.XPath("html/body/div[1]/ul/li/div"));
        }

        
    }

    
}
