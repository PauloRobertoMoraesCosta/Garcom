using System;
using System.Collections.Generic;

namespace Garcom.Dominio.Entidade.Models
{
    /// <summary>
    /// Classe Grupo de Produto
    /// </summary>
    public class GrupoProduto : ClasseBase
    {
        /// <summary>
        /// Nome do grupo de produto
        /// </summary>
        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        /// <summary>
        /// Permite divisão de sabores
        /// </summary>
        private bool _permiteDividir;
        public bool PermiteDividir
        {
            get { return _permiteDividir; }
            set { _permiteDividir = value; }
        }

        private bool _ativo;
        public bool Ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }


        /// <summary>
        /// Lista de produtos
        /// </summary>
        public ICollection<Produto> Produtos { get; set; }
        /// <summary>
        /// Coleção de produtos ingrediente tamanhos produtos
        /// </summary>
        public ICollection<GrupoProdutoTamanhoProduto> GruposProdutoTamanhosProduto { get; set; }

        /// <summary>
        /// Construtor
        /// </summary>
        public GrupoProduto(string nome)
        {
            this._nome = nome;
            this.Produtos = new List<Produto>();
            Validacao();
        }

        private GrupoProduto()
        {}

        /// <summary>
        /// Validação do Grupo de Produto
        /// </summary>
        /// <returns></returns>
        public override bool Validacao()
        {
            if (string.IsNullOrEmpty(this._nome))
                throw new Exception("Nome não foi informado.");

            if (this._nome.Length > 100)
                throw new Exception("Nome não pode conter mais de 100 caracteres.");

            return true;
        }
    }
}
