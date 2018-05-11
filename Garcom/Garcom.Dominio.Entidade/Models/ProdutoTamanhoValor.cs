using System;

namespace Garcom.Dominio.Entidade.Models
{
    public class ProdutoTamanhoValor : ClasseBase
    {
        /// <summary>
        /// Id do Produto
        /// </summary>
        private int _produtoId;
        public int ProdutoId
        {
            get { return _produtoId; }
            set { _produtoId = value; }
        }

        private int _tamanhoProdutoId;
        public int TamanhoProdutoId
        {
            get { return _tamanhoProdutoId; }
            set { _tamanhoProdutoId = value; }
        }

        /// <summary>
        /// Valor do produto para o tamanho vinculado
        /// </summary>
        private double _valor;
        public double Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }
        /// <summary>
        /// Produto
        /// </summary>
        public virtual Produto Produto { get; set; }

        public virtual TamanhoProduto TamanhoProduto { get; set; }

        private ProdutoTamanhoValor()
        {

        }

        public ProdutoTamanhoValor(double valor)
        {
            this._valor = valor;
        }

        public override bool Validacao()
        {
            if (_valor == 0 || _valor < 0)
                throw new Exception("Valor não informado");

            return true;
        }

        public override bool Equals(object obj)
        {
            ProdutoTamanhoValor t = obj as ProdutoTamanhoValor;
            if (t == null)
                return false;
            else
            {
                return Id.Equals(t.Id);

            }
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
