using System;
using System.Collections.Generic;

namespace Garcom.Dominio.Entidade.Models
{
    public class ProdutoIngrediente : ClasseBase
    {
        /// <summary>
        /// Id do produto
        /// </summary>
        private int _produtoId;
        public int ProdutoId
        {
            get { return _produtoId; }
            set { _produtoId = value; }
        }

        /// <summary>
        /// Id do ingrediente
        /// </summary>
        private int _ingredienteId;
        public int IngredienteId
        {
            get { return _ingredienteId; }
            set { _ingredienteId = value; }
        }

        /// <summary>
        /// Ingrendiente é opcional
        /// </summary>
        private bool _opcional;
        public bool Opcional
        {
            get { return _opcional; }
            set { _opcional = value; }
        }

        /// <summary>
        /// Ingrediente é adicional
        /// </summary>
        private bool _adicional;
        public bool Adicional
        {
            get { return _adicional; }
            set { _adicional = value; }
        }


        /// <summary>
        /// Valor será acrescentado para ingredientes adicionais
        /// </summary>
        private double? _valorAdicional;
        public double? ValorAdicional
        {
            get { return _valorAdicional; }
            set { _valorAdicional = value; }
        }


        /// <summary>
        /// Produto vinculado ao ingrediente
        /// </summary>
        public virtual Produto Produto { get; set; }
        /// <summary>
        /// Ingrediente vinculado ao produto
        /// </summary>
        public virtual Ingrediente Ingrediente { get; set; }

        /// <summary>
        /// Coleção de produtos ingrediente tamanhos produtos
        /// </summary>
        public ICollection<ProdutoIngredienteTamanhoValor> ProdutosIngredientesTamanhosValor { get; set; }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="produtoId">Id do produto</param>
        /// <param name="ingredienteId">Id do ingrediente</param>
        public ProdutoIngrediente(int produtoId, int ingredienteId)
        {
            this._produtoId = produtoId;
            this._ingredienteId = ingredienteId;
            this.ProdutosIngredientesTamanhosValor = new List<ProdutoIngredienteTamanhoValor>();
        }

        public ProdutoIngrediente()
        {
            this.ProdutosIngredientesTamanhosValor = new List<ProdutoIngredienteTamanhoValor>();
        }


        public override bool Validacao()
        {
            if (this._ingredienteId < 1)
                throw new Exception("IngredienteId não pode ser menor que 1");

            if (this._produtoId < 1)
                throw new Exception("ProdutoId não pode ser menor que 1");

            return true;
        }

        public override bool Equals(object obj)
        {
            ProdutoIngrediente t = obj as ProdutoIngrediente;
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
