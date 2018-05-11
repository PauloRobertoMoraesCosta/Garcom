using System.Collections.Generic;

namespace Garcom.Dominio.Entidade.Models.Auditoria
{
    public class ChangeLog
    {
        public string EntityName
        {
            get
            {
                if (this.Entity.Contains("Mesa"))
                    return "Mesa";
                else if (this.Entity.Contains("ProdutoIngredienteTamanhoProduto"))
                    return "Produto_Ingrediente_Tamanho_produto";
                else if (this.Entity.Contains("ProdutoIngrediente"))
                    return "Produto_Ingrediente";
                else if (this.Entity.Contains("TamanhoProdutoValor"))
                    return "Tamanho_Produto_Valor";
                else if (this.Entity.Contains("TamanhoProduto"))
                    return "Tamanho_Produto";
                else if (this.Entity.Contains("GrupoProduto"))
                    return "Grupo_Produto";
                else if (this.Entity.Contains("Produto"))
                    return "Produto";
                else if (this.Entity.Contains("Ingrediente"))
                    return "Ingrediente";
                else if (this.Entity.Contains("Perfil"))
                    return "Perfil";
                else if (this.Entity.Contains("Usuario"))
                    return "Usuario";
                else
                    return string.Empty;
            }
        }
        public string Entity { get; set; }
        public string PrimaryKeyValue { get; set; }
        public Acao State { get; set; }
        public ICollection<PropertyChangeLog> PropertiesChangeLog { get; set; }

        public ChangeLog()
        {
            this.PropertiesChangeLog = new List<PropertyChangeLog>();
        }
    }
}
