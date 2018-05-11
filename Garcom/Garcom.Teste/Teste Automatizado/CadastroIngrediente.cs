using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;

namespace Garcom.Teste.Teste_Automatizado
{
    internal class CadastroIngrediente
    {
        const string DESCRICAO = "Ingrediente15";
        const string DESCRICACAO_ALTERADO = DESCRICAO + "Alterado";

        Configuracao _configuracao;

        [SetUp]
        public void Initialize()
        {
            _configuracao = new Configuracao();
            _configuracao.Inicializacao();
            _configuracao.Login();
            Thread.Sleep(100);
            _configuracao.CampoCadastros().Click();
            Thread.Sleep(100);
            _configuracao.driver.FindElement(By.XPath("html/body/div[2]/div/div[1]/div[2]/section/ul/li[1]/ul/li[2]/div")).Click();
            Thread.Sleep(1000);
        }

        [Test]
        public void TestarCRUDIngrediente()
        {
            ClicarNovo();
            Incluir();
            Thread.Sleep(3000);
            ClicarNovo();
            IncluirIngredienteJaCadastrado();
            ClicarFechar();
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
            Thread.Sleep(1000);
            _configuracao.driver.FindElement(By.XPath("html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[1]/div/span"));
            IncluirCorreto(DESCRICAO, "Ingrediente cadastrado com sucesso.");
        }

        private void Alterar()
        {
            AbrirTelaAlteracao(DESCRICAO);
            AlterarCheckBoxAtivo();
            //Thread.Sleep(1000);
            //var descricao = _configuracao.driver.FindElement(By.Name("descricao"));
            //var teste = descricao.GetAttribute("value");
            IncluirCorreto(DESCRICACAO_ALTERADO, "Ingrediente alterado com sucesso.");
        }

        private void Excluir()
        {
            EncontrarIndexIngredienteEClicarExcluir();
            var index = RetornarIndexIngredienteGrid(DESCRICACAO_ALTERADO);
            Assert.IsTrue(index == -1);
        }

        private void Desfazer()
        {
            EncontrarIndexIngredienteEClicarExcluir();
            _configuracao.driver.FindElement(By.XPath("/html/body/div[1]/ul/li/div/a")).Click();
            Thread.Sleep(2000);
            var index = RetornarIndexIngredienteGrid(DESCRICACAO_ALTERADO);
            Assert.IsTrue(index != -1);
        }

        private void ClicarNovo()
        {
            _configuracao.driver.FindElement(By.XPath("html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div[1]/button")).Click();
            Thread.Sleep(2000);
        }

        private void ClicarFechar()
        {
            _configuracao.driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[3]/button[1]")).Click();
            Thread.Sleep(2000);
        }

        private void EncontrarIndexIngredienteEClicarExcluir()
        {
            var index = RetornarIndexIngredienteGrid(DESCRICACAO_ALTERADO);
            ClicarExcluir(index);
        }

        private void IncluirIngredienteJaCadastrado()
        {
            PreencherCampoNome(DESCRICAO);
            ClicarSalvar();
            var retorno = _configuracao.driver.FindElement(By.XPath("/html/body/div[1]/ul/li/div"));
            Assert.AreEqual(retorno.Text, _configuracao.Mensagens.GetMensagem("IngredienteJaCadastrado"));
        }

        public void IncluirCorreto(string descricao, string msgRetorno)
        {
            PreencherCampoNome(descricao);
            ClicarSalvar();
            var retorno = _configuracao.driver.FindElement(By.XPath("/html/body/div[1]/ul/li/div"));
            Assert.AreEqual(retorno.Text, msgRetorno);
        }

        private void AbrirTelaAlteracao(string ingrediente)
        {
            var grid = _configuracao.driver.FindElements(By.XPath("//*[@class='table-content ng-scope']"));

            foreach (var item in grid)
            {
                if (item.Text.Split(' ')[0].Equals(ingrediente))
                {
                    item.Click();
                    break;
                }
            }
            Thread.Sleep(2000);
        }

        private int RetornarIndexIngredienteGrid(string ingrediente)
        {
            var grid = _configuracao.driver.FindElements(By.XPath("//*[@class='table-content ng-scope']"));
            var index = 0;
            bool achou = false;

            foreach (var item in grid)
            {
                index++;
                if (item.Text.Split(' ')[0].Equals(ingrediente))
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
            string xPath = $"/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div[2]/table/tbody/tr[{index + 2}]/td[3]/button";
            _configuracao.driver.FindElement(By.XPath(xPath)).Click();
            Thread.Sleep(1000);
        }

        private void ClicarSalvar()
        {
            _configuracao.driver.FindElement(By.XPath("html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[3]/button[2]")).Click();
            Thread.Sleep(2000);
        }

        private void PreencherCampoNome(string valor)
        {
            var descricao = _configuracao.driver.FindElement(By.Name("descricao"));
            descricao.Clear();
            descricao.SendKeys(valor);
        }

        private void AlterarCheckBoxAtivo()
        {
            _configuracao.driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[2]/div[1]/checkbox/div/div/div")).Click();
        }
    }
}
