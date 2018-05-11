using Garcom.Dominio.Entidade.Models;
using NUnit.Framework;
using System;

namespace Garcom.Teste.Testes
{
    [TestFixture]
    internal class ProdutoTeste
    {
        [TestCase]
        public void DeveriaCriarProduto()
        {
            string descricao = "Produto Teste";
            Produto produto = new Produto(descricao, 1);

            Assert.IsNotNull(produto);
            Assert.AreEqual(produto.Nome, descricao);
        }

        [TestCase]
        public void NaoDeveriaCriarProdutoDescricaoNulo()
        {
            string descricao = null;

            try
            {
                Produto produto = new Produto(descricao, 1);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Descrição não pode ser nulo ou vazio.");
            }
        }

        [TestCase]
        public void NaoDeveriaCriarProdutoDescricaoVazio()
        {
            string descricao = string.Empty;
            try
            {
                Produto produto = new Produto(descricao, 1);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Descrição não pode ser nulo ou vazio.");
            }
        }

        [TestCase]
        public void NaoDeveriaCriarProdutoDescricaoMais100Caract()
        {
            string descricao = "fhsajfkaslfhsajfkljsaksajjasfhkasjfasfhkajededqeaskkjfdskjfksjfsklafjlkfjakljfsfjdakjjkajfjaksjfsklff";
            try
            {
                Produto produto = new Produto(descricao, 1);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Descrição não pode conter mais de 100 caracteres.");
            }
        }

        [TestCase]
        public void DeveriaValidarProdutoValor()
        {
            double valor = 10.50;
            string descricao = "Produto Teste";
            Produto produto = new Produto(descricao, 1);
            produto.Valor = valor;
            Assert.IsTrue(produto.ValidarValor());
        }

        [TestCase]
        public void DeveriaValidarProdutoTamanhoProdutoValores()
        {
            double valor = 10.50;
            string descricao = "Produto Teste";
            Produto produto = new Produto(descricao, 1);
            produto.Valor = null;
            produto.ProdutosTamanhosValor.Add(new ProdutoTamanhoValor(valor));
            Assert.IsTrue(produto.ValidarValor());
        }

        [TestCase]
        public void NaoDeveriaValidarProdutoValorMenor0()
        {
            string descricao = "Produto Teste";
            Produto produto = new Produto(descricao, 1);
            produto.Valor = -4.35;
            try
            {
                produto.ValidarValor();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Valor do produto não pode ser menor que 0.");
            }
        }

        [TestCase]
        public void NaoDeveriaValidarProdutoValorNullEListaTamanhoProdutoValor0()
        {
            string descricao = "Produto Teste";
            Produto produto = new Produto(descricao, 1);
            produto.Valor = null;
            try
            {
                produto.ValidarValor();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.Message, "Valor do produto não foi informado.");
            }
        }
    }
}
