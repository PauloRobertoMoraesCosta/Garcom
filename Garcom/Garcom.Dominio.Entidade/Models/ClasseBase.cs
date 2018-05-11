using System;

namespace Garcom.Dominio.Entidade.Models
{
    /// <summary>
    /// Classe base 
    /// </summary>
    public abstract class ClasseBase
    {
        /// <summary>
        /// Campo Id - Chave primaria
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Campo DataCadastro 
        /// </summary>
        public DateTime DataCadastro { get; set; }

        /// <summary>
        /// Método responsável pela validação dos atributos da classe
        /// </summary>
        /// <returns></returns>
        public abstract bool Validacao();

        /// <summary>
        /// Campo excluído
        /// </summary>
        public bool Excluido { get; set; }

        public ClasseBase()
        {
            this.DataCadastro = DateTime.Now;
        }
    }
}
