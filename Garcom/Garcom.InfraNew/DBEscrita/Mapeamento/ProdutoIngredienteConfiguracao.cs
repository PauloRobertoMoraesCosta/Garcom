using Garcom.Dominio.Entidade.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Garcom.Infra.DBEscrita.Mapeamento
{
    internal class ProdutoIngredienteConfiguracao : EntityTypeConfiguration<ProdutoIngrediente>
    {
        public ProdutoIngredienteConfiguracao()
        {
            this.ToTable("PRODUTO_INGREDIENTE");

            this.HasKey(p => p.Id);

            this.Property(p => p.Id).HasColumnName("Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.ProdutoId).HasColumnName("ProdutoId").IsRequired().HasColumnType("int");
            this.Property(p => p.IngredienteId).HasColumnName("IngredienteId").IsRequired().HasColumnType("int");
            this.Property(p => p.Opcional).HasColumnName("Opcional").IsRequired().HasColumnType("bit");
            this.Property(p => p.Adicional).HasColumnName("Adicional").IsRequired().HasColumnType("bit");
            this.Property(p => p.ValorAdicional).HasColumnName("ValorAdicional").IsOptional().HasColumnType("double");
            this.Property(p => p.DataCadastro).HasColumnName("DataCadastro").IsRequired();
            this.Property(p => p.Excluido).HasColumnName("Excluido").IsRequired().HasColumnType("bit");

            this.HasRequired(p => p.Produto).WithMany(p => p.ProdutoIngredientes).HasForeignKey(p => p.ProdutoId);
            this.HasRequired(p => p.Ingrediente).WithMany(p => p.ProdutoIngredientes).HasForeignKey(p => p.IngredienteId);
        }
    }
}
