using System.Collections;

namespace Garcom.Dominio.Entidade.Models
{
    public class ProdutoIngredienteTamanhoValor : ClasseBase
    {        
        /// <summary>
        /// Id do produto ingrediente
        /// </summary>
        private int _produtoIngredienteId;
        public int ProdutoIngredienteId
        {
            get { return _produtoIngredienteId; }
            set { _produtoIngredienteId = value; }
        }

        /// <summary>
        /// Id do tamanho de produto
        /// </summary>
        private int _tamanhoProdutoId;
        public int TamanhoProdutoId
        {
            get { return _tamanhoProdutoId; }
            set { _tamanhoProdutoId = value; }
        }

        /// <summary>
        /// Valor dos ingredientes adicionais
        /// </summary>
        private double? _valor;
        public double? Valor
        {
            get { return _valor; }
            set { _valor = value; }
        }

        public virtual ProdutoIngrediente ProdutoIngrediente { get; set; }
        public virtual TamanhoProduto TamanhoProduto { get; set; }

        /// <summary>
        /// Método responsável por realizar a validação dos atributos
        /// </summary>
        /// <returns></returns>
        public override bool Validacao()
        {
            return true;
        }

        public override bool Equals(object obj)
        {
            ProdutoIngredienteTamanhoValor t = obj as ProdutoIngredienteTamanhoValor;
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
