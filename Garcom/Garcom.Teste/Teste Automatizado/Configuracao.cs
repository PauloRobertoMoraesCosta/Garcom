using Garcom.Core;
using Garcom.Dominio.Entidade.Mapeamento;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace Garcom.Teste.Teste_Automatizado
{
    internal class Configuracao
    {
        public IWebDriver driver;
        private IGerenciadorMensagens _mensagem;
        public IGerenciadorMensagens Mensagens
        {
            get
            {
                if (_mensagem == null)
                    _mensagem = new GerenciadorMensagensRetorno();

                return _mensagem;
            }
        }
        public Configuracao()
        {
            MapperConfig.ConfigurarMapper();
        }

        public void Inicializacao()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "http://localhost/Garcom.View/#!/login";
            Thread.Sleep(5000);
        }

        public void Finalizacao()
        {
            driver.Close();
        }

        public void Login(string login = "usuario", string password = "Usuario1")
        {
            var usuario = driver.FindElement(By.Name("usuario"));
            usuario.Clear();
            usuario.SendKeys(login);
            var senha = driver.FindElement(By.Name("senha"));
            senha.Clear();
            senha.SendKeys(password);
            var botao = driver.FindElement(By.Id("logar"));
            botao.Click();
            Thread.Sleep(7000);
        }

        public IWebElement BotaoSair()
        {
            return this.driver.FindElement(By.XPath("/html/body/div[2]/div/div[1]/div[1]/section/div[1]"));
        }

        public IWebElement CampoCadastros()
        {
            return this.driver.FindElement(By.XPath("/html/body/div[2]/div/div[1]/div[2]/section/ul/li[1]/div"));
        }
    }
}
