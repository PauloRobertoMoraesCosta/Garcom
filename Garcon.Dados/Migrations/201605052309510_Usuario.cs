namespace Garcom.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Usuario : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ativo = c.String(maxLength: 5, unicode: false),
                        Login = c.String(nullable: false, maxLength: 20, unicode: false),
                        Senha = c.String(nullable: false, maxLength: 15, unicode: false),
                        Nome = c.String(nullable: false, maxLength: 150, unicode: false),
                        Data_Cadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Login, unique: true, name: "UN_Usuario");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Usuario", "UN_Usuario");
            DropTable("dbo.Usuario");
        }
    }
}
