using Garcom.Dominio.Entidade.Models;
using NUnit.Framework;
using System;

namespace Garcom.Teste.Testes
{
    [TestFixture]
    public class IngredienteTeste
    {
        [TestCase]
        public void DeveriaCriarIngrediente()
        {
            string descricao = "Ingrediente teste";
            Ingrediente ingrediente = new Ingrediente(descricao);
            Assert.IsNotNull(ingrediente);
            Assert.IsTrue(ingrediente.Descricao == descricao);
        }
        
        [TestCase]
        public void NaoDeveriaCriarIngredienteDescricaoNull()
        {
            string descricao = null;

            try
            {
                Ingrediente ingrediente = new Ingrediente(descricao);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Equals("Descrição não pode ser nulo ou vazio"));
            }
        }

        [TestCase]
        public void NaoDeveriaCriarIngredienteDescriçãoVazio()
        {
            string descricao = string.Empty;

            try
            {
                Ingrediente ingrediente = new Ingrediente(descricao);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Equals("Descrição não pode ser nulo ou vazio"));
            }
        }

        [TestCase]
        public void NaoDeveriaValidarIngredienteDescricaoMais100Caract()
        {
            string descricao = "fhsajfkaslfhsajfkljsaksajjasfhkasjfasfhkajededqeaskkjfdskjfksjfsklafjlkfjakljfsfjdakjjkajfjaksjfsklfd";

            try
            {
                Ingrediente ingrediente = new Ingrediente(descricao);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message.Equals("Descrição não pode conter mais de 100 caracteres"));
            }
        }
    }
}
