using Garcom.Dominio.Entidade.Models;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Garcom.Infra.DBEscrita.Mapeamento
{
    public class MesaConfiguracao : EntityTypeConfiguration<Mesa>
    {
        public MesaConfiguracao()
        {
            this.ToTable("MESA");

            this.HasKey(p => p.Id);

            this.Property(p => p.Id).HasColumnName("Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.Descricao).HasColumnName("Descricao").IsRequired().HasColumnType("varchar").HasMaxLength(100);
            this.Property(p => p.DataCadastro).HasColumnName("DataCadastro").IsRequired();
            this.Property(p => p.Ativo).HasColumnName("Ativo").IsRequired().HasColumnType("bit");
            this.Property(p => p.Excluido).HasColumnName("Excluido").IsRequired().HasColumnType("bit");
        }
        
    }
}
