using System;
using System.Collections.Generic;

namespace Garcom.Dominio.Entidade.Models
{
    public class GrupoProdutoTamanhoProduto : ClasseBase
    {
        /// <summary>
        /// Ordem do tamanho
        /// </summary>
        private int _ordem;
        public int Ordem
        {
            get { return _ordem; }
            set { _ordem = value; }
        }
        /// <summary>
        /// Id do grupo de produto
        /// </summary>
        private int _grupoProdutoId;
        public int GrupoProdutoId
        {
            get { return _grupoProdutoId; }
            set { _grupoProdutoId = value; }
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
        /// Grupo de produto
        /// </summary>
        public virtual GrupoProduto GrupoProduto { get; set; }
        /// <summary>
        /// Tamanho de produto
        /// </summary>
        public virtual TamanhoProduto TamanhoProduto { get; set; }

        private GrupoProdutoTamanhoProduto()
        {}
        
        public GrupoProdutoTamanhoProduto(int ordem, int grupoProdutoId, int tamanhoProdutoId)
        {
            this.Ordem = ordem;
            this.GrupoProdutoId = grupoProdutoId;
            this.TamanhoProdutoId = tamanhoProdutoId;
            Validacao();
        }

        public override bool Validacao()
        {
             if (this._ordem <= 0)
                throw new Exception("Ordem não foi informado");

            return true;
        }
    }
}
