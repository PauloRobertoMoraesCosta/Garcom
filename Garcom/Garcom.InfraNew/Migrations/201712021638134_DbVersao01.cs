namespace Garcom.Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DbVersao01 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AUDITORIA",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Acao = c.Byte(nullable: false),
                        Usuario = c.String(nullable: false, maxLength: 20, unicode: false),
                        Tabela = c.String(nullable: false, maxLength: 80, unicode: false),
                        Chave = c.Int(nullable: false),
                        ValoresAntigos = c.String(maxLength: 1000, unicode: false),
                        NovosValores = c.String(maxLength: 1000, unicode: false),
                        DataCadastro = c.DateTime(nullable: false, precision: 0),
                        Excluido = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EXCECAO",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rotina = c.String(nullable: false, maxLength: 100, unicode: false),
                        Mensagem = c.String(nullable: false, maxLength: 800, unicode: false),
                        Tabela = c.String(maxLength: 100, unicode: false),
                        DataCadastro = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GRUPO_PRODUTO",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        PermiteDividir = c.Boolean(nullable: false, storeType: "bit"),
                        Ativo = c.Boolean(nullable: false),
                        DataCadastro = c.DateTime(nullable: false, precision: 0),
                        Excluido = c.Boolean(nullable: false, storeType: "bit"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GRUPO_PRODUTO_TAMANHO_PRODUTO",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ordem = c.Int(),
                        GrupoProdutoId = c.Int(nullable: false),
                        TamanhoProdutoId = c.Int(nullable: false),
                        DataCadastro = c.DateTime(nullable: false, precision: 0),
                        Excluido = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GRUPO_PRODUTO", t => t.GrupoProdutoId, true)
                .ForeignKey("dbo.TAMANHO_PRODUTO", t => t.TamanhoProdutoId, true)
                .Index(t => t.GrupoProdutoId)
                .Index(t => t.TamanhoProdutoId);
            
            CreateTable(
                "dbo.TAMANHO_PRODUTO",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        Ativo = c.Boolean(nullable: false, storeType: "bit"),
                        DataCadastro = c.DateTime(nullable: false, precision: 0),
                        Excluido = c.Boolean(nullable: false, storeType: "bit"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PRODUTO_INGREDIENTE_TAMANHO_VALOR",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProdutoIngredienteId = c.Int(nullable: false),
                        TamanhoProdutoId = c.Int(nullable: false),
                        Valor = c.Double(),
                        DataCadastro = c.DateTime(nullable: false, precision: 0),
                        Excluido = c.Boolean(nullable: false, storeType: "bit"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PRODUTO_INGREDIENTE", t => t.ProdutoIngredienteId, true)
                .ForeignKey("dbo.TAMANHO_PRODUTO", t => t.TamanhoProdutoId, true)
                .Index(t => t.ProdutoIngredienteId)
                .Index(t => t.TamanhoProdutoId);
            
            CreateTable(
                "dbo.PRODUTO_INGREDIENTE",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProdutoId = c.Int(nullable: false),
                        IngredienteId = c.Int(nullable: false),
                        Opcional = c.Boolean(nullable: false, storeType: "bit"),
                        Adicional = c.Boolean(nullable: false, storeType: "bit"),
                        ValorAdicional = c.Double(),
                        DataCadastro = c.DateTime(nullable: false, precision: 0),
                        Excluido = c.Boolean(nullable: false, storeType: "bit"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.INGREDIENTE", t => t.IngredienteId, true)
                .ForeignKey("dbo.PRODUTO", t => t.ProdutoId, true)
                .Index(t => t.ProdutoId)
                .Index(t => t.IngredienteId);
            
            CreateTable(
                "dbo.INGREDIENTE",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 100, unicode: false),
                        Ativo = c.Boolean(nullable: false),
                        DataCadastro = c.DateTime(nullable: false, precision: 0),
                        Excluido = c.Boolean(nullable: false, storeType: "bit"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PRODUTO",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        Valor = c.Double(),
                        GrupoProdutoId = c.Int(),
                        Ativo = c.Boolean(nullable: false, storeType: "bit"),
                        NomeImagem = c.String(maxLength: 255, unicode: false),
                        CodigoRapido = c.String(maxLength: 20, unicode: false),
                        DataCadastro = c.DateTime(nullable: false, precision: 0),
                        Excluido = c.Boolean(nullable: false, storeType: "bit"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GRUPO_PRODUTO", t => t.GrupoProdutoId)
                .Index(t => t.GrupoProdutoId);
            
            CreateTable(
                "dbo.PRODUTO_TAMANHO_VALOR",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProdutoId = c.Int(nullable: false),
                        TamanhoProdutoId = c.Int(nullable: false),
                        Valor = c.Double(nullable: false),
                        DataCadastro = c.DateTime(nullable: false, precision: 0),
                        Excluido = c.Boolean(nullable: false, storeType: "bit"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PRODUTO", t => t.ProdutoId, true)
                .ForeignKey("dbo.TAMANHO_PRODUTO", t => t.TamanhoProdutoId, true)
                .Index(t => t.ProdutoId)
                .Index(t => t.TamanhoProdutoId);
            
            CreateTable(
                "dbo.MESA",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 100, unicode: false),
                        Ativo = c.Boolean(nullable: false, storeType: "bit"),
                        DataCadastro = c.DateTime(nullable: false, precision: 0),
                        Excluido = c.Boolean(nullable: false, storeType: "bit"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PERFIL",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 45, unicode: false),
                        DataCadastro = c.DateTime(nullable: false, precision: 0),
                        Excluido = c.Boolean(nullable: false, storeType: "bit"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.USUARIO",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 20, unicode: false),
                        Senha = c.String(nullable: false, maxLength: 32, unicode: false),
                        Nome = c.String(nullable: false, maxLength: 100, unicode: false),
                        PerfilId = c.Int(nullable: false),
                        Ativo = c.Boolean(nullable: false, storeType: "bit"),
                        DataCadastro = c.DateTime(nullable: false, precision: 0),
                        Excluido = c.Boolean(nullable: false, storeType: "bit"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PERFIL", t => t.PerfilId)
                .Index(t => t.PerfilId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.USUARIO", "PerfilId", "dbo.PERFIL");
            DropForeignKey("dbo.GRUPO_PRODUTO_TAMANHO_PRODUTO", "TamanhoProdutoId", "dbo.TAMANHO_PRODUTO");
            DropForeignKey("dbo.PRODUTO_INGREDIENTE_TAMANHO_VALOR", "TamanhoProdutoId", "dbo.TAMANHO_PRODUTO");
            DropForeignKey("dbo.PRODUTO_INGREDIENTE_TAMANHO_VALOR", "ProdutoIngredienteId", "dbo.PRODUTO_INGREDIENTE");
            DropForeignKey("dbo.PRODUTO_INGREDIENTE", "ProdutoId", "dbo.PRODUTO");
            DropForeignKey("dbo.PRODUTO_TAMANHO_VALOR", "TamanhoProdutoId", "dbo.TAMANHO_PRODUTO");
            DropForeignKey("dbo.PRODUTO_TAMANHO_VALOR", "ProdutoId", "dbo.PRODUTO");
            DropForeignKey("dbo.PRODUTO", "GrupoProdutoId", "dbo.GRUPO_PRODUTO");
            DropForeignKey("dbo.PRODUTO_INGREDIENTE", "IngredienteId", "dbo.INGREDIENTE");
            DropForeignKey("dbo.GRUPO_PRODUTO_TAMANHO_PRODUTO", "GrupoProdutoId", "dbo.GRUPO_PRODUTO");
            DropIndex("dbo.USUARIO", new[] { "PerfilId" });
            DropIndex("dbo.PRODUTO_TAMANHO_VALOR", new[] { "TamanhoProdutoId" });
            DropIndex("dbo.PRODUTO_TAMANHO_VALOR", new[] { "ProdutoId" });
            DropIndex("dbo.PRODUTO", new[] { "GrupoProdutoId" });
            DropIndex("dbo.PRODUTO_INGREDIENTE", new[] { "IngredienteId" });
            DropIndex("dbo.PRODUTO_INGREDIENTE", new[] { "ProdutoId" });
            DropIndex("dbo.PRODUTO_INGREDIENTE_TAMANHO_VALOR", new[] { "TamanhoProdutoId" });
            DropIndex("dbo.PRODUTO_INGREDIENTE_TAMANHO_VALOR", new[] { "ProdutoIngredienteId" });
            DropIndex("dbo.GRUPO_PRODUTO_TAMANHO_PRODUTO", new[] { "TamanhoProdutoId" });
            DropIndex("dbo.GRUPO_PRODUTO_TAMANHO_PRODUTO", new[] { "GrupoProdutoId" });
            DropTable("dbo.USUARIO");
            DropTable("dbo.PERFIL");
            DropTable("dbo.MESA");
            DropTable("dbo.PRODUTO_TAMANHO_VALOR");
            DropTable("dbo.PRODUTO");
            DropTable("dbo.INGREDIENTE");
            DropTable("dbo.PRODUTO_INGREDIENTE");
            DropTable("dbo.PRODUTO_INGREDIENTE_TAMANHO_VALOR");
            DropTable("dbo.TAMANHO_PRODUTO");
            DropTable("dbo.GRUPO_PRODUTO_TAMANHO_PRODUTO");
            DropTable("dbo.GRUPO_PRODUTO");
            DropTable("dbo.EXCECAO");
            DropTable("dbo.AUDITORIA");
        }
    }
}
