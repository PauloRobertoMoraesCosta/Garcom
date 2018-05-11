using System;

namespace Garcom.Dominio.Entidade.Models
{
    /// <summary>
    /// Classe responsável pelo Usuario do sistema
    /// </summary>
    public class Usuario : ClasseBase
    {
        /// <summary>
        /// Login do usuário
        /// </summary>
        private string _login;
        public string Login
        {
            get {
                return _login;
            }
            set{
                _login = value;
            }
        }

        /// <summary>
        /// Senha do usuário
        /// </summary>
        private string _senha;
        public string Senha
        {
            get{
                return _senha;
            }
            set{
                _senha = value;
            }
        }

        /// <summary>
        /// Nome do usuário
        /// </summary>
        private string _nome;
        public string Nome
        {
            get
            {
                return _nome;
            }
            set
            {
                _nome = value;
            }
        }

        /// <summary>
        /// Id do perfil vinculado ao usuario
        /// </summary>
        private int _perfilId;
        public int PerfilId
        {
            get
            {
                return _perfilId; 
            }
            set
            {
                _perfilId = value;
            }
        }

        /// <summary>
        /// Status do usuário
        /// </summary>
        private bool _ativo;
        public bool Ativo
        {
            get { return _ativo; }
            set { _ativo = value; }
        }
        
        /// <summary>
        /// Perfil vinculado ao usuario
        /// </summary>
        public Perfil Perfil { get; set; }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="login"></param>
        /// <param name="senha"></param>
        /// <param name="nome"></param>
        /// <param name="perfilId"></param>
        public Usuario(string login, string senha, string nome, int perfilId, bool ativo = false)
        {
            this._login = login;
            this._senha = senha;
            this._nome = nome;
            this._perfilId = perfilId;
            this._ativo = ativo;

            Validacao();
        }

        private Usuario()
        {}

        public override bool Validacao()
        {
            if (string.IsNullOrEmpty(this.Login))
                throw new Exception("Login não pode ser nulo");

            if (this.Login.Length > 20)
                throw new Exception("Login não pode conter mais que 20 caracteres");

            if (!string.IsNullOrEmpty(this.Senha))
            {
                if (this.Senha.Length < 6)
                    throw new Exception("Senha não pode conter menos que 6 caracteres");

                if (this.Senha.Length > 10)
                    throw new Exception("Senha não pode conter mais que 10 caracteres");

                if (!ContemLetraMaiusculaENumero(this.Senha))
                    throw new Exception("Senha deve ser composta por pelo menos 1 letra maiúscula e 1 número");
            }

            if (string.IsNullOrEmpty(this.Nome))
                throw new Exception("Nome não pode ser nulo");

            if (this.Nome.Length > 100)
                throw new Exception("Nome não pode conter mais que 100 caracteres");

            if (this.PerfilId == 0)
                throw new Exception("Perfil não vinculado");

            return true;
        }

        private bool ContemLetraMaiusculaENumero(string value)
        {
            bool contemLetraMaiuscula = false;
            bool contemNumero = false;
            foreach (var item in value)
            {
                if (item >= 65 && item <= 90)
                    contemLetraMaiuscula = true;
                if (item >= 48 && item <= 57)
                    contemNumero = true;
            }

            return contemNumero && contemLetraMaiuscula;
        }
    }
}
