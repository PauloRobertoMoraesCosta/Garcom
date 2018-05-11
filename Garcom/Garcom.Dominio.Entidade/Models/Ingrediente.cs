using System;
using System.Collections.Generic;

namespace Garcom.Dominio.Entidade.Models
{
    /// <summary>
    /// Classe responsável pelo Ingrediente
    /// </summary>
    public class Ingrediente : ClasseBase
    {
        /// <summary>
        /// Descrição do ingrediente
        /// </summary>
        private string _descricao;
        public string Descricao
        {
            get { return _descricao; }
            set { _descricao = value; }
        }

        /// <summary>
        /// Status do usuário
        /// </summary>
        private bool _ativo;
        public bool Ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }
        
        /// <summary>
        /// Coleção de ingredientes vinculado ao produto
        /// </summary>
        public ICollection<ProdutoIngrediente> ProdutoIngredientes { get; set; }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="descricao"></param>
        public Ingrediente(string descricao)
        {
            this._descricao = descricao;
            this.ProdutoIngredientes = new List<ProdutoIngrediente>();
            this.Validacao();
        }

        private Ingrediente() {  }

        public override bool Validacao()
        {
            if (string.IsNullOrEmpty(this.Descricao))
                throw new Exception("Descrição não pode ser nulo ou vazio");

            if (this.Descricao.Length > 100)
                throw new Exception("Descrição não pode conter mais de 100 caracteres");

            return true;
        }
    }
}
