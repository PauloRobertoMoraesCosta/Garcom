namespace Garcom.Dominio.Entidade.DTOs
{
    public class ProdutoIngredienteTamanhoValorDTO
    {
        public int Id { get; set; }
        public int ProdutoIngredienteId { get; set; }
        public int TamanhoProdutoId { get; set; }
        public double? Valor { get; set; }
    }
}
