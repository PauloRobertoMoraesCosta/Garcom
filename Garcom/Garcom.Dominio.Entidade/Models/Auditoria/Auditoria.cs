using System;

namespace Garcom.Dominio.Entidade.Models.Auditoria
{
    /// <summary>
    /// Classe responsavel por armazenar a auditoria
    /// </summary>
    public class Auditoria : ClasseBase
    {
        /// <summary>
        /// Ação da operação realizada
        /// </summary>
        public Acao Acao { get; set; }
        /// <summary>
        /// Login do usuário que realizou a ação
        /// </summary>
        public string Usuario { get; set; }
        /// <summary>
        /// Tabela que ocorreu a ação
        /// </summary>
        public string Tabela { get; set; }
        /// <summary>
        /// A chave primária do item que sofreu a ação
        /// </summary>
        public int Chave { get; set; }
        /// <summary>
        /// Os valores antes da ação
        /// </summary>
        public string ValoresAntigos { get; set; }
        /// <summary>
        /// Os valores depois da ação
        /// </summary>
        public string NovosValores { get; set; }

        public override bool Validacao()
        {
            if (string.IsNullOrEmpty(this.Usuario))
                throw new Exception("Usuario não pode ser nulo ou vazio");

            if (string.IsNullOrEmpty(this.Tabela))
                throw new Exception("Tabela não pode ser nulo ou vazio");

            return true;
        }
    }

    /// <summary>
    /// Enumeração das ações salvas na auditoria
    /// </summary>
    public enum Acao : byte
    {
        INCLUSAO,
        ALTERACAO,
        EXCLUSAO
    }
}
