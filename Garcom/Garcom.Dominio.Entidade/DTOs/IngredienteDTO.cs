using System;

namespace Garcom.Dominio.Entidade.DTOs
{
    public class IngredienteDTO
    {
        public string UsuarioLogado { get; set; }
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
    
    }
}
