using System;
using System.Collections.Generic;

namespace Garcom.Dominio.Entidade.DTOs
{
    public class ProdutoDTO
    {
        public string UsuarioLogado { get; set; }
        public int Id { get; set; }
        public string Nome { get; set; }
        public int? GrupoProdutoId { get; set; }
        public double? Valor { get; set; }
        public ICollection<ProdutoTamanhoValorDTO> ProdutosTamanhosValor { get; set; }
        public ICollection<ProdutoIngredienteDTO> ProdutoIngredientes { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Imagem { get; set; }
        public string NomeImagem { get; set; }
        public string CodigoRapido { get; set; }
    }
}
