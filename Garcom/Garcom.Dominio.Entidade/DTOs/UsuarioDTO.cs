using System;

namespace Garcom.Dominio.Entidade.DTOs
{
    public class UsuarioDTO
    {
        public string UsuarioLogado { get; set; }
        public int Id { get; set; }
        public string Login { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }
        public string ConfirmacaoSenha { get; set; }
        public int PerfilId { get; set; }
        public bool Ativo { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
