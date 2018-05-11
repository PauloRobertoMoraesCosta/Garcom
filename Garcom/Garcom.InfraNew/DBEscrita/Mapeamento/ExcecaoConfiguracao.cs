using Garcom.Dominio.Entidade.Models.Excecao;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Garcom.Infra.DBEscrita.Mapeamento
{
    internal class ExcecaoConfiguracao : EntityTypeConfiguration<Excecao>
    {
        public ExcecaoConfiguracao()
        {
            this.ToTable("EXCECAO");

            this.HasKey(p => p.Id);

            this.Property(p => p.Id).HasColumnName("Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.Rotina).HasColumnName("Rotina").IsRequired().HasColumnType("varchar").HasMaxLength(100);
            this.Property(p => p.Mensagem).HasColumnName("Mensagem").IsRequired().HasColumnType("varchar").HasMaxLength(800);
            this.Property(p => p.Parametros).HasColumnName("Tabela").IsOptional().HasColumnType("varchar").HasMaxLength(100);
            this.Property(p => p.DataCadastro).HasColumnName("DataCadastro").IsRequired();

            this.Ignore(p => p.Excluido);
        }
    }
}
