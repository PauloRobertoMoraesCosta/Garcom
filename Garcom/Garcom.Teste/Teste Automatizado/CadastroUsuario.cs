using NUnit.Framework;
using OpenQA.Selenium;
using System.Threading;

namespace Garcom.Teste.Teste_Automatizado
{
    [TestFixture]
    internal class CadastroUsuario
    {
        const string USUARIO = "teste10";
        const string SENHA = "Usuario1";
        const string SENHA_ALTERADO = "Usuario2";
        const string NOME = "Nome10";
        const string NOME_ALTERADO = USUARIO + "Alterado";
        const string PERFIL_ALTERADO = "Garçom";

        Configuracao _configuracao;
        private IWebElement _usuario;
        private IWebElement _nome;
        private IWebElement _senha;
        private IWebElement _confirmacaoSenha;
        private IWebElement _perfil;

        [SetUp]
        public void Initialize()
        {
            _configuracao = new Configuracao();
            _configuracao.Inicializacao();
            _configuracao.Login();
                                                        
            _configuracao.driver.FindElement(By.XPath("html/body/div[2]/div/div[1]/div[2]/section/ul/li[1]/ul/li[1]/div")).Click();
            Thread.Sleep(1000);
        }

        [Test]
        public void TestarCRUDUsuario()
        {
            ClicarNovo();
            Incluir();
            Thread.Sleep(3000);
            _configuracao.driver.FindElement(By.XPath("html/body/div[2]/div/div[1]/div[2]/section/ul/li[1]/ul/li[1]/div")).Click();
            Thread.Sleep(1000);
            ClicarNovo();
            IncluirUsuarioJaCadastrado();
            ClicarFecha();
            Thread.Sleep(3000);
            Alterar();
        }

        [TearDown]
        public void EndTest()
        {
            _configuracao.Finalizacao();
        }

        private void Incluir()
        {
            ClicaSalvar();
            Thread.Sleep(1000);
            _configuracao.driver.FindElement(By.XPath("html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[1]/div/span"));
            _configuracao.driver.FindElement(By.XPath("html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[2]/div/span"));
            _configuracao.driver.FindElement(By.XPath("html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[4]/div[1]/div/span"));
            PreencherDadosUsuario();
            IncluirUsuarioSenhaDiferenteConfirmacaoSenha(SENHA, "testestestes");
            Thread.Sleep(3000);
            IncluirUsuarioCorreto(USUARIO, SENHA, true, "Cadastro realizado com sucesso.");
            Thread.Sleep(1000);
            LogarComUsuarioCadastrado(USUARIO, SENHA);
        }
        
        private void Alterar()
        {
            AbreTelaAlteracaoUsuario(USUARIO);
            Thread.Sleep(2000);
            PreencheCampoNome(NOME_ALTERADO);
            PreencheCampoPerfil(PERFIL_ALTERADO);
            Thread.Sleep(1000);
            IncluirUsuarioCorreto(USUARIO, SENHA_ALTERADO, false, "Alterações realizadas com sucesso.");
            Thread.Sleep(1000);
            LogarComUsuarioCadastrado(USUARIO, SENHA_ALTERADO);
        }
        
        private void PreencherDadosUsuario(string usuario = USUARIO, string nome = NOME, string perfil = "Administrador")
        {
            PreencheCampoUsuario(usuario);
            PreencheCampoNome(nome);
            PreencheCampoPerfil(perfil);
        }

        private void ClicarNovo()
        {
            _configuracao.driver.FindElement(By.XPath("html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div[1]/button")).Click();
            Thread.Sleep(2000);
        }

        private void ClicarFecha()
        {
            _configuracao.driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[5]/button[1]")).Click();
            Thread.Sleep(2000);
        }

        private void IncluirUsuarioSenhaDiferenteConfirmacaoSenha(string senha, string confirmacaoSenha)
        {
            PreencheCampoSenha(senha);
            PreencheCampoConfirmacaoSenha(confirmacaoSenha);
            ClicaSalvar();
            Thread.Sleep(3000);
            var retorno = _configuracao.driver.FindElement(By.XPath("html/body/div[1]/ul/li/div"));
            Assert.IsTrue(retorno.Text.Contains(_configuracao.Mensagens.GetMensagem("ConfirmacaoSenhaDiferente")));
        }

        private void IncluirUsuarioJaCadastrado()
        {
            PreencherDadosUsuario();
            PreencheCampoSenha(SENHA);
            PreencheCampoConfirmacaoSenha(SENHA);

            ClicaSalvar();
            Thread.Sleep(1000);

            var retorno = _configuracao.driver.FindElement(By.XPath("html/body/div[1]/ul/li/div"));
            Assert.IsTrue(retorno.Text.Contains(_configuracao.Mensagens.GetMensagem("UsuarioJaCadastrado")));
        }

        private void IncluirUsuarioCorreto(string usuario, string senha, bool inclusao, string mensagemRetorno = "Usuário cadastrado com sucesso.")
        {
            if (inclusao)
                PreencheCampoUsuario(usuario);
            PreencheCampoSenha(senha);
            PreencheCampoConfirmacaoSenha(senha);

            ClicaSalvar();
            Thread.Sleep(3000);
            var retorno = _configuracao.driver.FindElement(By.XPath("html/body/div[1]/ul/li/div"));
            Assert.IsTrue(retorno.Text.Contains(mensagemRetorno));
        }

        private void ClicaSalvar()
        {
            var botaoCadastrar = _configuracao.driver.FindElement(By.XPath("html/body/div[2]/div/div[2]/div/div/div/div/div/div/div/div/form/div[5]/button[2]"));
            botaoCadastrar.Click();
        }

        private void LogarComUsuarioCadastrado(string usuario, string senha)
        {
            var botaoSair = _configuracao.driver.FindElement(By.XPath("/html/body/div[2]/div/div[1]/div[1]/section/div[1]"));
            botaoSair.Click();
            Thread.Sleep(1000);
            _configuracao.driver.FindElement(By.Id("ng-confirm-ok")).Click();
            Thread.Sleep(3000);
            _configuracao.Login(usuario, senha);
            Thread.Sleep(1000);
            var retorno = _configuracao.driver.FindElement(By.XPath("/html/body/div[2]/div/div[1]/div[1]/section/div[1]"));
        }

        private void AbreTelaAlteracaoUsuario(string usuario)
        {
            var usuariosGrid = _configuracao.driver.FindElements(By.XPath("//*[@class='table-content ng-scope']"));

            foreach (var usuarioGrid in usuariosGrid)
            {
                if (usuarioGrid.Text.Split(' ')[1].Equals(usuario))
                {
                    usuarioGrid.Click();
                    break;
                }
            }
        }

        private void VerificaDadosCadastroUsuario()
        {
            this._nome = _configuracao.driver.FindElement(By.XPath("//input[@name='nome']"));
            Assert.AreEqual(_nome.Text, NOME_ALTERADO);

            this._perfil = _configuracao.driver.FindElement(By.Name("perfilId"));
            Assert.AreEqual(_perfil.Text, PERFIL_ALTERADO);
        }
        
        private IWebElement PreencheCampoSenha(string valor)
        {
            this._senha = _configuracao.driver.FindElement(By.Name("senha"));
            this._senha.Clear();
            this._senha.SendKeys(valor);
            return this._senha;
        }

        private IWebElement PreencheCampoUsuario(string valor = USUARIO)
        {
            this._usuario = _configuracao.driver.FindElement(By.Name("login"));
            this._usuario.Clear();
            this._usuario.SendKeys(USUARIO);

            return this._usuario;
        }

        private IWebElement PreencheCampoNome(string valor)
        {
            this._nome = _configuracao.driver.FindElement(By.Name("nome"));
            this._nome.Clear();
            this._nome.SendKeys(valor);

            return this._nome;
        }

        private IWebElement PreencheCampoPerfil(string valor)
        {
            this._perfil = _configuracao.driver.FindElement(By.Name("perfilId"));
            this._perfil.SendKeys(Keys.Delete);
            this._perfil.SendKeys(valor);

            return _perfil;
        }

        private IWebElement PreencheCampoConfirmacaoSenha(string valor)
        {
            this._confirmacaoSenha = _configuracao.driver.FindElement(By.Name("confirmacaoSenha"));
            this._confirmacaoSenha.Clear();
            this._confirmacaoSenha.SendKeys(valor);

            return _confirmacaoSenha;
        }
    }
}
