using Garcom.Dominio.Entidade.Models;
using NUnit.Framework;
using System;

namespace Garcom.Teste.Testes
{
    [TestFixture]
    public class UsuarioTeste
    {
        [TestCase]
        public void DeveriaCriarUsuario()
        {
            string login = "usuario";
            string senha = "Usuario1";
            string nome = "Usuario";
            int perfilId = 1;
            Usuario usuario = new Usuario(login, senha, nome, perfilId);

            Assert.IsTrue(usuario.Login == login);
            Assert.IsTrue(usuario.Senha == senha);
            Assert.IsTrue(usuario.Nome == nome);
            Assert.IsTrue(usuario.PerfilId == perfilId);
        }

        [TestCase]
        public void NaoDeveriaCriarUsuarioLoginNulo()
        {
            string login = null;
            string senha = "samuel123";
            string nome = "Samuel";
            int perfilId = 1;

            try
            {
                Usuario usuario = new Usuario(login, senha, nome, perfilId);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Login não pode ser nulo"));
            }
        }

        [TestCase]
        public void NaoDeveriaCriarUsuarioLoginVazio()
        {
            string login = string.Empty;
            string senha = "samuel123";
            string nome = "Samuel";
            int perfilId = 1;

            try
            {
                Usuario usuario = new Usuario(login, senha, nome, perfilId);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Login não pode ser nulo"));
            }
        }

        [TestCase]
        public void NaoDeveriaCriarUsuarioLoginMais45Carac()
        {
            try
            {
                string login = "fhsajfkaslfhsajfkljsaksajjasfhkasjfasfhkajaskk";
                string senha = "samuel123";
                string nome = "Samuel";
                int perfilId = 1;

                Usuario usuario = new Usuario(login, senha, nome, perfilId);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Login não pode conter mais que 20 caracteres"));
            }
        }

        [TestCase]
        public void NaoDeveriaCriarUsuarioSenhaNulo()
        {
            string login = "samuel";
            string senha = null;
            string nome = "Samuel";
            int perfilId = 1;
            try
            {
                Usuario usuario = new Usuario(login, senha, nome, perfilId);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Senha não pode ser nulo"));
            }
        }

        [TestCase]
        public void NaoDeveriaCriarUsuarioSenhaVazio()
        {
            string login = "samuel";
            string senha = string.Empty;
            string nome = "Samuel";
            int perfilId = 1;
            try
            {
                Usuario usuario = new Usuario(login, senha, nome, perfilId);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Senha não pode ser nulo"));
            }
        }

        [TestCase]
        public void NaoDeveriaCriarUsuarioSenhaMais10Carac()
        {
            try
            {
                string login = "fhsajfkaslfhsajfkljs";
                string senha = "Senha123332";
                string nome = "Samuel";
                int perfilId = 1;

                Usuario usuario = new Usuario(login, senha, nome, perfilId);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Senha não pode conter mais que 10 caracteres"));
            }
        }

        [TestCase]
        public void NaoDeveriaCriarUsuarioSenhaMenor6Carac()
        {
            try
            {
                string login = "fhsajfkaslfhsajfkljs";
                string senha = "Se332";
                string nome = "Samuel";
                int perfilId = 1;

                Usuario usuario = new Usuario(login, senha, nome, perfilId);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Senha não pode conter menos que 6 caracteres"));
            }
        }

        [TestCase]
        public void NaoDeveriaCriarUsuarioSenhaSemLetraMaiuscula()
        {
            try
            {
                string login = "fhsajfkaslfhsajfkljs";
                string senha = "senha123";
                string nome = "Samuel";
                int perfilId = 1;

                Usuario usuario = new Usuario(login, senha, nome, perfilId);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Senha deve ser composta por pelo menos 1 letra maiúscula e 1 número"));
            }
        }

        [TestCase]
        public void NaoDeveriaCriarUsuarioSenhaSemNumero()
        {
            try
            {
                string login = "fhsajfkaslfhsajfkljs";
                string senha = "Senhaaa";
                string nome = "Samuel";
                int perfilId = 1;

                Usuario usuario = new Usuario(login, senha, nome, perfilId);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Senha deve ser composta por pelo menos 1 letra maiúscula e 1 número"));
            }
        }

        [TestCase]
        public void NaoDeveriaCriarUsuarioNomeNulo()
        {
            string login = "samuel";
            string senha = "Senha123";
            string nome = null;
            int perfilId = 1;

            try
            {
                Usuario usuario = new Usuario(login, senha, nome, perfilId);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Nome não pode ser nulo"));
            }
        }

        [TestCase]
        public void NaoDeveriaCriarUsuarioNomeVazio()
        {
            string login = "samuel";
            string senha = "Senha123";
            string nome = string.Empty;
            int perfilId = 1;

            try
            {
                Usuario usuario = new Usuario(login, senha, nome, perfilId);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Nome não pode ser nulo"));
            }
        }

        [TestCase]
        public void NaoDeveriaCriarUsuarioNomeMais100Carac()
        {
            try
            {
                string login = "fhsajfkaslfhsajfkljs";
                string senha = "Senha123";
                string nome = "fhsajfkaslfhsajfkljsaksajjasfhkasjfasfhkajededqeaskkjfdskjfksjfsklafjlkfjakljfsfjdakjjkajfjaksjfsklf1";
                int perfilId = 1;

                Usuario usuario = new Usuario(login, senha, nome, perfilId);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Nome não pode conter mais que 100 caracteres"));
            }
        }
    }
}
