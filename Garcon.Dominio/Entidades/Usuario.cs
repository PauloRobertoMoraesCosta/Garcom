using System;

namespace Garcom.Dominio.Entidades
{
    /// <summary>
    /// Classe com as regras para entidade Usuario
    /// </summary>
    public class Usuario
    {

        #region "Atributos"
        private int id { get; set; }
        private string login { get; set; }
        private string senha { get; set; }
        private string nome { get; set; }
        private DateTime dataCadastro { get; set; }
        private bool super { get; set; }
        private bool ativo { get; set; }
        
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Login
        {
            get { return login; }
            set { login = value; }
        }
        public string Senha
        {
            get { return senha; }
            set { senha = value; }
        }
        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        public DateTime DataCadastro
        {
            get { return dataCadastro; }
            set { dataCadastro = value; }
        }
        public string Super 
        {
            get { return super.ToString(); }
            set { super = Convert.ToBoolean(value); }
        }
        public string Ativo 
        {
            get { return ativo.ToString(); }
            set { ativo = Convert.ToBoolean(value); } 
        }
        
        #endregion

    }
}
