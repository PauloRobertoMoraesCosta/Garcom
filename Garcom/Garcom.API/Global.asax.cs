using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Garcom.Dominio.Entidade.Mapeamento;
using System.Data.Entity;
using Garcom.Infra.DBEscrita;


namespace Garcom.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MapperConfig.ConfigurarMapper();
            ContextoEscrita contexto = new ContextoEscrita();
            Database.SetInitializer<ContextoEscrita>(
                new MigrateDatabaseToLatestVersion<ContextoEscrita, Garcom.Infra.Migrations.Configuration>());
            contexto.Database.Initialize(false);

        }

    }
}
