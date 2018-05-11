using Garcom.Dominio.Entidade.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Garcom.Infra.DBEscrita.Mapeamento
{
    internal class TamanhoProdutoConfiguracao : EntityTypeConfiguration<TamanhoProduto>
    {
        public TamanhoProdutoConfiguracao()
        {
            this.ToTable("TAMANHO_PRODUTO");

            this.HasKey(p => p.Id);

            this.Property(p => p.Id).HasColumnName("Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.Nome).HasColumnName("Nome").IsRequired().HasColumnType("varchar").HasMaxLength(100);
            this.Property(p => p.Ativo).HasColumnName("Ativo").IsRequired().HasColumnType("bit");
            this.Property(p => p.DataCadastro).HasColumnName("DataCadastro").IsRequired();
            this.Property(p => p.Excluido).HasColumnName("Excluido").IsRequired().HasColumnType("bit");
        }
    }
}
