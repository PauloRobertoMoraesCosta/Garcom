using Garcom.Core;
using Garcom.Dominio.Entidade.Models;
using Garcom.Dominio.Servico.Servico;
using Garcom.Teste.Testes.Construtor;
using NUnit.Framework;
using System;

namespace Garcom.Teste.Testes.Servico
{
    [TestFixture]
    internal class ServicoUsuarioTeste : ServicoTesteBase
    {
        private readonly ServicoUsuario _servicoUsuario;
        const string LOGIN = "usuario";

        public ServicoUsuarioTeste()
        {
            this._servicoUsuario = new ServicoUsuario(this._unitOfWork);
        }
        
        [OneTimeTearDown]
        public void TestTearDown()
        {
            this._servicoUsuario.ExcluirPeloLoginBanco(LOGIN);
            _unitOfWork.Commit();
            _servicoUsuario.Dispose();
            _unitOfWork.Dispose();
        }

        [Test]
        public void DeveriaIncluirUsuario()
        {
            string senha = "Usuario1";
            string nome = "Usuario Teste";
            Usuario usuario = new UsuarioConstrutor()
                                  .ComLogin(LOGIN)
                                  .ComSenha(senha)
                                  .ComNome(nome)
                                  .Construir();
            
            var usuarioRetorno = _servicoUsuario.Incluir(usuario);
            Assert.IsTrue(usuario.Login == usuarioRetorno.Login);
            Assert.IsTrue(usuario.Nome == usuarioRetorno.Nome);
            Assert.IsTrue(usuario.PerfilId == usuarioRetorno.PerfilId);
        }

        [Test]
        public void DeveriaValidarLoginUsuario()
        {
            string login = "usuario";
            string senha = "Usuario1";

            using (var servicoUsuario = new ServicoUsuario(this._unitOfWork))
            {
                var usuarioRetorno = servicoUsuario.Autenticar(login, senha);

                Assert.IsNotNull(usuarioRetorno);
            }
        }

        [Test]
        public void NaoDeveriaValidarLoginUsuarioSenhaInvalida()
        {
            string login = "usuario";
            string senha = "senha312321";

            try
            {
                using (var servicoUsuario = new ServicoUsuario(this._unitOfWork))
                {
                    var usuarioRetorno = servicoUsuario.Autenticar(login, senha);

                    Assert.Fail();
                }
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Senha inválida"));
            }
        }

        [Test]
        public void NaoDeveriaValidarLoginUsuarioUsuarioInvalido()
        {
            string login = "usuariof332";
            string senha = "senha";

            try
            {
                using (var servicoUsuario = new ServicoUsuario(this._unitOfWork))
                {
                    var usuarioRetorno = servicoUsuario.Autenticar(login, senha);

                    Assert.Fail();
                }
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Usuário inválido"));
            }
        }

        [Test]
        public void DeveriaSelecionarUsuarioPorLogin()
        {
            string login = "usuario";

            using (var servicoUsuario = new ServicoUsuario(this._unitOfWork))
            {
                var usuario = servicoUsuario.SelecionarPorLogin(login);
                Assert.AreEqual(usuario.Login, login);
            }
        }

        [Test]
        public void NaoDeveriaSelecionarUsuarioLoginNull()
        {
            string login = null;

            try
            {
                using (var servicoUsuario = new ServicoUsuario(this._unitOfWork))
                {
                    var usuario = servicoUsuario.SelecionarPorLogin(login);
                    Assert.Fail();
                }
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Usuário inválido"));
            }
        }

        [Test]
        public void NaoDeveriaSelecionarUsuarioLoginEmpty()
        {
            string login = string.Empty;

            try
            {
                using (var servicoUsuario = new ServicoUsuario(this._unitOfWork))
                {
                    var usuario = servicoUsuario.SelecionarPorLogin(login);
                    Assert.Fail();
                }
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Usuário inválido"));
            }
        }

        [Test]
        public void DeveriaAlterarUsuario()
        {
            int id = 2;
            string login = "usuario";
            string nome = "UsuarioTes";
            int perfilId = 2;
            Usuario usuario = new UsuarioConstrutor()
                                 .ComId(id)
                                 .ComLogin(login)
                                 .ComNome(nome)
                                 .ComPerfilId(perfilId)
                                 .Construir();

            using (var servicoUsuario = new ServicoUsuario(this._unitOfWork))
            {
                var usuarioRetorno = servicoUsuario.Alterar(usuario);

                Assert.IsTrue(usuario.Id == usuarioRetorno.Id);
                Assert.IsTrue(usuario.Nome == usuarioRetorno.Nome);
                Assert.IsTrue(usuario.PerfilId == usuarioRetorno.PerfilId);
                Assert.IsTrue(usuario.Ativo == usuarioRetorno.Ativo);
            }
        }

        [Test]
        public void NaoDeveriaAlterarUsuarioNaoCadastrado()
        {
            int id = -1;
            string login = "usuario";
            string nome = "UsuarioTes";
            int perfilId = 2;
            Usuario usuario = new UsuarioConstrutor()
                                 .ComId(id)
                                 .ComLogin(login)
                                 .ComNome(nome)
                                 .ComPerfilId(perfilId)
                                 .Construir();
            try
            {
                using (var servicoUsuario = new ServicoUsuario(this._unitOfWork))
                {
                    var usuarioRetorno = servicoUsuario.Alterar(usuario);
                    Assert.Fail();
                }
            }
            catch (Exception ex)
            {
                Assert.True(!string.IsNullOrEmpty(ex.Message));
            }
        }

        [Test]
        public void TesteCriptDescript()
        {
            string senha = "Usuario1";
            string senhaCriptografada = Seguranca.EncryptString(senha);
            string senhaDescriptografada = Seguranca.DecryptString(senhaCriptografada);
            Assert.AreEqual(senha, senhaDescriptografada);
        }
    }
}
