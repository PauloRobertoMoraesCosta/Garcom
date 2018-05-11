using Garcom.Dominio.Entidade.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Garcom.Infra.DBEscrita.Mapeamento
{
    internal class UsuarioConfiguracao : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfiguracao()
        {
            this.ToTable("USUARIO");

            this.HasKey(p => p.Id);

            Property(p => p.Id).HasColumnName("Id").IsRequired().HasColumnType("int").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(p => p.Login).HasColumnName("Login").IsRequired().HasColumnType("varchar").HasMaxLength(20);
            this.Property(p => p.Senha).HasColumnName("Senha").IsRequired().HasColumnType("varchar").HasMaxLength(32);
            this.Property(p => p.Nome).HasColumnName("Nome").IsRequired().HasColumnType("varchar").HasMaxLength(100);
            this.Property(p => p.PerfilId).HasColumnName("PerfilId").IsRequired().HasColumnType("int");
            this.Property(p => p.DataCadastro).HasColumnName("DataCadastro").IsRequired();
            this.Property(p => p.Ativo).HasColumnName("Ativo").IsRequired().HasColumnType("bit");
            this.Property(p => p.Excluido).HasColumnName("Excluido").IsRequired().HasColumnType("bit");

            this.HasRequired(p => p.Perfil).WithMany(p => p.Usuarios).HasForeignKey(p => p.PerfilId);
        }
    }
}
