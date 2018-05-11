using Garcom.Dominio.Entidade.Models;
using NUnit.Framework;
using System;

namespace Garcom.Teste.Testes.Classes
{
    [TestFixture]
    public class GrupoProdutoTeste
    {
        [TestCase]
        public void DeveriaCriarGrupoProduto()
        {
            string nome = "grupo produto teste";
            var grupoProduto = new GrupoProduto(nome);
            
            Assert.IsNotNull(grupoProduto);
            Assert.AreEqual(grupoProduto.Nome, nome);
        }

        [TestCase]
        public void NaoDeveriaCriarGrupoProdutoNomeNull()
        {
            try
            {
                var grupoProduto = new GrupoProduto(null);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Nome não foi informado.");
            }
        }

        [TestCase]
        public void NaoDeveriaCriarGrupoProdutoNomeVazio()
        {
            try
            {
                var grupoProduto = new GrupoProduto(string.Empty);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Nome não foi informado.");
            }
        }

        [TestCase]
        public void NaoDeveriaCriarGrupoProdutoNomeMais100Caracteres()
        {
            string nome = "qwerioufjsakjfadjfifjsaiuewfjwiofjewfaifjeoiwjfoaifuojfoqiurqjfwioejcioruewjfweoijfwqeiofjeqjfejfwojwfji";
            try
            {

                var grupoProduto = new GrupoProduto(nome);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Nome não pode conter mais de 100 caracteres.");
            }
        }
    }
}
