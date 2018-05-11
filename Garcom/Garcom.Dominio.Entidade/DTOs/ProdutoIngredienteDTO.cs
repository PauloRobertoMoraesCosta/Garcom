using System.Collections.Generic;

namespace Garcom.Dominio.Entidade.DTOs
{
    public class ProdutoIngredienteDTO
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int IngredienteId { get; set; }
        public bool Opcional { get; set; }
        public bool Adicional { get; set; }
        public double? ValorAdicional { get; set; }
        public ICollection<ProdutoIngredienteTamanhoValorDTO> ProdutosIngredientesTamanhosValor { get; set; }
    }
}
