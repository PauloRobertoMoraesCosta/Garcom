using Garcom.Dominio.Entidade.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Garcom.Infra.DBEscrita.Mapeamento
{
    internal class PerfilConfiguracao : EntityTypeConfiguration<Perfil>
    {
        public PerfilConfiguracao()
        {
            this.ToTable("PERFIL");

            this.HasKey(p => p.Id);

            this.Property(p => p.Id).HasColumnName("Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.Descricao).HasColumnName("Descricao").IsRequired().HasColumnType("varchar").HasMaxLength(45);
            this.Property(p => p.DataCadastro).HasColumnName("DataCadastro").IsRequired();
            this.Property(p => p.Excluido).HasColumnName("Excluido").IsRequired().HasColumnType("bit");
        }
    }
}
