using System;

namespace Garcom.Dominio.Entidade.DTOs
{
    public class TamanhoProdutoDTO
    {
        public string UsuarioLogado { get; set; }
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool PossuiProduto { get; set; }
        public int? Ordem { get; set; }
        public bool? Utilizado { get; set; }
    }
}
