namespace Garcom.Infra.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Dominio.Entidade.Models;
    using System.Collections.Generic;
    using Core;

    public class Configuration : DbMigrationsConfiguration<Garcom.Infra.DBEscrita.ContextoEscrita>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
            
        }

        protected override void Seed(Garcom.Infra.DBEscrita.ContextoEscrita context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );

            //var p = new perfil[]
            //{
            //    new perfil("administrador"),
            //    new perfil("caixa"),
            //    new perfil("cozinha"),
            //    new perfil("garçom")
            //};

            //var u = new usuario[]
            //{
            //    new usuario("usuario", seguranca.encryptstring("usuario1"), "usuario", 1, true)
            //};

            //context.perfies.addorupdate(i => i.descricao, p);
            //context.usuarios.addorupdate(i => i.login, u);
        }
    }
}
