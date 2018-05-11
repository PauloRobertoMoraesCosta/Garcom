using Garcom.Dominio.Entidade.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Garcom.Infra.DBEscrita.Mapeamento
{
    internal class ProdutoIngredienteTamanhoValorConfiguracao : EntityTypeConfiguration<ProdutoIngredienteTamanhoValor>
    {
        public ProdutoIngredienteTamanhoValorConfiguracao()
        {
            this.ToTable("PRODUTO_INGREDIENTE_TAMANHO_VALOR");

            this.HasKey(p => p.Id);

            this.Property(p => p.Id).HasColumnName("Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.ProdutoIngredienteId).HasColumnName("ProdutoIngredienteId").IsRequired().HasColumnType("int");
            this.Property(p => p.TamanhoProdutoId).HasColumnName("TamanhoProdutoId").IsRequired().HasColumnType("int");
            this.Property(p => p.Valor).HasColumnName("Valor").IsOptional().HasColumnType("double");
            this.Property(p => p.DataCadastro).HasColumnName("DataCadastro").IsRequired();
            this.Property(p => p.Excluido).HasColumnName("Excluido").IsRequired().HasColumnType("bit");

            this.HasRequired(p => p.ProdutoIngrediente).WithMany(p => p.ProdutosIngredientesTamanhosValor).HasForeignKey(p => p.ProdutoIngredienteId);
            this.HasRequired(p => p.TamanhoProduto).WithMany(p => p.ProdutoIngredientesTamanhoValor).HasForeignKey(p => p.TamanhoProdutoId);
        }
    }
}
