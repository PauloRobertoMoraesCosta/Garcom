using Garcom.Dominio.Entidades;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace Garcom.Dados.EntiConfig
{
    public class UsuarioConfig : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfig()
        {
            HasKey(u => u.Id);

            Property(u => u.Login).IsRequired().HasMaxLength(20).HasColumnAnnotation(IndexAnnotation.AnnotationName, new IndexAnnotation(new IndexAttribute("UN_Usuario") { IsUnique = true }));
            Property(u => u.Senha).IsRequired().HasMaxLength(15);
            Property(u => u.Nome).IsRequired().HasMaxLength(150);
            Property(u => u.DataCadastro).HasColumnName("Data_Cadastro");
            Property(u => u.Ativo).HasMaxLength(5);
            
            this.Ignore(u => u.AtivoBool);
            
            
        }
    }
}
