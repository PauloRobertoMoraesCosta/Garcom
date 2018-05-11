using Garcom.Dominio.Entidade.Models;

namespace Garcom.Teste.Testes.Construtor
{
    public class UsuarioConstrutor
    {
        private int id;
        private string login;
        private string senha;
        private string nome;
        private int perfilId;
        private bool ativo;

        public UsuarioConstrutor()
        {
            this.id = default(int);
            this.login = string.Empty;
            this.senha = string.Empty;
            this.nome = string.Empty;
            this.perfilId = 1;
            this.ativo = true;
        }

        public UsuarioConstrutor ComId(int id)
        {
            this.id = id;
            return this;
        }

        public UsuarioConstrutor ComLogin(string login)
        {
            this.login = login;
            return this;
        }

        public UsuarioConstrutor ComSenha(string senha)
        {
            this.senha = senha;
            return this;
        }

        public UsuarioConstrutor ComNome(string nome)
        {
            this.nome = nome;
            return this;
        }

        public UsuarioConstrutor ComPerfilId(int perfilId)
        {
            this.perfilId = perfilId;
            return this;
        }

        public UsuarioConstrutor ComAtivo(bool ativo)
        {
            this.ativo = ativo;
            return this;
        }

        public Usuario Construir()
        {
            var usuario = new Usuario(this.login, this.senha, this.nome, this.perfilId, this.ativo);
            usuario.Id = this.id;
            return usuario;
        }
    }
}
