using Garcom.Dominio.Entidade.Models.Auditoria;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Garcom.Infra.DBEscrita.Mapeamento
{
    internal class AuditoriaConfiguracao : EntityTypeConfiguration<Auditoria>
    {
        public AuditoriaConfiguracao()
        {
            this.ToTable("AUDITORIA");

            this.HasKey(p => p.Id);

            this.Property(p => p.Id).HasColumnName("Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.Acao).HasColumnName("Acao").IsRequired();
            this.Property(p => p.Usuario).HasColumnName("Usuario").IsRequired().HasColumnType("varchar").HasMaxLength(20);
            this.Property(p => p.Tabela).HasColumnName("Tabela").IsRequired().HasColumnType("varchar").HasMaxLength(80);
            this.Property(p => p.Chave).HasColumnName("Chave").IsRequired().HasColumnType("int");
            this.Property(p => p.ValoresAntigos).HasColumnName("ValoresAntigos").IsOptional().HasColumnType("varchar");
            this.Property(p => p.NovosValores).HasColumnName("NovosValores").IsOptional().HasColumnType("varchar");
            this.Property(p => p.DataCadastro).HasColumnName("DataCadastro").IsRequired();
        }
    }
}
