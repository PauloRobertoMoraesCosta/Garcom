using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace Garcom.Teste.Teste_Automatizado
{
    [TestFixture]
    public class Login
    {
        Configuracao _configuracao;

        [SetUp]
        public void Initialize()
        {
            _configuracao = new Configuracao();
            _configuracao.driver = new ChromeDriver();
            _configuracao.driver.Manage().Window.Maximize();
            _configuracao.driver.Url = "http://localhost/Garcom.View/#!/login";
            Thread.Sleep(5000);
        }

        [Test]
        public void LoginTest()
        {
            LoginUsuarioInvalido();
            Thread.Sleep(3000);
            LoginSenhaInvalida();
            Thread.Sleep(3000);
            LoginSucesso();
            Assert.IsTrue(true);
        }

        [TearDown]
        public void EndTest()
        {
            _configuracao.driver.Close();
        }

        public void LoginUsuarioInvalido()
        {
            var usuario = _configuracao.driver.FindElement(By.Name("usuario"));
            usuario.Clear();
            usuario.SendKeys("teste");
            var botao = _configuracao.driver.FindElement(By.Id("logar"));
            botao.Click();
            Thread.Sleep(3000);
            _configuracao.driver.FindElement(By.Name("usuario"));
        }
        
        public void LoginSenhaInvalida()
        {
            var usuario = _configuracao.driver.FindElement(By.Name("usuario"));
            usuario.Clear();
            usuario.SendKeys("usuario");
            var senha = _configuracao.driver.FindElement(By.Name("senha"));
            senha.Clear();
            senha.SendKeys("teste");
            var botao = _configuracao.driver.FindElement(By.Id("logar"));
            botao.Click();
            Thread.Sleep(2000);
            var retorno = _configuracao.driver.FindElement(By.XPath("html/body/div[1]/ul/li/div"));
            Assert.IsTrue(retorno.Text.Contains(_configuracao.Mensagens.GetMensagem("SenhaInvalida")));
        }
        
        public void LoginSucesso()
        {
            var usuario = _configuracao.driver.FindElement(By.Name("usuario"));
            usuario.Clear();
            usuario.SendKeys("usuario");
            var senha = _configuracao.driver.FindElement(By.Name("senha"));
            senha.Clear();
            senha.SendKeys("Usuario1");
            var botao = _configuracao.driver.FindElement(By.Id("logar"));
            botao.Click();
            Thread.Sleep(5000);
            _configuracao.BotaoSair();
            Assert.IsTrue(true);
        }
    }
}
