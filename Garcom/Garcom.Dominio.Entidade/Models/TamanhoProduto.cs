using System;
using System.Collections.Generic;

namespace Garcom.Dominio.Entidade.Models
{
    /// <summary>
    /// Classe Tamanho de Produto
    /// </summary>
    public class TamanhoProduto : ClasseBase
    {
        /// <summary>
        /// Nome do tamanho
        /// </summary>
        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        /// <summary>
        /// Status
        /// </summary>
        private bool _ativo;
        public bool Ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }

        /// <summary>
        /// Coleção de tamanho produto valores
        /// </summary>
        public ICollection<ProdutoTamanhoValor> ProdutosTamanhosValor { get; set; }
        /// <summary>
        /// Coleção de produtos ingrediente tamanhos produtos
        /// </summary>
        public ICollection<ProdutoIngredienteTamanhoValor> ProdutoIngredientesTamanhoValor { get; set; }
        /// <summary>
        /// Coleção de produtos ingrediente tamanhos produtos
        /// </summary>
        public ICollection<GrupoProdutoTamanhoProduto> GruposProdutoTamanhosProduto { get; set; }

        /// <summary>
        /// Construtor
        /// </summary>
        public TamanhoProduto()
        {
            this.ProdutosTamanhosValor = new List<ProdutoTamanhoValor>();
        }

        /// <summary>
        /// Validação do tamanho do produto
        /// </summary>
        /// <returns></returns>
        public override bool Validacao()
        {
            if (string.IsNullOrEmpty(this._nome))
                throw new Exception("Nome não foi informado");

            if (this._nome.Length > 100)
                throw new Exception("Nome não pode conter mais de 80 caracteres");

            return true;
        }
    }
}
