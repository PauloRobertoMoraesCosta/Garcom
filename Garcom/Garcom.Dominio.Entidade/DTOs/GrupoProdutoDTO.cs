using System;
using System.Collections.Generic;

namespace Garcom.Dominio.Entidade.DTOs
{
    public class GrupoProdutoDTO
    {
        public string UsuarioLogado { get; set; }
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool PermiteDividir { get; set; }
        public DateTime? DataCadastro { get; set; }
        public bool Ativo { get; set; }

        public ICollection<TamanhoProdutoDTO> Tamanhos { get; set; }
    }
}
