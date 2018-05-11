using Garcom.Dominio.Entidade.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Garcom.Infra.DBEscrita.Mapeamento
{
    internal class ProdutoConfiguracao : EntityTypeConfiguration<Produto>
    {
        public ProdutoConfiguracao()
        {
            this.ToTable("PRODUTO");

            this.HasKey(p => p.Id);

            this.Property(p => p.Id).HasColumnName("Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.Nome).HasColumnName("Nome").IsRequired().HasColumnType("varchar").HasMaxLength(100);
            this.Property(p => p.Valor).HasColumnName("Valor").IsOptional().HasColumnType("double");
            this.Property(p => p.GrupoProdutoId).HasColumnName("GrupoProdutoId").IsOptional().HasColumnType("int");
            this.Property(p => p.Ativo).HasColumnName("Ativo").IsRequired().HasColumnType("bit");
            this.Property(p => p.DataCadastro).HasColumnName("DataCadastro").IsRequired();
            this.Property(p => p.Excluido).HasColumnName("Excluido").IsRequired().HasColumnType("bit");
            this.Property(p => p.NomeImagem).HasColumnName("NomeImagem").IsOptional().HasColumnType("varchar").HasMaxLength(255);
            this.Property(p => p.CodigoRapido).HasColumnName("CodigoRapido").IsOptional().HasColumnType("varchar").HasMaxLength(20);

            this.HasOptional(p => p.GrupoProduto).WithMany(p => p.Produtos).HasForeignKey(p => p.GrupoProdutoId);
        }
    }
}
