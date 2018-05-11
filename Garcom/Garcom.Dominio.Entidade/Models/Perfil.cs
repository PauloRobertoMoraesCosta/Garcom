using System;
using System.Collections.Generic;

namespace Garcom.Dominio.Entidade.Models
{
    /// <summary>
    /// Classe responsável pelo Perfil que é vinculado ao usuário
    /// </summary>
    public class Perfil : ClasseBase
    {
        /// <summary>
        /// Descrição do perfil
        /// </summary>
        private string _descricao;
        public string Descricao
        {
            get
            {
                return this._descricao;
            }
            set
            {
                this._descricao = value;
            }
        }
        
        /// <summary>
        /// Coleção de usuarios vinculado ao perfil
        /// </summary>
        public ICollection<Usuario> Usuarios { get; set; }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="descricao">Descrição do perfil</param>
        public Perfil(string descricao)
        {
            this._descricao = descricao;
            this.Usuarios = new List<Usuario>();
            Validacao();
        }

        private Perfil() { }

        public override bool Validacao()
        {
            if (string.IsNullOrEmpty(this._descricao))
                throw new ArgumentNullException("Descrição não pode ser nulo.");

            if (this._descricao.Length > 45)
                throw new Exception("Descrição não pode conter mais que 45 caracteres.");
            return true;
        }
    }
}
