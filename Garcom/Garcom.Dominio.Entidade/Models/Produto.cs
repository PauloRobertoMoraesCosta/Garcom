using System;
using System.Collections.Generic;

namespace Garcom.Dominio.Entidade.Models
{
    /// <summary>
    /// Classe responsável pelo Produto
    /// </summary>
    public class Produto : ClasseBase
    {
        /// <summary>
        /// Descrição do produto
        /// </summary>
        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        /// <summary>
        /// Valor do produto
        /// </summary>
        private double? _valor;
        public double? Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }

        /// <summary>
        /// Id do Grupo de Produto
        /// </summary>
        private int? _grupoProdutoId;
        public int? GrupoProdutoId
        {
            get { return _grupoProdutoId; }
            set { _grupoProdutoId = value; }
        }

        /// <summary>
        /// Situação do produto
        /// </summary>
        private bool _ativo;
        public bool Ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }

        /// <summary>
        /// Nome da imagem vinculado ao produto
        /// </summary>
        private string _nomeImagem;
        public string NomeImagem
        {
            get { return _nomeImagem; }
            set { _nomeImagem = value; }
        }

        /// <summary>
        /// Código rápido para acessar o produto
        /// </summary>
        private string _codigoRapido;
        public string CodigoRapido
        {
            get { return _codigoRapido; }
            set { _codigoRapido = value; }
        }
        
        /// <summary>
        /// Grupo de Produto vinculado ao produto
        /// </summary>
        public virtual GrupoProduto GrupoProduto { get; set; }

        /// <summary>
        /// Coleção de ingredientes vinculado ao produto
        /// </summary>
        public ICollection<ProdutoIngrediente> ProdutoIngredientes { get; set; }

        /// <summary>
        /// Coleção de tamanhos de produtos valores
        /// </summary>
        public ICollection<ProdutoTamanhoValor> ProdutosTamanhosValor { get; set; }

        /// <summary>
        /// Construtor com parâmetros
        /// </summary>
        /// <param name="descricao">Descrição do produto</param>
        public Produto(string descricao, int grupoProduto)
        {
            this._nome = descricao;
            this._grupoProdutoId = grupoProduto;
            this.ProdutoIngredientes = new List<ProdutoIngrediente>();
            this.ProdutosTamanhosValor = new List<ProdutoTamanhoValor>();
            Validacao();
        }

        /// <summary>
        /// Construtor sem parâmetros
        /// </summary>
        private Produto()
        {
            this.ProdutoIngredientes = new List<ProdutoIngrediente>();
            this.ProdutosTamanhosValor = new List<ProdutoTamanhoValor>();
        }

        /// <summary>
        /// Método de validação
        /// </summary>
        /// <returns></returns>
        public override bool Validacao()
        {
            if (string.IsNullOrEmpty(this._nome))
                throw new Exception("Descrição não pode ser nulo ou vazio.");

            if (this._nome.Length > 100)
                throw new Exception("Descrição não pode conter mais de 100 caracteres.");
            
            if (this._grupoProdutoId < 1)
                throw new Exception("Grupo de produto não foi informado.");

            return true;
        }

        public bool ValidarValor()
        {
            if ((this.Valor == null || this.Valor == 0) && (this.ProdutosTamanhosValor == null || this.ProdutosTamanhosValor.Count == 0))
                throw new Exception("Valor do produto não foi informado.");

            if (this.Valor != null && this.Valor < 0)
                throw new Exception("Valor do produto não pode ser menor que 0.");

            return true;
        }
    }
}
