using Garcom.Dominio.Entidade.Models;
using System.Data.Entity.ModelConfiguration;

namespace Garcom.Infra.DBEscrita.Mapeamento
{
    public class GrupoProdutoTamanhoProdutoConfiguracao : EntityTypeConfiguration<GrupoProdutoTamanhoProduto>
    {
        public GrupoProdutoTamanhoProdutoConfiguracao()
        {
            this.ToTable("GRUPO_PRODUTO_TAMANHO_PRODUTO");

            this.HasKey(p => p.Id);

            this.Property(p => p.Ordem).HasColumnName("Ordem").IsOptional().HasColumnType("int");
            this.Property(p => p.GrupoProdutoId).HasColumnName("GrupoProdutoId").IsRequired().HasColumnType("int");
            this.Property(p => p.TamanhoProdutoId).HasColumnName("TamanhoProdutoId").IsRequired().HasColumnType("int");

            HasRequired(p => p.GrupoProduto).WithMany(p => p.GruposProdutoTamanhosProduto).HasForeignKey(p => p.GrupoProdutoId);
            HasRequired(p => p.TamanhoProduto).WithMany(p => p.GruposProdutoTamanhosProduto).HasForeignKey(p => p.TamanhoProdutoId);
        }
    }
}
