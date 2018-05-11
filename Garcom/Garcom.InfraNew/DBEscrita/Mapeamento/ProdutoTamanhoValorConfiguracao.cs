using Garcom.Dominio.Entidade.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Garcom.Infra.DBEscrita.Mapeamento
{
    internal class ProdutoTamanhoValorConfiguracao : EntityTypeConfiguration<ProdutoTamanhoValor>
    {
        public ProdutoTamanhoValorConfiguracao()
        {
            this.ToTable("PRODUTO_TAMANHO_VALOR");

            this.HasKey(p => p.Id);

            this.Property(p => p.Id).HasColumnName("Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.ProdutoId).HasColumnName("ProdutoId").IsRequired().HasColumnType("int");
            this.Property(p => p.TamanhoProdutoId).HasColumnName("TamanhoProdutoId").IsRequired().HasColumnType("int");
            this.Property(p => p.Valor).HasColumnName("Valor").IsRequired().HasColumnType("double");
            this.Property(p => p.DataCadastro).HasColumnName("DataCadastro").IsRequired();
            this.Property(p => p.Excluido).HasColumnName("Excluido").IsRequired().HasColumnType("bit");

            this.HasRequired(p => p.Produto).WithMany(p => p.ProdutosTamanhosValor).HasForeignKey(p => p.ProdutoId);
            this.HasRequired(p => p.TamanhoProduto).WithMany(p => p.ProdutosTamanhosValor).HasForeignKey(p => p.TamanhoProdutoId);
        }
    }
}
