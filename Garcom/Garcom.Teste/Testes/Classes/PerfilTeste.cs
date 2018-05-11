using Garcom.Dominio.Entidade.Models;
using NUnit.Framework;
using System;

namespace Garcom.Teste.Testes
{
    [TestFixture]
    public class PerfilTeste
    {
        [TestCase]
        public void DeveriaCriarPerfil()
        {
            string descricao = "Perfil Teste";

            Perfil perfil = new Perfil(descricao);

            Assert.IsNotNull(perfil);
            Assert.IsTrue(perfil.Descricao == descricao);
        }

        [TestCase]
        public void NaoDeveriaCriarPerfilDescMais45Caract()
        {
            string descricao = "fhsajfkaslfhsajfkljsaksajjasfhkasjfasfhkajaskk";
            try
            {
                Perfil perfil = new Perfil(descricao);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Descrição não pode conter mais que 45 caracteres."));
            }
        }

        [TestCase]
        public void NaoDeveriaCriarPerfilDescNull()
        {
            string descricao = null;
            try
            {
                Perfil perfil = new Perfil(descricao);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Descrição não pode ser nulo."));
            }
        }

        [TestCase]
        public void NaoDeveriaCriarPerfilDescVazia()
        {
            string descricao = string.Empty;
            try
            {
                Perfil perfil = new Perfil(descricao);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Contains("Descrição não pode ser nulo."));
            }
        }
    }
}
